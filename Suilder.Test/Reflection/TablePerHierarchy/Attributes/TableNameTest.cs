using System.Collections.Generic;
using Suilder.Reflection;
using Suilder.Reflection.Builder;
using Xunit;

namespace Suilder.Test.Reflection.TablePerHierarchy.Attributes
{
    public class TableNameTest : BaseTest
    {
        protected override void InitConfig()
        {
            tableBuilder.DefaultInheritTable(true);

            tableBuilder.Add<Person>();

            tableBuilder.Add<Employee>();

            tableBuilder.Add<Department>();
        }

        [Fact]
        public void Schema_Name()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo employeeInfo = tableBuilder.GetConfig<Employee>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Null(personInfo.Schema);
            Assert.Null(employeeInfo.Schema);
            Assert.Null(deptInfo.Schema);
        }

        [Fact]
        public void Table_Name()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo employeeInfo = tableBuilder.GetConfig<Employee>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal("prefix_Person", personInfo.TableName);
            Assert.Equal("prefix_Person", employeeInfo.TableName);
            Assert.Equal("prefix_Department", deptInfo.TableName);
        }

        [Nested]
        public class Address
        {
            public string Street { get; set; }

            public string City { get; set; }
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
            public virtual Employee Boss { get; set; }

            public virtual List<Employee> Employees { get; set; }

            public virtual List<string> Tags { get; set; }
        }

        [Table("prefix_Employee")]
        public class Employee : Person
        {
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

            public virtual Address Address { get; set; }
        }
    }
}