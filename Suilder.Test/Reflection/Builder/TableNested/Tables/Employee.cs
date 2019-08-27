namespace Suilder.Test.Reflection.Builder.TableNested.Tables
{
    public class Employee
    {
        public Address Address { get; set; }

        public decimal Salary { get; set; }

        public int DepartmentId { get; set; }

        public Department Department { get; set; }
    }
}