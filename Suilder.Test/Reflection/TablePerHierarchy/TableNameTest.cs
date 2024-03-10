using Suilder.Reflection.Builder;
using Suilder.Test.Reflection.TablePerHierarchy.Tables;
using Xunit;

namespace Suilder.Test.Reflection.TablePerHierarchy
{
    public class TableNameTest : BaseTest
    {
        protected override void InitConfig()
        {
            tableBuilder.DefaultInheritTable(true);

            tableBuilder.Add<Person>()
                .TableName("prefix_Person");

            tableBuilder.Add<Employee>()
                .TableName("prefix_Employee");

            tableBuilder.Add<Department>()
                .TableName("prefix_Department");
        }

        [Fact]
        public void Schema_Name()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo employeeInfo = tableBuilder.GetConfig<Employee>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Null(personInfo.Schema);
            Assert.Null(employeeInfo.Schema);
            Assert.Null(deptInfo.Schema);
        }

        [Fact]
        public void Table_Name()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo employeeInfo = tableBuilder.GetConfig<Employee>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal("prefix_Person", personInfo.TableName);
            Assert.Equal("prefix_Person", employeeInfo.TableName);
            Assert.Equal("prefix_Department", deptInfo.TableName);
        }
    }
}