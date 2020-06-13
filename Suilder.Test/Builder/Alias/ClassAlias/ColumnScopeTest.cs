using Suilder.Builder;
using Suilder.Core;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Alias.ClassAlias
{
    public class ColumnScopeTest : BuilderBaseTest
    {
        public Person personField;

        public Person PersonProperty { get; set; }

        public static Person personFieldStatic;

        public static Person PersonPropertyStatic { get; set; }

        [Fact]
        public void Field()
        {
            IColumn column = sql.Col(() => personField.Id);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"personField\".\"Id\"", result.Sql);
        }

        [Fact]
        public void Property()
        {
            IColumn column = sql.Col(() => PersonProperty.Id);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"PersonProperty\".\"Id\"", result.Sql);
        }

        [Fact]
        public void Field_Static()
        {
            IColumn column = sql.Col(() => personFieldStatic.Id);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"personFieldStatic\".\"Id\"", result.Sql);
        }

        [Fact]
        public void Property_Static()
        {
            IColumn column = sql.Col(() => PersonPropertyStatic.Id);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"PersonPropertyStatic\".\"Id\"", result.Sql);
        }
    }
}