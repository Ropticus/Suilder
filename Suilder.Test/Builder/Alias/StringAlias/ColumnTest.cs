using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Xunit;

namespace Suilder.Test.Builder.Alias.StringAlias
{
    public class ColumnTest : BuilderBaseTest
    {
        [Fact]
        public void All_Property()
        {
            IAlias person = sql.Alias("person");
            IColumn column = person.All;

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".*", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Indexer_All()
        {
            IAlias person = sql.Alias("person");
            IColumn column = person["*"];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".*", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Indexer_Column()
        {
            IAlias person = sql.Alias("person");
            IColumn column = person["Id"];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_All()
        {
            IAlias person = sql.Alias("person");
            IColumn column = person.Col("*");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".*", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_Column()
        {
            IAlias person = sql.Alias("person");
            IColumn column = person.Col("Id");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void All_Property_With_Alias_Name()
        {
            IAlias person = sql.Alias("person", "per");
            IColumn column = person.All;

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".*", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Indexer_All_With_Alias_Name()
        {
            IAlias person = sql.Alias("person", "per");
            IColumn column = person["*"];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".*", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Indexer_Column_With_Alias_Name()
        {
            IAlias person = sql.Alias("person", "per");
            IColumn column = person["Id"];

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_All_With_Alias_Name()
        {
            IAlias person = sql.Alias("person", "per");
            IColumn column = person.Col("*");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".*", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Col_Column_With_Alias_Name()
        {
            IAlias person = sql.Alias("person", "per");
            IColumn column = person.Col("Id");

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"per\".\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Name_All()
        {
            IAlias person = sql.Alias("person");
            IColumn column = person.All.Name;

            QueryResult result = engine.Compile(column);

            Assert.Equal("*", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Name_Column()
        {
            IAlias person = sql.Alias("person");
            IColumn column = person["Id"].Name;

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Name_All_With_Alias_Name()
        {
            IAlias person = sql.Alias("person", "per");
            IColumn column = person.All.Name;

            QueryResult result = engine.Compile(column);

            Assert.Equal("*", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Name_Column_With_Alias_Name()
        {
            IAlias person = sql.Alias("person", "per");
            IColumn column = person["Id"].Name;

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Name_Name_All()
        {
            IAlias person = sql.Alias("person");
            IColumn column = person.All.Name.Name;

            QueryResult result = engine.Compile(column);

            Assert.Equal("*", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Name_Name_Column()
        {
            IAlias person = sql.Alias("person");
            IColumn column = person["Id"].Name.Name;

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void To_String_All()
        {
            IAlias person = sql.Alias("person");
            IColumn column = person.All;

            Assert.Equal("person.*", column.ToString());
        }

        [Fact]
        public void To_String_Column()
        {
            IAlias person = sql.Alias("person");
            IColumn column = person["Id"];

            Assert.Equal("person.Id", column.ToString());
        }

        [Fact]
        public void To_String_All_With_Alias_Name()
        {
            IAlias person = sql.Alias("person", "per");
            IColumn column = person.All;

            Assert.Equal("per.*", column.ToString());
        }

        [Fact]
        public void To_String_Column_With_Alias_Name()
        {
            IAlias person = sql.Alias("person", "per");
            IColumn column = person["Id"];

            Assert.Equal("per.Id", column.ToString());
        }

        [Fact]
        public void To_String_Name_All()
        {
            IAlias person = sql.Alias("person");
            IColumn column = person.All.Name;

            Assert.Equal("*", column.ToString());
        }

        [Fact]
        public void To_String_Name_Column()
        {
            IAlias person = sql.Alias("person");
            IColumn column = person["Id"].Name;

            Assert.Equal("Id", column.ToString());
        }

        [Fact]
        public void To_String_Name_All_With_Alias_Name()
        {
            IAlias person = sql.Alias("person", "per");
            IColumn column = person.All.Name;

            Assert.Equal("*", column.ToString());
        }

        [Fact]
        public void To_String_Name_Colum_With_Alias_Name()
        {
            IAlias person = sql.Alias("person", "per");
            IColumn column = person["Id"].Name;

            Assert.Equal("Id", column.ToString());
        }
    }
}