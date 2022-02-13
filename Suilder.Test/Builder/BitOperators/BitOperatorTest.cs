using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.BitOperators
{
    public class BitOperatorTest : BuilderBaseTest
    {
        [Fact]
        public void And_Or()
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => person.Flags & 1ul | person.Flags);

            QueryResult result = engine.Compile(op);

            Assert.Equal("(\"person\".\"Flags\" & @p0) | \"person\".\"Flags\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1ul
            }, result.Parameters);
        }

        [Fact]
        public void And_Or_Parentheses()
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => person.Flags & (1ul | person.Flags));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Flags\" & (@p0 | \"person\".\"Flags\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1ul
            }, result.Parameters);
        }

        [Fact]
        public void And_Or_Xor()
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => person.Flags & 1ul | person.Flags ^ 2ul);

            QueryResult result = engine.Compile(op);

            Assert.Equal("(\"person\".\"Flags\" & @p0) | (\"person\".\"Flags\" ^ @p1)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1ul,
                ["@p1"] = 2ul
            }, result.Parameters);
        }

        [Fact]
        public void And_Or_Parentheses_Xor()
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => (person.Flags & 1ul | person.Flags) ^ 2ul);

            QueryResult result = engine.Compile(op);

            Assert.Equal("((\"person\".\"Flags\" & @p0) | \"person\".\"Flags\") ^ @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1ul,
                ["@p1"] = 2ul
            }, result.Parameters);
        }

        [Fact]
        public void Not_And_Or()
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => ~person.Flags & 1ul | person.Flags);

            QueryResult result = engine.Compile(op);

            Assert.Equal("((~ \"person\".\"Flags\") & @p0) | \"person\".\"Flags\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1ul
            }, result.Parameters);
        }

        [Fact]
        public void Not_Parentheses_And_Or()
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => ~(person.Flags & 1ul) | person.Flags);

            QueryResult result = engine.Compile(op);

            Assert.Equal("(~ (\"person\".\"Flags\" & @p0)) | \"person\".\"Flags\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1ul
            }, result.Parameters);
        }

        [Fact]
        public void With_Arith_Operator()
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => (person.Flags + 1ul) & (person.Flags | 2ul));

            QueryResult result = engine.Compile(op);

            Assert.Equal("(\"person\".\"Flags\" + @p0) & (\"person\".\"Flags\" | @p1)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1ul,
                ["@p1"] = 2ul
            }, result.Parameters);
        }

        [Fact]
        public void With_Comparison_Operator()
        {
            Person person = null;
            IOperator op = sql.Op(() => (person.Flags & 1ul) > 2ul);

            QueryResult result = engine.Compile(op);

            Assert.Equal("(\"person\".\"Flags\" & @p0) > @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1ul,
                ["@p1"] = 2ul
            }, result.Parameters);
        }

        [Fact]
        public void With_Logical_Operator_Left()
        {
            Person person = null;
            IOperator op = sql.Op(() => (person.Flags & 1ul) > 2ul & person.Name == "abcd");

            QueryResult result = engine.Compile(op);

            Assert.Equal("(\"person\".\"Flags\" & @p0) > @p1 AND \"person\".\"Name\" = @p2", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1ul,
                ["@p1"] = 2ul,
                ["@p2"] = "abcd"
            }, result.Parameters);
        }

        [Fact]
        public void With_Logical_Operator_Right()
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Name == "abcd" & (person.Flags & 1ul) > 2ul);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" = @p0 AND (\"person\".\"Flags\" & @p1) > @p2", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "abcd",
                ["@p1"] = 1ul,
                ["@p2"] = 2ul
            }, result.Parameters);
        }
    }
}