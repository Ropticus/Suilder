using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Extensions;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Query
{
    public class HavingTest : BuilderBaseTest
    {
        [Fact]
        public void Having_Value()
        {
            IAlias person = sql.Alias("person");
            IQuery query = sql.Query
                .Having(person["Id"].Eq(1));

            QueryResult result = engine.Compile(query);

            Assert.Equal("HAVING \"person\".\"Id\" = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1
            }, result.Parameters);
        }

        [Fact]
        public void Having_Value_Logical()
        {
            IAlias person = sql.Alias("person");
            IQuery query = sql.Query
                .Having(sql.And.Add(person["Id"].Gt(10), person["Id"].Lt(20)));

            QueryResult result = engine.Compile(query);

            Assert.Equal("HAVING \"person\".\"Id\" > @p0 AND \"person\".\"Id\" < @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 10,
                ["@p1"] = 20
            }, result.Parameters);
        }

        [Fact]
        public void Having_Expression()
        {
            Person person = null;
            IQuery query = sql.Query
                .Having(() => person.Id == 1);

            QueryResult result = engine.Compile(query);

            Assert.Equal("HAVING \"person\".\"Id\" = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1
            }, result.Parameters);
        }

        [Fact]
        public void Having_Expression_Logical()
        {
            Person person = null;
            IQuery query = sql.Query
                .Having(() => person.Id > 10 && person.Id < 20);

            QueryResult result = engine.Compile(query);

            Assert.Equal("HAVING \"person\".\"Id\" > @p0 AND \"person\".\"Id\" < @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 10,
                ["@p1"] = 20
            }, result.Parameters);
        }

        [Fact]
        public void Having_Raw()
        {
            IAlias person = sql.Alias("person");
            IQuery query = sql.Query.Having(sql.Raw("HAVING {0} = {1}", person["Id"], 1));

            QueryResult result = engine.Compile(query);

            Assert.Equal("HAVING \"person\".\"Id\" = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1
            }, result.Parameters);
        }
    }
}