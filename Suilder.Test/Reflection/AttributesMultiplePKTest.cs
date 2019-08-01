using System;
using System.Collections.Generic;
using Suilder.Reflection;
using Xunit;

namespace Suilder.Test.Reflection
{
    public class AttributesMultiplePKTest : BaseTest
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

            Assert.Equal(new string[] { "Id2", "Id1" }, personTable.PrimaryKeys);
            Assert.Equal(new string[] { "Id" }, deptTable.PrimaryKeys);
        }

        [Fact]
        public void Columns()
        {
            TableInfo personTable = GetConfig<Person>();
            TableInfo deptTable = GetConfig<Department>();

            Assert.Equal(new string[] { "Id2", "Id1", "Active", "Name", "SurName", "Salary", "Created", "DepartmentId",
                "Department.Id" }, personTable.Columns);
            Assert.Equal(new string[] { "Id", "Active", "Name", "Boss.Id2", "Boss.Id1" }, deptTable.Columns);
        }

        [Fact]
        public void Columns_Names()
        {
            TableInfo personTable = GetConfig<Person>();
            TableInfo deptTable = GetConfig<Department>();

            Assert.Equal(new Dictionary<string, string>
            {
                ["Id1"] = "Id1",
                ["Id2"] = "Id2",
                ["Active"] = "Active",
                ["Name"] = "Name",
                ["SurName"] = "SurName",
                ["Salary"] = "Salary",
                ["Created"] = "DateCreated",
                ["DepartmentId"] = "DepartmentId",
                ["Department.Id"] = "DepartmentId",
            }, personTable.ColumnNamesDic);

            Assert.Equal(new Dictionary<string, string>
            {
                ["Id"] = "Id",
                ["Active"] = "Active",
                ["Name"] = "Name",
                ["Boss.Id2"] = "BossId2",
                ["Boss.Id1"] = "BossId1"
            }, deptTable.ColumnNamesDic);
        }

        [Fact]
        public void Columns_Names_List()
        {
            TableInfo personTable = GetConfig<Person>();
            TableInfo deptTable = GetConfig<Department>();

            Assert.Equal(new string[] { "Id2", "Id1", "Active", "Name", "SurName", "Salary", "DateCreated", "DepartmentId" },
                personTable.ColumnNames);
            Assert.Equal(new string[] { "Id", "Active", "Name", "BossId2", "BossId1" }, deptTable.ColumnNames);
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
            [PrimaryKey(2)]
            public int Id1 { get; set; }

            [PrimaryKey(1)]
            public int Id2 { get; set; }

            public bool Active { get; set; }

            public string Name { get; set; }

            public string SurName { get; set; }

            public string FullName => $"{Name} {SurName}".TrimEnd();

            public decimal Salary { get; set; }

            [Column("DateCreated")]
            public DateTime Created { get; set; }

            public int DepartmentId { get; set; }

            public Department Department { get; set; }
        }
    }
}