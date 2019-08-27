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
            IValList valList = sql.ValList.Add(value);

            QueryResult result = engine.Compile(valList);

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
            IValList valList = sql.ValList.Add(value);

            QueryResult result = engine.Compile(valList);

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
            IValList valList = sql.ValList.Add(value);

            QueryResult result = engine.Compile(valList);

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
            IValList valList = sql.ValList.Add(value);

            QueryResult result = engine.Compile(valList);

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
            IValList valList = sql.ValList.Add(value);

            QueryResult result = engine.Compile(valList);

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
            IValList valList = sql.ValList.Add(value);

            QueryResult result = engine.Compile(valList);

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
            IValList valList = sql.ValList.Add(value);

            QueryResult result = engine.Compile(valList);

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
            IValList valList = sql.ValList.Add(value);

            QueryResult result = engine.Compile(valList);

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
            IValList valList = sql.ValList.Add(value);

            QueryResult result = engine.Compile(valList);

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
            IValList valList = sql.ValList.Add(value);

            QueryResult result = engine.Compile(valList);

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
            IValList valList = sql.ValList.Add(value);

            QueryResult result = engine.Compile(valList);

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
            IValList valList = sql.ValList.Add(value);

            QueryResult result = engine.Compile(valList);

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
            IValList valList = sql.ValList.Add(value);

            QueryResult result = engine.Compile(valList);

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
            IValList valList = sql.ValList.Add(value);

            QueryResult result = engine.Compile(valList);

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
            IValList valList = sql.ValList.Add(value);

            QueryResult result = engine.Compile(valList);

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
            IValList valList = sql.ValList.Add(value);

            QueryResult result = engine.Compile(valList);

            Assert.Equal("NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Array_Value()
        {
            int[] values = new int[] { 1, 2, 3 };
            IValList valList = sql.ValList.Add((object)values);

            QueryResult result = engine.Compile(valList);

            Assert.Equal("(@p0, @p1, @p2)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = values[0],
                ["@p1"] = values[1],
                ["@p2"] = values[2]
            }, result.Parameters);
        }

        [Fact]
        public void Array_Value_With_Null()
        {
            string[] values = new string[] { "a", null, "c" };
            IValList valList = sql.ValList.Add((object)values);

            QueryResult result = engine.Compile(valList);

            Assert.Equal("(@p0, NULL, @p1)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = values[0],
                ["@p1"] = values[2]
            }, result.Parameters);
        }

        [Fact]
        public void Boolean_Value_Expression()
        {
            bool value = true;
            IValList valList = sql.ValList.Add(() => value);

            QueryResult result = engine.Compile(valList);

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
            IValList valList = sql.ValList.Add(() => value);

            QueryResult result = engine.Compile(valList);

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
            IValList valList = sql.ValList.Add(() => value);

            QueryResult result = engine.Compile(valList);

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
            IValList valList = sql.ValList.Add(() => value);

            QueryResult result = engine.Compile(valList);

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
            IValList valList = sql.ValList.Add(() => value);

            QueryResult result = engine.Compile(valList);

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
            IValList valList = sql.ValList.Add(() => value);

            QueryResult result = engine.Compile(valList);

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
            IValList valList = sql.ValList.Add(() => value);

            QueryResult result = engine.Compile(valList);

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
            IValList valList = sql.ValList.Add(() => value);

            QueryResult result = engine.Compile(valList);

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
            IValList valList = sql.ValList.Add(() => value);

            QueryResult result = engine.Compile(valList);

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
            IValList valList = sql.ValList.Add(() => value);

            QueryResult result = engine.Compile(valList);

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
            IValList valList = sql.ValList.Add(() => value);

            QueryResult result = engine.Compile(valList);

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
            IValList valList = sql.ValList.Add(() => value);

            QueryResult result = engine.Compile(valList);

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
            IValList valList = sql.ValList.Add(() => value);

            QueryResult result = engine.Compile(valList);

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
            IValList valList = sql.ValList.Add(() => value);

            QueryResult result = engine.Compile(valList);

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
            IValList valList = sql.ValList.Add(() => value);

            QueryResult result = engine.Compile(valList);

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
            IValList valList = sql.ValList.Add(() => value);

            QueryResult result = engine.Compile(valList);

            Assert.Equal("NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Array_Value_Expression()
        {
            int[] values = new int[] { 1, 2, 3 };
            IValList valList = sql.ValList.Add(() => values);

            QueryResult result = engine.Compile(valList);

            Assert.Equal("(@p0, @p1, @p2)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = values[0],
                ["@p1"] = values[1],
                ["@p2"] = values[2]
            }, result.Parameters);
        }

        [Fact]
        public void Array_Value_Expression_With_Null()
        {
            string[] values = new string[] { "a", null, "c" };
            IValList valList = sql.ValList.Add(() => values);

            QueryResult result = engine.Compile(valList);

            Assert.Equal("(@p0, NULL, @p1)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = values[0],
                ["@p1"] = values[2]
            }, result.Parameters);
        }

        [Fact]
        public void Boolean_Value_Expression_Inline()
        {
            IValList valList = sql.ValList.Add(() => true);

            QueryResult result = engine.Compile(valList);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = true
            }, result.Parameters);
        }

        [Fact]
        public void Byte_Value_Expression_Inline()
        {
            IValList valList = sql.ValList.Add(() => (byte)1);

            QueryResult result = engine.Compile(valList);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = (byte)1
            }, result.Parameters);
        }

        [Fact]
        public void SByte_Value_Expression_Inline()
        {
            IValList valList = sql.ValList.Add(() => (sbyte)1);

            QueryResult result = engine.Compile(valList);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = (sbyte)1
            }, result.Parameters);
        }

        [Fact]
        public void Int16_Value_Expression_Inline()
        {
            IValList valList = sql.ValList.Add(() => (short)1);

            QueryResult result = engine.Compile(valList);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = (short)1
            }, result.Parameters);
        }

        [Fact]
        public void UInt16_Value_Expression_Inline()
        {
            IValList valList = sql.ValList.Add(() => (ushort)1);

            QueryResult result = engine.Compile(valList);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = (ushort)1
            }, result.Parameters);
        }

        [Fact]
        public void Int32_Value_Expression_Inline()
        {
            IValList valList = sql.ValList.Add(() => 1);

            QueryResult result = engine.Compile(valList);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1
            }, result.Parameters);
        }

        [Fact]
        public void UInt32_Value_Expression_Inline()
        {
            IValList valList = sql.ValList.Add(() => 1u);

            QueryResult result = engine.Compile(valList);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1u
            }, result.Parameters);
        }

        [Fact]
        public void Int64_Value_Expression_Inline()
        {
            IValList valList = sql.ValList.Add(() => 1L);

            QueryResult result = engine.Compile(valList);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1L
            }, result.Parameters);
        }

        [Fact]
        public void UInt64_Value_Expression_Inline()
        {
            IValList valList = sql.ValList.Add(() => 1uL);

            QueryResult result = engine.Compile(valList);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1uL
            }, result.Parameters);
        }

        [Fact]
        public void Single_Value_Expression_Inline()
        {
            IValList valList = sql.ValList.Add(() => 1.5f);

            QueryResult result = engine.Compile(valList);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1.5f
            }, result.Parameters);
        }

        [Fact]
        public void Double_Value_Expression_Inline()
        {
            IValList valList = sql.ValList.Add(() => 1.5);

            QueryResult result = engine.Compile(valList);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1.5
            }, result.Parameters);
        }

        [Fact]
        public void Decimal_Value_Expression_Inline()
        {
            IValList valList = sql.ValList.Add(() => 1.5m);

            QueryResult result = engine.Compile(valList);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1.5m
            }, result.Parameters);
        }

        [Fact]
        public void Char_Value_Expression_Inline()
        {
            IValList valList = sql.ValList.Add(() => 'a');

            QueryResult result = engine.Compile(valList);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 'a'
            }, result.Parameters);
        }

        [Fact]
        public void String_Value_Expression_Inline()
        {
            IValList valList = sql.ValList.Add(() => "abcd");

            QueryResult result = engine.Compile(valList);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "abcd"
            }, result.Parameters);
        }

        [Fact]
        public void DateTime_Value_Expression_Inline()
        {
            IValList valList = sql.ValList.Add(() => new DateTime(2000, 1, 1));

            QueryResult result = engine.Compile(valList);

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = new DateTime(2000, 1, 1)
            }, result.Parameters);
        }

        [Fact]
        public void Null_Value_Expression_Inline()
        {
            IValList valList = sql.ValList.Add(() => null);

            QueryResult result = engine.Compile(valList);

            Assert.Equal("NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Array_Value_Expression_Inline()
        {
            IValList valList = sql.ValList.Add(() => new int[] { 1, 2, 3 });

            QueryResult result = engine.Compile(valList);

            Assert.Equal("(@p0, @p1, @p2)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = 2,
                ["@p2"] = 3
            }, result.Parameters);
        }

        [Fact]
        public void Array_Value_Expression_Inline_With_Null()
        {
            IValList valList = sql.ValList.Add(() => new string[] { "a", null, "c" });

            QueryResult result = engine.Compile(valList);

            Assert.Equal("(@p0, NULL, @p1)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "a",
                ["@p1"] = "c"
            }, result.Parameters);
        }
    }
}