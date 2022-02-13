using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Functions
{
    public class ConcatTest : BuilderBaseTest
    {
        [Theory]
        [MemberData(nameof(DataString))]
        public void Expression(string value)
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => person.Name + value);

            QueryResult result = engine.Compile(func);

            Assert.Equal("CONCAT(\"person\".\"Name\", @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Inline_Value()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => person.Name + "abcd");

            QueryResult result = engine.Compile(func);

            Assert.Equal("CONCAT(\"person\".\"Name\", @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "abcd"
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataString))]
        public void Expression_Values(string value)
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => person.Name + "abcd" + value);

            QueryResult result = engine.Compile(func);

            Assert.Equal("CONCAT(\"person\".\"Name\", @p0, @p1)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "abcd",
                ["@p1"] = value
            }, result.Parameters);
        }

        [Theory]
        [InlineData("efgh", "ijkl", "mnop")]
        public void Expression_Large(string value1, string value2, string value3)
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => person.Name + "abcd" + value1 + value2 + value3 + "qrst");

            QueryResult result = engine.Compile(func);

            Assert.Equal("CONCAT(\"person\".\"Name\", @p0, @p1, @p2, @p3, @p4)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "abcd",
                ["@p1"] = value1,
                ["@p2"] = value2,
                ["@p3"] = value3,
                ["@p4"] = "qrst"
            }, result.Parameters);
        }

        [Theory]
        [InlineData(200, 300, 400)]
        public void Expression_Number(int value1, long value2, decimal value3)
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => person.Name + 100m + value1 + value2 + value3 + 500);

            QueryResult result = engine.Compile(func);

            Assert.Equal("CONCAT(\"person\".\"Name\", @p0, @p1, @p2, @p3, @p4)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 100m,
                ["@p1"] = value1,
                ["@p2"] = value2,
                ["@p3"] = value3,
                ["@p4"] = 500
            }, result.Parameters);
        }

        [Theory]
        [InlineData(200, 300, 400)]
        public void Expression_Number_Left(int value1, long value2, decimal value3)
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => person.Salary + 100m + person.Name + value1 + value2 + value3 + 500);

            QueryResult result = engine.Compile(func);

            Assert.Equal("CONCAT(\"person\".\"Salary\" + @p0, \"person\".\"Name\", @p1, @p2, @p3, @p4)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 100m,
                ["@p1"] = value1,
                ["@p2"] = value2,
                ["@p3"] = value3,
                ["@p4"] = 500
            }, result.Parameters);
        }

        [Theory]
        [InlineData(200, 300, 400)]
        public void Expression_Number_Parentheses_Center(int value1, long value2, decimal value3)
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => person.Name + 100m + (value1 + value2) + value3 + 500);

            QueryResult result = engine.Compile(func);

            Assert.Equal("CONCAT(\"person\".\"Name\", @p0, @p1 + @p2, @p3, @p4)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 100m,
                ["@p1"] = value1,
                ["@p2"] = value2,
                ["@p3"] = value3,
                ["@p4"] = 500
            }, result.Parameters);
        }

        [Theory]
        [InlineData(200, 300, 400)]
        public void Expression_Number_Parentheses_Right(decimal value1, long value2, int value3)
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => person.Name + 100m + value1 + value2 + (value3 + 500));

            QueryResult result = engine.Compile(func);

            Assert.Equal("CONCAT(\"person\".\"Name\", @p0, @p1, @p2, @p3 + @p4)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 100m,
                ["@p1"] = value1,
                ["@p2"] = value2,
                ["@p3"] = value3,
                ["@p4"] = 500
            }, result.Parameters);
        }

        [Theory]
        [InlineData(200, 300, 400)]
        public void Expression_Number_Parentheses_Both(int value1, long value2, short value3)
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => person.Name + 100m + (value1 + value2) + (value3 + 500));

            QueryResult result = engine.Compile(func);

            Assert.Equal("CONCAT(\"person\".\"Name\", @p0, @p1 + @p2, @p3 + @p4)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 100m,
                ["@p1"] = value1,
                ["@p2"] = value2,
                ["@p3"] = value3,
                ["@p4"] = 500
            }, result.Parameters);
        }
    }
}