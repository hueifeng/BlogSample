using System;

namespace LoadBalanceDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //随机法
            for (int i = 0; i < 10; i++)
            {
                var server =RandomBalance.GetServer();
                Console.WriteLine(server[0]);
            }


            Console.ReadLine();
        }
    }
}
