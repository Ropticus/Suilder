using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Query
{
    public class SelectTest : BaseTest
    {
        [Fact]
        public void Select()
        {
            IAlias person = sql.Alias("person");
            IQuery query = sql.Query.Select(person["Name"]);

            QueryResult result = engine.Compile(query);

            Assert.Equal("SELECT \"person\".\"Name\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Select_Params()
        {
            IAlias person = sql.Alias("person");
            IQuery query = sql.Query.Select(person["Name"], ", ", person["SurName"]);

            QueryResult result = engine.Compile(query);

            Assert.Equal("SELECT \"person\".\"Name\", @p0, \"person\".\"SurName\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = ", " }, result.Parameters);
        }

        [Fact]
        public void Select_Enumerable()
        {
            IAlias person = sql.Alias("person");
            IQuery query = sql.Query.Select(new List<object>() { person["Name"], ", ", person["SurName"] });

            QueryResult result = engine.Compile(query);

            Assert.Equal("SELECT \"person\".\"Name\", @p0, \"person\".\"SurName\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = ", " }, result.Parameters);
        }

        [Fact]
        public void Select_Expression()
        {
            Person person = null;
            IQuery query = sql.Query.Select(() => person.Name);

            QueryResult result = engine.Compile(query);

            Assert.Equal("SELECT \"person\".\"Name\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Select_Expression_Params()
        {
            Person person = null;
            IQuery query = sql.Query.Select(() => person.Name, () => ", ", () => person.SurName);

            QueryResult result = engine.Compile(query);

            Assert.Equal("SELECT \"person\".\"Name\", @p0, \"person\".\"SurName\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = ", " }, result.Parameters);
        }

        [Fact]
        public void Select_Expression_Enumerable()
        {
            Person person = null;
            IQuery query = sql.Query.Select(new List<Expression<Func<object>>>() { () => person.Name,
                () => ", ", () => person.SurName });

            QueryResult result = engine.Compile(query);

            Assert.Equal("SELECT \"person\".\"Name\", @p0, \"person\".\"SurName\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = ", " }, result.Parameters);
        }

        [Fact]
        public void Select_Func()
        {
            IAlias person = sql.Alias("person");
            IQuery query = sql.Query.Select(x => x.Add(person["Name"]));

            QueryResult result = engine.Compile(query);

            Assert.Equal("SELECT \"person\".\"Name\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Select_Value()
        {
            IAlias person = sql.Alias("person");
            IQuery query = sql.Query.Select(sql.Select().Add(person["Name"]));

            QueryResult result = engine.Compile(query);

            Assert.Equal("SELECT \"person\".\"Name\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Select_Raw()
        {
            IAlias person = sql.Alias("person");
            IQuery query = sql.Query.Select(sql.Raw("SELECT {0}", person["Name"]));

            QueryResult result = engine.Compile(query);

            Assert.Equal("SELECT \"person\".\"Name\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }
    }
}