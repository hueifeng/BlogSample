using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ObjectPoolBlog
{
    class Program
    {
        static void Main(string[] args)
        {
            CancellationTokenSource cts = new CancellationTokenSource();

            // Create an opportunity for the user to cancel.
            Task.Run(() =>
            {
                if (Console.ReadKey().KeyChar == 'c' || Console.ReadKey().KeyChar == 'C')
                    cts.Cancel();
            });

            ObjectPool<MyClass> pool = new ObjectPool<MyClass>(() => new MyClass());

            // Create a high demand for MyClass objects.
            Parallel.For(0, 1000000, (i, loopState) =>
            {
                MyClass mc = pool.CheckOut();
                Console.CursorLeft = 0;
                // This is the bottleneck in our application. All threads in this loop
                // must serialize their access to the static Console class.
                Console.WriteLine("{0:####.####}", mc.GetValue(i));

                pool.CheckIn(mc);
                if (cts.Token.IsCancellationRequested)
                    loopState.Stop();

            });
            Console.WriteLine("Press the Enter key to exit.");
            Console.ReadLine();
            cts.Dispose();
        }

        class MyClass
        {
            public int[] Nums { get; set; }
            public double GetValue(long i)
            {
                return Math.Sqrt(Nums[i]);
            }
            public MyClass()
            {
                Nums = new int[1000000];
                Random rand = new Random();
                for (int i = 0; i < Nums.Length; i++)
                    Nums[i] = rand.Next();
            }
        }


    }
}
