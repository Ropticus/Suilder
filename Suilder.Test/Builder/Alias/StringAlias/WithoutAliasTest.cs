using Suilder.Builder;
using Suilder.Core;
using Xunit;

namespace Suilder.Test.Builder.Alias.StringAlias
{
    public class WithoutAliasTest : BuilderBaseTest
    {
        [Fact]
        public void Col_All_One_Param()
        {
            IColumn column = sql.Col("person.*");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".*", result.Sql);
        }

        [Fact]
        public void Col_Column_One_Param()
        {
            IColumn column = sql.Col("person.Id");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\"", result.Sql);
        }

        [Fact]
        public void Col_All_Two_Params()
        {
            IColumn column = sql.Col("person", "*");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".*", result.Sql);
        }

        [Fact]
        public void Col_Column_Two_Params()
        {
            IColumn column = sql.Col("person", "Id");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\"", result.Sql);
        }

        [Fact]
        public void Col_All_Without_Table_Name()
        {
            IColumn column = sql.Col("*");

            QueryResult result = engine.Compile(column);

            Assert.Equal("*", result.Sql);
        }

        [Fact]
        public void Col_Column_Without_Table_Name()
        {
            IColumn column = sql.Col("Id");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"Id\"", result.Sql);
        }

        [Fact]
        public void Escape_Characters()
        {
            engine.Options.EscapeStart = '[';
            engine.Options.EscapeEnd = ']';

            IColumn column = sql.Col("person.Id");

            QueryResult result = engine.Compile(column);

            Assert.Equal("[person].[Id]", result.Sql);
        }
    }
}