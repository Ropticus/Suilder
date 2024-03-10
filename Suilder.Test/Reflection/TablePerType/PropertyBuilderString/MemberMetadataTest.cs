using System.Collections.Generic;
using Suilder.Reflection.Builder;
using Suilder.Reflection.Builder.Processors;
using Suilder.Test.Reflection.TablePerType.Tables;
using Xunit;

namespace Suilder.Test.Reflection.TablePerType.PropertyBuilderString
{
    public class MemberMetadataTest : BaseTest
    {
        protected override void InitConfig()
        {
            tableBuilder.Add<BaseConfig>()
                .Property("Id")
                .AddMetadata("Id1", "Id1_Base")
                .AddMetadata("Id2", "Id2_Base")
                .AddMetadata("Id3", "Id3_Base");

            tableBuilder.Add<Person>()
                .Property("Id")
                .AddMetadata("Id3", "Id3_Person")
                .AddMetadata("Id4", "Id4_Person")
                .AddMetadata("Id5", "Id5_Person");

            tableBuilder.Add<Person>()
                .Property("Address.Street")
                .AddMetadata("AddressStreet1", "AddressStreet1_Person")
                .AddMetadata("AddressStreet2", "AddressStreet2_Person")
                .AddMetadata("AddressStreet3", "AddressStreet3_Person");

            tableBuilder.Add<Employee>()
                .Property("Id")
                .AddMetadata("Id5", "Id5_Employee")
                .AddMetadata("Id6", "Id6_Employee")
                .AddMetadata("Id7", "Id7_Employee");

            tableBuilder.Add<Employee>()
                .Property("Address.Street")
                .AddMetadata("AddressStreet3", "AddressStreet3_Employee")
                .AddMetadata("AddressStreet4", "AddressStreet4_Employee")
                .AddMetadata("AddressStreet5", "AddressStreet5_Employee");

            tableBuilder.Add<Employee>()
                .Property("Department")
                .AddMetadata("Department1", "Department1_Employee")
                .AddMetadata("Department2", "Department2_Employee")
                .AddMetadata("Department3", "Department3_Employee");

            tableBuilder.Add<Department>()
                .Property("Id")
                .AddMetadata("Id3", "Id3_Department")
                .AddMetadata("Id4", "Id4_Department")
                .AddMetadata("Id5", "Id5_Department");

            tableBuilder.Add<Department>()
                .Property("Employees")
                .AddMetadata("Employees1", "Employees1_Department")
                .AddMetadata("Employees2", "Employees2_Department")
                .AddMetadata("Employees3", "Employees3_Department");
        }

        [Fact]
        public void Default_Metadata()
        {
            tableBuilder.AddProcessor(new DefaultMetadataProcessor());

            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo employeeInfo = tableBuilder.GetConfig<Employee>();
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
                ["Address.Street"] = new Dictionary<string, object>
                {
                    ["AddressStreet1"] = "AddressStreet1_Person",
                    ["AddressStreet2"] = "AddressStreet2_Person",
                    ["AddressStreet3"] = "AddressStreet3_Person"
                }
            }, personInfo.MemberMetadata);

            Assert.Equal(new Dictionary<string, IDictionary<string, object>>
            {
                ["Id"] = new Dictionary<string, object>
                {
                    ["Id5"] = "Id5_Employee",
                    ["Id6"] = "Id6_Employee",
                    ["Id7"] = "Id7_Employee"
                },
                ["Address.Street"] = new Dictionary<string, object>
                {
                    ["AddressStreet3"] = "AddressStreet3_Employee",
                    ["AddressStreet4"] = "AddressStreet4_Employee",
                    ["AddressStreet5"] = "AddressStreet5_Employee"
                },
                ["Department"] = new Dictionary<string, object>
                {
                    ["Department1"] = "Department1_Employee",
                    ["Department2"] = "Department2_Employee",
                    ["Department3"] = "Department3_Employee"
                }
            }, employeeInfo.MemberMetadata);

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
            ITableInfo employeeInfo = tableBuilder.GetConfig<Employee>();
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
                ["Address.Street"] = new Dictionary<string, object>
                {
                    ["AddressStreet1"] = "AddressStreet1_Person",
                    ["AddressStreet2"] = "AddressStreet2_Person",
                    ["AddressStreet3"] = "AddressStreet3_Person"
                }
            }, personInfo.MemberMetadata);

            Assert.Equal(new Dictionary<string, IDictionary<string, object>>
            {
                ["Id"] = new Dictionary<string, object>
                {
                    ["Id1"] = "Id1_Base",
                    ["Id2"] = "Id2_Base",
                    ["Id3"] = "Id3_Person",
                    ["Id4"] = "Id4_Person",
                    ["Id5"] = "Id5_Employee",
                    ["Id6"] = "Id6_Employee",
                    ["Id7"] = "Id7_Employee"
                },
                ["Address.Street"] = new Dictionary<string, object>
                {
                    ["AddressStreet1"] = "AddressStreet1_Person",
                    ["AddressStreet2"] = "AddressStreet2_Person",
                    ["AddressStreet3"] = "AddressStreet3_Employee",
                    ["AddressStreet4"] = "AddressStreet4_Employee",
                    ["AddressStreet5"] = "AddressStreet5_Employee"
                },
                ["Department"] = new Dictionary<string, object>
                {
                    ["Department1"] = "Department1_Employee",
                    ["Department2"] = "Department2_Employee",
                    ["Department3"] = "Department3_Employee"
                }
            }, employeeInfo.MemberMetadata);

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
                    ["Employees2"] = "Employees2_Department",
                    ["Employees3"] = "Employees3_Department"
                }
            }, deptInfo.MemberMetadata);
        }

        [Fact]
        public void Inherit_Always()
        {
            tableBuilder.AddProcessor(new DefaultMetadataProcessor()
                .InheritAlways("Id2", "Id4", "AddressStreet2"));

            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo employeeInfo = tableBuilder.GetConfig<Employee>();
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
                ["Address.Street"] = new Dictionary<string, object>
                {
                    ["AddressStreet1"] = "AddressStreet1_Person",
                    ["AddressStreet2"] = "AddressStreet2_Person",
                    ["AddressStreet3"] = "AddressStreet3_Person"
                }
            }, personInfo.MemberMetadata);

            Assert.Equal(new Dictionary<string, IDictionary<string, object>>
            {
                ["Id"] = new Dictionary<string, object>
                {
                    ["Id2"] = "Id2_Base",
                    ["Id4"] = "Id4_Person",
                    ["Id5"] = "Id5_Employee",
                    ["Id6"] = "Id6_Employee",
                    ["Id7"] = "Id7_Employee"
                },
                ["Address.Street"] = new Dictionary<string, object>
                {
                    ["AddressStreet2"] = "AddressStreet2_Person",
                    ["AddressStreet3"] = "AddressStreet3_Employee",
                    ["AddressStreet4"] = "AddressStreet4_Employee",
                    ["AddressStreet5"] = "AddressStreet5_Employee"
                },
                ["Department"] = new Dictionary<string, object>
                {
                    ["Department1"] = "Department1_Employee",
                    ["Department2"] = "Department2_Employee",
                    ["Department3"] = "Department3_Employee"
                }
            }, employeeInfo.MemberMetadata);

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
                    ["Employees2"] = "Employees2_Department",
                    ["Employees3"] = "Employees3_Department"
                }
            }, deptInfo.MemberMetadata);
        }

        [Fact]
        public void Ignore()
        {
            tableBuilder.AddProcessor(new DefaultMetadataProcessor()
                .Ignore("Id2", "Id4", "Id6", "AddressStreet2", "AddressStreet4", "Department2", "Employees2"));

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
                    ["Id5"] = "Id5_Employee",
                    ["Id7"] = "Id7_Employee"
                },
                ["Address.Street"] = new Dictionary<string, object>
                {
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

        [Fact]
        public void Ignore_Delegate()
        {
            tableBuilder.AddProcessor(new DefaultMetadataProcessor()
                .Ignore(x => x.EndsWith("2") || x.EndsWith("4") || x.EndsWith("6")));

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
                    ["Id5"] = "Id5_Employee",
                    ["Id7"] = "Id7_Employee"
                },
                ["Address.Street"] = new Dictionary<string, object>
                {
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