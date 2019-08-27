using Suilder.Builder;
using Suilder.Core;
using Xunit;

namespace Suilder.Test.Builder.Alias
{
    public class StringAliasColumnTest : BuilderBaseTest
    {
        [Fact]
        public void Indexer_Column()
        {
            IAlias person = sql.Alias("person");
            IColumn column = person["Id"];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\"", result.Sql);
        }

        [Fact]
        public void Indexer_All_Columns()
        {
            IAlias person = sql.Alias("person");
            IColumn column = person["*"];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".*", result.Sql);
        }

        [Fact]
        public void Col_Column()
        {
            IAlias person = sql.Alias("person");
            IColumn column = person.Col("Id");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\"", result.Sql);
        }

        [Fact]
        public void Col_All_Columns()
        {
            IAlias person = sql.Alias("person");
            IColumn column = person.Col("*");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".*", result.Sql);
        }

        [Fact]
        public void All_Property()
        {
            IAlias person = sql.Alias("person");
            IColumn column = person.All;

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".*", result.Sql);
        }

        [Fact]
        public void Indexer_Column_With_Alias_Name()
        {
            IAlias person = sql.Alias("person", "per");
            IColumn column = person["Id"];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"Id\"", result.Sql);
        }

        [Fact]
        public void Indexer_All_Columns_With_Alias_Name()
        {
            IAlias person = sql.Alias("person", "per");
            IColumn column = person["*"];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".*", result.Sql);
        }

        [Fact]
        public void Col_Column_With_Alias_Name()
        {
            IAlias person = sql.Alias("person", "per");
            IColumn column = person.Col("Id");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"Id\"", result.Sql);
        }

        [Fact]
        public void Col_All_Columns_With_Alias_Name()
        {
            IAlias person = sql.Alias("person", "per");
            IColumn column = person.Col("*");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".*", result.Sql);
        }

        [Fact]
        public void All_Property_With_Alias_Name()
        {
            IAlias person = sql.Alias("person", "per");
            IColumn column = person.All;

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".*", result.Sql);
        }

        [Fact]
        public void To_String()
        {
            IAlias person = sql.Alias("person");
            IColumn column = person["Id"];

            Assert.Equal("person.Id", column.ToString());
        }
    }
}