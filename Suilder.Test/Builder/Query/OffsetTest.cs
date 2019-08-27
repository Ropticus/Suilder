using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Xunit;

namespace Suilder.Test.Builder.Query
{
    public class OffsetTest : BuilderBaseTest
    {
        [Fact]
        public void Offset()
        {
            IQuery query = sql.Query.Offset(10);

            QueryResult result = engine.Compile(query);

            Assert.Equal("OFFSET @p0 ROWS", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 10
            }, result.Parameters);
        }

        [Fact]
        public void Offset_Fetch_Overload()
        {
            IQuery query = sql.Query.Offset(10, 20);

            QueryResult result = engine.Compile(query);

            Assert.Equal("OFFSET @p0 ROWS FETCH NEXT @p1 ROWS ONLY", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 10,
                ["@p1"] = 20
            }, result.Parameters);
        }

        [Fact]
        public void Fetch()
        {
            IQuery query = sql.Query.Fetch(20);

            QueryResult result = engine.Compile(query);

            Assert.Equal("OFFSET @p0 ROWS FETCH NEXT @p1 ROWS ONLY", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 0,
                ["@p1"] = 20
            }, result.Parameters);
        }

        [Fact]
        public void Offset_Fetch()
        {
            IQuery query = sql.Query.Offset(10).Fetch(20);

            QueryResult result = engine.Compile(query);

            Assert.Equal("OFFSET @p0 ROWS FETCH NEXT @p1 ROWS ONLY", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 10,
                ["@p1"] = 20
            }, result.Parameters);
        }

        [Fact]
        public void Fetch_Offset()
        {
            IQuery query = sql.Query.Fetch(20).Offset(10);

            QueryResult result = engine.Compile(query);

            Assert.Equal("OFFSET @p0 ROWS FETCH NEXT @p1 ROWS ONLY", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 10,
                ["@p1"] = 20
            }, result.Parameters);
        }

        [Fact]
        public void Offset_Value()
        {
            IQuery query = sql.Query.Offset(sql.Offset(10, 20));

            QueryResult result = engine.Compile(query);

            Assert.Equal("OFFSET @p0 ROWS FETCH NEXT @p1 ROWS ONLY", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 10,
                ["@p1"] = 20
            }, result.Parameters);
        }

        [Fact]
        public void Offset_Raw()
        {
            IQuery query = sql.Query.Offset(sql.Raw("OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY", 10, 20));

            QueryResult result = engine.Compile(query);

            Assert.Equal("OFFSET @p0 ROWS FETCH NEXT @p1 ROWS ONLY", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 10,
                ["@p1"] = 20
            }, result.Parameters);
        }
    }
}