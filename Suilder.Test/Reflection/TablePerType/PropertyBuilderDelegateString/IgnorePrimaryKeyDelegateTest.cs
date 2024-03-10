using System.Collections.Generic;
using Suilder.Reflection.Builder;
using Suilder.Test.Reflection.TablePerType.Tables;
using Xunit;

namespace Suilder.Test.Reflection.TablePerType.PropertyBuilderDelegateString
{
    public class IgnorePrimaryKeyDelegateTest : BaseTest
    {
        protected override void InitConfig()
        {
            tableBuilder.DefaultPrimaryKey(x => x.GetProperty("Guid")?.Name);

            tableBuilder.Add<BaseConfig>()
                .Property("Guid", p => p
                    .Ignore());

            tableBuilder.Add<Person>();

            tableBuilder.Add<Employee>()
                .Property("Department.Id", p => p
                    .ForeignKey());

            tableBuilder.Add<Department>()
                .Property("Boss.Id", p => p
                    .ForeignKey());
        }

        [Fact]
        public void Primary_Keys()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo employeeInfo = tableBuilder.GetConfig<Employee>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { }, personInfo.PrimaryKeys);
            Assert.Equal(new string[] { }, employeeInfo.PrimaryKeys);
            Assert.Equal(new string[] { }, deptInfo.PrimaryKeys);
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

            Assert.Equal(new string[] { "Id", "Name", "Surname", "Address.Street", "Address.City" }, personInfo.Columns);
            Assert.Equal(new string[] { "Salary", "DepartmentId", "Department.Id", "Image" }, employeeInfo.Columns);
            Assert.Equal(new string[] { "Id", "Name", "Boss.Id", "Tags" }, deptInfo.Columns);
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
                ["Name"] = "Name",
                ["Surname"] = "Surname",
                ["Address.Street"] = "AddressStreet",
                ["Address.City"] = "AddressCity"
            }, personInfo.ColumnNamesDic);

            Assert.Equal(new Dictionary<string, string>
            {
                ["Salary"] = "Salary",
                ["DepartmentId"] = "DepartmentId",
                ["Department.Id"] = "DepartmentId",
                ["Image"] = "Image"
            }, employeeInfo.ColumnNamesDic);

            Assert.Equal(new Dictionary<string, string>
            {
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

            Assert.Equal(new string[] { "Id", "Name", "Surname", "AddressStreet", "AddressCity" }, personInfo.ColumnNames);
            Assert.Equal(new string[] { "Salary", "DepartmentId", "Image" }, employeeInfo.ColumnNames);
            Assert.Equal(new string[] { "Id", "Name", "BossId", "Tags" }, deptInfo.ColumnNames);
        }
    }
}