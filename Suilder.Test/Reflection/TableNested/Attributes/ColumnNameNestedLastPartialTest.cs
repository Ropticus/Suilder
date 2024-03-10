using System.Collections.Generic;
using Suilder.Reflection;
using Suilder.Reflection.Builder;
using Xunit;

namespace Suilder.Test.Reflection.TableNested.Attributes
{
    public class ColumnNameNestedLastPartialTest : BaseTest
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

            Assert.Equal(new string[] { "Employee.Department.Id" }, personInfo.ForeignKeys);
            Assert.Equal(new string[] { "Boss.Id" }, deptInfo.ForeignKeys);
        }

        [Fact]
        public void Columns()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { "Id", "Guid", "Name", "Surname", "Employee.Address.Street", "Employee.Address.City",
                "Employee.Salary", "Employee.DepartmentId", "Employee.Department.Id", "Employee.Image" },
                personInfo.Columns);
            Assert.Equal(new string[] { "Id", "Guid", "Name", "Boss.Id", "Tags" }, deptInfo.Columns);
        }

        [Fact]
        public void Column_Names_Dic()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new Dictionary<string, string>
            {
                ["Id"] = "Id2",
                ["Guid"] = "Guid",
                ["Name"] = "Name2",
                ["Surname"] = "Surname",
                ["Employee.Address.Street"] = "EmployeeAddressStreet2",
                ["Employee.Address.City"] = "EmployeeAddressCity",
                ["Employee.Salary"] = "EmployeeSalary",
                ["Employee.DepartmentId"] = "EmployeeDepartmentId2",
                ["Employee.Department.Id"] = "EmployeeDepartmentId2",
                ["Employee.Image"] = "EmployeeImage2"
            }, personInfo.ColumnNamesDic);

            Assert.Equal(new Dictionary<string, string>
            {
                ["Id"] = "Id3",
                ["Guid"] = "Guid",
                ["Name"] = "Name3",
                ["Boss.Id"] = "BossId3",
                ["Tags"] = "Tags3"
            }, deptInfo.ColumnNamesDic);
        }

        [Fact]
        public void Column_Names()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { "Id2", "Guid", "Name2", "Surname", "EmployeeAddressStreet2", "EmployeeAddressCity",
                "EmployeeSalary", "EmployeeDepartmentId2", "EmployeeImage2" }, personInfo.ColumnNames);
            Assert.Equal(new string[] { "Id3", "Guid", "Name3", "BossId3", "Tags3" }, deptInfo.ColumnNames);
        }

        [Nested]
        public class Address
        {
            [Column("Street2", true)]
            public virtual string Street { get; set; }

            public virtual string City { get; set; }
        }

        public abstract class BaseConfig
        {
            public virtual int Id { get; set; }

            public virtual string Guid { get; set; }

            public virtual string Name { get; set; }
        }

        public class Department : BaseConfig
        {
            [Column("Id3", true)]
            public override int Id { get; set; }

            [Column("Name3", true)]
            public override string Name { get; set; }

            [Column("BossId3", true)]
            public virtual Person Boss { get; set; }

            public virtual List<Person> Employees { get; set; }

            [Column("Tags3", true)]
            public virtual List<string> Tags { get; set; }
        }

        [Nested]
        public class Employee
        {
            public virtual Address Address { get; set; }

            public virtual decimal Salary { get; set; }

            [Column("DepartmentId2", true)]
            public virtual int DepartmentId { get; set; }

            [Column("DepartmentId2", true)]
            public virtual Department Department { get; set; }

            [Column("Image2", true)]
            public virtual byte[] Image { get; set; }
        }

        public class Person : BaseConfig
        {
            [Column("Id2", true)]
            public override int Id { get; set; }

            [Column("Name2", true)]
            public override string Name { get; set; }

            public virtual string Surname { get; set; }

            public virtual string FullName => $"{Name} {Surname}".TrimEnd();

            public virtual Employee Employee { get; set; }
        }
    }
}