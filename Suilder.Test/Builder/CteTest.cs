using Suilder.Builder;
using Suilder.Core;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder
{
    public class CteTest : BaseTest
    {
        [Fact]
        public void Cte()
        {
            IAlias person = sql.Alias("person");
            ICte cte = sql.Cte("cte").As(sql.Query.Select(person["Id"], person["Name"]).From(person));

            QueryResult result = engine.Compile(cte);

            Assert.Equal("\"cte\" AS (SELECT \"person\".\"Id\", \"person\".\"Name\" FROM \"person\")",
                result.Sql);
        }

        [Fact]
        public void Cte_With_Columns()
        {
            IAlias person = sql.Alias("person");
            ICte cte = sql.Cte("cte").Add(person["Id"], person["Name"])
                .As(sql.Query.Select(person["Id"], person["Name"]).From(person));

            QueryResult result = engine.Compile(cte);

            Assert.Equal("\"cte\" (\"Id\", \"Name\") AS (SELECT \"person\".\"Id\", \"person\".\"Name\" "
                + "FROM \"person\")", result.Sql);
        }

        [Fact]
        public void Cte_With_Columns_Expression()
        {
            Person person = null;
            ICte cte = sql.Cte("cte").Add(() => person.Id, () => person.Name)
                .As(sql.Query.Select(() => person.Id, () => person.Name).From(() => person));

            QueryResult result = engine.Compile(cte);

            Assert.Equal("\"cte\" (\"Id\", \"Name\") AS (SELECT \"person\".\"Id\", \"person\".\"Name\" "
                + "FROM \"Person\" AS \"person\")", result.Sql);
        }

        [Fact]
        public void Cte_ToString()
        {
            IAlias person = sql.Alias("person");
            ICte cte = sql.Cte("cte").As(sql.Query.Select(person["Id"], person["Name"]).From(person));

            Assert.Equal("cte AS (SELECT person.Id, person.Name FROM person)", cte.ToString());
        }
    }
}