using System;
using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Exceptions;
using Suilder.Extensions;
using Suilder.Functions;
using Xunit;

namespace Suilder.Test.Builder.Query
{
    public class QueryTest : BuilderBaseTest
    {
        [Fact]
        public void Select_From()
        {
            IAlias person = sql.Alias("person");
            IQuery query = sql.Query
                .Select(person.All)
                .From(person);

            QueryResult result = engine.Compile(query);

            Assert.Equal("SELECT \"person\".* FROM \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Select_From_Where()
        {
            IAlias person = sql.Alias("person");
            IQuery query = sql.Query
                .Select(person.All)
                .From(person)
                .Where(person["Active"].Eq(true));

            QueryResult result = engine.Compile(query);

            Assert.Equal("SELECT \"person\".* FROM \"person\" WHERE \"person\".\"Active\" = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = true
            }, result.Parameters);
        }

        [Fact]
        public void Select_From_Join_Where()
        {
            IAlias person = sql.Alias("person");
            IAlias dept = sql.Alias("dept");

            IQuery query = sql.Query
                .Select(person.All, dept.All)
                .From(person)
                .Join(dept)
                    .On(dept["Id"].Eq(person["DepartmentId"]))
                .Where(person["Active"].Eq(true));

            QueryResult result = engine.Compile(query);

            Assert.Equal("SELECT \"person\".*, \"dept\".* FROM \"person\" INNER JOIN \"dept\" "
                + "ON \"dept\".\"Id\" = \"person\".\"DepartmentId\" WHERE \"person\".\"Active\" = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = true
            }, result.Parameters);
        }

        [Fact]
        public void Select_From_Join_Where_OrderBy()
        {
            IAlias person = sql.Alias("person");
            IAlias dept = sql.Alias("dept");

            IQuery query = sql.Query
                .Select(person.All, dept.All)
                .From(person)
                .Join(dept)
                    .On(dept["Id"].Eq(person["DepartmentId"]))
                .Where(person["Active"].Eq(true))
                .OrderBy(x => x.Add(person["Name"]));

            QueryResult result = engine.Compile(query);

            Assert.Equal("SELECT \"person\".*, \"dept\".* FROM \"person\" INNER JOIN \"dept\" "
                + "ON \"dept\".\"Id\" = \"person\".\"DepartmentId\" WHERE \"person\".\"Active\" = @p0 "
                + "ORDER BY \"person\".\"Name\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = true
            }, result.Parameters);
        }

        [Fact]
        public void Select_From_Join_Where_OrderBy_Offset()
        {
            IAlias person = sql.Alias("person");
            IAlias dept = sql.Alias("dept");

            IQuery query = sql.Query
                .Select(person.All, dept.All)
                .From(person)
                .Join(dept)
                    .On(dept["Id"].Eq(person["DepartmentId"]))
                .Where(person["Active"].Eq(true))
                .OrderBy(x => x.Add(person["Name"]))
                .Offset(10, 20);

            QueryResult result = engine.Compile(query);

            Assert.Equal("SELECT \"person\".*, \"dept\".* FROM \"person\" INNER JOIN \"dept\" "
                + "ON \"dept\".\"Id\" = \"person\".\"DepartmentId\" WHERE \"person\".\"Active\" = @p0 "
                + "ORDER BY \"person\".\"Name\" OFFSET @p1 ROWS FETCH NEXT @p2 ROWS ONLY", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = true,
                ["@p1"] = 10,
                ["@p2"] = 20
            }, result.Parameters);
        }

        [Fact]
        public void Select_From_Join_Where_GroupBy_OrderBy_Offset()
        {
            IAlias person = sql.Alias("person");
            IAlias dept = sql.Alias("dept");

            IQuery query = sql.Query
                .Select(dept["Id"], dept["Name"], SqlFn.Count())
                .From(person)
                .Join(dept)
                    .On(dept["Id"].Eq(person["DepartmentId"]))
                .Where(person["Active"].Eq(true))
                .GroupBy(dept["Id"], dept["Name"])
                .OrderBy(x => x.Add(dept["Name"]))
                .Offset(10, 20);

            QueryResult result = engine.Compile(query);

            Assert.Equal("SELECT \"dept\".\"Id\", \"dept\".\"Name\", COUNT(*) FROM \"person\" INNER JOIN \"dept\" "
                + "ON \"dept\".\"Id\" = \"person\".\"DepartmentId\" WHERE \"person\".\"Active\" = @p0 "
                + "GROUP BY \"dept\".\"Id\", \"dept\".\"Name\" ORDER BY \"dept\".\"Name\" OFFSET @p1 ROWS FETCH NEXT @p2 "
                + "ROWS ONLY", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = true,
                ["@p1"] = 10,
                ["@p2"] = 20
            }, result.Parameters);
        }

        [Fact]
        public void Select_From_Join_Where_GroupBy_Having_OrderBy_Offset()
        {
            IAlias person = sql.Alias("person");
            IAlias dept = sql.Alias("dept");

            IQuery query = sql.Query
                .Select(dept["Id"], dept["Name"], SqlFn.Count())
                .From(person)
                .Join(dept)
                    .On(dept["Id"].Eq(person["DepartmentId"]))
                .Where(person["Active"].Eq(true))
                .GroupBy(dept["Id"], dept["Name"])
                .Having(SqlFn.Count().Gt(10))
                .OrderBy(x => x.Add(dept["Name"]))
                .Offset(10, 20);

            QueryResult result = engine.Compile(query);

            Assert.Equal("SELECT \"dept\".\"Id\", \"dept\".\"Name\", COUNT(*) FROM \"person\" INNER JOIN \"dept\" "
                + "ON \"dept\".\"Id\" = \"person\".\"DepartmentId\" WHERE \"person\".\"Active\" = @p0 "
                + "GROUP BY \"dept\".\"Id\", \"dept\".\"Name\" HAVING COUNT(*) > @p1 ORDER BY \"dept\".\"Name\" "
                + "OFFSET @p2 ROWS FETCH NEXT @p3 ROWS ONLY", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = true,
                ["@p1"] = 10,
                ["@p2"] = 10,
                ["@p3"] = 20
            }, result.Parameters);
        }

        [Fact]
        public void With_Select_From_Join_Where()
        {
            IAlias person = sql.Alias("person");
            IAlias dept = sql.Alias("dept");
            IAlias personCte = sql.Alias("personCte");
            ICte cte = sql.Cte("cte").As(sql.Query
                .Select(person.All)
                .From(person)
                .Where(person["Active"].Eq(true)));

            IQuery query = sql.Query
                .With(cte)
                .Select(personCte.All, dept.All)
                .From(cte, personCte)
                .Join(dept)
                    .On(dept["Id"].Eq(personCte["DepartmentId"]));

            QueryResult result = engine.Compile(query);

            Assert.Equal("WITH \"cte\" AS (SELECT \"person\".* FROM \"person\" WHERE \"person\".\"Active\" = @p0) "
                + "SELECT \"personCte\".*, \"dept\".* FROM \"cte\" AS \"personCte\" INNER JOIN \"dept\" "
                + "ON \"dept\".\"Id\" = \"personCte\".\"DepartmentId\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = true
            }, result.Parameters);
        }

        [Fact]
        public void Count()
        {
            IAlias person = sql.Alias("person");
            IAlias dept = sql.Alias("dept");

            IQuery query = sql.Query
                .Select(person.All, dept.All)
                .From(person)
                .Join(dept)
                    .On(dept["Id"].Eq(person["DepartmentId"]))
                .Where(person["Active"].Eq(true))
                .OrderBy(x => x.Add(person["Name"]))
                .Offset(10, 20)
                .Count();

            QueryResult result = engine.Compile(query);

            Assert.Equal("SELECT COUNT(*) FROM \"person\" INNER JOIN \"dept\" "
                + "ON \"dept\".\"Id\" = \"person\".\"DepartmentId\" WHERE \"person\".\"Active\" = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = true
            }, result.Parameters);
        }

        [Fact]
        public void Before()
        {
            IAlias person = sql.Alias("person");
            IAlias personCte = sql.Alias("personCte");
            ICte cte = sql.Cte("cte").As(sql.Query
                .Select(person.All)
                .From(person));

            IQuery query = sql.Query
                .Before(sql.Raw("BEFORE VALUE"))
                .With(cte)
                .Select(personCte.All)
                .From(cte, personCte);

            QueryResult result = engine.Compile(query);

            Assert.Equal("BEFORE VALUE WITH \"cte\" AS (SELECT \"person\".* FROM \"person\") "
                + "SELECT \"personCte\".* FROM \"cte\" AS \"personCte\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void After()
        {
            IAlias person = sql.Alias("person");
            IQuery query = sql.Query
                .Select(person.All)
                .From(person)
                .Offset(10, 20)
                .After(sql.Raw("AFTER VALUE"));

            QueryResult result = engine.Compile(query);

            Assert.Equal("SELECT \"person\".* FROM \"person\" OFFSET @p0 ROWS FETCH NEXT @p1 ROWS ONLY AFTER VALUE",
                result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 10,
                ["@p1"] = 20
            }, result.Parameters);
        }

        [Fact]
        public void Reuse_Query()
        {
            IAlias person = sql.Alias("person");
            IQuery query = sql.Query;

            QueryResult result;

            // Select
            query.Select(person.All)
                .From(person)
                .Where(person["Id"].Eq(1));

            result = engine.Compile(query);

            Assert.Equal("SELECT \"person\".* FROM \"person\" WHERE \"person\".\"Id\" = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1
            }, result.Parameters);

            // Update
            query.Update()
                .Set(person["Name"], "Name1");

            result = engine.Compile(query);

            Assert.Equal("UPDATE \"person\" SET \"Name\" = @p0 WHERE \"person\".\"Id\" = @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "Name1",
                ["@p1"] = 1
            }, result.Parameters);

            // Delete
            query = query.Delete();

            result = engine.Compile(query);

            Assert.Equal("DELETE FROM \"person\" WHERE \"person\".\"Id\" = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1
            }, result.Parameters);

            // Select
            query.Select(person["Name"]);

            result = engine.Compile(query);

            Assert.Equal("SELECT \"person\".\"Name\" FROM \"person\" WHERE \"person\".\"Id\" = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1
            }, result.Parameters);

            // Insert select
            query = query.Insert(x => x.Into(person).Add(person["Name"]));

            result = engine.Compile(query);

            Assert.Equal("INSERT INTO \"person\" (\"Name\") "
                + "SELECT \"person\".\"Name\" FROM \"person\" WHERE \"person\".\"Id\" = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1
            }, result.Parameters);

            // Insert
            query = query.Insert(person)
                .Values(1, "Name1");

            result = engine.Compile(query);

            Assert.Equal("INSERT INTO \"person\" VALUES (@p0, @p1)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = "Name1"
            }, result.Parameters);

            // Update
            query.Update()
                .Set(person["Name"], "Name1")
                .From(person)
                .Where(person["Id"].Eq(1));

            result = engine.Compile(query);

            Assert.Equal("UPDATE \"person\" SET \"Name\" = @p0 WHERE \"person\".\"Id\" = @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "Name1",
                ["@p1"] = 1
            }, result.Parameters);
        }

        [Fact]
        public void As_SubQuery()
        {
            IAlias person = sql.Alias("person");
            IQuery query = sql.Query
                .Select(person.All)
                .From(person);

            QueryResult result = engine.Compile(sql.Raw("{0}", query));

            Assert.Equal("(SELECT \"person\".* FROM \"person\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Empty_Query()
        {
            IQuery query = sql.Query;

            Exception ex = Assert.Throws<CompileException>(() => engine.Compile(query));
            Assert.Equal("Query is empty.", ex.Message);
        }

        [Fact]
        public void To_String()
        {
            IAlias person = sql.Alias("person");
            IAlias dept = sql.Alias("dept");

            IQuery query = sql.Query
                .Select(dept["Id"], dept["Name"], SqlFn.Count())
                .From(person)
                .Join(dept)
                    .On(dept["Id"].Eq(person["DepartmentId"]))
                .Where(person["Active"].Eq(true))
                .GroupBy(dept["Id"], dept["Name"])
                .Having(SqlFn.Count().Gt(10))
                .OrderBy(x => x.Add(dept["Name"]))
                .Offset(10, 20);

            Assert.Equal("SELECT dept.Id, dept.Name, COUNT(*) FROM person INNER JOIN dept "
                + "ON dept.Id = person.DepartmentId WHERE person.Active = true "
                + "GROUP BY dept.Id, dept.Name HAVING COUNT(*) > 10 ORDER BY dept.Name "
                + "OFFSET 10 FETCH 20", query.ToString());
        }

        [Fact]
        public void To_String_Insert()
        {
            IAlias person = sql.Alias("person");

            IQuery query = sql.Query.Insert(x => x.Into(person)
                .Add(person["Name"], person["SurName"]))
                .Values("Name1", "SurName1");

            Assert.Equal("INSERT INTO person (person.Name, person.SurName) VALUES (\"Name1\", \"SurName1\")",
                query.ToString());
        }

        [Fact]
        public void To_String_Update()
        {
            IAlias person = sql.Alias("person");

            IQuery query = sql.Query.Update()
                .Set("person.Name", "abcd")
                .From(person);

            Assert.Equal("UPDATE SET person.Name = \"abcd\" FROM person", query.ToString());
        }

        [Fact]
        public void To_String_Delete()
        {
            IAlias person = sql.Alias("person");

            IQuery query = sql.Query.Delete()
                .From(person);

            Assert.Equal("DELETE FROM person", query.ToString());
        }

        [Fact]
        public void To_String_With()
        {
            IAlias person = sql.Alias("person");
            ICte cte = sql.Cte("personCte").As(sql.Query.Select(person.All).From(person));
            IQuery query = sql.Query.With(cte);

            Assert.Equal("WITH personCte AS (SELECT person.* FROM person)", query.ToString());
        }

        [Fact]
        public void To_String_Before()
        {
            IAlias person = sql.Alias("person");
            IQuery query = sql.Query
                .Before(sql.Raw("BEFORE VALUE"))
                .Select(person.All)
                .From(person);

            Assert.Equal("BEFORE VALUE SELECT person.* FROM person", query.ToString());
        }

        [Fact]
        public void To_String_After()
        {
            IAlias person = sql.Alias("person");
            IQuery query = sql.Query
                .Select(person.All)
                .From(person)
                .After(sql.Raw("AFTER VALUE"));

            Assert.Equal("SELECT person.* FROM person AFTER VALUE", query.ToString());
        }
    }
}