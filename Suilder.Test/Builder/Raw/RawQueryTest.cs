using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Xunit;

namespace Suilder.Test.Builder.Raw
{
    public class RawQueryTest : BaseTest
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
        public void Offset()
        {
            IRawQuery rawQuery = sql.RawQuery("SELECT * FROM person").Offset(10);

            QueryResult result = engine.Compile(rawQuery);

            Assert.Equal("SELECT * FROM person OFFSET @p0 ROWS", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = 10 }, result.Parameters);
        }

        [Fact]
        public void Offset_Fetch_Overload()
        {
            IRawQuery rawQuery = sql.RawQuery("SELECT * FROM person").Offset(10, 20);

            QueryResult result = engine.Compile(rawQuery);

            Assert.Equal("SELECT * FROM person OFFSET @p0 ROWS FETCH NEXT @p1 ROWS ONLY", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = 10, ["@p1"] = 20 }, result.Parameters);
        }

        [Fact]
        public void Fetch()
        {
            IRawQuery rawQuery = sql.RawQuery("SELECT * FROM person").Fetch(20);

            QueryResult result = engine.Compile(rawQuery);

            Assert.Equal("SELECT * FROM person OFFSET @p0 ROWS FETCH NEXT @p1 ROWS ONLY", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = 0, ["@p1"] = 20 }, result.Parameters);
        }

        [Fact]
        public void Offset_Fetch()
        {
            IRawQuery rawQuery = sql.RawQuery("SELECT * FROM person").Offset(10).Fetch(20);

            QueryResult result = engine.Compile(rawQuery);

            Assert.Equal("SELECT * FROM person OFFSET @p0 ROWS FETCH NEXT @p1 ROWS ONLY", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = 10, ["@p1"] = 20 }, result.Parameters);
        }

        [Fact]
        public void Fetch_Offset()
        {
            IRawQuery rawQuery = sql.RawQuery("SELECT * FROM person").Fetch(20).Offset(10);

            QueryResult result = engine.Compile(rawQuery);

            Assert.Equal("SELECT * FROM person OFFSET @p0 ROWS FETCH NEXT @p1 ROWS ONLY", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = 10, ["@p1"] = 20 }, result.Parameters);
        }

        [Fact]
        public void Offset_Value()
        {
            IRawQuery rawQuery = sql.RawQuery("SELECT * FROM person").Offset(sql.Offset(10).Fetch(20));

            QueryResult result = engine.Compile(rawQuery);

            Assert.Equal("SELECT * FROM person OFFSET @p0 ROWS FETCH NEXT @p1 ROWS ONLY", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = 10, ["@p1"] = 20 }, result.Parameters);
        }

        [Fact]
        public void Offset_Raw()
        {
            IRawQuery rawQuery = sql.RawQuery("SELECT * FROM person")
                .Offset(sql.Raw("OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY", 10, 20));

            QueryResult result = engine.Compile(rawQuery);

            Assert.Equal("SELECT * FROM person OFFSET @p0 ROWS FETCH NEXT @p1 ROWS ONLY", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = 10, ["@p1"] = 20 }, result.Parameters);
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
        public void To_String()
        {
            IRawQuery rawQuery = sql.RawQuery("SELECT * FROM person").Offset(10).Fetch(20);

            Assert.Equal("SELECT * FROM person OFFSET 10 FETCH 20", rawQuery.ToString());
        }
    }
}