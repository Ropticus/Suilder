using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Query
{
    public class GroupByTest : BaseTest
    {
        [Fact]
        public void GroupBy()
        {
            IAlias person = sql.Alias("person");
            IQuery query = sql.Query.GroupBy(person["Name"]);

            QueryResult result = engine.Compile(query);

            Assert.Equal("GROUP BY \"person\".\"Name\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void GroupBy_Params()
        {
            IAlias person = sql.Alias("person");
            IQuery query = sql.Query.GroupBy(person["Name"], person["SurName"]);

            QueryResult result = engine.Compile(query);

            Assert.Equal("GROUP BY \"person\".\"Name\", \"person\".\"SurName\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void GroupBy_Enumerable()
        {
            IAlias person = sql.Alias("person");
            IQuery query = sql.Query.GroupBy(new List<object>() { person["Name"], person["SurName"] });

            QueryResult result = engine.Compile(query);

            Assert.Equal("GROUP BY \"person\".\"Name\", \"person\".\"SurName\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void GroupBy_Expression()
        {
            Person person = null;
            IQuery query = sql.Query.GroupBy(() => person.Name);

            QueryResult result = engine.Compile(query);

            Assert.Equal("GROUP BY \"person\".\"Name\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void GroupBy_Expression_Params()
        {
            Person person = null;
            IQuery query = sql.Query.GroupBy(() => person.Name, () => person.SurName);

            QueryResult result = engine.Compile(query);

            Assert.Equal("GROUP BY \"person\".\"Name\", \"person\".\"SurName\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void GroupBy_Expression_Enumerable()
        {
            Person person = null;
            IQuery query = sql.Query.GroupBy(new List<Expression<Func<object>>>() { () => person.Name,
                () => person.SurName });

            QueryResult result = engine.Compile(query);

            Assert.Equal("GROUP BY \"person\".\"Name\", \"person\".\"SurName\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void GroupBy_Func()
        {
            IAlias person = sql.Alias("person");
            IQuery query = sql.Query.GroupBy(x => x.Add(person["Name"]));

            QueryResult result = engine.Compile(query);

            Assert.Equal("GROUP BY \"person\".\"Name\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void GroupBy_Value()
        {
            IAlias person = sql.Alias("person");
            IQuery query = sql.Query.GroupBy(sql.ValList.Add(person["Name"]));

            QueryResult result = engine.Compile(query);

            Assert.Equal("GROUP BY \"person\".\"Name\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void GroupBy_Raw()
        {
            IAlias person = sql.Alias("person");
            IQuery query = sql.Query.GroupBy(sql.Raw("GROUP BY {0}", person["Name"]));

            QueryResult result = engine.Compile(query);

            Assert.Equal("GROUP BY \"person\".\"Name\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }
    }
}