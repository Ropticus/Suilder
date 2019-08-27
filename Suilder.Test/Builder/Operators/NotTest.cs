using System;
using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Extensions;
using Suilder.Functions;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Operators
{
    public class NotTest : BuilderBaseTest
    {
        [Fact]
        public void Builder_Object()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Not(person["Id"].Eq(1));

            QueryResult result = engine.Compile(op);

            Assert.Equal("NOT \"person\".\"Id\" = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1
            }, result.Parameters);
        }

        [Fact]
        public void Builder_Object_Right_Null()
        {
            IOperator op = sql.Not(null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("NOT NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Builder_Expression()
        {
            Person person = null;
            IOperator op = sql.Not(() => person.Id == 1);

            QueryResult result = engine.Compile(op);

            Assert.Equal("NOT \"person\".\"Id\" = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1
            }, result.Parameters);
        }

        [Fact]
        public void Extension_Object()
        {
            IAlias person = sql.Alias("person");
            IOperator op = person["Id"].Eq(1).Not();

            QueryResult result = engine.Compile(op);

            Assert.Equal("NOT \"person\".\"Id\" = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1
            }, result.Parameters);
        }

        [Fact]
        public void Expression()
        {
            Person person = null;
            IOperator op = sql.Op(() => !(person.Id == 1));

            QueryResult result = engine.Compile(op);

            Assert.Equal("NOT \"person\".\"Id\" = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Method()
        {
            Person person = null;
            IOperator op = sql.Op(() => SqlExp.Not(person.Id == 1));

            QueryResult result = engine.Compile(op);

            Assert.Equal("NOT \"person\".\"Id\" = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Method_Invalid_Call()
        {
            Person person = new Person();

            Exception ex = Assert.Throws<InvalidOperationException>(() => SqlExp.Not(person.Id == 1));
            Assert.Equal("Only for expressions.", ex.Message);
        }

        [Fact]
        public void Expression_Bool_Function()
        {
            ExpressionProcessor.AddFunction(typeof(CustomExp), nameof(CustomExp.IsNumeric));

            Person person = null;
            IOperator op = sql.Op(() => !CustomExp.IsNumeric(person.Id));

            QueryResult result = engine.Compile(op);

            Assert.Equal("NOT ISNUMERIC(\"person\".\"Id\") = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = true
            }, result.Parameters);
        }

        [Fact]
        public void Invalid_Expression_Bool_Function()
        {
            Person person = new Person();

            Exception ex = Assert.Throws<ArgumentException>(() => sql.Op(() => !CustomExp.Invalid(person.Id)));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        [Fact]
        public void To_String()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Not(person["Id"].Eq(1));

            Assert.Equal("NOT person.Id = 1", op.ToString());
        }

        private static class CustomExp
        {
            public static bool IsNumeric(object value)
            {
                throw new InvalidOperationException("Only for expressions.");
            }

            public static bool Invalid(object value)
            {
                return true;
            }
        }
    }
}