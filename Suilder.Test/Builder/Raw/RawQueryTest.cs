using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Xunit;

namespace Suilder.Test.Builder.Raw
{
    public class RawQueryTest : BuilderBaseTest
    {
        [Fact]
        public void Raw()
        {
            IRawQuery rawQuery = sql.RawQuery("SELECT * FROM person");

            QueryResult result = engine.Compile(rawQuery);

            Assert.Equal("SELECT * FROM person", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Raw_Format()
        {
            IAlias person = sql.Alias("person");
            IRawQuery rawQuery = sql.RawQuery("SELECT {0}, {1} FROM {2}", person["Name"], "abcd", person);

            QueryResult result = engine.Compile(rawQuery);

            Assert.Equal("SELECT \"person\".\"Name\", @p0 FROM \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "abcd"
            }, result.Parameters);
        }

        [Fact]
        public void Before()
        {
            IRawQuery rawQuery = sql.RawQuery("SELECT * FROM person").Before(sql.Raw("BEFORE VALUE"));

            QueryResult result = engine.Compile(rawQuery);

            Assert.Equal("BEFORE VALUE SELECT * FROM person", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void After()
        {
            IRawQuery rawQuery = sql.RawQuery("SELECT * FROM person").After(sql.Raw("AFTER VALUE"));

            QueryResult result = engine.Compile(rawQuery);

            Assert.Equal("SELECT * FROM person AFTER VALUE", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void As_SubQuery()
        {
            IRawQuery rawQuery = sql.RawQuery("SELECT * FROM person");

            QueryResult result = engine.Compile(sql.Raw("{0}", rawQuery));

            Assert.Equal("(SELECT * FROM person)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void To_String()
        {
            IRawQuery rawQuery = sql.RawQuery("SELECT * FROM person");

            Assert.Equal("SELECT * FROM person", rawQuery.ToString());
        }

        [Fact]
        public void To_String_Format()
        {
            IAlias person = sql.Alias("person");
            IRawQuery rawQuery = sql.RawQuery("SELECT {0}, {1} FROM {2}", person["Name"], "abcd", person);

            Assert.Equal("SELECT person.Name, \"abcd\" FROM person", rawQuery.ToString());
        }

        [Fact]
        public void To_String_Offset()
        {
            IRawQuery rawQuery = sql.RawQuery("SELECT * FROM person").Offset(10).Fetch(20);

            Assert.Equal("SELECT * FROM person OFFSET 10 FETCH 20", rawQuery.ToString());
        }

        [Fact]
        public void To_String_Before()
        {
            IRawQuery rawQuery = sql.RawQuery("SELECT * FROM person")
                .Before(sql.Raw("BEFORE VALUE"))
                .Offset(10).Fetch(20);

            Assert.Equal("BEFORE VALUE SELECT * FROM person OFFSET 10 FETCH 20", rawQuery.ToString());
        }

        [Fact]
        public void To_String_After()
        {
            IRawQuery rawQuery = sql.RawQuery("SELECT * FROM person")
                .Offset(10).Fetch(20)
                .After(sql.Raw("AFTER VALUE"));

            Assert.Equal("SELECT * FROM person OFFSET 10 FETCH 20 AFTER VALUE", rawQuery.ToString());
        }
    }
}