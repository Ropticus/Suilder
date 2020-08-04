using Suilder.Builder;
using Suilder.Core;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Query
{
    public class FromSchemaTest : BuilderBaseTest
    {
        [Fact]
        public void From_String()
        {
            IQuery query = sql.Query.From("dbo.person");

            QueryResult result = engine.Compile(query);

            Assert.Equal("FROM \"dbo\".\"person\"", result.Sql);
        }

        [Fact]
        public void From_String_With_Alias_Name()
        {
            IQuery query = sql.Query.From("dbo.person", "per");

            QueryResult result = engine.Compile(query);

            Assert.Equal("FROM \"dbo\".\"person\" AS \"per\"", result.Sql);
        }

        [Fact]
        public void From_Alias()
        {
            IAlias person = sql.Alias("dbo.person");
            IQuery query = sql.Query.From(person);

            QueryResult result = engine.Compile(query);

            Assert.Equal("FROM \"dbo\".\"person\"", result.Sql);
        }

        [Fact]
        public void From_Alias_With_Alias_Name()
        {
            IAlias person = sql.Alias("dbo.person", "per");
            IQuery query = sql.Query.From(person);

            QueryResult result = engine.Compile(query);

            Assert.Equal("FROM \"dbo\".\"person\" AS \"per\"", result.Sql);
        }

        [Fact]
        public void From_Typed_Alias()
        {
            IAlias<Person2> person = sql.Alias<Person2>();
            IQuery query = sql.Query.From(person);

            QueryResult result = engine.Compile(query);

            Assert.Equal("FROM \"dbo\".\"Person\" AS \"person2\"", result.Sql);
        }

        [Fact]
        public void From_Typed_Alias_With_Alias_Name()
        {
            IAlias<Person2> person = sql.Alias<Person2>("per");
            IQuery query = sql.Query.From(person);

            QueryResult result = engine.Compile(query);

            Assert.Equal("FROM \"dbo\".\"Person\" AS \"per\"", result.Sql);
        }

        [Fact]
        public void From_Expression()
        {
            Person2 person = null;
            IQuery query = sql.Query.From(() => person);

            QueryResult result = engine.Compile(query);

            Assert.Equal("FROM \"dbo\".\"Person\" AS \"person\"", result.Sql);
        }
    }
}