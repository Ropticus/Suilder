using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.FunctionOperators
{
    public class CoalesceTest : BuilderBaseTest
    {
        [Theory]
        [MemberData(nameof(DataString))]
        public void Expression(string value)
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => person.Name ?? value);

            QueryResult result = engine.Compile(func);

            Assert.Equal("COALESCE(\"person\".\"Name\", @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Inline_Value()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => person.Name ?? "abcd");

            QueryResult result = engine.Compile(func);

            Assert.Equal("COALESCE(\"person\".\"Name\", @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "abcd"
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Column()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => person.Name ?? person.SurName);

            QueryResult result = engine.Compile(func);

            Assert.Equal("COALESCE(\"person\".\"Name\", \"person\".\"SurName\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataString))]
        public void Expression_Multiple(string value)
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => person.Name ?? person.SurName ?? value);

            QueryResult result = engine.Compile(func);

            Assert.Equal("COALESCE(\"person\".\"Name\", COALESCE(\"person\".\"SurName\", @p0))", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }
    }
}