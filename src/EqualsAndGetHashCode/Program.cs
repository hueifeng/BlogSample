using System;
using System.Collections.Generic;
using System.Linq;

namespace EqualsAndGetHashCode
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Student> students = new List<Student>
            {
                new Student{ Name = "MR.A", Age = 32},
                new Student{ Name = "MR.B", Age = 34},
                new Student{ Name = "MR.A", Age = 32}
            };
            Console.WriteLine("distinctStudents has Count = {0}", students.Distinct().Count());
           //Console.WriteLine("distinctStudents has Count = {0}", students.Distinct(new StudentComparator()).Count());

            //var stu1 = new Student { Name = "MR.A", Age = 32 };
            //var stu2 = new Student { Name = "MR.A", Age = 32 };

            //bool result = stu1.Equals(stu2); //false because it's reference Equals
            //Console.WriteLine(result);
            Console.ReadLine();
        }
    }
}
