using System.Collections.Generic;
using Suilder.Reflection.Builder;
using Suilder.Test.Reflection.TablePerType.Tables;
using Xunit;

namespace Suilder.Test.Reflection.TablePerType.PropertyBuilderDelegateString
{
    public class PrimaryKeyForeignKeyTest : BaseTest
    {
        protected override void InitConfig()
        {
            tableBuilder.Add<Person>();

            tableBuilder.Add<Employee>()
                .Property("Department.Id", p => p
                    .PrimaryKey())
                .Property("Id", p => p
                    .PrimaryKey());

            tableBuilder.Add<Department>();
        }

        [Fact]
        public void Primary_Keys()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo employeeInfo = tableBuilder.GetConfig<Employee>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { "Id" }, personInfo.PrimaryKeys);
            Assert.Equal(new string[] { "Department.Id", "Id" }, employeeInfo.PrimaryKeys);
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
            Assert.Equal(new string[] { "Boss.Department.Id", "Boss.Id" }, deptInfo.ForeignKeys);
        }

        [Fact]
        public void Columns()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo employeeInfo = tableBuilder.GetConfig<Employee>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { "Id", "Guid", "Name", "Surname", "Address.Street", "Address.City" },
                personInfo.Columns);
            Assert.Equal(new string[] { "Department.Id", "Id", "Salary", "DepartmentId", "Image" }, employeeInfo.Columns);
            Assert.Equal(new string[] { "Id", "Guid", "Name", "Boss.Department.Id", "Boss.Id", "Tags" }, deptInfo.Columns);
        }

        [Fact]
        public void Column_Names_Dic()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo employeeInfo = tableBuilder.GetConfig<Employee>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new Dictionary<string, string>
            {
                ["Id"] = "Id",
                ["Guid"] = "Guid",
                ["Name"] = "Name",
                ["Surname"] = "Surname",
                ["Address.Street"] = "AddressStreet",
                ["Address.City"] = "AddressCity"
            }, personInfo.ColumnNamesDic);

            Assert.Equal(new Dictionary<string, string>
            {
                ["Department.Id"] = "DepartmentId",
                ["Id"] = "Id",
                ["Salary"] = "Salary",
                ["DepartmentId"] = "DepartmentId",
                ["Image"] = "Image"
            }, employeeInfo.ColumnNamesDic);

            Assert.Equal(new Dictionary<string, string>
            {
                ["Id"] = "Id",
                ["Guid"] = "Guid",
                ["Name"] = "Name",
                ["Boss.Department.Id"] = "BossDepartmentId",
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

            Assert.Equal(new string[] { "Id", "Guid", "Name", "Surname", "AddressStreet", "AddressCity" },
                personInfo.ColumnNames);
            Assert.Equal(new string[] { "DepartmentId", "Id", "Salary", "Image" }, employeeInfo.ColumnNames);
            Assert.Equal(new string[] { "Id", "Guid", "Name", "BossDepartmentId", "BossId", "Tags" }, deptInfo.ColumnNames);
        }
    }
}