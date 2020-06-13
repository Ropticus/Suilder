using System.Collections.Generic;
using Suilder.Reflection;

namespace Suilder.Test.Builder.Tables
{
    [Table("Dept")]
    public class Department2
    {
        [PrimaryKey]
        public string Guid { get; set; }

        public bool Active { get; set; }

        public string Name { get; set; }

        public Person2 Boss { get; set; }

        public List<Person2> Employees { get; set; }
    }
}