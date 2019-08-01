using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Xunit;

namespace Suilder.Test.Builder
{
    public class ParametersTest : BaseTest
    {
        [Fact]
        public void Test()
        {
            IRawSql raw = sql.Raw("{0} {1} {2} {3} {4}", 1, "text", null, new int[] { 1, 2, 3 },
                new string[] { "a", null, "c" });

            QueryResult result = engine.Compile(raw);

            Assert.Equal("@p0 @p1 NULL (@p2, @p3, @p4) (@p5, NULL, @p6)", result.Sql);
            Assert.Equal(new Dictionary<string, object>()
            {
                ["@p0"] = 1,
                ["@p1"] = "text",
                ["@p2"] = 1,
                ["@p3"] = 2,
                ["@p4"] = 3,
                ["@p5"] = "a",
                ["@p6"] = "c"
            }, result.Parameters);
        }
    }
}