using System;
using System.Collections.Generic;
using Suilder.Exceptions;
using Suilder.Reflection;
using Suilder.Reflection.Builder;
using Suilder.Reflection.Builder.Processors;
using Suilder.Test.Reflection.Builder.TablePerType.Tables;
using Xunit;
using Nested = Suilder.Test.Reflection.Builder.TableNested.Tables;

namespace Suilder.Test.Reflection.Builder
{
    public class TableBuilderTest : BaseTest
    {
        [Fact]
        public void Add_Type()
        {
            tableBuilder.Add(typeof(Person));

            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            Assert.NotNull(personInfo);
        }

        [Fact]
        public void Add_Type_Delegate()
        {
            tableBuilder.Add(typeof(Person), config => config
                    .PrimaryKey("Guid"))
                .Add(typeof(Department), config => config
                    .PrimaryKey("Guid"));

            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { "Guid" }, personInfo.PrimaryKeys);
            Assert.Equal(new string[] { "Guid" }, deptInfo.PrimaryKeys);
        }

        [Fact]
        public void Add_Type_Generic()
        {
            tableBuilder.Add<Person>();

            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            Assert.NotNull(personInfo);
        }

        [Fact]
        public void Add_Type_Generic_Delegate()
        {
            tableBuilder.Add<Person>(config => config
                .PrimaryKey(x => x.Guid));

            tableBuilder.Add<Person>(config => config
                    .PrimaryKey("Guid"))
                .Add<Department>(config => config
                    .PrimaryKey("Guid"));

            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { "Guid" }, personInfo.PrimaryKeys);
            Assert.Equal(new string[] { "Guid" }, deptInfo.PrimaryKeys);
        }

        [Fact]
        public void Add_Type_Invalid_Object()
        {
            Exception ex = Assert.Throws<InvalidConfigurationException>(() => tableBuilder.Add<object>());
            Assert.Equal($"Invalid type \"{typeof(object)}\".", ex.Message);
        }

        [Fact]
        public void Add_Type_Invalid_Interface()
        {
            Exception ex = Assert.Throws<InvalidConfigurationException>(() => tableBuilder.Add<ITableBuilder>());
            Assert.Equal($"Invalid type \"{typeof(ITableBuilder)}\".", ex.Message);
        }

        [Fact]
        public void Reset_Type()
        {
            tableBuilder.Add(typeof(Person))
                .PrimaryKey("Guid");

            tableBuilder.Reset(typeof(Person));

            ITableInfo personInfo = tableBuilder.GetConfig<Person>();

            Assert.Equal(new string[] { "Id" }, personInfo.PrimaryKeys);
        }

        [Fact]
        public void Reset_Type_Generic()
        {
            tableBuilder.Add<Person>()
                .PrimaryKey(x => x.Guid);

            tableBuilder.Reset<Person>();

            ITableInfo personInfo = tableBuilder.GetConfig<Person>();

            Assert.Equal(new string[] { "Id" }, personInfo.PrimaryKeys);
        }

        [Fact]
        public void Add_Nested_Type()
        {
            tableBuilder.Add(typeof(Nested.Person));

            tableBuilder.AddNested(typeof(Nested.Employee));

            tableBuilder.Add(typeof(Nested.Department));

            ITableInfo personInfo = tableBuilder.GetConfig<Nested.Person>();

            Assert.Equal(new string[] { "Id", "Guid", "Name", "SurName", "Employee.Address.Street", "Employee.Address.City",
                "Employee.Salary", "Employee.DepartmentId", "Employee.Department.Id", "Employee.Image" },
                personInfo.Columns);
        }

        [Fact]
        public void Add_Nested_Type_Generic()
        {
            tableBuilder.Add<Nested.Person>();

            tableBuilder.AddNested<Nested.Employee>();

            tableBuilder.Add<Nested.Department>();

            ITableInfo personInfo = tableBuilder.GetConfig<Nested.Person>();

            Assert.Equal(new string[] { "Id", "Guid", "Name", "SurName", "Employee.Address.Street", "Employee.Address.City",
                "Employee.Salary", "Employee.DepartmentId", "Employee.Department.Id", "Employee.Image" },
                personInfo.Columns);
        }

        [Fact]
        public void Add_Nested_Type_Invalid_Object()
        {
            Exception ex = Assert.Throws<InvalidConfigurationException>(() => tableBuilder.AddNested<object>());
            Assert.Equal($"Invalid type \"{typeof(object)}\".", ex.Message);
        }

        [Fact]
        public void Add_Nested_Type_Invalid_Interface()
        {
            Exception ex = Assert.Throws<InvalidConfigurationException>(() => tableBuilder.AddNested<ITableBuilder>());
            Assert.Equal($"Invalid type \"{typeof(ITableBuilder)}\".", ex.Message);
        }

        [Fact]
        public void Remove_Nested_Type()
        {
            tableBuilder.Add(typeof(Nested.Person));

            tableBuilder.AddNested(typeof(Nested.Employee));

            tableBuilder.Add(typeof(Nested.Department));

            tableBuilder.RemoveNested(typeof(Nested.Employee));

            ITableInfo personInfo = tableBuilder.GetConfig<Nested.Person>();

            Assert.Equal(new string[] { "Id", "Guid", "Name", "SurName", "Employee" }, personInfo.Columns);
        }

        [Fact]
        public void Remove_Nested_Type_Generic()
        {
            tableBuilder.Add<Nested.Person>();

            tableBuilder.AddNested<Nested.Employee>();

            tableBuilder.Add<Nested.Department>();

            tableBuilder.RemoveNested<Nested.Employee>();

            ITableInfo personInfo = tableBuilder.GetConfig<Nested.Person>();

            Assert.Equal(new string[] { "Id", "Guid", "Name", "SurName", "Employee" }, personInfo.Columns);
        }

        [Fact]
        public void Get_Registered_Types()
        {
            tableBuilder.Add<Employee>();

            tableBuilder.Add<Department>();

            Assert.Equal(new Type[] { typeof(BaseConfig), typeof(Person), typeof(Employee), typeof(Department) },
                tableBuilder.GetRegisteredTypes());
        }

        [Fact]
        public void Invalid_Initialized()
        {
            tableBuilder.Add<Person>();
            tableBuilder.GetConfig();

            Exception ex = Assert.Throws<InvalidOperationException>(() => tableBuilder.Add<Department>());
            Assert.Equal("The builder is already initialized.", ex.Message);
        }

        [Fact]
        public void IsTable()
        {
            tableBuilder.Add<BaseConfig>()
                .IsTable(true);

            tableBuilder.Add<Person>()
                .IsTable(false);

            ITableInfo baseInfo = tableBuilder.GetConfig<BaseConfig>();
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();

            Assert.NotNull(baseInfo);
            Assert.Null(personInfo);
        }

        [Fact]
        public void Clear_Processors()
        {
            tableBuilder.Add<Employee>();

            tableBuilder.Add<Department>();

            tableBuilder.ClearProcessors();

            Assert.Equal(new TableInfo[0], tableBuilder.GetConfig());
        }

        [Fact]
        public void Enable_Attributes()
        {
            tableBuilder.Add<Attr.Person>();

            tableBuilder.DisableAttributes();
            tableBuilder.EnableAttributes();

            ITableInfo personInfo = tableBuilder.GetConfig<Attr.Person>();

            Assert.Equal("prefix_Person", personInfo.TableName);
            Assert.Equal(new string[] { "Guid" }, personInfo.PrimaryKeys);
            Assert.Equal(new string[] { "Guid2", "Id2" }, personInfo.ColumnNames);
        }

        [Fact]
        public void Disable_Attributes()
        {
            tableBuilder.Add<Attr.Person>();

            tableBuilder.DisableAttributes();

            ITableInfo personInfo = tableBuilder.GetConfig<Attr.Person>();

            Assert.Equal("Person", personInfo.TableName);
            Assert.Equal(new string[] { "Id" }, personInfo.PrimaryKeys);
            Assert.Equal(new string[] { "Id", "Guid" }, personInfo.ColumnNames);
        }

        [Fact]
        public void Enable_Metadata()
        {
            tableBuilder.AddProcessor(new DefaultMetadataProcessor());

            tableBuilder.Add<Person>()
                .AddTableMetadata("Key1", "Val1_Person")
                .AddMetadata(x => x.Id, "Id1", "Id1_Person");

            tableBuilder.DisableMetadata();
            tableBuilder.EnableMetadata();

            ITableInfo personInfo = tableBuilder.GetConfig<Person>();

            Assert.Equal(new Dictionary<string, object>
            {
                ["Key1"] = "Val1_Person"
            }, personInfo.TableMetadata);

            Assert.Equal(new Dictionary<string, IDictionary<string, object>>
            {
                ["Id"] = new Dictionary<string, object>
                {
                    ["Id1"] = "Id1_Person"
                }
            }, personInfo.MemberMetadata);
        }

        [Fact]
        public void Disable_Metadata()
        {
            tableBuilder.AddProcessor(new DefaultMetadataProcessor());

            tableBuilder.Add<Person>()
                .AddTableMetadata("Key1", "Val1_Person")
                .AddMetadata(x => x.Id, "Id1", "Id1_Person");

            tableBuilder.DisableMetadata();

            ITableInfo personInfo = tableBuilder.GetConfig<Person>();

            Assert.Equal(new Dictionary<string, object>(), personInfo.TableMetadata);
            Assert.Equal(new Dictionary<string, object>(), personInfo.TableMetadata);
        }

        private class Attr
        {
            [Table("prefix_Person")]
            public class Person
            {
                [Column("Id2")]
                public int Id { get; set; }

                [PrimaryKey]
                [Column("Guid2")]
                public string Guid { get; set; }
            }
        }
    }
}