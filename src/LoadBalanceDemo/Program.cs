using System;

namespace LoadBalanceDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                string server =RandomBalance.GetServer();
                Console.WriteLine(server);

            }
            Console.ReadLine();
        }
    }
}
