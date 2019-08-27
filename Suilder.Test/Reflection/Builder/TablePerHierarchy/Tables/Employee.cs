namespace Suilder.Test.Reflection.Builder.TablePerHierarchy.Tables
{
    public class Employee : Person
    {
        public virtual decimal Salary { get; set; }

        public virtual int DepartmentId { get; set; }

        public virtual Department Department { get; set; }
    }
}