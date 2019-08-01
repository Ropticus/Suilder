using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.ArithOperators
{
    public class ArithOperatorTest : BaseTest
    {
        [Fact]
        public void Combined1()
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => person.Salary + person.Salary * 0.5m);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Salary\" + (\"person\".\"Salary\" * @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = 0.5m }, result.Parameters);
        }

        [Fact]
        public void Combined2()
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => person.Salary * 0.5m + person.Salary);

            QueryResult result = engine.Compile(op);

            Assert.Equal("(\"person\".\"Salary\" * @p0) + \"person\".\"Salary\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = 0.5m }, result.Parameters);;
        }

        [Fact]
        public void Combined3()
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => (person.Salary + person.Salary) * 0.5m);

            QueryResult result = engine.Compile(op);

            Assert.Equal("(\"person\".\"Salary\" + \"person\".\"Salary\") * @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = 0.5m }, result.Parameters);
        }

        [Fact]
        public void Combined4()
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => person.Salary / 2 + person.Salary * 0.5m - 100);

            QueryResult result = engine.Compile(op);

            Assert.Equal("((\"person\".\"Salary\" / @p0) + (\"person\".\"Salary\" * @p1)) - @p2", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = 2m, ["@p1"] = 0.5m, ["@p2"] = 100m },
                result.Parameters);
        }

        [Fact]
        public void With_Bool_Operator()
        {
            Person person = null;
            IOperator op = (IOperator)sql.Op(() => person.Salary * 2 > 2000);

            QueryResult result = engine.Compile(op);

            Assert.Equal("(\"person\".\"Salary\" * @p0) > @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = 2m, ["@p1"] = 2000m }, result.Parameters);
        }
    }
}