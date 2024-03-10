using System.Collections.Generic;
using Suilder.Reflection.Builder;
using Suilder.Test.Reflection.TableNested.Tables;
using Xunit;

namespace Suilder.Test.Reflection.TableNested.EntityBuilder
{
    public class IgnoreNestedLastTest : BaseTest
    {
        protected override void InitConfig()
        {
            tableBuilder.Add<BaseConfig>()
                .Ignore(x => x.Guid);

            tableBuilder.Add<Person>()
                .Ignore(x => x.Employee.Address.City)
                .ColumnName(x => x.Employee.Address.City, "City2")
                .Ignore(x => x.Employee.DepartmentId)
                .Ignore(x => x.Employee.Department.Id)
                .Ignore(x => x.Employee.Image);

            tableBuilder.AddNested<Employee>();

            tableBuilder.Add<Department>()
                .Ignore(x => x.Boss.Id)
                .Ignore(x => x.Tags);
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

            Assert.Equal(new string[] { "Id", "Name", "Surname", "Employee.Address.Street", "Employee.Salary" },
                personInfo.Columns);
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
                ["Employee.Address.Street"] = "EmployeeAddressStreet",
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

            Assert.Equal(new string[] { "Id", "Name", "Surname", "EmployeeAddressStreet", "EmployeeSalary" },
                personInfo.ColumnNames);
            Assert.Equal(new string[] { "Id", "Name" }, deptInfo.ColumnNames);
        }
    }
}