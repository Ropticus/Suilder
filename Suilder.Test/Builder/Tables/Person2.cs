using System;
using Suilder.Reflection;

namespace Suilder.Test.Builder.Tables
{
    [Table("Person", Schema = "dbo")]
    public class Person2
    {
        [PrimaryKey]
        public string Guid { get; set; }

        public bool Active { get; set; }

        public string Name { get; set; }

        public string SurName { get; set; }

        public string FullName => $"{Name} {SurName}".TrimEnd();

        public Address2 Address { get; set; }

        public decimal Salary { get; set; }

        [Column("DateCreated")]
        public DateTime Created { get; set; }

        [ForeignKey]
        public string DepartmentGuid { get; set; }

        public Department2 Department { get; set; }

        public byte[] Image { get; set; }

        [Ignore]
        public string Ignore { get; set; }
    }
}