using System.Collections.Generic;
using Suilder.Reflection;
using Suilder.Reflection.Builder;
using Xunit;

namespace Suilder.Test.Reflection.NoInherit.Attributes
{
    public class ColumnNameNestedLastPartialTest : BaseTest
    {
        protected override void InitConfig()
        {
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
                ["Id"] = "Id2",
                ["Guid"] = "Guid",
                ["Name"] = "Name2",
                ["Surname"] = "Surname",
                ["Address.Street"] = "AddressStreet2",
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

            Assert.Equal(new string[] { "Id2", "Guid", "Name2", "Surname", "AddressStreet2", "AddressCity", "DepartmentId2",
                "Image2" }, personInfo.ColumnNames);
            Assert.Equal(new string[] { "Id3", "Guid", "Name3", "BossId3", "Tags3" }, deptInfo.ColumnNames);
        }

        [Nested]
        public class Address
        {
            [Column("Street2", true)]
            public string Street { get; set; }

            public string City { get; set; }
        }

        public class Department
        {
            [Column("Id3", true)]
            public int Id { get; set; }

            public string Guid { get; set; }

            [Column("Name3", true)]
            public string Name { get; set; }

            [Column("BossId3", true)]
            public Person Boss { get; set; }

            public List<Person> Employees { get; set; }

            [Column("Tags3", true)]
            public List<string> Tags { get; set; }
        }

        public class Person
        {
            [Column("Id2", true)]
            public int Id { get; set; }

            public string Guid { get; set; }

            [Column("Name2", true)]
            public string Name { get; set; }

            public string Surname { get; set; }

            public string FullName => $"{Name} {Surname}".TrimEnd();

            public Address Address { get; set; }

            [Column("DepartmentId2", true)]
            public int DepartmentId { get; set; }

            [Column("DepartmentId2", true)]
            public Department Department { get; set; }

            [Column("Image2", true)]
            public byte[] Image { get; set; }
        }
    }
}