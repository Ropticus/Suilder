using System.Collections.Generic;
using Suilder.Reflection;
using Suilder.Reflection.Builder;
using Xunit;

namespace Suilder.Test.Reflection.TableNested.Attributes
{
    public class TableNameTest : BaseTest
    {
        protected override void InitConfig()
        {
            tableBuilder.Add<Person>();

            tableBuilder.Add<Department>();
        }

        [Fact]
        public void Schema_Name()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Null(personInfo.Schema);
            Assert.Null(deptInfo.Schema);
        }

        [Fact]
        public void Table_Name()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal("prefix_Person", personInfo.TableName);
            Assert.Equal("prefix_Department", deptInfo.TableName);
        }

        [Nested]
        public class Address
        {
            public virtual string Street { get; set; }

            public virtual string City { get; set; }
        }

        public abstract class BaseConfig
        {
            public virtual int Id { get; set; }

            public virtual string Guid { get; set; }

            public virtual string Name { get; set; }
        }

        [Table("prefix_Department")]
        public class Department : BaseConfig
        {
            public virtual Person Boss { get; set; }

            public virtual List<Person> Employees { get; set; }

            public virtual List<string> Tags { get; set; }
        }

        [Nested]
        public class Employee
        {
            public virtual Address Address { get; set; }

            public virtual decimal Salary { get; set; }

            public virtual int DepartmentId { get; set; }

            public virtual Department Department { get; set; }

            public virtual byte[] Image { get; set; }
        }

        [Table("prefix_Person")]
        public class Person : BaseConfig
        {
            public virtual string Surname { get; set; }

            public virtual string FullName => $"{Name} {Surname}".TrimEnd();

            public virtual Employee Employee { get; set; }
        }
    }
}