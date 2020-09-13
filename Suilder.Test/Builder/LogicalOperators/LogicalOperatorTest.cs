using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Extensions;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.LogicalOperators
{
    public class LogicalOperatorTest : BuilderBaseTest
    {
        [Fact]
        public void Or_Parentheses()
        {
            Person person = null;
            IOperator op = sql.Op(() => (person.Id == 1 || person.Active) && person.Name.Like("%abcd%"));

            QueryResult result = engine.Compile(op);

            Assert.Equal("(\"person\".\"Id\" = @p0 OR \"person\".\"Active\" = @p1) "
                + "AND \"person\".\"Name\" LIKE @p2", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = true,
                ["@p2"] = "%abcd%"
            }, result.Parameters);
        }

        [Fact]
        public void And_Parentheses()
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Id == 1 || (person.Active && person.Name.Like("%abcd%")));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" = @p0 OR (\"person\".\"Active\" = @p1 "
                + "AND \"person\".\"Name\" LIKE @p2)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = true,
                ["@p2"] = "%abcd%"
            }, result.Parameters);
        }
    }
}