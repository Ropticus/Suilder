using System;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Exceptions;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Alias
{
    public class ClassAliasColumnTest : BaseTest
    {
        [Fact]
        public void Expression()
        {
            Person person = null;
            IColumn column = sql.Col(() => person.Id);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\"", result.Sql);
        }

        [Fact]
        public void Expression_Nested()
        {
            Person person = null;
            IColumn column = sql.Col(() => person.Department.Id);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"DepartmentId\"", result.Sql);
        }

        [Fact]
        public void Expression_Translation()
        {
            Person person = null;
            IColumn column = sql.Col(() => person.Created);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"DateCreated\"", result.Sql);
        }

        [Fact]
        public void All()
        {
            Person person = null;
            IColumn column = sql.Col(() => person);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\", \"person\".\"Active\", \"person\".\"Name\", \"person\".\"SurName\", "
                + "\"person\".\"Salary\", \"person\".\"DateCreated\", \"person\".\"DepartmentId\"", result.Sql);
        }

        [Fact]
        public void Ignored_Property()
        {
            Person person = null;
            IColumn column = sql.Col(() => person.Ignore);

            Exception ex = Assert.Throws<InvalidConfigurationException>(() => engine.Compile(column));
            Assert.Equal($"Property \"Ignore\" for type \"{typeof(Person).FullName}\" is not registered.", ex.Message);
        }

        [Fact]
        public void Invalid_Property()
        {
            Person person = null;
            IColumn column = sql.Col(() => person.FullName);

            Exception ex = Assert.Throws<InvalidConfigurationException>(() => engine.Compile(column));
            Assert.Equal($"Property \"FullName\" for type \"{typeof(Person).FullName}\" is not registered.", ex.Message);
        }

        [Fact]
        public void Invalid_Nested_Property()
        {
            Person person = null;
            IColumn column = sql.Col(() => person.Department.Name);

            Exception ex = Assert.Throws<InvalidConfigurationException>(() => engine.Compile(column));
            Assert.Equal($"Property \"Department.Name\" for type \"{typeof(Person).FullName}\" is not registered.",
                ex.Message);
        }

        [Fact]
        public void To_String()
        {
            Person person = null;
            IColumn column = sql.Col(() => person.Id);

            Assert.Equal("person.Id", column.ToString());
        }
    }
}