using System.Collections.Generic;
using Suilder.Reflection.Builder;
using Suilder.Test.Reflection.TableNested.Tables;
using Xunit;

namespace Suilder.Test.Reflection.TableNested.PropertyBuilder
{
    public class ColumnNameNestedPartialTest : BaseTest
    {
        protected override void InitConfig()
        {
            tableBuilder.Add<Person>()
                .Property(x => x.Id)
                .ColumnName("Id2", true);

            tableBuilder.Add<Person>()
                .Property(x => x.Name)
                .ColumnName("Name2", true);

            tableBuilder.Add<Person>()
                .Property(x => x.Employee)
                .ColumnName("Employee2", true);

            tableBuilder.Add<Person>()
                .Property(x => x.Employee.Address)
                .ColumnName("Address2", true);

            tableBuilder.Add<Person>()
                .Property(x => x.Employee.Address.Street)
                .ColumnName("Street2", true);

            tableBuilder.Add<Person>()
                .Property(x => x.Employee.DepartmentId)
                .ColumnName("DepartmentId2", true);

            tableBuilder.Add<Person>()
                .Property(x => x.Employee.Department)
                .ColumnName("DepartmentId2", true);

            tableBuilder.AddNested<Employee>();

            tableBuilder.Add<Department>()
                .Property(x => x.Id)
                .ColumnName("Id3", true);

            tableBuilder.Add<Department>()
                .Property(x => x.Name)
                .ColumnName("Name3", true);

            tableBuilder.Add<Department>()
                .Property(x => x.Boss.Id)
                .ColumnName("BossId3", true);

            tableBuilder.Add<Department>()
                .Property(x => x.Tags)
                .ColumnName("Tags3", true);
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
                ["Employee.Address.Street"] = "Employee2Address2Street2",
                ["Employee.Address.City"] = "Employee2Address2City",
                ["Employee.Salary"] = "Employee2Salary",
                ["Employee.DepartmentId"] = "Employee2DepartmentId2",
                ["Employee.Department.Id"] = "Employee2DepartmentId2",
                ["Employee.Image"] = "Employee2Image"
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

            Assert.Equal(new string[] { "Id2", "Guid", "Name2", "Surname", "Employee2Address2Street2",
                "Employee2Address2City", "Employee2Salary", "Employee2DepartmentId2", "Employee2Image" },
                personInfo.ColumnNames);
            Assert.Equal(new string[] { "Id3", "Guid", "Name3", "BossId3", "Tags3" }, deptInfo.ColumnNames);
        }
    }
}