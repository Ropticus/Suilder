using System.Collections.Generic;
using Suilder.Reflection.Builder;
using Suilder.Reflection.Builder.Processors;
using Suilder.Test.Reflection.TablePerType.Tables;
using Xunit;

namespace Suilder.Test.Reflection.TablePerType.EntityBuilder
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
                .ColumnName(x => x.Address, "Address2", true)
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
                .RemoveMetadata(x => x.Address.City, "AddressCity2");

            tableBuilder.Add<Employee>()
                .Schema("schema_Employee")
                .TableName("prefix_Employee")
                .AddTableMetadata("Key5", "Val5_Employee")
                .AddTableMetadata("Key6", "Val6_Employee")
                .AddTableMetadata("Key7", "Val7_Employee")
                .RemoveTableMetadata("Key6")
                .AddMetadata(x => x.Id, "Id5", "Id5_Employee")
                .AddMetadata(x => x.Id, "Id6", "Id6_Employee")
                .AddMetadata(x => x.Id, "Id7", "Id7_Employee")
                .RemoveMetadata(x => x.Id, "Id6")
                .AddMetadata(x => x.Guid, "Guid5", "Guid5_Employee")
                .AddMetadata(x => x.Guid, "Guid6", "Guid6_Employee")
                .AddMetadata(x => x.Guid, "Guid7", "Guid7_Employee")
                .RemoveMetadata(x => x.Guid, "Guid6")
                .AddMetadata(x => x.Name, "Name5", "Name5_Employee")
                .AddMetadata(x => x.Name, "Name6", "Name6_Employee")
                .AddMetadata(x => x.Name, "Name7", "Name7_Employee")
                .RemoveMetadata(x => x.Name, "Name6")
                .AddMetadata(x => x.Surname, "Surname3", "Surname3_Employee")
                .AddMetadata(x => x.Surname, "Surname4", "Surname4_Employee")
                .AddMetadata(x => x.Surname, "Surname5", "Surname5_Employee")
                .RemoveMetadata(x => x.Surname, "Surname4")
                .AddMetadata(x => x.Address, "Address3", "Address3_Employee")
                .AddMetadata(x => x.Address, "Address4", "Address4_Employee")
                .AddMetadata(x => x.Address, "Address5", "Address5_Employee")
                .RemoveMetadata(x => x.Address, "Address4")
                .AddMetadata(x => x.Address.Street, "AddressStreet3", "AddressStreet3_Employee")
                .AddMetadata(x => x.Address.Street, "AddressStreet4", "AddressStreet4_Employee")
                .AddMetadata(x => x.Address.Street, "AddressStreet5", "AddressStreet5_Employee")
                .RemoveMetadata(x => x.Address.Street, "AddressStreet4")
                .AddMetadata(x => x.Address.City, "AddressCity3", "AddressCity3_Employee")
                .AddMetadata(x => x.Address.City, "AddressCity4", "AddressCity4_Employee")
                .AddMetadata(x => x.Address.City, "AddressCity5", "AddressCity5_Employee")
                .RemoveMetadata(x => x.Address.City, "AddressCity4")
                .ForeignKey(x => x.DepartmentId, "DepartmentId2")
                .AddMetadata(x => x.DepartmentId, "DepartmentId1", "DepartmentId1_Employee")
                .AddMetadata(x => x.DepartmentId, "DepartmentId2", "DepartmentId2_Employee")
                .AddMetadata(x => x.DepartmentId, "DepartmentId3", "DepartmentId3_Employee")
                .RemoveMetadata(x => x.DepartmentId, "DepartmentId2")
                .ForeignKey(x => x.Department.Id)
                .ColumnName(x => x.Department.Id, "DepartmentId2")
                .AddMetadata(x => x.Department.Id, "DepartmentId1", "DepartmentId1_Employee")
                .AddMetadata(x => x.Department.Id, "DepartmentId2", "DepartmentId2_Employee")
                .AddMetadata(x => x.Department.Id, "DepartmentId3", "DepartmentId3_Employee")
                .RemoveMetadata(x => x.Department.Id, "DepartmentId2");

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
            ITableInfo employeeInfo = tableBuilder.GetConfig<Employee>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal("schema_Person", personInfo.Schema);
            Assert.Equal("schema_Employee", employeeInfo.Schema);
            Assert.Equal("schema_Department", deptInfo.Schema);
        }

        [Fact]
        public void Table_Name()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo employeeInfo = tableBuilder.GetConfig<Employee>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal("prefix_Person", personInfo.TableName);
            Assert.Equal("prefix_Employee", employeeInfo.TableName);
            Assert.Equal("prefix_Department", deptInfo.TableName);
        }

        [Fact]
        public void Primary_Keys()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo employeeInfo = tableBuilder.GetConfig<Employee>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { "Guid" }, personInfo.PrimaryKeys);
            Assert.Equal(new string[] { "Guid" }, employeeInfo.PrimaryKeys);
            Assert.Equal(new string[] { "Guid" }, deptInfo.PrimaryKeys);
        }

        [Fact]
        public void Foreign_Keys()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo employeeInfo = tableBuilder.GetConfig<Employee>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { }, personInfo.ForeignKeys);
            Assert.Equal(new string[] { "DepartmentId", "Department.Id" }, employeeInfo.ForeignKeys);
            Assert.Equal(new string[] { "Boss.Id" }, deptInfo.ForeignKeys);
        }

        [Fact]
        public void Columns()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo employeeInfo = tableBuilder.GetConfig<Employee>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { "Guid", "Id", "Name", "Address.Street", "Address.City" }, personInfo.Columns);
            Assert.Equal(new string[] { "Guid", "Salary", "DepartmentId", "Department.Id", "Image" }, employeeInfo.Columns);
            Assert.Equal(new string[] { "Guid", "Id", "Name", "Boss.Id", "Tags" }, deptInfo.Columns);
        }

        [Fact]
        public void Column_Names_Dic()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo employeeInfo = tableBuilder.GetConfig<Employee>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new Dictionary<string, string>
            {
                ["Guid"] = "Guid2",
                ["Id"] = "Id2",
                ["Name"] = "Name2",
                ["Address.Street"] = "Street2",
                ["Address.City"] = "Address2City2"
            }, personInfo.ColumnNamesDic);

            Assert.Equal(new Dictionary<string, string>
            {
                ["Guid"] = "Guid2",
                ["Salary"] = "Salary",
                ["DepartmentId"] = "DepartmentId2",
                ["Department.Id"] = "DepartmentId2",
                ["Image"] = "Image"
            }, employeeInfo.ColumnNamesDic);

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
            ITableInfo employeeInfo = tableBuilder.GetConfig<Employee>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { "Guid2", "Id2", "Name2", "Street2", "Address2City2" }, personInfo.ColumnNames);
            Assert.Equal(new string[] { "Guid2", "Salary", "DepartmentId2", "Image" }, employeeInfo.ColumnNames);
            Assert.Equal(new string[] { "Guid3", "Id3", "Name3", "BossId3", "Tags" }, deptInfo.ColumnNames);
        }

        [Fact]
        public void Table_Metadata()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo employeeInfo = tableBuilder.GetConfig<Employee>();
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
                ["Key3"] = "Val3_Person",
                ["Key5"] = "Val5_Employee",
                ["Key7"] = "Val7_Employee"
            }, employeeInfo.TableMetadata);

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
            ITableInfo employeeInfo = tableBuilder.GetConfig<Employee>();
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
                }
            }, personInfo.MemberMetadata);

            Assert.Equal(new Dictionary<string, IDictionary<string, object>>
            {
                ["Id"] = new Dictionary<string, object>
                {
                    ["Id1"] = "Id1_Base",
                    ["Id3"] = "Id3_Person",
                    ["Id5"] = "Id5_Employee",
                    ["Id7"] = "Id7_Employee"
                },
                ["Guid"] = new Dictionary<string, object>
                {
                    ["Guid1"] = "Guid1_Base",
                    ["Guid3"] = "Guid3_Person",
                    ["Guid5"] = "Guid5_Employee",
                    ["Guid7"] = "Guid7_Employee"
                },
                ["Name"] = new Dictionary<string, object>
                {
                    ["Name1"] = "Name1_Base",
                    ["Name3"] = "Name3_Person",
                    ["Name5"] = "Name5_Employee",
                    ["Name7"] = "Name7_Employee"
                },
                ["Surname"] = new Dictionary<string, object>
                {
                    ["Surname1"] = "Surname1_Person",
                    ["Surname3"] = "Surname3_Employee",
                    ["Surname5"] = "Surname5_Employee"
                },
                ["Address"] = new Dictionary<string, object>
                {
                    ["Address1"] = "Address1_Person",
                    ["Address3"] = "Address3_Employee",
                    ["Address5"] = "Address5_Employee"
                },
                ["Address.Street"] = new Dictionary<string, object>
                {
                    ["AddressStreet1"] = "AddressStreet1_Person",
                    ["AddressStreet3"] = "AddressStreet3_Employee",
                    ["AddressStreet5"] = "AddressStreet5_Employee"
                },
                ["Address.City"] = new Dictionary<string, object>
                {
                    ["AddressCity1"] = "AddressCity1_Person",
                    ["AddressCity3"] = "AddressCity3_Employee",
                    ["AddressCity5"] = "AddressCity5_Employee"
                },
                ["DepartmentId"] = new Dictionary<string, object>
                {
                    ["DepartmentId1"] = "DepartmentId1_Employee",
                    ["DepartmentId3"] = "DepartmentId3_Employee"
                },
                ["Department.Id"] = new Dictionary<string, object>
                {
                    ["DepartmentId1"] = "DepartmentId1_Employee",
                    ["DepartmentId3"] = "DepartmentId3_Employee"
                }
            }, employeeInfo.MemberMetadata);

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