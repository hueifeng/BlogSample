using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace PerformanceCounterDemo
{
    class Program
    {

        static async Task Main(string[] args)
        {
            //for (int i = 0; i < 10; i++)
            //{
            //    //Console.WriteLine("fx：" + await GetCpuUsageForProcessByfx());
            //    Console.WriteLine("core：" + await GetCpuUsageForProcess());

            //}
            var task = Task.Run(() => ConsumeCPU(50));

            while (true)
            {
                await Task.Delay(2000);
                var cpuUsage = await GetCpuUsageForProcess();

                Console.WriteLine(cpuUsage);
            }
            Console.ReadLine();
        }

        public static void ConsumeCPU(int percentage)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            while (true)
            {
                if (watch.ElapsedMilliseconds > percentage)
                {
                    Thread.Sleep(100 - percentage);
                    watch.Reset();
                    watch.Start();
                }
            }
        }


        private static async Task<double> GetCpuUsageForProcess()
        {
            var startTime = DateTime.UtcNow;
            var startCpuUsage = Process.GetCurrentProcess().TotalProcessorTime;

            await Task.Delay(500);

            var endTime = DateTime.UtcNow;
            var endCpuUsage = Process.GetCurrentProcess().TotalProcessorTime;

            var cpuUsedMs = (endCpuUsage - startCpuUsage).TotalMilliseconds;
            var totalMsPassed = (endTime - startTime).TotalMilliseconds;

            var cpuUsageTotal = cpuUsedMs / (Environment.ProcessorCount * totalMsPassed);

            return cpuUsageTotal * 100;
        }

        private static async Task<int> GetCpuUsageForProcessByfx()
        {
            var currentProcessName = Process.GetCurrentProcess().ProcessName;
            var cpuCounter = new PerformanceCounter("Process", "% Processor Time", currentProcessName);
            cpuCounter.NextValue();
            await Task.Delay(500);
            return (int)cpuCounter.NextValue();
        }

    }
}
