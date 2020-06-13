using System;
using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Engines;
using Xunit;

namespace Suilder.Test.Builder
{
    public class ParametersTest : BuilderBaseTest
    {
        [Theory]
        [MemberData(nameof(DataObjectAll))]
        public void Object_Value(object value)
        {
            IRawSql raw = sql.Raw("{0}", value);

            QueryResult result = engine.Compile(raw);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataArrayAll))]
        public void Array_Value<T>(T[] value)
        {
            IRawSql raw = sql.Raw("{0}", value);

            QueryResult result = engine.Compile(raw);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Null_Value()
        {
            string value = null;
            IRawSql raw = sql.Raw("{0}", value);

            QueryResult result = engine.Compile(raw);

            Assert.Equal("NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }


        [Fact]
        public void Parameter_Prefix_Custom()
        {
            IEngine engine = new Engine();
            engine.Options.ParameterPrefix = ":i";

            IRawSql raw = sql.Raw("{0}", 1);

            QueryResult result = engine.Compile(raw);

            Assert.Equal(":i0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                [":i0"] = 1
            }, result.Parameters);
        }
    }
}