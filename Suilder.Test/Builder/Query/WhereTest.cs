using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Extensions;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Query
{
    public class WhereTest : BuilderBaseTest
    {
        [Fact]
        public void Where_Value()
        {
            IAlias person = sql.Alias("person");
            IQuery query = sql.Query
                .Where(person["Id"].Eq(1));

            QueryResult result = engine.Compile(query);

            Assert.Equal("WHERE \"person\".\"Id\" = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1
            }, result.Parameters);
        }

        [Fact]
        public void Where_Expression()
        {
            Person person = null;
            IQuery query = sql.Query
                .Where(() => person.Id == 1);

            QueryResult result = engine.Compile(query);

            Assert.Equal("WHERE \"person\".\"Id\" = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1
            }, result.Parameters);
        }


        [Fact]
        public void Where_Raw()
        {
            IAlias person = sql.Alias("person");
            IQuery query = sql.Query.Where(sql.Raw("WHERE {0} = {1}", person["Id"], 1));

            QueryResult result = engine.Compile(query);

            Assert.Equal("WHERE \"person\".\"Id\" = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1
            }, result.Parameters);
        }
    }
}