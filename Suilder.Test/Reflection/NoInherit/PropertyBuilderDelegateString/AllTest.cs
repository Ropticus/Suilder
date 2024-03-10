using System.Collections.Generic;
using Suilder.Reflection.Builder;
using Suilder.Reflection.Builder.Processors;
using Suilder.Test.Reflection.NoInherit.Tables;
using Xunit;

namespace Suilder.Test.Reflection.NoInherit.PropertyBuilderDelegateString
{
    public class AllTest : BaseTest
    {
        protected override void InitConfig()
        {
            tableBuilder.AddProcessor(new DefaultMetadataProcessor()
                .InheritAll(true));

            tableBuilder.Add<Person>()
                .Schema("schema_Person")
                .TableName("prefix_Person")
                .AddTableMetadata("Key1", "Val1_Person")
                .AddTableMetadata("Key2", "Val2_Person")
                .AddTableMetadata("Key3", "Val3_Person")
                .RemoveTableMetadata("Key2")
                .Property("Id", p => p
                    .ColumnName("Id2")
                    .AddMetadata("Id1", "Id1_Person")
                    .AddMetadata("Id2", "Id2_Person")
                    .AddMetadata("Id3", "Id3_Person")
                    .RemoveMetadata("Id2"))
                .Property("Guid", p => p
                    .PrimaryKey()
                    .ColumnName("Guid2")
                    .AddMetadata("Guid1", "Guid1_Person")
                    .AddMetadata("Guid2", "Guid2_Person")
                    .AddMetadata("Guid3", "Guid3_Person")
                    .RemoveMetadata("Guid2"))
                .Property("Name", p => p
                    .ColumnName("Name2")
                    .AddMetadata("Name1", "Name1_Person")
                    .AddMetadata("Name2", "Name2_Person")
                    .AddMetadata("Name3", "Name3_Person")
                    .RemoveMetadata("Name2"))
                .Property("Surname", p => p
                    .Ignore()
                    .AddMetadata("Surname1", "Surname1_Person")
                    .AddMetadata("Surname2", "Surname2_Person")
                    .AddMetadata("Surname3", "Surname3_Person")
                    .RemoveMetadata("Surname2"))
                .Property("Address", p => p
                    .ColumnName("Address2")
                    .AddMetadata("Address1", "Address1_Person")
                    .AddMetadata("Address2", "Address2_Person")
                    .AddMetadata("Address3", "Address3_Person")
                    .RemoveMetadata("Address2"))
                .Property("Address.Street", p => p
                    .ColumnName("Street2")
                    .AddMetadata("AddressStreet1", "AddressStreet1_Person")
                    .AddMetadata("AddressStreet2", "AddressStreet2_Person")
                    .AddMetadata("AddressStreet3", "AddressStreet3_Person")
                    .RemoveMetadata("AddressStreet2"))
                .Property("Address.City", p => p
                    .ColumnName("City2", true)
                    .AddMetadata("AddressCity1", "AddressCity1_Person")
                    .AddMetadata("AddressCity2", "AddressCity2_Person")
                    .AddMetadata("AddressCity3", "AddressCity3_Person")
                    .RemoveMetadata("AddressCity2"))
                .Property("DepartmentId", p => p
                    .ForeignKey("DepartmentId2")
                    .AddMetadata("DepartmentId1", "DepartmentId1_Person")
                    .AddMetadata("DepartmentId2", "DepartmentId2_Person")
                    .AddMetadata("DepartmentId3", "DepartmentId3_Person")
                    .RemoveMetadata("DepartmentId2"))
                .Property("Department.Id", p => p
                    .ForeignKey()
                    .ColumnName("DepartmentId2")
                    .AddMetadata("DepartmentId1", "DepartmentId1_Person")
                    .AddMetadata("DepartmentId2", "DepartmentId2_Person")
                    .AddMetadata("DepartmentId3", "DepartmentId3_Person")
                    .RemoveMetadata("DepartmentId2"));

            tableBuilder.Add<Department>()
                .Schema("schema_Department")
                .TableName("prefix_Department")
                .AddTableMetadata("Key1", "Val1_Department")
                .AddTableMetadata("Key2", "Val2_Department")
                .AddTableMetadata("Key3", "Val3_Department")
                .RemoveTableMetadata("Key2")
                .Property("Id", p => p
                    .ColumnName("Id3")
                    .AddMetadata("Id1", "Id1_Department")
                    .AddMetadata("Id2", "Id2_Department")
                    .AddMetadata("Id3", "Id3_Department")
                    .RemoveMetadata("Id2"))
                .Property("Guid", p => p
                    .PrimaryKey()
                    .ColumnName("Guid3")
                    .AddMetadata("Guid1", "Guid1_Department")
                    .AddMetadata("Guid2", "Guid2_Department")
                    .AddMetadata("Guid3", "Guid3_Department")
                    .RemoveMetadata("Guid2"))
                .Property("Name", p => p
                    .ColumnName("Name3")
                    .AddMetadata("Name1", "Name1_Department")
                    .AddMetadata("Name2", "Name2_Department")
                    .AddMetadata("Name3", "Name3_Department")
                    .RemoveMetadata("Name2"))
                .Property("Boss.Id", p => p
                    .ForeignKey()
                    .ColumnName("BossId3")
                    .AddMetadata("BossId1", "BossId1_Department")
                    .AddMetadata("BossId2", "BossId2_Department")
                    .AddMetadata("BossId3", "BossId3_Department")
                    .RemoveMetadata("BossId2"));
        }

        [Fact]
        public void Schema_Name()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal("schema_Person", personInfo.Schema);
            Assert.Equal("schema_Department", deptInfo.Schema);
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
            Assert.Equal(new string[] { "Guid", "Id", "Name", "Boss.Id", "Tags" }, deptInfo.Columns);
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
                ["Address.City"] = "Address2City2",
                ["DepartmentId"] = "DepartmentId2",
                ["Department.Id"] = "DepartmentId2",
                ["Image"] = "Image"
            }, personInfo.ColumnNamesDic);

            Assert.Equal(new Dictionary<string, string>
            {
                ["Guid"] = "Guid3",
                ["Id"] = "Id3",
                ["Name"] = "Name3",
                ["Boss.Id"] = "BossId3",
                ["Tags"] = "Tags"
            }, deptInfo.ColumnNamesDic);
        }

        [Fact]
        public void Column_Names()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { "Guid2", "Id2", "Name2", "Street2", "Address2City2", "DepartmentId2", "Image" },
                personInfo.ColumnNames);
            Assert.Equal(new string[] { "Guid3", "Id3", "Name3", "BossId3", "Tags" }, deptInfo.ColumnNames);
        }

        [Fact]
        public void Table_Metadata()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new Dictionary<string, object>
            {
                ["Key1"] = "Val1_Person",
                ["Key3"] = "Val3_Person"
            }, personInfo.TableMetadata);

            Assert.Equal(new Dictionary<string, object>
            {
                ["Key1"] = "Val1_Department",
                ["Key3"] = "Val3_Department"
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
                    ["Id3"] = "Id3_Person"
                },
                ["Guid"] = new Dictionary<string, object>
                {
                    ["Guid1"] = "Guid1_Person",
                    ["Guid3"] = "Guid3_Person"
                },
                ["Name"] = new Dictionary<string, object>
                {
                    ["Name1"] = "Name1_Person",
                    ["Name3"] = "Name3_Person"
                },
                ["Surname"] = new Dictionary<string, object>
                {
                    ["Surname1"] = "Surname1_Person",
                    ["Surname3"] = "Surname3_Person"
                },
                ["Address"] = new Dictionary<string, object>
                {
                    ["Address1"] = "Address1_Person",
                    ["Address3"] = "Address3_Person"
                },
                ["Address.Street"] = new Dictionary<string, object>
                {
                    ["AddressStreet1"] = "AddressStreet1_Person",
                    ["AddressStreet3"] = "AddressStreet3_Person"
                },
                ["Address.City"] = new Dictionary<string, object>
                {
                    ["AddressCity1"] = "AddressCity1_Person",
                    ["AddressCity3"] = "AddressCity3_Person"
                },
                ["DepartmentId"] = new Dictionary<string, object>
                {
                    ["DepartmentId1"] = "DepartmentId1_Person",
                    ["DepartmentId3"] = "DepartmentId3_Person"
                },
                ["Department.Id"] = new Dictionary<string, object>
                {
                    ["DepartmentId1"] = "DepartmentId1_Person",
                    ["DepartmentId3"] = "DepartmentId3_Person"
                }
            }, personInfo.MemberMetadata);

            Assert.Equal(new Dictionary<string, IDictionary<string, object>>
            {
                ["Id"] = new Dictionary<string, object>
                {
                    ["Id1"] = "Id1_Department",
                    ["Id3"] = "Id3_Department"
                },
                ["Guid"] = new Dictionary<string, object>
                {
                    ["Guid1"] = "Guid1_Department",
                    ["Guid3"] = "Guid3_Department"
                },
                ["Name"] = new Dictionary<string, object>
                {
                    ["Name1"] = "Name1_Department",
                    ["Name3"] = "Name3_Department"
                },
                ["Boss.Id"] = new Dictionary<string, object>
                {
                    ["BossId1"] = "BossId1_Department",
                    ["BossId3"] = "BossId3_Department"
                }
            }, deptInfo.MemberMetadata);
        }
    }
}