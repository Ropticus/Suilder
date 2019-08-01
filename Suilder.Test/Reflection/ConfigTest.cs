using System;
using System.Collections.Generic;
using Suilder.Reflection;
using Xunit;

namespace Suilder.Test.Reflection
{
    public class ConfigTest : BaseTest
    {
        protected override void Configure()
        {
            tableBuilder
                .PrimaryKey(x => "Guid")
                .TableName(x => "prefix_" + x.Name);

            tableBuilder.Add<Person>(config => config
                .ColumnName(x => x.Created, "DateCreated")
                .ForeignKey(x => x.DepartmentId, "DeptId")
                .ForeignKey(x => x.Department.Id, "DeptId")
                .Ignore(x => x.Ignore));
            tableBuilder.Add<Department>(config => config
                .WithName("Dept")
                .PrimaryKey(x => x.Id)
                .ColumnName(x => x.Id, "DeptId")
                .ForeignKey(x => x.Boss.Guid, "ManagerId"));
        }

        [Fact]
        public void Table_Name()
        {
            TableInfo personTable = GetConfig<Person>();
            TableInfo deptTable = GetConfig<Department>();

            Assert.Equal("prefix_Person", personTable.TableName);
            Assert.Equal("Dept", deptTable.TableName);
        }

        [Fact]
        public void Primary_Key()
        {
            TableInfo personTable = GetConfig<Person>();
            TableInfo deptTable = GetConfig<Department>();

            Assert.Equal(new string[] { "Guid" }, personTable.PrimaryKeys);
            Assert.Equal(new string[] { "Id" }, deptTable.PrimaryKeys);
        }

        [Fact]
        public void Columns()
        {
            TableInfo personTable = GetConfig<Person>();
            TableInfo deptTable = GetConfig<Department>();

            Assert.Equal(new string[] { "Guid", "Id", "Active", "Name", "SurName", "Salary", "Created", "DepartmentId",
                "Department.Id" }, personTable.Columns);
            Assert.Equal(new string[] { "Id", "Guid", "Active", "Name", "Boss.Guid" }, deptTable.Columns);
        }

        [Fact]
        public void Columns_Names()
        {
            TableInfo personTable = GetConfig<Person>();
            TableInfo deptTable = GetConfig<Department>();

            Assert.Equal(new Dictionary<string, string>
            {
                ["Id"] = "Id",
                ["Guid"] = "Guid",
                ["Active"] = "Active",
                ["Name"] = "Name",
                ["SurName"] = "SurName",
                ["Salary"] = "Salary",
                ["Created"] = "DateCreated",
                ["DepartmentId"] = "DeptId",
                ["Department.Id"] = "DeptId",
            }, personTable.ColumnNamesDic);

            Assert.Equal(new Dictionary<string, string>
            {
                ["Id"] = "DeptId",
                ["Guid"] = "Guid",
                ["Active"] = "Active",
                ["Name"] = "Name",
                ["Boss.Guid"] = "ManagerId"
            }, deptTable.ColumnNamesDic);
        }

        [Fact]
        public void Columns_Names_List()
        {
            TableInfo personTable = GetConfig<Person>();
            TableInfo deptTable = GetConfig<Department>();

            Assert.Equal(new string[] { "Guid", "Id", "Active", "Name", "SurName", "Salary", "DateCreated", "DeptId" },
                personTable.ColumnNames);
            Assert.Equal(new string[] { "DeptId", "Guid", "Active", "Name", "ManagerId" }, deptTable.ColumnNames);
        }

        public class Department
        {
            public int Id { get; set; }

            public string Guid { get; set; }

            public bool Active { get; set; }

            public string Name { get; set; }

            public Person Boss { get; set; }

            public List<Person> Employees { get; set; }
        }

        public class Person
        {
            public int Id { get; set; }

            public string Guid { get; set; }

            public bool Active { get; set; }

            public string Name { get; set; }

            public string SurName { get; set; }

            public string FullName => $"{Name} {SurName}".TrimEnd();

            public decimal Salary { get; set; }

            public DateTime Created { get; set; }

            public int DepartmentId { get; set; }

            public Department Department { get; set; }

            public string Ignore { get; set; }
        }
    }
}