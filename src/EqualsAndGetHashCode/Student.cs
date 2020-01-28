namespace EqualsAndGetHashCode
{
    public class Student
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public override bool Equals(object obj)
        {
            var stu = obj as Student;
            if (stu == null) return false;
            return Name == stu.Name && Age == stu.Age; 
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode() * Age;
        }
    }
}
