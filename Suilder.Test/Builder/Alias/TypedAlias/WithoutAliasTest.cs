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
            IColumn column = sql.Col<Person>("person.*");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\", \"person\".\"Active\", \"person\".\"Name\", \"person\".\"SurName\", "
                + "\"person\".\"AddressStreet\", \"person\".\"AddressNumber\", \"person\".\"AddressCity\", "
                + "\"person\".\"Salary\", \"person\".\"DateCreated\", \"person\".\"DepartmentId\", \"person\".\"Image\"",
                result.Sql);
        }

        [Fact]
        public void Col_String_Column_One_Param()
        {
            IColumn column = sql.Col<Person>("person.Id");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\"", result.Sql);
        }

        [Fact]
        public void Col_Expression_All_One_Param()
        {
            IColumn column = sql.Col<Person>(x => x);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\", \"person\".\"Active\", \"person\".\"Name\", \"person\".\"SurName\", "
                + "\"person\".\"AddressStreet\", \"person\".\"AddressNumber\", \"person\".\"AddressCity\", "
                + "\"person\".\"Salary\", \"person\".\"DateCreated\", \"person\".\"DepartmentId\", \"person\".\"Image\"",
                result.Sql);
        }

        [Fact]
        public void Col_Expression_Column_One_Param()
        {
            IColumn column = sql.Col<Person>(x => x.Id);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\"", result.Sql);
        }

        [Fact]
        public void Col_String_All_Two_Params()
        {
            IColumn column = sql.Col<Person>("person", "*");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\", \"person\".\"Active\", \"person\".\"Name\", \"person\".\"SurName\", "
                + "\"person\".\"AddressStreet\", \"person\".\"AddressNumber\", \"person\".\"AddressCity\", "
                + "\"person\".\"Salary\", \"person\".\"DateCreated\", \"person\".\"DepartmentId\", \"person\".\"Image\"",
                result.Sql);
        }

        [Fact]
        public void Col_String_Column_Two_Params()
        {
            IColumn column = sql.Col<Person>("person", "Id");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\"", result.Sql);
        }

        [Fact]
        public void Col_Expression_All_Two_Params()
        {
            IColumn column = sql.Col<Person>("person", x => x);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\", \"person\".\"Active\", \"person\".\"Name\", \"person\".\"SurName\", "
                + "\"person\".\"AddressStreet\", \"person\".\"AddressNumber\", \"person\".\"AddressCity\", "
                + "\"person\".\"Salary\", \"person\".\"DateCreated\", \"person\".\"DepartmentId\", \"person\".\"Image\"",
                result.Sql);
        }

        [Fact]
        public void Col_Expression_Column_Two_Params()
        {
            IColumn column = sql.Col<Person>("person", x => x.Id);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\"", result.Sql);
        }

        [Fact]
        public void Col_String_All_Without_Table_Name()
        {
            IColumn column = sql.Col<Person>("*");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"Id\", \"Active\", \"Name\", \"SurName\", \"AddressStreet\", \"AddressNumber\", "
                + "\"AddressCity\", \"Salary\", \"DateCreated\", \"DepartmentId\", \"Image\"", result.Sql);
        }

        [Fact]
        public void Col_String_Column_Without_Table_Name()
        {
            IColumn column = sql.Col<Person>("Id");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"Id\"", result.Sql);
        }
    }
}