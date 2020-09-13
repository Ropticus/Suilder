using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Cte
{
    public class CteTest : BuilderBaseTest
    {
        [Fact]
        public void Cte_Without_Columns()
        {
            IAlias person = sql.Alias("person");
            ICte cte = sql.Cte("cte").As(sql.Query.Select(person["Id"], person["Name"]).From(person));

            QueryResult result = engine.Compile(cte);

            Assert.Equal("\"cte\" AS (SELECT \"person\".\"Id\", \"person\".\"Name\" FROM \"person\")",
                result.Sql);
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
                + "\"AddressCity\", \"Salary\", \"DateCreated\", \"DepartmentId\", \"Image\") "
                + "AS (SELECT \"person\".\"Id\", \"person\".\"Active\", \"person\".\"Name\", \"person\".\"SurName\", "
                + "\"person\".\"AddressStreet\", \"person\".\"AddressNumber\", \"person\".\"AddressCity\", "
                + "\"person\".\"Salary\", \"person\".\"DateCreated\", \"person\".\"DepartmentId\", \"person\".\"Image\" "
                + "FROM \"Person\" AS \"person\")", result.Sql);
        }

        [Fact]
        public void Cte_RawQuery()
        {
            IAlias person = sql.Alias("person");
            ICte cte = sql.Cte("cte").As(sql.RawQuery("SELECT {0}, {1} FROM {2}", person["Id"], person["Name"], person));

            QueryResult result = engine.Compile(cte);

            Assert.Equal("\"cte\" AS (SELECT \"person\".\"Id\", \"person\".\"Name\" FROM \"person\")",
                result.Sql);
        }

        [Fact]
        public void Cte_Name()
        {
            ICte cte = sql.Cte("cte");

            Assert.Equal("cte", cte.Name);
        }

        [Fact]
        public void To_String()
        {
            IAlias person = sql.Alias("person");
            ICte cte = sql.Cte("cte").As(sql.Query.Select(person["Id"], person["Name"]).From(person));

            Assert.Equal("cte AS (SELECT person.Id, person.Name FROM person)", cte.ToString());
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