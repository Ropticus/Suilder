using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Xunit;

namespace Suilder.Test.Builder.Alias.StringAlias
{
    public class WithoutAliasSchemaTest : BuilderBaseTest
    {
        [Fact]
        public void Col_All_One_Param()
        {
            IColumn column = sql.Col("dbo.person.*");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"dbo\".\"person\".*", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_Column_One_Param()
        {
            IColumn column = sql.Col("dbo.person.Id");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"dbo\".\"person\".\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_All_Two_Params()
        {
            IColumn column = sql.Col("dbo.person", "*");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"dbo\".\"person\".*", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_Column_Two_Params()
        {
            IColumn column = sql.Col("dbo.person", "Id");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"dbo\".\"person\".\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Escape_Characters()
        {
            engine.Options.EscapeStart = '[';
            engine.Options.EscapeEnd = ']';

            IColumn column = sql.Col("dbo.person.Id");

            QueryResult result = engine.Compile(column);

            Assert.Equal("[dbo].[person].[Id]", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }
    }
}