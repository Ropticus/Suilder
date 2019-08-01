using System;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Exceptions;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Alias
{
    public class TypedAliasColumnTest : BaseTest
    {
        [Fact]
        public void Expression()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person[x => x.Id];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\"", result.Sql);
        }

        [Fact]
        public void Expression_Nested()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person[x => x.Department.Id];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"DepartmentId\"", result.Sql);
        }

        [Fact]
        public void Expression_Translation()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person[x => x.Created];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"DateCreated\"", result.Sql);
        }

        [Fact]
        public void Column()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person["Id"];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\"", result.Sql);
        }

        [Fact]
        public void Nested()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person["Department.Id"];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"DepartmentId\"", result.Sql);
        }

        [Fact]
        public void Translation()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person["Created"];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"DateCreated\"", result.Sql);
        }

        [Fact]
        public void All()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person.All;

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\", \"person\".\"Active\", \"person\".\"Name\", \"person\".\"SurName\", "
                + "\"person\".\"Salary\", \"person\".\"DateCreated\", \"person\".\"DepartmentId\"", result.Sql);
        }

        [Fact]
        public void All_String()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person["*"];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\", \"person\".\"Active\", \"person\".\"Name\", \"person\".\"SurName\", "
                + "\"person\".\"Salary\", \"person\".\"DateCreated\", \"person\".\"DepartmentId\"", result.Sql);
        }

        [Fact]
        public void Expression_With_Alias()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person[x => x.Id];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"Id\"", result.Sql);
        }

        [Fact]
        public void Expression_Nested_With_Alias()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person[x => x.Department.Id];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"DepartmentId\"", result.Sql);
        }

        [Fact]
        public void Expression_Translation_With_Alias()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person[x => x.Created];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"DateCreated\"", result.Sql);
        }

        [Fact]
        public void With_Alias()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person["Id"];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"Id\"", result.Sql);
        }

        [Fact]
        public void Nested_With_Alias()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person["Department.Id"];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"DepartmentId\"", result.Sql);
        }

        [Fact]
        public void Translation_With_Alias()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person["Created"];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"DateCreated\"", result.Sql);
        }

        [Fact]
        public void All_With_Alias()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person.All;

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"Id\", \"per\".\"Active\", \"per\".\"Name\", \"per\".\"SurName\", "
                + "\"per\".\"Salary\", \"per\".\"DateCreated\", \"per\".\"DepartmentId\"", result.Sql);
        }

        [Fact]
        public void All_String_With_Alias()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person["*"];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"Id\", \"per\".\"Active\", \"per\".\"Name\", \"per\".\"SurName\", "
                + "\"per\".\"Salary\", \"per\".\"DateCreated\", \"per\".\"DepartmentId\"", result.Sql);
        }

        [Fact]
        public void Ignored_Property()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person[x => x.Ignore];

            Exception ex = Assert.Throws<InvalidConfigurationException>(() => engine.Compile(column));
            Assert.Equal($"Property \"Ignore\" for type \"{typeof(Person).FullName}\" is not registered.", ex.Message);
        }

        [Fact]
        public void Invalid_Property()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person[x => x.FullName];

            Exception ex = Assert.Throws<InvalidConfigurationException>(() => engine.Compile(column));
            Assert.Equal($"Property \"FullName\" for type \"{typeof(Person).FullName}\" is not registered.", ex.Message);
        }

        [Fact]
        public void Not_Exists_Property()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person["Other"];

            Exception ex = Assert.Throws<InvalidConfigurationException>(() => engine.Compile(column));
            Assert.Equal($"Property \"Other\" for type \"{typeof(Person).FullName}\" is not registered.", ex.Message);
        }

        [Fact]
        public void Invalid_Nested_Property()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person[x => x.Department.Name];

            Exception ex = Assert.Throws<InvalidConfigurationException>(() => engine.Compile(column));
            Assert.Equal($"Property \"Department.Name\" for type \"{typeof(Person).FullName}\" is not registered.",
                ex.Message);
        }

        [Fact]
        public void To_String()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person[x => x.Id];

            Assert.Equal("person.Id", column.ToString());
        }
    }
}