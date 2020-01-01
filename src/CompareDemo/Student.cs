using System;
using System.Diagnostics.CodeAnalysis;

namespace CompareDemo
{
    class Student : IComparable<Student>
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public int CompareTo([AllowNull] Student other)
        {
            return Age.CompareTo(other.Age);
        }
        //public int CompareTo(object obj)
        //{
        //    if (!(obj is Student))
        //    {
        //        throw new ArgumentException("Compared Object is not of student");
        //    }
        //    Student student = obj as Student;
        //    return Age.CompareTo(student.Age);
        //}
    }

}
