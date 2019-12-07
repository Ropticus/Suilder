using System.Collections.Generic;
using Suilder.Reflection.Builder;
using Suilder.Test.Reflection.Builder.TableNested.Tables;
using Xunit;

namespace Suilder.Test.Reflection.Builder.TableNested
{
    public class ColumnNameNestedTest : BaseTest
    {
        protected override void InitConfig()
        {
            tableBuilder.Add<Person>()
                .ColumnName(x => x.Id, "Id2")
                .ColumnName(x => x.Name, "Name2")
                .ColumnName(x => x.Employee, "Employee2")
                .ColumnName(x => x.Employee.Address, "Address2")
                .ColumnName(x => x.Employee.DepartmentId, "DepartmentId2")
                .ColumnName(x => x.Employee.Department, "DepartmentId2");

            tableBuilder.AddNested<Employee>();

            tableBuilder.Add<Department>()
                .ColumnName(x => x.Id, "Id3")
                .ColumnName(x => x.Name, "Name3")
                .ColumnName(x => x.Boss.Id, "BossId3");
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

            Assert.Equal(new string[] { "Id", "Guid", "Name", "SurName", "Employee.Address.Street", "Employee.Address.City",
                "Employee.Salary", "Employee.DepartmentId", "Employee.Department.Id", "Employee.Image" },
                personInfo.Columns);
            Assert.Equal(new string[] { "Id", "Guid", "Name", "Boss.Id" }, deptInfo.Columns);
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
                ["SurName"] = "SurName",
                ["Employee.Address.Street"] = "Address2Street",
                ["Employee.Address.City"] = "Address2City",
                ["Employee.Salary"] = "Employee2Salary",
                ["Employee.DepartmentId"] = "DepartmentId2",
                ["Employee.Department.Id"] = "DepartmentId2",
                ["Employee.Image"] = "Employee2Image"
            }, personInfo.ColumnNamesDic);

            Assert.Equal(new Dictionary<string, string>
            {
                ["Id"] = "Id3",
                ["Guid"] = "Guid",
                ["Name"] = "Name3",
                ["Boss.Id"] = "BossId3"
            }, deptInfo.ColumnNamesDic);
        }

        [Fact]
        public void Column_Names()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { "Id2", "Guid", "Name2", "SurName", "Address2Street", "Address2City",
                "Employee2Salary", "DepartmentId2", "Employee2Image" }, personInfo.ColumnNames);
            Assert.Equal(new string[] { "Id3", "Guid", "Name3", "BossId3" }, deptInfo.ColumnNames);
        }
    }
}