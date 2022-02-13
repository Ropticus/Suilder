using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.ArithOperators
{
    public class ArithOperatorTest : BuilderBaseTest
    {
        [Fact]
        public void Add_Multiply()
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => person.Salary + person.Salary * 0.5m);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Salary\" + (\"person\".\"Salary\" * @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 0.5m
            }, result.Parameters);
        }

        [Fact]
        public void Add_Parentheses_Multiply()
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => (person.Salary + person.Salary) * 0.5m);

            QueryResult result = engine.Compile(op);

            Assert.Equal("(\"person\".\"Salary\" + \"person\".\"Salary\") * @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 0.5m
            }, result.Parameters);
        }

        [Fact]
        public void Multiply_Add()
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => person.Salary * 0.5m + person.Salary);

            QueryResult result = engine.Compile(op);

            Assert.Equal("(\"person\".\"Salary\" * @p0) + \"person\".\"Salary\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 0.5m
            }, result.Parameters);
        }

        [Fact]
        public void Divide_Add_Multiply_Subtract()
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => person.Salary / 2 + person.Salary * 0.5m - 100);

            QueryResult result = engine.Compile(op);

            Assert.Equal("((\"person\".\"Salary\" / @p0) + (\"person\".\"Salary\" * @p1)) - @p2", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 2m,
                ["@p1"] = 0.5m,
                ["@p2"] = 100m
            }, result.Parameters);
        }

        [Fact]
        public void Negate_Add_Multiply()
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => -person.Salary + person.Salary * 0.5m);

            QueryResult result = engine.Compile(op);

            Assert.Equal("(- \"person\".\"Salary\") + (\"person\".\"Salary\" * @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 0.5m
            }, result.Parameters);
        }

        [Fact]
        public void Negate_Parentheses_Add_Multiply()
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => -(person.Salary + person.Salary) * 0.5m);

            QueryResult result = engine.Compile(op);

            Assert.Equal("(- (\"person\".\"Salary\" + \"person\".\"Salary\")) * @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 0.5m
            }, result.Parameters);
        }

        [Fact]
        public void Add_Negate_Parentheses_Negate_Add()
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => person.Salary + -(-person.Salary + 0.5m));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Salary\" + (- ((- \"person\".\"Salary\") + @p0))", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 0.5m
            }, result.Parameters);
        }

        [Fact]
        public void With_Bit_Operator()
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => (person.Id + person.Id) * (person.Id & person.Id));

            QueryResult result = engine.Compile(op);

            Assert.Equal("(\"person\".\"Id\" + \"person\".\"Id\") * (\"person\".\"Id\" & \"person\".\"Id\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void With_Comparison_Operator()
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Salary * 2 > 3000);

            QueryResult result = engine.Compile(op);

            Assert.Equal("(\"person\".\"Salary\" * @p0) > @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 2m,
                ["@p1"] = 3000m
            }, result.Parameters);
        }

        [Fact]
        public void With_Logical_Operator_Left()
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Salary * 2 > 3000 & person.Name == "abcd");

            QueryResult result = engine.Compile(op);

            Assert.Equal("(\"person\".\"Salary\" * @p0) > @p1 AND \"person\".\"Name\" = @p2", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 2m,
                ["@p1"] = 3000m,
                ["@p2"] = "abcd"
            }, result.Parameters);
        }

        [Fact]
        public void With_Logical_Operator_Right()
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Name == "abcd" & (person.Salary * 2) > 3000);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" = @p0 AND (\"person\".\"Salary\" * @p1) > @p2", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "abcd",
                ["@p1"] = 2m,
                ["@p2"] = 3000m
            }, result.Parameters);
        }
    }
}