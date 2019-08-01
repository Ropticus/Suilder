using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Functions;
using Xunit;

namespace Suilder.Test.Builder.Operators
{
    public class ExistsTest : BaseTest
    {
        [Fact]
        public void Builder()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Exists(sql.RawQuery("Subquery"));

            QueryResult result = engine.Compile(op);

            Assert.Equal("EXISTS (Subquery)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Method()
        {
            IOperator op = sql.Op(() => SqlExp.Exists(sql.RawQuery("Subquery")));

            QueryResult result = engine.Compile(op);

            Assert.Equal("EXISTS (Subquery)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void To_String()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Exists(sql.RawQuery("Subquery"));

            Assert.Equal("EXISTS (Subquery)", op.ToString());
        }
    }
}