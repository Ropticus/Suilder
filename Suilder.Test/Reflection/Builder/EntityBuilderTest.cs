using System;
using Suilder.Test.Reflection.TablePerType.Tables;
using Xunit;

namespace Suilder.Test.Reflection.Builder
{
    public class EntityBuilderTest : BaseTest
    {
        [Fact]
        public void Primary_Key_Not_Exists()
        {
            Exception ex = Assert.Throws<ArgumentException>(() => tableBuilder.Add<Person>().PrimaryKey("Other"));
            Assert.Equal($"The type \"{typeof(Person)}\" does not have property \"Other\". (Parameter 'propertyName')",
                ex.Message);
        }

        [Fact]
        public void Foreign_Key_Not_Exists()
        {
            Exception ex = Assert.Throws<ArgumentException>(() => tableBuilder.Add<Person>().ForeignKey("Other"));
            Assert.Equal($"The type \"{typeof(Person)}\" does not have property \"Other\". (Parameter 'propertyName')",
                ex.Message);
        }

        [Fact]
        public void Foreign_Key_With_Name_Not_Exists()
        {
            Exception ex = Assert.Throws<ArgumentException>(() => tableBuilder.Add<Person>().ForeignKey("Other", "Other2"));
            Assert.Equal($"The type \"{typeof(Person)}\" does not have property \"Other\". (Parameter 'propertyName')",
                ex.Message);
        }

        [Fact]
        public void Foreign_Key_With_Name_Partial_Not_Exists()
        {
            Exception ex = Assert.Throws<ArgumentException>(() => tableBuilder.Add<Person>()
                .ForeignKey("Other", "Other2", true));
            Assert.Equal($"The type \"{typeof(Person)}\" does not have property \"Other\". (Parameter 'propertyName')",
                ex.Message);
        }

        [Fact]
        public void Column_Name_Not_Exists()
        {
            Exception ex = Assert.Throws<ArgumentException>(() => tableBuilder.Add<Person>().ColumnName("Other", "Other2"));
            Assert.Equal($"The type \"{typeof(Person)}\" does not have property \"Other\". (Parameter 'propertyName')",
                ex.Message);
        }

        [Fact]
        public void Column_Name_Partial_Not_Exists()
        {
            Exception ex = Assert.Throws<ArgumentException>(() => tableBuilder.Add<Person>()
                .ColumnName("Other", "Other2", true));
            Assert.Equal($"The type \"{typeof(Person)}\" does not have property \"Other\". (Parameter 'propertyName')",
                ex.Message);
        }

        [Fact]
        public void Ignore_Not_Exists()
        {
            Exception ex = Assert.Throws<ArgumentException>(() => tableBuilder.Add<Person>().Ignore("Other"));
            Assert.Equal($"The type \"{typeof(Person)}\" does not have property \"Other\". (Parameter 'propertyName')",
                ex.Message);
        }

        [Fact]
        public void Property_Not_Exists()
        {
            Exception ex = Assert.Throws<ArgumentException>(() => tableBuilder.Add<Person>().Property("Other"));
            Assert.Equal($"The type \"{typeof(Person)}\" does not have property \"Other\". (Parameter 'propertyName')",
                ex.Message);
        }

        [Fact]
        public void Property_Delegate_Not_Exists()
        {
            Exception ex = Assert.Throws<ArgumentException>(() => tableBuilder.Add<Person>().Property("Other", p => p));
            Assert.Equal($"The type \"{typeof(Person)}\" does not have property \"Other\". (Parameter 'propertyName')",
                ex.Message);
        }
    }
}