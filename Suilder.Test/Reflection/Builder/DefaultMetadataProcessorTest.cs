using System.Collections.Generic;
using Suilder.Reflection.Builder;
using Suilder.Reflection.Builder.Processors;
using Suilder.Test.Reflection.Builder.TablePerType.Tables;
using Xunit;

namespace Suilder.Test.Reflection.Builder
{
    public class DefaultMetadataProcessorTest : BaseTest
    {
        [Fact]
        public void Inherit_All_Table()
        {
            tableBuilder.AddProcessor(new DefaultMetadataProcessor()
                .InheritAllTable(true));

            tableBuilder.Add<Person>()
                .AddTableMetadata("Key1", "Val1_Person")
                .AddTableMetadata("Key2", "Val2_Person")
                .AddMetadata(x => x.Id, "Id1", "Id1_Person")
                .AddMetadata(x => x.Id, "Id2", "Id2_Person");

            tableBuilder.Add<Employee>();

            ITableInfo employeeInfo = tableBuilder.GetConfig<Employee>();

            Assert.Equal(new Dictionary<string, object>
            {
                ["Key1"] = "Val1_Person",
                ["Key2"] = "Val2_Person"
            }, employeeInfo.TableMetadata);

            Assert.Equal(new Dictionary<string, IDictionary<string, object>>(), employeeInfo.MemberMetadata);
        }

        [Fact]
        public void Inherit_All_Members()
        {
            tableBuilder.AddProcessor(new DefaultMetadataProcessor()
                .InheritAllMembers(true));

            tableBuilder.Add<Person>()
                .AddTableMetadata("Key1", "Val1_Person")
                .AddTableMetadata("Key2", "Val2_Person")
                .AddMetadata(x => x.Id, "Id1", "Id1_Person")
                .AddMetadata(x => x.Id, "Id2", "Id2_Person");

            tableBuilder.Add<Employee>();

            ITableInfo employeeInfo = tableBuilder.GetConfig<Employee>();

            Assert.Equal(new Dictionary<string, object>(), employeeInfo.TableMetadata);

            Assert.Equal(new Dictionary<string, IDictionary<string, object>>
            {
                ["Id"] = new Dictionary<string, object>
                {
                    ["Id1"] = "Id1_Person",
                    ["Id2"] = "Id2_Person"
                }
            }, employeeInfo.MemberMetadata);
        }

        [Fact]
        public void Inherit_Always_Params()
        {
            tableBuilder.AddProcessor(new DefaultMetadataProcessor()
                .InheritAlways("Key2", "Id2"));

            tableBuilder.Add<Person>()
                .AddTableMetadata("Key1", "Val1_Person")
                .AddTableMetadata("Key2", "Val2_Person")
                .AddMetadata(x => x.Id, "Id1", "Id1_Person")
                .AddMetadata(x => x.Id, "Id2", "Id2_Person");

            tableBuilder.Add<Employee>();

            ITableInfo employeeInfo = tableBuilder.GetConfig<Employee>();

            Assert.Equal(new Dictionary<string, object>
            {
                ["Key2"] = "Val2_Person"
            }, employeeInfo.TableMetadata);

            Assert.Equal(new Dictionary<string, IDictionary<string, object>>
            {
                ["Id"] = new Dictionary<string, object>
                {
                    ["Id2"] = "Id2_Person"
                }
            }, employeeInfo.MemberMetadata);
        }

        [Fact]
        public void Inherit_Always_Enumerable()
        {
            tableBuilder.AddProcessor(new DefaultMetadataProcessor()
                .InheritAlways(new List<string> { "Key2", "Id2" }));

            tableBuilder.Add<Person>()
                .AddTableMetadata("Key1", "Val1_Person")
                .AddTableMetadata("Key2", "Val2_Person")
                .AddMetadata(x => x.Id, "Id1", "Id1_Person")
                .AddMetadata(x => x.Id, "Id2", "Id2_Person");

            tableBuilder.Add<Employee>();

            ITableInfo employeeInfo = tableBuilder.GetConfig<Employee>();

            Assert.Equal(new Dictionary<string, object>
            {
                ["Key2"] = "Val2_Person"
            }, employeeInfo.TableMetadata);

            Assert.Equal(new Dictionary<string, IDictionary<string, object>>
            {
                ["Id"] = new Dictionary<string, object>
                {
                    ["Id2"] = "Id2_Person"
                }
            }, employeeInfo.MemberMetadata);
        }

        [Fact]
        public void Ignore_Params()
        {
            tableBuilder.AddProcessor(new DefaultMetadataProcessor()
                .Ignore("Key2", "Id2"));

            tableBuilder.Add<Person>()
                .AddTableMetadata("Key1", "Val1_Person")
                .AddTableMetadata("Key2", "Val2_Person")
                .AddMetadata(x => x.Id, "Id1", "Id1_Person")
                .AddMetadata(x => x.Id, "Id2", "Id2_Person");

            ITableInfo personInfo = tableBuilder.GetConfig<Person>();

            Assert.Equal(new Dictionary<string, object>
            {
                ["Key1"] = "Val1_Person"
            }, personInfo.TableMetadata);

            Assert.Equal(new Dictionary<string, IDictionary<string, object>>
            {
                ["Id"] = new Dictionary<string, object>
                {
                    ["Id1"] = "Id1_Person"
                }
            }, personInfo.MemberMetadata);
        }

        [Fact]
        public void Ignore_Enumerable()
        {
            tableBuilder.AddProcessor(new DefaultMetadataProcessor()
                .Ignore(new List<string> { "Key2", "Id2" }));

            tableBuilder.Add<Person>()
                .AddTableMetadata("Key1", "Val1_Person")
                .AddTableMetadata("Key2", "Val2_Person")
                .AddMetadata(x => x.Id, "Id1", "Id1_Person")
                .AddMetadata(x => x.Id, "Id2", "Id2_Person");

            ITableInfo personInfo = tableBuilder.GetConfig<Person>();

            Assert.Equal(new Dictionary<string, object>
            {
                ["Key1"] = "Val1_Person"
            }, personInfo.TableMetadata);

            Assert.Equal(new Dictionary<string, IDictionary<string, object>>
            {
                ["Id"] = new Dictionary<string, object>
                {
                    ["Id1"] = "Id1_Person"
                }
            }, personInfo.MemberMetadata);
        }

        [Fact]
        public void Empty_Metadata()
        {
            tableBuilder.AddProcessor(new DefaultMetadataProcessor()
                .Ignore("Key1", "Key2", "Id1", "Id2"));

            tableBuilder.Add<Person>()
                .AddTableMetadata("Key1", "Val1_Person")
                .AddTableMetadata("Key2", "Val2_Person")
                .AddMetadata(x => x.Id, "Id1", "Id1_Person")
                .AddMetadata(x => x.Id, "Id2", "Id2_Person");

            ITableInfo personInfo = tableBuilder.GetConfig<Person>();

            Assert.Equal(new Dictionary<string, object>(), personInfo.TableMetadata);

            Assert.Equal(new Dictionary<string, IDictionary<string, object>>(), personInfo.MemberMetadata);
        }

        [Fact]
        public void Multiple_Processors()
        {
            tableBuilder.AddProcessor(new DefaultMetadataProcessor()
                .Ignore("Key2", "Id2"));

            tableBuilder.AddProcessor(new DefaultMetadataProcessor()
                .Ignore("Key1", "Id1"));

            tableBuilder.Add<Person>()
                .AddTableMetadata("Key1", "Val1_Person")
                .AddTableMetadata("Key2", "Val2_Person")
                .AddMetadata(x => x.Id, "Id1", "Id1_Person")
                .AddMetadata(x => x.Id, "Id2", "Id2_Person");

            ITableInfo personInfo = tableBuilder.GetConfig<Person>();

            Assert.Equal(new Dictionary<string, object>
            {
                ["Key1"] = "Val1_Person",
                ["Key2"] = "Val2_Person"
            }, personInfo.TableMetadata);

            Assert.Equal(new Dictionary<string, IDictionary<string, object>>
            {
                ["Id"] = new Dictionary<string, object>
                {
                    ["Id1"] = "Id1_Person",
                    ["Id2"] = "Id2_Person"
                }
            }, personInfo.MemberMetadata);
        }
    }
}