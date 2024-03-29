namespace Suilder.Test.Reflection.NoInherit.Tables
{
    public class Person
    {
        public int Id { get; set; }

        public string Guid { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string FullName => $"{Name} {Surname}".TrimEnd();

        public Address Address { get; set; }

        public int DepartmentId { get; set; }

        public Department Department { get; set; }

        public byte[] Image { get; set; }
    }
}