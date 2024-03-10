using System.Collections.Generic;
using Suilder.Reflection.Builder;
using Suilder.Test.Reflection.TablePerHierarchy.Tables;
using Xunit;

namespace Suilder.Test.Reflection.TablePerHierarchy.PropertyBuilder
{
    public class PrimaryKeyOverrideTest : BaseTest
    {
        protected override void InitConfig()
        {
            tableBuilder.DefaultInheritTable(true);

            tableBuilder.Add<Person>()
                .Property(x => x.Guid)
                .PrimaryKey();

            tableBuilder.Add<Employee>()
                .Property(x => x.Id)
                .PrimaryKey();

            tableBuilder.Add<Department>()
                .Property(x => x.Guid)
                .PrimaryKey();
        }

        [Fact]
        public void Primary_Keys()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo employeeInfo = tableBuilder.GetConfig<Employee>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { "Guid" }, personInfo.PrimaryKeys);
            Assert.Equal(new string[] { "Id" }, employeeInfo.PrimaryKeys);
            Assert.Equal(new string[] { "Guid" }, deptInfo.PrimaryKeys);
        }

        [Fact]
        public void Foreign_Keys()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo employeeInfo = tableBuilder.GetConfig<Employee>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { }, personInfo.ForeignKeys);
            Assert.Equal(new string[] { "Department.Guid" }, employeeInfo.ForeignKeys);
            Assert.Equal(new string[] { "Boss.Id" }, deptInfo.ForeignKeys);
        }

        [Fact]
        public void Columns()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo employeeInfo = tableBuilder.GetConfig<Employee>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { "Guid", "Id", "Name", "Surname", "Address.Street", "Address.City" },
                personInfo.Columns);
            Assert.Equal(new string[] { "Id", "Guid", "Name", "Surname", "Address.Street", "Address.City", "Salary",
                "DepartmentId", "Department.Guid", "Image" }, employeeInfo.Columns);
            Assert.Equal(new string[] { "Guid", "Id", "Name", "Boss.Id", "Tags" }, deptInfo.Columns);
        }

        [Fact]
        public void Column_Names_Dic()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo employeeInfo = tableBuilder.GetConfig<Employee>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new Dictionary<string, string>
            {
                ["Guid"] = "Guid",
                ["Id"] = "Id",
                ["Name"] = "Name",
                ["Surname"] = "Surname",
                ["Address.Street"] = "AddressStreet",
                ["Address.City"] = "AddressCity"
            }, personInfo.ColumnNamesDic);

            Assert.Equal(new Dictionary<string, string>
            {
                ["Id"] = "Id",
                ["Guid"] = "Guid",
                ["Name"] = "Name",
                ["Surname"] = "Surname",
                ["Address.Street"] = "AddressStreet",
                ["Address.City"] = "AddressCity",
                ["Salary"] = "Salary",
                ["DepartmentId"] = "DepartmentId",
                ["Department.Guid"] = "DepartmentGuid",
                ["Image"] = "Image"
            }, employeeInfo.ColumnNamesDic);

            Assert.Equal(new Dictionary<string, string>
            {
                ["Guid"] = "Guid",
                ["Id"] = "Id",
                ["Name"] = "Name",
                ["Boss.Id"] = "BossId",
                ["Tags"] = "Tags"
            }, deptInfo.ColumnNamesDic);
        }

        [Fact]
        public void Column_Names()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo employeeInfo = tableBuilder.GetConfig<Employee>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { "Guid", "Id", "Name", "Surname", "AddressStreet", "AddressCity" },
                personInfo.ColumnNames);
            Assert.Equal(new string[] { "Id", "Guid", "Name", "Surname", "AddressStreet", "AddressCity", "Salary",
                "DepartmentId", "DepartmentGuid", "Image" }, employeeInfo.ColumnNames);
            Assert.Equal(new string[] { "Guid", "Id", "Name", "BossId", "Tags" }, deptInfo.ColumnNames);
        }
    }
}