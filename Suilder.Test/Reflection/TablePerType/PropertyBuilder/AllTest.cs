using System.Collections.Generic;
using Suilder.Reflection.Builder;
using Suilder.Reflection.Builder.Processors;
using Suilder.Test.Reflection.TablePerType.Tables;
using Xunit;

namespace Suilder.Test.Reflection.TablePerType.PropertyBuilder
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
                .RemoveTableMetadata("Key2");

            tableBuilder.Add<BaseConfig>()
                .Property(x => x.Id)
                .AddMetadata("Id1", "Id1_Base")
                .AddMetadata("Id2", "Id2_Base")
                .AddMetadata("Id3", "Id3_Base")
                .RemoveMetadata("Id2");

            tableBuilder.Add<BaseConfig>()
                .Property(x => x.Guid)
                .AddMetadata("Guid1", "Guid1_Base")
                .AddMetadata("Guid2", "Guid2_Base")
                .AddMetadata("Guid3", "Guid3_Base")
                .RemoveMetadata("Guid2");

            tableBuilder.Add<BaseConfig>()
                .Property(x => x.Name)
                .AddMetadata("Name1", "Name1_Base")
                .AddMetadata("Name2", "Name2_Base")
                .AddMetadata("Name3", "Name3_Base")
                .RemoveMetadata("Name2");

            tableBuilder.Add<Person>()
                .Schema("schema_Person")
                .TableName("prefix_Person")
                .AddTableMetadata("Key3", "Val3_Person")
                .AddTableMetadata("Key4", "Val4_Person")
                .AddTableMetadata("Key5", "Val5_Person")
                .RemoveTableMetadata("Key4");

            tableBuilder.Add<Person>()
                .Property(x => x.Id)
                .ColumnName("Id2")
                .AddMetadata("Id3", "Id3_Person")
                .AddMetadata("Id4", "Id4_Person")
                .AddMetadata("Id5", "Id5_Person")
                .RemoveMetadata("Id4");

            tableBuilder.Add<Person>()
                .Property(x => x.Guid)
                .PrimaryKey()
                .ColumnName("Guid2")
                .AddMetadata("Guid3", "Guid3_Person")
                .AddMetadata("Guid4", "Guid4_Person")
                .AddMetadata("Guid5", "Guid5_Person")
                .RemoveMetadata("Guid4");

            tableBuilder.Add<Person>()
                .Property(x => x.Name)
                .ColumnName("Name2")
                .AddMetadata("Name3", "Name3_Person")
                .AddMetadata("Name4", "Name4_Person")
                .AddMetadata("Name5", "Name5_Person")
                .RemoveMetadata("Name4");

            tableBuilder.Add<Person>()
                .Property(x => x.Surname)
                .Ignore()
                .AddMetadata("Surname1", "Surname1_Person")
                .AddMetadata("Surname2", "Surname2_Person")
                .AddMetadata("Surname3", "Surname3_Person")
                .RemoveMetadata("Surname2");

            tableBuilder.Add<Person>()
                .Property(x => x.Address)
                .ColumnName("Address2", true)
                .AddMetadata("Address1", "Address1_Person")
                .AddMetadata("Address2", "Address2_Person")
                .AddMetadata("Address3", "Address3_Person")
                .RemoveMetadata("Address2");

            tableBuilder.Add<Person>()
                .Property(x => x.Address.Street)
                .ColumnName("Street2")
                .AddMetadata("AddressStreet1", "AddressStreet1_Person")
                .AddMetadata("AddressStreet2", "AddressStreet2_Person")
                .AddMetadata("AddressStreet3", "AddressStreet3_Person")
                .RemoveMetadata("AddressStreet2");

            tableBuilder.Add<Person>()
                .Property(x => x.Address.City)
                .ColumnName("City2", true)
                .AddMetadata("AddressCity1", "AddressCity1_Person")
                .AddMetadata("AddressCity2", "AddressCity2_Person")
                .AddMetadata("AddressCity3", "AddressCity3_Person")
                .RemoveMetadata("AddressCity2");

            tableBuilder.Add<Employee>()
                .Schema("schema_Employee")
                .TableName("prefix_Employee")
                .AddTableMetadata("Key5", "Val5_Employee")
                .AddTableMetadata("Key6", "Val6_Employee")
                .AddTableMetadata("Key7", "Val7_Employee")
                .RemoveTableMetadata("Key6");

            tableBuilder.Add<Employee>()
                .Property(x => x.Id)
                .AddMetadata("Id5", "Id5_Employee")
                .AddMetadata("Id6", "Id6_Employee")
                .AddMetadata("Id7", "Id7_Employee")
                .RemoveMetadata("Id6");

            tableBuilder.Add<Employee>()
                .Property(x => x.Guid)
                .AddMetadata("Guid5", "Guid5_Employee")
                .AddMetadata("Guid6", "Guid6_Employee")
                .AddMetadata("Guid7", "Guid7_Employee")
                .RemoveMetadata("Guid6");

            tableBuilder.Add<Employee>()
                .Property(x => x.Name)
                .AddMetadata("Name5", "Name5_Employee")
                .AddMetadata("Name6", "Name6_Employee")
                .AddMetadata("Name7", "Name7_Employee")
                .RemoveMetadata("Name6");

            tableBuilder.Add<Employee>()
                .Property(x => x.Surname)
                .AddMetadata("Surname3", "Surname3_Employee")
                .AddMetadata("Surname4", "Surname4_Employee")
                .AddMetadata("Surname5", "Surname5_Employee")
                .RemoveMetadata("Surname4");

            tableBuilder.Add<Employee>()
                .Property(x => x.Address)
                .AddMetadata("Address3", "Address3_Employee")
                .AddMetadata("Address4", "Address4_Employee")
                .AddMetadata("Address5", "Address5_Employee")
                .RemoveMetadata("Address4");

            tableBuilder.Add<Employee>()
                .Property(x => x.Address.Street)
                .AddMetadata("AddressStreet3", "AddressStreet3_Employee")
                .AddMetadata("AddressStreet4", "AddressStreet4_Employee")
                .AddMetadata("AddressStreet5", "AddressStreet5_Employee")
                .RemoveMetadata("AddressStreet4");

            tableBuilder.Add<Employee>()
                .Property(x => x.Address.City)
                .AddMetadata("AddressCity3", "AddressCity3_Employee")
                .AddMetadata("AddressCity4", "AddressCity4_Employee")
                .AddMetadata("AddressCity5", "AddressCity5_Employee")
                .RemoveMetadata("AddressCity4");

            tableBuilder.Add<Employee>()
                .Property(x => x.DepartmentId)
                .ForeignKey("DepartmentId2", true)
                .AddMetadata("DepartmentId1", "DepartmentId1_Employee")
                .AddMetadata("DepartmentId2", "DepartmentId2_Employee")
                .AddMetadata("DepartmentId3", "DepartmentId3_Employee")
                .RemoveMetadata("DepartmentId2");

            tableBuilder.Add<Employee>()
                .Property(x => x.Department.Id)
                .ForeignKey()
                .ColumnName("DepartmentId2", true)
                .AddMetadata("DepartmentId1", "DepartmentId1_Employee")
                .AddMetadata("DepartmentId2", "DepartmentId2_Employee")
                .AddMetadata("DepartmentId3", "DepartmentId3_Employee")
                .RemoveMetadata("DepartmentId2");

            tableBuilder.Add<Department>()
                .Schema("schema_Department")
                .TableName("prefix_Department")
                .AddTableMetadata("Key3", "Val3_Department")
                .AddTableMetadata("Key4", "Val4_Department")
                .AddTableMetadata("Key5", "Val5_Department")
                .RemoveTableMetadata("Key4");

            tableBuilder.Add<Department>()
                .Property(x => x.Id)
                .ColumnName("Id3")
                .AddMetadata("Id3", "Id3_Department")
                .AddMetadata("Id4", "Id4_Department")
                .AddMetadata("Id5", "Id5_Department")
                .RemoveMetadata("Id4");

            tableBuilder.Add<Department>()
                .Property(x => x.Guid)
                .PrimaryKey()
                .ColumnName("Guid3")
                .AddMetadata("Guid3", "Guid3_Department")
                .AddMetadata("Guid4", "Guid4_Department")
                .AddMetadata("Guid5", "Guid5_Department")
                .RemoveMetadata("Guid4");

            tableBuilder.Add<Department>()
                .Property(x => x.Name)
                .ColumnName("Name3")
                .AddMetadata("Name3", "Name3_Department")
                .AddMetadata("Name4", "Name4_Department")
                .AddMetadata("Name5", "Name5_Department")
                .RemoveMetadata("Name4");

            tableBuilder.Add<Department>()
                .Property(x => x.Boss.Id)
                .ForeignKey()
                .ColumnName("BossId3")
                .AddMetadata("BossId1", "BossId1_Department")
                .AddMetadata("BossId2", "BossId2_Department")
                .AddMetadata("BossId3", "BossId3_Department")
                .RemoveMetadata("BossId2");
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