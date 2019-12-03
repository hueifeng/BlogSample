using System;

namespace LazyTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Lazy<User> user = new Lazy<User>();
            if (!user.IsValueCreated)
                Console.WriteLine("The object is not initialized");
            Console.WriteLine(user.Value.Name);
            user.Value.Name = "Name1";
            user.Value.Age = 1;
            Console.WriteLine(user.Value.Name);
            Console.Read();
        }
    }
}
