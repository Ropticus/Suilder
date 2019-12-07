using System;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Exceptions;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Alias
{
    public class TypedAliasColumnTest : BuilderBaseTest
    {
        [Fact]
        public void Indexer_String_Column()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person["Id"];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\"", result.Sql);
        }

        [Fact]
        public void Indexer_String_All_Columns()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person["*"];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\", \"person\".\"Active\", \"person\".\"Name\", \"person\".\"SurName\", "
                + "\"person\".\"AddressStreet\", \"person\".\"AddressCity\", \"person\".\"Salary\", "
                + "\"person\".\"DateCreated\", \"person\".\"DepartmentId\", \"person\".\"Image\"", result.Sql);
        }

        [Fact]
        public void Col_String_Column()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person.Col("Id");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\"", result.Sql);
        }

        [Fact]
        public void Col_String_All_Columns()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person.Col("*");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\", \"person\".\"Active\", \"person\".\"Name\", \"person\".\"SurName\", "
                + "\"person\".\"AddressStreet\", \"person\".\"AddressCity\", \"person\".\"Salary\", "
                + "\"person\".\"DateCreated\", \"person\".\"DepartmentId\", \"person\".\"Image\"", result.Sql);
        }

        [Fact]
        public void String_Column_Nested()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person["Address.Street"];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"AddressStreet\"", result.Sql);
        }

        [Fact]
        public void String_Column_ForeignKey()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person["Department.Id"];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"DepartmentId\"", result.Sql);
        }

        [Fact]
        public void Indexer_Expression_Column()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person[x => x.Id];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\"", result.Sql);
        }

        [Fact]
        public void Indexer_Expression_All_Columns()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person[x => x];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\", \"person\".\"Active\", \"person\".\"Name\", \"person\".\"SurName\", "
                + "\"person\".\"AddressStreet\", \"person\".\"AddressCity\", \"person\".\"Salary\", "
                + "\"person\".\"DateCreated\", \"person\".\"DepartmentId\", \"person\".\"Image\"", result.Sql);
        }

        [Fact]
        public void Col_Expression_Column()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person.Col(x => x.Id);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\"", result.Sql);
        }

        [Fact]
        public void Col_Expression_All_Columns()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person.Col(x => x);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\", \"person\".\"Active\", \"person\".\"Name\", \"person\".\"SurName\", "
                + "\"person\".\"AddressStreet\", \"person\".\"AddressCity\", \"person\".\"Salary\", "
                + "\"person\".\"DateCreated\", \"person\".\"DepartmentId\", \"person\".\"Image\"", result.Sql);
        }

        [Fact]
        public void Expression_Column_Nested()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person[x => x.Address.Street];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"AddressStreet\"", result.Sql);
        }

        [Fact]
        public void Expression_Column_ForeignKey()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person[x => x.Department.Id];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"DepartmentId\"", result.Sql);
        }

        [Fact]
        public void All_Property()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person.All;

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\", \"person\".\"Active\", \"person\".\"Name\", \"person\".\"SurName\", "
                + "\"person\".\"AddressStreet\", \"person\".\"AddressCity\", \"person\".\"Salary\", "
                + "\"person\".\"DateCreated\", \"person\".\"DepartmentId\", \"person\".\"Image\"", result.Sql);
        }

        [Fact]
        public void String_Column_With_Translation()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person["Created"];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"DateCreated\"", result.Sql);
        }

        [Fact]
        public void Expression_Column_With_Translation()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person[x => x.Created];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"DateCreated\"", result.Sql);
        }

        [Fact]
        public void Indexer_String_Column_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person["Id"];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"Id\"", result.Sql);
        }

        [Fact]
        public void Indexer_String_All_Columns_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person["*"];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"Id\", \"per\".\"Active\", \"per\".\"Name\", \"per\".\"SurName\", "
                + "\"per\".\"AddressStreet\", \"per\".\"AddressCity\", \"per\".\"Salary\", "
                + "\"per\".\"DateCreated\", \"per\".\"DepartmentId\", \"per\".\"Image\"", result.Sql);
        }

        [Fact]
        public void Col_String_Column_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person.Col("Id");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"Id\"", result.Sql);
        }

        [Fact]
        public void Col_String_All_Columns_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person.Col("*");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"Id\", \"per\".\"Active\", \"per\".\"Name\", \"per\".\"SurName\", "
                + "\"per\".\"AddressStreet\", \"per\".\"AddressCity\", \"per\".\"Salary\", "
                + "\"per\".\"DateCreated\", \"per\".\"DepartmentId\", \"per\".\"Image\"", result.Sql);
        }

        [Fact]
        public void String_Column_Nested_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person["Address.Street"];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"AddressStreet\"", result.Sql);
        }

        [Fact]
        public void String_Column_ForeignKey_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person["Department.Id"];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"DepartmentId\"", result.Sql);
        }

        [Fact]
        public void Indexer_Expression_Column_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person[x => x.Id];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"Id\"", result.Sql);
        }

        [Fact]
        public void Indexer_Expression_All_Columns_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person[x => x];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"Id\", \"per\".\"Active\", \"per\".\"Name\", \"per\".\"SurName\", "
                + "\"per\".\"AddressStreet\", \"per\".\"AddressCity\", \"per\".\"Salary\", "
                + "\"per\".\"DateCreated\", \"per\".\"DepartmentId\", \"per\".\"Image\"", result.Sql);
        }

        [Fact]
        public void Col_Expression_Column_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person.Col(x => x.Id);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"Id\"", result.Sql);
        }

        [Fact]
        public void Col_Expression_All_Columns_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person.Col(x => x);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"Id\", \"per\".\"Active\", \"per\".\"Name\", \"per\".\"SurName\", "
                + "\"per\".\"AddressStreet\", \"per\".\"AddressCity\", \"per\".\"Salary\", "
                + "\"per\".\"DateCreated\", \"per\".\"DepartmentId\", \"per\".\"Image\"", result.Sql);
        }

        [Fact]
        public void Expression_Column_Nested_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person[x => x.Address.Street];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"AddressStreet\"", result.Sql);
        }

        [Fact]
        public void Expression_Column_ForeignKey_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person[x => x.Department.Id];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"DepartmentId\"", result.Sql);
        }

        [Fact]
        public void All_Property_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person.All;

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"Id\", \"per\".\"Active\", \"per\".\"Name\", \"per\".\"SurName\", "
                + "\"per\".\"AddressStreet\", \"per\".\"AddressCity\", \"per\".\"Salary\", "
                + "\"per\".\"DateCreated\", \"per\".\"DepartmentId\", \"per\".\"Image\"", result.Sql);
        }

        [Fact]
        public void String_Column_With_Translation_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person["Created"];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"DateCreated\"", result.Sql);
        }

        [Fact]
        public void Expression_Column_With_Translation_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person[x => x.Created];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"DateCreated\"", result.Sql);
        }


        [Fact]
        public void Ignored_Property()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person[x => x.Ignore];

            Exception ex = Assert.Throws<InvalidConfigurationException>(() => engine.Compile(column));
            Assert.Equal($"The property \"Ignore\" for type \"{typeof(Person).FullName}\" is not registered.", ex.Message);
        }

        [Fact]
        public void Invalid_Not_Registered_Property()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person[x => x.FullName];

            Exception ex = Assert.Throws<InvalidConfigurationException>(() => engine.Compile(column));
            Assert.Equal($"The property \"FullName\" for type \"{typeof(Person).FullName}\" is not registered.", ex.Message);
        }

        [Fact]
        public void Not_Exists_Property()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person["Other"];

            Exception ex = Assert.Throws<InvalidConfigurationException>(() => engine.Compile(column));
            Assert.Equal($"The property \"Other\" for type \"{typeof(Person).FullName}\" is not registered.", ex.Message);
        }

        [Fact]
        public void Invalid_Nested_Property()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person[x => x.Address.Ignore];

            Exception ex = Assert.Throws<InvalidConfigurationException>(() => engine.Compile(column));
            Assert.Equal($"The property \"Address.Ignore\" for type \"{typeof(Person).FullName}\" is not registered.",
                ex.Message);
        }

        [Fact]
        public void Invalid_ForeignKey_Property()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person[x => x.Department.Name];

            Exception ex = Assert.Throws<InvalidConfigurationException>(() => engine.Compile(column));
            Assert.Equal($"The property \"Department.Name\" for type \"{typeof(Person).FullName}\" is not registered.",
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