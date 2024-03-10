using System.Collections.Generic;
using Suilder.Reflection.Builder;
using Suilder.Test.Reflection.TablePerHierarchy.Tables;
using Xunit;

namespace Suilder.Test.Reflection.TablePerHierarchy.PropertyBuilderDelegate
{
    public class ForeignKeyCompositeWithNamePartialTest : BaseTest
    {
        protected override void InitConfig()
        {
            tableBuilder.DefaultInheritTable(true);

            tableBuilder.Add<Person>();

            tableBuilder.Add<Employee>()
                .Property(x => x.DepartmentId, p => p
                    .ForeignKey("DepartmentId2", true))
                .Property(x => x.Department.Id, p => p
                    .ForeignKey("DepartmentId2", true))
                .Property(x => x.Department.Guid, p => p
                    .ForeignKey("DepartmentGuid2", true));

            tableBuilder.Add<Department>()
                .Property(x => x.Boss.Id, p => p
                    .ForeignKey("BossId2", true))
                .Property(x => x.Boss.Guid, p => p
                    .ForeignKey("BossGuid2", true));
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
            Assert.Equal(new string[] { "DepartmentId", "Department.Id", "Department.Guid" }, employeeInfo.ForeignKeys);
            Assert.Equal(new string[] { "Boss.Id", "Boss.Guid" }, deptInfo.ForeignKeys);
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
                "DepartmentId", "Department.Id", "Department.Guid", "Image" }, employeeInfo.Columns);
            Assert.Equal(new string[] { "Id", "Guid", "Name", "Boss.Id", "Boss.Guid", "Tags" }, deptInfo.Columns);
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
                ["Id"] = "Id",
                ["Guid"] = "Guid",
                ["Name"] = "Name",
                ["Surname"] = "Surname",
                ["Address.Street"] = "AddressStreet",
                ["Address.City"] = "AddressCity",
                ["Salary"] = "Salary",
                ["DepartmentId"] = "DepartmentId2",
                ["Department.Id"] = "DepartmentId2",
                ["Department.Guid"] = "DepartmentGuid2",
                ["Image"] = "Image"
            }, employeeInfo.ColumnNamesDic);

            Assert.Equal(new Dictionary<string, string>
            {
                ["Id"] = "Id",
                ["Guid"] = "Guid",
                ["Name"] = "Name",
                ["Boss.Id"] = "BossId2",
                ["Boss.Guid"] = "BossGuid2",
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
            Assert.Equal(new string[] { "Id", "Guid", "Name", "Surname", "AddressStreet", "AddressCity", "Salary",
                "DepartmentId2", "DepartmentGuid2", "Image" }, employeeInfo.ColumnNames);
            Assert.Equal(new string[] { "Id", "Guid", "Name", "BossId2", "BossGuid2", "Tags" }, deptInfo.ColumnNames);
        }
    }
}