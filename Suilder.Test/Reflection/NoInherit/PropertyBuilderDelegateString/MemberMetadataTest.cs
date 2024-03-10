using System.Collections.Generic;
using Suilder.Reflection.Builder;
using Suilder.Reflection.Builder.Processors;
using Suilder.Test.Reflection.NoInherit.Tables;
using Xunit;

namespace Suilder.Test.Reflection.NoInherit.PropertyBuilderDelegateString
{
    public class MemberMetadataTest : BaseTest
    {
        protected override void InitConfig()
        {
            tableBuilder.Add<Person>()
                .Property("Id", p => p
                    .AddMetadata("Id1", "Id1_Person")
                    .AddMetadata("Id2", "Id2_Person")
                    .AddMetadata("Id3", "Id3_Person"))
                .Property("Address.Street", p => p
                    .AddMetadata("AddressStreet1", "AddressStreet1_Person")
                    .AddMetadata("AddressStreet2", "AddressStreet2_Person")
                    .AddMetadata("AddressStreet3", "AddressStreet3_Person"))
                .Property("Department", p => p
                    .AddMetadata("Department1", "Department1_Person")
                    .AddMetadata("Department2", "Department2_Person")
                    .AddMetadata("Department3", "Department3_Person"));

            tableBuilder.Add<Department>()
                .Property("Id", p => p
                    .AddMetadata("Id1", "Id1_Department")
                    .AddMetadata("Id2", "Id2_Department")
                    .AddMetadata("Id3", "Id3_Department"))
                .Property("Employees", p => p
                    .AddMetadata("Employees1", "Employees1_Department")
                    .AddMetadata("Employees2", "Employees2_Department")
                    .AddMetadata("Employees3", "Employees3_Department"));
        }

        [Fact]
        public void Default_Metadata()
        {
            tableBuilder.AddProcessor(new DefaultMetadataProcessor());

            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new Dictionary<string, IDictionary<string, object>>
            {
                ["Id"] = new Dictionary<string, object>
                {
                    ["Id1"] = "Id1_Person",
                    ["Id2"] = "Id2_Person",
                    ["Id3"] = "Id3_Person"
                },
                ["Address.Street"] = new Dictionary<string, object>
                {
                    ["AddressStreet1"] = "AddressStreet1_Person",
                    ["AddressStreet2"] = "AddressStreet2_Person",
                    ["AddressStreet3"] = "AddressStreet3_Person"
                },
                ["Department"] = new Dictionary<string, object>
                {
                    ["Department1"] = "Department1_Person",
                    ["Department2"] = "Department2_Person",
                    ["Department3"] = "Department3_Person"
                }
            }, personInfo.MemberMetadata);

            Assert.Equal(new Dictionary<string, IDictionary<string, object>>
            {
                ["Id"] = new Dictionary<string, object>
                {
                    ["Id1"] = "Id1_Department",
                    ["Id2"] = "Id2_Department",
                    ["Id3"] = "Id3_Department"
                },
                ["Employees"] = new Dictionary<string, object>
                {
                    ["Employees1"] = "Employees1_Department",
                    ["Employees2"] = "Employees2_Department",
                    ["Employees3"] = "Employees3_Department"
                }
            }, deptInfo.MemberMetadata);
        }

        [Fact]
        public void Inherit_All()
        {
            tableBuilder.AddProcessor(new DefaultMetadataProcessor()
                .InheritAll(true));

            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new Dictionary<string, IDictionary<string, object>>
            {
                ["Id"] = new Dictionary<string, object>
                {
                    ["Id1"] = "Id1_Person",
                    ["Id2"] = "Id2_Person",
                    ["Id3"] = "Id3_Person"
                },
                ["Address.Street"] = new Dictionary<string, object>
                {
                    ["AddressStreet1"] = "AddressStreet1_Person",
                    ["AddressStreet2"] = "AddressStreet2_Person",
                    ["AddressStreet3"] = "AddressStreet3_Person"
                },
                ["Department"] = new Dictionary<string, object>
                {
                    ["Department1"] = "Department1_Person",
                    ["Department2"] = "Department2_Person",
                    ["Department3"] = "Department3_Person"
                }
            }, personInfo.MemberMetadata);

            Assert.Equal(new Dictionary<string, IDictionary<string, object>>
            {
                ["Id"] = new Dictionary<string, object>
                {
                    ["Id1"] = "Id1_Department",
                    ["Id2"] = "Id2_Department",
                    ["Id3"] = "Id3_Department"
                },
                ["Employees"] = new Dictionary<string, object>
                {
                    ["Employees1"] = "Employees1_Department",
                    ["Employees2"] = "Employees2_Department",
                    ["Employees3"] = "Employees3_Department"
                }
            }, deptInfo.MemberMetadata);
        }

        [Fact]
        public void Inherit_Always()
        {
            tableBuilder.AddProcessor(new DefaultMetadataProcessor()
                .InheritAlways("Id2", "AddressStreet2"));

            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new Dictionary<string, IDictionary<string, object>>
            {
                ["Id"] = new Dictionary<string, object>
                {
                    ["Id1"] = "Id1_Person",
                    ["Id2"] = "Id2_Person",
                    ["Id3"] = "Id3_Person"
                },
                ["Address.Street"] = new Dictionary<string, object>
                {
                    ["AddressStreet1"] = "AddressStreet1_Person",
                    ["AddressStreet2"] = "AddressStreet2_Person",
                    ["AddressStreet3"] = "AddressStreet3_Person"
                },
                ["Department"] = new Dictionary<string, object>
                {
                    ["Department1"] = "Department1_Person",
                    ["Department2"] = "Department2_Person",
                    ["Department3"] = "Department3_Person"
                }
            }, personInfo.MemberMetadata);

            Assert.Equal(new Dictionary<string, IDictionary<string, object>>
            {
                ["Id"] = new Dictionary<string, object>
                {
                    ["Id1"] = "Id1_Department",
                    ["Id2"] = "Id2_Department",
                    ["Id3"] = "Id3_Department"
                },
                ["Employees"] = new Dictionary<string, object>
                {
                    ["Employees1"] = "Employees1_Department",
                    ["Employees2"] = "Employees2_Department",
                    ["Employees3"] = "Employees3_Department"
                }
            }, deptInfo.MemberMetadata);
        }

        [Fact]
        public void Ignore()
        {
            tableBuilder.AddProcessor(new DefaultMetadataProcessor()
                .Ignore("Id2", "AddressStreet2", "Department2", "Employees2"));

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

        [Fact]
        public void Ignore_Delegate()
        {
            tableBuilder.AddProcessor(new DefaultMetadataProcessor()
                .Ignore(x => x.EndsWith("2") || x.EndsWith("4") || x.EndsWith("6")));

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