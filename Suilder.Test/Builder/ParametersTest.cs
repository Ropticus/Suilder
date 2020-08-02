using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
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
            engine.Options.ParameterPrefix = ":i";

            IRawSql raw = sql.Raw("{0}", 1);

            QueryResult result = engine.Compile(raw);

            Assert.Equal(":i0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                [":i0"] = 1
            }, result.Parameters);
        }

        [Fact]
        public void Parameters()
        {
            IRawSql raw = sql.Raw("{0}, {1}, {2}, {3}, {4}", 1, 2, null, 4, 5);

            QueryResult result = engine.Compile(raw);

            Assert.Equal("@p0, @p1, NULL, @p2, @p3", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = 2,
                ["@p2"] = 4,
                ["@p3"] = 5
            }, result.Parameters);
            Assert.Null(result.ParametersList);
        }

        [Fact]
        public void Parameters_List()
        {
            engine.Options.ParameterPrefix = "?";
            engine.Options.ParameterIndex = false;

            IRawSql raw = sql.Raw("{0}, {1}, {2}, {3}, {4}", 1, 2, null, 4, 5);

            QueryResult result = engine.Compile(raw);

            Assert.Equal("?, ?, NULL, ?, ?", result.Sql);
            Assert.Equal(new List<object> { 1, 2, 4, 5 }, result.ParametersList);
            Assert.Null(result.Parameters);
        }
    }
}