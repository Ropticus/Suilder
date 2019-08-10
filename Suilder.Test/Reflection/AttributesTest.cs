using System;
using System.Collections.Generic;
using Suilder.Reflection;
using Xunit;

namespace Suilder.Test.Reflection
{
    public class AttributesTest : BaseTest
    {
        protected override void Configure()
        {
            // This config must be overridden
            tableBuilder
                .PrimaryKey(x => "Active")
                .TableName(x => "prefix_" + x.Name);

            tableBuilder.Add<Person>();
            tableBuilder.Add<Department>();
        }

        [Fact]
        public void Table_Name()
        {
            TableInfo personTable = GetConfig<Person>();
            TableInfo deptTable = GetConfig<Department>();

            Assert.Equal("Employee", personTable.TableName);
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

        [Table("Dept")]
        public class Department
        {
            [PrimaryKey]
            [Column("DeptId")]
            public int Id { get; set; }

            public string Guid { get; set; }

            public bool Active { get; set; }

            public string Name { get; set; }

            [ForeignKey("ManagerId")]
            public Person Boss { get; set; }

            public List<Person> Employees { get; set; }
        }

        [Table("Employee")]
        public class Person
        {
            public int Id { get; set; }

            [PrimaryKey]
            public string Guid { get; set; }

            public bool Active { get; set; }

            public string Name { get; set; }

            public string SurName { get; set; }

            public string FullName => $"{Name} {SurName}".TrimEnd();

            public decimal Salary { get; set; }

            [Column("DateCreated")]
            public DateTime Created { get; set; }

            [ForeignKey("DeptId")]
            public int DepartmentId { get; set; }

            [ForeignKey("DeptId")]
            public Department Department { get; set; }

            [Ignore]
            public string Ignore { get; set; }
        }
    }
}