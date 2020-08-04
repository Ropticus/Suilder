using Suilder.Reflection.Builder;
using Suilder.Test.Reflection.Builder.TableNested.Tables;
using Xunit;

namespace Suilder.Test.Reflection.Builder.TableNested
{
    public class SchemaTest : BaseTest
    {
        protected override void InitConfig()
        {
            tableBuilder.Add<Person>()
                .Schema("schema_Person");

            tableBuilder.AddNested<Employee>();

            tableBuilder.Add<Department>()
                .Schema("schema_Department");
        }

        [Fact]
        public void Schema_Name()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal("schema_Person", personInfo.Schema);
            Assert.Equal("schema_Department", deptInfo.Schema);
        }

        [Fact]
        public void Table_Name()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal("Person", personInfo.TableName);
            Assert.Equal("Department", deptInfo.TableName);
        }
    }
}