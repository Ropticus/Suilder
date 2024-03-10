using System.Collections.Generic;
using Suilder.Reflection.Builder;
using Suilder.Reflection.Builder.Processors;
using Suilder.Test.Reflection.TablePerType.Tables;
using Xunit;

namespace Suilder.Test.Reflection.TablePerType.EntityBuilder
{
    public class MemberMetadataRemoveTest : BaseTest
    {
        protected override void InitConfig()
        {
            tableBuilder.AddProcessor(new DefaultMetadataProcessor()
                .InheritAll(true));

            tableBuilder.Add<BaseConfig>()
                .AddMetadata(x => x.Id, "Id1", "Id1_Base")
                .AddMetadata(x => x.Id, "Id2", "Id2_Base")
                .AddMetadata(x => x.Id, "Id3", "Id3_Base")
                .RemoveMetadata(x => x.Id, "Id2");

            tableBuilder.Add<Person>()
                .AddMetadata(x => x.Id, "Id3", "Id3_Person")
                .AddMetadata(x => x.Id, "Id4", "Id4_Person")
                .AddMetadata(x => x.Id, "Id5", "Id5_Person")
                .RemoveMetadata(x => x.Id, "Id4")
                .AddMetadata(x => x.Address.Street, "AddressStreet1", "AddressStreet1_Person")
                .AddMetadata(x => x.Address.Street, "AddressStreet2", "AddressStreet2_Person")
                .AddMetadata(x => x.Address.Street, "AddressStreet3", "AddressStreet3_Person")
                .RemoveMetadata(x => x.Address.Street, "AddressStreet2");

            tableBuilder.Add<Employee>()
                .AddMetadata(x => x.Id, "Id5", "Id5_Employee")
                .AddMetadata(x => x.Id, "Id6", "Id6_Employee")
                .AddMetadata(x => x.Id, "Id7", "Id7_Employee")
                .RemoveMetadata(x => x.Id, "Id6")
                .AddMetadata(x => x.Address.Street, "AddressStreet3", "AddressStreet3_Employee")
                .AddMetadata(x => x.Address.Street, "AddressStreet4", "AddressStreet4_Employee")
                .AddMetadata(x => x.Address.Street, "AddressStreet5", "AddressStreet5_Employee")
                .RemoveMetadata(x => x.Address.Street, "AddressStreet4")
                .AddMetadata(x => x.Department, "Department1", "Department1_Employee")
                .AddMetadata(x => x.Department, "Department2", "Department2_Employee")
                .AddMetadata(x => x.Department, "Department3", "Department3_Employee")
                .RemoveMetadata(x => x.Department, "Department2");

            tableBuilder.Add<Department>()
                .AddMetadata(x => x.Id, "Id3", "Id3_Department")
                .AddMetadata(x => x.Id, "Id4", "Id4_Department")
                .AddMetadata(x => x.Id, "Id5", "Id5_Department")
                .RemoveMetadata(x => x.Id, "Id4")
                .AddMetadata(x => x.Employees, "Employees1", "Employees1_Department")
                .AddMetadata(x => x.Employees, "Employees2", "Employees2_Department")
                .AddMetadata(x => x.Employees, "Employees3", "Employees3_Department")
                .RemoveMetadata(x => x.Employees, "Employees2");
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