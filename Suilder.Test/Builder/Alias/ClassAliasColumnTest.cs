using System;
using System.Linq.Expressions;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Exceptions;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Alias
{
    public class ClassAliasColumnTest : BuilderBaseTest
    {
        [Fact]
        public void Expression_Column()
        {
            Person person = null;
            IColumn column = sql.Col(() => person.Id);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\"", result.Sql);
        }

        [Fact]
        public void Expression_All_Columns()
        {
            Person person = null;
            IColumn column = sql.Col(() => person);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\", \"person\".\"Active\", \"person\".\"Name\", \"person\".\"SurName\", "
                + "\"person\".\"AddressStreet\", \"person\".\"AddressCity\", \"person\".\"Salary\", "
                + "\"person\".\"DateCreated\", \"person\".\"DepartmentId\", \"person\".\"Image\"", result.Sql);
        }

        [Fact]
        public void Expression_Column_Nested()
        {
            Person person = null;
            IColumn column = sql.Col(() => person.Address.Street);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"AddressStreet\"", result.Sql);
        }

        [Fact]
        public void Expression_Column_ForeignKey()
        {
            Person person = null;
            IColumn column = sql.Col(() => person.Department.Id);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"DepartmentId\"", result.Sql);
        }

        [Fact]
        public void Expression_Column_With_Translation()
        {
            Person person = null;
            IColumn column = sql.Col(() => person.Created);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"DateCreated\"", result.Sql);
        }

        [Fact]
        public void Expression_Body_Overload()
        {
            Person person = null;
            Expression<Func<object>> expression = () => person.Id;
            IColumn column = sql.Col(expression.Body);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\"", result.Sql);
        }

        [Fact]
        public void Invalid_Ignored_Property()
        {
            Person person = null;
            IColumn column = sql.Col(() => person.Ignore);

            Exception ex = Assert.Throws<InvalidConfigurationException>(() => engine.Compile(column));
            Assert.Equal($"The property \"Ignore\" for type \"{typeof(Person).FullName}\" is not registered.", ex.Message);
        }

        [Fact]
        public void Invalid_Not_Registered_Property()
        {
            Person person = null;
            IColumn column = sql.Col(() => person.FullName);

            Exception ex = Assert.Throws<InvalidConfigurationException>(() => engine.Compile(column));
            Assert.Equal($"The property \"FullName\" for type \"{typeof(Person).FullName}\" is not registered.", ex.Message);
        }

        [Fact]
        public void Invalid_Nested_Property()
        {
            Person person = null;
            IColumn column = sql.Col(() => person.Department.Name);

            Exception ex = Assert.Throws<InvalidConfigurationException>(() => engine.Compile(column));
            Assert.Equal($"The property \"Department.Name\" for type \"{typeof(Person).FullName}\" is not registered.",
                ex.Message);
        }

        [Fact]
        public void Invalid_Property()
        {
            Person person = null;

            Exception ex = Assert.Throws<ArgumentException>(() => sql.Col(() => person.ToString()));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        [Fact]
        public void Invalid_Property_Object_Overload()
        {
            Person person = null;
            Expression<Func<object>> expression = () => person.ToString();

            Exception ex = Assert.Throws<ArgumentException>(() => sql.Col(() => person.ToString()));
            Assert.Equal("Invalid expression.", ex.Message);
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