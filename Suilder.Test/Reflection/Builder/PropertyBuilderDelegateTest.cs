using System.Collections.Generic;
using Suilder.Reflection.Builder;
using Suilder.Reflection.Builder.Processors;
using Suilder.Test.Reflection.Builder.NoInherit.Tables;
using Xunit;

namespace Suilder.Test.Reflection.Builder
{
    public class PropertyBuilderDelegateTest : BaseTest
    {
        protected override void InitConfig()
        {
            tableBuilder.AddProcessor(new DefaultMetadataProcessor());

            tableBuilder.Add<Person>()
                .Property(x => x.Id, p => p
                    .ColumnName("Id2")
                    .AddMetadata("Id1", "Id1_Person")
                    .AddMetadata("Id2", "Id2_Person")
                    .RemoveMetadata("Id2"))
                .Property(x => x.Guid, p => p
                    .PrimaryKey()
                    .ColumnName("Guid2")
                    .AddMetadata("Guid1", "Guid1_Person")
                    .AddMetadata("Guid2", "Guid2_Person")
                    .RemoveMetadata("Guid2"))
                .Property(x => x.Name, p => p
                    .ColumnName("Name2")
                    .AddMetadata("Name1", "Name1_Person")
                    .AddMetadata("Name2", "Name2_Person")
                    .RemoveMetadata("Name2"))
                .Property(x => x.SurName, p => p
                    .Ignore()
                    .AddMetadata("SurName1", "SurName1_Person")
                    .AddMetadata("SurName2", "SurName2_Person")
                    .RemoveMetadata("SurName2"))
                .Property(x => x.Address, p => p
                    .ColumnName("Address2")
                    .AddMetadata("Address1", "Address1_Person")
                    .AddMetadata("Address2", "Address2_Person")
                    .RemoveMetadata("Address2"))
                .Property(x => x.Address.Street, p => p
                    .ColumnName("Street2")
                    .AddMetadata("AddressStreet1", "AddressStreet1_Person")
                    .AddMetadata("AddressStreet2", "AddressStreet2_Person")
                    .RemoveMetadata("AddressStreet2"))
                .Property(x => x.DepartmentId, p => p
                    .ForeignKey("DepartmentId2")
                    .AddMetadata("DepartmentId1", "DepartmentId1_Person")
                    .AddMetadata("DepartmentId2", "DepartmentId2_Person")
                    .RemoveMetadata("DepartmentId2"))
                .Property(x => x.Department.Id, p => p
                    .ForeignKey()
                    .ColumnName("DepartmentId2")
                    .AddMetadata("DepartmentId1", "DepartmentId1_Person")
                    .AddMetadata("DepartmentId2", "DepartmentId2_Person")
                    .RemoveMetadata("DepartmentId2"));

            tableBuilder.Add<Department>()
                .Property(x => x.Id, p => p
                    .ColumnName("Id3")
                    .AddMetadata("Id1", "Id1_Department")
                    .AddMetadata("Id2", "Id2_Department")
                    .RemoveMetadata("Id2"))
                .Property(x => x.Guid, p => p
                    .PrimaryKey()
                    .ColumnName("Guid3")
                    .AddMetadata("Guid1", "Guid1_Department")
                    .AddMetadata("Guid2", "Guid2_Department")
                    .RemoveMetadata("Guid2"))
                .Property(x => x.Name, p => p
                    .ColumnName("Name3")
                    .AddMetadata("Name1", "Name1_Department")
                     .AddMetadata("Name2", "Name2_Department")
                     .RemoveMetadata("Name2"))
                .Property(x => x.Boss.Id, p => p
                    .ForeignKey()
                    .ColumnName("BossId3")
                    .AddMetadata("BossId1", "BossId1_Department")
                    .AddMetadata("BossId2", "BossId2_Department")
                    .RemoveMetadata("BossId2"));
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
                ["Boss.Id"] = "BossId3",
                ["Tags"] = "Tags"
            }, deptInfo.ColumnNamesDic);
        }

        [Fact]
        public void Column_Names()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { "Guid2", "Id2", "Name2", "Street2", "Address2City", "DepartmentId2", "Image" },
                personInfo.ColumnNames);
            Assert.Equal(new string[] { "Guid3", "Id3", "Name3", "BossId3", "Tags" }, deptInfo.ColumnNames);
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