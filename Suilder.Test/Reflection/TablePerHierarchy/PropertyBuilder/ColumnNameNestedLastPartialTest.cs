using System.Collections.Generic;
using Suilder.Reflection.Builder;
using Suilder.Test.Reflection.TablePerHierarchy.Tables;
using Xunit;

namespace Suilder.Test.Reflection.TablePerHierarchy.PropertyBuilder
{
    public class ColumnNameNestedLastPartialTest : BaseTest
    {
        protected override void InitConfig()
        {
            tableBuilder.DefaultInheritTable(true);

            tableBuilder.Add<Person>()
                .Property(x => x.Id)
                .ColumnName("Id2", true);

            tableBuilder.Add<Person>()
                .Property(x => x.Name)
                .ColumnName("Name2", true);

            tableBuilder.Add<Person>()
                .Property(x => x.Address.Street)
                .ColumnName("Street2", true);

            tableBuilder.Add<Employee>()
                .Property(x => x.Salary)
                .ColumnName("Salary2", true);

            tableBuilder.Add<Employee>()
                .Property(x => x.DepartmentId)
                .ColumnName("DepartmentId2", true);

            tableBuilder.Add<Employee>()
                .Property(x => x.Department.Id)
                .ColumnName("DepartmentId2", true);

            tableBuilder.Add<Employee>()
                .Property(x => x.Image)
                .ColumnName("Image2", true);

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
            ITableInfo employeeInfo = tableBuilder.GetConfig<Employee>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { "Id" }, personInfo.PrimaryKeys);
            Assert.Equal(new string[] { "Id" }, employeeInfo.PrimaryKeys);
            Assert.Equal(new string[] { "Id" }, deptInfo.PrimaryKeys);
        }

        [Fact]
        public void Foreign_Keys()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo employeeInfo = tableBuilder.GetConfig<Employee>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { }, personInfo.ForeignKeys);
            Assert.Equal(new string[] { "Department.Id" }, employeeInfo.ForeignKeys);
            Assert.Equal(new string[] { "Boss.Id" }, deptInfo.ForeignKeys);
        }

        [Fact]
        public void Columns()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo employeeInfo = tableBuilder.GetConfig<Employee>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { "Id", "Guid", "Name", "Surname", "Address.Street", "Address.City" },
                personInfo.Columns);
            Assert.Equal(new string[] { "Id", "Guid", "Name", "Surname", "Address.Street", "Address.City", "Salary",
                "DepartmentId", "Department.Id", "Image" }, employeeInfo.Columns);
            Assert.Equal(new string[] { "Id", "Guid", "Name", "Boss.Id", "Tags" }, deptInfo.Columns);
        }

        [Fact]
        public void Column_Names_Dic()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo employeeInfo = tableBuilder.GetConfig<Employee>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new Dictionary<string, string>
            {
                ["Id"] = "Id2",
                ["Guid"] = "Guid",
                ["Name"] = "Name2",
                ["Surname"] = "Surname",
                ["Address.Street"] = "AddressStreet2",
                ["Address.City"] = "AddressCity"
            }, personInfo.ColumnNamesDic);

            Assert.Equal(new Dictionary<string, string>
            {
                ["Id"] = "Id2",
                ["Guid"] = "Guid",
                ["Name"] = "Name2",
                ["Surname"] = "Surname",
                ["Address.Street"] = "AddressStreet2",
                ["Address.City"] = "AddressCity",
                ["Salary"] = "Salary2",
                ["DepartmentId"] = "DepartmentId2",
                ["Department.Id"] = "DepartmentId2",
                ["Image"] = "Image2"
            }, employeeInfo.ColumnNamesDic);

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
            ITableInfo employeeInfo = tableBuilder.GetConfig<Employee>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { "Id2", "Guid", "Name2", "Surname", "AddressStreet2", "AddressCity" },
                personInfo.ColumnNames);
            Assert.Equal(new string[] { "Id2", "Guid", "Name2", "Surname", "AddressStreet2", "AddressCity", "Salary2",
                "DepartmentId2", "Image2" }, employeeInfo.ColumnNames);
            Assert.Equal(new string[] { "Id3", "Guid", "Name3", "BossId3", "Tags3" }, deptInfo.ColumnNames);
        }
    }
}