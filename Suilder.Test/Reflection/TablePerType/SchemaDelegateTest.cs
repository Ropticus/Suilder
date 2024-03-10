using Suilder.Reflection.Builder;
using Suilder.Test.Reflection.TablePerType.Tables;
using Xunit;

namespace Suilder.Test.Reflection.TablePerType
{
    public class SchemaDelegateTest : BaseTest
    {
        protected override void InitConfig()
        {
            tableBuilder.DefaultSchema(x => $"schema_{x.Name}");

            tableBuilder.Add<Person>();

            tableBuilder.Add<Employee>();

            tableBuilder.Add<Department>();
        }

        [Fact]
        public void Schema_Name()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo employeeInfo = tableBuilder.GetConfig<Employee>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal("schema_Person", personInfo.Schema);
            Assert.Equal("schema_Employee", employeeInfo.Schema);
            Assert.Equal("schema_Department", deptInfo.Schema);
        }

        [Fact]
        public void Table_Name()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo employeeInfo = tableBuilder.GetConfig<Employee>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal("Person", personInfo.TableName);
            Assert.Equal("Employee", employeeInfo.TableName);
            Assert.Equal("Department", deptInfo.TableName);
        }
    }
}