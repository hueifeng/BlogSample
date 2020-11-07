using System;
using System.Diagnostics;
using System.Threading;

namespace PerformanceCounterDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //创建对CPU占用百分比的性能检测器
            PerformanceCounter cupCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");

            for (int i = 0; i < 10; i++)
            {
                //获取一个计数器sample，去计算获取原始或者未计算的值
                CounterSample cs1 = cupCounter.NextSample();
                Thread.Sleep(100);
                //获取第二个计数器sample，用于通过前一个sample值去计算出cpu占用率
                CounterSample cs2 = cupCounter.NextSample();
                //使用两个计数器sample的性能数据，此处用于计算性能类型以及两个计数器的平均值，
                //对于线程等待的这区间获取出两个值.
                float finalCpuCounter = CounterSample.Calculate(cs1, cs2);
               
                Console.WriteLine(CounterSample.Calculate(cs1));
            }

            Console.ReadLine();
        }
    }
}
