using System;
using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Functions;
using Xunit;

namespace Suilder.Test.Builder.QueryOperators
{
    public class ExistsTest : BuilderBaseTest
    {
        [Fact]
        public void Builder_Object()
        {
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
        public void Expression_Method_Invalid_Call()
        {
            Exception ex = Assert.Throws<NotSupportedException>(() => SqlExp.Exists(sql.RawQuery("Subquery")));
            Assert.Equal("Only for expressions.", ex.Message);
        }

        [Fact]
        public void To_String()
        {
            IOperator op = sql.Exists(sql.RawQuery("Subquery"));

            Assert.Equal("EXISTS (Subquery)", op.ToString());
        }
    }
}