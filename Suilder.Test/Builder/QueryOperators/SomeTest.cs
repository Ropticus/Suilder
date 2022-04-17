using System;
using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Functions;
using Suilder.Operators;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.QueryOperators
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
        public void Expression_Method_Generic()
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Id > SqlExp.Some<int>(sql.RawQuery("Subquery")));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" > SOME (Subquery)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Method_Invalid_Call()
        {
            Exception ex = Assert.Throws<NotSupportedException>(() => SqlExp.Some(sql.RawQuery("Subquery")));
            Assert.Equal("Only for expressions.", ex.Message);
        }

        [Fact]
        public void Expression_Method_Generic_Invalid_Call()
        {
            Exception ex = Assert.Throws<NotSupportedException>(() => SqlExp.Some<int>(sql.RawQuery("Subquery")));
            Assert.Equal("Only for expressions.", ex.Message);
        }

        [Fact]
        public void Translation()
        {
            engine.AddOperator(OperatorName.Some, "TRANSLATED");

            IOperator op = sql.Some(sql.RawQuery("Subquery"));

            QueryResult result = engine.Compile(op);

            Assert.Equal("TRANSLATED (Subquery)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Translation_Function()
        {
            engine.AddOperator(OperatorName.Some, "TRANSLATED", true);

            IOperator op = sql.Some(sql.RawQuery("Subquery"));

            QueryResult result = engine.Compile(op);

            Assert.Equal("TRANSLATED((Subquery))", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void To_String()
        {
            IOperator op = sql.Some(sql.RawQuery("Subquery"));

            Assert.Equal("SOME (Subquery)", op.ToString());
        }
    }
}