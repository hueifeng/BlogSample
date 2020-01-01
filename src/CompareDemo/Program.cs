using System;
using System.Collections;
using System.Collections.Generic;

namespace CompareDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Student> studentList = new List<Student>  {
                new Student{Name="a",Age=9 },
                  new Student{Name="a3",Age=7 },
                 new Student{Name="a1",Age=6 },
                 new Student{Name="a2",Age=10 },
            };
            //studentList.Sort(new StudentComparer());
            studentList.Sort();
            StudentComparable(studentList);

            Console.ReadLine();
        }

        private static void StudentComparable(List<Student> studentList)
        {
            foreach (Student item in studentList)
            {
                Console.WriteLine("Name:{0},Age:{1}", item.Name, item.Age);
            }
        }
    }
}
