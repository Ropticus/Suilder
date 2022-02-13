using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Functions;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.SetOperators
{
    public class SetOperatorTest : BuilderBaseTest
    {
        [Fact]
        public void OrderBy_Offset()
        {
            Person person = null;
            Department dept = null;
            IOperator op = sql.Union(
                sql.Query.Select(() => person.Name).From(() => person),
                sql.Query.Select(() => dept.Name).From(() => dept))
                .OrderBy(x => x.Add(() => SqlExp.ColName(person.Name)))
                .Offset(10, 20);

            QueryResult result = engine.Compile(op);

            Assert.Equal("(SELECT \"person\".\"Name\" FROM \"Person\" AS \"person\") "
                + "UNION (SELECT \"dept\".\"Name\" FROM \"Dept\" AS \"dept\") "
                + "ORDER BY \"Name\" OFFSET @p0 ROWS FETCH NEXT @p1 ROWS ONLY", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 10,
                ["@p1"] = 20
            }, result.Parameters);
        }

        [Fact]
        public void With_OrderBy_Offset()
        {
            Person person = null;
            Department dept = null;
            ICte cte = sql.Cte("cte").As(sql.Query
                .Select(() => person.Name)
                .From(() => person));

            IOperator op = sql.Union(
                sql.Query.Select(() => person.Name).From(cte, () => person),
                sql.Query.Select(() => dept.Name).From(() => dept))
                .With(cte)
                .OrderBy(x => x.Add(() => SqlExp.ColName(person.Name)))
                .Offset(10, 20);

            QueryResult result = engine.Compile(op);

            Assert.Equal("WITH \"cte\" AS (SELECT \"person\".\"Name\" FROM \"Person\" AS \"person\") "
                + "(SELECT \"person\".\"Name\" FROM \"cte\" AS \"person\") "
                + "UNION (SELECT \"dept\".\"Name\" FROM \"Dept\" AS \"dept\") "
                + "ORDER BY \"Name\" OFFSET @p0 ROWS FETCH NEXT @p1 ROWS ONLY", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 10,
                ["@p1"] = 20
            }, result.Parameters);
        }

        [Fact]
        public void Before()
        {
            Person person = null;
            Department dept = null;
            ICte cte = sql.Cte("cte").As(sql.Query
                .Select(() => person.Name)
                .From(() => person));

            IOperator op = sql.Union(
                sql.Query.Select(() => person.Name).From(cte, () => person),
                sql.Query.Select(() => dept.Name).From(() => dept))
                .Before(sql.Raw("BEFORE VALUE"))
                .With(cte);

            QueryResult result = engine.Compile(op);

            Assert.Equal("BEFORE VALUE WITH \"cte\" AS (SELECT \"person\".\"Name\" FROM \"Person\" AS \"person\") "
                + "(SELECT \"person\".\"Name\" FROM \"cte\" AS \"person\") "
                + "UNION (SELECT \"dept\".\"Name\" FROM \"Dept\" AS \"dept\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void After()
        {
            Person person = null;
            Department dept = null;
            IOperator op = sql.Union(
                sql.Query.Select(() => person.Name).From(() => person),
                sql.Query.Select(() => dept.Name).From(() => dept))
                .Offset(10, 20)
                .After(sql.Raw("AFTER VALUE"));

            QueryResult result = engine.Compile(op);

            Assert.Equal("(SELECT \"person\".\"Name\" FROM \"Person\" AS \"person\") "
                + "UNION (SELECT \"dept\".\"Name\" FROM \"Dept\" AS \"dept\") "
                + "OFFSET @p0 ROWS FETCH NEXT @p1 ROWS ONLY AFTER VALUE", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 10,
                ["@p1"] = 20
            }, result.Parameters);
        }

        [Fact]
        public void As_SubQuery()
        {
            Person person = null;
            Department dept = null;
            IOperator op = sql.Union(
                sql.Query.Select(() => person.Name).From(() => person),
                sql.Query.Select(() => dept.Name).From(() => dept));

            QueryResult result = engine.Compile(sql.Raw("{0}", op));

            Assert.Equal("((SELECT \"person\".\"Name\" FROM \"Person\" AS \"person\") "
                + "UNION (SELECT \"dept\".\"Name\" FROM \"Dept\" AS \"dept\"))", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void To_String()
        {
            Person person = null;
            Department dept = null;
            IOperator op = sql.Union(
                sql.Query.Select(() => person.Name).From(() => person),
                sql.Query.Select(() => dept.Name).From(() => dept))
                .OrderBy(x => x.Add(() => SqlExp.ColName(person.Name)))
                .Offset(10, 20);

            Assert.Equal("(SELECT person.Name FROM Person AS person) "
                + "UNION (SELECT dept.Name FROM Department AS dept) "
                + "ORDER BY Name OFFSET 10 FETCH 20", op.ToString());
        }

        [Fact]
        public void To_String_With()
        {
            Person person = null;
            Department dept = null;
            ICte cte = sql.Cte("personCte").As(sql.Query.Select(() => person).From(() => person));

            IOperator op = sql.Union(
                sql.Query.Select(() => person.Name).From(cte, () => person),
                sql.Query.Select(() => dept.Name).From(() => dept))
                .With(cte);

            Assert.Equal("WITH personCte AS (SELECT person.* FROM Person AS person) "
                + "(SELECT person.Name FROM personCte AS person) "
                + "UNION (SELECT dept.Name FROM Department AS dept)", op.ToString());
        }

        [Fact]
        public void To_String_Before()
        {
            Person person = null;
            Department dept = null;
            IOperator op = sql.Union(
                sql.Query.Select(() => person.Name).From(() => person),
                sql.Query.Select(() => dept.Name).From(() => dept))
                .Before(sql.Raw("BEFORE VALUE"));

            Assert.Equal("BEFORE VALUE (SELECT person.Name FROM Person AS person) "
                + "UNION (SELECT dept.Name FROM Department AS dept)", op.ToString());
        }

        [Fact]
        public void To_String_After()
        {
            Person person = null;
            Department dept = null;
            IOperator op = sql.Union(
                sql.Query.Select(() => person.Name).From(() => person),
                sql.Query.Select(() => dept.Name).From(() => dept))
                .After(sql.Raw("AFTER VALUE"));

            Assert.Equal("(SELECT person.Name FROM Person AS person) "
                + "UNION (SELECT dept.Name FROM Department AS dept) AFTER VALUE", op.ToString());
        }
    }
}