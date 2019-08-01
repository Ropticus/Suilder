using System;
using System.Collections.Generic;
using Suilder.Reflection;
using Xunit;

namespace Suilder.Test.Reflection
{
    public class DefaultTest : BaseTest
    {
        protected override void Configure()
        {
            tableBuilder.Add<Person>();
            tableBuilder.Add<Department>();
        }

        [Fact]
        public void Table_Name()
        {
            TableInfo personTable = GetConfig<Person>();
            TableInfo deptTable = GetConfig<Department>();

            Assert.Equal("Person", personTable.TableName);
            Assert.Equal("Department", deptTable.TableName);
        }

        [Fact]
        public void Primary_Key()
        {
            TableInfo personTable = GetConfig<Person>();
            TableInfo deptTable = GetConfig<Department>();

            Assert.Equal(new string[] { "Id" }, personTable.PrimaryKeys);
            Assert.Equal(new string[] { "Id" }, deptTable.PrimaryKeys);
        }

        [Fact]
        public void Columns()
        {
            TableInfo personTable = GetConfig<Person>();
            TableInfo deptTable = GetConfig<Department>();

            Assert.Equal(new string[] { "Id", "Active", "Name", "SurName", "Salary", "Created", "DepartmentId",
                "Department.Id" }, personTable.Columns);
            Assert.Equal(new string[] { "Id", "Active", "Name", "Boss.Id" }, deptTable.Columns);
        }

        [Fact]
        public void Columns_Names()
        {
            TableInfo personTable = GetConfig<Person>();
            TableInfo deptTable = GetConfig<Department>();

            Assert.Equal(new Dictionary<string, string>
            {
                ["Id"] = "Id",
                ["Active"] = "Active",
                ["Name"] = "Name",
                ["SurName"] = "SurName",
                ["Salary"] = "Salary",
                ["Created"] = "Created",
                ["DepartmentId"] = "DepartmentId",
                ["Department.Id"] = "DepartmentId",
            }, personTable.ColumnNamesDic);

            Assert.Equal(new Dictionary<string, string>
            {
                ["Id"] = "Id",
                ["Active"] = "Active",
                ["Name"] = "Name",
                ["Boss.Id"] = "BossId"
            }, deptTable.ColumnNamesDic);
        }

        [Fact]
        public void Columns_Names_List()
        {
            TableInfo personTable = GetConfig<Person>();
            TableInfo deptTable = GetConfig<Department>();

            Assert.Equal(new string[] { "Id", "Active", "Name", "SurName", "Salary", "Created", "DepartmentId" },
                personTable.ColumnNames);
            Assert.Equal(new string[] { "Id", "Active", "Name", "BossId" }, deptTable.ColumnNames);
        }

        public class Department
        {
            public int Id { get; set; }

            public bool Active { get; set; }

            public string Name { get; set; }

            public Person Boss { get; set; }

            public List<Person> Employees { get; set; }
        }

        public class Person
        {
            public int Id { get; set; }

            public bool Active { get; set; }

            public string Name { get; set; }

            public string SurName { get; set; }

            public string FullName => $"{Name} {SurName}".TrimEnd();

            public decimal Salary { get; set; }

            public DateTime Created { get; set; }

            public int DepartmentId { get; set; }

            public Department Department { get; set; }
        }
    }
}