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

        [Fact]
        public void Boolean_Value_Expression()
        {
            bool value = true;
            IRawSql raw = sql.Raw("{0}", sql.Val(() => value));

            QueryResult result = engine.Compile(raw);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Byte_Value_Expression()
        {
            byte value = 1;
            IRawSql raw = sql.Raw("{0}", sql.Val(() => value));

            QueryResult result = engine.Compile(raw);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void SByte_Value_Expression()
        {
            sbyte value = 1;
            IRawSql raw = sql.Raw("{0}", sql.Val(() => value));

            QueryResult result = engine.Compile(raw);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Int16_Value_Expression()
        {
            short value = 1;
            IRawSql raw = sql.Raw("{0}", sql.Val(() => value));

            QueryResult result = engine.Compile(raw);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void UInt16_Value_Expression()
        {
            ushort value = 1;
            IRawSql raw = sql.Raw("{0}", sql.Val(() => value));

            QueryResult result = engine.Compile(raw);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Int32_Value_Expression()
        {
            int value = 1;
            IRawSql raw = sql.Raw("{0}", sql.Val(() => value));

            QueryResult result = engine.Compile(raw);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void UInt32_Value_Expression()
        {
            uint value = 1;
            IRawSql raw = sql.Raw("{0}", sql.Val(() => value));

            QueryResult result = engine.Compile(raw);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Int64_Value_Expression()
        {
            long value = 1;
            IRawSql raw = sql.Raw("{0}", sql.Val(() => value));

            QueryResult result = engine.Compile(raw);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void UInt64_Value_Expression()
        {
            ulong value = 1;
            IRawSql raw = sql.Raw("{0}", sql.Val(() => value));

            QueryResult result = engine.Compile(raw);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Single_Value_Expression()
        {
            float value = 1.5f;
            IRawSql raw = sql.Raw("{0}", sql.Val(() => value));

            QueryResult result = engine.Compile(raw);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Double_Value_Expression()
        {
            double value = 1.5;
            IRawSql raw = sql.Raw("{0}", sql.Val(() => value));

            QueryResult result = engine.Compile(raw);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Decimal_Value_Expression()
        {
            decimal value = 1.5m;
            IRawSql raw = sql.Raw("{0}", sql.Val(() => value));

            QueryResult result = engine.Compile(raw);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Char_Value_Expression()
        {
            char value = 'a';
            IRawSql raw = sql.Raw("{0}", sql.Val(() => value));

            QueryResult result = engine.Compile(raw);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void String_Value_Expression()
        {
            string value = "abcd";
            IRawSql raw = sql.Raw("{0}", sql.Val(() => value));

            QueryResult result = engine.Compile(raw);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void DateTime_Value_Expression()
        {
            DateTime value = DateTime.Now;
            IRawSql raw = sql.Raw("{0}", sql.Val(() => value));

            QueryResult result = engine.Compile(raw);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Null_Value_Expression()
        {
            string value = null;
            IRawSql raw = sql.Raw("{0}", sql.Val(() => value));

            QueryResult result = engine.Compile(raw);

            Assert.Equal("NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Array_Value_Expression()
        {
            byte[] values = new byte[] { 1, 2, 3 };
            IRawSql raw = sql.Raw("{0}", sql.Val(() => values));

            QueryResult result = engine.Compile(raw);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = values
            }, result.Parameters);
        }

        [Fact]
        public void Boolean_Value_Expression_Inline()
        {
            IRawSql raw = sql.Raw("{0}", sql.Val(() => true));

            QueryResult result = engine.Compile(raw);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = true
            }, result.Parameters);
        }

        [Fact]
        public void Byte_Value_Expression_Inline()
        {
            IRawSql raw = sql.Raw("{0}", sql.Val(() => (byte)1));

            QueryResult result = engine.Compile(raw);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = (byte)1
            }, result.Parameters);
        }

        [Fact]
        public void SByte_Value_Expression_Inline()
        {
            IRawSql raw = sql.Raw("{0}", sql.Val(() => (sbyte)1));

            QueryResult result = engine.Compile(raw);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = (sbyte)1
            }, result.Parameters);
        }

        [Fact]
        public void Int16_Value_Expression_Inline()
        {
            IRawSql raw = sql.Raw("{0}", sql.Val(() => (short)1));

            QueryResult result = engine.Compile(raw);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = (short)1
            }, result.Parameters);
        }

        [Fact]
        public void UInt16_Value_Expression_Inline()
        {
            IRawSql raw = sql.Raw("{0}", sql.Val(() => (ushort)1));

            QueryResult result = engine.Compile(raw);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = (ushort)1
            }, result.Parameters);
        }

        [Fact]
        public void Int32_Value_Expression_Inline()
        {
            IRawSql raw = sql.Raw("{0}", sql.Val(() => 1));

            QueryResult result = engine.Compile(raw);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1
            }, result.Parameters);
        }

        [Fact]
        public void UInt32_Value_Expression_Inline()
        {
            IRawSql raw = sql.Raw("{0}", sql.Val(() => 1u));

            QueryResult result = engine.Compile(raw);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1u
            }, result.Parameters);
        }

        [Fact]
        public void Int64_Value_Expression_Inline()
        {
            IRawSql raw = sql.Raw("{0}", sql.Val(() => 1L));

            QueryResult result = engine.Compile(raw);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1L
            }, result.Parameters);
        }

        [Fact]
        public void UInt64_Value_Expression_Inline()
        {
            IRawSql raw = sql.Raw("{0}", sql.Val(() => 1uL));

            QueryResult result = engine.Compile(raw);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1uL
            }, result.Parameters);
        }

        [Fact]
        public void Single_Value_Expression_Inline()
        {
            IRawSql raw = sql.Raw("{0}", sql.Val(() => 1.5f));

            QueryResult result = engine.Compile(raw);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1.5f
            }, result.Parameters);
        }

        [Fact]
        public void Double_Value_Expression_Inline()
        {
            IRawSql raw = sql.Raw("{0}", sql.Val(() => 1.5));

            QueryResult result = engine.Compile(raw);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1.5
            }, result.Parameters);
        }

        [Fact]
        public void Decimal_Value_Expression_Inline()
        {
            IRawSql raw = sql.Raw("{0}", sql.Val(() => 1.5m));

            QueryResult result = engine.Compile(raw);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1.5m
            }, result.Parameters);
        }

        [Fact]
        public void Char_Value_Expression_Inline()
        {
            IRawSql raw = sql.Raw("{0}", sql.Val(() => 'a'));

            QueryResult result = engine.Compile(raw);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 'a'
            }, result.Parameters);
        }

        [Fact]
        public void String_Value_Expression_Inline()
        {
            IRawSql raw = sql.Raw("{0}", sql.Val(() => "abcd"));

            QueryResult result = engine.Compile(raw);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "abcd"
            }, result.Parameters);
        }

        [Fact]
        public void DateTime_Value_Expression_Inline()
        {
            IRawSql raw = sql.Raw("{0}", sql.Val(() => new DateTime(2000, 1, 1)));

            QueryResult result = engine.Compile(raw);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = new DateTime(2000, 1, 1)
            }, result.Parameters);
        }

        [Fact]
        public void Null_Value_Expression_Inline()
        {
            IRawSql raw = sql.Raw("{0}", sql.Val(() => null));

            QueryResult result = engine.Compile(raw);

            Assert.Equal("NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Array_Value_Expression_Inline()
        {
            IRawSql raw = sql.Raw("{0}", sql.Val(() => new byte[] { 1, 2, 3 }));

            QueryResult result = engine.Compile(raw);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = new byte[] { 1, 2, 3 }
            }, result.Parameters);
        }
    }
}