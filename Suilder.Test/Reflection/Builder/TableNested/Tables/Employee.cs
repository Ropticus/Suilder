namespace Suilder.Test.Reflection.Builder.TableNested.Tables
{
    public class Employee
    {
        public virtual Address Address { get; set; }

        public virtual decimal Salary { get; set; }

        public virtual int DepartmentId { get; set; }

        public virtual Department Department { get; set; }

        public virtual byte[] Image { get; set; }
    }
}