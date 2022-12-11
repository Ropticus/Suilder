using System.Collections.Generic;

namespace Suilder.Test.Reflection.Builder.TableNested.Tables
{
    public class Department : BaseConfig
    {
        public virtual Person Boss { get; set; }

        public virtual List<Person> Employees { get; set; }

        public virtual List<string> Tags { get; set; }
    }
}