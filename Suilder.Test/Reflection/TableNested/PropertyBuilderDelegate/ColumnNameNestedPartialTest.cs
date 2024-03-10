using System.Collections.Generic;
using Suilder.Reflection.Builder;
using Suilder.Test.Reflection.TableNested.Tables;
using Xunit;

namespace Suilder.Test.Reflection.TableNested.PropertyBuilderDelegate
{
    public class ColumnNameNestedPartialTest : BaseTest
    {
        protected override void InitConfig()
        {
            tableBuilder.Add<Person>()
                .Property(x => x.Id, p => p
                    .ColumnName("Id2", true))
                .Property(x => x.Name, p => p
                    .ColumnName("Name2", true))
                .Property(x => x.Employee, p => p
                    .ColumnName("Employee2", true))
                .Property(x => x.Employee.Address, p => p
                    .ColumnName("Address2", true))
                .Property(x => x.Employee.Address.Street, p => p
                    .ColumnName("Street2", true))
                .Property(x => x.Employee.DepartmentId, p => p
                    .ColumnName("DepartmentId2", true))
                .Property(x => x.Employee.Department, p => p
                    .ColumnName("DepartmentId2", true));

            tableBuilder.AddNested<Employee>();

            tableBuilder.Add<Department>()
                .Property(x => x.Id, p => p
                    .ColumnName("Id3", true))
                .Property(x => x.Name, p => p
                    .ColumnName("Name3", true))
                .Property(x => x.Boss.Id, p => p
                    .ColumnName("BossId3", true))
                .Property(x => x.Tags, p => p
                    .ColumnName("Tags3", true));
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