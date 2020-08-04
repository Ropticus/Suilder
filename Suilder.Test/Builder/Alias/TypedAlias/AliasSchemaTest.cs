using Suilder.Builder;
using Suilder.Core;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Alias.TypedAlias
{
    public class AliasSchemaTest : BuilderBaseTest
    {
        [Fact]
        public void Alias()
        {
            IAlias<Person2> alias = sql.Alias<Person2>();

            QueryResult result = engine.Compile(alias);

            Assert.Equal("\"dbo\".\"Person\"", result.Sql);
        }

        [Fact]
        public void Alias_With_Translation()
        {
            IAlias<Department2> alias = sql.Alias<Department2>();

            QueryResult result = engine.Compile(alias);

            Assert.Equal("\"dbo\".\"Dept\"", result.Sql);
        }

        [Fact]
        public void Alias_With_Alias_Name()
        {
            IAlias<Person2> alias = sql.Alias<Person2>("per");

            QueryResult result = engine.Compile(alias);

            Assert.Equal("\"dbo\".\"Person\"", result.Sql);
        }

        [Fact]
        public void Alias_With_Translation_With_Alias_Name()
        {
            IAlias<Department2> alias = sql.Alias<Department2>("dept");

            QueryResult result = engine.Compile(alias);

            Assert.Equal("\"dbo\".\"Dept\"", result.Sql);
        }

        [Fact]
        public void AliasOrTableName_Property()
        {
            IAlias<Person2> alias = sql.Alias<Person2>();

            Assert.Equal("person2", alias.AliasOrTableName);
        }

        [Fact]
        public void AliasOrTableName_PropertyWith_Alias_Name()
        {
            IAlias<Person2> alias = sql.Alias<Person2>("per");

            Assert.Equal("per", alias.AliasOrTableName);
        }

        [Fact]
        public void To_String()
        {
            IAlias<Person2> alias = sql.Alias<Person2>();

            Assert.Equal("Person2 AS person2", alias.ToString());
        }

        [Fact]
        public void To_String_With_Alias_Name()
        {
            IAlias<Person2> alias = sql.Alias<Person2>("per");

            Assert.Equal("Person2 AS per", alias.ToString());
        }
    }
}