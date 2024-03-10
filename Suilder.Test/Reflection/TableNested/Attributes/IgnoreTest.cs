using System.Collections.Generic;
using Suilder.Reflection;
using Suilder.Reflection.Builder;
using Xunit;

namespace Suilder.Test.Reflection.TableNested.Attributes
{
    public class IgnoreTest : BaseTest
    {
        protected override void InitConfig()
        {
            tableBuilder.Add<Person>();

            tableBuilder.Add<Department>();
        }

        [Fact]
        public void Primary_Keys()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { "Id" }, personInfo.PrimaryKeys);
            Assert.Equal(new string[] { "Id" }, deptInfo.PrimaryKeys);
        }

        [Fact]
        public void Foreign_Keys()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { }, personInfo.ForeignKeys);
            Assert.Equal(new string[] { }, deptInfo.ForeignKeys);
        }

        [Fact]
        public void Columns()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { "Id", "Name", "Surname", "Employee.Salary" }, personInfo.Columns);
            Assert.Equal(new string[] { "Id", "Name" }, deptInfo.Columns);
        }

        [Fact]
        public void Column_Names_Dic()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new Dictionary<string, string>
            {
                ["Id"] = "Id",
                ["Name"] = "Name",
                ["Surname"] = "Surname",
                ["Employee.Salary"] = "EmployeeSalary"
            }, personInfo.ColumnNamesDic);

            Assert.Equal(new Dictionary<string, string>
            {
                ["Id"] = "Id",
                ["Name"] = "Name"
            }, deptInfo.ColumnNamesDic);
        }

        [Fact]
        public void Column_Names()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { "Id", "Name", "Surname", "EmployeeSalary" }, personInfo.ColumnNames);
            Assert.Equal(new string[] { "Id", "Name" }, deptInfo.ColumnNames);
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

            [Ignore]
            [Column("Guid2")]
            public virtual string Guid { get; set; }

            public virtual string Name { get; set; }
        }

        public class Department : BaseConfig
        {
            [Ignore]
            public virtual Person Boss { get; set; }

            public virtual List<Person> Employees { get; set; }

            [Ignore]
            public virtual List<string> Tags { get; set; }
        }

        [Nested]
        public class Employee
        {
            [Ignore]
            [Column("Address2")]
            public virtual Address Address { get; set; }

            public virtual decimal Salary { get; set; }

            [Ignore]
            [ForeignKey("DepartmentId2")]
            public virtual int DepartmentId { get; set; }

            [Ignore]
            [ForeignKey("DepartmentId2")]
            public virtual Department Department { get; set; }

            [Ignore]
            public virtual byte[] Image { get; set; }
        }

        public class Person : BaseConfig
        {
            public virtual string Surname { get; set; }

            public virtual string FullName => $"{Name} {Surname}".TrimEnd();

            public virtual Employee Employee { get; set; }
        }
    }
}