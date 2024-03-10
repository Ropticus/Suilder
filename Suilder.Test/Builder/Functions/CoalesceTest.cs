using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Functions
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
            IFunction func = (IFunction)sql.Val(() => person.Name ?? person.Surname);

            QueryResult result = engine.Compile(func);

            Assert.Equal("COALESCE(\"person\".\"Name\", \"person\".\"Surname\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataString))]
        public void Expression_Multiple(string value)
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => person.Name ?? person.Surname ?? value);

            QueryResult result = engine.Compile(func);

            Assert.Equal("COALESCE(\"person\".\"Name\", COALESCE(\"person\".\"Surname\", @p0))", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }
    }
}