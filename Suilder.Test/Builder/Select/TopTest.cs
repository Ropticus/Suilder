using System;
using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Exceptions;
using Xunit;

namespace Suilder.Test.Builder.Select
{
    public class TopTest : BuilderBaseTest
    {
        [Fact]
        public void Top()
        {
            ITop top = sql.Top(10);

            QueryResult result = engine.Compile(top);

            Assert.Equal("TOP(@p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 10
            }, result.Parameters);
        }

        [Fact]
        public void Top_Percent()
        {
            ITop top = sql.Top(10).Percent();

            QueryResult result = engine.Compile(top);

            Assert.Equal("TOP(@p0) PERCENT", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 10
            }, result.Parameters);
        }

        [Fact]
        public void Top_With_Ties()
        {
            ITop top = sql.Top(10).WithTies();

            QueryResult result = engine.Compile(top);

            Assert.Equal("TOP(@p0) WITH TIES", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 10
            }, result.Parameters);
        }

        [Fact]
        public void Top_Percent_With_Ties()
        {
            ITop top = sql.Top(10).Percent().WithTies();

            QueryResult result = engine.Compile(top);

            Assert.Equal("TOP(@p0) PERCENT WITH TIES", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 10
            }, result.Parameters);
        }

        [Fact]
        public void Top_Without_Parameters()
        {
            engine.Options.TopAsParameters = false;

            ITop top = sql.Top(10);

            QueryResult result = engine.Compile(top);

            Assert.Equal("TOP(10)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Top_Not_Supported()
        {
            engine.Options.TopSupported = false;

            ITop top = sql.Top(10);

            Exception ex = Assert.Throws<ClauseNotSupportedException>(() => engine.Compile(top));
            Assert.Equal("Top clause is not supported in this engine.", ex.Message);
        }

        [Fact]
        public void To_String()
        {
            ITop top = sql.Top(10);

            Assert.Equal("TOP(10)", top.ToString());
        }

        [Fact]
        public void To_String_Percent()
        {
            ITop top = sql.Top(10).Percent();

            Assert.Equal("TOP(10) PERCENT", top.ToString());
        }

        [Fact]
        public void To_String_With_Ties()
        {
            ITop top = sql.Top(10).WithTies();

            Assert.Equal("TOP(10) WITH TIES", top.ToString());
        }

        [Fact]
        public void To_String_Percent_With_Ties()
        {
            ITop top = sql.Top(10).Percent().WithTies();

            Assert.Equal("TOP(10) PERCENT WITH TIES", top.ToString());
        }
    }
}