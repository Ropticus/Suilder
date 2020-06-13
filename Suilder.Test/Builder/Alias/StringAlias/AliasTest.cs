using Suilder.Builder;
using Suilder.Core;
using Xunit;

namespace Suilder.Test.Builder.Alias.StringAlias
{
    public class AliasTest : BuilderBaseTest
    {
        [Fact]
        public void Alias()
        {
            IAlias alias = sql.Alias("person");

            QueryResult result = engine.Compile(alias);

            Assert.Equal("\"person\"", result.Sql);
        }

        [Fact]
        public void Alias_With_Alias_Name()
        {
            IAlias alias = sql.Alias("person", "per");

            QueryResult result = engine.Compile(alias);

            Assert.Equal("\"person\"", result.Sql);
        }

        [Fact]
        public void Alias_With_Schema()
        {
            IAlias alias = sql.Alias("dbo.person");

            QueryResult result = engine.Compile(alias);

            Assert.Equal("\"dbo\".\"person\"", result.Sql);
        }

        [Fact]
        public void AliasOrTableName_Property()
        {
            IAlias alias = sql.Alias("person");

            Assert.Equal("person", alias.AliasOrTableName);
        }

        [Fact]
        public void AliasOrTableName_Property_With_Alias_Name()
        {
            IAlias alias = sql.Alias("person", "per");

            Assert.Equal("per", alias.AliasOrTableName);
        }

        [Fact]
        public void To_String()
        {
            IAlias alias = sql.Alias("person");

            Assert.Equal("person", alias.ToString());
        }

        [Fact]
        public void To_String_With_Alias_Name()
        {
            IAlias alias = sql.Alias("person", "per");

            Assert.Equal("person AS per", alias.ToString());
        }
    }
}