using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Alias.ClassAlias
{
    public class AliasSchemaTest : BuilderBaseTest
    {
        [Fact]
        public void Expression()
        {
            Person2 person = null;
            IAlias alias = sql.Alias(() => person);

            QueryResult result = engine.Compile(alias);

            Assert.Equal("\"dbo\".\"Person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_With_Translation()
        {
            Department2 dept = null;
            IAlias alias = sql.Alias(() => dept);

            QueryResult result = engine.Compile(alias);

            Assert.Equal("\"dbo\".\"Dept\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void To_String()
        {
            Person2 person = null;
            IAlias alias = sql.Alias(() => person);

            Assert.Equal("Person2", alias.ToString());
        }
    }
}