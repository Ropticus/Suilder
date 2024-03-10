using System.Collections.Generic;
using Suilder.Reflection.Builder;
using Suilder.Reflection.Builder.Processors;
using Suilder.Test.Reflection.NoInherit.Tables;
using Xunit;

namespace Suilder.Test.Reflection.NoInherit.EntityBuilder
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
                .ColumnName(x => x.Id, "Id2")
                .AddMetadata(x => x.Id, "Id1", "Id1_Person")
                .AddMetadata(x => x.Id, "Id2", "Id2_Person")
                .AddMetadata(x => x.Id, "Id3", "Id3_Person")
                .RemoveMetadata(x => x.Id, "Id2")
                .PrimaryKey(x => x.Guid)
                .ColumnName(x => x.Guid, "Guid2")
                .AddMetadata(x => x.Guid, "Guid1", "Guid1_Person")
                .AddMetadata(x => x.Guid, "Guid2", "Guid2_Person")
                .AddMetadata(x => x.Guid, "Guid3", "Guid3_Person")
                .RemoveMetadata(x => x.Guid, "Guid2")
                .ColumnName(x => x.Name, "Name2")
                .AddMetadata(x => x.Name, "Name1", "Name1_Person")
                .AddMetadata(x => x.Name, "Name2", "Name2_Person")
                .AddMetadata(x => x.Name, "Name3", "Name3_Person")
                .RemoveMetadata(x => x.Name, "Name2")
                .Ignore(x => x.Surname)
                .AddMetadata(x => x.Surname, "Surname1", "Surname1_Person")
                .AddMetadata(x => x.Surname, "Surname2", "Surname2_Person")
                .AddMetadata(x => x.Surname, "Surname3", "Surname3_Person")
                .RemoveMetadata(x => x.Surname, "Surname2")
                .ColumnName(x => x.Address, "Address2")
                .AddMetadata(x => x.Address, "Address1", "Address1_Person")
                .AddMetadata(x => x.Address, "Address2", "Address2_Person")
                .AddMetadata(x => x.Address, "Address3", "Address3_Person")
                .RemoveMetadata(x => x.Address, "Address2")
                .ColumnName(x => x.Address.Street, "Street2")
                .AddMetadata(x => x.Address.Street, "AddressStreet1", "AddressStreet1_Person")
                .AddMetadata(x => x.Address.Street, "AddressStreet2", "AddressStreet2_Person")
                .AddMetadata(x => x.Address.Street, "AddressStreet3", "AddressStreet3_Person")
                .RemoveMetadata(x => x.Address.Street, "AddressStreet2")
                .ColumnName(x => x.Address.City, "City2", true)
                .AddMetadata(x => x.Address.City, "AddressCity1", "AddressCity1_Person")
                .AddMetadata(x => x.Address.City, "AddressCity2", "AddressCity2_Person")
                .AddMetadata(x => x.Address.City, "AddressCity3", "AddressCity3_Person")
                .RemoveMetadata(x => x.Address.City, "AddressCity2")
                .ForeignKey(x => x.DepartmentId, "DepartmentId2")
                .AddMetadata(x => x.DepartmentId, "DepartmentId1", "DepartmentId1_Person")
                .AddMetadata(x => x.DepartmentId, "DepartmentId2", "DepartmentId2_Person")
                .AddMetadata(x => x.DepartmentId, "DepartmentId3", "DepartmentId3_Person")
                .RemoveMetadata(x => x.DepartmentId, "DepartmentId2")
                .ForeignKey(x => x.Department.Id)
                .ColumnName(x => x.Department.Id, "DepartmentId2")
                .AddMetadata(x => x.Department.Id, "DepartmentId1", "DepartmentId1_Person")
                .AddMetadata(x => x.Department.Id, "DepartmentId2", "DepartmentId2_Person")
                .AddMetadata(x => x.Department.Id, "DepartmentId3", "DepartmentId3_Person")
                .RemoveMetadata(x => x.Department.Id, "DepartmentId2");

            tableBuilder.Add<Department>()
                .Schema("schema_Department")
                .TableName("prefix_Department")
                .AddTableMetadata("Key1", "Val1_Department")
                .AddTableMetadata("Key2", "Val2_Department")
                .AddTableMetadata("Key3", "Val3_Department")
                .RemoveTableMetadata("Key2")
                .ColumnName(x => x.Id, "Id3")
                .AddMetadata(x => x.Id, "Id1", "Id1_Department")
                .AddMetadata(x => x.Id, "Id2", "Id2_Department")
                .AddMetadata(x => x.Id, "Id3", "Id3_Department")
                .RemoveMetadata(x => x.Id, "Id2")
                .PrimaryKey(x => x.Guid)
                .ColumnName(x => x.Guid, "Guid3")
                .AddMetadata(x => x.Guid, "Guid1", "Guid1_Department")
                .AddMetadata(x => x.Guid, "Guid2", "Guid2_Department")
                .AddMetadata(x => x.Guid, "Guid3", "Guid3_Department")
                .RemoveMetadata(x => x.Guid, "Guid2")
                .ColumnName(x => x.Name, "Name3")
                .AddMetadata(x => x.Name, "Name1", "Name1_Department")
                .AddMetadata(x => x.Name, "Name2", "Name2_Department")
                .AddMetadata(x => x.Name, "Name3", "Name3_Department")
                .RemoveMetadata(x => x.Name, "Name2")
                .ForeignKey(x => x.Boss.Id)
                .ColumnName(x => x.Boss.Id, "BossId3")
                .AddMetadata(x => x.Boss.Id, "BossId1", "BossId1_Department")
                .AddMetadata(x => x.Boss.Id, "BossId2", "BossId2_Department")
                .AddMetadata(x => x.Boss.Id, "BossId3", "BossId3_Department")
                .RemoveMetadata(x => x.Boss.Id, "BossId2");
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