using System.Collections.Generic;
using Suilder.Reflection.Builder;
using Suilder.Test.Reflection.NoInherit.Tables;
using Xunit;

namespace Suilder.Test.Reflection.NoInherit.PropertyBuilder
{
    public class PrimaryKeyCompositeTest : BaseTest
    {
        protected override void InitConfig()
        {
            tableBuilder.Add<Person>()
                .Property(x => x.Guid)
                .PrimaryKey();

            tableBuilder.Add<Person>()
                .Property(x => x.Id)
                .PrimaryKey();

            tableBuilder.Add<Department>()
                .Property(x => x.Guid)
                .PrimaryKey();

            tableBuilder.Add<Department>()
                .Property(x => x.Id)
                .PrimaryKey();
        }

        [Fact]
        public void Primary_Keys()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { "Guid", "Id" }, personInfo.PrimaryKeys);
            Assert.Equal(new string[] { "Guid", "Id" }, deptInfo.PrimaryKeys);
        }

        [Fact]
        public void Foreign_Keys()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { "Department.Guid", "Department.Id" }, personInfo.ForeignKeys);
            Assert.Equal(new string[] { "Boss.Guid", "Boss.Id" }, deptInfo.ForeignKeys);
        }

        [Fact]
        public void Columns()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { "Guid", "Id", "Name", "Surname", "Address.Street", "Address.City", "DepartmentId",
                "Department.Guid", "Department.Id", "Image" }, personInfo.Columns);
            Assert.Equal(new string[] { "Guid", "Id", "Name", "Boss.Guid", "Boss.Id", "Tags" }, deptInfo.Columns);
        }

        [Fact]
        public void Column_Names_Dic()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new Dictionary<string, string>
            {
                ["Guid"] = "Guid",
                ["Id"] = "Id",
                ["Name"] = "Name",
                ["Surname"] = "Surname",
                ["Address.Street"] = "AddressStreet",
                ["Address.City"] = "AddressCity",
                ["DepartmentId"] = "DepartmentId",
                ["Department.Guid"] = "DepartmentGuid",
                ["Department.Id"] = "DepartmentId",
                ["Image"] = "Image"
            }, personInfo.ColumnNamesDic);

            Assert.Equal(new Dictionary<string, string>
            {
                ["Guid"] = "Guid",
                ["Id"] = "Id",
                ["Name"] = "Name",
                ["Boss.Guid"] = "BossGuid",
                ["Boss.Id"] = "BossId",
                ["Tags"] = "Tags"
            }, deptInfo.ColumnNamesDic);
        }

        [Fact]
        public void Column_Names()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { "Guid", "Id", "Name", "Surname",  "AddressStreet", "AddressCity", "DepartmentId",
                "DepartmentGuid", "Image" }, personInfo.ColumnNames);
            Assert.Equal(new string[] { "Guid", "Id", "Name", "BossGuid", "BossId", "Tags" }, deptInfo.ColumnNames);
        }
    }
}