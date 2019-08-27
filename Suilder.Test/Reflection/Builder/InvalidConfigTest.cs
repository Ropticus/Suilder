using System;
using Suilder.Exceptions;
using Suilder.Test.Reflection.Builder.TablePerType.Tables;
using Xunit;

namespace Suilder.Test.Reflection.Builder
{
    public class InvalidConfigTest : BaseTest
    {
        [Fact]
        public void Invalid_Table_And_Nested()
        {
            tableBuilder.Add<Person>();

            tableBuilder.Add<Employee>();

            tableBuilder.AddNested<Employee>();

            tableBuilder.Add<Department>();

            Exception ex = Assert.Throws<InvalidConfigurationException>(() => tableBuilder.GetConfig());
            Assert.Equal($"The type \"{typeof(Employee)}\" registered as a table, cannot be marked as nested.", ex.Message);
        }

        [Fact]
        public void Inherit_Table_Without_Base()
        {
            tableBuilder.Add<BaseConfig>()
                .InheritTable(true);

            tableBuilder.Add<Person>();

            Exception ex = Assert.Throws<InvalidConfigurationException>(() => tableBuilder.GetConfig());
            Assert.Equal($"The type \"{typeof(BaseConfig)}\" does not have a base type to inherit.", ex.Message);
        }

        [Fact]
        public void Inherit_Columns_Without_Base()
        {
            tableBuilder.Add<BaseConfig>()
                .InheritColumns(true);

            tableBuilder.Add<Person>();

            Exception ex = Assert.Throws<InvalidConfigurationException>(() => tableBuilder.GetConfig());
            Assert.Equal($"The type \"{typeof(BaseConfig)}\" does not have a base type to inherit.", ex.Message);
        }

        [Fact]
        public void Not_Exists_Primary_Key()
        {
            tableBuilder.DefaultPrimaryKey(x => "Other");

            tableBuilder.Add<Person>();

            Exception ex = Assert.Throws<InvalidConfigurationException>(() => tableBuilder.GetConfig());
            Assert.Equal($"The type \"{typeof(BaseConfig)}\" does not have property \"Other\".", ex.Message);
        }

        [Fact]
        public void Foreign_Key_Without_Primary_Key()
        {
            tableBuilder.Add<Employee>();

            tableBuilder.Add<Department>()
                .Ignore(x => x.Id);

            Exception ex = Assert.Throws<InvalidConfigurationException>(() => tableBuilder.GetConfig());
            Assert.Equal($"Foreign key property not specified for property \"Department\" of the type "
                + $"\"{typeof(Employee)}\", and the type \"{typeof(Department)}\" does not have a primary key.", ex.Message);
        }

        [Fact]
        public void Foreign_Key_Composite_One_Name()
        {
            tableBuilder.Add<Employee>()
                .ForeignKey(x => x.Department, "DeptId");

            tableBuilder.Add<Department>()
                .PrimaryKey(x => x.Id)
                .PrimaryKey(x => x.Guid);

            Exception ex = Assert.Throws<InvalidConfigurationException>(() => tableBuilder.GetConfig());
            Assert.Equal($"Foreign key property not specified for property \"Department\" of the type "
                + $"\"{typeof(Employee)}\", and the type \"{typeof(Department)}\" have multiple primary keys.", ex.Message);
        }
    }
}