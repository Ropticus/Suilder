using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Xunit;

namespace Suilder.Test.Builder.Alias.StringAlias
{
    public class ColumnSchemaTest : BuilderBaseTest
    {
        [Fact]
        public void All_Property()
        {
            IAlias person = sql.Alias("dbo.person");
            IColumn column = person.All;

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"dbo\".\"person\".*", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Indexer_All()
        {
            IAlias person = sql.Alias("dbo.person");
            IColumn column = person["*"];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"dbo\".\"person\".*", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Indexer_Column()
        {
            IAlias person = sql.Alias("dbo.person");
            IColumn column = person["Id"];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"dbo\".\"person\".\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_All()
        {
            IAlias person = sql.Alias("dbo.person");
            IColumn column = person.Col("*");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"dbo\".\"person\".*", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_Column()
        {
            IAlias person = sql.Alias("dbo.person");
            IColumn column = person.Col("Id");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"dbo\".\"person\".\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void All_Property_With_Alias_Name()
        {
            IAlias person = sql.Alias("dbo.person", "per");
            IColumn column = person.All;

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".*", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Indexer_All_With_Alias_Name()
        {
            IAlias person = sql.Alias("dbo.person", "per");
            IColumn column = person["*"];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".*", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Indexer_Column_With_Alias_Name()
        {
            IAlias person = sql.Alias("dbo.person", "per");
            IColumn column = person["Id"];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_All_With_Alias_Name()
        {
            IAlias person = sql.Alias("dbo.person", "per");
            IColumn column = person.Col("*");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".*", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_Column_With_Alias_Name()
        {
            IAlias person = sql.Alias("dbo.person", "per");
            IColumn column = person.Col("Id");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void To_String()
        {
            IAlias person = sql.Alias("dbo.person");
            IColumn column = person["Id"];

            Assert.Equal("dbo.person.Id", column.ToString());
        }

        [Fact]
        public void To_String_With_Alias_Name()
        {
            IAlias person = sql.Alias("dbo.person", "per");
            IColumn column = person["Id"];

            Assert.Equal("per.Id", column.ToString());
        }
    }
}