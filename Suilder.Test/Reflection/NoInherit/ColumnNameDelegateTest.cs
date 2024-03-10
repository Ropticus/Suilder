using System.Collections.Generic;
using Suilder.Reflection.Builder;
using Suilder.Test.Reflection.NoInherit.Tables;
using Xunit;

namespace Suilder.Test.Reflection.NoInherit
{
    public class ColumnNameDelegateTest : BaseTest
    {
        protected override void InitConfig()
        {
            tableBuilder.DefaultColumnName((x, p, i) => $"{(i == 0 ? $"{x.Name}_" : "")}{p[i].Name}{i}");

            tableBuilder.Add<Person>();

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

            Assert.Equal(new string[] { "Department.Id" }, personInfo.ForeignKeys);
            Assert.Equal(new string[] { "Boss.Id" }, deptInfo.ForeignKeys);
        }

        [Fact]
        public void Columns()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { "Id", "Guid", "Name", "Surname", "Address.Street", "Address.City", "DepartmentId",
                "Department.Id", "Image" }, personInfo.Columns);
            Assert.Equal(new string[] { "Id", "Guid", "Name", "Boss.Id", "Tags" }, deptInfo.Columns);
        }

        [Fact]
        public void Column_Names_Dic()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new Dictionary<string, string>
            {
                ["Id"] = "Person_Id0",
                ["Guid"] = "Person_Guid0",
                ["Name"] = "Person_Name0",
                ["Surname"] = "Person_Surname0",
                ["Address.Street"] = "Person_Address0Street1",
                ["Address.City"] = "Person_Address0City1",
                ["DepartmentId"] = "Person_DepartmentId0",
                ["Department.Id"] = "Person_Department0Id1",
                ["Image"] = "Person_Image0"
            }, personInfo.ColumnNamesDic);

            Assert.Equal(new Dictionary<string, string>
            {
                ["Id"] = "Department_Id0",
                ["Guid"] = "Department_Guid0",
                ["Name"] = "Department_Name0",
                ["Boss.Id"] = "Department_Boss0Id1",
                ["Tags"] = "Department_Tags0"
            }, deptInfo.ColumnNamesDic);
        }

        [Fact]
        public void Column_Names()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { "Person_Id0", "Person_Guid0", "Person_Name0", "Person_Surname0",
                "Person_Address0Street1", "Person_Address0City1", "Person_DepartmentId0", "Person_Department0Id1",
                "Person_Image0" }, personInfo.ColumnNames);
            Assert.Equal(new string[] { "Department_Id0", "Department_Guid0", "Department_Name0", "Department_Boss0Id1",
                "Department_Tags0" }, deptInfo.ColumnNames);
        }
    }
}