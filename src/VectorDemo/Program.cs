using System;
using System.Numerics;

namespace VectorDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(
                VectorDot(3, 4));
            Console.ReadLine();
        }


        public static double VectorDot(float left, float right)
        {
            Vector3 a = new Vector3(left);
            Vector3 b = new Vector3(right);

            float dotProduct = Vector3.Dot(a, b);

            return dotProduct;
        }
    }
}
