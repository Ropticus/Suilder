using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Exceptions;
using Suilder.Functions;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Select
{
    public class SelectTest : BuilderBaseTest
    {
        [Fact]
        public void Add()
        {
            IAlias person = sql.Alias("person");
            ISelect select = sql.Select()
                .Add(person["Name"])
                .Add(", ")
                .Add(person["SurName"]);

            QueryResult result = engine.Compile(select);

            Assert.Equal("SELECT \"person\".\"Name\", @p0, \"person\".\"SurName\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = ", "
            }, result.Parameters);
        }

        [Fact]
        public void Add_Params()
        {
            IAlias person = sql.Alias("person");
            ISelect select = sql.Select().Add(person["Name"], ", ", person["SurName"]);

            QueryResult result = engine.Compile(select);

            Assert.Equal("SELECT \"person\".\"Name\", @p0, \"person\".\"SurName\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = ", "
            }, result.Parameters);
        }

        [Fact]
        public void Add_Enumerable()
        {
            IAlias person = sql.Alias("person");
            ISelect select = sql.Select().Add(new List<object> { person["Name"], ", ", person["SurName"] });

            QueryResult result = engine.Compile(select);

            Assert.Equal("SELECT \"person\".\"Name\", @p0, \"person\".\"SurName\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = ", "
            }, result.Parameters);
        }

        [Fact]
        public void Add_Expression()
        {
            Person person = null;
            ISelect select = sql.Select()
                .Add(() => person.Name)
                .Add(() => ", ")
                .Add(() => person.SurName);

            QueryResult result = engine.Compile(select);

            Assert.Equal("SELECT \"person\".\"Name\", @p0, \"person\".\"SurName\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = ", "
            }, result.Parameters);
        }

        [Fact]
        public void Add_Expression_Params()
        {
            Person person = null;
            ISelect select = sql.Select().Add(() => person.Name, () => ", ", () => person.SurName);

            QueryResult result = engine.Compile(select);

            Assert.Equal("SELECT \"person\".\"Name\", @p0, \"person\".\"SurName\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = ", "
            }, result.Parameters);
        }

        [Fact]
        public void Add_Expression_Enumerable()
        {
            Person person = null;
            ISelect select = sql.Select().Add(new List<Expression<Func<object>>> { () => person.Name, () => ", ",
                () => person.SurName });

            QueryResult result = engine.Compile(select);

            Assert.Equal("SELECT \"person\".\"Name\", @p0, \"person\".\"SurName\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = ", "
            }, result.Parameters);
        }

        [Fact]
        public void Alias()
        {
            IAlias person = sql.Alias("person");
            ISelect select = sql.Select()
                .Add(person["Name"]).As("Name")
                .Add(", ").As("Literal")
                .Add(person["SurName"]).As("SurName");

            QueryResult result = engine.Compile(select);

            Assert.Equal("SELECT \"person\".\"Name\" AS \"Name\", @p0 AS \"Literal\", \"person\".\"SurName\" "
                + "AS \"SurName\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = ", "
            }, result.Parameters);
        }

        [Fact]
        public void Alias_Params()
        {
            IAlias person = sql.Alias("person");
            ISelect select = sql.Select().Add(person["Name"], ", ", person["SurName"]).As("SurName");

            QueryResult result = engine.Compile(select);

            Assert.Equal("SELECT \"person\".\"Name\", @p0, \"person\".\"SurName\" AS \"SurName\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = ", "
            }, result.Parameters);
        }

        [Fact]
        public void Invalid_Alias_Empty()
        {
            Exception ex = Assert.Throws<InvalidOperationException>(() => sql.Select().As("Name"));
            Assert.Equal("List is empty.", ex.Message);
        }

        [Fact]
        public void Invalid_Alias_All()
        {
            IAlias person = sql.Alias("person");

            Exception ex = Assert.Throws<InvalidOperationException>(() => sql.Select().Add(person.All).As("Name"));
            Assert.Equal("Cannot add alias for select all column.", ex.Message);
        }

        [Fact]
        public void Distinct()
        {
            IAlias person = sql.Alias("person");
            ISelect select = sql.Select().Distinct().Add(person["Name"]);

            QueryResult result = engine.Compile(select);

            Assert.Equal("SELECT DISTINCT \"person\".\"Name\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Distinct_False()
        {
            IAlias person = sql.Alias("person");
            ISelect select = sql.Select().Distinct(false).Add(person["Name"]);

            QueryResult result = engine.Compile(select);

            Assert.Equal("SELECT \"person\".\"Name\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Distinct_On()
        {
            IAlias person = sql.Alias("person");
            ISelect select = sql.Select().DistinctOn(person["Id"]).Add(person.All);

            QueryResult result = engine.Compile(select);

            Assert.Equal("SELECT DISTINCT ON(\"person\".\"Id\") \"person\".*", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Distinct_On_Params()
        {
            IAlias person = sql.Alias("person");
            ISelect select = sql.Select().DistinctOn(person["Name"], person["SurName"]).Add(person.All);

            QueryResult result = engine.Compile(select);

            Assert.Equal("SELECT DISTINCT ON(\"person\".\"Name\", \"person\".\"SurName\") \"person\".*", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Distinct_On_Enumerable()
        {
            IAlias person = sql.Alias("person");
            ISelect select = sql.Select().DistinctOn(new List<object> { person["Name"], person["SurName"] }).Add(person.All);

            QueryResult result = engine.Compile(select);

            Assert.Equal("SELECT DISTINCT ON(\"person\".\"Name\", \"person\".\"SurName\") \"person\".*", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Distinct_On_Expression()
        {
            Person person = null;
            ISelect select = sql.Select().DistinctOn(() => person.Id).Add(() => person);

            QueryResult result = engine.Compile(select);

            Assert.Equal("SELECT DISTINCT ON(\"person\".\"Id\") \"person\".\"Id\", \"person\".\"Active\", "
                + "\"person\".\"Name\", \"person\".\"SurName\", \"person\".\"AddressStreet\", \"person\".\"AddressCity\", "
                + "\"person\".\"Salary\", \"person\".\"DateCreated\", \"person\".\"DepartmentId\", \"person\".\"Image\"",
                result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Distinct_On_Expression_Params()
        {
            Person person = null;
            ISelect select = sql.Select().DistinctOn(() => person.Name, () => person.SurName).Add(() => person);

            QueryResult result = engine.Compile(select);

            Assert.Equal("SELECT DISTINCT ON(\"person\".\"Name\", \"person\".\"SurName\") \"person\".\"Id\", "
                + "\"person\".\"Active\", \"person\".\"Name\", \"person\".\"SurName\", \"person\".\"AddressStreet\", "
                + "\"person\".\"AddressCity\", \"person\".\"Salary\", \"person\".\"DateCreated\", "
                + "\"person\".\"DepartmentId\", \"person\".\"Image\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Distinct_On_Expression_Enumerable()
        {
            Person person = null;
            ISelect select = sql.Select().DistinctOn(new List<Expression<Func<object>>> {() => person.Name,
                () => person.SurName}).Add(() => person);

            QueryResult result = engine.Compile(select);

            Assert.Equal("SELECT DISTINCT ON(\"person\".\"Name\", \"person\".\"SurName\") \"person\".\"Id\", "
                + "\"person\".\"Active\", \"person\".\"Name\", \"person\".\"SurName\", \"person\".\"AddressStreet\", "
                + "\"person\".\"AddressCity\", \"person\".\"Salary\", \"person\".\"DateCreated\", "
                + "\"person\".\"DepartmentId\", \"person\".\"Image\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Distinct_On_Value()
        {
            IAlias person = sql.Alias("person");
            ISelect select = sql.Select().DistinctOn(on => on.Add(person["Id"])).Add(person.All);

            QueryResult result = engine.Compile(select);

            Assert.Equal("SELECT DISTINCT ON(\"person\".\"Id\") \"person\".*", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Distinct_On_Not_Supported()
        {
            engine.Options.DistinctOnSupported = false;

            IAlias person = sql.Alias("person");
            ISelect select = sql.Select().DistinctOn(person["Id"]).Add(person.All);

            Exception ex = Assert.Throws<ClauseNotSupportedException>(() => engine.Compile(select));
            Assert.Equal("Distinct on clause is not supported in this engine.", ex.Message);
        }

        [Fact]
        public void Top()
        {
            IAlias person = sql.Alias("person");
            ISelect select = sql.Select().Top(10).Add(person.All);

            QueryResult result = engine.Compile(select);

            Assert.Equal("SELECT TOP(@p0) \"person\".*", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 10
            }, result.Parameters);
        }

        [Fact]
        public void Top_Percent()
        {
            IAlias person = sql.Alias("person");
            ISelect select = sql.Select().Top(10).Percent().Add(person.All);

            QueryResult result = engine.Compile(select);

            Assert.Equal("SELECT TOP(@p0) PERCENT \"person\".*", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 10
            }, result.Parameters);
        }

        [Fact]
        public void Top_WithTies()
        {
            IAlias person = sql.Alias("person");
            ISelect select = sql.Select().Top(10).WithTies().Add(person.All);

            QueryResult result = engine.Compile(select);

            Assert.Equal("SELECT TOP(@p0) WITH TIES \"person\".*", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 10
            }, result.Parameters);
        }

        [Fact]
        public void Top_Value()
        {
            IAlias person = sql.Alias("person");
            ISelect select = sql.Select().Top(sql.Top(10)).Add(person.All);

            QueryResult result = engine.Compile(select);

            Assert.Equal("SELECT TOP(@p0) \"person\".*", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 10
            }, result.Parameters);
        }

        [Fact]
        public void Top_Raw()
        {
            IAlias person = sql.Alias("person");
            ISelect select = sql.Select().Top(sql.Raw("TOP({0})", 10)).Add(person.All);

            QueryResult result = engine.Compile(select);

            Assert.Equal("SELECT TOP(@p0) \"person\".*", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 10
            }, result.Parameters);
        }

        [Fact]
        public void Invalid_Top_Percent()
        {
            IAlias person = sql.Alias("person");
            ISelect select = sql.Select().Top(sql.Raw("TOP({0})", 10)).Add(person.All);

            Exception ex = Assert.Throws<InvalidOperationException>(() => ((ISelectTop)select).Percent());
            Assert.Equal("Top value must be a \"Suilder.Core.ITop\" instance.", ex.Message);
        }

        [Fact]
        public void Invalid_Top_WithTies()
        {
            IAlias person = sql.Alias("person");
            ISelect select = sql.Select().Top(sql.Raw("TOP({0})", 10)).Add(person.All);

            Exception ex = Assert.Throws<InvalidOperationException>(() => ((ISelectTop)select).WithTies());
            Assert.Equal("Top value must be a \"Suilder.Core.ITop\" instance.", ex.Message);
        }

        [Fact]
        public void Top_Distinct()
        {
            IAlias person = sql.Alias("person");
            ISelect select = sql.Select().Distinct().Top(10).Add(person["Name"]);

            QueryResult result = engine.Compile(select);

            Assert.Equal("SELECT DISTINCT TOP(@p0) \"person\".\"Name\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 10
            }, result.Parameters);
        }

        [Fact]
        public void Over()
        {
            ISelect select = sql.Select().Add(SqlFn.Count()).Over();

            QueryResult result = engine.Compile(select);

            Assert.Equal("SELECT COUNT(*) OVER()", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Over_Value()
        {
            IAlias person = sql.Alias("person");
            ISelect select = sql.Select().Add(SqlFn.Sum(person["Salary"]))
                .Over(o => o.PartitionBy(x => x.Add(person["DepartmentId"])));

            QueryResult result = engine.Compile(select);

            Assert.Equal("SELECT SUM(\"person\".\"Salary\") OVER(PARTITION BY \"person\".\"DepartmentId\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Invalid_Over_Empty()
        {
            Exception ex = Assert.Throws<InvalidOperationException>(() => sql.Select().Over());
            Assert.Equal("List is empty.", ex.Message);
        }

        [Fact]
        public void Invalid_Over_All()
        {
            IAlias person = sql.Alias("person");

            Exception ex = Assert.Throws<InvalidOperationException>(() => sql.Select().Add(person.All).Over());
            Assert.Equal("Cannot add over clause for select all column.", ex.Message);
        }

        [Fact]
        public void Empty_List()
        {
            ISelect select = sql.Select();

            Exception ex = Assert.Throws<CompileException>(() => engine.Compile(select));
            Assert.Equal("List is empty.", ex.Message);
        }

        [Fact]
        public void To_String()
        {
            IAlias person = sql.Alias("person");
            ISelect select = sql.Select().Add(person["Name"]).As("Name");

            Assert.Equal("SELECT person.Name AS Name", select.ToString());
        }

        [Fact]
        public void To_String_Distinct()
        {
            IAlias person = sql.Alias("person");
            ISelect select = sql.Select().Distinct().Add(person["Name"]).As("Name");

            Assert.Equal("SELECT DISTINCT person.Name AS Name", select.ToString());
        }

        [Fact]
        public void To_String_Distinct_On()
        {
            IAlias person = sql.Alias("person");
            ISelect select = sql.Select().DistinctOn(person["Id"]).Add(person.All);

            Assert.Equal("SELECT DISTINCT ON(person.Id) person.*", select.ToString());
        }

        [Fact]
        public void To_String_Top()
        {
            IAlias person = sql.Alias("person");
            ISelect select = sql.Select().Top(10).Add(person["Name"]).As("Name");

            Assert.Equal("SELECT TOP(10) person.Name AS Name", select.ToString());
        }

        [Fact]
        public void To_String_Top_Distinct()
        {
            IAlias person = sql.Alias("person");
            ISelect select = sql.Select().Distinct().Top(10).Add(person["Name"]).As("Name");

            Assert.Equal("SELECT DISTINCT TOP(10) person.Name AS Name", select.ToString());
        }

        [Fact]
        public void To_String_Over()
        {
            ISelect select = sql.Select().Add(SqlFn.Count()).Over();

            Assert.Equal("SELECT COUNT(*) OVER()", select.ToString());
        }
    }
}