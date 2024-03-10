using System.Collections.Generic;
using Suilder.Reflection;
using Suilder.Reflection.Builder;
using Xunit;

namespace Suilder.Test.Reflection.Builder
{
    public class AttributeOverrideTest : BaseTest
    {
        protected override void InitConfig()
        {
            tableBuilder.Add<Person>()
                .InheritTable(false)
                .InheritColumns(true)
                .Schema("schema")
                .TableName("Person")
                .PrimaryKey(x => x.Id)
                .ColumnName(x => x.Id, "Id")
                .ColumnName(x => x.Address, "Address")
                .ColumnName(x => x.Address.Street, "Street");

            tableBuilder.Add<Employee>()
                .InheritTable(false)
                .InheritColumns(false)
                .ForeignKey(x => x.DepartmentId, "DepartmentId")
                .ForeignKey(x => x.Department.Id, "DepartmentId");

            tableBuilder.Add<Department>()
                .InheritTable(false)
                .InheritColumns(true);
        }

        [Fact]
        public void Schema_Name()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo employeeInfo = tableBuilder.GetConfig<Employee>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal("schema", personInfo.Schema);
            Assert.Null(employeeInfo.Schema);
            Assert.Null(deptInfo.Schema);
        }

        [Fact]
        public void Table_Name()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo employeeInfo = tableBuilder.GetConfig<Employee>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal("Person", personInfo.TableName);
            Assert.Equal("Employee", employeeInfo.TableName);
            Assert.Equal("Department", deptInfo.TableName);
        }

        [Fact]
        public void Primary_Keys()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo employeeInfo = tableBuilder.GetConfig<Employee>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { "Id" }, personInfo.PrimaryKeys);
            Assert.Equal(new string[] { "Id" }, employeeInfo.PrimaryKeys);
            Assert.Equal(new string[] { "Guid" }, deptInfo.PrimaryKeys);
        }

        [Fact]
        public void Foreign_Keys()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo employeeInfo = tableBuilder.GetConfig<Employee>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { }, personInfo.ForeignKeys);
            Assert.Equal(new string[] { "DepartmentId", "Department.Id" }, employeeInfo.ForeignKeys);
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
            Assert.Equal(new string[] { "Id", "Salary", "DepartmentId", "Department.Id", "Image" }, employeeInfo.Columns);
            Assert.Equal(new string[] { "Guid", "Id", "Name", "Boss.Id", "Tags" }, deptInfo.Columns);
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
                ["Address.Street"] = "Street",
                ["Address.City"] = "AddressCity"
            }, personInfo.ColumnNamesDic);

            Assert.Equal(new Dictionary<string, string>
            {
                ["Id"] = "Id",
                ["Salary"] = "Salary",
                ["DepartmentId"] = "DepartmentId",
                ["Department.Id"] = "DepartmentId",
                ["Image"] = "Image"
            }, employeeInfo.ColumnNamesDic);

            Assert.Equal(new Dictionary<string, string>
            {
                ["Id"] = "Id2",
                ["Guid"] = "Guid",
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

            Assert.Equal(new string[] { "Id", "Guid", "Name", "Surname", "Street", "AddressCity" }, personInfo.ColumnNames);
            Assert.Equal(new string[] { "Id", "Salary", "DepartmentId", "Image" }, employeeInfo.ColumnNames);
            Assert.Equal(new string[] { "Guid", "Id2", "Name", "BossId", "Tags" }, deptInfo.ColumnNames);
        }

        [Nested]
        public class Address
        {
            [Column("Street2", true)]
            public string Street { get; set; }

            public string City { get; set; }
        }

        public abstract class BaseConfig
        {
            [Column("Id2")]
            public virtual int Id { get; set; }

            [PrimaryKey]
            public virtual string Guid { get; set; }

            public virtual string Name { get; set; }
        }

        [Table(InheritTable = true, InheritColumns = false)]
        public class Department : BaseConfig
        {
            public virtual Employee Boss { get; set; }

            public virtual List<Employee> Employees { get; set; }

            public List<string> Tags { get; set; }
        }

        [Table(InheritTable = true, InheritColumns = true)]
        public class Employee : Person
        {
            public virtual decimal Salary { get; set; }

            [ForeignKey("DepartmentGuid")]
            public virtual int DepartmentId { get; set; }

            [ForeignKey(PropertyName = "Guid", Name = "DepartmentGuid")]
            public virtual Department Department { get; set; }

            public byte[] Image { get; set; }
        }

        [Table("prefix_Person", Schema = "schema_Person", InheritTable = true, InheritColumns = false)]
        public class Person : BaseConfig
        {
            public virtual string Surname { get; set; }

            public virtual string FullName => $"{Name} {Surname}".TrimEnd();

            [Column("Address2", true)]
            public virtual Address Address { get; set; }
        }
    }
}