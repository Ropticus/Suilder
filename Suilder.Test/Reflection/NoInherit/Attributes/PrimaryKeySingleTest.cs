using System.Collections.Generic;
using Suilder.Reflection;
using Suilder.Reflection.Builder;
using Xunit;

namespace Suilder.Test.Reflection.NoInherit.Attributes
{
    public class PrimaryKeySingleTest : BaseTest
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

            Assert.Equal(new string[] { "Guid" }, personInfo.PrimaryKeys);
            Assert.Equal(new string[] { "Guid" }, deptInfo.PrimaryKeys);
        }

        [Fact]
        public void Foreign_Keys()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { "Department.Guid" }, personInfo.ForeignKeys);
            Assert.Equal(new string[] { "Boss.Guid" }, deptInfo.ForeignKeys);
        }

        [Fact]
        public void Columns()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { "Guid", "Id", "Name", "Surname", "Address.Street", "Address.City", "DepartmentId",
                "Department.Guid", "Image" }, personInfo.Columns);
            Assert.Equal(new string[] { "Guid", "Id", "Name", "Boss.Guid", "Tags" }, deptInfo.Columns);
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
                ["Image"] = "Image"
            }, personInfo.ColumnNamesDic);

            Assert.Equal(new Dictionary<string, string>
            {
                ["Guid"] = "Guid",
                ["Id"] = "Id",
                ["Name"] = "Name",
                ["Boss.Guid"] = "BossGuid",
                ["Tags"] = "Tags"
            }, deptInfo.ColumnNamesDic);
        }

        [Fact]
        public void Column_Names()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { "Guid", "Id", "Name", "Surname", "AddressStreet", "AddressCity", "DepartmentId",
                "DepartmentGuid", "Image" }, personInfo.ColumnNames);
            Assert.Equal(new string[] { "Guid", "Id", "Name", "BossGuid", "Tags" }, deptInfo.ColumnNames);
        }

        [Nested]
        public class Address
        {
            public string Street { get; set; }

            public string City { get; set; }
        }

        public class Department
        {
            public int Id { get; set; }

            [PrimaryKey]
            public string Guid { get; set; }

            public string Name { get; set; }

            public Person Boss { get; set; }

            public List<Person> Employees { get; set; }

            public List<string> Tags { get; set; }
        }

        public class Person
        {
            public int Id { get; set; }

            [PrimaryKey]
            public string Guid { get; set; }

            public string Name { get; set; }

            public string Surname { get; set; }

            public string FullName => $"{Name} {Surname}".TrimEnd();

            public Address Address { get; set; }

            public int DepartmentId { get; set; }

            public Department Department { get; set; }

            public byte[] Image { get; set; }
        }
    }
}