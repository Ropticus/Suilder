using System.Collections.Generic;
using Suilder.Reflection.Builder;
using Suilder.Reflection.Builder.Processors;
using Suilder.Test.Reflection.Builder.TableNested.Tables;
using Xunit;

namespace Suilder.Test.Reflection.Builder.TableNested
{
    public class MemberMetadataTest : BaseTest
    {
        protected override void InitConfig()
        {
            tableBuilder.Add<BaseConfig>()
                .AddMetadata(x => x.Id, "Id1", "Id1_Base")
                .AddMetadata(x => x.Id, "Id2", "Id2_Base")
                .AddMetadata(x => x.Id, "Id3", "Id3_Base");

            tableBuilder.Add<Person>()
                .AddMetadata(x => x.Id, "Id3", "Id3_Person")
                .AddMetadata(x => x.Id, "Id4", "Id4_Person")
                .AddMetadata(x => x.Id, "Id5", "Id5_Person")
                .AddMetadata(x => x.Employee.Address.Street, "AddressStreet1", "AddressStreet1_Person")
                .AddMetadata(x => x.Employee.Address.Street, "AddressStreet2", "AddressStreet2_Person")
                .AddMetadata(x => x.Employee.Department, "Department1", "Department1_Person")
                .AddMetadata(x => x.Employee.Department, "Department2", "Department2_Person");

            tableBuilder.AddNested<Employee>();

            tableBuilder.Add<Department>()
                .AddMetadata(x => x.Id, "Id3", "Id3_Department")
                .AddMetadata(x => x.Id, "Id4", "Id4_Department")
                .AddMetadata(x => x.Id, "Id5", "Id5_Department")
                .AddMetadata(x => x.Employees, "Employees1", "Employees1_Department")
                .AddMetadata(x => x.Employees, "Employees2", "Employees2_Department");
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
                    ["Id1"] = "Id1_Base",
                    ["Id2"] = "Id2_Base",
                    ["Id3"] = "Id3_Person",
                    ["Id4"] = "Id4_Person",
                    ["Id5"] = "Id5_Person"
                },
                ["Employee.Address.Street"] = new Dictionary<string, object>
                {
                    ["AddressStreet1"] = "AddressStreet1_Person",
                    ["AddressStreet2"] = "AddressStreet2_Person"
                },
                ["Employee.Department"] = new Dictionary<string, object>
                {
                    ["Department1"] = "Department1_Person",
                    ["Department2"] = "Department2_Person"
                }
            }, personInfo.MemberMetadata);

            Assert.Equal(new Dictionary<string, IDictionary<string, object>>
            {
                ["Id"] = new Dictionary<string, object>
                {
                    ["Id1"] = "Id1_Base",
                    ["Id2"] = "Id2_Base",
                    ["Id3"] = "Id3_Department",
                    ["Id4"] = "Id4_Department",
                    ["Id5"] = "Id5_Department"
                },
                ["Employees"] = new Dictionary<string, object>
                {
                    ["Employees1"] = "Employees1_Department",
                    ["Employees2"] = "Employees2_Department"
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
                    ["Id1"] = "Id1_Base",
                    ["Id2"] = "Id2_Base",
                    ["Id3"] = "Id3_Person",
                    ["Id4"] = "Id4_Person",
                    ["Id5"] = "Id5_Person"
                },
                ["Employee.Address.Street"] = new Dictionary<string, object>
                {
                    ["AddressStreet1"] = "AddressStreet1_Person",
                    ["AddressStreet2"] = "AddressStreet2_Person"
                },
                ["Employee.Department"] = new Dictionary<string, object>
                {
                    ["Department1"] = "Department1_Person",
                    ["Department2"] = "Department2_Person"
                }
            }, personInfo.MemberMetadata);

            Assert.Equal(new Dictionary<string, IDictionary<string, object>>
            {
                ["Id"] = new Dictionary<string, object>
                {
                    ["Id1"] = "Id1_Base",
                    ["Id2"] = "Id2_Base",
                    ["Id3"] = "Id3_Department",
                    ["Id4"] = "Id4_Department",
                    ["Id5"] = "Id5_Department"
                },
                ["Employees"] = new Dictionary<string, object>
                {
                    ["Employees1"] = "Employees1_Department",
                    ["Employees2"] = "Employees2_Department"
                }
            }, deptInfo.MemberMetadata);
        }

        [Fact]
        public void Inherit_Always()
        {
            tableBuilder.AddProcessor(new DefaultMetadataProcessor()
               .InheritAlways("Id2", "Id4", "AddressStreet2"));

            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new Dictionary<string, IDictionary<string, object>>
            {
                ["Id"] = new Dictionary<string, object>
                {
                    ["Id1"] = "Id1_Base",
                    ["Id2"] = "Id2_Base",
                    ["Id3"] = "Id3_Person",
                    ["Id4"] = "Id4_Person",
                    ["Id5"] = "Id5_Person"
                },
                ["Employee.Address.Street"] = new Dictionary<string, object>
                {
                    ["AddressStreet1"] = "AddressStreet1_Person",
                    ["AddressStreet2"] = "AddressStreet2_Person"
                },
                ["Employee.Department"] = new Dictionary<string, object>
                {
                    ["Department1"] = "Department1_Person",
                    ["Department2"] = "Department2_Person"
                }
            }, personInfo.MemberMetadata);

            Assert.Equal(new Dictionary<string, IDictionary<string, object>>
            {
                ["Id"] = new Dictionary<string, object>
                {
                    ["Id1"] = "Id1_Base",
                    ["Id2"] = "Id2_Base",
                    ["Id3"] = "Id3_Department",
                    ["Id4"] = "Id4_Department",
                    ["Id5"] = "Id5_Department"
                },
                ["Employees"] = new Dictionary<string, object>
                {
                    ["Employees1"] = "Employees1_Department",
                    ["Employees2"] = "Employees2_Department"
                }
            }, deptInfo.MemberMetadata);
        }

        [Fact]
        public void Ignore()
        {
            tableBuilder.AddProcessor(new DefaultMetadataProcessor()
                .Ignore("Id2", "Id4", "AddressStreet2", "Department2", "Employees2"));

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
                    ["AddressStreet1"] = "AddressStreet1_Person"
                },
                ["Employee.Department"] = new Dictionary<string, object>
                {
                    ["Department1"] = "Department1_Person"
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
                    ["Employees1"] = "Employees1_Department"
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
                    ["Id1"] = "Id1_Base",
                    ["Id3"] = "Id3_Person",
                    ["Id5"] = "Id5_Person"
                },
                ["Employee.Address.Street"] = new Dictionary<string, object>
                {
                    ["AddressStreet1"] = "AddressStreet1_Person"
                },
                ["Employee.Department"] = new Dictionary<string, object>
                {
                    ["Department1"] = "Department1_Person"
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
                    ["Employees1"] = "Employees1_Department"
                }
            }, deptInfo.MemberMetadata);
        }
    }
}