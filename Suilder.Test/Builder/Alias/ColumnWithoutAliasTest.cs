using Suilder.Builder;
using Suilder.Core;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Alias
{
    public class ColumnWithoutAliasTest : BuilderBaseTest
    {
        [Fact]
        public void String_Column_One_Param()
        {
            IColumn column = sql.Col("person.Id");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\"", result.Sql);
        }

        [Fact]
        public void String_All_Columns_One_Param()
        {
            IColumn column = sql.Col("person.*");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".*", result.Sql);
        }

        [Fact]
        public void String_Column_With_Schema_One_Param()
        {
            IColumn column = sql.Col("dbo.person.Id");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"dbo\".\"person\".\"Id\"", result.Sql);
        }

        [Fact]
        public void String_Column_Two_Params()
        {
            IColumn column = sql.Col("person", "Id");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\"", result.Sql);
        }

        [Fact]
        public void String_All_Column_Two_Params()
        {
            IColumn column = sql.Col("person", "*");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".*", result.Sql);
        }

        [Fact]
        public void String_Column_With_Schema_Two_Params()
        {
            IColumn column = sql.Col("dbo.person", "Id");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"dbo\".\"person\".\"Id\"", result.Sql);
        }

        [Fact]
        public void Typed_String_Column_One_Param()
        {
            IColumn column = sql.Col<Person>("person.Id");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\"", result.Sql);
        }

        [Fact]
        public void Typed_String_All_Columns_One_Param()
        {
            IColumn column = sql.Col<Person>("person.*");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\", \"person\".\"Active\", \"person\".\"Name\", \"person\".\"SurName\", "
                + "\"person\".\"AddressStreet\", \"person\".\"AddressCity\", \"person\".\"Salary\", "
                + "\"person\".\"DateCreated\", \"person\".\"DepartmentId\"", result.Sql);
        }

        [Fact]
        public void Expression_Column_One_Param()
        {
            IColumn column = sql.Col<Person>(x => x.Id);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\"", result.Sql);
        }

        [Fact]
        public void Expression_All_Columns_One_Param()
        {
            IColumn column = sql.Col<Person>(x => x);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\", \"person\".\"Active\", \"person\".\"Name\", \"person\".\"SurName\", "
                + "\"person\".\"AddressStreet\", \"person\".\"AddressCity\", \"person\".\"Salary\", "
                + "\"person\".\"DateCreated\", \"person\".\"DepartmentId\"", result.Sql);
        }

        [Fact]
        public void Typed_String_Column_Two_Params()
        {
            IColumn column = sql.Col<Person>("person", "Id");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\"", result.Sql);
        }

        [Fact]
        public void Typed_String_All_Columns_Two_Params()
        {
            IColumn column = sql.Col<Person>("person", "*");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\", \"person\".\"Active\", \"person\".\"Name\", \"person\".\"SurName\", "
                + "\"person\".\"AddressStreet\", \"person\".\"AddressCity\", \"person\".\"Salary\", "
                + "\"person\".\"DateCreated\", \"person\".\"DepartmentId\"", result.Sql);
        }

        [Fact]
        public void Expression_Column_Two_Params()
        {
            IColumn column = sql.Col<Person>("person", x => x.Id);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\"", result.Sql);
        }

        [Fact]
        public void Expression_All_Columns_Two_Params()
        {
            IColumn column = sql.Col<Person>("person", x => x);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\", \"person\".\"Active\", \"person\".\"Name\", \"person\".\"SurName\", "
                + "\"person\".\"AddressStreet\", \"person\".\"AddressCity\", \"person\".\"Salary\", "
                + "\"person\".\"DateCreated\", \"person\".\"DepartmentId\"", result.Sql);
        }

        [Fact]
        public void String_Column_Without_Table_Name()
        {
            IColumn column = sql.Col("Id");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"Id\"", result.Sql);
        }

        [Fact]
        public void String_All_Columns_Without_Table_Name()
        {
            IColumn column = sql.Col("*");

            QueryResult result = engine.Compile(column);

            Assert.Equal("*", result.Sql);
        }

        [Fact]
        public void Typed_String_Column_Without_Table_Name()
        {
            IColumn column = sql.Col<Person>("Id");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"Id\"", result.Sql);
        }

        [Fact]
        public void Typed_String_All_Columns_Without_Table_Name()
        {
            IColumn column = sql.Col<Person>("*");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"Id\", \"Active\", \"Name\", \"SurName\", \"AddressStreet\", \"AddressCity\", \"Salary\", "
                + "\"DateCreated\", \"DepartmentId\"", result.Sql);
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