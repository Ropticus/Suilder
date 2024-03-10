using System.Collections.Generic;
using Suilder.Reflection.Builder;
using Suilder.Test.Reflection.NoInherit.Tables;
using Xunit;

namespace Suilder.Test.Reflection.NoInherit.PropertyBuilder
{
    public class ColumnNameNestedLastPartialFalseTest : BaseTest
    {
        protected override void InitConfig()
        {
            tableBuilder.Add<Person>()
                .Property(x => x.Id)
                .ColumnName("Id2", false);

            tableBuilder.Add<Person>()
                .Property(x => x.Name)
                .ColumnName("Name2", false);

            tableBuilder.Add<Person>()
                .Property(x => x.Address.Street)
                .ColumnName("Street2", false);

            tableBuilder.Add<Person>()
                .Property(x => x.DepartmentId)
                .ColumnName("DepartmentId2", false);

            tableBuilder.Add<Person>()
                .Property(x => x.Department.Id)
                .ColumnName("DepartmentId2", false);

            tableBuilder.Add<Person>()
                .Property(x => x.Image)
                .ColumnName("Image2", false);

            tableBuilder.Add<Department>()
                .Property(x => x.Id)
                .ColumnName("Id3", false);

            tableBuilder.Add<Department>()
                .Property(x => x.Name)
                .ColumnName("Name3", false);

            tableBuilder.Add<Department>()
                .Property(x => x.Boss.Id)
                .ColumnName("BossId3", false);

            tableBuilder.Add<Department>()
                .Property(x => x.Tags)
                .ColumnName("Tags3", false);
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
                ["Id"] = "Id2",
                ["Guid"] = "Guid",
                ["Name"] = "Name2",
                ["Surname"] = "Surname",
                ["Address.Street"] = "Street2",
                ["Address.City"] = "AddressCity",
                ["DepartmentId"] = "DepartmentId2",
                ["Department.Id"] = "DepartmentId2",
                ["Image"] = "Image2"
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

            Assert.Equal(new string[] { "Id2", "Guid", "Name2", "Surname", "Street2", "AddressCity", "DepartmentId2",
                "Image2" }, personInfo.ColumnNames);
            Assert.Equal(new string[] { "Id3", "Guid", "Name3", "BossId3", "Tags3" }, deptInfo.ColumnNames);
        }
    }
}