using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Engines;
using Xunit;

namespace Suilder.Test.Builder
{
    public class OffsetTest : BaseTest
    {
        [Fact]
        public void Offset()
        {
            IOffset offset = sql.Offset(10);

            QueryResult result = engine.Compile(offset);

            Assert.Equal("OFFSET @p0 ROWS", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = 10 }, result.Parameters);
        }

        [Fact]
        public void Fetch()
        {
            IOffset offset = sql.Fetch(20);

            QueryResult result = engine.Compile(offset);

            Assert.Equal("OFFSET @p0 ROWS FETCH NEXT @p1 ROWS ONLY", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = 0, ["@p1"] = 20 }, result.Parameters);
        }

        [Fact]
        public void Offset_Fetch()
        {
            IOffset offset = sql.Offset(10).Fetch(20);

            QueryResult result = engine.Compile(offset);

            Assert.Equal("OFFSET @p0 ROWS FETCH NEXT @p1 ROWS ONLY", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = 10, ["@p1"] = 20 }, result.Parameters);
        }

        [Fact]
        public void Fetch_Offset()
        {
            IOffset offset = sql.Fetch(20).Offset(10);

            QueryResult result = engine.Compile(offset);

            Assert.Equal("OFFSET @p0 ROWS FETCH NEXT @p1 ROWS ONLY", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = 10, ["@p1"] = 20 }, result.Parameters);
        }

        [Fact]
        public void Offset_Fetch_Overload()
        {
            IOffset offset = sql.Offset(10, 20);

            QueryResult result = engine.Compile(offset);

            Assert.Equal("OFFSET @p0 ROWS FETCH NEXT @p1 ROWS ONLY", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = 10, ["@p1"] = 20 }, result.Parameters);
        }

        [Fact]
        public void Offset_Limit()
        {
            engine.Options.OffsetStyle = OffsetStyle.Limit;

            IOffset offset = sql.Offset(10);

            QueryResult result = engine.Compile(offset);

            Assert.Equal("LIMIT @p0 OFFSET @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = long.MaxValue, ["@p1"] = 10 }, result.Parameters);
        }

        [Fact]
        public void Fetch_Limit()
        {
            engine.Options.OffsetStyle = OffsetStyle.Limit;

            IOffset offset = sql.Fetch(20);

            QueryResult result = engine.Compile(offset);

            Assert.Equal("LIMIT @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = 20 }, result.Parameters);
        }

        [Fact]
        public void Offset_Fetch_Limit()
        {
            engine.Options.OffsetStyle = OffsetStyle.Limit;

            IOffset offset = sql.Offset(10).Fetch(20);

            QueryResult result = engine.Compile(offset);

            Assert.Equal("LIMIT @p0 OFFSET @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = 20, ["@p1"] = 10 }, result.Parameters);
        }

        [Fact]
        public void Offset_Without_Parameters()
        {
            engine.Options.OffsetAsParameters = false;

            IOffset offset = sql.Offset(10).Fetch(20);

            QueryResult result = engine.Compile(offset);

            Assert.Equal("OFFSET 10 ROWS FETCH NEXT 20 ROWS ONLY", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Limit_Without_Parameters()
        {
            engine.Options.OffsetStyle = OffsetStyle.Limit;
            engine.Options.OffsetAsParameters = false;

            IOffset offset = sql.Offset(10).Fetch(20);

            QueryResult result = engine.Compile(offset);

            Assert.Equal("LIMIT 20 OFFSET 10", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void To_String()
        {
            IOffset offset = sql.Offset(10).Fetch(20);

            Assert.Equal("OFFSET 10 FETCH 20", offset.ToString());
        }
    }
}