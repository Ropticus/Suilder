using System;
using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Functions;
using Xunit;

namespace Suilder.Test.Builder.Operators
{
    public class SomeTest : BuilderBaseTest
    {
        [Fact]
        public void Builder_Object()
        {
            IOperator op = sql.Some(sql.RawQuery("Subquery"));

            QueryResult result = engine.Compile(op);

            Assert.Equal("SOME (Subquery)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Method()
        {
            IOperator op = sql.Op(() => SqlExp.Some(sql.RawQuery("Subquery")));

            QueryResult result = engine.Compile(op);

            Assert.Equal("SOME (Subquery)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Method_Invalid_Call()
        {
            Exception ex = Assert.Throws<InvalidOperationException>(() => SqlExp.Some(sql.RawQuery("Subquery")));
            Assert.Equal("Only for expressions.", ex.Message);
        }

        [Fact]
        public void To_String()
        {
            IOperator op = sql.Some(sql.RawQuery("Subquery"));

            Assert.Equal("SOME (Subquery)", op.ToString());
        }
    }
}