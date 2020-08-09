using System.Collections.Generic;

namespace Suilder.Test.Reflection.Builder.NoInherit.Tables
{
    public class Department
    {
        public int Id { get; set; }

        public string Guid { get; set; }

        public string Name { get; set; }

        public Person Boss { get; set; }

        public List<Person> Employees { get; set; }

        public List<string> Tags { get; set; }
    }
}