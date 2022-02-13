using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Alias.TypedAlias
{
    public class WithoutAliasTest : BuilderBaseTest
    {
        [Fact]
        public void Col_String_All_One_Param()
        {
            IColumn column = sql.Col<Person>("*");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\", \"person\".\"Active\", \"person\".\"Name\", \"person\".\"SurName\", "
                + "\"person\".\"AddressStreet\", \"person\".\"AddressNumber\", \"person\".\"AddressCity\", "
                + "\"person\".\"Salary\", \"person\".\"DateCreated\", \"person\".\"DepartmentId\", \"person\".\"Image\", "
                + "\"person\".\"Flags\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_String_Column_One_Param()
        {
            IColumn column = sql.Col<Person>("Id");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_String_Column_ForeignKey_One_Param()
        {
            IColumn column = sql.Col<Person>("Department.Id");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"DepartmentId\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_String_Column_Nested_One_Param()
        {
            IColumn column = sql.Col<Person>("Address.Street");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"AddressStreet\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_String_Column_Nested_Deep_One_Param()
        {
            IColumn column = sql.Col<Person2>("Address.City.Country.Name");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person2\".\"AddressCityCountryName\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_String_Column_With_Translation_One_Param()
        {
            IColumn column = sql.Col<Person>("Created");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"DateCreated\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_Expression_All_One_Param()
        {
            IColumn column = sql.Col<Person>(x => x);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\", \"person\".\"Active\", \"person\".\"Name\", \"person\".\"SurName\", "
                + "\"person\".\"AddressStreet\", \"person\".\"AddressNumber\", \"person\".\"AddressCity\", "
                + "\"person\".\"Salary\", \"person\".\"DateCreated\", \"person\".\"DepartmentId\", \"person\".\"Image\", "
                + "\"person\".\"Flags\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_Expression_Column_One_Param()
        {
            IColumn column = sql.Col<Person>(x => x.Id);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_Expression_Column_ForeignKey_One_Param()
        {
            IColumn column = sql.Col<Person>(x => x.Department.Id);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"DepartmentId\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_Expression_Column_Nested_One_Param()
        {
            IColumn column = sql.Col<Person>(x => x.Address.Street);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"AddressStreet\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_Expression_Column_Nested_Deep_One_Param()
        {
            IColumn column = sql.Col<Person2>(x => x.Address.City.Country.Name);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person2\".\"AddressCityCountryName\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_Expression_Column_With_Translation_One_Param()
        {
            IColumn column = sql.Col<Person>(x => x.Created);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"DateCreated\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_String_All_Two_Params()
        {
            IColumn column = sql.Col<Person>("per", "*");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"Id\", \"per\".\"Active\", \"per\".\"Name\", \"per\".\"SurName\", "
                + "\"per\".\"AddressStreet\", \"per\".\"AddressNumber\", \"per\".\"AddressCity\", "
                + "\"per\".\"Salary\", \"per\".\"DateCreated\", \"per\".\"DepartmentId\", \"per\".\"Image\", "
                + "\"per\".\"Flags\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_String_Column_Two_Params()
        {
            IColumn column = sql.Col<Person>("per", "Id");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_String_Column_ForeignKey_Two_Params()
        {
            IColumn column = sql.Col<Person>("per", "Department.Id");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"DepartmentId\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_String_Column_Nested_Two_Params()
        {
            IColumn column = sql.Col<Person>("per", "Address.Street");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"AddressStreet\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_String_Column_Nested_Deep_Two_Params()
        {
            IColumn column = sql.Col<Person2>("per", "Address.City.Country.Name");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"AddressCityCountryName\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_String_Column_With_Translation_Two_Params()
        {
            IColumn column = sql.Col<Person>("per", "Created");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"DateCreated\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_Expression_All_Two_Params()
        {
            IColumn column = sql.Col<Person>("per", x => x);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"Id\", \"per\".\"Active\", \"per\".\"Name\", \"per\".\"SurName\", "
                + "\"per\".\"AddressStreet\", \"per\".\"AddressNumber\", \"per\".\"AddressCity\", "
                + "\"per\".\"Salary\", \"per\".\"DateCreated\", \"per\".\"DepartmentId\", \"per\".\"Image\", "
                + "\"per\".\"Flags\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_Expression_Column_Two_Params()
        {
            IColumn column = sql.Col<Person>("per", x => x.Id);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_Expression_Column_ForeignKey_Two_Params()
        {
            IColumn column = sql.Col<Person>("per", x => x.Department.Id);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"DepartmentId\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_Expression_Column_Nested_Two_Params()
        {
            IColumn column = sql.Col<Person>("per", x => x.Address.Street);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"AddressStreet\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_Expression_Column_Nested_Deep_Two_Params()
        {
            IColumn column = sql.Col<Person2>("per", x => x.Address.City.Country.Name);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"AddressCityCountryName\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_Expression_Column_With_Translation_Two_Params()
        {
            IColumn column = sql.Col<Person>("per", x => x.Created);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"DateCreated\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }
    }
}