using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Extensions;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Cte
{
    public class CteTest : BuilderBaseTest
    {
        [Fact]
        public void Cte_String()
        {
            ICte cte = sql.Cte("cte").As(sql.RawQuery("Subquery"));

            QueryResult result;

            result = engine.Compile(cte);

            Assert.Equal("\"cte\" AS (Subquery)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);

            result = engine.Compile(cte.Alias);

            Assert.Equal("\"cte\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Cte_Alias()
        {
            IAlias person = sql.Alias("person");
            ICte cte = sql.Cte(person).As(sql.RawQuery("Subquery"));

            QueryResult result;

            result = engine.Compile(cte);

            Assert.Equal("\"person\" AS (Subquery)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);

            result = engine.Compile(cte.Alias);

            Assert.Equal("\"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Cte_Alias_With_Alias_Name()
        {
            IAlias person = sql.Alias("person", "per");
            ICte cte = sql.Cte(person).As(sql.RawQuery("Subquery"));

            QueryResult result;

            result = engine.Compile(cte);

            Assert.Equal("\"per\" AS (Subquery)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);

            result = engine.Compile(cte.Alias);

            Assert.Equal("\"per\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Cte_Typed_Alias()
        {
            IAlias<Person> person = sql.Alias<Person>();
            ICte cte = sql.Cte(person).As(sql.RawQuery("Subquery"));

            QueryResult result;

            result = engine.Compile(cte);

            Assert.Equal("\"person\" AS (Subquery)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);

            result = engine.Compile(cte.Alias);

            Assert.Equal("\"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Cte_Typed_Alias_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            ICte cte = sql.Cte(person).As(sql.RawQuery("Subquery"));

            QueryResult result;

            result = engine.Compile(cte);

            Assert.Equal("\"per\" AS (Subquery)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);

            result = engine.Compile(cte.Alias);

            Assert.Equal("\"per\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Cte_Expression()
        {
            Person person = null;
            ICte cte = sql.Cte(() => person).As(sql.RawQuery("Subquery"));

            QueryResult result;

            result = engine.Compile(cte);

            Assert.Equal("\"person\" AS (Subquery)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);

            result = engine.Compile(cte.Alias);

            Assert.Equal("\"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Cte_Without_Columns()
        {
            IAlias person = sql.Alias("person");
            ICte cte = sql.Cte("cte").As(sql.Query.Select(person["Id"], person["Name"]).From(person));

            QueryResult result = engine.Compile(cte);

            Assert.Equal("\"cte\" AS (SELECT \"person\".\"Id\", \"person\".\"Name\" FROM \"person\")",
                result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Add()
        {
            IAlias person = sql.Alias("person");
            ICte cte = sql.Cte("cte")
                .Add(person["Id"])
                .Add(person["Name"])
                .As(sql.Query.Select(person["Id"], person["Name"]).From(person));

            QueryResult result = engine.Compile(cte);

            Assert.Equal("\"cte\" (\"Id\", \"Name\") AS (SELECT \"person\".\"Id\", \"person\".\"Name\" "
                + "FROM \"person\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Add_Params()
        {
            IAlias person = sql.Alias("person");
            ICte cte = sql.Cte("cte")
                .Add(person["Id"], person["Name"])
                .As(sql.Query.Select(person["Id"], person["Name"]).From(person));

            QueryResult result = engine.Compile(cte);

            Assert.Equal("\"cte\" (\"Id\", \"Name\") AS (SELECT \"person\".\"Id\", \"person\".\"Name\" "
                + "FROM \"person\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Add_Enumerable()
        {
            IAlias person = sql.Alias("person");
            ICte cte = sql.Cte("cte")
                .Add(new List<IColumn> { person["Id"], person["Name"] })
                .As(sql.Query.Select(person["Id"], person["Name"]).From(person));

            QueryResult result = engine.Compile(cte);

            Assert.Equal("\"cte\" (\"Id\", \"Name\") AS (SELECT \"person\".\"Id\", \"person\".\"Name\" "
                + "FROM \"person\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Add_Expression()
        {
            Person person = null;
            ICte cte = sql.Cte("cte")
                .Add(() => person.Id)
                .Add(() => person.Name)
                .As(sql.Query.Select(() => person.Id, () => person.Name).From(() => person));

            QueryResult result = engine.Compile(cte);

            Assert.Equal("\"cte\" (\"Id\", \"Name\") AS (SELECT \"person\".\"Id\", \"person\".\"Name\" "
                + "FROM \"Person\" AS \"person\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Add_Expression_Params()
        {
            Person person = null;
            ICte cte = sql.Cte("cte")
                .Add(() => person.Id, () => person.Name)
                .As(sql.Query.Select(() => person.Id, () => person.Name).From(() => person));

            QueryResult result = engine.Compile(cte);

            Assert.Equal("\"cte\" (\"Id\", \"Name\") AS (SELECT \"person\".\"Id\", \"person\".\"Name\" "
                + "FROM \"Person\" AS \"person\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Add_Expression_Enumerable()
        {
            Person person = null;
            ICte cte = sql.Cte("cte")
                .Add(new List<Expression<Func<object>>> { () => person.Id, () => person.Name })
                .As(sql.Query.Select(() => person.Id, () => person.Name).From(() => person));

            QueryResult result = engine.Compile(cte);

            Assert.Equal("\"cte\" (\"Id\", \"Name\") AS (SELECT \"person\".\"Id\", \"person\".\"Name\" "
                + "FROM \"Person\" AS \"person\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Add_One_Value()
        {
            IAlias person = sql.Alias("person");
            ICte cte = sql.Cte("cte")
                .Add(person["Id"])
                .As(sql.Query.Select(person["Id"]).From(person));

            QueryResult result = engine.Compile(cte);

            Assert.Equal("\"cte\" (\"Id\") AS (SELECT \"person\".\"Id\" FROM \"person\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Add_All_Columns()
        {
            Person person = null;
            ICte cte = sql.Cte("cte")
                .Add(() => person)
                .As(sql.Query.Select(() => person).From(() => person));

            QueryResult result = engine.Compile(cte);

            Assert.Equal("\"cte\" (\"Id\", \"Active\", \"Name\", \"SurName\", \"AddressStreet\", \"AddressNumber\", "
                + "\"AddressCity\", \"Salary\", \"DateCreated\", \"DepartmentId\", \"Image\", \"Flags\") "
                + "AS (SELECT \"person\".\"Id\", \"person\".\"Active\", \"person\".\"Name\", \"person\".\"SurName\", "
                + "\"person\".\"AddressStreet\", \"person\".\"AddressNumber\", \"person\".\"AddressCity\", "
                + "\"person\".\"Salary\", \"person\".\"DateCreated\", \"person\".\"DepartmentId\", \"person\".\"Image\", "
                + "\"person\".\"Flags\" FROM \"Person\" AS \"person\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Cte_RawQuery()
        {
            IAlias person = sql.Alias("person");
            ICte cte = sql.Cte("cte").As(sql.RawQuery("SELECT {0}, {1} FROM {2}", person["Id"], person["Name"], person));

            QueryResult result = engine.Compile(cte);

            Assert.Equal("\"cte\" AS (SELECT \"person\".\"Id\", \"person\".\"Name\" FROM \"person\")",
                result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Cte_Recursive()
        {
            IAlias cteAlias = sql.Alias("cte");
            ICte cte = sql.Cte(cteAlias);
            cte.Add(cteAlias["Number"])
                .As(sql.UnionAll(
                    sql.Query
                        .Select(1)
                        .From(sql.FromDummy),
                    sql.Query
                        .Select(cteAlias["Number"].Plus(1))
                        .From(cte.Alias)
                        .Where(cteAlias["Number"].Lt(10))));

            IQuery query = sql.Query.With(cte)
                .Select(cteAlias["Number"])
                .From(cte.Alias);

            QueryResult result = engine.Compile(query);

            Assert.Equal("WITH \"cte\" (\"Number\") AS ((SELECT @p0 ) UNION ALL "
                + "(SELECT \"cte\".\"Number\" + @p1 FROM \"cte\" WHERE \"cte\".\"Number\" < @p2)) "
                + "SELECT \"cte\".\"Number\" FROM \"cte\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = 1,
                ["@p2"] = 10
            }, result.Parameters);
        }

        [Fact]
        public void Cte_Name()
        {
            ICte cte = sql.Cte("cte");

            Assert.Equal("cte", cte.Name);
        }

        [Fact]
        public void Cte_Invalid_Alias_Name()
        {
            string alias = null;
            IAlias person = sql.Alias(alias);

            Exception ex = Assert.Throws<ArgumentException>(() => sql.Cte(person));
            Assert.Equal($"Alias name is null. (Parameter 'alias')", ex.Message);
        }

        [Fact]
        public void To_String()
        {
            IAlias person = sql.Alias("person");
            ICte cte = sql.Cte("cte").As(sql.Query.Select(person["Id"], person["Name"]).From(person));

            Assert.Equal("cte AS (SELECT person.Id, person.Name FROM person)", cte.ToString());
        }

        [Fact]
        public void To_String_One_Value()
        {
            IAlias person = sql.Alias("person");
            ICte cte = sql.Cte("cte").As(sql.Query.Select(person["Id"]).From(person));

            Assert.Equal("cte AS (SELECT person.Id FROM person)", cte.ToString());
        }

        [Fact]
        public void To_String_With_Columns()
        {
            IAlias person = sql.Alias("person");
            ICte cte = sql.Cte("cte")
                .Add(person["Id"])
                .Add(person["Name"])
                .As(sql.Query.Select(person["Id"], person["Name"]).From(person));

            Assert.Equal("cte (person.Id, person.Name) AS (SELECT person.Id, person.Name FROM person)", cte.ToString());
        }
    }
}