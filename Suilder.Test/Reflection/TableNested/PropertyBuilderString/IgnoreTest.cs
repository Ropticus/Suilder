using System.Collections.Generic;
using Suilder.Reflection.Builder;
using Suilder.Test.Reflection.TableNested.Tables;
using Xunit;

namespace Suilder.Test.Reflection.TableNested.PropertyBuilderString
{
    public class IgnoreTest : BaseTest
    {
        protected override void InitConfig()
        {
            tableBuilder.Add<BaseConfig>()
                .Property("Guid")
                .Ignore()
                .ColumnName("Guid2");

            tableBuilder.Add<Person>()
                .Property("Employee.Address")
                .Ignore()
                .ColumnName("Address2");

            tableBuilder.Add<Person>()
                .Property("Employee.DepartmentId")
                .Ignore()
                .ForeignKey("DepartmentId2");

            tableBuilder.Add<Person>()
                .Property("Employee.Department")
                .Ignore()
                .ForeignKey("DepartmentId2");

            tableBuilder.Add<Person>()
                .Property("Employee.Image")
                .Ignore();

            tableBuilder.AddNested<Employee>();

            tableBuilder.Add<Department>()
                .Property("Boss")
                .Ignore();

            tableBuilder.Add<Department>()
                .Property("Tags")
                .Ignore();
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
    }
}