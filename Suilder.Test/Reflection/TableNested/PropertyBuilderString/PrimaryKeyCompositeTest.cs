using System.Collections.Generic;
using Suilder.Reflection.Builder;
using Suilder.Test.Reflection.TableNested.Tables;
using Xunit;

namespace Suilder.Test.Reflection.TableNested.PropertyBuilderString
{
    public class PrimaryKeyCompositeTest : BaseTest
    {
        protected override void InitConfig()
        {
            tableBuilder.Add<BaseConfig>()
                .Property("Guid")
                .PrimaryKey();

            tableBuilder.Add<BaseConfig>()
                .Property("Id")
                .PrimaryKey();

            tableBuilder.Add<Person>();

            tableBuilder.AddNested<Employee>();

            tableBuilder.Add<Department>();
        }

        [Fact]
        public void Primary_Keys()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { "Guid", "Id" }, personInfo.PrimaryKeys);
            Assert.Equal(new string[] { "Guid", "Id" }, deptInfo.PrimaryKeys);
        }

        [Fact]
        public void Foreign_Keys()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { "Employee.Department.Guid", "Employee.Department.Id" }, personInfo.ForeignKeys);
            Assert.Equal(new string[] { "Boss.Guid", "Boss.Id" }, deptInfo.ForeignKeys);
        }

        [Fact]
        public void Columns()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { "Guid", "Id", "Name", "Surname", "Employee.Address.Street", "Employee.Address.City",
                "Employee.Salary", "Employee.DepartmentId", "Employee.Department.Guid", "Employee.Department.Id",
                "Employee.Image" }, personInfo.Columns);
            Assert.Equal(new string[] { "Guid", "Id", "Name", "Boss.Guid", "Boss.Id", "Tags" }, deptInfo.Columns);
        }

        [Fact]
        public void Column_Names_Dic()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new Dictionary<string, string>
            {
                ["Guid"] = "Guid",
                ["Id"] = "Id",
                ["Name"] = "Name",
                ["Surname"] = "Surname",
                ["Employee.Address.Street"] = "EmployeeAddressStreet",
                ["Employee.Address.City"] = "EmployeeAddressCity",
                ["Employee.Salary"] = "EmployeeSalary",
                ["Employee.DepartmentId"] = "EmployeeDepartmentId",
                ["Employee.Department.Guid"] = "EmployeeDepartmentGuid",
                ["Employee.Department.Id"] = "EmployeeDepartmentId",
                ["Employee.Image"] = "EmployeeImage"
            }, personInfo.ColumnNamesDic);

            Assert.Equal(new Dictionary<string, string>
            {
                ["Guid"] = "Guid",
                ["Id"] = "Id",
                ["Name"] = "Name",
                ["Boss.Guid"] = "BossGuid",
                ["Boss.Id"] = "BossId",
                ["Tags"] = "Tags"
            }, deptInfo.ColumnNamesDic);
        }

        [Fact]
        public void Column_Names()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { "Guid", "Id", "Name", "Surname", "EmployeeAddressStreet", "EmployeeAddressCity",
                "EmployeeSalary", "EmployeeDepartmentId", "EmployeeDepartmentGuid", "EmployeeImage" },
                personInfo.ColumnNames);
            Assert.Equal(new string[] { "Guid", "Id", "Name", "BossGuid", "BossId", "Tags" }, deptInfo.ColumnNames);
        }
    }
}