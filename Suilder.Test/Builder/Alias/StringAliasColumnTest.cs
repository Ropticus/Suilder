using Suilder.Builder;
using Suilder.Core;
using Xunit;

namespace Suilder.Test.Builder.Alias
{
    public class StringAliasColumnTest : BaseTest
    {
        [Fact]
        public void Column()
        {
            IAlias person = sql.Alias("person");
            IColumn column = person["Id"];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\"", result.Sql);
        }

        [Fact]
        public void All()
        {
            IAlias person = sql.Alias("person");
            IColumn column = person.All;

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".*", result.Sql);
        }

        [Fact]
        public void All_String()
        {
            IAlias person = sql.Alias("person");
            IColumn column = person["*"];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".*", result.Sql);
        }

        [Fact]
        public void With_Alias()
        {
            IAlias person = sql.Alias("person", "per");
            IColumn column = person["Id"];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"Id\"", result.Sql);
        }

        [Fact]
        public void All_With_Alias()
        {
            IAlias person = sql.Alias("person", "per");
            IColumn column = person.All;

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".*", result.Sql);
        }

        [Fact]
        public void All_String_With_Alias()
        {
            IAlias person = sql.Alias("person", "per");
            IColumn column = person["*"];

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