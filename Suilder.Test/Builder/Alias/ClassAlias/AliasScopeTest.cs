using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Alias.ClassAlias
{
    public class AliasScopeTest : BuilderBaseTest
    {
        public Person personField;

        public Person PersonProperty { get; set; }

        public static Person personFieldStatic;

        public static Person PersonPropertyStatic { get; set; }

        [Fact]
        public void Field()
        {
            IAlias alias = sql.Alias(() => personField);

            QueryResult result = engine.Compile(alias);

            Assert.Equal("\"Person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Property()
        {
            IAlias alias = sql.Alias(() => PersonProperty);

            QueryResult result = engine.Compile(alias);

            Assert.Equal("\"Person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Field_Static()
        {
            IAlias alias = sql.Alias(() => personFieldStatic);

            QueryResult result = engine.Compile(alias);

            Assert.Equal("\"Person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Property_Static()
        {
            IAlias alias = sql.Alias(() => PersonPropertyStatic);

            QueryResult result = engine.Compile(alias);

            Assert.Equal("\"Person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Nested_Field_Static()
        {
            IAlias alias = sql.Alias(() => Tables.person);

            QueryResult result = engine.Compile(alias);

            Assert.Equal("\"Person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Nested_Property_Static()
        {
            IAlias alias = sql.Alias(() => Tables.Person);

            QueryResult result = engine.Compile(alias);

            Assert.Equal("\"Person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Field_Object_Overload()
        {
            Expression<Func<object>> expression = () => personField;
            IAlias alias = sql.Alias(expression);

            QueryResult result = engine.Compile(alias);

            Assert.Equal("\"Person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Property_Object_Overload()
        {
            Expression<Func<object>> expression = () => PersonProperty;
            IAlias alias = sql.Alias(expression);

            QueryResult result = engine.Compile(alias);

            Assert.Equal("\"Person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Field_Static_Object_Overload()
        {
            Expression<Func<object>> expression = () => personFieldStatic;
            IAlias alias = sql.Alias(expression);

            QueryResult result = engine.Compile(alias);

            Assert.Equal("\"Person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Property_Static_Object_Overload()
        {
            Expression<Func<object>> expression = () => PersonPropertyStatic;
            IAlias alias = sql.Alias(expression);

            QueryResult result = engine.Compile(alias);

            Assert.Equal("\"Person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Nested_Field_Static_Object_Overload()
        {
            Expression<Func<object>> expression = () => Tables.person;
            IAlias alias = sql.Alias(expression);

            QueryResult result = engine.Compile(alias);

            Assert.Equal("\"Person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Nested_Property_Static_Object_Overload()
        {
            Expression<Func<object>> expression = () => Tables.Person;
            IAlias alias = sql.Alias(expression);

            QueryResult result = engine.Compile(alias);

            Assert.Equal("\"Person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Invalid_Nested_Field()
        {
            TablesError tables = null;

            Exception ex = Assert.Throws<ArgumentException>(() => sql.Alias(() => tables.person));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        [Fact]
        public void Invalid_Nested_Property()
        {
            TablesError tables = null;

            Exception ex = Assert.Throws<ArgumentException>(() => sql.Alias(() => tables.Person));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        [Fact]
        public void Invalid_Nested_Field_Object_Overload()
        {
            TablesError tables = null;
            Expression<Func<object>> expression = () => tables.person;

            Exception ex = Assert.Throws<ArgumentException>(() => sql.Alias(expression));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        [Fact]
        public void Invalid_Nested_Property_Object_Overload()
        {
            TablesError tables = null;
            Expression<Func<object>> expression = () => tables.Person;

            Exception ex = Assert.Throws<ArgumentException>(() => sql.Alias(expression));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        protected static class Tables
        {
            public static Person person;

            public static Person Person { get; set; }
        }

        protected class TablesError
        {
            public Person person;

            public Person Person { get; set; }
        }
    }
}