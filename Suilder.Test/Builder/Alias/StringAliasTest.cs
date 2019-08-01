using Suilder.Builder;
using Suilder.Core;
using Xunit;

namespace Suilder.Test.Builder.Alias
{
    public class StringAliasTest : BaseTest
    {
        [Fact]
        public void Without_Alias()
        {
            IAlias alias = sql.Alias("person");

            QueryResult result = engine.Compile(alias);

            Assert.Equal("\"person\"", result.Sql);
        }

        [Fact]
        public void With_Alias()
        {
            IAlias alias = sql.Alias("person", "per");

            QueryResult result = engine.Compile(alias);

            Assert.Equal("\"person\"", result.Sql);
        }

        [Fact]
        public void With_Schema()
        {
            IAlias alias = sql.Alias("dbo.person");

            QueryResult result = engine.Compile(alias);

            Assert.Equal("\"dbo\".\"person\"", result.Sql);
        }

        [Fact]
        public void To_String()
        {
            IAlias alias = sql.Alias("person", "per");

            Assert.Equal("person AS per", alias.ToString());
        }
    }
}