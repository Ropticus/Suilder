using System.Collections.Generic;
using Suilder.Reflection.Builder;
using Suilder.Reflection.Builder.Processors;
using Suilder.Test.Reflection.NoInherit.Tables;
using Xunit;

namespace Suilder.Test.Reflection.NoInherit
{
    public class TableMetadataRemoveTest : BaseTest
    {
        protected override void InitConfig()
        {
            tableBuilder.Add<Person>()
                .AddTableMetadata("Key1", "Val1_Person")
                .AddTableMetadata("Key2", "Val2_Person")
                .AddTableMetadata("Key3", "Val3_Person")
                .RemoveTableMetadata("Key2");

            tableBuilder.Add<Department>()
                .AddTableMetadata("Key1", "Val1_Department")
                .AddTableMetadata("Key2", "Val2_Department")
                .AddTableMetadata("Key3", "Val3_Department")
                .RemoveTableMetadata("Key2");
        }

        [Fact]
        public void Table_Metadata()
        {
            tableBuilder.AddProcessor(new DefaultMetadataProcessor()
                .InheritAll(true));

            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new Dictionary<string, object>
            {
                ["Key1"] = "Val1_Person",
                ["Key3"] = "Val3_Person"
            }, personInfo.TableMetadata);

            Assert.Equal(new Dictionary<string, object>
            {
                ["Key1"] = "Val1_Department",
                ["Key3"] = "Val3_Department"
            }, deptInfo.TableMetadata);
        }
    }
}