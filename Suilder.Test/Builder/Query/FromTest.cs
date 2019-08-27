using Suilder.Builder;
using Suilder.Core;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Query
{
    public class FromTest : BuilderBaseTest
    {
        [Fact]
        public void From_String()
        {
            IQuery query = sql.Query.From("person");

            QueryResult result = engine.Compile(query);

            Assert.Equal("FROM \"person\"", result.Sql);
        }

        [Fact]
        public void From_String_With_Alias_Name()
        {
            IQuery query = sql.Query.From("person", "per");

            QueryResult result = engine.Compile(query);

            Assert.Equal("FROM \"person\" AS \"per\"", result.Sql);
        }

        [Fact]
        public void From_Alias()
        {
            IAlias person = sql.Alias("person");
            IQuery query = sql.Query.From(person);

            QueryResult result = engine.Compile(query);

            Assert.Equal("FROM \"person\"", result.Sql);
        }

        [Fact]
        public void From_Alias_With_Alias_Name()
        {
            IAlias person = sql.Alias("person", "per");
            IQuery query = sql.Query.From(person);

            QueryResult result = engine.Compile(query);

            Assert.Equal("FROM \"person\" AS \"per\"", result.Sql);
        }

        [Fact]
        public void From_Typed_Alias()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IQuery query = sql.Query.From(person);

            QueryResult result = engine.Compile(query);

            Assert.Equal("FROM \"Person\" AS \"person\"", result.Sql);
        }

        [Fact]
        public void From_Typed_Alias_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IQuery query = sql.Query.From(person);

            QueryResult result = engine.Compile(query);

            Assert.Equal("FROM \"Person\" AS \"per\"", result.Sql);
        }

        [Fact]
        public void From_Expression()
        {
            Person person = null;
            IQuery query = sql.Query.From(() => person);

            QueryResult result = engine.Compile(query);

            Assert.Equal("FROM \"Person\" AS \"person\"", result.Sql);
        }

        [Fact]
        public void From_Subquery_String()
        {
            IQuery query = sql.Query.From(sql.RawQuery("Subquery"), "sub");

            QueryResult result = engine.Compile(query);

            Assert.Equal("FROM (Subquery) AS \"sub\"", result.Sql);
        }

        [Fact]
        public void From_Subquery_Alias()
        {
            IAlias person = sql.Alias("person");
            IQuery query = sql.Query.From(sql.RawQuery("Subquery"), person);

            QueryResult result = engine.Compile(query);

            Assert.Equal("FROM (Subquery) AS \"person\"", result.Sql);
        }

        [Fact]
        public void From_Subquery_Alias_With_Alias_Name()
        {
            IAlias person = sql.Alias("person", "per");
            IQuery query = sql.Query.From(sql.RawQuery("Subquery"), person);

            QueryResult result = engine.Compile(query);

            Assert.Equal("FROM (Subquery) AS \"per\"", result.Sql);
        }

        [Fact]
        public void From_Subquery_Expression()
        {
            Person person = null;
            IQuery query = sql.Query.From(sql.RawQuery("Subquery"), () => person);

            QueryResult result = engine.Compile(query);

            Assert.Equal("FROM (Subquery) AS \"person\"", result.Sql);
        }

        [Fact]
        public void Options()
        {
            Person person = null;
            IQuery query = sql.Query.From(() => person).Options(sql.Raw("WITH (NO LOCK)"));

            QueryResult result = engine.Compile(query);

            Assert.Equal("FROM \"Person\" AS \"person\" WITH (NO LOCK)", result.Sql);
        }

        [Fact]
        public void From_Value()
        {
            IAlias person = sql.Alias("person");
            IQuery query = sql.Query.From(sql.From(person));

            QueryResult result = engine.Compile(query);

            Assert.Equal("FROM \"person\"", result.Sql);
        }

        [Fact]
        public void From_Raw()
        {
            IAlias person = sql.Alias("person");
            IQuery query = sql.Query.From(sql.Raw("FROM {0}", person));

            QueryResult result = engine.Compile(query);

            Assert.Equal("FROM \"person\"", result.Sql);
        }

        [Fact]
        public void From_Dummy_Empty()
        {
            IQuery query = sql.Query.From(sql.FromDummy);

            QueryResult result = engine.Compile(query);

            Assert.Equal("", result.Sql);
        }

        [Fact]
        public void From_Dummy_Value()
        {
            engine.Options.FromDummyName = "DUAL";

            IQuery query = sql.Query.From(sql.FromDummy);

            QueryResult result = engine.Compile(query);

            Assert.Equal("FROM DUAL", result.Sql);
        }
    }
}