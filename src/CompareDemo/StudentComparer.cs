using System.Collections;

namespace CompareDemo
{
    class StudentComparer : IComparer
    {

        public int Compare(object x, object y)
        {
            Student x1 = x as Student;
            Student y1 = y as Student;
            return x1.Name.CompareTo(y1.Name);
        }
    }
}
