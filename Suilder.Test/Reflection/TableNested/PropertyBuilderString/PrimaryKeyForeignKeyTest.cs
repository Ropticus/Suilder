using System.Collections.Generic;
using Suilder.Reflection.Builder;
using Suilder.Test.Reflection.TableNested.Tables;
using Xunit;

namespace Suilder.Test.Reflection.TableNested.PropertyBuilderString
{
    public class PrimaryKeyForeignKeyTest : BaseTest
    {
        protected override void InitConfig()
        {
            tableBuilder.Add<Person>()
                .Property("Employee.Department.Id")
                .PrimaryKey();

            tableBuilder.Add<Person>()
                .Property("Id")
                .PrimaryKey();

            tableBuilder.AddNested<Employee>();

            tableBuilder.Add<Department>();
        }

        [Fact]
        public void Primary_Keys()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { "Employee.Department.Id", "Id" }, personInfo.PrimaryKeys);
            Assert.Equal(new string[] { "Id" }, deptInfo.PrimaryKeys);
        }

        [Fact]
        public void Foreign_Keys()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { "Employee.Department.Id" }, personInfo.ForeignKeys);
            Assert.Equal(new string[] { "Boss.Employee.Department.Id", "Boss.Id" }, deptInfo.ForeignKeys);
        }

        [Fact]
        public void Columns()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { "Employee.Department.Id", "Id", "Guid", "Name", "Surname",
                "Employee.Address.Street", "Employee.Address.City", "Employee.Salary", "Employee.DepartmentId",
                "Employee.Image" }, personInfo.Columns);
            Assert.Equal(new string[] { "Id", "Guid", "Name", "Boss.Employee.Department.Id", "Boss.Id", "Tags" },
                deptInfo.Columns);
        }

        [Fact]
        public void Column_Names_Dic()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new Dictionary<string, string>
            {
                ["Employee.Department.Id"] = "EmployeeDepartmentId",
                ["Id"] = "Id",
                ["Guid"] = "Guid",
                ["Name"] = "Name",
                ["Surname"] = "Surname",
                ["Employee.Address.Street"] = "EmployeeAddressStreet",
                ["Employee.Address.City"] = "EmployeeAddressCity",
                ["Employee.Salary"] = "EmployeeSalary",
                ["Employee.DepartmentId"] = "EmployeeDepartmentId",
                ["Employee.Image"] = "EmployeeImage"
            }, personInfo.ColumnNamesDic);

            Assert.Equal(new Dictionary<string, string>
            {
                ["Id"] = "Id",
                ["Guid"] = "Guid",
                ["Name"] = "Name",
                ["Boss.Employee.Department.Id"] = "BossEmployeeDepartmentId",
                ["Boss.Id"] = "BossId",
                ["Tags"] = "Tags"
            }, deptInfo.ColumnNamesDic);
        }

        [Fact]
        public void Column_Names()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { "EmployeeDepartmentId", "Id", "Guid", "Name", "Surname", "EmployeeAddressStreet",
                "EmployeeAddressCity", "EmployeeSalary", "EmployeeImage" }, personInfo.ColumnNames);
            Assert.Equal(new string[] { "Id", "Guid", "Name", "BossEmployeeDepartmentId", "BossId", "Tags" },
                deptInfo.ColumnNames);
        }
    }
}