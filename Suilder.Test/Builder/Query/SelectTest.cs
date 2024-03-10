using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Query
{
    public class SelectTest : BuilderBaseTest
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

        [Theory]
        [MemberData(nameof(DataObject))]
        public void Select_Params(object value)
        {
            IAlias person = sql.Alias("person");
            IQuery query = sql.Query.Select(person["Name"], ", ", person["Surname"], value);

            QueryResult result = engine.Compile(query);

            Assert.Equal("SELECT \"person\".\"Name\", @p0, \"person\".\"Surname\", @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = ", ",
                ["@p1"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataObject))]
        public void Select_Enumerable(object value)
        {
            IAlias person = sql.Alias("person");
            IQuery query = sql.Query.Select(new List<object> { person["Name"], ", ", person["Surname"], value });

            QueryResult result = engine.Compile(query);

            Assert.Equal("SELECT \"person\".\"Name\", @p0, \"person\".\"Surname\", @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = ", ",
                ["@p1"] = value
            }, result.Parameters);
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

        [Theory]
        [MemberData(nameof(DataObject))]
        public void Select_Expression_Params(object value)
        {
            Person person = null;
            IQuery query = sql.Query.Select(() => person.Name, () => ", ", () => person.Surname, () => value);

            QueryResult result = engine.Compile(query);

            Assert.Equal("SELECT \"person\".\"Name\", @p0, \"person\".\"Surname\", @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = ", ",
                ["@p1"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataObject))]
        public void Select_Expression_Enumerable(object value)
        {
            Person person = null;
            IQuery query = sql.Query.Select(new List<Expression<Func<object>>> { () => person.Name, () => ", ",
                () => person.Surname, () => value });

            QueryResult result = engine.Compile(query);

            Assert.Equal("SELECT \"person\".\"Name\", @p0, \"person\".\"Surname\", @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = ", ",
                ["@p1"] = value
            }, result.Parameters);
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