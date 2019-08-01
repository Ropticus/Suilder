using Suilder.Builder;
using Suilder.Core;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Alias
{
    public class ColumnWithoutAliasTest : BaseTest
    {
        [Fact]
        public void One_Param()
        {
            IColumn column = sql.Col("person.Id");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\"", result.Sql);
        }

        [Fact]
        public void All_One_Param()
        {
            IColumn column = sql.Col("person.*");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".*", result.Sql);
        }

        [Fact]
        public void One_Param_With_Schema()
        {
            IColumn column = sql.Col("dbo.person.Id");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"dbo\".\"person\".\"Id\"", result.Sql);
        }

        [Fact]
        public void Two_Params()
        {
            IColumn column = sql.Col("person", "Id");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\"", result.Sql);
        }

        [Fact]
        public void All_Two_Params()
        {
            IColumn column = sql.Col("person", "*");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".*", result.Sql);
        }

        [Fact]
        public void Two_Params_With_Schema()
        {
            IColumn column = sql.Col("dbo.person", "Id");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"dbo\".\"person\".\"Id\"", result.Sql);
        }

        [Fact]
        public void One_Param_Typed()
        {
            IColumn column = sql.Col<Person>("person.Id");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\"", result.Sql);
        }

        [Fact]
        public void All_One_Param_Typed()
        {
            IColumn column = sql.Col<Person>("person.*");

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
        public void All_Without_Table_Name()
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