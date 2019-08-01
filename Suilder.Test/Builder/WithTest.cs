using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Xunit;

namespace Suilder.Test.Builder
{
    public class WithTest : BaseTest
    {
        [Fact]
        public void Add()
        {
            IAlias person = sql.Alias("person");
            IAlias dept = sql.Alias("dept");
            ICte cte1 = sql.Cte("cte1").As(sql.Query.Select(person.All).From(person));
            ICte cte2 = sql.Cte("cte2").As(sql.Query.Select(dept.All).From(dept));
            IWith with = sql.With.Add(cte1).Add(cte2);

            QueryResult result = engine.Compile(with);

            Assert.Equal("WITH \"cte1\" AS (SELECT \"person\".* FROM \"person\"), "
                + "\"cte2\" AS (SELECT \"dept\".* FROM \"dept\")", result.Sql);
        }

        [Fact]
        public void Add_Params()
        {
            IAlias person = sql.Alias("person");
            IAlias dept = sql.Alias("dept");
            ICte cte1 = sql.Cte("cte1").As(sql.Query.Select(person.All).From(person));
            ICte cte2 = sql.Cte("cte2").As(sql.Query.Select(dept.All).From(dept));
            IWith with = sql.With.Add(cte1, cte2);

            QueryResult result = engine.Compile(with);

            Assert.Equal("WITH \"cte1\" AS (SELECT \"person\".* FROM \"person\"), "
                + "\"cte2\" AS (SELECT \"dept\".* FROM \"dept\")", result.Sql);
        }

        [Fact]
        public void Add_Enumerable()
        {
            IAlias person = sql.Alias("person");
            IAlias dept = sql.Alias("dept");
            ICte cte1 = sql.Cte("cte1").As(sql.Query.Select(person.All).From(person));
            ICte cte2 = sql.Cte("cte2").As(sql.Query.Select(dept.All).From(dept));
            IWith with = sql.With.Add(new List<ICte>() { cte1, cte2 });

            QueryResult result = engine.Compile(with);

            Assert.Equal("WITH \"cte1\" AS (SELECT \"person\".* FROM \"person\"), "
                + "\"cte2\" AS (SELECT \"dept\".* FROM \"dept\")", result.Sql);
        }

        [Fact]
        public void With_Recursive()
        {
            engine.Options.WithRecursive = true;

            IAlias person = sql.Alias("person");
            IAlias dept = sql.Alias("dept");
            ICte cte1 = sql.Cte("cte1").As(sql.Query.Select(person.All).From(person));
            ICte cte2 = sql.Cte("cte2").As(sql.Query.Select(dept.All).From(dept));
            IWith with = sql.With.Add(cte1).Add(cte2);

            QueryResult result = engine.Compile(with);

            Assert.Equal("WITH RECURSIVE \"cte1\" AS (SELECT \"person\".* FROM \"person\"), "
                + "\"cte2\" AS (SELECT \"dept\".* FROM \"dept\")", result.Sql);
        }

        [Fact]
        public void With_ToString()
        {
            IAlias person = sql.Alias("person");
            ICte cte = sql.Cte("cte1").As(sql.Query.Select(person["Id"], person["Name"]).From(person));
            IWith with = sql.With.Add(cte);

            Assert.Equal("WITH cte1 AS (SELECT person.Id, person.Name FROM person)", with.ToString());
        }
    }
}