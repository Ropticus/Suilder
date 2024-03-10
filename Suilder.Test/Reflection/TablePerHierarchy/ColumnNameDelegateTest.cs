using System.Collections.Generic;
using Suilder.Reflection.Builder;
using Suilder.Test.Reflection.TablePerHierarchy.Tables;
using Xunit;

namespace Suilder.Test.Reflection.TablePerHierarchy
{
    public class ColumnNameDelegateTest : BaseTest
    {
        protected override void InitConfig()
        {
            tableBuilder.DefaultInheritTable(true)
                .DefaultColumnName((x, p, i) => $"{(i == 0 ? $"{x.Name}_" : "")}{p[i].Name}{i}");

            tableBuilder.Add<Person>();

            tableBuilder.Add<Employee>();

            tableBuilder.Add<Department>();
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
                ["Id"] = "BaseConfig_Id0",
                ["Guid"] = "BaseConfig_Guid0",
                ["Name"] = "BaseConfig_Name0",
                ["Surname"] = "Person_Surname0",
                ["Address.Street"] = "Person_Address0Street1",
                ["Address.City"] = "Person_Address0City1"
            }, personInfo.ColumnNamesDic);

            Assert.Equal(new Dictionary<string, string>
            {
                ["Id"] = "BaseConfig_Id0",
                ["Guid"] = "BaseConfig_Guid0",
                ["Name"] = "BaseConfig_Name0",
                ["Surname"] = "Person_Surname0",
                ["Address.Street"] = "Person_Address0Street1",
                ["Address.City"] = "Person_Address0City1",
                ["Salary"] = "Employee_Salary0",
                ["DepartmentId"] = "Employee_DepartmentId0",
                ["Department.Id"] = "Employee_Department0Id1",
                ["Image"] = "Employee_Image0"
            }, employeeInfo.ColumnNamesDic);

            Assert.Equal(new Dictionary<string, string>
            {
                ["Id"] = "BaseConfig_Id0",
                ["Guid"] = "BaseConfig_Guid0",
                ["Name"] = "BaseConfig_Name0",
                ["Boss.Id"] = "Department_Boss0Id1",
                ["Tags"] = "Department_Tags0"
            }, deptInfo.ColumnNamesDic);
        }

        [Fact]
        public void Column_Names()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo employeeInfo = tableBuilder.GetConfig<Employee>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { "BaseConfig_Id0", "BaseConfig_Guid0", "BaseConfig_Name0", "Person_Surname0",
                "Person_Address0Street1", "Person_Address0City1" }, personInfo.ColumnNames);
            Assert.Equal(new string[] { "BaseConfig_Id0", "BaseConfig_Guid0", "BaseConfig_Name0", "Person_Surname0",
                "Person_Address0Street1", "Person_Address0City1", "Employee_Salary0", "Employee_DepartmentId0",
                "Employee_Department0Id1", "Employee_Image0" }, employeeInfo.ColumnNames);
            Assert.Equal(new string[] { "BaseConfig_Id0", "BaseConfig_Guid0", "BaseConfig_Name0", "Department_Boss0Id1",
                "Department_Tags0" }, deptInfo.ColumnNames);
        }
    }
}