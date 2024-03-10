using System.Collections.Generic;

namespace Suilder.Test.Reflection.TablePerHierarchy.Tables
{
    public class Department : BaseConfig
    {
        public virtual Employee Boss { get; set; }

        public virtual List<Employee> Employees { get; set; }

        public virtual List<string> Tags { get; set; }
    }
}