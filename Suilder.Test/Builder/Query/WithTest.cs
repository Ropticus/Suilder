using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Xunit;

namespace Suilder.Test.Builder.Query
{
    public class WithTest : BuilderBaseTest
    {
        [Fact]
        public void With()
        {
            IAlias person = sql.Alias("person");
            ICte cte = sql.Cte("personCte").As(sql.Query.Select(person.All).From(person));
            IQuery query = sql.Query.With(cte);

            QueryResult result = engine.Compile(query);

            Assert.Equal("WITH \"personCte\" AS (SELECT \"person\".* FROM \"person\")", result.Sql);
        }

        [Fact]
        public void With_Params()
        {
            IAlias person = sql.Alias("person");
            IAlias dept = sql.Alias("dept");
            ICte cte1 = sql.Cte("personCte").As(sql.Query.Select(person.All).From(person));
            ICte cte2 = sql.Cte("deptCte").As(sql.Query.Select(dept.All).From(dept));
            IQuery query = sql.Query.With(cte1, cte2);

            QueryResult result = engine.Compile(query);

            Assert.Equal("WITH \"personCte\" AS (SELECT \"person\".* FROM \"person\"), "
                + "\"deptCte\" AS (SELECT \"dept\".* FROM \"dept\")", result.Sql);
        }

        [Fact]
        public void With_Enumerable()
        {
            IAlias person = sql.Alias("person");
            IAlias dept = sql.Alias("dept");
            ICte cte1 = sql.Cte("personCte").As(sql.Query.Select(person.All).From(person));
            ICte cte2 = sql.Cte("deptCte").As(sql.Query.Select(dept.All).From(dept));
            IQuery query = sql.Query.With(new List<IQueryFragment> { cte1, cte2 });

            QueryResult result = engine.Compile(query);

            Assert.Equal("WITH \"personCte\" AS (SELECT \"person\".* FROM \"person\"), "
                + "\"deptCte\" AS (SELECT \"dept\".* FROM \"dept\")", result.Sql);
        }

        [Fact]
        public void With_Func()
        {
            IAlias person = sql.Alias("person");
            ICte cte = sql.Cte("personCte").As(sql.Query.Select(person.All).From(person));
            IQuery query = sql.Query.With(x => x.Add(cte));

            QueryResult result = engine.Compile(query);

            Assert.Equal("WITH \"personCte\" AS (SELECT \"person\".* FROM \"person\")", result.Sql);
        }

        [Fact]
        public void With_Value()
        {
            IAlias person = sql.Alias("person");
            ICte cte = sql.Cte("personCte").As(sql.Query.Select(person.All).From(person));
            IQuery query = sql.Query.With(sql.With.Add(cte));

            QueryResult result = engine.Compile(query);

            Assert.Equal("WITH \"personCte\" AS (SELECT \"person\".* FROM \"person\")", result.Sql);
        }

        [Fact]
        public void With_Raw()
        {
            IAlias person = sql.Alias("person");
            ICte cte = sql.Cte("personCte").As(sql.Query.Select(person.All).From(person));
            IQuery query = sql.Query.With(sql.Raw("WITH {0}", cte));

            QueryResult result = engine.Compile(query);

            Assert.Equal("WITH \"personCte\" AS (SELECT \"person\".* FROM \"person\")", result.Sql);
        }
    }
}