using System;
using System.Numerics;

namespace VectorDemo
{
    public class VectorExample
    {
        Vector<double> douZero = Vector<double>.Zero;// douZero.ToString() shows: <0, 0, 0, 0>
        Vector<float> flOne = Vector<float>.One;// <1, 1, 1, 1, 1, 1, 1, 1>
        Vector<ushort> shAny = new Vector<ushort>(43);

        public static void Run()
        {
            //创建Vector 重复相同的值
            Vector<double> douZero = Vector<double>.Zero;   // douZero.ToString() shows: <0, 0, 0, 0>
            Vector<float> flOne = Vector<float>.One;    // <1, 1, 1, 1, 1, 1, 1, 1>
            Vector<ushort> shAny = new Vector<ushort>(43);

            //不同的值
            double[] doubArray = new double[] { 1, 2, 3, 4, 4, 3, 2, 1, -1, -2, -3, -4, -5 };
            Span<double> douSpan = new Span<double>(doubArray, 8, 4);

            Vector<double> douV = new Vector<double>(doubArray); //Will contain <1, 2, 3, 4>
            Vector<double> spanduoV = new Vector<double>(douSpan); //Will contain <-1, -2, -3, -4>
            Vector<double> dou2V = new Vector<double>(doubArray, 5); //Will contain <3, 2, 1, -1>
            Vector<double> sumV = douV + dou2V; //Will contain <4, 4, 4, 3>

            //针对不同的硬件平台进行编程
            if (Vector.IsHardwareAccelerated == false)
            {
                //IsHardwareAccelerated会判断当前硬件是否支持该功能

                //fallback to some other code;
                return;
            }

            //Count一个特性的Vector类型，可以获取我们可以同时处理多少个T类型，允许我们以较大的快来处理数据
            int bitWidth = Vector<byte>.Count * 8; // bitWidth will contain the available vector size in bits

            int floatSlots = Vector<float>.Count;// floatSlots will contain the number of floats per vector;

            int i;
            for (i = 0; i < doubArray.Length; i += floatSlots)
            {
                //do some useful stuff
            }
            for (; i < doubArray.Length; i++)
            {
                //handle data left over (if any) in serial fashion
            }
            //检索Vector值
            double[] sumArray = new double[48]; //Will hold several result values

            sumV.CopyTo(sumArray); //Copies the vector values to start of array
            sumV.CopyTo(sumArray, 8); //Copies the vector values to array starting at index 8

            double lastVal = sumV[3]; //Retrieves the 4th value from vector, read only!

            //向量比较
            //Results in a AVX / AVX 2 system
            float[] flArray = new float[] { 1, 2, 3, 4, 4, 3, 2, 1, -1, -2, -3, -4, -5 };
            ushort[] ushArray = new ushort[] { 1, 2, 3, 4, 5, 6, 7, 8, 8, 7, 6, 5, 4, 3, 2, 1, 0, 1, 2, 3, 4 };
            ulong[] ulARray = new ulong[] { 1, 2, 3, 4, 4, 3, 2, 1 };

            Vector<double> left = new Vector<double>(doubArray); //<1, 2, 3, 4>
            Vector<double> right = new Vector<double>(doubArray, 4); //<4, 3, 2, 1>
            Vector<float> leftFl = new Vector<float>(flArray); //<1, 2, 3, 4, 4, 3, 2, 1>
            Vector<float> rightFl = new Vector<float>(flArray, 4); //<4, 3, 2, 1, -1, -2, -3, -4>
            Vector<ushort> leftUSh = new Vector<ushort>(ushArray); //<1, 2, 3, 4, 5, 6, 7, 8, 8, 7, 6, 5, 4, 3, 2, 1>
            Vector<ushort> rightUSh = new Vector<ushort>(ushArray, 2); //<3, 4, 5, 6, 7, 8, 8, 7, 6, 5, 4, 3, 2, 1, 0, 1>
            Vector<ulong> leftUl = new Vector<ulong>(ulARray);
            Vector<ulong> rightUl = new Vector<ulong>(ulARray, 4);

            Vector<long> lessThan = Vector.LessThan(left, right);//lessThan = <-1, -1, 0, 0>
            Vector<int> lessThanInt = Vector.LessThan(leftFl, rightFl); //lessThanInt = <-1, -1, 0, 0, 0, 0, 0, 0>
            Vector<ushort> greaterEqual = Vector.GreaterThanOrEqual(leftUSh, rightUSh); //greaterEqual = <0, 0, 0, 0, 0, 0, 0, 65535, 65535, 65535, 65535, 65535, 65535, 65535, 65535, 65535>
            Vector<ulong> lessThanul = Vector.LessThan(leftUl, rightUl);// lessTahUl = <18446744073709551615, 18446744073709551615, 0, 0>

            //转换
            Vector<double> minOne = new Vector<double>(-1);//<-1, -1, -1, -1>
            Vector<long> convToLong = Vector.ConvertToInt64(minOne);//<-1, -1, -1, -1>

            //位运算
            Vector<byte> byteF0 = new Vector<byte>(0b11110000);
            Vector<byte> onesComp = Vector.OnesComplement(byteF0);//onesComp = <15, 15, ...> = <0b00001111, ...>

            //数学运算
            double[] douArray = new double[] { 1, 3, 5, 7, 2, 4, 6, 8, 9, 11, 13, 15, 10, 12, 14, 16 };

            Vector<int> Four = new Vector<int>(4);
            Vector<double> aVector = new Vector<double>(douArray); //<1, 3, 5, 7>
            Vector<double> otherVector = new Vector<double>(douArray, 4);// <2, 4, 6, 8>

            Vector<double> resVDou = aVector * otherVector;// resVDou = <2, 12, 30, 56>
            double resDou = Vector.Dot(aVector, otherVector);// resDou = 2 + 12 + 30 + 56 = 100
            Vector<int> resVInt = Vector.SquareRoot(Four);// resVint = <2, 2, 2, 2, 2, 2, 2, 2>
            resVDou = Vector.Max(aVector, new Vector<double>(4));// resVDou = <4, 4, 5, 7>
            resVDou = Vector.Negate(resVDou);//resVDou = <-4, -4, -5, -7>
            Vector<ushort> resVUSort = Vector.Negate(Vector<ushort>.One);
            // resVUShort = <65534, 65534, 65534, 65534, 65534, 65534, 65534, 65534, 65534, 65534, 65534, 65534, 65534, 65534, 65534, 65534>

        }

    }
}
