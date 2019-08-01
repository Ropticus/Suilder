using Suilder.Builder;
using Suilder.Core;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder
{
    public class FromTest : BaseTest
    {
        [Fact]
        public void From_String()
        {
            IFrom from = sql.From("person");

            QueryResult result = engine.Compile(from);

            Assert.Equal("FROM \"person\"", result.Sql);
        }

        [Fact]
        public void From_String_With_Alias()
        {
            IFrom from = sql.From("person", "per");

            QueryResult result = engine.Compile(from);

            Assert.Equal("FROM \"person\" AS \"per\"", result.Sql);
        }

        [Fact]
        public void From_StringAlias()
        {
            IAlias person = sql.Alias("person");
            IFrom from = sql.From(person);

            QueryResult result = engine.Compile(from);

            Assert.Equal("FROM \"person\"", result.Sql);
        }

        [Fact]
        public void From_StringAlias_With_Alias()
        {
            IAlias person = sql.Alias("person", "per");
            IFrom from = sql.From(person);

            QueryResult result = engine.Compile(from);

            Assert.Equal("FROM \"person\" AS \"per\"", result.Sql);
        }

        [Fact]
        public void From_TypedAlias()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IFrom from = sql.From(person);

            QueryResult result = engine.Compile(from);

            Assert.Equal("FROM \"Person\" AS \"person\"", result.Sql);
        }

        [Fact]
        public void From_TypedAlias_With_Alias()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IFrom from = sql.From(person);

            QueryResult result = engine.Compile(from);

            Assert.Equal("FROM \"Person\" AS \"per\"", result.Sql);
        }

        [Fact]
        public void From_Expression()
        {
            Person person = null;
            IFrom from = sql.From(() => person);

            QueryResult result = engine.Compile(from);

            Assert.Equal("FROM \"Person\" AS \"person\"", result.Sql);
        }

        [Fact]
        public void From_Subquery_String()
        {
            IFrom from = sql.From(sql.RawQuery("Subquery"), "sub");

            QueryResult result = engine.Compile(from);

            Assert.Equal("FROM (Subquery) AS \"sub\"", result.Sql);
        }

        [Fact]
        public void From_Subquery_StringAlias()
        {
            IAlias person = sql.Alias("person");
            IFrom from = sql.From(sql.RawQuery("Subquery"), person);

            QueryResult result = engine.Compile(from);

            Assert.Equal("FROM (Subquery) AS \"person\"", result.Sql);
        }

        [Fact]
        public void From_Subquery_StringAlias_With_Alias()
        {
            IAlias person = sql.Alias("person", "per");
            IFrom from = sql.From(sql.RawQuery("Subquery"), person);

            QueryResult result = engine.Compile(from);

            Assert.Equal("FROM (Subquery) AS \"per\"", result.Sql);
        }

        [Fact]
        public void From_Subquery_Expression()
        {
            Person person = null;
            IFrom from = sql.From(sql.RawQuery("Subquery"), () => person);

            QueryResult result = engine.Compile(from);

            Assert.Equal("FROM (Subquery) AS \"person\"", result.Sql);
        }

        [Fact]
        public void From_Cte()
        {
            IAlias person = sql.Alias("person");
            IAlias personCte = sql.Alias("personCte");
            ICte cte = sql.Cte("cte").As(sql.Query.Select(person.All).From(person));
            IFrom from = sql.From(cte, personCte);

            QueryResult result = engine.Compile(from);

            Assert.Equal("FROM \"cte\" AS \"personCte\"", result.Sql);
        }

        [Fact]
        public void From_Cte_Expression()
        {
            Person person = null;
            Person personCte = null;
            ICte cte = sql.Cte("cte").As(sql.Query.Select(() => person).From(() => person));
            IFrom from = sql.From(cte, () => personCte);

            QueryResult result = engine.Compile(from);

            Assert.Equal("FROM \"cte\" AS \"personCte\"", result.Sql);
        }

        [Fact]
        public void Options()
        {
            Person person = null;
            IFrom from = sql.From(() => person).Options(sql.Raw("WITH (NO LOCK)"));

            QueryResult result = engine.Compile(from);

            Assert.Equal("FROM \"Person\" AS \"person\" WITH (NO LOCK)", result.Sql);
        }

        [Fact]
        public void To_String()
        {
            IAlias person = sql.Alias("person", "per");
            IFrom from = sql.From(person);

            Assert.Equal("FROM person AS per", from.ToString());
        }
    }
}