using System.Collections.Generic;
using Suilder.Reflection.Builder;
using Suilder.Reflection.Builder.Processors;
using Suilder.Test.Reflection.TableNested.Tables;
using Xunit;

namespace Suilder.Test.Reflection.TableNested.PropertyBuilder
{
    public class MemberMetadataRemoveTest : BaseTest
    {
        protected override void InitConfig()
        {
            tableBuilder.AddProcessor(new DefaultMetadataProcessor()
                .InheritAll(true));

            tableBuilder.Add<BaseConfig>()
                .Property(x => x.Id)
                .AddMetadata("Id1", "Id1_Base")
                .AddMetadata("Id2", "Id2_Base")
                .AddMetadata("Id3", "Id3_Base")
                .RemoveMetadata("Id2");

            tableBuilder.Add<Person>()
                .Property(x => x.Id)
                .AddMetadata("Id3", "Id3_Person")
                .AddMetadata("Id4", "Id4_Person")
                .AddMetadata("Id5", "Id5_Person")
                .RemoveMetadata("Id4");

            tableBuilder.Add<Person>()
                .Property(x => x.Employee.Address.Street)
                .AddMetadata("AddressStreet1", "AddressStreet1_Person")
                .AddMetadata("AddressStreet2", "AddressStreet2_Person")
                .AddMetadata("AddressStreet3", "AddressStreet3_Person")
                .RemoveMetadata("AddressStreet2");

            tableBuilder.Add<Person>()
                .Property(x => x.Employee.Department)
                .AddMetadata("Department1", "Department1_Person")
                .AddMetadata("Department2", "Department2_Person")
                .AddMetadata("Department3", "Department3_Person")
                .RemoveMetadata("Department2");

            tableBuilder.AddNested<Employee>();

            tableBuilder.Add<Department>()
                .Property(x => x.Id)
                .AddMetadata("Id3", "Id3_Department")
                .AddMetadata("Id4", "Id4_Department")
                .AddMetadata("Id5", "Id5_Department")
                .RemoveMetadata("Id4");

            tableBuilder.Add<Department>()
                .Property(x => x.Employees)
                .AddMetadata("Employees1", "Employees1_Department")
                .AddMetadata("Employees2", "Employees2_Department")
                .AddMetadata("Employees3", "Employees3_Department")
                .RemoveMetadata("Employees2");
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
                ["Employee.Address.Street"] = new Dictionary<string, object>
                {
                    ["AddressStreet1"] = "AddressStreet1_Person",
                    ["AddressStreet3"] = "AddressStreet3_Person"
                },
                ["Employee.Department"] = new Dictionary<string, object>
                {
                    ["Department1"] = "Department1_Person",
                    ["Department3"] = "Department3_Person"
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
                ["Employees"] = new Dictionary<string, object>
                {
                    ["Employees1"] = "Employees1_Department",
                    ["Employees3"] = "Employees3_Department"
                }
            }, deptInfo.MemberMetadata);
        }
    }
}