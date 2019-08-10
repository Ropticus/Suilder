using Suilder.Builder;
using Suilder.Core;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Alias
{
    public class ColumnWithoutAliasTest : BaseTest
    {
        [Fact]
        public void One_Param_String()
        {
            IColumn column = sql.Col("person.Id");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\"", result.Sql);
        }

        [Fact]
        public void One_Param_All_String()
        {
            IColumn column = sql.Col("person.*");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".*", result.Sql);
        }

        [Fact]
        public void One_Param_With_Schema_String()
        {
            IColumn column = sql.Col("dbo.person.Id");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"dbo\".\"person\".\"Id\"", result.Sql);
        }

        [Fact]
        public void Two_Params_String()
        {
            IColumn column = sql.Col("person", "Id");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\"", result.Sql);
        }

        [Fact]
        public void Two_Params_All_String()
        {
            IColumn column = sql.Col("person", "*");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".*", result.Sql);
        }

        [Fact]
        public void Two_Params_With_Schema_String()
        {
            IColumn column = sql.Col("dbo.person", "Id");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"dbo\".\"person\".\"Id\"", result.Sql);
        }

        [Fact]
        public void One_Param_Typed_String()
        {
            IColumn column = sql.Col<Person>("person.Id");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\"", result.Sql);
        }

        [Fact]
        public void One_Param_All_Typed_String()
        {
            IColumn column = sql.Col<Person>("person.*");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\", \"person\".\"Active\", \"person\".\"Name\", \"person\".\"SurName\", "
                + "\"person\".\"Salary\", \"person\".\"DateCreated\", \"person\".\"DepartmentId\"", result.Sql);
        }

        [Fact]
        public void One_Param_Typed_Expression()
        {
            IColumn column = sql.Col<Person>(x => x.Id);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\"", result.Sql);
        }

        [Fact]
        public void One_Param_All_Typed_Expression()
        {
            IColumn column = sql.Col<Person>(x => x);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\", \"person\".\"Active\", \"person\".\"Name\", \"person\".\"SurName\", "
                + "\"person\".\"Salary\", \"person\".\"DateCreated\", \"person\".\"DepartmentId\"", result.Sql);
        }

        [Fact]
        public void Two_Params_Typed_String()
        {
            IColumn column = sql.Col<Person>("person", "Id");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\"", result.Sql);
        }

        [Fact]
        public void Two_Params_All_Typed_String()
        {
            IColumn column = sql.Col<Person>("person", "*");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\", \"person\".\"Active\", \"person\".\"Name\", \"person\".\"SurName\", "
                + "\"person\".\"Salary\", \"person\".\"DateCreated\", \"person\".\"DepartmentId\"", result.Sql);
        }

        [Fact]
        public void Two_Params_Typed_Expression()
        {
            IColumn column = sql.Col<Person>("person", x => x.Id);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\"", result.Sql);
        }

        [Fact]
        public void Two_Params_All_Typed_Expression()
        {
            IColumn column = sql.Col<Person>("person", x => x);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\", \"person\".\"Active\", \"person\".\"Name\", \"person\".\"SurName\", "
                + "\"person\".\"Salary\", \"person\".\"DateCreated\", \"person\".\"DepartmentId\"", result.Sql);
        }


        [Fact]
        public void Without_Table_Name()
        {
            IColumn column = sql.Col("Id");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"Id\"", result.Sql);
        }

        [Fact]
        public void Without_Table_Name_All()
        {
            IColumn column = sql.Col("*");

            QueryResult result = engine.Compile(column);

            Assert.Equal("*", result.Sql);
        }

        [Fact]
        public void Escape_Characters()
        {
            engine.Options.EscapeStart = '[';
            engine.Options.EscapeEnd = ']';

            IColumn column = sql.Col("dbo.person.Id");

            QueryResult result = engine.Compile(column);

            Assert.Equal("[dbo].[person].[Id]", result.Sql);
        }
    }
}