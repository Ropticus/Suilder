using System;
using System.Linq.Expressions;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Alias
{
    public class ClassAliasScopeTest : BuilderBaseTest
    {
        private static Person person = null;

        private static Person Person { get; set; }

        private Person personNonStatic = null;

        private Person PersonNonStatic { get; set; }

        [Fact]
        public void Class_Scope_Static_Field()
        {
            IAlias alias = sql.Alias(() => person);

            QueryResult result = engine.Compile(alias);

            Assert.Equal("\"Person\"", result.Sql);
        }

        [Fact]
        public void Class_Scope_Static_Property()
        {
            IAlias alias = sql.Alias(() => Person);

            QueryResult result = engine.Compile(alias);

            Assert.Equal("\"Person\"", result.Sql);
        }

        [Fact]
        public void Class_Scope_Field()
        {
            IAlias alias = sql.Alias(() => personNonStatic);

            QueryResult result = engine.Compile(alias);

            Assert.Equal("\"Person\"", result.Sql);
        }

        [Fact]
        public void Class_Scope_Property()
        {
            IAlias alias = sql.Alias(() => PersonNonStatic);

            QueryResult result = engine.Compile(alias);

            Assert.Equal("\"Person\"", result.Sql);
        }

        [Fact]
        public void Class_Scope_Static_Field_Object_Overload()
        {
            Expression<Func<object>> expression = () => person;
            IAlias alias = sql.Alias(expression);

            QueryResult result = engine.Compile(alias);

            Assert.Equal("\"Person\"", result.Sql);
        }

        [Fact]
        public void Class_Scope_Static_Property_Object_Overload()
        {
            Expression<Func<object>> expression = () => Person;
            IAlias alias = sql.Alias(expression);

            QueryResult result = engine.Compile(alias);

            Assert.Equal("\"Person\"", result.Sql);
        }

        [Fact]
        public void Class_Scope_Field_Object_Overload()
        {
            Expression<Func<object>> expression = () => personNonStatic;
            IAlias alias = sql.Alias(expression);

            QueryResult result = engine.Compile(alias);

            Assert.Equal("\"Person\"", result.Sql);
        }

        [Fact]
        public void Class_Scope_Property_Object_Overload()
        {
            Expression<Func<object>> expression = () => PersonNonStatic;
            IAlias alias = sql.Alias(expression);

            QueryResult result = engine.Compile(alias);

            Assert.Equal("\"Person\"", result.Sql);
        }
        [Fact]
        public void Nested_Static_Field()
        {
            IAlias alias = sql.Alias(() => Tables.person);

            QueryResult result = engine.Compile(alias);

            Assert.Equal("\"Person\"", result.Sql);
        }

        [Fact]
        public void Nested_Static_Property()
        {
            IAlias alias = sql.Alias(() => Tables.Person);

            QueryResult result = engine.Compile(alias);

            Assert.Equal("\"Person\"", result.Sql);
        }

        [Fact]
        public void Invalid_Nested_Field()
        {
            TablesNonStatic tables = null;

            Exception ex = Assert.Throws<ArgumentException>(() => sql.Alias(() => tables.person));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        [Fact]
        public void Invalid_Nested_Property()
        {
            TablesNonStatic tables = null;

            Exception ex = Assert.Throws<ArgumentException>(() => sql.Alias(() => tables.Person));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        [Fact]
        public void Nested_Static_Field_Object_Overload()
        {
            Expression<Func<object>> expression = () => Tables.person;
            IAlias alias = sql.Alias(expression);

            QueryResult result = engine.Compile(alias);

            Assert.Equal("\"Person\"", result.Sql);
        }

        [Fact]
        public void Nested_Static_Property_Object_Overload()
        {
            Expression<Func<object>> expression = () => Tables.Person;
            IAlias alias = sql.Alias(expression);

            QueryResult result = engine.Compile(alias);

            Assert.Equal("\"Person\"", result.Sql);
        }

        [Fact]
        public void Invalid_Nested_Field_Object_Overload()
        {
            TablesNonStatic tables = null;
            Expression<Func<object>> expression = () => tables.person;

            Exception ex = Assert.Throws<ArgumentException>(() => sql.Alias(expression));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        [Fact]
        public void Invalid_Nested_Property_Object_Overload()
        {
            TablesNonStatic tables = null;
            Expression<Func<object>> expression = () => tables.Person;

            Exception ex = Assert.Throws<ArgumentException>(() => sql.Alias(expression));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        protected static class Tables
        {
            public static Person person = null;

            public static Person Person { get; set; }

        }

        protected class TablesNonStatic
        {
            public Person person = null;

            public Person Person { get; set; }

        }
    }
}