using System.Collections.Generic;

namespace Suilder.Test.Reflection.Builder.TablePerType.Tables
{
    public class Department : BaseConfig
    {
        public virtual Employee Boss { get; set; }

        public virtual List<Employee> Employees { get; set; }

        public List<string> Tags { get; set; }
    }
}