using System;
using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Exceptions;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Alias.TypedAlias
{
    public class ColumnTest : BuilderBaseTest
    {
        [Fact]
        public void All_Property()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person.All;

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\", \"person\".\"Active\", \"person\".\"Name\", \"person\".\"SurName\", "
                + "\"person\".\"AddressStreet\", \"person\".\"AddressNumber\", \"person\".\"AddressCity\", "
                + "\"person\".\"Salary\", \"person\".\"DateCreated\", \"person\".\"DepartmentId\", \"person\".\"Image\", "
                + "\"person\".\"Flags\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Indexer_String_All()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person["*"];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\", \"person\".\"Active\", \"person\".\"Name\", \"person\".\"SurName\", "
                + "\"person\".\"AddressStreet\", \"person\".\"AddressNumber\", \"person\".\"AddressCity\", "
                + "\"person\".\"Salary\", \"person\".\"DateCreated\", \"person\".\"DepartmentId\", \"person\".\"Image\", "
                + "\"person\".\"Flags\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Indexer_String_Column()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person["Id"];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Indexer_String_Column_ForeignKey()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person["Department.Id"];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"DepartmentId\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Indexer_String_Column_Nested()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person["Address.Street"];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"AddressStreet\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Indexer_String_Column_Nested_Deep()
        {
            IAlias<Person2> person = sql.Alias<Person2>();
            IColumn column = person["Address.City.Country.Name"];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person2\".\"AddressCityCountryName\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Indexer_String_Column_With_Translation()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person["Created"];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"DateCreated\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Indexer_Expression_All()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person[x => x];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\", \"person\".\"Active\", \"person\".\"Name\", \"person\".\"SurName\", "
                + "\"person\".\"AddressStreet\", \"person\".\"AddressNumber\", \"person\".\"AddressCity\", "
                + "\"person\".\"Salary\", \"person\".\"DateCreated\", \"person\".\"DepartmentId\", \"person\".\"Image\", "
                + "\"person\".\"Flags\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Indexer_Expression_Column()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person[x => x.Id];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Indexer_Expression_Column_ForeignKey()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person[x => x.Department.Id];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"DepartmentId\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Indexer_Expression_Column_Nested()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person[x => x.Address.Street];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"AddressStreet\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Indexer_Expression_Column_Nested_Deep()
        {
            IAlias<Person2> person = sql.Alias<Person2>();
            IColumn column = person[x => x.Address.City.Country.Name];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person2\".\"AddressCityCountryName\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Indexer_Expression_Column_With_Translation()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person[x => x.Created];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"DateCreated\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_String_All()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person.Col("*");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\", \"person\".\"Active\", \"person\".\"Name\", \"person\".\"SurName\", "
                + "\"person\".\"AddressStreet\", \"person\".\"AddressNumber\", \"person\".\"AddressCity\", "
                + "\"person\".\"Salary\", \"person\".\"DateCreated\", \"person\".\"DepartmentId\", \"person\".\"Image\", "
                + "\"person\".\"Flags\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_String_Column()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person.Col("Id");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_String_Column_ForeignKey()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person.Col("Department.Id");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"DepartmentId\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_String_Column_Nested()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person.Col("Address.Street");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"AddressStreet\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_String_Column_Nested_Deep()
        {
            IAlias<Person2> person = sql.Alias<Person2>();
            IColumn column = person.Col("Address.City.Country.Name");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person2\".\"AddressCityCountryName\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_String_Column_With_Translation()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person.Col("Created");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"DateCreated\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_Expression_All()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person.Col(x => x);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\", \"person\".\"Active\", \"person\".\"Name\", \"person\".\"SurName\", "
                + "\"person\".\"AddressStreet\", \"person\".\"AddressNumber\", \"person\".\"AddressCity\", "
                + "\"person\".\"Salary\", \"person\".\"DateCreated\", \"person\".\"DepartmentId\", \"person\".\"Image\", "
                + "\"person\".\"Flags\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_Expression_Column()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person.Col(x => x.Id);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_Expression_Column_ForeignKey()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person.Col(x => x.Department.Id);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"DepartmentId\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_Expression_Column_Nested()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person.Col(x => x.Address.Street);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"AddressStreet\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_Expression_Column_Nested_Deep()
        {
            IAlias<Person2> person = sql.Alias<Person2>();
            IColumn column = person.Col(x => x.Address.City.Country.Name);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person2\".\"AddressCityCountryName\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_Expression_Column_With_Translation()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person.Col(x => x.Created);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"DateCreated\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void All_Property_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person.All;

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"Id\", \"per\".\"Active\", \"per\".\"Name\", \"per\".\"SurName\", "
                + "\"per\".\"AddressStreet\", \"per\".\"AddressNumber\", \"per\".\"AddressCity\", "
                + "\"per\".\"Salary\", \"per\".\"DateCreated\", \"per\".\"DepartmentId\", \"per\".\"Image\", "
                + "\"per\".\"Flags\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Indexer_String_All_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person["*"];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"Id\", \"per\".\"Active\", \"per\".\"Name\", \"per\".\"SurName\", "
                + "\"per\".\"AddressStreet\", \"per\".\"AddressNumber\", \"per\".\"AddressCity\", "
                + "\"per\".\"Salary\", \"per\".\"DateCreated\", \"per\".\"DepartmentId\", \"per\".\"Image\", "
                + "\"per\".\"Flags\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Indexer_String_Column_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person["Id"];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Indexer_String_Column_ForeignKey_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person["Department.Id"];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"DepartmentId\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Indexer_String_Column_Nested_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person["Address.Street"];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"AddressStreet\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Indexer_String_Column_Nested_Deep_With_Alias_Name()
        {
            IAlias<Person2> person = sql.Alias<Person2>("per");
            IColumn column = person["Address.City.Country.Name"];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"AddressCityCountryName\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Indexer_String_Column_With_Translation_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person["Created"];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"DateCreated\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Indexer_Expression_All_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person[x => x];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"Id\", \"per\".\"Active\", \"per\".\"Name\", \"per\".\"SurName\", "
                + "\"per\".\"AddressStreet\", \"per\".\"AddressNumber\", \"per\".\"AddressCity\", "
                + "\"per\".\"Salary\", \"per\".\"DateCreated\", \"per\".\"DepartmentId\", \"per\".\"Image\", "
                + "\"per\".\"Flags\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Indexer_Expression_Column_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person[x => x.Id];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Indexer_Expression_Column_ForeignKey_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person[x => x.Department.Id];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"DepartmentId\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Indexer_Expression_Column_Nested_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person[x => x.Address.Street];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"AddressStreet\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Indexer_Expression_Column_Nested_Deep_With_Alias_Name()
        {
            IAlias<Person2> person = sql.Alias<Person2>("per");
            IColumn column = person[x => x.Address.City.Country.Name];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"AddressCityCountryName\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Indexer_Expression_Column_With_Translation_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person[x => x.Created];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"DateCreated\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_String_All_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person.Col("*");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"Id\", \"per\".\"Active\", \"per\".\"Name\", \"per\".\"SurName\", "
                + "\"per\".\"AddressStreet\", \"per\".\"AddressNumber\", \"per\".\"AddressCity\", "
                + "\"per\".\"Salary\", \"per\".\"DateCreated\", \"per\".\"DepartmentId\", \"per\".\"Image\", "
                + "\"per\".\"Flags\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_String_Column_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person.Col("Id");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_String_Column_ForeignKey_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person.Col("Department.Id");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"DepartmentId\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_String_Column_Nested_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person.Col("Address.Street");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"AddressStreet\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_String_Column_Nested_Deep_With_Alias_Name()
        {
            IAlias<Person2> person = sql.Alias<Person2>("per");
            IColumn column = person.Col("Address.City.Country.Name");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"AddressCityCountryName\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_String_Column_With_Translation_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person.Col("Created");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"DateCreated\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_Expression_All_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person.Col(x => x);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"Id\", \"per\".\"Active\", \"per\".\"Name\", \"per\".\"SurName\", "
                + "\"per\".\"AddressStreet\", \"per\".\"AddressNumber\", \"per\".\"AddressCity\", "
                + "\"per\".\"Salary\", \"per\".\"DateCreated\", \"per\".\"DepartmentId\", \"per\".\"Image\", "
                + "\"per\".\"Flags\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_Expression_Column_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person.Col(x => x.Id);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_Expression_Column_ForeignKey_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person.Col(x => x.Department.Id);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"DepartmentId\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_Expression_Column_Nested_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person.Col(x => x.Address.Street);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"AddressStreet\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_Expression_Column_Nested_Deep_With_Alias_Name()
        {
            IAlias<Person2> person = sql.Alias<Person2>("per");
            IColumn column = person.Col(x => x.Address.City.Country.Name);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"AddressCityCountryName\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_Expression_Column_With_Translation_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person.Col(x => x.Created);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"DateCreated\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Invalid_Ignored_Property()
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
        public void Invalid_Not_Exists_Property()
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
        public void Name_All()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person.All.Name;

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"Id\", \"Active\", \"Name\", \"SurName\", \"AddressStreet\", \"AddressNumber\", "
                + "\"AddressCity\", \"Salary\", \"DateCreated\", \"DepartmentId\", \"Image\", \"Flags\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Name_Column()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person[x => x.Id].Name;

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Name_Column_ForeignKey()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person[x => x.Department.Id].Name;

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"DepartmentId\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Name_Column_Nested()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person[x => x.Address.Street].Name;

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"AddressStreet\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Name_Column_Nested_Deep()
        {
            IAlias<Person2> person = sql.Alias<Person2>();
            IColumn column = person[x => x.Address.City.Country.Name].Name;

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"AddressCityCountryName\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Name_Column_With_Translation()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person[x => x.Created].Name;

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"DateCreated\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Name_All_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person.All.Name;

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"Id\", \"Active\", \"Name\", \"SurName\", \"AddressStreet\", \"AddressNumber\", "
                + "\"AddressCity\", \"Salary\", \"DateCreated\", \"DepartmentId\", \"Image\", \"Flags\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Name_Column_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person[x => x.Id].Name;

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Name_Column_ForeignKey_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person[x => x.Department.Id].Name;

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"DepartmentId\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Name_Column_Nested_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person[x => x.Address.Street].Name;

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"AddressStreet\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Name_Column_Nested_Deep_With_Alias_Name()
        {
            IAlias<Person2> person = sql.Alias<Person2>("per");
            IColumn column = person[x => x.Address.City.Country.Name].Name;

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"AddressCityCountryName\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Name_Column_With_Translation_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person[x => x.Created].Name;

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"DateCreated\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Name_Name_All()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person.All.Name.Name;

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"Id\", \"Active\", \"Name\", \"SurName\", \"AddressStreet\", \"AddressNumber\", "
                + "\"AddressCity\", \"Salary\", \"DateCreated\", \"DepartmentId\", \"Image\", \"Flags\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Name_Name_Column()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person[x => x.Id].Name.Name;

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void To_String_All()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person.All;

            Assert.Equal("person.*", column.ToString());
        }

        [Fact]
        public void To_String_Column()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person[x => x.Id];

            Assert.Equal("person.Id", column.ToString());
        }

        [Fact]
        public void To_String_Column_ForeignKey()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person[x => x.Department.Id];

            Assert.Equal("person.Department.Id", column.ToString());
        }

        [Fact]
        public void To_String_Column_Nested()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person[x => x.Address.Street];

            Assert.Equal("person.Address.Street", column.ToString());
        }

        [Fact]
        public void To_String_Column_Nested_Deep()
        {
            IAlias<Person2> person = sql.Alias<Person2>();
            IColumn column = person[x => x.Address.City.Country.Name];

            Assert.Equal("person2.Address.City.Country.Name", column.ToString());
        }

        [Fact]
        public void To_String_Column_With_Translation()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person[x => x.Created];

            Assert.Equal("person.Created", column.ToString());
        }

        [Fact]
        public void To_String_All_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person.All;

            Assert.Equal("per.*", column.ToString());
        }

        [Fact]
        public void To_String_Column_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person[x => x.Id];

            Assert.Equal("per.Id", column.ToString());
        }

        [Fact]
        public void To_String_Column_ForeignKey_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person[x => x.Department.Id];

            Assert.Equal("per.Department.Id", column.ToString());
        }

        [Fact]
        public void To_String_Column_Nested_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person[x => x.Address.Street];

            Assert.Equal("per.Address.Street", column.ToString());
        }

        [Fact]
        public void To_String_Column_Nested_Deep_With_Alias_Name()
        {
            IAlias<Person2> person = sql.Alias<Person2>("per");
            IColumn column = person[x => x.Address.City.Country.Name];

            Assert.Equal("per.Address.City.Country.Name", column.ToString());
        }

        [Fact]
        public void To_String_Column_With_Translation_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person[x => x.Created];

            Assert.Equal("per.Created", column.ToString());
        }

        [Fact]
        public void To_String_Name_All()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person.All.Name;

            Assert.Equal("*", column.ToString());
        }

        [Fact]
        public void To_String_Name_Column()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person[x => x.Id].Name;

            Assert.Equal("Id", column.ToString());
        }

        [Fact]
        public void To_String_Name_Column_ForeignKey()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person[x => x.Department.Id].Name;

            Assert.Equal("Department.Id", column.ToString());
        }

        [Fact]
        public void To_String_Name_Column_Nested()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person[x => x.Address.Street].Name;

            Assert.Equal("Address.Street", column.ToString());
        }

        [Fact]
        public void To_String_Name_Column_Nested_Deep()
        {
            IAlias<Person2> person = sql.Alias<Person2>();
            IColumn column = person[x => x.Address.City.Country.Name].Name;

            Assert.Equal("Address.City.Country.Name", column.ToString());
        }

        [Fact]
        public void To_String_Name_Column_With_Translation()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person[x => x.Created].Name;

            Assert.Equal("Created", column.ToString());
        }

        [Fact]
        public void To_String_Name_All_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person.All.Name;

            Assert.Equal("*", column.ToString());
        }

        [Fact]
        public void To_String_Name_Column_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person[x => x.Id].Name;

            Assert.Equal("Id", column.ToString());
        }

        [Fact]
        public void To_String_Name_Column_ForeignKey_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person[x => x.Department.Id].Name;

            Assert.Equal("Department.Id", column.ToString());
        }

        [Fact]
        public void To_String_Name_Column_Nested_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person[x => x.Address.Street].Name;

            Assert.Equal("Address.Street", column.ToString());
        }

        [Fact]
        public void To_String_Name_Column_Nested_Deep_With_Alias_Name()
        {
            IAlias<Person2> person = sql.Alias<Person2>("per");
            IColumn column = person[x => x.Address.City.Country.Name].Name;

            Assert.Equal("Address.City.Country.Name", column.ToString());
        }

        [Fact]
        public void To_String_Name_Column_With_Translation_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IColumn column = person[x => x.Created].Name;

            Assert.Equal("Created", column.ToString());
        }
    }
}