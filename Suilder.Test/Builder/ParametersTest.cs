using System;
using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Xunit;

namespace Suilder.Test.Builder
{
    public class ParametersTest : BuilderBaseTest
    {
        [Fact]
        public void Boolean_Value()
        {
            bool value = true;
            IRawSql raw = sql.Raw("{0}", value);

            QueryResult result = engine.Compile(raw);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Byte_Value()
        {
            byte value = 1;
            IRawSql raw = sql.Raw("{0}", value);

            QueryResult result = engine.Compile(raw);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void SByte_Value()
        {
            sbyte value = 1;
            IRawSql raw = sql.Raw("{0}", value);

            QueryResult result = engine.Compile(raw);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Int16_Value()
        {
            short value = 1;
            IRawSql raw = sql.Raw("{0}", value);

            QueryResult result = engine.Compile(raw);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void UInt16_Value()
        {
            ushort value = 1;
            IRawSql raw = sql.Raw("{0}", value);

            QueryResult result = engine.Compile(raw);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Int32_Value()
        {
            int value = 1;
            IRawSql raw = sql.Raw("{0}", value);

            QueryResult result = engine.Compile(raw);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void UInt32_Value()
        {
            uint value = 1;
            IRawSql raw = sql.Raw("{0}", value);

            QueryResult result = engine.Compile(raw);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Int64_Value()
        {
            long value = 1;
            IRawSql raw = sql.Raw("{0}", value);

            QueryResult result = engine.Compile(raw);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void UInt64_Value()
        {
            ulong value = 1;
            IRawSql raw = sql.Raw("{0}", value);

            QueryResult result = engine.Compile(raw);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Single_Value()
        {
            float value = 1.5f;
            IRawSql raw = sql.Raw("{0}", value);

            QueryResult result = engine.Compile(raw);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Double_Value()
        {
            double value = 1.5;
            IRawSql raw = sql.Raw("{0}", value);

            QueryResult result = engine.Compile(raw);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Decimal_Value()
        {
            decimal value = 1.5m;
            IRawSql raw = sql.Raw("{0}", value);

            QueryResult result = engine.Compile(raw);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Char_Value()
        {
            char value = 'a';
            IRawSql raw = sql.Raw("{0}", value);

            QueryResult result = engine.Compile(raw);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void String_Value()
        {
            string value = "abcd";
            IRawSql raw = sql.Raw("{0}", value);

            QueryResult result = engine.Compile(raw);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void DateTime_Value()
        {
            DateTime value = DateTime.Now;
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
        public void Array_Value()
        {
            byte[] values = new byte[] { 1, 2, 3 };
            IRawSql raw = sql.Raw("{0}", values);

            QueryResult result = engine.Compile(raw);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = values
            }, result.Parameters);
        }
    }
}