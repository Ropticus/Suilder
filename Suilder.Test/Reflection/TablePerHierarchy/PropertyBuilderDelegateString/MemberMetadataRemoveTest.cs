using System.Collections.Generic;
using Suilder.Reflection.Builder;
using Suilder.Reflection.Builder.Processors;
using Suilder.Test.Reflection.TablePerHierarchy.Tables;
using Xunit;

namespace Suilder.Test.Reflection.TablePerHierarchy.PropertyBuilderDelegateString
{
    public class MemberMetadataRemoveTest : BaseTest
    {
        protected override void InitConfig()
        {
            tableBuilder.DefaultInheritTable(true)
                .AddProcessor(new DefaultMetadataProcessor()
                    .InheritAll(true));

            tableBuilder.Add<BaseConfig>()
                .Property("Id", p => p
                    .AddMetadata("Id1", "Id1_Base")
                    .AddMetadata("Id2", "Id2_Base")
                    .AddMetadata("Id3", "Id3_Base")
                    .RemoveMetadata("Id2"));

            tableBuilder.Add<Person>()
                .Property("Id", p => p
                    .AddMetadata("Id3", "Id3_Person")
                    .AddMetadata("Id4", "Id4_Person")
                    .AddMetadata("Id5", "Id5_Person")
                    .RemoveMetadata("Id4"))
                .Property("Address.Street", p => p
                    .AddMetadata("AddressStreet1", "AddressStreet1_Person")
                    .AddMetadata("AddressStreet2", "AddressStreet2_Person")
                    .AddMetadata("AddressStreet3", "AddressStreet3_Person")
                    .RemoveMetadata("AddressStreet2"));

            tableBuilder.Add<Employee>()
                .Property("Id", p => p
                    .AddMetadata("Id5", "Id5_Employee")
                    .AddMetadata("Id6", "Id6_Employee")
                    .AddMetadata("Id7", "Id7_Employee")
                    .RemoveMetadata("Id6"))
                .Property("Address.Street", p => p
                    .AddMetadata("AddressStreet3", "AddressStreet3_Employee")
                    .AddMetadata("AddressStreet4", "AddressStreet4_Employee")
                    .AddMetadata("AddressStreet5", "AddressStreet5_Employee")
                    .RemoveMetadata("AddressStreet4"))
                .Property("Department", p => p
                    .AddMetadata("Department1", "Department1_Employee")
                    .AddMetadata("Department2", "Department2_Employee")
                    .AddMetadata("Department3", "Department3_Employee")
                    .RemoveMetadata("Department2"));

            tableBuilder.Add<Department>()
                .Property("Id", p => p
                    .AddMetadata("Id3", "Id3_Department")
                    .AddMetadata("Id4", "Id4_Department")
                    .AddMetadata("Id5", "Id5_Department")
                    .RemoveMetadata("Id4"))
                .Property("Employees", p => p
                    .AddMetadata("Employees1", "Employees1_Department")
                    .AddMetadata("Employees2", "Employees2_Department")
                    .AddMetadata("Employees3", "Employees3_Department")
                    .RemoveMetadata("Employees2"));
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
                ["Address.Street"] = new Dictionary<string, object>
                {
                    ["AddressStreet1"] = "AddressStreet1_Person",
                    ["AddressStreet3"] = "AddressStreet3_Person"
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
                ["Address.Street"] = new Dictionary<string, object>
                {
                    ["AddressStreet1"] = "AddressStreet1_Person",
                    ["AddressStreet3"] = "AddressStreet3_Employee",
                    ["AddressStreet5"] = "AddressStreet5_Employee"
                },
                ["Department"] = new Dictionary<string, object>
                {
                    ["Department1"] = "Department1_Employee",
                    ["Department3"] = "Department3_Employee"
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
                ["Employees"] = new Dictionary<string, object>
                {
                    ["Employees1"] = "Employees1_Department",
                    ["Employees3"] = "Employees3_Department"
                }
            }, deptInfo.MemberMetadata);
        }
    }
}