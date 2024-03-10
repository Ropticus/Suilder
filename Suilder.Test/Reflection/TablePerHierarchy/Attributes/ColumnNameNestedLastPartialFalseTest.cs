using System.Collections.Generic;
using Suilder.Reflection;
using Suilder.Reflection.Builder;
using Xunit;

namespace Suilder.Test.Reflection.TablePerHierarchy.Attributes
{
    public class ColumnNameNestedLastPartialFalseTest : BaseTest
    {
        protected override void InitConfig()
        {
            tableBuilder.DefaultInheritTable(true);

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
                ["Id"] = "Id2",
                ["Guid"] = "Guid",
                ["Name"] = "Name2",
                ["Surname"] = "Surname",
                ["Address.Street"] = "Street2",
                ["Address.City"] = "AddressCity"
            }, personInfo.ColumnNamesDic);

            Assert.Equal(new Dictionary<string, string>
            {
                ["Id"] = "Id2",
                ["Guid"] = "Guid",
                ["Name"] = "Name2",
                ["Surname"] = "Surname",
                ["Address.Street"] = "Street2",
                ["Address.City"] = "AddressCity",
                ["Salary"] = "Salary2",
                ["DepartmentId"] = "DepartmentId2",
                ["Department.Id"] = "DepartmentId2",
                ["Image"] = "Image2"
            }, employeeInfo.ColumnNamesDic);

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
            ITableInfo employeeInfo = tableBuilder.GetConfig<Employee>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { "Id2", "Guid", "Name2", "Surname", "Street2", "AddressCity" },
                personInfo.ColumnNames);
            Assert.Equal(new string[] { "Id2", "Guid", "Name2", "Surname", "Street2", "AddressCity", "Salary2",
                "DepartmentId2", "Image2" }, employeeInfo.ColumnNames);
            Assert.Equal(new string[] { "Id3", "Guid", "Name3", "BossId3", "Tags3" }, deptInfo.ColumnNames);
        }

        [Nested]
        public class Address
        {
            [Column("Street2", false)]
            public string Street { get; set; }

            public string City { get; set; }
        }

        public abstract class BaseConfig
        {
            public virtual int Id { get; set; }

            public virtual string Guid { get; set; }

            public virtual string Name { get; set; }
        }

        public class Department : BaseConfig
        {
            [Column("Id3", false)]
            public override int Id { get; set; }

            [Column("Name3", false)]
            public override string Name { get; set; }

            [Column("BossId3", false)]
            public virtual Employee Boss { get; set; }

            public virtual List<Employee> Employees { get; set; }

            [Column("Tags3", false)]
            public virtual List<string> Tags { get; set; }
        }

        public class Employee : Person
        {
            [Column("Salary2", false)]
            public virtual decimal Salary { get; set; }

            [Column("DepartmentId2", false)]
            public virtual int DepartmentId { get; set; }

            [Column("DepartmentId2", false)]
            public virtual Department Department { get; set; }

            [Column("Image2", false)]
            public virtual byte[] Image { get; set; }
        }

        public class Person : BaseConfig
        {
            [Column("Id2", false)]
            public override int Id { get; set; }

            [Column("Name2", false)]
            public override string Name { get; set; }

            public virtual string Surname { get; set; }

            public virtual string FullName => $"{Name} {Surname}".TrimEnd();

            public virtual Address Address { get; set; }
        }
    }
}