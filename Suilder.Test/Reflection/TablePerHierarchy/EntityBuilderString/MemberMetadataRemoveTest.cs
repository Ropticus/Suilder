using System.Collections.Generic;
using Suilder.Reflection.Builder;
using Suilder.Reflection.Builder.Processors;
using Suilder.Test.Reflection.TablePerHierarchy.Tables;
using Xunit;

namespace Suilder.Test.Reflection.TablePerHierarchy.EntityBuilderString
{
    public class MemberMetadataRemoveTest : BaseTest
    {
        protected override void InitConfig()
        {
            tableBuilder.DefaultInheritTable(true)
                .AddProcessor(new DefaultMetadataProcessor()
                    .InheritAll(true));

            tableBuilder.Add<BaseConfig>()
                .AddMetadata("Id", "Id1", "Id1_Base")
                .AddMetadata("Id", "Id2", "Id2_Base")
                .AddMetadata("Id", "Id3", "Id3_Base")
                .RemoveMetadata("Id", "Id2");

            tableBuilder.Add<Person>()
                .AddMetadata("Id", "Id3", "Id3_Person")
                .AddMetadata("Id", "Id4", "Id4_Person")
                .AddMetadata("Id", "Id5", "Id5_Person")
                .RemoveMetadata("Id", "Id4")
                .AddMetadata("Address.Street", "AddressStreet1", "AddressStreet1_Person")
                .AddMetadata("Address.Street", "AddressStreet2", "AddressStreet2_Person")
                .AddMetadata("Address.Street", "AddressStreet3", "AddressStreet3_Person")
                .RemoveMetadata("Address.Street", "AddressStreet2");

            tableBuilder.Add<Employee>()
                .AddMetadata("Id", "Id5", "Id5_Employee")
                .AddMetadata("Id", "Id6", "Id6_Employee")
                .AddMetadata("Id", "Id7", "Id7_Employee")
                .RemoveMetadata("Id", "Id6")
                .AddMetadata("Address.Street", "AddressStreet3", "AddressStreet3_Employee")
                .AddMetadata("Address.Street", "AddressStreet4", "AddressStreet4_Employee")
                .AddMetadata("Address.Street", "AddressStreet5", "AddressStreet5_Employee")
                .RemoveMetadata("Address.Street", "AddressStreet4")
                .AddMetadata("Department", "Department1", "Department1_Employee")
                .AddMetadata("Department", "Department2", "Department2_Employee")
                .AddMetadata("Department", "Department3", "Department3_Employee")
                .RemoveMetadata("Department", "Department2");

            tableBuilder.Add<Department>()
                .AddMetadata("Id", "Id3", "Id3_Department")
                .AddMetadata("Id", "Id4", "Id4_Department")
                .AddMetadata("Id", "Id5", "Id5_Department")
                .RemoveMetadata("Id", "Id4")
                .AddMetadata("Employees", "Employees1", "Employees1_Department")
                .AddMetadata("Employees", "Employees2", "Employees2_Department")
                .AddMetadata("Employees", "Employees3", "Employees3_Department")
                .RemoveMetadata("Employees", "Employees2");
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