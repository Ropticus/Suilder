using System;
using Suilder.Reflection;

namespace Suilder.Test.Builder.Tables
{
    public class Person
    {
        public int Id { get; set; }

        public bool Active { get; set; }

        public string Name { get; set; }

        public string SurName { get; set; }

        public string FullName => $"{Name} {SurName}".TrimEnd();

        public Address Address { get; set; }

        public decimal Salary { get; set; }

        [Column("DateCreated")]
        public DateTime Created { get; set; }

        [ForeignKey]
        public int DepartmentId { get; set; }

        public Department Department { get; set; }

        public byte[] Image { get; set; }

        public ulong Flags { get; set; }

        [Ignore]
        public string Ignore { get; set; }
    }
}