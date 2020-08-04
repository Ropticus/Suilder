using System.Collections.Generic;
using Suilder.Reflection;
using Suilder.Reflection.Builder;
using Xunit;

namespace Suilder.Test.Reflection.Attributes.TablePerHierarchy
{
    public class SchemaTest : BaseTest
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

            Assert.Equal("schema_Person", personInfo.Schema);
            Assert.Equal("schema_Person", employeeInfo.Schema);
            Assert.Equal("schema_Department", deptInfo.Schema);
        }

        [Fact]
        public void Table_Name()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo employeeInfo = tableBuilder.GetConfig<Employee>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal("Person", personInfo.TableName);
            Assert.Equal("Person", employeeInfo.TableName);
            Assert.Equal("Department", deptInfo.TableName);
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

        [Table(Schema = "schema_Department")]
        public class Department : BaseConfig
        {
            public virtual Employee Boss { get; set; }

            public virtual List<Employee> Employees { get; set; }
        }

        [Table(Schema = "schema_Employee")]
        public class Employee : Person
        {
            public virtual decimal Salary { get; set; }

            public virtual int DepartmentId { get; set; }

            public virtual Department Department { get; set; }

            public byte[] Image { get; set; }
        }

        [Table(Schema = "schema_Person")]
        public class Person : BaseConfig
        {
            public virtual string SurName { get; set; }

            public virtual string FullName => $"{Name} {SurName}".TrimEnd();

            public virtual Address Address { get; set; }
        }
    }
}