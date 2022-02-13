using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Xunit;

namespace Suilder.Test.Builder.Alias.StringAlias
{
    public class AliasSchemaTest : BuilderBaseTest
    {
        [Fact]
        public void Alias()
        {
            IAlias alias = sql.Alias("dbo.person");

            QueryResult result = engine.Compile(alias);

            Assert.Equal("\"dbo\".\"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Alias_With_Alias_Name()
        {
            IAlias alias = sql.Alias("dbo.person", "per");

            QueryResult result = engine.Compile(alias);

            Assert.Equal("\"dbo\".\"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void AliasOrTableName_Property()
        {
            IAlias alias = sql.Alias("dbo.person");

            Assert.Equal("dbo.person", alias.AliasOrTableName);
        }

        [Fact]
        public void To_String()
        {
            IAlias alias = sql.Alias("dbo.person");

            Assert.Equal("dbo.person", alias.ToString());
        }

        [Fact]
        public void To_String_With_Alias_Name()
        {
            IAlias alias = sql.Alias("dbo.person", "per");

            Assert.Equal("dbo.person", alias.ToString());
        }
    }
}