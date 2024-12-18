using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Alias.ClassAlias
{
    public class AliasTest : BuilderBaseTest
    {
        [Fact]
        public void Expression()
        {
            Person person = null;
            IAlias alias = sql.Alias(() => person);

            QueryResult result = engine.Compile(alias);

            Assert.Equal("\"Person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_With_Translation()
        {
            Department dept = null;
            IAlias alias = sql.Alias(() => dept);

            QueryResult result = engine.Compile(alias);

            Assert.Equal("\"Dept\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Object_Overload()
        {
            Person person = null;
            Expression<Func<object>> expression = () => person;
            IAlias alias = sql.Alias(expression);

            QueryResult result = engine.Compile(alias);

            Assert.Equal("\"Person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Lambda_Overload()
        {
            Person person = null;
            Expression<Func<object>> expression = () => person;
            IAlias alias = sql.Alias((LambdaExpression)expression);

            QueryResult result = engine.Compile(alias);

            Assert.Equal("\"Person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Body_Overload()
        {
            Person person = null;
            Expression<Func<object>> expression = () => person;
            IAlias alias = sql.Alias(expression.Body);

            QueryResult result = engine.Compile(alias);

            Assert.Equal("\"Person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Invalid_Expression_Property()
        {
            Person person = null;

            Exception ex = Assert.Throws<ArgumentException>(() => sql.Alias(() => person.Name));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        [Fact]
        public void Invalid_Expression_Property_Object_Overload()
        {
            Person person = null;
            Expression<Func<object>> expression = () => person.Name;

            Exception ex = Assert.Throws<ArgumentException>(() => sql.Alias(expression));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        [Fact]
        public void To_String()
        {
            Person person = null;
            IAlias alias = sql.Alias(() => person);

            Assert.Equal("Person", alias.ToString());
        }
    }
}