using System.Collections.Generic;
using Suilder.Reflection;

namespace Suilder.Performance.Tables
{
    [Table("Dept")]
    public class Department
    {
        public int Id { get; set; }

        public bool Active { get; set; }

        public string Name { get; set; }

        public Person Boss { get; set; }

        public List<Person> Employees { get; set; }

        public List<string> Tags { get; set; }
    }
}