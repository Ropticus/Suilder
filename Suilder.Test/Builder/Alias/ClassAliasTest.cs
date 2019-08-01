using Suilder.Builder;
using Suilder.Core;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Alias
{
    public class ClassAliasTest : BaseTest
    {
        [Fact]
        public void With_Alias()
        {
            Person person = null;
            IAlias alias = sql.Alias(() => person);

            QueryResult result = engine.Compile(alias);

            Assert.Equal("\"Person\"", result.Sql);
        }

        [Fact]
        public void With_Alias_Translation()
        {
            Department dept = null;
            IAlias alias = sql.Alias(() => dept);

            QueryResult result = engine.Compile(alias);

            Assert.Equal("\"Dept\"", result.Sql);
        }

        [Fact]
        public void To_String()
        {
            Person person = null;
            IAlias alias = sql.Alias(() => person);

            Assert.Equal("Person AS person", alias.ToString());
        }
    }
}