using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.BitOperators
{
    public class BitOperatorTest : BaseTest
    {
        [Fact]
        public void Property_And_Value_Or_Value()
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => person.Id & 1 | 2);

            QueryResult result = engine.Compile(op);

            Assert.Equal("(\"person\".\"Id\" & @p0) | @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = 1, ["@p1"] = 2 }, result.Parameters);
        }

        [Fact]
        public void Property_And_Property_Or_Value()
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => person.Id & person.Id | 1);

            QueryResult result = engine.Compile(op);

            Assert.Equal("(\"person\".\"Id\" & \"person\".\"Id\") | @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = 1 }, result.Parameters);
        }

        [Fact]
        public void Property_And_Value_Or_Property_Xor_Value()
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => person.Id & 1 | person.Id ^ 2);

            QueryResult result = engine.Compile(op);

            Assert.Equal("(\"person\".\"Id\" & @p0) | (\"person\".\"Id\" ^ @p1)", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = 1, ["@p1"] = 2 }, result.Parameters);
        }

        [Fact]
        public void With_Arith_Operator()
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => (person.Id + person.Id) & (person.Id & person.Id));

            QueryResult result = engine.Compile(op);

            Assert.Equal("(\"person\".\"Id\" + \"person\".\"Id\") & (\"person\".\"Id\" & \"person\".\"Id\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void With_Bool_Operator()
        {
            Person person = null;
            IOperator op = (IOperator)sql.Op(() => (person.Id & 1) > 2);

            QueryResult result = engine.Compile(op);

            Assert.Equal("(\"person\".\"Id\" & @p0) > @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = 1, ["@p1"] = 2 }, result.Parameters);
        }
    }
}