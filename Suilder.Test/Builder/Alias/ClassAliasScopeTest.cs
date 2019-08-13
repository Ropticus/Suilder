using Suilder.Builder;
using Suilder.Core;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Alias
{
    public class ClassAliasScopeTest : BaseTest
    {
        private static Person person = null;

        public static Department Dept { get; set; }

        [Fact]
        public void Field()
        {
            IAlias alias = sql.Alias(() => person);

            QueryResult result = engine.Compile(alias);

            Assert.Equal("\"Person\"", result.Sql);
        }

        [Fact]
        public void Property()
        {
            IAlias alias = sql.Alias(() => Dept);

            QueryResult result = engine.Compile(alias);

            Assert.Equal("\"Dept\"", result.Sql);
        }
    }
}