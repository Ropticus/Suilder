using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Functions
{
    public class SystemFunctionTest : BuilderBaseTest
    {
        [Fact]
        public void Contains()
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Name.Contains("abcd"));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "%abcd%"
            }, result.Parameters);
        }

        [Fact]
        public void StartsWith()
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Name.StartsWith("abcd"));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "abcd%"
            }, result.Parameters);
        }

        [Fact]
        public void EndsWith()
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Name.EndsWith("abcd"));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "%abcd"
            }, result.Parameters);
        }
    }
}