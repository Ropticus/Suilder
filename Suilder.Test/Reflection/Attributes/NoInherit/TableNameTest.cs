using System.Collections.Generic;
using Suilder.Reflection;
using Suilder.Reflection.Builder;
using Xunit;

namespace Suilder.Test.Reflection.Attributes.NoInherit
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
            public string Street { get; set; }

            public string City { get; set; }
        }

        [Table("prefix_Department")]
        public class Department
        {
            public int Id { get; set; }

            public string Guid { get; set; }

            public string Name { get; set; }

            public Person Boss { get; set; }

            public List<Person> Employees { get; set; }
        }

        [Table("prefix_Person")]
        public class Person
        {
            public int Id { get; set; }

            public string Guid { get; set; }

            public string Name { get; set; }

            public string SurName { get; set; }

            public string FullName => $"{Name} {SurName}".TrimEnd();

            public Address Address { get; set; }

            public int DepartmentId { get; set; }

            public Department Department { get; set; }

            public byte[] Image { get; set; }
        }
    }
}