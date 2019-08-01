using Suilder.Builder;
using Suilder.Core;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Alias
{
    public class TypedAliasTest : BaseTest
    {
        [Fact]
        public void Default_Alias()
        {
            IAlias<Person> alias = sql.Alias<Person>();

            QueryResult result = engine.Compile(alias);

            Assert.Equal("\"Person\"", result.Sql);
        }

        [Fact]
        public void Default_Alias_Translation()
        {
            IAlias<Department> alias = sql.Alias<Department>();

            QueryResult result = engine.Compile(alias);

            Assert.Equal("\"Dept\"", result.Sql);
        }

        [Fact]
        public void With_Alias()
        {
            IAlias<Person> alias = sql.Alias<Person>("per");

            QueryResult result = engine.Compile(alias);

            Assert.Equal("\"Person\"", result.Sql);
        }

        [Fact]
        public void With_Alias_Translation()
        {
            IAlias<Department> alias = sql.Alias<Department>("dept");

            QueryResult result = engine.Compile(alias);

            Assert.Equal("\"Dept\"", result.Sql);
        }

        [Fact]
        public void To_String()
        {
            IAlias<Person> alias = sql.Alias<Person>();

            Assert.Equal("Person AS person", alias.ToString());
        }
    }
}