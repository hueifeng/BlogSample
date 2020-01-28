using System.Collections.Generic;

namespace EqualsAndGetHashCode
{
    public class StudentComparator : EqualityComparer<Student>
    {
        public override bool Equals(Student x,Student y)
        {
            return x.Name == y.Name && x.Age == y.Age;
        }

        public override int GetHashCode(Student obj)
        {
            return obj.Name.GetHashCode() * obj.Age;
        }
    }
}
