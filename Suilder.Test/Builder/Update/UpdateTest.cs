using System;
using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Xunit;

namespace Suilder.Test.Builder.Update
{
    public class UpdateTest : BuilderBaseTest
    {
        [Fact]
        public void Update()
        {
            IUpdate update = sql.Update();

            QueryResult result = engine.Compile(update);

            Assert.Equal("UPDATE", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Top()
        {
            IUpdate update = sql.Update().Top(10);

            QueryResult result = engine.Compile(update);

            Assert.Equal("UPDATE TOP(@p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 10
            }, result.Parameters);
        }

        [Fact]
        public void Top_Percent()
        {
            IUpdate update = sql.Update().Top(10).Percent();

            QueryResult result = engine.Compile(update);

            Assert.Equal("UPDATE TOP(@p0) PERCENT", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 10
            }, result.Parameters);
        }

        [Fact]
        public void Top_WithTies()
        {
            IUpdate update = sql.Update().Top(10).WithTies();

            QueryResult result = engine.Compile(update);

            Assert.Equal("UPDATE TOP(@p0) WITH TIES", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 10
            }, result.Parameters);
        }

        [Fact]
        public void Top_Value()
        {
            IUpdate update = sql.Update().Top(sql.Top(10));

            QueryResult result = engine.Compile(update);

            Assert.Equal("UPDATE TOP(@p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 10
            }, result.Parameters);
        }

        [Fact]
        public void Top_Raw()
        {
            IUpdate update = sql.Update().Top(sql.Raw("TOP({0})", 10));

            QueryResult result = engine.Compile(update);

            Assert.Equal("UPDATE TOP(@p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 10
            }, result.Parameters);
        }

        [Fact]
        public void Invalid_Top_Percent()
        {
            IUpdate update = sql.Update().Top(sql.Raw("TOP({0})", 10));

            Exception ex = Assert.Throws<InvalidOperationException>(() => ((IUpdateTop)update).Percent());
            Assert.Equal("Top value must be a \"Suilder.Core.ITop\" instance.", ex.Message);
        }

        [Fact]
        public void Invalid_Top_WithTies()
        {
            IUpdate update = sql.Update().Top(sql.Raw("TOP({0})", 10));

            Exception ex = Assert.Throws<InvalidOperationException>(() => ((IUpdateTop)update).WithTies());
            Assert.Equal("Top value must be a \"Suilder.Core.ITop\" instance.", ex.Message);
        }

        [Fact]
        public void To_String()
        {
            IUpdate update = sql.Update();

            Assert.Equal("UPDATE", update.ToString());
        }

        [Fact]
        public void To_String_Top()
        {
            IUpdate update = sql.Update().Top(10);

            Assert.Equal("UPDATE TOP(10)", update.ToString());
        }
    }
}