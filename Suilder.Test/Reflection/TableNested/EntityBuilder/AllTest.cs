using System.Collections.Generic;
using Suilder.Reflection.Builder;
using Suilder.Reflection.Builder.Processors;
using Suilder.Test.Reflection.TableNested.Tables;
using Xunit;

namespace Suilder.Test.Reflection.TableNested.EntityBuilder
{
    public class AllTest : BaseTest
    {
        protected override void InitConfig()
        {
            tableBuilder.AddProcessor(new DefaultMetadataProcessor()
                .InheritAll(true));

            tableBuilder.Add<BaseConfig>()
                .AddTableMetadata("Key1", "Val1_Base")
                .AddTableMetadata("Key2", "Val2_Base")
                .AddTableMetadata("Key3", "Val3_Base")
                .RemoveTableMetadata("Key2")
                .AddMetadata(x => x.Id, "Id1", "Id1_Base")
                .AddMetadata(x => x.Id, "Id2", "Id2_Base")
                .AddMetadata(x => x.Id, "Id3", "Id3_Base")
                .RemoveMetadata(x => x.Id, "Id2")
                .AddMetadata(x => x.Guid, "Guid1", "Guid1_Base")
                .AddMetadata(x => x.Guid, "Guid2", "Guid2_Base")
                .AddMetadata(x => x.Guid, "Guid3", "Guid3_Base")
                .RemoveMetadata(x => x.Guid, "Guid2")
                .AddMetadata(x => x.Name, "Name1", "Name1_Base")
                .AddMetadata(x => x.Name, "Name2", "Name2_Base")
                .AddMetadata(x => x.Name, "Name3", "Name3_Base")
                .RemoveMetadata(x => x.Name, "Name2");

            tableBuilder.Add<Person>()
                .Schema("schema_Person")
                .TableName("prefix_Person")
                .AddTableMetadata("Key3", "Val3_Person")
                .AddTableMetadata("Key4", "Val4_Person")
                .AddTableMetadata("Key5", "Val5_Person")
                .RemoveTableMetadata("Key4")
                .ColumnName(x => x.Id, "Id2")
                .AddMetadata(x => x.Id, "Id3", "Id3_Person")
                .AddMetadata(x => x.Id, "Id4", "Id4_Person")
                .AddMetadata(x => x.Id, "Id5", "Id5_Person")
                .RemoveMetadata(x => x.Id, "Id4")
                .PrimaryKey(x => x.Guid)
                .ColumnName(x => x.Guid, "Guid2")
                .AddMetadata(x => x.Guid, "Guid3", "Guid3_Person")
                .AddMetadata(x => x.Guid, "Guid4", "Guid4_Person")
                .AddMetadata(x => x.Guid, "Guid5", "Guid5_Person")
                .RemoveMetadata(x => x.Guid, "Guid4")
                .ColumnName(x => x.Name, "Name2")
                .AddMetadata(x => x.Name, "Name3", "Name3_Person")
                .AddMetadata(x => x.Name, "Name4", "Name4_Person")
                .AddMetadata(x => x.Name, "Name5", "Name5_Person")
                .RemoveMetadata(x => x.Name, "Name4")
                .Ignore(x => x.Surname)
                .AddMetadata(x => x.Surname, "Surname1", "Surname1_Person")
                .AddMetadata(x => x.Surname, "Surname2", "Surname2_Person")
                .AddMetadata(x => x.Surname, "Surname3", "Surname3_Person")
                .RemoveMetadata(x => x.Surname, "Surname2")
                .ColumnName(x => x.Employee, "Employee2")
                .AddMetadata(x => x.Employee, "Employee1", "Employee1_Person")
                .AddMetadata(x => x.Employee, "Employee2", "Employee2_Person")
                .AddMetadata(x => x.Employee, "Employee3", "Employee3_Person")
                .RemoveMetadata(x => x.Employee, "Employee2")
                .ColumnName(x => x.Employee.Address, "Address2", true)
                .AddMetadata(x => x.Employee.Address, "Address1", "Address1_Person")
                .AddMetadata(x => x.Employee.Address, "Address2", "Address2_Person")
                .AddMetadata(x => x.Employee.Address, "Address3", "Address3_Person")
                .RemoveMetadata(x => x.Employee.Address, "Address2")
                .ColumnName(x => x.Employee.Address.Street, "Street2")
                .AddMetadata(x => x.Employee.Address.Street, "AddressStreet1", "AddressStreet1_Person")
                .AddMetadata(x => x.Employee.Address.Street, "AddressStreet2", "AddressStreet2_Person")
                .AddMetadata(x => x.Employee.Address.Street, "AddressStreet3", "AddressStreet3_Person")
                .RemoveMetadata(x => x.Employee.Address.Street, "AddressStreet2")
                .ColumnName(x => x.Employee.Address.City, "City2", true)
                .AddMetadata(x => x.Employee.Address.City, "AddressCity1", "AddressCity1_Person")
                .AddMetadata(x => x.Employee.Address.City, "AddressCity2", "AddressCity2_Person")
                .AddMetadata(x => x.Employee.Address.City, "AddressCity3", "AddressCity3_Person")
                .RemoveMetadata(x => x.Employee.Address.City, "AddressCity2")
                .ForeignKey(x => x.Employee.DepartmentId, "DepartmentId2", true)
                .AddMetadata(x => x.Employee.DepartmentId, "DepartmentId1", "DepartmentId1_Person")
                .AddMetadata(x => x.Employee.DepartmentId, "DepartmentId2", "DepartmentId2_Person")
                .AddMetadata(x => x.Employee.DepartmentId, "DepartmentId3", "DepartmentId3_Person")
                .RemoveMetadata(x => x.Employee.DepartmentId, "DepartmentId2")
                .ForeignKey(x => x.Employee.Department.Id)
                .ColumnName(x => x.Employee.Department.Id, "DepartmentId2", true)
                .AddMetadata(x => x.Employee.Department.Id, "DepartmentId1", "DepartmentId1_Person")
                .AddMetadata(x => x.Employee.Department.Id, "DepartmentId2", "DepartmentId2_Person")
                .AddMetadata(x => x.Employee.Department.Id, "DepartmentId3", "DepartmentId3_Person")
                .RemoveMetadata(x => x.Employee.Department.Id, "DepartmentId2");

            tableBuilder.AddNested<Employee>();

            tableBuilder.Add<Department>()
                .Schema("schema_Department")
                .TableName("prefix_Department")
                .AddTableMetadata("Key3", "Val3_Department")
                .AddTableMetadata("Key4", "Val4_Department")
                .AddTableMetadata("Key5", "Val5_Department")
                .RemoveTableMetadata("Key4")
                .ColumnName(x => x.Id, "Id3")
                .AddMetadata(x => x.Id, "Id3", "Id3_Department")
                .AddMetadata(x => x.Id, "Id4", "Id4_Department")
                .AddMetadata(x => x.Id, "Id5", "Id5_Department")
                .RemoveMetadata(x => x.Id, "Id4")
                .PrimaryKey(x => x.Guid)
                .ColumnName(x => x.Guid, "Guid3")
                .AddMetadata(x => x.Guid, "Guid3", "Guid3_Department")
                .AddMetadata(x => x.Guid, "Guid4", "Guid4_Department")
                .AddMetadata(x => x.Guid, "Guid5", "Guid5_Department")
                .RemoveMetadata(x => x.Guid, "Guid4")
                .ColumnName(x => x.Name, "Name3")
                .AddMetadata(x => x.Name, "Name3", "Name3_Department")
                .AddMetadata(x => x.Name, "Name4", "Name4_Department")
                .AddMetadata(x => x.Name, "Name5", "Name5_Department")
                .RemoveMetadata(x => x.Name, "Name4")
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

            Assert.Equal(new string[] { "Employee.DepartmentId", "Employee.Department.Id" }, personInfo.ForeignKeys);
            Assert.Equal(new string[] { "Boss.Id" }, deptInfo.ForeignKeys);
        }

        [Fact]
        public void Columns()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { "Guid", "Id", "Name", "Employee.Address.Street", "Employee.Address.City",
                "Employee.Salary", "Employee.DepartmentId", "Employee.Department.Id", "Employee.Image" },
                personInfo.Columns);
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
                ["Employee.Address.Street"] = "Street2",
                ["Employee.Address.City"] = "Employee2Address2City2",
                ["Employee.Salary"] = "Employee2Salary",
                ["Employee.DepartmentId"] = "Employee2DepartmentId2",
                ["Employee.Department.Id"] = "Employee2DepartmentId2",
                ["Employee.Image"] = "Employee2Image"
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

            Assert.Equal(new string[] { "Guid2", "Id2", "Name2", "Street2", "Employee2Address2City2", "Employee2Salary",
                "Employee2DepartmentId2", "Employee2Image" }, personInfo.ColumnNames);
            Assert.Equal(new string[] { "Guid3", "Id3", "Name3", "BossId3", "Tags" }, deptInfo.ColumnNames);
        }

        [Fact]
        public void Table_Metadata()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new Dictionary<string, object>
            {
                ["Key1"] = "Val1_Base",
                ["Key3"] = "Val3_Person",
                ["Key5"] = "Val5_Person"
            }, personInfo.TableMetadata);

            Assert.Equal(new Dictionary<string, object>
            {
                ["Key1"] = "Val1_Base",
                ["Key3"] = "Val3_Department",
                ["Key5"] = "Val5_Department"
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
                    ["Id1"] = "Id1_Base",
                    ["Id3"] = "Id3_Person",
                    ["Id5"] = "Id5_Person"
                },
                ["Guid"] = new Dictionary<string, object>
                {
                    ["Guid1"] = "Guid1_Base",
                    ["Guid3"] = "Guid3_Person",
                    ["Guid5"] = "Guid5_Person"
                },
                ["Name"] = new Dictionary<string, object>
                {
                    ["Name1"] = "Name1_Base",
                    ["Name3"] = "Name3_Person",
                    ["Name5"] = "Name5_Person"
                },
                ["Surname"] = new Dictionary<string, object>
                {
                    ["Surname1"] = "Surname1_Person",
                    ["Surname3"] = "Surname3_Person"
                },
                ["Employee"] = new Dictionary<string, object>
                {
                    ["Employee1"] = "Employee1_Person",
                    ["Employee3"] = "Employee3_Person"
                },
                ["Employee.Address"] = new Dictionary<string, object>
                {
                    ["Address1"] = "Address1_Person",
                    ["Address3"] = "Address3_Person"
                },
                ["Employee.Address.Street"] = new Dictionary<string, object>
                {
                    ["AddressStreet1"] = "AddressStreet1_Person",
                    ["AddressStreet3"] = "AddressStreet3_Person"
                },
                ["Employee.Address.City"] = new Dictionary<string, object>
                {
                    ["AddressCity1"] = "AddressCity1_Person",
                    ["AddressCity3"] = "AddressCity3_Person"
                },
                ["Employee.DepartmentId"] = new Dictionary<string, object>
                {
                    ["DepartmentId1"] = "DepartmentId1_Person",
                    ["DepartmentId3"] = "DepartmentId3_Person"
                },
                ["Employee.Department.Id"] = new Dictionary<string, object>
                {
                    ["DepartmentId1"] = "DepartmentId1_Person",
                    ["DepartmentId3"] = "DepartmentId3_Person"
                }
            }, personInfo.MemberMetadata);

            Assert.Equal(new Dictionary<string, IDictionary<string, object>>
            {
                ["Id"] = new Dictionary<string, object>
                {
                    ["Id1"] = "Id1_Base",
                    ["Id3"] = "Id3_Department",
                    ["Id5"] = "Id5_Department"
                },
                ["Guid"] = new Dictionary<string, object>
                {
                    ["Guid1"] = "Guid1_Base",
                    ["Guid3"] = "Guid3_Department",
                    ["Guid5"] = "Guid5_Department"
                },
                ["Name"] = new Dictionary<string, object>
                {
                    ["Name1"] = "Name1_Base",
                    ["Name3"] = "Name3_Department",
                    ["Name5"] = "Name5_Department"
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