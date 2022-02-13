using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Alias.TypedAlias
{
    public class AliasTest : BuilderBaseTest
    {
        [Fact]
        public void Alias()
        {
            IAlias<Person> alias = sql.Alias<Person>();

            QueryResult result = engine.Compile(alias);

            Assert.Equal("\"Person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Alias_With_Translation()
        {
            IAlias<Department> alias = sql.Alias<Department>();

            QueryResult result = engine.Compile(alias);

            Assert.Equal("\"Dept\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Alias_With_Alias_Name()
        {
            IAlias<Person> alias = sql.Alias<Person>("per");

            QueryResult result = engine.Compile(alias);

            Assert.Equal("\"Person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Alias_With_Translation_With_Alias_Name()
        {
            IAlias<Department> alias = sql.Alias<Department>("dept");

            QueryResult result = engine.Compile(alias);

            Assert.Equal("\"Dept\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void AliasOrTableName_Property()
        {
            IAlias<Person> alias = sql.Alias<Person>();

            Assert.Equal("person", alias.AliasOrTableName);
        }

        [Fact]
        public void AliasOrTableName_Property_With_Alias_Name()
        {
            IAlias<Person> alias = sql.Alias<Person>("per");

            Assert.Equal("per", alias.AliasOrTableName);
        }

        [Fact]
        public void To_String()
        {
            IAlias<Person> alias = sql.Alias<Person>();

            Assert.Equal("Person", alias.ToString());
        }

        [Fact]
        public void To_String_With_Alias_Name()
        {
            IAlias<Person> alias = sql.Alias<Person>("per");

            Assert.Equal("Person", alias.ToString());
        }
    }
}