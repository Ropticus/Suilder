using System.Collections.Generic;
using Suilder.Reflection.Builder;
using Suilder.Test.Reflection.TableNested.Tables;
using Xunit;

namespace Suilder.Test.Reflection.TableNested.PropertyBuilderDelegate
{
    public class ColumnNameTest : BaseTest
    {
        protected override void InitConfig()
        {
            tableBuilder.Add<Person>()
                .Property(x => x.Id, p => p
                    .ColumnName("Id2"))
                .Property(x => x.Name, p => p
                    .ColumnName("Name2"))
                .Property(x => x.Employee.Address, p => p
                    .ColumnName("Address2"))
                .Property(x => x.Employee.DepartmentId, p => p
                    .ColumnName("DepartmentId2"))
                .Property(x => x.Employee.Department, p => p
                    .ColumnName("DepartmentId2"))
                .Property(x => x.Employee.Image, p => p
                    .ColumnName("Image2"));

            tableBuilder.AddNested<Employee>();

            tableBuilder.Add<Department>()
                .Property(x => x.Id, p => p
                    .ColumnName("Id3"))
                .Property(x => x.Name, p => p
                    .ColumnName("Name3"))
                .Property(x => x.Boss.Id, p => p
                    .ColumnName("BossId3"))
                .Property(x => x.Tags, p => p
                    .ColumnName("Tags3"));
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
                ["Employee.Address.Street"] = "Address2Street",
                ["Employee.Address.City"] = "Address2City",
                ["Employee.Salary"] = "EmployeeSalary",
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

            Assert.Equal(new string[] { "Id2", "Guid", "Name2", "Surname", "Address2Street", "Address2City",
                "EmployeeSalary", "DepartmentId2", "Image2" }, personInfo.ColumnNames);
            Assert.Equal(new string[] { "Id3", "Guid", "Name3", "BossId3", "Tags3" }, deptInfo.ColumnNames);
        }
    }
}