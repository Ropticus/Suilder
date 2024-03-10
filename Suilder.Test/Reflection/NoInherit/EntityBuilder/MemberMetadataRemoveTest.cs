using System.Collections.Generic;
using Suilder.Reflection.Builder;
using Suilder.Reflection.Builder.Processors;
using Suilder.Test.Reflection.NoInherit.Tables;
using Xunit;

namespace Suilder.Test.Reflection.NoInherit.EntityBuilder
{
    public class MemberMetadataRemoveTest : BaseTest
    {
        protected override void InitConfig()
        {
            tableBuilder.AddProcessor(new DefaultMetadataProcessor()
                .InheritAll(true));

            tableBuilder.Add<Person>()
                .AddMetadata(x => x.Id, "Id1", "Id1_Person")
                .AddMetadata(x => x.Id, "Id2", "Id2_Person")
                .AddMetadata(x => x.Id, "Id3", "Id3_Person")
                .RemoveMetadata(x => x.Id, "Id2")
                .AddMetadata(x => x.Address.Street, "AddressStreet1", "AddressStreet1_Person")
                .AddMetadata(x => x.Address.Street, "AddressStreet2", "AddressStreet2_Person")
                .AddMetadata(x => x.Address.Street, "AddressStreet3", "AddressStreet3_Person")
                .RemoveMetadata(x => x.Address.Street, "AddressStreet2")
                .AddMetadata(x => x.Department, "Department1", "Department1_Person")
                .AddMetadata(x => x.Department, "Department2", "Department2_Person")
                .AddMetadata(x => x.Department, "Department3", "Department3_Person")
                .RemoveMetadata(x => x.Department, "Department2");

            tableBuilder.Add<Department>()
                .AddMetadata(x => x.Id, "Id1", "Id1_Department")
                .AddMetadata(x => x.Id, "Id2", "Id2_Department")
                .AddMetadata(x => x.Id, "Id3", "Id3_Department")
                .RemoveMetadata(x => x.Id, "Id2")
                .AddMetadata(x => x.Employees, "Employees1", "Employees1_Department")
                .AddMetadata(x => x.Employees, "Employees2", "Employees2_Department")
                .AddMetadata(x => x.Employees, "Employees3", "Employees3_Department")
                .RemoveMetadata(x => x.Employees, "Employees2");
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