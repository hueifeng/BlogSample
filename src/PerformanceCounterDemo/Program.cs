using System;
using System.Diagnostics;
using System.Threading;

namespace PerformanceCounterDemo
{
    class Program
    {
        private const float KB = 1024f;

        static void Main(string[] args)
        {
            //创建对CPU占用百分比的性能检测器
            //PerformanceCounter cupCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            var cpuCounterPF = new PerformanceCounter("Processor", "% Processor Time", "_Total", true);
            var availableMemoryCounterPF = new PerformanceCounter("Memory", "Available Bytes", true);
            string thisProcess = Process.GetCurrentProcess().ProcessName;
            PerformanceCounterCategory cat = new PerformanceCounterCategory("Process");

            string[] instances = cat.GetInstanceNames();
            var process = Process.GetCurrentProcess();
            thisProcess = GetProcessInstanceName(process.Id);
            var genSizesPF = new PerformanceCounter[] {
                new PerformanceCounter(".NET CLR Memory", "Gen 0 heap size", thisProcess, true),
                new PerformanceCounter(".NET CLR Memory", "Gen 1 heap size", thisProcess, true),
                new PerformanceCounter(".NET CLR Memory", "Gen 2 heap size", thisProcess, true)
            };
            for (int i = 0; i < 10; i++)
            {
                //    //获取一个计数器sample，去计算获取原始或者未计算的值
                //    CounterSample cs1 = cupCounter.NextSample();
                Thread.Sleep(100);
                //    //获取第二个计数器sample，用于通过前一个sample值去计算出cpu占用率
                //    CounterSample cs2 = cupCounter.NextSample();
                //    //使用两个计数器sample的性能数据，此处用于计算性能类型以及两个计数器的平均值，
                //    //对于线程等待的这区间获取出两个值.
                //    float finalCpuCounter = CounterSample.Calculate(cs1, cs2);

                //Console.WriteLine(finalCpuCounter);
                //Console.WriteLine(cupCounter.NextValue());
                var availableMemory = availableMemoryCounterPF.NextValue();
                //Console.WriteLine((long)((AvailableMemory / KB) / KB));
                Console.WriteLine(
                    $"gen0={genSizesPF[0].NextValue() / KB:0.00}, gen1={genSizesPF[1].NextValue() / KB:0.00}, gen2={genSizesPF[2].NextValue() / KB:0.00}");
            }

            Console.ReadLine();
        }
        public static string GetProcessInstanceName(int process_id)
        {
            PerformanceCounterCategory cat = new PerformanceCounterCategory("Process");
            string[] instances = cat.GetInstanceNames();
            foreach (string instance in instances)
            {
                using (PerformanceCounter cnt = new PerformanceCounter("Process", "ID Process", instance, true))
                {
                    int val = (int)cnt.RawValue;
                    if (val == process_id)
                        return instance;
                }
            }
            throw new Exception("Could not find performance counter ");
        }
    }
}
