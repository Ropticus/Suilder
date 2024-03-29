namespace Suilder.Test.Reflection.TablePerHierarchy.Tables
{
    public class Employee : Person
    {
        public virtual decimal Salary { get; set; }

        public virtual int DepartmentId { get; set; }

        public virtual Department Department { get; set; }

        public virtual byte[] Image { get; set; }
    }
}