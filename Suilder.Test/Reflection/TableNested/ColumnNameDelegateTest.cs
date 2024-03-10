using System.Collections.Generic;
using Suilder.Reflection.Builder;
using Suilder.Test.Reflection.TableNested.Tables;
using Xunit;

namespace Suilder.Test.Reflection.TableNested
{
    public class ColumnNameDelegateTest : BaseTest
    {
        protected override void InitConfig()
        {
            tableBuilder.DefaultColumnName((x, p, i) => $"{(i == 0 ? $"{x.Name}_" : "")}{p[i].Name}{i}");

            tableBuilder.Add<Person>();

            tableBuilder.AddNested<Employee>();

            tableBuilder.Add<Department>();
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
                ["Id"] = "BaseConfig_Id0",
                ["Guid"] = "BaseConfig_Guid0",
                ["Name"] = "BaseConfig_Name0",
                ["Surname"] = "Person_Surname0",
                ["Employee.Address.Street"] = "Person_Employee0Address1Street2",
                ["Employee.Address.City"] = "Person_Employee0Address1City2",
                ["Employee.Salary"] = "Person_Employee0Salary1",
                ["Employee.DepartmentId"] = "Person_Employee0DepartmentId1",
                ["Employee.Department.Id"] = "Person_Employee0Department1Id2",
                ["Employee.Image"] = "Person_Employee0Image1"
            }, personInfo.ColumnNamesDic);

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
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { "BaseConfig_Id0", "BaseConfig_Guid0", "BaseConfig_Name0", "Person_Surname0",
                "Person_Employee0Address1Street2", "Person_Employee0Address1City2", "Person_Employee0Salary1",
                "Person_Employee0DepartmentId1", "Person_Employee0Department1Id2", "Person_Employee0Image1" },
                personInfo.ColumnNames);
            Assert.Equal(new string[] { "BaseConfig_Id0", "BaseConfig_Guid0", "BaseConfig_Name0", "Department_Boss0Id1",
                "Department_Tags0" }, deptInfo.ColumnNames);
        }
    }
}