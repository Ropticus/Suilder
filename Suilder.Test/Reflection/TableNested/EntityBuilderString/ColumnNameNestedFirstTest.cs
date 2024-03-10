using System.Collections.Generic;
using Suilder.Reflection.Builder;
using Suilder.Test.Reflection.TableNested.Tables;
using Xunit;

namespace Suilder.Test.Reflection.TableNested.EntityBuilderString
{
    public class ColumnNameNestedFirstTest : BaseTest
    {
        protected override void InitConfig()
        {
            tableBuilder.Add<Person>()
                .ColumnName("Id", "Id2")
                .ColumnName("Name", "Name2")
                .ColumnName("Employee", "Employee2")
                .ColumnName("Employee.DepartmentId", "DepartmentId2")
                .ColumnName("Employee.Department", "DepartmentId2")
                .ColumnName("Employee.Image", "Image2");

            tableBuilder.AddNested<Employee>();

            tableBuilder.Add<Department>()
                .ColumnName("Id", "Id3")
                .ColumnName("Name", "Name3")
                .ColumnName("Boss.Id", "BossId3")
                .ColumnName("Tags", "Tags3");
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
                ["Employee.Address.Street"] = "Employee2AddressStreet",
                ["Employee.Address.City"] = "Employee2AddressCity",
                ["Employee.Salary"] = "Employee2Salary",
                ["Employee.DepartmentId"] = "DepartmentId2",
                ["Employee.Department.Id"] = "DepartmentId2",
                ["Employee.Image"] = "Image2"
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

            Assert.Equal(new string[] { "Id2", "Guid", "Name2", "Surname", "Employee2AddressStreet", "Employee2AddressCity",
                "Employee2Salary", "DepartmentId2", "Image2" }, personInfo.ColumnNames);
            Assert.Equal(new string[] { "Id3", "Guid", "Name3", "BossId3", "Tags3" }, deptInfo.ColumnNames);
        }
    }
}