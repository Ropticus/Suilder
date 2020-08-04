using Suilder.Builder;
using Suilder.Core;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Query
{
    public class JoinSchemaTest : BuilderBaseTest
    {
        [Fact]
        public void Join_String()
        {
            IQuery query = sql.Query.Join("dbo.person");

            QueryResult result = engine.Compile(query);

            Assert.Equal("INNER JOIN \"dbo\".\"person\"", result.Sql);
        }

        [Fact]
        public void Join_String_With_Alias_Name()
        {
            IQuery query = sql.Query.Join("dbo.person", "per");

            QueryResult result = engine.Compile(query);

            Assert.Equal("INNER JOIN \"dbo\".\"person\" AS \"per\"", result.Sql);
        }

        [Fact]
        public void Join_Alias()
        {
            IAlias person = sql.Alias("dbo.person");
            IQuery query = sql.Query.Join(person);

            QueryResult result = engine.Compile(query);

            Assert.Equal("INNER JOIN \"dbo\".\"person\"", result.Sql);
        }

        [Fact]
        public void Join_Alias_With_Alias_Name()
        {
            IAlias person = sql.Alias("dbo.person", "per");
            IQuery query = sql.Query.Join(person);

            QueryResult result = engine.Compile(query);

            Assert.Equal("INNER JOIN \"dbo\".\"person\" AS \"per\"", result.Sql);
        }

        [Fact]
        public void Join_Typed_Alias()
        {
            IAlias<Person2> person = sql.Alias<Person2>();
            IQuery query = sql.Query.Join(person);

            QueryResult result = engine.Compile(query);

            Assert.Equal("INNER JOIN \"dbo\".\"Person\" AS \"person2\"", result.Sql);
        }

        [Fact]
        public void Join_Expression()
        {
            Person2 person = null;
            IQuery query = sql.Query.Join(() => person);

            QueryResult result = engine.Compile(query);

            Assert.Equal("INNER JOIN \"dbo\".\"Person\" AS \"person\"", result.Sql);
        }
    }
}