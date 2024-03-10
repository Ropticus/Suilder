using System.Collections.Generic;
using Suilder.Reflection.Builder;
using Suilder.Reflection.Builder.Processors;
using Suilder.Test.Reflection.NoInherit.Tables;
using Xunit;

namespace Suilder.Test.Reflection.NoInherit.PropertyBuilder
{
    public class MemberMetadataRemoveTest : BaseTest
    {
        protected override void InitConfig()
        {
            tableBuilder.AddProcessor(new DefaultMetadataProcessor()
                .InheritAll(true));

            tableBuilder.Add<Person>()
                .Property(x => x.Id)
                .AddMetadata("Id1", "Id1_Person")
                .AddMetadata("Id2", "Id2_Person")
                .AddMetadata("Id3", "Id3_Person")
                .RemoveMetadata("Id2");

            tableBuilder.Add<Person>()
                .Property(x => x.Address.Street)
                .AddMetadata("AddressStreet1", "AddressStreet1_Person")
                .AddMetadata("AddressStreet2", "AddressStreet2_Person")
                .AddMetadata("AddressStreet3", "AddressStreet3_Person")
                .RemoveMetadata("AddressStreet2");

            tableBuilder.Add<Person>()
                .Property(x => x.Department)
                .AddMetadata("Department1", "Department1_Person")
                .AddMetadata("Department2", "Department2_Person")
                .AddMetadata("Department3", "Department3_Person")
                .RemoveMetadata("Department2");

            tableBuilder.Add<Department>()
                .Property(x => x.Id)
                .AddMetadata("Id1", "Id1_Department")
                .AddMetadata("Id2", "Id2_Department")
                .AddMetadata("Id3", "Id3_Department")
                .RemoveMetadata("Id2");

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
                    ["Id1"] = "Id1_Person",
                    ["Id3"] = "Id3_Person"
                },
                ["Address.Street"] = new Dictionary<string, object>
                {
                    ["AddressStreet1"] = "AddressStreet1_Person",
                    ["AddressStreet3"] = "AddressStreet3_Person"
                },
                ["Department"] = new Dictionary<string, object>
                {
                    ["Department1"] = "Department1_Person",
                    ["Department3"] = "Department3_Person"
                }
            }, personInfo.MemberMetadata);

            Assert.Equal(new Dictionary<string, IDictionary<string, object>>
            {
                ["Id"] = new Dictionary<string, object>
                {
                    ["Id1"] = "Id1_Department",
                    ["Id3"] = "Id3_Department"
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