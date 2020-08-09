using System.Collections.Generic;

namespace Suilder.Test.Reflection.Builder.TableNested.Tables
{
    public class Department : BaseConfig
    {
        public Person Boss { get; set; }

        public List<Person> Employees { get; set; }

        public List<string> Tags { get; set; }
    }
}