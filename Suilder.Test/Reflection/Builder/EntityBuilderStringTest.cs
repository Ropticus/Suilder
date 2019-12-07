using System;
using System.Collections.Generic;
using Suilder.Reflection.Builder;
using Suilder.Reflection.Builder.Processors;
using Suilder.Test.Reflection.Builder.NoInherit.Tables;
using Xunit;

namespace Suilder.Test.Reflection.Builder
{
    public class EntityBuilderStringTest : BaseTest
    {
        protected override void InitConfig()
        {
            tableBuilder.AddProcessor(new DefaultMetadataProcessor());

            tableBuilder.Add<Person>()
                .TableName("prefix_Person")
                .AddTableMetadata("Key1", "Val1_Person")
                .AddTableMetadata("Key2", "Val2_Person")
                .RemoveTableMetadata("Key2")
                .ColumnName("Id", "Id2")
                .AddMetadata("Id", "Id1", "Id1_Person")
                .AddMetadata("Id", "Id2", "Id2_Person")
                .RemoveMetadata("Id", "Id2")
                .PrimaryKey("Guid")
                .ColumnName("Guid", "Guid2")
                .AddMetadata("Guid", "Guid1", "Guid1_Person")
                .AddMetadata("Guid", "Guid2", "Guid2_Person")
                .RemoveMetadata("Guid", "Guid2")
                .ColumnName("Name", "Name2")
                .AddMetadata("Name", "Name1", "Name1_Person")
                .AddMetadata("Name", "Name2", "Name2_Person")
                .RemoveMetadata("Name", "Name2")
                .Ignore("SurName")
                .AddMetadata("SurName", "SurName1", "SurName1_Person")
                .AddMetadata("SurName", "SurName2", "SurName2_Person")
                .RemoveMetadata("SurName", "SurName2")
                .ColumnName("Address", "Address2")
                .AddMetadata("Address", "Address1", "Address1_Person")
                .AddMetadata("Address", "Address2", "Address2_Person")
                .RemoveMetadata("Address", "Address2")
                .ColumnName("Address.Street", "Street2")
                .AddMetadata("Address.Street", "AddressStreet1", "AddressStreet1_Person")
                .AddMetadata("Address.Street", "AddressStreet2", "AddressStreet2_Person")
                .RemoveMetadata("Address.Street", "AddressStreet2")
                .ForeignKey("DepartmentId", "DepartmentId2")
                .AddMetadata("DepartmentId", "DepartmentId1", "DepartmentId1_Person")
                .AddMetadata("DepartmentId", "DepartmentId2", "DepartmentId2_Person")
                .RemoveMetadata("DepartmentId", "DepartmentId2")
                .ForeignKey("Department.Id")
                .ColumnName("Department.Id", "DepartmentId2")
                .AddMetadata("Department.Id", "DepartmentId1", "DepartmentId1_Person")
                .AddMetadata("Department.Id", "DepartmentId2", "DepartmentId2_Person")
                .RemoveMetadata("Department.Id", "DepartmentId2");

            tableBuilder.Add<Department>()
                .TableName("prefix_Department")
                .AddTableMetadata("Key1", "Val1_Department")
                .AddTableMetadata("Key2", "Val2_Department")
                .RemoveTableMetadata("Key2")
                .ColumnName("Id", "Id3")
                .AddMetadata("Id", "Id1", "Id1_Department")
                .AddMetadata("Id", "Id2", "Id2_Department")
                .RemoveMetadata("Id", "Id2")
                .PrimaryKey("Guid")
                .ColumnName("Guid", "Guid3")
                .AddMetadata("Guid", "Guid1", "Guid1_Department")
                .AddMetadata("Guid", "Guid2", "Guid2_Department")
                .RemoveMetadata("Guid", "Guid2")
                .ColumnName("Name", "Name3")
                .AddMetadata("Name", "Name1", "Name1_Department")
                .AddMetadata("Name", "Name2", "Name2_Department")
                .RemoveMetadata("Name", "Name2")
                .ForeignKey("Boss.Id")
                .ColumnName("Boss.Id", "BossId3")
                .AddMetadata("Boss.Id", "BossId1", "BossId1_Department")
                .AddMetadata("Boss.Id", "BossId2", "BossId2_Department")
                .RemoveMetadata("Boss.Id", "BossId2");
        }

        [Fact]
        public void Table_Name()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal("prefix_Person", personInfo.TableName);
            Assert.Equal("prefix_Department", deptInfo.TableName);
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

            Assert.Equal(new string[] { "DepartmentId", "Department.Id" }, personInfo.ForeignKeys);
            Assert.Equal(new string[] { "Boss.Id" }, deptInfo.ForeignKeys);
        }

        [Fact]
        public void Columns()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { "Guid", "Id", "Name", "Address.Street", "Address.City", "DepartmentId",
                "Department.Id", "Image" }, personInfo.Columns);
            Assert.Equal(new string[] { "Guid", "Id", "Name", "Boss.Id" }, deptInfo.Columns);
        }

        [Fact]
        public void Column_Names_Dic()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new Dictionary<string, string>
            {
                ["Guid"] = "Guid2",
                ["Id"] = "Id2",
                ["Name"] = "Name2",
                ["Address.Street"] = "Street2",
                ["Address.City"] = "Address2City",
                ["DepartmentId"] = "DepartmentId2",
                ["Department.Id"] = "DepartmentId2",
                ["Image"] = "Image"
            }, personInfo.ColumnNamesDic);

            Assert.Equal(new Dictionary<string, string>
            {
                ["Guid"] = "Guid3",
                ["Id"] = "Id3",
                ["Name"] = "Name3",
                ["Boss.Id"] = "BossId3"
            }, deptInfo.ColumnNamesDic);
        }

        [Fact]
        public void Column_Names()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { "Guid2", "Id2", "Name2", "Street2", "Address2City", "DepartmentId2", "Image" },
                personInfo.ColumnNames);
            Assert.Equal(new string[] { "Guid3", "Id3", "Name3", "BossId3" }, deptInfo.ColumnNames);
        }

        [Fact]
        public void Not_Exists_Property()
        {
            TableBuilder tableBuilder = new TableBuilder();

            Exception ex = Assert.Throws<ArgumentException>(() =>
            {
                tableBuilder.Add<Person>()
                    .PrimaryKey("Other");
            });
            Assert.Equal($"The type \"{typeof(Person)}\" does not have property \"Other\".{Environment.NewLine}"
                + "Parameter name: propertyName", ex.Message);
        }

        [Fact]
        public void Table_Metadata()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new Dictionary<string, object>
            {
                ["Key1"] = "Val1_Person"
            }, personInfo.TableMetadata);

            Assert.Equal(new Dictionary<string, object>
            {
                ["Key1"] = "Val1_Department"
            }, deptInfo.TableMetadata);
        }

        [Fact]
        public void Member_Metadata()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new Dictionary<string, IDictionary<string, object>>
            {
                ["Id"] = new Dictionary<string, object>
                {
                    ["Id1"] = "Id1_Person",
                },
                ["Guid"] = new Dictionary<string, object>
                {
                    ["Guid1"] = "Guid1_Person",
                },
                ["Name"] = new Dictionary<string, object>
                {
                    ["Name1"] = "Name1_Person",
                },
                ["SurName"] = new Dictionary<string, object>
                {
                    ["SurName1"] = "SurName1_Person",
                },
                ["Address"] = new Dictionary<string, object>
                {
                    ["Address1"] = "Address1_Person",
                },
                ["Address.Street"] = new Dictionary<string, object>
                {
                    ["AddressStreet1"] = "AddressStreet1_Person",
                },
                ["DepartmentId"] = new Dictionary<string, object>
                {
                    ["DepartmentId1"] = "DepartmentId1_Person",
                },
                ["Department.Id"] = new Dictionary<string, object>
                {
                    ["DepartmentId1"] = "DepartmentId1_Person",
                }
            }, personInfo.MemberMetadata);

            Assert.Equal(new Dictionary<string, IDictionary<string, object>>
            {
                ["Id"] = new Dictionary<string, object>
                {
                    ["Id1"] = "Id1_Department",
                },
                ["Guid"] = new Dictionary<string, object>
                {
                    ["Guid1"] = "Guid1_Department",
                },
                ["Name"] = new Dictionary<string, object>
                {
                    ["Name1"] = "Name1_Department",
                },
                ["Boss.Id"] = new Dictionary<string, object>
                {
                    ["BossId1"] = "BossId1_Department",
                }
            }, deptInfo.MemberMetadata);
        }
    }
}