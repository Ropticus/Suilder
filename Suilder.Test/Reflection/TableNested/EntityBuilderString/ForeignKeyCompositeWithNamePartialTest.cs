using System.Collections.Generic;
using Suilder.Reflection.Builder;
using Suilder.Test.Reflection.TableNested.Tables;
using Xunit;

namespace Suilder.Test.Reflection.TableNested.EntityBuilderString
{
    public class ForeignKeyCompositeWithNamePartialTest : BaseTest
    {
        protected override void InitConfig()
        {
            tableBuilder.Add<Person>()
                .ForeignKey("Employee.DepartmentId", "DepartmentId2", true)
                .ForeignKey("Employee.Department.Id", "DepartmentId2", true)
                .ForeignKey("Employee.Department.Guid", "DepartmentGuid2", true);

            tableBuilder.AddNested<Employee>();

            tableBuilder.Add<Department>()
                .ForeignKey("Boss.Id", "BossId2", true)
                .ForeignKey("Boss.Guid", "BossGuid2", true);
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
    }
}