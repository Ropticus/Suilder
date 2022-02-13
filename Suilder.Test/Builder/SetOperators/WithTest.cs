using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.SetOperators
{
    public class WithTest : BuilderBaseTest
    {
        [Fact]
        public void With()
        {
            IAlias person = sql.Alias("person");
            ICte cte = sql.Cte("personCte").As(sql.Query.Select(person.All).From(person));
            IOperator op = sql.Union(sql.RawQuery("Subquery1"), sql.RawQuery("Subquery2"))
                .With(cte);

            QueryResult result = engine.Compile(op);

            Assert.Equal("WITH \"personCte\" AS (SELECT \"person\".* FROM \"person\") "
                + "(Subquery1) UNION (Subquery2)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void With_Params()
        {
            IAlias person = sql.Alias("person");
            IAlias dept = sql.Alias("dept");
            ICte cte1 = sql.Cte("personCte").As(sql.Query.Select(person.All).From(person));
            ICte cte2 = sql.Cte("deptCte").As(sql.Query.Select(dept.All).From(dept));
            IOperator op = sql.Union(sql.RawQuery("Subquery1"), sql.RawQuery("Subquery2"))
                .With(cte1, cte2);

            QueryResult result = engine.Compile(op);

            Assert.Equal("WITH \"personCte\" AS (SELECT \"person\".* FROM \"person\"), "
                + "\"deptCte\" AS (SELECT \"dept\".* FROM \"dept\") "
                + "(Subquery1) UNION (Subquery2)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void With_Enumerable()
        {
            IAlias person = sql.Alias("person");
            IAlias dept = sql.Alias("dept");
            ICte cte1 = sql.Cte("personCte").As(sql.Query.Select(person.All).From(person));
            ICte cte2 = sql.Cte("deptCte").As(sql.Query.Select(dept.All).From(dept));
            IOperator op = sql.Union(sql.RawQuery("Subquery1"), sql.RawQuery("Subquery2"))
                .With(new List<IQueryFragment> { cte1, cte2 });

            QueryResult result = engine.Compile(op);

            Assert.Equal("WITH \"personCte\" AS (SELECT \"person\".* FROM \"person\"), "
                + "\"deptCte\" AS (SELECT \"dept\".* FROM \"dept\") "
                + "(Subquery1) UNION (Subquery2)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void With_Func()
        {
            IAlias person = sql.Alias("person");
            ICte cte = sql.Cte("personCte").As(sql.Query.Select(person.All).From(person));
            IOperator op = sql.Union(sql.RawQuery("Subquery1"), sql.RawQuery("Subquery2"))
                .With(x => x.Add(cte));

            QueryResult result = engine.Compile(op);

            Assert.Equal("WITH \"personCte\" AS (SELECT \"person\".* FROM \"person\") "
                + "(Subquery1) UNION (Subquery2)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void With_Value()
        {
            IAlias person = sql.Alias("person");
            ICte cte = sql.Cte("personCte").As(sql.Query.Select(person.All).From(person));
            IOperator op = sql.Union(sql.RawQuery("Subquery1"), sql.RawQuery("Subquery2"))
                .With(sql.With.Add(cte));

            QueryResult result = engine.Compile(op);

            Assert.Equal("WITH \"personCte\" AS (SELECT \"person\".* FROM \"person\") "
                + "(Subquery1) UNION (Subquery2)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void With_Raw()
        {
            IAlias person = sql.Alias("person");
            ICte cte = sql.Cte("personCte").As(sql.Query.Select(person.All).From(person));
            IOperator op = sql.Union(sql.RawQuery("Subquery1"), sql.RawQuery("Subquery2"))
                .With(sql.Raw("WITH {0}", cte));

            QueryResult result = engine.Compile(op);

            Assert.Equal("WITH \"personCte\" AS (SELECT \"person\".* FROM \"person\") "
                + "(Subquery1) UNION (Subquery2)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }
    }
}