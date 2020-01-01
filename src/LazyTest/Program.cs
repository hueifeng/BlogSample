using System;
using System.Threading;

namespace LazyTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Lazy<User> user = new Lazy<User>();
            ThreadLocal<User> threadLocal = new ThreadLocal<User>();

            if (!user.IsValueCreated)
                Console.WriteLine("The object is not initialized");
            Console.WriteLine(user.Value.Name);
            user.Value.Name = "Name1";
            user.Value.Age = 1;
            Console.WriteLine(user.Value.Name);
            Console.Read();


            Lazy<int> number = new Lazy<int>(() => Thread.CurrentThread.ManagedThreadId);

            Thread t1 = new Thread(() => Console.WriteLine("number on t1 = {0} ThreadID = {1}",
                                                    number.Value, Thread.CurrentThread.ManagedThreadId));
            t1.Start();

            Thread t2 = new Thread(() => Console.WriteLine("number on t2 = {0} ThreadID = {1}",
                                                    number.Value, Thread.CurrentThread.ManagedThreadId));
            t2.Start();

            Thread t3 = new Thread(() => Console.WriteLine("number on t3 = {0} ThreadID = {1}", number.Value,
                                                    Thread.CurrentThread.ManagedThreadId));
            t3.Start();

            // Ensure that thread IDs are not recycled if the 
            // first thread completes before the last one starts.
            t1.Join();
            t2.Join();
            t3.Join();
            Console.Read();





            //Lazy<int> number = new Lazy<int>(() => Thread.CurrentThread.ManagedThreadId);

            //Thread t1 = new Thread(() => Console.WriteLine("number on t1 = {0} ThreadID = {1}",
            //                                        number.Value, Thread.CurrentThread.ManagedThreadId));
            //t1.Start();

            //Thread t2 = new Thread(() => Console.WriteLine("number on t2 = {0} ThreadID = {1}",
            //                                        number.Value, Thread.CurrentThread.ManagedThreadId));
            //t2.Start();

            //Thread t3 = new Thread(() => Console.WriteLine("number on t3 = {0} ThreadID = {1}", number.Value,
            //                                        Thread.CurrentThread.ManagedThreadId));
            //t3.Start();

            //// Ensure that thread IDs are not recycled if the 
            //// first thread completes before the last one starts.
            //t1.Join();
            //t2.Join();
            //t3.Join();
            //Console.Read();


            //ThreadLocal<int> threadLocalNumber = new ThreadLocal<int>(() => Thread.CurrentThread.ManagedThreadId);
            //Thread t4 = new Thread(() => Console.WriteLine("threadLocalNumber on t4 = {0} ThreadID = {1}",
            //                                    threadLocalNumber.Value, Thread.CurrentThread.ManagedThreadId));
            //t4.Start();

            //Thread t5 = new Thread(() => Console.WriteLine("threadLocalNumber on t5 = {0} ThreadID = {1}",
            //                                    threadLocalNumber.Value, Thread.CurrentThread.ManagedThreadId));
            //t5.Start();

            //Thread t6 = new Thread(() => Console.WriteLine("threadLocalNumber on t6 = {0} ThreadID = {1}",
            //                                    threadLocalNumber.Value, Thread.CurrentThread.ManagedThreadId));
            //t6.Start();

            //// Ensure that thread IDs are not recycled if the 
            //// first thread completes before the last one starts.
            //t4.Join();
            //t5.Join();
            //t6.Join();
            //Console.Read();
        }
    }
}
