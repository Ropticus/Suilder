using System;
using System.Collections.Generic;
using Suilder.Reflection;
using Xunit;

namespace Suilder.Test.Reflection
{
    public class AttributesTPHTest : BaseTest
    {
        protected override void Configure()
        {
            tableBuilder.Add<BaseConfig>(config => config
                .ColumnName(x => x.Created, "DateCreated")
                .Ignore(x => x.Ignore));

            tableBuilder.Add<Person>();
            tableBuilder.Add<Employee>();
            tableBuilder.Add<Department>();
        }

        [Fact]
        public void Table_Name()
        {
            TableInfo personTable = GetConfig<Person>();
            TableInfo employeeTable = GetConfig<Employee>();
            TableInfo deptTable = GetConfig<Department>();

            Assert.Equal("Person", personTable.TableName);
            Assert.Equal("Person", employeeTable.TableName);
            Assert.Equal("Department", deptTable.TableName);
        }

        [Fact]
        public void Primary_Key()
        {
            TableInfo personTable = GetConfig<Person>();
            TableInfo employeeTable = GetConfig<Employee>();
            TableInfo deptTable = GetConfig<Department>();

            Assert.Equal(new string[] { "Id" }, personTable.PrimaryKeys);
            Assert.Equal(new string[] { "Id" }, employeeTable.PrimaryKeys);
            Assert.Equal(new string[] { "Id" }, deptTable.PrimaryKeys);
        }

        [Fact]
        public void Columns()
        {
            TableInfo personTable = GetConfig<Person>();
            TableInfo employeeTable = GetConfig<Employee>();
            TableInfo deptTable = GetConfig<Department>();

            Assert.Equal(new string[] { "Id", "Guid", "Active", "Name", "Created", "SurName", "PersonType" },
                personTable.Columns);
            Assert.Equal(new string[] { "Id", "Guid", "Active", "Name", "Created", "SurName", "PersonType", "Salary",
                "DepartmentId", "Department.Id" }, employeeTable.Columns);
            Assert.Equal(new string[] { "Id", "Guid", "Active", "Name", "Boss.Id" }, deptTable.Columns);
        }

        [Fact]
        public void Columns_Names()
        {
            TableInfo personTable = GetConfig<Person>();
            TableInfo employeeTable = GetConfig<Employee>();
            TableInfo deptTable = GetConfig<Department>();

            Assert.Equal(new Dictionary<string, string>
            {
                ["Id"] = "Id",
                ["Guid"] = "Guid",
                ["Active"] = "Active",
                ["Name"] = "Name",
                ["Created"] = "DateCreated",
                ["SurName"] = "SurName",
                ["PersonType"] = "PersonType"
            }, personTable.ColumnNamesDic);

            Assert.Equal(new Dictionary<string, string>
            {
                ["Id"] = "Id",
                ["Guid"] = "Guid",
                ["Active"] = "Active",
                ["Name"] = "Name",
                ["Created"] = "DateCreated",
                ["SurName"] = "SurName",
                ["PersonType"] = "PersonType",
                ["Salary"] = "Salary",
                ["DepartmentId"] = "DepartmentId",
                ["Department.Id"] = "DepartmentId"
            }, employeeTable.ColumnNamesDic);

            Assert.Equal(new Dictionary<string, string>
            {
                ["Id"] = "Id",
                ["Guid"] = "Guid",
                ["Active"] = "Active",
                ["Name"] = "Name",
                ["Boss.Id"] = "BossId"
            }, deptTable.ColumnNamesDic);
        }

        [Fact]
        public void Columns_Names_List()
        {
            TableInfo personTable = GetConfig<Person>();
            TableInfo employeeTable = GetConfig<Employee>();
            TableInfo deptTable = GetConfig<Department>();

            Assert.Equal(new string[] { "Id", "Guid", "Active", "Name", "DateCreated", "SurName", "PersonType" },
                personTable.ColumnNames);
            Assert.Equal(new string[] { "Id", "Guid", "Active", "Name", "DateCreated", "SurName", "PersonType", "Salary",
                "DepartmentId" }, employeeTable.ColumnNames);
            Assert.Equal(new string[] { "Id", "Guid", "Active", "Name", "BossId" }, deptTable.ColumnNames);
        }

        public abstract class BaseConfig
        {
            public int Id { get; set; }

            public string Guid { get; set; }

            public bool Active { get; set; }

            public string Name { get; set; }

            [Column("DateCreated")]
            public virtual DateTime Created { get; set; }

            [Ignore]
            public string Ignore { get; set; }
        }

        public class Department : BaseConfig
        {
            public Employee Boss { get; set; }

            public List<Employee> Employees { get; set; }

            [Ignore]
            public override DateTime Created { get; set; }
        }

        public class Person : BaseConfig
        {
            public string SurName { get; set; }

            public string FullName => $"{Name} {SurName}".TrimEnd();

            public string PersonType { get; set; }
        }

        [Table(InheritTable = true)]
        public class Employee : Person
        {
            public decimal Salary { get; set; }

            public int DepartmentId { get; set; }

            public Department Department { get; set; }
        }
    }
}