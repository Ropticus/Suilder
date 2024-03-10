using System;
using Suilder.Exceptions;
using Suilder.Reflection.Builder;
using Suilder.Reflection.Builder.Processors;
using Suilder.Test.Reflection.TablePerType.Tables;
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
        public void Primary_Key_Not_Exists()
        {
            tableBuilder.DefaultPrimaryKey(x => "Other");

            tableBuilder.Add<Person>();

            Exception ex = Assert.Throws<InvalidConfigurationException>(() => tableBuilder.GetConfig());
            Assert.Equal($"The type \"{typeof(BaseConfig)}\" does not have property \"Other\".", ex.Message);
        }

        [Fact]
        public void Primary_Key_Not_Column()
        {
            tableBuilder.Add<Department>()
                .PrimaryKey(x => x.Tags.Count);

            Exception ex = Assert.Throws<InvalidConfigurationException>(() => tableBuilder.GetConfig());
            Assert.Equal($"Primary key does not exists as a column for property \"Tags.Count\" of the type "
                + $"\"{typeof(Department)}\".", ex.Message);
        }

        [Fact]
        public void Primary_Key_Not_Foreign_Key()
        {
            tableBuilder.Add<Employee>()
                .PrimaryKey(x => x.Department.Guid);

            Exception ex = Assert.Throws<InvalidConfigurationException>(() => tableBuilder.GetConfig());
            Assert.Equal($"Primary key does not exists as a column for property \"Department.Guid\" of the type "
                + $"\"{typeof(Employee)}\".", ex.Message);
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
            Assert.Equal($"Foreign key property not specified for column name of property \"Department\" of the type "
                + $"\"{typeof(Employee)}\", and the property have multiple foreign keys.", ex.Message);
        }

        [Fact]
        public void Table_Name_Empty()
        {
            tableBuilder.DefaultTableName(x => "");

            tableBuilder.Add<Person>();

            Exception ex = Assert.Throws<InvalidConfigurationException>(() => tableBuilder.GetConfig());
            Assert.Equal($"Empty table name for type \"{typeof(Person)}\".", ex.Message);
        }

        [Fact]
        public void Columns_Empty()
        {
            tableBuilder.Add<Person>()
                .InheritColumns(false)
                .Ignore(x => x.Id)
                .Ignore(x => x.Surname)
                .Ignore(x => x.Address);

            Exception ex = Assert.Throws<InvalidConfigurationException>(() => tableBuilder.GetConfig());
            Assert.Equal($"Empty columns for type \"{typeof(Person)}\".", ex.Message);
        }

        [Fact]
        public void Column_Name_Empty()
        {
            tableBuilder.Add<Person>()
                .ColumnName(x => x.Name, "");

            Exception ex = Assert.Throws<InvalidConfigurationException>(() => tableBuilder.GetConfig());
            Assert.Equal($"Empty column name for property \"{nameof(Person.Name)}\" of the type \"{typeof(Person)}\".",
                ex.Message);
        }

        [Fact]
        public void Invalid_Primary_Keys_Null()
        {
            tableBuilder.AddProcessor(new InvalidPrimaryKeysNullProcessor());

            tableBuilder.Add<Person>();

            Exception ex = Assert.Throws<InvalidConfigurationException>(() => tableBuilder.GetConfig());
            Assert.Equal($"Null primary keys for type \"{typeof(Person)}\".", ex.Message);
        }

        [Fact]
        public void Invalid_Columns_Null()
        {
            tableBuilder.AddProcessor(new InvalidColumnsNullProcessor());

            tableBuilder.Add<Person>();

            Exception ex = Assert.Throws<InvalidConfigurationException>(() => tableBuilder.GetConfig());
            Assert.Equal($"Null columns for type \"{typeof(Person)}\".", ex.Message);
        }

        [Fact]
        public void Invalid_Columns_Empty()
        {
            tableBuilder.AddProcessor(new InvalidColumnsEmptyProcessor());

            tableBuilder.Add<Person>();

            Exception ex = Assert.Throws<InvalidConfigurationException>(() => tableBuilder.GetConfig());
            Assert.Equal($"Empty columns for type \"{typeof(Person)}\".", ex.Message);
        }

        [Fact]
        public void Invalid_Column_Names_Null()
        {
            tableBuilder.AddProcessor(new InvalidColumnNamesNullProcessor());

            tableBuilder.Add<Person>();

            Exception ex = Assert.Throws<InvalidConfigurationException>(() => tableBuilder.GetConfig());
            Assert.Equal($"Null column names for type \"{typeof(Person)}\".", ex.Message);
        }

        [Fact]
        public void Invalid_Column_Names_Empty()
        {
            tableBuilder.AddProcessor(new InvalidColumnNamesEmptyProcessor());

            tableBuilder.Add<Person>();

            Exception ex = Assert.Throws<InvalidConfigurationException>(() => tableBuilder.GetConfig());
            Assert.Equal($"Empty column names for type \"{typeof(Person)}\".", ex.Message);
        }

        [Fact]
        public void Invalid_Column_Names_Dic_Null()
        {
            tableBuilder.AddProcessor(new InvalidColumnNamesDicNullProcessor());

            tableBuilder.Add<Person>();

            Exception ex = Assert.Throws<InvalidConfigurationException>(() => tableBuilder.GetConfig());
            Assert.Equal($"Null column names dictionary for type \"{typeof(Person)}\".", ex.Message);
        }

        [Fact]
        public void Invalid_Column_Names_Dic_Empty()
        {
            tableBuilder.AddProcessor(new InvalidColumnNamesDicEmptyProcessor());

            tableBuilder.Add<Person>();

            Exception ex = Assert.Throws<InvalidConfigurationException>(() => tableBuilder.GetConfig());
            Assert.Equal($"Empty column names dictionary for type \"{typeof(Person)}\".", ex.Message);
        }

        [Fact]
        public void Invalid_Column_Names_Dic_Key()
        {
            tableBuilder.AddProcessor(new InvalidColumnNamesDicKeyProcessor());

            tableBuilder.Add<Person>();

            Exception ex = Assert.Throws<InvalidConfigurationException>(() => tableBuilder.GetConfig());
            Assert.Equal($"Empty column name for property \"{nameof(Person.Name)}\" of the type \"{typeof(Person)}\".",
                ex.Message);
        }

        [Fact]
        public void Invalid_Foreign_Keys_Null()
        {
            tableBuilder.AddProcessor(new InvalidForeignKeysNullProcessor());

            tableBuilder.Add<Person>();

            Exception ex = Assert.Throws<InvalidConfigurationException>(() => tableBuilder.GetConfig());
            Assert.Equal($"Null foreign keys for type \"{typeof(Person)}\".", ex.Message);
        }

        [Fact]
        public void Invalid_Table_Metadata_Null()
        {
            tableBuilder.AddProcessor(new InvalidTableMetadataNullProcessor());

            tableBuilder.Add<Person>();

            Exception ex = Assert.Throws<InvalidConfigurationException>(() => tableBuilder.GetConfig());
            Assert.Equal($"Null table metadata for type \"{typeof(Person)}\".", ex.Message);
        }

        [Fact]
        public void Invalid_Member_Metadata_Null()
        {
            tableBuilder.AddProcessor(new InvalidMemberMetadataNullProcessor());

            tableBuilder.Add<Person>();

            Exception ex = Assert.Throws<InvalidConfigurationException>(() => tableBuilder.GetConfig());
            Assert.Equal($"Null member metadata for type \"{typeof(Person)}\".", ex.Message);
        }

        private class InvalidPrimaryKeysNullProcessor : BaseConfigProcessor
        {
            protected override void ProcessData()
            {
                foreach (TableInfo tableInfo in ResultData.ResultTypes.Values)
                {
                    tableInfo.PrimaryKeys = null;
                }
            }
        }

        private class InvalidColumnsNullProcessor : BaseConfigProcessor
        {
            protected override void ProcessData()
            {
                foreach (TableInfo tableInfo in ResultData.ResultTypes.Values)
                {
                    tableInfo.Columns = null;
                }
            }
        }

        private class InvalidColumnsEmptyProcessor : BaseConfigProcessor
        {
            protected override void ProcessData()
            {
                foreach (TableInfo tableInfo in ResultData.ResultTypes.Values)
                {
                    tableInfo.Columns.Clear();
                }
            }
        }

        private class InvalidColumnNamesNullProcessor : BaseConfigProcessor
        {
            protected override void ProcessData()
            {
                foreach (TableInfo tableInfo in ResultData.ResultTypes.Values)
                {
                    tableInfo.ColumnNames = null;
                }
            }
        }

        private class InvalidColumnNamesEmptyProcessor : BaseConfigProcessor
        {
            protected override void ProcessData()
            {
                foreach (TableInfo tableInfo in ResultData.ResultTypes.Values)
                {
                    tableInfo.ColumnNames.Clear();
                }
            }
        }

        private class InvalidColumnNamesDicNullProcessor : BaseConfigProcessor
        {
            protected override void ProcessData()
            {
                foreach (TableInfo tableInfo in ResultData.ResultTypes.Values)
                {
                    tableInfo.ColumnNamesDic = null;
                }
            }
        }

        private class InvalidColumnNamesDicEmptyProcessor : BaseConfigProcessor
        {
            protected override void ProcessData()
            {
                foreach (TableInfo tableInfo in ResultData.ResultTypes.Values)
                {
                    tableInfo.ColumnNamesDic.Clear();
                }
            }
        }

        private class InvalidColumnNamesDicKeyProcessor : BaseConfigProcessor
        {
            protected override void ProcessData()
            {
                foreach (TableInfo tableInfo in ResultData.ResultTypes.Values)
                {
                    tableInfo.ColumnNamesDic.Remove(nameof(Person.Name));
                }
            }
        }

        private class InvalidForeignKeysNullProcessor : BaseConfigProcessor
        {
            protected override void ProcessData()
            {
                foreach (TableInfo tableInfo in ResultData.ResultTypes.Values)
                {
                    tableInfo.ForeignKeys = null;
                }
            }
        }

        private class InvalidTableMetadataNullProcessor : BaseConfigProcessor
        {
            protected override void ProcessData()
            {
                foreach (TableInfo tableInfo in ResultData.ResultTypes.Values)
                {
                    tableInfo.TableMetadata = null;
                }
            }
        }

        private class InvalidMemberMetadataNullProcessor : BaseConfigProcessor
        {
            protected override void ProcessData()
            {
                foreach (TableInfo tableInfo in ResultData.ResultTypes.Values)
                {
                    tableInfo.MemberMetadata = null;
                }
            }
        }
    }
}