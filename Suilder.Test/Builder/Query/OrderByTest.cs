using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Xunit;

namespace Suilder.Test.Builder.Query
{
    public class OrderByTest : BaseTest
    {
        [Fact]
        public void OrderBy_Func()
        {
            IAlias person = sql.Alias("person");
            IQuery query = sql.Query.OrderBy(x => x
                .Add(person["Name"])
                .Add(person["SurName"]));

            QueryResult result = engine.Compile(query);

            Assert.Equal("ORDER BY \"person\".\"Name\", \"person\".\"SurName\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void OrderBy_Value()
        {
            IAlias person = sql.Alias("person");
            IQuery query = sql.Query.OrderBy(sql.OrderBy()
                .Add(person["Name"])
                .Add(person["SurName"]));

            QueryResult result = engine.Compile(query);

            Assert.Equal("ORDER BY \"person\".\"Name\", \"person\".\"SurName\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void OrderBy_Raw()
        {
            IAlias person = sql.Alias("person");
            IQuery query = sql.Query.OrderBy(sql.Raw("ORDER BY {0}, {1}", person["Name"], person["SurName"]));

            QueryResult result = engine.Compile(query);

            Assert.Equal("ORDER BY \"person\".\"Name\", \"person\".\"SurName\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }
    }
}