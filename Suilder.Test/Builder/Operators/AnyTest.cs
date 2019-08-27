using System;
using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Functions;
using Xunit;

namespace Suilder.Test.Builder.Operators
{
    public class AnyTest : BuilderBaseTest
    {
        [Fact]
        public void Builder_Object()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Any(sql.RawQuery("Subquery"));

            QueryResult result = engine.Compile(op);

            Assert.Equal("ANY (Subquery)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Method()
        {
            IOperator op = sql.Op(() => SqlExp.Any(sql.RawQuery("Subquery")));

            QueryResult result = engine.Compile(op);

            Assert.Equal("ANY (Subquery)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Method_Invalid_Call()
        {
            Exception ex = Assert.Throws<InvalidOperationException>(() => SqlExp.Any(sql.RawQuery("Subquery")));
            Assert.Equal("Only for expressions.", ex.Message);
        }

        [Fact]
        public void To_String()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Any(sql.RawQuery("Subquery"));

            Assert.Equal("ANY (Subquery)", op.ToString());
        }
    }
}