using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Xunit;

namespace Suilder.Test.Builder.With
{
    public class WithTest : BuilderBaseTest
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
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
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
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Add_Enumerable()
        {
            IAlias person = sql.Alias("person");
            IAlias dept = sql.Alias("dept");
            ICte cte1 = sql.Cte("cte1").As(sql.Query.Select(person.All).From(person));
            ICte cte2 = sql.Cte("cte2").As(sql.Query.Select(dept.All).From(dept));
            IWith with = sql.With.Add(new List<ICte> { cte1, cte2 });

            QueryResult result = engine.Compile(with);

            Assert.Equal("WITH \"cte1\" AS (SELECT \"person\".* FROM \"person\"), "
                + "\"cte2\" AS (SELECT \"dept\".* FROM \"dept\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Add_One_Value()
        {
            IAlias person = sql.Alias("person");
            ICte cte1 = sql.Cte("cte1").As(sql.Query.Select(person.All).From(person));
            IWith with = sql.With.Add(cte1);

            QueryResult result = engine.Compile(with);

            Assert.Equal("WITH \"cte1\" AS (SELECT \"person\".* FROM \"person\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
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
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void To_String()
        {
            IAlias person = sql.Alias("person");
            IAlias dept = sql.Alias("dept");
            ICte cte1 = sql.Cte("cte1").As(sql.Query.Select(person.All).From(person));
            ICte cte2 = sql.Cte("cte2").As(sql.Query.Select(dept.All).From(dept));
            IWith with = sql.With.Add(cte1).Add(cte2);

            Assert.Equal("WITH cte1 AS (SELECT person.* FROM person), cte2 AS (SELECT dept.* FROM dept)", with.ToString());
        }

        [Fact]
        public void To_String_One_Value()
        {
            IAlias person = sql.Alias("person");
            ICte cte = sql.Cte("cte1").As(sql.Query.Select(person.All).From(person));
            IWith with = sql.With.Add(cte);

            Assert.Equal("WITH cte1 AS (SELECT person.* FROM person)", with.ToString());
        }
    }
}