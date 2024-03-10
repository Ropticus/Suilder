using System.Collections.Generic;
using Suilder.Reflection;
using Suilder.Reflection.Builder;
using Xunit;

namespace Suilder.Test.Reflection.TableNested.Attributes
{
    public class ForeignKeyCompositeWithNamePartialTest : BaseTest
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

            Assert.Equal(new string[] { "Employee.DepartmentId", "Employee.Department.Id", "Employee.Department.Guid" },
                personInfo.ForeignKeys);
            Assert.Equal(new string[] { "Boss.Id", "Boss.Guid" }, deptInfo.ForeignKeys);
        }

        [Fact]
        public void Columns()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { "Id", "Guid", "Name", "Surname", "Employee.Address.Street", "Employee.Address.City",
                "Employee.Salary", "Employee.DepartmentId", "Employee.Department.Id", "Employee.Department.Guid",
                "Employee.Image" }, personInfo.Columns);
            Assert.Equal(new string[] { "Id", "Guid", "Name", "Boss.Id", "Boss.Guid", "Tags" }, deptInfo.Columns);
        }

        [Fact]
        public void Column_Names_Dic()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new Dictionary<string, string>
            {
                ["Id"] = "Id",
                ["Guid"] = "Guid",
                ["Name"] = "Name",
                ["Surname"] = "Surname",
                ["Employee.Address.Street"] = "EmployeeAddressStreet",
                ["Employee.Address.City"] = "EmployeeAddressCity",
                ["Employee.Salary"] = "EmployeeSalary",
                ["Employee.DepartmentId"] = "EmployeeDepartmentId2",
                ["Employee.Department.Id"] = "EmployeeDepartmentId2",
                ["Employee.Department.Guid"] = "EmployeeDepartmentGuid2",
                ["Employee.Image"] = "EmployeeImage"
            }, personInfo.ColumnNamesDic);

            Assert.Equal(new Dictionary<string, string>
            {
                ["Id"] = "Id",
                ["Guid"] = "Guid",
                ["Name"] = "Name",
                ["Boss.Id"] = "BossId2",
                ["Boss.Guid"] = "BossGuid2",
                ["Tags"] = "Tags"
            }, deptInfo.ColumnNamesDic);
        }

        [Fact]
        public void Column_Names()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { "Id", "Guid", "Name", "Surname", "EmployeeAddressStreet", "EmployeeAddressCity",
                "EmployeeSalary", "EmployeeDepartmentId2", "EmployeeDepartmentGuid2", "EmployeeImage" },
                personInfo.ColumnNames);
            Assert.Equal(new string[] { "Id", "Guid", "Name", "BossId2", "BossGuid2", "Tags" }, deptInfo.ColumnNames);
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

        public class Department : BaseConfig
        {
            [ForeignKey("Id", "BossId2", true)]
            [ForeignKey("Guid", "BossGuid2", true)]
            public virtual Person Boss { get; set; }

            public virtual List<Person> Employees { get; set; }

            public virtual List<string> Tags { get; set; }
        }

        [Nested]
        public class Employee
        {
            public virtual Address Address { get; set; }

            public virtual decimal Salary { get; set; }

            [ForeignKey("DepartmentId2", true)]
            public virtual int DepartmentId { get; set; }

            [ForeignKey("Id", "DepartmentId2", true)]
            [ForeignKey("Guid", "DepartmentGuid2", true)]
            public virtual Department Department { get; set; }

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