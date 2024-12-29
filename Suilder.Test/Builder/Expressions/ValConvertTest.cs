using System;
using System.Reflection;
using Xunit;

namespace Suilder.Test.Builder.Expressions
{
    public class ValConvertTest : BuilderBaseTest
    {
        [Theory]
        [InlineData(char.MinValue)]
        [InlineData((char)1)]
        [InlineData(char.MaxValue)]
        public void Convert_Value_Char(char value)
        {
            Assert.Equal(TestValue.ConvertChar(value), sql.Val(() => TestValue.ConvertChar(value)));
            Assert.Equal(TestValue.ConvertSByte((sbyte)value), sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
            Assert.Equal(TestValue.ConvertByte((byte)value), sql.Val(() => TestValue.ConvertByte((byte)value)));
            Assert.Equal(TestValue.ConvertShort((short)value), sql.Val(() => TestValue.ConvertShort((short)value)));
            Assert.Equal(TestValue.ConvertUShort(value), sql.Val(() => TestValue.ConvertUShort(value)));
            Assert.Equal(TestValue.ConvertInt(value), sql.Val(() => TestValue.ConvertInt(value)));
            Assert.Equal(TestValue.ConvertUInt(value), sql.Val(() => TestValue.ConvertUInt(value)));
            Assert.Equal(TestValue.ConvertLong(value), sql.Val(() => TestValue.ConvertLong(value)));
            Assert.Equal(TestValue.ConvertULong(value), sql.Val(() => TestValue.ConvertULong(value)));
            Assert.Equal(TestValue.ConvertFloat(value), sql.Val(() => TestValue.ConvertFloat(value)));
            Assert.Equal(TestValue.ConvertDouble(value), sql.Val(() => TestValue.ConvertDouble(value)));
        }

        [Theory]
        [InlineData(char.MinValue)]
        [InlineData((char)1)]
        public void Convert_Value_Char_Checked(char value)
        {
            checked
            {
                Assert.Equal(TestValue.ConvertChar(value), sql.Val(() => TestValue.ConvertChar(value)));
                Assert.Equal(TestValue.ConvertSByte((sbyte)value), sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
                Assert.Equal(TestValue.ConvertByte((byte)value), sql.Val(() => TestValue.ConvertByte((byte)value)));
                Assert.Equal(TestValue.ConvertShort((short)value), sql.Val(() => TestValue.ConvertShort((short)value)));
                Assert.Equal(TestValue.ConvertUShort(value), sql.Val(() => TestValue.ConvertUShort(value)));
                Assert.Equal(TestValue.ConvertInt(value), sql.Val(() => TestValue.ConvertInt(value)));
                Assert.Equal(TestValue.ConvertUInt(value), sql.Val(() => TestValue.ConvertUInt(value)));
                Assert.Equal(TestValue.ConvertLong(value), sql.Val(() => TestValue.ConvertLong(value)));
                Assert.Equal(TestValue.ConvertULong(value), sql.Val(() => TestValue.ConvertULong(value)));
                Assert.Equal(TestValue.ConvertFloat(value), sql.Val(() => TestValue.ConvertFloat(value)));
                Assert.Equal(TestValue.ConvertDouble(value), sql.Val(() => TestValue.ConvertDouble(value)));
            }
        }

        [Theory]
        [InlineData(char.MaxValue)]
        public void Convert_Value_Char_Checked_Overflow(char value)
        {
            checked
            {
                Exception ex;
                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertByte((byte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertShort((short)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                Assert.Equal(TestValue.ConvertChar(value), sql.Val(() => TestValue.ConvertChar(value)));
                Assert.Equal(TestValue.ConvertUShort(value), sql.Val(() => TestValue.ConvertUShort(value)));
                Assert.Equal(TestValue.ConvertInt(value), sql.Val(() => TestValue.ConvertInt(value)));
                Assert.Equal(TestValue.ConvertUInt(value), sql.Val(() => TestValue.ConvertUInt(value)));
                Assert.Equal(TestValue.ConvertLong(value), sql.Val(() => TestValue.ConvertLong(value)));
                Assert.Equal(TestValue.ConvertULong(value), sql.Val(() => TestValue.ConvertULong(value)));
                Assert.Equal(TestValue.ConvertFloat(value), sql.Val(() => TestValue.ConvertFloat(value)));
                Assert.Equal(TestValue.ConvertDouble(value), sql.Val(() => TestValue.ConvertDouble(value)));
            }
        }

        [Theory]
        [InlineData(char.MinValue)]
        [InlineData((char)1)]
        [InlineData(char.MaxValue)]
        public void Convert_Value_Char_ValueType(char? value)
        {
            Assert.Equal(TestValue.ConvertChar((char)value), sql.Val(() => TestValue.ConvertChar((char)value)));
            Assert.Equal(TestValue.ConvertSByte((sbyte)value), sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
            Assert.Equal(TestValue.ConvertByte((byte)value), sql.Val(() => TestValue.ConvertByte((byte)value)));
            Assert.Equal(TestValue.ConvertShort((short)value), sql.Val(() => TestValue.ConvertShort((short)value)));
            Assert.Equal(TestValue.ConvertUShort((ushort)value), sql.Val(() => TestValue.ConvertUShort((ushort)value)));
            Assert.Equal(TestValue.ConvertInt((int)value), sql.Val(() => TestValue.ConvertInt((int)value)));
            Assert.Equal(TestValue.ConvertUInt((uint)value), sql.Val(() => TestValue.ConvertUInt((uint)value)));
            Assert.Equal(TestValue.ConvertLong((long)value), sql.Val(() => TestValue.ConvertLong((long)value)));
            Assert.Equal(TestValue.ConvertULong((ulong)value), sql.Val(() => TestValue.ConvertULong((ulong)value)));
            Assert.Equal(TestValue.ConvertFloat((float)value), sql.Val(() => TestValue.ConvertFloat((float)value)));
            Assert.Equal(TestValue.ConvertDouble((double)value), sql.Val(() => TestValue.ConvertDouble((double)value)));
        }

        [Theory]
        [InlineData(char.MinValue)]
        [InlineData((char)1)]
        public void Convert_Value_Char_ValueType_Checked(char? value)
        {
            checked
            {
                Assert.Equal(TestValue.ConvertChar((char)value), sql.Val(() => TestValue.ConvertChar((char)value)));
                Assert.Equal(TestValue.ConvertSByte((sbyte)value), sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
                Assert.Equal(TestValue.ConvertByte((byte)value), sql.Val(() => TestValue.ConvertByte((byte)value)));
                Assert.Equal(TestValue.ConvertShort((short)value), sql.Val(() => TestValue.ConvertShort((short)value)));
                Assert.Equal(TestValue.ConvertUShort((ushort)value), sql.Val(() => TestValue.ConvertUShort((ushort)value)));
                Assert.Equal(TestValue.ConvertInt((int)value), sql.Val(() => TestValue.ConvertInt((int)value)));
                Assert.Equal(TestValue.ConvertUInt((uint)value), sql.Val(() => TestValue.ConvertUInt((uint)value)));
                Assert.Equal(TestValue.ConvertLong((long)value), sql.Val(() => TestValue.ConvertLong((long)value)));
                Assert.Equal(TestValue.ConvertULong((ulong)value), sql.Val(() => TestValue.ConvertULong((ulong)value)));
                Assert.Equal(TestValue.ConvertFloat((float)value), sql.Val(() => TestValue.ConvertFloat((float)value)));
                Assert.Equal(TestValue.ConvertDouble((double)value), sql.Val(() => TestValue.ConvertDouble((double)value)));
            }
        }

        [Theory]
        [InlineData(char.MaxValue)]
        public void Convert_Value_Char_ValueType_Checked_Overflow(char? value)
        {
            checked
            {
                Exception ex;
                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertByte((byte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertShort((short)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                Assert.Equal(TestValue.ConvertChar((char)value), sql.Val(() => TestValue.ConvertChar((char)value)));
                Assert.Equal(TestValue.ConvertUShort((ushort)value), sql.Val(() => TestValue.ConvertUShort((ushort)value)));
                Assert.Equal(TestValue.ConvertInt((int)value), sql.Val(() => TestValue.ConvertInt((int)value)));
                Assert.Equal(TestValue.ConvertUInt((uint)value), sql.Val(() => TestValue.ConvertUInt((uint)value)));
                Assert.Equal(TestValue.ConvertLong((long)value), sql.Val(() => TestValue.ConvertLong((long)value)));
                Assert.Equal(TestValue.ConvertULong((ulong)value), sql.Val(() => TestValue.ConvertULong((ulong)value)));
                Assert.Equal(TestValue.ConvertFloat((float)value), sql.Val(() => TestValue.ConvertFloat((float)value)));
                Assert.Equal(TestValue.ConvertDouble((double)value), sql.Val(() => TestValue.ConvertDouble((double)value)));
            }
        }

        [Theory]
        [InlineData(sbyte.MinValue)]
        [InlineData((sbyte)1)]
        [InlineData(sbyte.MaxValue)]
        public void Convert_Value_SByte(sbyte value)
        {
            Assert.Equal(TestValue.ConvertChar((char)value), sql.Val(() => TestValue.ConvertChar((char)value)));
            Assert.Equal(TestValue.ConvertSByte(value), sql.Val(() => TestValue.ConvertSByte(value)));
            Assert.Equal(TestValue.ConvertByte((byte)value), sql.Val(() => TestValue.ConvertByte((byte)value)));
            Assert.Equal(TestValue.ConvertShort(value), sql.Val(() => TestValue.ConvertShort(value)));
            Assert.Equal(TestValue.ConvertUShort((ushort)value), sql.Val(() => TestValue.ConvertUShort((ushort)value)));
            Assert.Equal(TestValue.ConvertInt(value), sql.Val(() => TestValue.ConvertInt(value)));
            Assert.Equal(TestValue.ConvertUInt((uint)value), sql.Val(() => TestValue.ConvertUInt((uint)value)));
            Assert.Equal(TestValue.ConvertLong(value), sql.Val(() => TestValue.ConvertLong(value)));
            Assert.Equal(TestValue.ConvertULong((ulong)value), sql.Val(() => TestValue.ConvertULong((ulong)value)));
            Assert.Equal(TestValue.ConvertFloat(value), sql.Val(() => TestValue.ConvertFloat(value)));
            Assert.Equal(TestValue.ConvertDouble(value), sql.Val(() => TestValue.ConvertDouble(value)));
        }

        [Theory]
        [InlineData((sbyte)1)]
        [InlineData(sbyte.MaxValue)]
        public void Convert_Value_SByte_Checked(sbyte value)
        {
            checked
            {
                Assert.Equal(TestValue.ConvertChar((char)value), sql.Val(() => TestValue.ConvertChar((char)value)));
                Assert.Equal(TestValue.ConvertSByte(value), sql.Val(() => TestValue.ConvertSByte(value)));
                Assert.Equal(TestValue.ConvertByte((byte)value), sql.Val(() => TestValue.ConvertByte((byte)value)));
                Assert.Equal(TestValue.ConvertShort(value), sql.Val(() => TestValue.ConvertShort(value)));
                Assert.Equal(TestValue.ConvertUShort((ushort)value), sql.Val(() => TestValue.ConvertUShort((ushort)value)));
                Assert.Equal(TestValue.ConvertInt(value), sql.Val(() => TestValue.ConvertInt(value)));
                Assert.Equal(TestValue.ConvertUInt((uint)value), sql.Val(() => TestValue.ConvertUInt((uint)value)));
                Assert.Equal(TestValue.ConvertLong(value), sql.Val(() => TestValue.ConvertLong(value)));
                Assert.Equal(TestValue.ConvertULong((ulong)value), sql.Val(() => TestValue.ConvertULong((ulong)value)));
                Assert.Equal(TestValue.ConvertFloat(value), sql.Val(() => TestValue.ConvertFloat(value)));
                Assert.Equal(TestValue.ConvertDouble(value), sql.Val(() => TestValue.ConvertDouble(value)));
            }
        }

        [Theory]
        [InlineData(sbyte.MinValue)]
        public void Convert_Value_SByte_Checked_Overflow(sbyte value)
        {
            checked
            {
                Exception ex;
                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertChar((char)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertByte((byte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertUShort((ushort)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertUInt((uint)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertULong((ulong)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                Assert.Equal(TestValue.ConvertSByte(value), sql.Val(() => TestValue.ConvertSByte(value)));
                Assert.Equal(TestValue.ConvertShort(value), sql.Val(() => TestValue.ConvertShort(value)));
                Assert.Equal(TestValue.ConvertInt(value), sql.Val(() => TestValue.ConvertInt(value)));
                Assert.Equal(TestValue.ConvertLong(value), sql.Val(() => TestValue.ConvertLong(value)));
                Assert.Equal(TestValue.ConvertFloat(value), sql.Val(() => TestValue.ConvertFloat(value)));
                Assert.Equal(TestValue.ConvertDouble(value), sql.Val(() => TestValue.ConvertDouble(value)));
            }
        }

        [Theory]
        [InlineData(sbyte.MinValue)]
        [InlineData((sbyte)1)]
        [InlineData(sbyte.MaxValue)]
        public void Convert_Value_SByte_ValueType(sbyte? value)
        {
            Assert.Equal(TestValue.ConvertChar((char)value), sql.Val(() => TestValue.ConvertChar((char)value)));
            Assert.Equal(TestValue.ConvertSByte((sbyte)value), sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
            Assert.Equal(TestValue.ConvertByte((byte)value), sql.Val(() => TestValue.ConvertByte((byte)value)));
            Assert.Equal(TestValue.ConvertShort((short)value), sql.Val(() => TestValue.ConvertShort((short)value)));
            Assert.Equal(TestValue.ConvertUShort((ushort)value), sql.Val(() => TestValue.ConvertUShort((ushort)value)));
            Assert.Equal(TestValue.ConvertInt((int)value), sql.Val(() => TestValue.ConvertInt((int)value)));
            Assert.Equal(TestValue.ConvertUInt((uint)value), sql.Val(() => TestValue.ConvertUInt((uint)value)));
            Assert.Equal(TestValue.ConvertLong((long)value), sql.Val(() => TestValue.ConvertLong((long)value)));
            Assert.Equal(TestValue.ConvertULong((ulong)value), sql.Val(() => TestValue.ConvertULong((ulong)value)));
            Assert.Equal(TestValue.ConvertFloat((float)value), sql.Val(() => TestValue.ConvertFloat((float)value)));
            Assert.Equal(TestValue.ConvertDouble((double)value), sql.Val(() => TestValue.ConvertDouble((double)value)));
        }

        [Theory]
        [InlineData((sbyte)1)]
        [InlineData(sbyte.MaxValue)]
        public void Convert_Value_SByte_ValueType_Checked(sbyte? value)
        {
            checked
            {
                Assert.Equal(TestValue.ConvertChar((char)value), sql.Val(() => TestValue.ConvertChar((char)value)));
                Assert.Equal(TestValue.ConvertSByte((sbyte)value), sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
                Assert.Equal(TestValue.ConvertByte((byte)value), sql.Val(() => TestValue.ConvertByte((byte)value)));
                Assert.Equal(TestValue.ConvertShort((short)value), sql.Val(() => TestValue.ConvertShort((short)value)));
                Assert.Equal(TestValue.ConvertUShort((ushort)value), sql.Val(() => TestValue.ConvertUShort((ushort)value)));
                Assert.Equal(TestValue.ConvertInt((int)value), sql.Val(() => TestValue.ConvertInt((int)value)));
                Assert.Equal(TestValue.ConvertUInt((uint)value), sql.Val(() => TestValue.ConvertUInt((uint)value)));
                Assert.Equal(TestValue.ConvertLong((long)value), sql.Val(() => TestValue.ConvertLong((long)value)));
                Assert.Equal(TestValue.ConvertULong((ulong)value), sql.Val(() => TestValue.ConvertULong((ulong)value)));
                Assert.Equal(TestValue.ConvertFloat((float)value), sql.Val(() => TestValue.ConvertFloat((float)value)));
                Assert.Equal(TestValue.ConvertDouble((double)value), sql.Val(() => TestValue.ConvertDouble((double)value)));
            }
        }

        [Theory]
        [InlineData(sbyte.MinValue)]
        public void Convert_Value_SByte_ValueType_Checked_Overflow(sbyte? value)
        {
            checked
            {
                Exception ex;
                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertChar((char)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertByte((byte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertUShort((ushort)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertUInt((uint)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertULong((ulong)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                Assert.Equal(TestValue.ConvertSByte((sbyte)value), sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
                Assert.Equal(TestValue.ConvertShort((short)value), sql.Val(() => TestValue.ConvertShort((short)value)));
                Assert.Equal(TestValue.ConvertInt((int)value), sql.Val(() => TestValue.ConvertInt((int)value)));
                Assert.Equal(TestValue.ConvertLong((long)value), sql.Val(() => TestValue.ConvertLong((long)value)));
                Assert.Equal(TestValue.ConvertFloat((float)value), sql.Val(() => TestValue.ConvertFloat((float)value)));
                Assert.Equal(TestValue.ConvertDouble((double)value), sql.Val(() => TestValue.ConvertDouble((double)value)));
            }
        }

        [Theory]
        [InlineData(byte.MinValue)]
        [InlineData((byte)1)]
        [InlineData(byte.MaxValue)]
        public void Convert_Value_Byte(byte value)
        {
            Assert.Equal(TestValue.ConvertChar((char)value), sql.Val(() => TestValue.ConvertChar((char)value)));
            Assert.Equal(TestValue.ConvertSByte((sbyte)value), sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
            Assert.Equal(TestValue.ConvertByte(value), sql.Val(() => TestValue.ConvertByte(value)));
            Assert.Equal(TestValue.ConvertShort(value), sql.Val(() => TestValue.ConvertShort(value)));
            Assert.Equal(TestValue.ConvertUShort(value), sql.Val(() => TestValue.ConvertUShort(value)));
            Assert.Equal(TestValue.ConvertInt(value), sql.Val(() => TestValue.ConvertInt(value)));
            Assert.Equal(TestValue.ConvertUInt(value), sql.Val(() => TestValue.ConvertUInt(value)));
            Assert.Equal(TestValue.ConvertLong(value), sql.Val(() => TestValue.ConvertLong(value)));
            Assert.Equal(TestValue.ConvertULong(value), sql.Val(() => TestValue.ConvertULong(value)));
            Assert.Equal(TestValue.ConvertFloat(value), sql.Val(() => TestValue.ConvertFloat(value)));
            Assert.Equal(TestValue.ConvertDouble(value), sql.Val(() => TestValue.ConvertDouble(value)));
        }

        [Theory]
        [InlineData(byte.MinValue)]
        [InlineData((byte)1)]
        public void Convert_Value_Byte_Checked(byte value)
        {
            checked
            {
                Assert.Equal(TestValue.ConvertChar((char)value), sql.Val(() => TestValue.ConvertChar((char)value)));
                Assert.Equal(TestValue.ConvertSByte((sbyte)value), sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
                Assert.Equal(TestValue.ConvertByte(value), sql.Val(() => TestValue.ConvertByte(value)));
                Assert.Equal(TestValue.ConvertShort(value), sql.Val(() => TestValue.ConvertShort(value)));
                Assert.Equal(TestValue.ConvertUShort(value), sql.Val(() => TestValue.ConvertUShort(value)));
                Assert.Equal(TestValue.ConvertInt(value), sql.Val(() => TestValue.ConvertInt(value)));
                Assert.Equal(TestValue.ConvertUInt(value), sql.Val(() => TestValue.ConvertUInt(value)));
                Assert.Equal(TestValue.ConvertLong(value), sql.Val(() => TestValue.ConvertLong(value)));
                Assert.Equal(TestValue.ConvertULong(value), sql.Val(() => TestValue.ConvertULong(value)));
                Assert.Equal(TestValue.ConvertFloat(value), sql.Val(() => TestValue.ConvertFloat(value)));
                Assert.Equal(TestValue.ConvertDouble(value), sql.Val(() => TestValue.ConvertDouble(value)));
            }
        }

        [Theory]
        [InlineData(byte.MaxValue)]
        public void Convert_Value_Byte_Checked_Overflow(byte value)
        {
            checked
            {
                Exception ex;
                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                Assert.Equal(TestValue.ConvertChar((char)value), sql.Val(() => TestValue.ConvertChar((char)value)));
                Assert.Equal(TestValue.ConvertByte(value), sql.Val(() => TestValue.ConvertByte(value)));
                Assert.Equal(TestValue.ConvertShort(value), sql.Val(() => TestValue.ConvertShort(value)));
                Assert.Equal(TestValue.ConvertUShort(value), sql.Val(() => TestValue.ConvertUShort(value)));
                Assert.Equal(TestValue.ConvertInt(value), sql.Val(() => TestValue.ConvertInt(value)));
                Assert.Equal(TestValue.ConvertUInt(value), sql.Val(() => TestValue.ConvertUInt(value)));
                Assert.Equal(TestValue.ConvertLong(value), sql.Val(() => TestValue.ConvertLong(value)));
                Assert.Equal(TestValue.ConvertULong(value), sql.Val(() => TestValue.ConvertULong(value)));
                Assert.Equal(TestValue.ConvertFloat(value), sql.Val(() => TestValue.ConvertFloat(value)));
                Assert.Equal(TestValue.ConvertDouble(value), sql.Val(() => TestValue.ConvertDouble(value)));
            }
        }

        [Theory]
        [InlineData(byte.MinValue)]
        [InlineData((byte)1)]
        [InlineData(byte.MaxValue)]
        public void Convert_Value_Byte_ValueType(byte? value)
        {
            Assert.Equal(TestValue.ConvertChar((char)value), sql.Val(() => TestValue.ConvertChar((char)value)));
            Assert.Equal(TestValue.ConvertSByte((sbyte)value), sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
            Assert.Equal(TestValue.ConvertByte((byte)value), sql.Val(() => TestValue.ConvertByte((byte)value)));
            Assert.Equal(TestValue.ConvertShort((short)value), sql.Val(() => TestValue.ConvertShort((short)value)));
            Assert.Equal(TestValue.ConvertUShort((ushort)value), sql.Val(() => TestValue.ConvertUShort((ushort)value)));
            Assert.Equal(TestValue.ConvertInt((int)value), sql.Val(() => TestValue.ConvertInt((int)value)));
            Assert.Equal(TestValue.ConvertUInt((uint)value), sql.Val(() => TestValue.ConvertUInt((uint)value)));
            Assert.Equal(TestValue.ConvertLong((long)value), sql.Val(() => TestValue.ConvertLong((long)value)));
            Assert.Equal(TestValue.ConvertULong((ulong)value), sql.Val(() => TestValue.ConvertULong((ulong)value)));
            Assert.Equal(TestValue.ConvertFloat((float)value), sql.Val(() => TestValue.ConvertFloat((float)value)));
            Assert.Equal(TestValue.ConvertDouble((double)value), sql.Val(() => TestValue.ConvertDouble((double)value)));
        }

        [Theory]
        [InlineData(byte.MinValue)]
        [InlineData((byte)1)]
        public void Convert_Value_Byte_ValueType_Checked(byte? value)
        {
            checked
            {
                Assert.Equal(TestValue.ConvertChar((char)value), sql.Val(() => TestValue.ConvertChar((char)value)));
                Assert.Equal(TestValue.ConvertSByte((sbyte)value), sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
                Assert.Equal(TestValue.ConvertByte((byte)value), sql.Val(() => TestValue.ConvertByte((byte)value)));
                Assert.Equal(TestValue.ConvertShort((short)value), sql.Val(() => TestValue.ConvertShort((short)value)));
                Assert.Equal(TestValue.ConvertUShort((ushort)value), sql.Val(() => TestValue.ConvertUShort((ushort)value)));
                Assert.Equal(TestValue.ConvertInt((int)value), sql.Val(() => TestValue.ConvertInt((int)value)));
                Assert.Equal(TestValue.ConvertUInt((uint)value), sql.Val(() => TestValue.ConvertUInt((uint)value)));
                Assert.Equal(TestValue.ConvertLong((long)value), sql.Val(() => TestValue.ConvertLong((long)value)));
                Assert.Equal(TestValue.ConvertULong((ulong)value), sql.Val(() => TestValue.ConvertULong((ulong)value)));
                Assert.Equal(TestValue.ConvertFloat((float)value), sql.Val(() => TestValue.ConvertFloat((float)value)));
                Assert.Equal(TestValue.ConvertDouble((double)value), sql.Val(() => TestValue.ConvertDouble((double)value)));
            }
        }

        [Theory]
        [InlineData(byte.MaxValue)]
        public void Convert_Value_Byte_ValueType_Checked_Overflow(byte? value)
        {
            checked
            {
                Exception ex;
                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                Assert.Equal(TestValue.ConvertChar((char)value), sql.Val(() => TestValue.ConvertChar((char)value)));
                Assert.Equal(TestValue.ConvertByte((byte)value), sql.Val(() => TestValue.ConvertByte((byte)value)));
                Assert.Equal(TestValue.ConvertUShort((ushort)value), sql.Val(() => TestValue.ConvertUShort((ushort)value)));
                Assert.Equal(TestValue.ConvertShort((short)value), sql.Val(() => TestValue.ConvertShort((short)value)));
                Assert.Equal(TestValue.ConvertInt((int)value), sql.Val(() => TestValue.ConvertInt((int)value)));
                Assert.Equal(TestValue.ConvertUInt((uint)value), sql.Val(() => TestValue.ConvertUInt((uint)value)));
                Assert.Equal(TestValue.ConvertLong((long)value), sql.Val(() => TestValue.ConvertLong((long)value)));
                Assert.Equal(TestValue.ConvertULong((ulong)value), sql.Val(() => TestValue.ConvertULong((ulong)value)));
                Assert.Equal(TestValue.ConvertFloat((float)value), sql.Val(() => TestValue.ConvertFloat((float)value)));
                Assert.Equal(TestValue.ConvertDouble((double)value), sql.Val(() => TestValue.ConvertDouble((double)value)));
            }
        }

        [Theory]
        [InlineData(short.MinValue)]
        [InlineData((short)1)]
        [InlineData(short.MaxValue)]
        public void Convert_Value_Short(short value)
        {
            Assert.Equal(TestValue.ConvertChar((char)value), sql.Val(() => TestValue.ConvertChar((char)value)));
            Assert.Equal(TestValue.ConvertSByte((sbyte)value), sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
            Assert.Equal(TestValue.ConvertByte((byte)value), sql.Val(() => TestValue.ConvertByte((byte)value)));
            Assert.Equal(TestValue.ConvertShort(value), sql.Val(() => TestValue.ConvertShort(value)));
            Assert.Equal(TestValue.ConvertUShort((ushort)value), sql.Val(() => TestValue.ConvertUShort((ushort)value)));
            Assert.Equal(TestValue.ConvertInt(value), sql.Val(() => TestValue.ConvertInt(value)));
            Assert.Equal(TestValue.ConvertUInt((uint)value), sql.Val(() => TestValue.ConvertUInt((uint)value)));
            Assert.Equal(TestValue.ConvertLong(value), sql.Val(() => TestValue.ConvertLong(value)));
            Assert.Equal(TestValue.ConvertULong((ulong)value), sql.Val(() => TestValue.ConvertULong((ulong)value)));
            Assert.Equal(TestValue.ConvertFloat(value), sql.Val(() => TestValue.ConvertFloat(value)));
            Assert.Equal(TestValue.ConvertDouble(value), sql.Val(() => TestValue.ConvertDouble(value)));
        }

        [Theory]
        [InlineData((short)1)]
        public void Convert_Value_Short_Checked(short value)
        {
            checked
            {
                Assert.Equal(TestValue.ConvertChar((char)value), sql.Val(() => TestValue.ConvertChar((char)value)));
                Assert.Equal(TestValue.ConvertSByte((sbyte)value), sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
                Assert.Equal(TestValue.ConvertByte((byte)value), sql.Val(() => TestValue.ConvertByte((byte)value)));
                Assert.Equal(TestValue.ConvertShort(value), sql.Val(() => TestValue.ConvertShort(value)));
                Assert.Equal(TestValue.ConvertUShort((ushort)value), sql.Val(() => TestValue.ConvertUShort((ushort)value)));
                Assert.Equal(TestValue.ConvertInt(value), sql.Val(() => TestValue.ConvertInt(value)));
                Assert.Equal(TestValue.ConvertUInt((uint)value), sql.Val(() => TestValue.ConvertUInt((uint)value)));
                Assert.Equal(TestValue.ConvertLong(value), sql.Val(() => TestValue.ConvertLong(value)));
                Assert.Equal(TestValue.ConvertULong((ulong)value), sql.Val(() => TestValue.ConvertULong((ulong)value)));
                Assert.Equal(TestValue.ConvertFloat(value), sql.Val(() => TestValue.ConvertFloat(value)));
                Assert.Equal(TestValue.ConvertDouble(value), sql.Val(() => TestValue.ConvertDouble(value)));
            }
        }

        [Theory]
        [InlineData(short.MinValue)]
        public void Convert_Value_Short_Checked_Overflow_Min(short value)
        {
            checked
            {
                Exception ex;
                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertChar((char)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertByte((byte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertUShort((ushort)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertUInt((uint)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertULong((ulong)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                Assert.Equal(TestValue.ConvertShort(value), sql.Val(() => TestValue.ConvertShort(value)));
                Assert.Equal(TestValue.ConvertInt(value), sql.Val(() => TestValue.ConvertInt(value)));
                Assert.Equal(TestValue.ConvertLong(value), sql.Val(() => TestValue.ConvertLong(value)));
                Assert.Equal(TestValue.ConvertFloat(value), sql.Val(() => TestValue.ConvertFloat(value)));
                Assert.Equal(TestValue.ConvertDouble(value), sql.Val(() => TestValue.ConvertDouble(value)));
            }
        }

        [Theory]
        [InlineData(short.MaxValue)]
        public void Convert_Value_Short_Checked_Overflow_Max(short value)
        {
            checked
            {
                Exception ex;
                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertByte((byte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                Assert.Equal(TestValue.ConvertChar((char)value), sql.Val(() => TestValue.ConvertChar((char)value)));
                Assert.Equal(TestValue.ConvertShort(value), sql.Val(() => TestValue.ConvertShort(value)));
                Assert.Equal(TestValue.ConvertUShort((ushort)value), sql.Val(() => TestValue.ConvertUShort((ushort)value)));
                Assert.Equal(TestValue.ConvertInt(value), sql.Val(() => TestValue.ConvertInt(value)));
                Assert.Equal(TestValue.ConvertUInt((uint)value), sql.Val(() => TestValue.ConvertUInt((uint)value)));
                Assert.Equal(TestValue.ConvertLong(value), sql.Val(() => TestValue.ConvertLong(value)));
                Assert.Equal(TestValue.ConvertULong((ulong)value), sql.Val(() => TestValue.ConvertULong((ulong)value)));
                Assert.Equal(TestValue.ConvertFloat(value), sql.Val(() => TestValue.ConvertFloat(value)));
                Assert.Equal(TestValue.ConvertDouble(value), sql.Val(() => TestValue.ConvertDouble(value)));
            }
        }

        [Theory]
        [InlineData(short.MinValue)]
        [InlineData((short)1)]
        [InlineData(short.MaxValue)]
        public void Convert_Value_Short_ValueType(short? value)
        {
            Assert.Equal(TestValue.ConvertChar((char)value), sql.Val(() => TestValue.ConvertChar((char)value)));
            Assert.Equal(TestValue.ConvertSByte((sbyte)value), sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
            Assert.Equal(TestValue.ConvertByte((byte)value), sql.Val(() => TestValue.ConvertByte((byte)value)));
            Assert.Equal(TestValue.ConvertShort((short)value), sql.Val(() => TestValue.ConvertShort((short)value)));
            Assert.Equal(TestValue.ConvertUShort((ushort)value), sql.Val(() => TestValue.ConvertUShort((ushort)value)));
            Assert.Equal(TestValue.ConvertInt((int)value), sql.Val(() => TestValue.ConvertInt((int)value)));
            Assert.Equal(TestValue.ConvertUInt((uint)value), sql.Val(() => TestValue.ConvertUInt((uint)value)));
            Assert.Equal(TestValue.ConvertLong((long)value), sql.Val(() => TestValue.ConvertLong((long)value)));
            Assert.Equal(TestValue.ConvertULong((ulong)value), sql.Val(() => TestValue.ConvertULong((ulong)value)));
            Assert.Equal(TestValue.ConvertFloat((float)value), sql.Val(() => TestValue.ConvertFloat((float)value)));
            Assert.Equal(TestValue.ConvertDouble((double)value), sql.Val(() => TestValue.ConvertDouble((double)value)));
        }

        [Theory]
        [InlineData((short)1)]
        public void Convert_Value_Short_ValueType_Checked(short? value)
        {
            checked
            {
                Assert.Equal(TestValue.ConvertChar((char)value), sql.Val(() => TestValue.ConvertChar((char)value)));
                Assert.Equal(TestValue.ConvertSByte((sbyte)value), sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
                Assert.Equal(TestValue.ConvertByte((byte)value), sql.Val(() => TestValue.ConvertByte((byte)value)));
                Assert.Equal(TestValue.ConvertShort((short)value), sql.Val(() => TestValue.ConvertShort((short)value)));
                Assert.Equal(TestValue.ConvertUShort((ushort)value), sql.Val(() => TestValue.ConvertUShort((ushort)value)));
                Assert.Equal(TestValue.ConvertInt((int)value), sql.Val(() => TestValue.ConvertInt((int)value)));
                Assert.Equal(TestValue.ConvertUInt((uint)value), sql.Val(() => TestValue.ConvertUInt((uint)value)));
                Assert.Equal(TestValue.ConvertLong((long)value), sql.Val(() => TestValue.ConvertLong((long)value)));
                Assert.Equal(TestValue.ConvertULong((ulong)value), sql.Val(() => TestValue.ConvertULong((ulong)value)));
                Assert.Equal(TestValue.ConvertFloat((float)value), sql.Val(() => TestValue.ConvertFloat((float)value)));
                Assert.Equal(TestValue.ConvertDouble((double)value), sql.Val(() => TestValue.ConvertDouble((double)value)));
            }
        }

        [Theory]
        [InlineData(short.MinValue)]
        public void Convert_Value_Short_ValueType_Checked_Overflow_Min(short? value)
        {
            checked
            {
                Exception ex;
                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertChar((char)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertByte((byte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertUShort((ushort)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertUInt((uint)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertULong((ulong)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                Assert.Equal(TestValue.ConvertShort((short)value), sql.Val(() => TestValue.ConvertShort((short)value)));
                Assert.Equal(TestValue.ConvertInt((int)value), sql.Val(() => TestValue.ConvertInt((int)value)));
                Assert.Equal(TestValue.ConvertLong((long)value), sql.Val(() => TestValue.ConvertLong((long)value)));
                Assert.Equal(TestValue.ConvertFloat((float)value), sql.Val(() => TestValue.ConvertFloat((float)value)));
                Assert.Equal(TestValue.ConvertDouble((double)value), sql.Val(() => TestValue.ConvertDouble((double)value)));
            }
        }

        [Theory]
        [InlineData(short.MaxValue)]
        public void Convert_Value_Short_ValueType_Checked_Overflow_Max(short? value)
        {
            checked
            {
                Exception ex;
                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertByte((byte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                Assert.Equal(TestValue.ConvertChar((char)value), sql.Val(() => TestValue.ConvertChar((char)value)));
                Assert.Equal(TestValue.ConvertShort((short)value), sql.Val(() => TestValue.ConvertShort((short)value)));
                Assert.Equal(TestValue.ConvertUShort((ushort)value), sql.Val(() => TestValue.ConvertUShort((ushort)value)));
                Assert.Equal(TestValue.ConvertInt((int)value), sql.Val(() => TestValue.ConvertInt((int)value)));
                Assert.Equal(TestValue.ConvertUInt((uint)value), sql.Val(() => TestValue.ConvertUInt((uint)value)));
                Assert.Equal(TestValue.ConvertLong((long)value), sql.Val(() => TestValue.ConvertLong((long)value)));
                Assert.Equal(TestValue.ConvertULong((ulong)value), sql.Val(() => TestValue.ConvertULong((ulong)value)));
                Assert.Equal(TestValue.ConvertFloat((float)value), sql.Val(() => TestValue.ConvertFloat((float)value)));
                Assert.Equal(TestValue.ConvertDouble((double)value), sql.Val(() => TestValue.ConvertDouble((double)value)));
            }
        }

        [Theory]
        [InlineData(ushort.MinValue)]
        [InlineData((ushort)1)]
        [InlineData(ushort.MaxValue)]
        public void Convert_Value_UShort(ushort value)
        {
            Assert.Equal(TestValue.ConvertChar((char)value), sql.Val(() => TestValue.ConvertChar((char)value)));
            Assert.Equal(TestValue.ConvertSByte((sbyte)value), sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
            Assert.Equal(TestValue.ConvertByte((byte)value), sql.Val(() => TestValue.ConvertByte((byte)value)));
            Assert.Equal(TestValue.ConvertShort((short)value), sql.Val(() => TestValue.ConvertShort((short)value)));
            Assert.Equal(TestValue.ConvertUShort(value), sql.Val(() => TestValue.ConvertUShort(value)));
            Assert.Equal(TestValue.ConvertInt(value), sql.Val(() => TestValue.ConvertInt(value)));
            Assert.Equal(TestValue.ConvertUInt(value), sql.Val(() => TestValue.ConvertUInt(value)));
            Assert.Equal(TestValue.ConvertLong(value), sql.Val(() => TestValue.ConvertLong(value)));
            Assert.Equal(TestValue.ConvertULong(value), sql.Val(() => TestValue.ConvertULong(value)));
            Assert.Equal(TestValue.ConvertFloat(value), sql.Val(() => TestValue.ConvertFloat(value)));
            Assert.Equal(TestValue.ConvertDouble(value), sql.Val(() => TestValue.ConvertDouble(value)));
        }

        [Theory]
        [InlineData(ushort.MinValue)]
        [InlineData((ushort)1)]
        public void Convert_Value_UShort_Checked(ushort value)
        {
            checked
            {
                Assert.Equal(TestValue.ConvertChar((char)value), sql.Val(() => TestValue.ConvertChar((char)value)));
                Assert.Equal(TestValue.ConvertSByte((sbyte)value), sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
                Assert.Equal(TestValue.ConvertByte((byte)value), sql.Val(() => TestValue.ConvertByte((byte)value)));
                Assert.Equal(TestValue.ConvertShort((short)value), sql.Val(() => TestValue.ConvertShort((short)value)));
                Assert.Equal(TestValue.ConvertUShort(value), sql.Val(() => TestValue.ConvertUShort(value)));
                Assert.Equal(TestValue.ConvertInt(value), sql.Val(() => TestValue.ConvertInt(value)));
                Assert.Equal(TestValue.ConvertUInt(value), sql.Val(() => TestValue.ConvertUInt(value)));
                Assert.Equal(TestValue.ConvertLong(value), sql.Val(() => TestValue.ConvertLong(value)));
                Assert.Equal(TestValue.ConvertULong(value), sql.Val(() => TestValue.ConvertULong(value)));
                Assert.Equal(TestValue.ConvertFloat(value), sql.Val(() => TestValue.ConvertFloat(value)));
                Assert.Equal(TestValue.ConvertDouble(value), sql.Val(() => TestValue.ConvertDouble(value)));
            }
        }

        [Theory]
        [InlineData(ushort.MaxValue)]
        public void Convert_Value_UShort_Checked_Overflow(ushort value)
        {
            checked
            {
                Exception ex;
                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertByte((byte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertShort((short)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                Assert.Equal(TestValue.ConvertChar((char)value), sql.Val(() => TestValue.ConvertChar((char)value)));
                Assert.Equal(TestValue.ConvertUShort(value), sql.Val(() => TestValue.ConvertUShort(value)));
                Assert.Equal(TestValue.ConvertInt(value), sql.Val(() => TestValue.ConvertInt(value)));
                Assert.Equal(TestValue.ConvertUInt(value), sql.Val(() => TestValue.ConvertUInt(value)));
                Assert.Equal(TestValue.ConvertLong(value), sql.Val(() => TestValue.ConvertLong(value)));
                Assert.Equal(TestValue.ConvertULong(value), sql.Val(() => TestValue.ConvertULong(value)));
                Assert.Equal(TestValue.ConvertFloat(value), sql.Val(() => TestValue.ConvertFloat(value)));
                Assert.Equal(TestValue.ConvertDouble(value), sql.Val(() => TestValue.ConvertDouble(value)));
            }
        }

        [Theory]
        [InlineData(ushort.MinValue)]
        [InlineData((ushort)1)]
        [InlineData(ushort.MaxValue)]
        public void Convert_Value_UShort_ValueType(ushort? value)
        {
            Assert.Equal(TestValue.ConvertChar((char)value), sql.Val(() => TestValue.ConvertChar((char)value)));
            Assert.Equal(TestValue.ConvertSByte((sbyte)value), sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
            Assert.Equal(TestValue.ConvertByte((byte)value), sql.Val(() => TestValue.ConvertByte((byte)value)));
            Assert.Equal(TestValue.ConvertShort((short)value), sql.Val(() => TestValue.ConvertShort((short)value)));
            Assert.Equal(TestValue.ConvertUShort((ushort)value), sql.Val(() => TestValue.ConvertUShort((ushort)value)));
            Assert.Equal(TestValue.ConvertInt((int)value), sql.Val(() => TestValue.ConvertInt((int)value)));
            Assert.Equal(TestValue.ConvertUInt((uint)value), sql.Val(() => TestValue.ConvertUInt((uint)value)));
            Assert.Equal(TestValue.ConvertLong((long)value), sql.Val(() => TestValue.ConvertLong((long)value)));
            Assert.Equal(TestValue.ConvertULong((ulong)value), sql.Val(() => TestValue.ConvertULong((ulong)value)));
            Assert.Equal(TestValue.ConvertFloat((float)value), sql.Val(() => TestValue.ConvertFloat((float)value)));
            Assert.Equal(TestValue.ConvertDouble((double)value), sql.Val(() => TestValue.ConvertDouble((double)value)));
        }

        [Theory]
        [InlineData(ushort.MinValue)]
        [InlineData((ushort)1)]
        public void Convert_Value_UShort_ValueType_Checked(ushort? value)
        {
            checked
            {
                Assert.Equal(TestValue.ConvertChar((char)value), sql.Val(() => TestValue.ConvertChar((char)value)));
                Assert.Equal(TestValue.ConvertSByte((sbyte)value), sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
                Assert.Equal(TestValue.ConvertByte((byte)value), sql.Val(() => TestValue.ConvertByte((byte)value)));
                Assert.Equal(TestValue.ConvertShort((short)value), sql.Val(() => TestValue.ConvertShort((short)value)));
                Assert.Equal(TestValue.ConvertUShort((ushort)value), sql.Val(() => TestValue.ConvertUShort((ushort)value)));
                Assert.Equal(TestValue.ConvertInt((int)value), sql.Val(() => TestValue.ConvertInt((int)value)));
                Assert.Equal(TestValue.ConvertUInt((uint)value), sql.Val(() => TestValue.ConvertUInt((uint)value)));
                Assert.Equal(TestValue.ConvertLong((long)value), sql.Val(() => TestValue.ConvertLong((long)value)));
                Assert.Equal(TestValue.ConvertULong((ulong)value), sql.Val(() => TestValue.ConvertULong((ulong)value)));
                Assert.Equal(TestValue.ConvertFloat((float)value), sql.Val(() => TestValue.ConvertFloat((float)value)));
                Assert.Equal(TestValue.ConvertDouble((double)value), sql.Val(() => TestValue.ConvertDouble((double)value)));
            }
        }

        [Theory]
        [InlineData(ushort.MaxValue)]
        public void Convert_Value_UShort_ValueType_Checked_Overflow(ushort? value)
        {
            checked
            {
                Exception ex;
                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertByte((byte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertShort((short)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                Assert.Equal(TestValue.ConvertChar((char)value), sql.Val(() => TestValue.ConvertChar((char)value)));
                Assert.Equal(TestValue.ConvertUShort((ushort)value), sql.Val(() => TestValue.ConvertUShort((ushort)value)));
                Assert.Equal(TestValue.ConvertInt((int)value), sql.Val(() => TestValue.ConvertInt((int)value)));
                Assert.Equal(TestValue.ConvertUInt((uint)value), sql.Val(() => TestValue.ConvertUInt((uint)value)));
                Assert.Equal(TestValue.ConvertLong((long)value), sql.Val(() => TestValue.ConvertLong((long)value)));
                Assert.Equal(TestValue.ConvertULong((ulong)value), sql.Val(() => TestValue.ConvertULong((ulong)value)));
                Assert.Equal(TestValue.ConvertFloat((float)value), sql.Val(() => TestValue.ConvertFloat((float)value)));
                Assert.Equal(TestValue.ConvertDouble((double)value), sql.Val(() => TestValue.ConvertDouble((double)value)));
            }
        }

        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(1)]
        [InlineData(int.MaxValue)]
        public void Convert_Value_Int(int value)
        {
            Assert.Equal(TestValue.ConvertChar((char)value), sql.Val(() => TestValue.ConvertChar((char)value)));
            Assert.Equal(TestValue.ConvertSByte((sbyte)value), sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
            Assert.Equal(TestValue.ConvertByte((byte)value), sql.Val(() => TestValue.ConvertByte((byte)value)));
            Assert.Equal(TestValue.ConvertShort((short)value), sql.Val(() => TestValue.ConvertShort((short)value)));
            Assert.Equal(TestValue.ConvertUShort((ushort)value), sql.Val(() => TestValue.ConvertUShort((ushort)value)));
            Assert.Equal(TestValue.ConvertInt(value), sql.Val(() => TestValue.ConvertInt(value)));
            Assert.Equal(TestValue.ConvertUInt((uint)value), sql.Val(() => TestValue.ConvertUInt((uint)value)));
            Assert.Equal(TestValue.ConvertLong(value), sql.Val(() => TestValue.ConvertLong(value)));
            Assert.Equal(TestValue.ConvertULong((ulong)value), sql.Val(() => TestValue.ConvertULong((ulong)value)));
            Assert.Equal(TestValue.ConvertFloat(value), sql.Val(() => TestValue.ConvertFloat(value)));
            Assert.Equal(TestValue.ConvertDouble(value), sql.Val(() => TestValue.ConvertDouble(value)));
        }

        [Theory]
        [InlineData(1)]
        public void Convert_Value_Int_Checked(int value)
        {
            checked
            {
                Assert.Equal(TestValue.ConvertChar((char)value), sql.Val(() => TestValue.ConvertChar((char)value)));
                Assert.Equal(TestValue.ConvertSByte((sbyte)value), sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
                Assert.Equal(TestValue.ConvertByte((byte)value), sql.Val(() => TestValue.ConvertByte((byte)value)));
                Assert.Equal(TestValue.ConvertShort((short)value), sql.Val(() => TestValue.ConvertShort((short)value)));
                Assert.Equal(TestValue.ConvertUShort((ushort)value), sql.Val(() => TestValue.ConvertUShort((ushort)value)));
                Assert.Equal(TestValue.ConvertInt(value), sql.Val(() => TestValue.ConvertInt(value)));
                Assert.Equal(TestValue.ConvertUInt((uint)value), sql.Val(() => TestValue.ConvertUInt((uint)value)));
                Assert.Equal(TestValue.ConvertLong(value), sql.Val(() => TestValue.ConvertLong(value)));
                Assert.Equal(TestValue.ConvertULong((ulong)value), sql.Val(() => TestValue.ConvertULong((ulong)value)));
                Assert.Equal(TestValue.ConvertFloat(value), sql.Val(() => TestValue.ConvertFloat(value)));
                Assert.Equal(TestValue.ConvertDouble(value), sql.Val(() => TestValue.ConvertDouble(value)));
            }
        }

        [Theory]
        [InlineData(int.MinValue)]
        public void Convert_Value_Int_Checked_Overflow_Min(int value)
        {
            checked
            {
                Exception ex;
                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertChar((char)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertByte((byte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertShort((short)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertUShort((ushort)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertUInt((uint)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertULong((ulong)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                Assert.Equal(TestValue.ConvertInt(value), sql.Val(() => TestValue.ConvertInt(value)));
                Assert.Equal(TestValue.ConvertLong(value), sql.Val(() => TestValue.ConvertLong(value)));
                Assert.Equal(TestValue.ConvertFloat(value), sql.Val(() => TestValue.ConvertFloat(value)));
                Assert.Equal(TestValue.ConvertDouble(value), sql.Val(() => TestValue.ConvertDouble(value)));
            }
        }

        [Theory]
        [InlineData(int.MaxValue)]
        public void Convert_Value_Int_Checked_Overflow_Max(int value)
        {
            checked
            {
                Exception ex;
                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertChar((char)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertByte((byte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertShort((short)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertUShort((ushort)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                Assert.Equal(TestValue.ConvertInt(value), sql.Val(() => TestValue.ConvertInt(value)));
                Assert.Equal(TestValue.ConvertUInt((uint)value), sql.Val(() => TestValue.ConvertUInt((uint)value)));
                Assert.Equal(TestValue.ConvertLong(value), sql.Val(() => TestValue.ConvertLong(value)));
                Assert.Equal(TestValue.ConvertULong((ulong)value), sql.Val(() => TestValue.ConvertULong((ulong)value)));
                Assert.Equal(TestValue.ConvertFloat(value), sql.Val(() => TestValue.ConvertFloat(value)));
                Assert.Equal(TestValue.ConvertDouble(value), sql.Val(() => TestValue.ConvertDouble(value)));
            }
        }

        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(1)]
        [InlineData(int.MaxValue)]
        public void Convert_Value_Int_ValueType(int? value)
        {
            Assert.Equal(TestValue.ConvertChar((char)value), sql.Val(() => TestValue.ConvertChar((char)value)));
            Assert.Equal(TestValue.ConvertSByte((sbyte)value), sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
            Assert.Equal(TestValue.ConvertByte((byte)value), sql.Val(() => TestValue.ConvertByte((byte)value)));
            Assert.Equal(TestValue.ConvertShort((short)value), sql.Val(() => TestValue.ConvertShort((short)value)));
            Assert.Equal(TestValue.ConvertUShort((ushort)value), sql.Val(() => TestValue.ConvertUShort((ushort)value)));
            Assert.Equal(TestValue.ConvertInt((int)value), sql.Val(() => TestValue.ConvertInt((int)value)));
            Assert.Equal(TestValue.ConvertUInt((uint)value), sql.Val(() => TestValue.ConvertUInt((uint)value)));
            Assert.Equal(TestValue.ConvertLong((long)value), sql.Val(() => TestValue.ConvertLong((long)value)));
            Assert.Equal(TestValue.ConvertULong((ulong)value), sql.Val(() => TestValue.ConvertULong((ulong)value)));
            Assert.Equal(TestValue.ConvertFloat((float)value), sql.Val(() => TestValue.ConvertFloat((float)value)));
            Assert.Equal(TestValue.ConvertDouble((double)value), sql.Val(() => TestValue.ConvertDouble((double)value)));
        }

        [Theory]
        [InlineData(1)]
        public void Convert_Value_Int_ValueType_Checked(int? value)
        {
            checked
            {
                Assert.Equal(TestValue.ConvertChar((char)value), sql.Val(() => TestValue.ConvertChar((char)value)));
                Assert.Equal(TestValue.ConvertSByte((sbyte)value), sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
                Assert.Equal(TestValue.ConvertByte((byte)value), sql.Val(() => TestValue.ConvertByte((byte)value)));
                Assert.Equal(TestValue.ConvertShort((short)value), sql.Val(() => TestValue.ConvertShort((short)value)));
                Assert.Equal(TestValue.ConvertUShort((ushort)value), sql.Val(() => TestValue.ConvertUShort((ushort)value)));
                Assert.Equal(TestValue.ConvertInt((int)value), sql.Val(() => TestValue.ConvertInt((int)value)));
                Assert.Equal(TestValue.ConvertUInt((uint)value), sql.Val(() => TestValue.ConvertUInt((uint)value)));
                Assert.Equal(TestValue.ConvertLong((long)value), sql.Val(() => TestValue.ConvertLong((long)value)));
                Assert.Equal(TestValue.ConvertULong((ulong)value), sql.Val(() => TestValue.ConvertULong((ulong)value)));
                Assert.Equal(TestValue.ConvertFloat((float)value), sql.Val(() => TestValue.ConvertFloat((float)value)));
                Assert.Equal(TestValue.ConvertDouble((double)value), sql.Val(() => TestValue.ConvertDouble((double)value)));
            }
        }

        [Theory]
        [InlineData(int.MinValue)]
        public void Convert_Value_Int_ValueType_Checked_Overflow_Min(int? value)
        {
            checked
            {
                Exception ex;
                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertChar((char)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertByte((byte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertShort((short)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertUShort((ushort)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertUInt((uint)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertULong((ulong)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                Assert.Equal(TestValue.ConvertInt((int)value), sql.Val(() => TestValue.ConvertInt((int)value)));
                Assert.Equal(TestValue.ConvertLong((long)value), sql.Val(() => TestValue.ConvertLong((long)value)));
                Assert.Equal(TestValue.ConvertFloat((float)value), sql.Val(() => TestValue.ConvertFloat((float)value)));
                Assert.Equal(TestValue.ConvertDouble((double)value), sql.Val(() => TestValue.ConvertDouble((double)value)));
            }
        }

        [Theory]
        [InlineData(int.MaxValue)]
        public void Convert_Value_Int_ValueType_Checked_Overflow_Max(int? value)
        {
            checked
            {
                Exception ex;
                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertChar((char)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertByte((byte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertShort((short)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertUShort((ushort)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                Assert.Equal(TestValue.ConvertInt((int)value), sql.Val(() => TestValue.ConvertInt((int)value)));
                Assert.Equal(TestValue.ConvertUInt((uint)value), sql.Val(() => TestValue.ConvertUInt((uint)value)));
                Assert.Equal(TestValue.ConvertLong((long)value), sql.Val(() => TestValue.ConvertLong((long)value)));
                Assert.Equal(TestValue.ConvertULong((ulong)value), sql.Val(() => TestValue.ConvertULong((ulong)value)));
                Assert.Equal(TestValue.ConvertFloat((float)value), sql.Val(() => TestValue.ConvertFloat((float)value)));
                Assert.Equal(TestValue.ConvertDouble((double)value), sql.Val(() => TestValue.ConvertDouble((double)value)));
            }
        }

        [Theory]
        [InlineData(uint.MinValue)]
        [InlineData(1u)]
        [InlineData(uint.MaxValue)]
        public void Convert_Value_UInt(uint value)
        {
            Assert.Equal(TestValue.ConvertChar((char)value), sql.Val(() => TestValue.ConvertChar((char)value)));
            Assert.Equal(TestValue.ConvertSByte((sbyte)value), sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
            Assert.Equal(TestValue.ConvertByte((byte)value), sql.Val(() => TestValue.ConvertByte((byte)value)));
            Assert.Equal(TestValue.ConvertShort((short)value), sql.Val(() => TestValue.ConvertShort((short)value)));
            Assert.Equal(TestValue.ConvertUShort((ushort)value), sql.Val(() => TestValue.ConvertUShort((ushort)value)));
            Assert.Equal(TestValue.ConvertInt((int)value), sql.Val(() => TestValue.ConvertInt((int)value)));
            Assert.Equal(TestValue.ConvertUInt(value), sql.Val(() => TestValue.ConvertUInt(value)));
            Assert.Equal(TestValue.ConvertLong(value), sql.Val(() => TestValue.ConvertLong(value)));
            Assert.Equal(TestValue.ConvertULong(value), sql.Val(() => TestValue.ConvertULong(value)));
            Assert.Equal(TestValue.ConvertFloat(value), sql.Val(() => TestValue.ConvertFloat(value)));
            Assert.Equal(TestValue.ConvertDouble(value), sql.Val(() => TestValue.ConvertDouble(value)));
        }

        [Theory]
        [InlineData(uint.MinValue)]
        [InlineData(1u)]
        public void Convert_Value_UInt_Checked(uint value)
        {
            checked
            {
                Assert.Equal(TestValue.ConvertChar((char)value), sql.Val(() => TestValue.ConvertChar((char)value)));
                Assert.Equal(TestValue.ConvertSByte((sbyte)value), sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
                Assert.Equal(TestValue.ConvertByte((byte)value), sql.Val(() => TestValue.ConvertByte((byte)value)));
                Assert.Equal(TestValue.ConvertShort((short)value), sql.Val(() => TestValue.ConvertShort((short)value)));
                Assert.Equal(TestValue.ConvertUShort((ushort)value), sql.Val(() => TestValue.ConvertUShort((ushort)value)));
                Assert.Equal(TestValue.ConvertInt((int)value), sql.Val(() => TestValue.ConvertInt((int)value)));
                Assert.Equal(TestValue.ConvertUInt(value), sql.Val(() => TestValue.ConvertUInt(value)));
                Assert.Equal(TestValue.ConvertLong(value), sql.Val(() => TestValue.ConvertLong(value)));
                Assert.Equal(TestValue.ConvertULong(value), sql.Val(() => TestValue.ConvertULong(value)));
                Assert.Equal(TestValue.ConvertFloat(value), sql.Val(() => TestValue.ConvertFloat(value)));
                Assert.Equal(TestValue.ConvertDouble(value), sql.Val(() => TestValue.ConvertDouble(value)));
            }
        }

        [Theory]
        [InlineData(uint.MaxValue)]
        public void Convert_Value_UInt_Checked_Overflow(uint value)
        {
            checked
            {
                Exception ex;
                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertChar((char)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertByte((byte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertShort((short)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertUShort((ushort)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertInt((int)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                Assert.Equal(TestValue.ConvertUInt(value), sql.Val(() => TestValue.ConvertUInt(value)));
                Assert.Equal(TestValue.ConvertLong(value), sql.Val(() => TestValue.ConvertLong(value)));
                Assert.Equal(TestValue.ConvertULong(value), sql.Val(() => TestValue.ConvertULong(value)));
                Assert.Equal(TestValue.ConvertFloat(value), sql.Val(() => TestValue.ConvertFloat(value)));
                Assert.Equal(TestValue.ConvertDouble(value), sql.Val(() => TestValue.ConvertDouble(value)));
            }
        }

        [Theory]
        [InlineData(uint.MinValue)]
        [InlineData(1u)]
        [InlineData(uint.MaxValue)]
        public void Convert_Value_UInt_ValueType(uint? value)
        {
            Assert.Equal(TestValue.ConvertChar((char)value), sql.Val(() => TestValue.ConvertChar((char)value)));
            Assert.Equal(TestValue.ConvertSByte((sbyte)value), sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
            Assert.Equal(TestValue.ConvertByte((byte)value), sql.Val(() => TestValue.ConvertByte((byte)value)));
            Assert.Equal(TestValue.ConvertShort((short)value), sql.Val(() => TestValue.ConvertShort((short)value)));
            Assert.Equal(TestValue.ConvertUShort((ushort)value), sql.Val(() => TestValue.ConvertUShort((ushort)value)));
            Assert.Equal(TestValue.ConvertInt((int)value), sql.Val(() => TestValue.ConvertInt((int)value)));
            Assert.Equal(TestValue.ConvertUInt((uint)value), sql.Val(() => TestValue.ConvertUInt((uint)value)));
            Assert.Equal(TestValue.ConvertLong((long)value), sql.Val(() => TestValue.ConvertLong((long)value)));
            Assert.Equal(TestValue.ConvertULong((ulong)value), sql.Val(() => TestValue.ConvertULong((ulong)value)));
            Assert.Equal(TestValue.ConvertFloat((float)value), sql.Val(() => TestValue.ConvertFloat((float)value)));
            Assert.Equal(TestValue.ConvertDouble((double)value), sql.Val(() => TestValue.ConvertDouble((double)value)));
        }

        [Theory]
        [InlineData(uint.MinValue)]
        [InlineData(1u)]
        public void Convert_Value_UInt_ValueType_Checked(uint? value)
        {
            checked
            {
                Assert.Equal(TestValue.ConvertChar((char)value), sql.Val(() => TestValue.ConvertChar((char)value)));
                Assert.Equal(TestValue.ConvertSByte((sbyte)value), sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
                Assert.Equal(TestValue.ConvertByte((byte)value), sql.Val(() => TestValue.ConvertByte((byte)value)));
                Assert.Equal(TestValue.ConvertShort((short)value), sql.Val(() => TestValue.ConvertShort((short)value)));
                Assert.Equal(TestValue.ConvertUShort((ushort)value), sql.Val(() => TestValue.ConvertUShort((ushort)value)));
                Assert.Equal(TestValue.ConvertInt((int)value), sql.Val(() => TestValue.ConvertInt((int)value)));
                Assert.Equal(TestValue.ConvertUInt((uint)value), sql.Val(() => TestValue.ConvertUInt((uint)value)));
                Assert.Equal(TestValue.ConvertLong((long)value), sql.Val(() => TestValue.ConvertLong((long)value)));
                Assert.Equal(TestValue.ConvertULong((ulong)value), sql.Val(() => TestValue.ConvertULong((ulong)value)));
                Assert.Equal(TestValue.ConvertFloat((float)value), sql.Val(() => TestValue.ConvertFloat((float)value)));
                Assert.Equal(TestValue.ConvertDouble((double)value), sql.Val(() => TestValue.ConvertDouble((double)value)));
            }
        }

        [Theory]
        [InlineData(uint.MaxValue)]
        public void Convert_Value_UInt_ValueType_Checked_Overflow(uint? value)
        {
            checked
            {
                Exception ex;
                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertChar((char)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertByte((byte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertShort((short)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertUShort((ushort)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertInt((int)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                Assert.Equal(TestValue.ConvertUInt((uint)value), sql.Val(() => TestValue.ConvertUInt((uint)value)));
                Assert.Equal(TestValue.ConvertLong((long)value), sql.Val(() => TestValue.ConvertLong((long)value)));
                Assert.Equal(TestValue.ConvertULong((ulong)value), sql.Val(() => TestValue.ConvertULong((ulong)value)));
                Assert.Equal(TestValue.ConvertFloat((float)value), sql.Val(() => TestValue.ConvertFloat((float)value)));
                Assert.Equal(TestValue.ConvertDouble((double)value), sql.Val(() => TestValue.ConvertDouble((double)value)));
            }
        }

        [Theory]
        [InlineData(long.MinValue)]
        [InlineData(1L)]
        [InlineData(long.MaxValue)]
        public void Convert_Value_Long(long value)
        {
            Assert.Equal(TestValue.ConvertChar((char)value), sql.Val(() => TestValue.ConvertChar((char)value)));
            Assert.Equal(TestValue.ConvertSByte((sbyte)value), sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
            Assert.Equal(TestValue.ConvertByte((byte)value), sql.Val(() => TestValue.ConvertByte((byte)value)));
            Assert.Equal(TestValue.ConvertShort((short)value), sql.Val(() => TestValue.ConvertShort((short)value)));
            Assert.Equal(TestValue.ConvertUShort((ushort)value), sql.Val(() => TestValue.ConvertUShort((ushort)value)));
            Assert.Equal(TestValue.ConvertInt((int)value), sql.Val(() => TestValue.ConvertInt((int)value)));
            Assert.Equal(TestValue.ConvertUInt((uint)value), sql.Val(() => TestValue.ConvertUInt((uint)value)));
            Assert.Equal(TestValue.ConvertLong(value), sql.Val(() => TestValue.ConvertLong(value)));
            Assert.Equal(TestValue.ConvertULong((ulong)value), sql.Val(() => TestValue.ConvertULong((ulong)value)));
            Assert.Equal(TestValue.ConvertFloat(value), sql.Val(() => TestValue.ConvertFloat(value)));
            Assert.Equal(TestValue.ConvertDouble(value), sql.Val(() => TestValue.ConvertDouble(value)));
        }

        [Theory]
        [InlineData(1L)]
        public void Convert_Value_Long_Checked(long value)
        {
            checked
            {
                Assert.Equal(TestValue.ConvertChar((char)value), sql.Val(() => TestValue.ConvertChar((char)value)));
                Assert.Equal(TestValue.ConvertSByte((sbyte)value), sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
                Assert.Equal(TestValue.ConvertByte((byte)value), sql.Val(() => TestValue.ConvertByte((byte)value)));
                Assert.Equal(TestValue.ConvertShort((short)value), sql.Val(() => TestValue.ConvertShort((short)value)));
                Assert.Equal(TestValue.ConvertUShort((ushort)value), sql.Val(() => TestValue.ConvertUShort((ushort)value)));
                Assert.Equal(TestValue.ConvertInt((int)value), sql.Val(() => TestValue.ConvertInt((int)value)));
                Assert.Equal(TestValue.ConvertUInt((uint)value), sql.Val(() => TestValue.ConvertUInt((uint)value)));
                Assert.Equal(TestValue.ConvertLong(value), sql.Val(() => TestValue.ConvertLong(value)));
                Assert.Equal(TestValue.ConvertULong((ulong)value), sql.Val(() => TestValue.ConvertULong((ulong)value)));
                Assert.Equal(TestValue.ConvertFloat(value), sql.Val(() => TestValue.ConvertFloat(value)));
                Assert.Equal(TestValue.ConvertDouble(value), sql.Val(() => TestValue.ConvertDouble(value)));
            }
        }

        [Theory]
        [InlineData(long.MinValue)]
        public void Convert_Value_Long_Checked_Overflow_Min(long value)
        {
            checked
            {
                Exception ex;
                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertChar((char)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertByte((byte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertShort((short)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertUShort((ushort)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertInt((int)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertUInt((uint)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertULong((ulong)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                Assert.Equal(TestValue.ConvertLong(value), sql.Val(() => TestValue.ConvertLong(value)));
                Assert.Equal(TestValue.ConvertFloat(value), sql.Val(() => TestValue.ConvertFloat(value)));
                Assert.Equal(TestValue.ConvertDouble(value), sql.Val(() => TestValue.ConvertDouble(value)));
            }
        }

        [Theory]
        [InlineData(long.MaxValue)]
        public void Convert_Value_Long_Checked_Overflow_Max(long value)
        {
            checked
            {
                Exception ex;
                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertChar((char)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertByte((byte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertShort((short)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertUShort((ushort)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertInt((int)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertUInt((uint)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                Assert.Equal(TestValue.ConvertLong(value), sql.Val(() => TestValue.ConvertLong(value)));
                Assert.Equal(TestValue.ConvertULong((ulong)value), sql.Val(() => TestValue.ConvertULong((ulong)value)));
                Assert.Equal(TestValue.ConvertFloat(value), sql.Val(() => TestValue.ConvertFloat(value)));
                Assert.Equal(TestValue.ConvertDouble(value), sql.Val(() => TestValue.ConvertDouble(value)));
            }
        }

        [Theory]
        [InlineData(long.MinValue)]
        [InlineData(1L)]
        [InlineData(long.MaxValue)]
        public void Convert_Value_Long_ValueType(long? value)
        {
            Assert.Equal(TestValue.ConvertChar((char)value), sql.Val(() => TestValue.ConvertChar((char)value)));
            Assert.Equal(TestValue.ConvertSByte((sbyte)value), sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
            Assert.Equal(TestValue.ConvertByte((byte)value), sql.Val(() => TestValue.ConvertByte((byte)value)));
            Assert.Equal(TestValue.ConvertShort((short)value), sql.Val(() => TestValue.ConvertShort((short)value)));
            Assert.Equal(TestValue.ConvertUShort((ushort)value), sql.Val(() => TestValue.ConvertUShort((ushort)value)));
            Assert.Equal(TestValue.ConvertInt((int)value), sql.Val(() => TestValue.ConvertInt((int)value)));
            Assert.Equal(TestValue.ConvertUInt((uint)value), sql.Val(() => TestValue.ConvertUInt((uint)value)));
            Assert.Equal(TestValue.ConvertLong((long)value), sql.Val(() => TestValue.ConvertLong((long)value)));
            Assert.Equal(TestValue.ConvertULong((ulong)value), sql.Val(() => TestValue.ConvertULong((ulong)value)));
            Assert.Equal(TestValue.ConvertFloat((float)value), sql.Val(() => TestValue.ConvertFloat((float)value)));
            Assert.Equal(TestValue.ConvertDouble((double)value), sql.Val(() => TestValue.ConvertDouble((double)value)));
        }

        [Theory]
        [InlineData(1L)]
        public void Convert_Value_Long_ValueType_Checked(long? value)
        {
            checked
            {
                Assert.Equal(TestValue.ConvertChar((char)value), sql.Val(() => TestValue.ConvertChar((char)value)));
                Assert.Equal(TestValue.ConvertSByte((sbyte)value), sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
                Assert.Equal(TestValue.ConvertByte((byte)value), sql.Val(() => TestValue.ConvertByte((byte)value)));
                Assert.Equal(TestValue.ConvertShort((short)value), sql.Val(() => TestValue.ConvertShort((short)value)));
                Assert.Equal(TestValue.ConvertUShort((ushort)value), sql.Val(() => TestValue.ConvertUShort((ushort)value)));
                Assert.Equal(TestValue.ConvertInt((int)value), sql.Val(() => TestValue.ConvertInt((int)value)));
                Assert.Equal(TestValue.ConvertUInt((uint)value), sql.Val(() => TestValue.ConvertUInt((uint)value)));
                Assert.Equal(TestValue.ConvertLong((long)value), sql.Val(() => TestValue.ConvertLong((long)value)));
                Assert.Equal(TestValue.ConvertULong((ulong)value), sql.Val(() => TestValue.ConvertULong((ulong)value)));
                Assert.Equal(TestValue.ConvertFloat((float)value), sql.Val(() => TestValue.ConvertFloat((float)value)));
                Assert.Equal(TestValue.ConvertDouble((double)value), sql.Val(() => TestValue.ConvertDouble((double)value)));
            }
        }

        [Theory]
        [InlineData(long.MinValue)]
        public void Convert_Value_Long_ValueType_Checked_Overflow_Min(long? value)
        {
            checked
            {
                Exception ex;
                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertChar((char)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertByte((byte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertShort((short)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertUShort((ushort)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertInt((int)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertUInt((uint)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertULong((ulong)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                Assert.Equal(TestValue.ConvertLong((long)value), sql.Val(() => TestValue.ConvertLong((long)value)));
                Assert.Equal(TestValue.ConvertFloat((float)value), sql.Val(() => TestValue.ConvertFloat((float)value)));
                Assert.Equal(TestValue.ConvertDouble((double)value), sql.Val(() => TestValue.ConvertDouble((double)value)));
            }
        }

        [Theory]
        [InlineData(long.MaxValue)]
        public void Convert_Value_Long_ValueType_Checked_Overflow_Max(long? value)
        {
            checked
            {
                Exception ex;
                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertChar((char)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertByte((byte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertShort((short)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertUShort((ushort)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertInt((int)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertUInt((uint)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                Assert.Equal(TestValue.ConvertLong((long)value), sql.Val(() => TestValue.ConvertLong((long)value)));
                Assert.Equal(TestValue.ConvertULong((ulong)value), sql.Val(() => TestValue.ConvertULong((ulong)value)));
                Assert.Equal(TestValue.ConvertFloat((float)value), sql.Val(() => TestValue.ConvertFloat((float)value)));
                Assert.Equal(TestValue.ConvertDouble((double)value), sql.Val(() => TestValue.ConvertDouble((double)value)));
            }
        }

        [Theory]
        [InlineData(ulong.MinValue)]
        [InlineData(1ul)]
        [InlineData(ulong.MaxValue)]
        public void Convert_Value_ULong(ulong value)
        {
            Assert.Equal(TestValue.ConvertChar((char)value), sql.Val(() => TestValue.ConvertChar((char)value)));
            Assert.Equal(TestValue.ConvertSByte((sbyte)value), sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
            Assert.Equal(TestValue.ConvertByte((byte)value), sql.Val(() => TestValue.ConvertByte((byte)value)));
            Assert.Equal(TestValue.ConvertShort((short)value), sql.Val(() => TestValue.ConvertShort((short)value)));
            Assert.Equal(TestValue.ConvertUShort((ushort)value), sql.Val(() => TestValue.ConvertUShort((ushort)value)));
            Assert.Equal(TestValue.ConvertInt((int)value), sql.Val(() => TestValue.ConvertInt((int)value)));
            Assert.Equal(TestValue.ConvertUInt((uint)value), sql.Val(() => TestValue.ConvertUInt((uint)value)));
            Assert.Equal(TestValue.ConvertLong((long)value), sql.Val(() => TestValue.ConvertLong((long)value)));
            Assert.Equal(TestValue.ConvertULong(value), sql.Val(() => TestValue.ConvertULong(value)));
            Assert.Equal(TestValue.ConvertFloat(value), sql.Val(() => TestValue.ConvertFloat(value)));
            Assert.Equal(TestValue.ConvertDouble(value), sql.Val(() => TestValue.ConvertDouble(value)));
        }

        [Theory]
        [InlineData(ulong.MinValue)]
        [InlineData(1ul)]
        public void Convert_Value_ULong_Checked(ulong value)
        {
            checked
            {
                Assert.Equal(TestValue.ConvertChar((char)value), sql.Val(() => TestValue.ConvertChar((char)value)));
                Assert.Equal(TestValue.ConvertSByte((sbyte)value), sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
                Assert.Equal(TestValue.ConvertByte((byte)value), sql.Val(() => TestValue.ConvertByte((byte)value)));
                Assert.Equal(TestValue.ConvertShort((short)value), sql.Val(() => TestValue.ConvertShort((short)value)));
                Assert.Equal(TestValue.ConvertUShort((ushort)value), sql.Val(() => TestValue.ConvertUShort((ushort)value)));
                Assert.Equal(TestValue.ConvertInt((int)value), sql.Val(() => TestValue.ConvertInt((int)value)));
                Assert.Equal(TestValue.ConvertUInt((uint)value), sql.Val(() => TestValue.ConvertUInt((uint)value)));
                Assert.Equal(TestValue.ConvertLong((long)value), sql.Val(() => TestValue.ConvertLong((long)value)));
                Assert.Equal(TestValue.ConvertULong(value), sql.Val(() => TestValue.ConvertULong(value)));
                Assert.Equal(TestValue.ConvertFloat(value), sql.Val(() => TestValue.ConvertFloat(value)));
                Assert.Equal(TestValue.ConvertDouble(value), sql.Val(() => TestValue.ConvertDouble(value)));
            }
        }

        [Theory]
        [InlineData(ulong.MaxValue)]
        public void Convert_Value_ULong_Checked_Overflow(ulong value)
        {
            checked
            {
                Exception ex;
                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertChar((char)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertByte((byte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertShort((short)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertUShort((ushort)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertInt((int)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertUInt((uint)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertLong((long)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                Assert.Equal(TestValue.ConvertULong(value), sql.Val(() => TestValue.ConvertULong(value)));
                Assert.Equal(TestValue.ConvertFloat(value), sql.Val(() => TestValue.ConvertFloat(value)));
                Assert.Equal(TestValue.ConvertDouble(value), sql.Val(() => TestValue.ConvertDouble(value)));
            }
        }

        [Theory]
        [InlineData(ulong.MinValue)]
        [InlineData(1ul)]
        [InlineData(ulong.MaxValue)]
        public void Convert_Value_ULong_ValueType(ulong? value)
        {
            Assert.Equal(TestValue.ConvertChar((char)value), sql.Val(() => TestValue.ConvertChar((char)value)));
            Assert.Equal(TestValue.ConvertSByte((sbyte)value), sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
            Assert.Equal(TestValue.ConvertByte((byte)value), sql.Val(() => TestValue.ConvertByte((byte)value)));
            Assert.Equal(TestValue.ConvertShort((short)value), sql.Val(() => TestValue.ConvertShort((short)value)));
            Assert.Equal(TestValue.ConvertUShort((ushort)value), sql.Val(() => TestValue.ConvertUShort((ushort)value)));
            Assert.Equal(TestValue.ConvertInt((int)value), sql.Val(() => TestValue.ConvertInt((int)value)));
            Assert.Equal(TestValue.ConvertUInt((uint)value), sql.Val(() => TestValue.ConvertUInt((uint)value)));
            Assert.Equal(TestValue.ConvertLong((long)value), sql.Val(() => TestValue.ConvertLong((long)value)));
            Assert.Equal(TestValue.ConvertULong((ulong)value), sql.Val(() => TestValue.ConvertULong((ulong)value)));
            Assert.Equal(TestValue.ConvertFloat((float)value), sql.Val(() => TestValue.ConvertFloat((float)value)));
            Assert.Equal(TestValue.ConvertDouble((double)value), sql.Val(() => TestValue.ConvertDouble((double)value)));
        }

        [Theory]
        [InlineData(ulong.MinValue)]
        [InlineData(1ul)]
        public void Convert_Value_ULong_ValueType_Checked(ulong? value)
        {
            checked
            {
                Assert.Equal(TestValue.ConvertChar((char)value), sql.Val(() => TestValue.ConvertChar((char)value)));
                Assert.Equal(TestValue.ConvertSByte((sbyte)value), sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
                Assert.Equal(TestValue.ConvertByte((byte)value), sql.Val(() => TestValue.ConvertByte((byte)value)));
                Assert.Equal(TestValue.ConvertShort((short)value), sql.Val(() => TestValue.ConvertShort((short)value)));
                Assert.Equal(TestValue.ConvertUShort((ushort)value), sql.Val(() => TestValue.ConvertUShort((ushort)value)));
                Assert.Equal(TestValue.ConvertInt((int)value), sql.Val(() => TestValue.ConvertInt((int)value)));
                Assert.Equal(TestValue.ConvertUInt((uint)value), sql.Val(() => TestValue.ConvertUInt((uint)value)));
                Assert.Equal(TestValue.ConvertLong((long)value), sql.Val(() => TestValue.ConvertLong((long)value)));
                Assert.Equal(TestValue.ConvertULong((ulong)value), sql.Val(() => TestValue.ConvertULong((ulong)value)));
                Assert.Equal(TestValue.ConvertFloat((float)value), sql.Val(() => TestValue.ConvertFloat((float)value)));
                Assert.Equal(TestValue.ConvertDouble((double)value), sql.Val(() => TestValue.ConvertDouble((double)value)));
            }
        }

        [Theory]
        [InlineData(ulong.MaxValue)]
        public void Convert_Value_ULong_ValueType_Checked_Overflow(ulong? value)
        {
            checked
            {
                Exception ex;
                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertChar((char)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertByte((byte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertShort((short)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertUShort((ushort)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertInt((int)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertUInt((uint)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertLong((long)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                Assert.Equal(TestValue.ConvertULong((ulong)value), sql.Val(() => TestValue.ConvertULong((ulong)value)));
                Assert.Equal(TestValue.ConvertFloat((float)value), sql.Val(() => TestValue.ConvertFloat((float)value)));
                Assert.Equal(TestValue.ConvertDouble((double)value), sql.Val(() => TestValue.ConvertDouble((double)value)));
            }
        }

        [Theory]
        [InlineData(float.MinValue)]
        [InlineData(1f)]
        [InlineData(float.MaxValue)]
        public void Convert_Value_Float(float value)
        {
            Assert.Equal(TestValue.ConvertChar((char)value), sql.Val(() => TestValue.ConvertChar((char)value)));
            Assert.Equal(TestValue.ConvertSByte((sbyte)value), sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
            Assert.Equal(TestValue.ConvertByte((byte)value), sql.Val(() => TestValue.ConvertByte((byte)value)));
            Assert.Equal(TestValue.ConvertShort((short)value), sql.Val(() => TestValue.ConvertShort((short)value)));
            Assert.Equal(TestValue.ConvertUShort((ushort)value), sql.Val(() => TestValue.ConvertUShort((ushort)value)));
            Assert.Equal(TestValue.ConvertInt((int)value), sql.Val(() => TestValue.ConvertInt((int)value)));
            Assert.Equal(TestValue.ConvertUInt((uint)value), sql.Val(() => TestValue.ConvertUInt((uint)value)));
            Assert.Equal(TestValue.ConvertLong((long)value), sql.Val(() => TestValue.ConvertLong((long)value)));
            Assert.Equal(TestValue.ConvertULong((ulong)value), sql.Val(() => TestValue.ConvertULong((ulong)value)));
            Assert.Equal(TestValue.ConvertFloat(value), sql.Val(() => TestValue.ConvertFloat(value)));
            Assert.Equal(TestValue.ConvertDouble(value), sql.Val(() => TestValue.ConvertDouble(value)));
        }

        [Theory]
        [InlineData(1f)]
        public void Convert_Value_Float_Checked(float value)
        {
            checked
            {
                Assert.Equal(TestValue.ConvertChar((char)value), sql.Val(() => TestValue.ConvertChar((char)value)));
                Assert.Equal(TestValue.ConvertSByte((sbyte)value), sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
                Assert.Equal(TestValue.ConvertByte((byte)value), sql.Val(() => TestValue.ConvertByte((byte)value)));
                Assert.Equal(TestValue.ConvertShort((short)value), sql.Val(() => TestValue.ConvertShort((short)value)));
                Assert.Equal(TestValue.ConvertUShort((ushort)value), sql.Val(() => TestValue.ConvertUShort((ushort)value)));
                Assert.Equal(TestValue.ConvertInt((int)value), sql.Val(() => TestValue.ConvertInt((int)value)));
                Assert.Equal(TestValue.ConvertUInt((uint)value), sql.Val(() => TestValue.ConvertUInt((uint)value)));
                Assert.Equal(TestValue.ConvertLong((long)value), sql.Val(() => TestValue.ConvertLong((long)value)));
                Assert.Equal(TestValue.ConvertULong((ulong)value), sql.Val(() => TestValue.ConvertULong((ulong)value)));
                Assert.Equal(TestValue.ConvertFloat(value), sql.Val(() => TestValue.ConvertFloat(value)));
                Assert.Equal(TestValue.ConvertDouble(value), sql.Val(() => TestValue.ConvertDouble(value)));
            }
        }

        [Theory]
        [InlineData(float.MinValue)]
        [InlineData(float.MaxValue)]
        public void Convert_Value_Float_Checked_Overflow(float value)
        {
            checked
            {
                Exception ex;
                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertChar((char)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertByte((byte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertShort((short)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertUShort((ushort)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertInt((int)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertUInt((uint)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertLong((long)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertULong((ulong)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                Assert.Equal(TestValue.ConvertFloat((float)value), sql.Val(() => TestValue.ConvertFloat((float)value)));
                Assert.Equal(TestValue.ConvertDouble(value), sql.Val(() => TestValue.ConvertDouble(value)));
            }
        }

        [Theory]
        [InlineData(float.MinValue)]
        [InlineData(1f)]
        [InlineData(float.MaxValue)]
        public void Convert_Value_Float_ValueType(float? value)
        {
            Assert.Equal(TestValue.ConvertChar((char)value), sql.Val(() => TestValue.ConvertChar((char)value)));
            Assert.Equal(TestValue.ConvertSByte((sbyte)value), sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
            Assert.Equal(TestValue.ConvertByte((byte)value), sql.Val(() => TestValue.ConvertByte((byte)value)));
            Assert.Equal(TestValue.ConvertShort((short)value), sql.Val(() => TestValue.ConvertShort((short)value)));
            Assert.Equal(TestValue.ConvertUShort((ushort)value), sql.Val(() => TestValue.ConvertUShort((ushort)value)));
            Assert.Equal(TestValue.ConvertInt((int)value), sql.Val(() => TestValue.ConvertInt((int)value)));
            Assert.Equal(TestValue.ConvertUInt((uint)value), sql.Val(() => TestValue.ConvertUInt((uint)value)));
            Assert.Equal(TestValue.ConvertLong((long)value), sql.Val(() => TestValue.ConvertLong((long)value)));
            Assert.Equal(TestValue.ConvertULong((ulong)value), sql.Val(() => TestValue.ConvertULong((ulong)value)));
            Assert.Equal(TestValue.ConvertFloat((float)value), sql.Val(() => TestValue.ConvertFloat((float)value)));
            Assert.Equal(TestValue.ConvertDouble((double)value), sql.Val(() => TestValue.ConvertDouble((double)value)));
        }

        [Theory]
        [InlineData(1f)]
        public void Convert_Value_Float_ValueType_Checked(float? value)
        {
            checked
            {
                Assert.Equal(TestValue.ConvertChar((char)value), sql.Val(() => TestValue.ConvertChar((char)value)));
                Assert.Equal(TestValue.ConvertSByte((sbyte)value), sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
                Assert.Equal(TestValue.ConvertByte((byte)value), sql.Val(() => TestValue.ConvertByte((byte)value)));
                Assert.Equal(TestValue.ConvertShort((short)value), sql.Val(() => TestValue.ConvertShort((short)value)));
                Assert.Equal(TestValue.ConvertUShort((ushort)value), sql.Val(() => TestValue.ConvertUShort((ushort)value)));
                Assert.Equal(TestValue.ConvertInt((int)value), sql.Val(() => TestValue.ConvertInt((int)value)));
                Assert.Equal(TestValue.ConvertUInt((uint)value), sql.Val(() => TestValue.ConvertUInt((uint)value)));
                Assert.Equal(TestValue.ConvertLong((long)value), sql.Val(() => TestValue.ConvertLong((long)value)));
                Assert.Equal(TestValue.ConvertULong((ulong)value), sql.Val(() => TestValue.ConvertULong((ulong)value)));
                Assert.Equal(TestValue.ConvertFloat((float)value), sql.Val(() => TestValue.ConvertFloat((float)value)));
                Assert.Equal(TestValue.ConvertDouble((double)value), sql.Val(() => TestValue.ConvertDouble((double)value)));
            }
        }

        [Theory]
        [InlineData(float.MinValue)]
        [InlineData(float.MaxValue)]
        public void Convert_Value_Float_ValueType_Checked_Overflow(float? value)
        {
            checked
            {
                Exception ex;
                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertChar((char)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertByte((byte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertShort((short)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertUShort((ushort)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertInt((int)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertUInt((uint)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertLong((long)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertULong((ulong)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                Assert.Equal(TestValue.ConvertFloat((float)value), sql.Val(() => TestValue.ConvertFloat((float)value)));
                Assert.Equal(TestValue.ConvertDouble((double)value), sql.Val(() => TestValue.ConvertDouble((double)value)));
            }
        }

        [Theory]
        [InlineData(double.MinValue)]
        [InlineData(1d)]
        [InlineData(double.MaxValue)]
        public void Convert_Value_Double(double value)
        {
            Assert.Equal(TestValue.ConvertChar((char)value), sql.Val(() => TestValue.ConvertChar((char)value)));
            Assert.Equal(TestValue.ConvertSByte((sbyte)value), sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
            Assert.Equal(TestValue.ConvertByte((byte)value), sql.Val(() => TestValue.ConvertByte((byte)value)));
            Assert.Equal(TestValue.ConvertShort((short)value), sql.Val(() => TestValue.ConvertShort((short)value)));
            Assert.Equal(TestValue.ConvertUShort((ushort)value), sql.Val(() => TestValue.ConvertUShort((ushort)value)));
            Assert.Equal(TestValue.ConvertInt((int)value), sql.Val(() => TestValue.ConvertInt((int)value)));
            Assert.Equal(TestValue.ConvertUInt((uint)value), sql.Val(() => TestValue.ConvertUInt((uint)value)));
            Assert.Equal(TestValue.ConvertLong((long)value), sql.Val(() => TestValue.ConvertLong((long)value)));
            Assert.Equal(TestValue.ConvertULong((ulong)value), sql.Val(() => TestValue.ConvertULong((ulong)value)));
            Assert.Equal(TestValue.ConvertFloat((float)value), sql.Val(() => TestValue.ConvertFloat((float)value)));
            Assert.Equal(TestValue.ConvertDouble(value), sql.Val(() => TestValue.ConvertDouble(value)));
        }

        [Theory]
        [InlineData(1d)]
        public void Convert_Value_Double_Checked(double value)
        {
            checked
            {
                Assert.Equal(TestValue.ConvertChar((char)value), sql.Val(() => TestValue.ConvertChar((char)value)));
                Assert.Equal(TestValue.ConvertSByte((sbyte)value), sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
                Assert.Equal(TestValue.ConvertByte((byte)value), sql.Val(() => TestValue.ConvertByte((byte)value)));
                Assert.Equal(TestValue.ConvertShort((short)value), sql.Val(() => TestValue.ConvertShort((short)value)));
                Assert.Equal(TestValue.ConvertUShort((ushort)value), sql.Val(() => TestValue.ConvertUShort((ushort)value)));
                Assert.Equal(TestValue.ConvertInt((int)value), sql.Val(() => TestValue.ConvertInt((int)value)));
                Assert.Equal(TestValue.ConvertUInt((uint)value), sql.Val(() => TestValue.ConvertUInt((uint)value)));
                Assert.Equal(TestValue.ConvertLong((long)value), sql.Val(() => TestValue.ConvertLong((long)value)));
                Assert.Equal(TestValue.ConvertULong((ulong)value), sql.Val(() => TestValue.ConvertULong((ulong)value)));
                Assert.Equal(TestValue.ConvertFloat((float)value), sql.Val(() => TestValue.ConvertFloat((float)value)));
                Assert.Equal(TestValue.ConvertDouble(value), sql.Val(() => TestValue.ConvertDouble(value)));
            }
        }

        [Theory]
        [InlineData(double.MinValue)]
        [InlineData(double.MaxValue)]
        public void Convert_Value_Double_Checked_Overflow(double value)
        {
            checked
            {
                Exception ex;
                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertChar((char)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertByte((byte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertShort((short)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertUShort((ushort)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertInt((int)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertUInt((uint)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertLong((long)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertULong((ulong)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                Assert.Equal(TestValue.ConvertFloat((float)value), sql.Val(() => TestValue.ConvertFloat((float)value)));
                Assert.Equal(TestValue.ConvertDouble(value), sql.Val(() => TestValue.ConvertDouble(value)));
            }
        }

        [Theory]
        [InlineData(double.MinValue)]
        [InlineData(1d)]
        [InlineData(double.MaxValue)]
        public void Convert_Value_Double_ValueType(double? value)
        {
            Assert.Equal(TestValue.ConvertChar((char)value), sql.Val(() => TestValue.ConvertChar((char)value)));
            Assert.Equal(TestValue.ConvertSByte((sbyte)value), sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
            Assert.Equal(TestValue.ConvertByte((byte)value), sql.Val(() => TestValue.ConvertByte((byte)value)));
            Assert.Equal(TestValue.ConvertShort((short)value), sql.Val(() => TestValue.ConvertShort((short)value)));
            Assert.Equal(TestValue.ConvertUShort((ushort)value), sql.Val(() => TestValue.ConvertUShort((ushort)value)));
            Assert.Equal(TestValue.ConvertInt((int)value), sql.Val(() => TestValue.ConvertInt((int)value)));
            Assert.Equal(TestValue.ConvertUInt((uint)value), sql.Val(() => TestValue.ConvertUInt((uint)value)));
            Assert.Equal(TestValue.ConvertLong((long)value), sql.Val(() => TestValue.ConvertLong((long)value)));
            Assert.Equal(TestValue.ConvertULong((ulong)value), sql.Val(() => TestValue.ConvertULong((ulong)value)));
            Assert.Equal(TestValue.ConvertFloat((float)value), sql.Val(() => TestValue.ConvertFloat((float)value)));
            Assert.Equal(TestValue.ConvertDouble((double)value), sql.Val(() => TestValue.ConvertDouble((double)value)));
        }

        [Theory]
        [InlineData(1d)]
        public void Convert_Value_Double_ValueType_Checked(double? value)
        {
            checked
            {
                Assert.Equal(TestValue.ConvertChar((char)value), sql.Val(() => TestValue.ConvertChar((char)value)));
                Assert.Equal(TestValue.ConvertSByte((sbyte)value), sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
                Assert.Equal(TestValue.ConvertByte((byte)value), sql.Val(() => TestValue.ConvertByte((byte)value)));
                Assert.Equal(TestValue.ConvertShort((short)value), sql.Val(() => TestValue.ConvertShort((short)value)));
                Assert.Equal(TestValue.ConvertUShort((ushort)value), sql.Val(() => TestValue.ConvertUShort((ushort)value)));
                Assert.Equal(TestValue.ConvertInt((int)value), sql.Val(() => TestValue.ConvertInt((int)value)));
                Assert.Equal(TestValue.ConvertUInt((uint)value), sql.Val(() => TestValue.ConvertUInt((uint)value)));
                Assert.Equal(TestValue.ConvertLong((long)value), sql.Val(() => TestValue.ConvertLong((long)value)));
                Assert.Equal(TestValue.ConvertULong((ulong)value), sql.Val(() => TestValue.ConvertULong((ulong)value)));
                Assert.Equal(TestValue.ConvertFloat((float)value), sql.Val(() => TestValue.ConvertFloat((float)value)));
                Assert.Equal(TestValue.ConvertDouble((double)value), sql.Val(() => TestValue.ConvertDouble((double)value)));
            }
        }

        [Theory]
        [InlineData(double.MinValue)]
        [InlineData(double.MaxValue)]
        public void Convert_Value_Double_ValueType_Checked_Overflow(double? value)
        {
            checked
            {
                Exception ex;
                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertChar((char)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertByte((byte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertShort((short)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertUShort((ushort)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertInt((int)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertUInt((uint)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertLong((long)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertULong((ulong)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                Assert.Equal(TestValue.ConvertFloat((float)value), sql.Val(() => TestValue.ConvertFloat((float)value)));
                Assert.Equal(TestValue.ConvertDouble((double)value), sql.Val(() => TestValue.ConvertDouble((double)value)));
            }
        }

        [Theory]
        [MemberData(nameof(DataDecimalOne))]
        public void Convert_Value_From_Operator(decimal value)
        {
            Assert.Equal(TestValue.ConvertChar((char)value), sql.Val(() => TestValue.ConvertChar((char)value)));
            Assert.Equal(TestValue.ConvertSByte((sbyte)value), sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
            Assert.Equal(TestValue.ConvertByte((byte)value), sql.Val(() => TestValue.ConvertByte((byte)value)));
            Assert.Equal(TestValue.ConvertShort((short)value), sql.Val(() => TestValue.ConvertShort((short)value)));
            Assert.Equal(TestValue.ConvertUShort((ushort)value), sql.Val(() => TestValue.ConvertUShort((ushort)value)));
            Assert.Equal(TestValue.ConvertInt((int)value), sql.Val(() => TestValue.ConvertInt((int)value)));
            Assert.Equal(TestValue.ConvertUInt((uint)value), sql.Val(() => TestValue.ConvertUInt((uint)value)));
            Assert.Equal(TestValue.ConvertLong((long)value), sql.Val(() => TestValue.ConvertLong((long)value)));
            Assert.Equal(TestValue.ConvertULong((ulong)value), sql.Val(() => TestValue.ConvertULong((ulong)value)));
            Assert.Equal(TestValue.ConvertFloat((float)value), sql.Val(() => TestValue.ConvertFloat((float)value)));
            Assert.Equal(TestValue.ConvertDouble((double)value), sql.Val(() => TestValue.ConvertDouble((double)value)));
        }

        [Theory]
        [MemberData(nameof(DataDecimalMinMax))]
        public void Convert_Value_From_Operator_Overflow(decimal value)
        {
            Exception ex;
            ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertChar((char)value)));
            Assert.IsType<OverflowException>(ex.InnerException);

            ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
            Assert.IsType<OverflowException>(ex.InnerException);

            ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertByte((byte)value)));
            Assert.IsType<OverflowException>(ex.InnerException);

            ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertShort((short)value)));
            Assert.IsType<OverflowException>(ex.InnerException);

            ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertUShort((ushort)value)));
            Assert.IsType<OverflowException>(ex.InnerException);

            ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertInt((int)value)));
            Assert.IsType<OverflowException>(ex.InnerException);

            ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertUInt((uint)value)));
            Assert.IsType<OverflowException>(ex.InnerException);

            ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertLong((long)value)));
            Assert.IsType<OverflowException>(ex.InnerException);

            ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertULong((ulong)value)));
            Assert.IsType<OverflowException>(ex.InnerException);

            Assert.Equal(TestValue.ConvertFloat((float)value), sql.Val(() => TestValue.ConvertFloat((float)value)));
            Assert.Equal(TestValue.ConvertDouble((double)value), sql.Val(() => TestValue.ConvertDouble((double)value)));
        }

        [Theory]
        [MemberData(nameof(DataDecimalOneNullable))]
        public void Convert_Value_From_Operator_ValueType(decimal? value)
        {
            Assert.Equal(TestValue.ConvertChar((char)value), sql.Val(() => TestValue.ConvertChar((char)value)));
            Assert.Equal(TestValue.ConvertSByte((sbyte)value), sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
            Assert.Equal(TestValue.ConvertByte((byte)value), sql.Val(() => TestValue.ConvertByte((byte)value)));
            Assert.Equal(TestValue.ConvertShort((short)value), sql.Val(() => TestValue.ConvertShort((short)value)));
            Assert.Equal(TestValue.ConvertUShort((ushort)value), sql.Val(() => TestValue.ConvertUShort((ushort)value)));
            Assert.Equal(TestValue.ConvertInt((int)value), sql.Val(() => TestValue.ConvertInt((int)value)));
            Assert.Equal(TestValue.ConvertUInt((uint)value), sql.Val(() => TestValue.ConvertUInt((uint)value)));
            Assert.Equal(TestValue.ConvertLong((long)value), sql.Val(() => TestValue.ConvertLong((long)value)));
            Assert.Equal(TestValue.ConvertULong((ulong)value), sql.Val(() => TestValue.ConvertULong((ulong)value)));
            Assert.Equal(TestValue.ConvertFloat((float)value), sql.Val(() => TestValue.ConvertFloat((float)value)));
            Assert.Equal(TestValue.ConvertDouble((double)value), sql.Val(() => TestValue.ConvertDouble((double)value)));
        }

        [Theory]
        [MemberData(nameof(DataDecimalMinMaxNullable))]
        public void Convert_Value_From_Operator_ValueType_Overflow(decimal? value)
        {
            Exception ex;
            ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertChar((char)value)));
            Assert.IsType<OverflowException>(ex.InnerException);

            ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
            Assert.IsType<OverflowException>(ex.InnerException);

            ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertByte((byte)value)));
            Assert.IsType<OverflowException>(ex.InnerException);

            ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertShort((short)value)));
            Assert.IsType<OverflowException>(ex.InnerException);

            ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertUShort((ushort)value)));
            Assert.IsType<OverflowException>(ex.InnerException);

            ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertInt((int)value)));
            Assert.IsType<OverflowException>(ex.InnerException);

            ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertUInt((uint)value)));
            Assert.IsType<OverflowException>(ex.InnerException);

            ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertLong((long)value)));
            Assert.IsType<OverflowException>(ex.InnerException);

            ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertULong((ulong)value)));
            Assert.IsType<OverflowException>(ex.InnerException);

            Assert.Equal(TestValue.ConvertFloat((float)value), sql.Val(() => TestValue.ConvertFloat((float)value)));
            Assert.Equal(TestValue.ConvertDouble((double)value), sql.Val(() => TestValue.ConvertDouble((double)value)));
        }

        [Theory]
        [InlineData((char)1, (sbyte)1, (byte)1, (short)1, (ushort)1, 1, 1u, 1L, 1ul, 1f, 1d)]
        public void Convert_Value_To_Operator(char a, sbyte b, byte c, short d, ushort e, int f, uint g, long h, ulong i,
            float j, double k)
        {
            Assert.Equal(TestValue.ConvertDecimal(a), sql.Val(() => TestValue.ConvertDecimal(a)));
            Assert.Equal(TestValue.ConvertDecimal(b), sql.Val(() => TestValue.ConvertDecimal(b)));
            Assert.Equal(TestValue.ConvertDecimal(c), sql.Val(() => TestValue.ConvertDecimal(c)));
            Assert.Equal(TestValue.ConvertDecimal(d), sql.Val(() => TestValue.ConvertDecimal(d)));
            Assert.Equal(TestValue.ConvertDecimal(e), sql.Val(() => TestValue.ConvertDecimal(e)));
            Assert.Equal(TestValue.ConvertDecimal(f), sql.Val(() => TestValue.ConvertDecimal(f)));
            Assert.Equal(TestValue.ConvertDecimal(g), sql.Val(() => TestValue.ConvertDecimal(g)));
            Assert.Equal(TestValue.ConvertDecimal(h), sql.Val(() => TestValue.ConvertDecimal(h)));
            Assert.Equal(TestValue.ConvertDecimal(i), sql.Val(() => TestValue.ConvertDecimal(i)));
            Assert.Equal(TestValue.ConvertDecimal((decimal)j), sql.Val(() => TestValue.ConvertDecimal((decimal)j)));
            Assert.Equal(TestValue.ConvertDecimal((decimal)k), sql.Val(() => TestValue.ConvertDecimal((decimal)k)));
        }

        [Theory]
        [InlineData(char.MinValue, sbyte.MinValue, byte.MinValue, short.MinValue, ushort.MinValue, int.MinValue,
            uint.MinValue, long.MinValue, ulong.MinValue, float.MinValue, double.MinValue)]
        [InlineData(char.MaxValue, sbyte.MaxValue, byte.MaxValue, short.MaxValue, ushort.MaxValue, int.MaxValue,
            uint.MaxValue, long.MaxValue, ulong.MaxValue, float.MaxValue, double.MaxValue)]
        public void Convert_Value_To_Operator_Overflow(char a, sbyte b, byte c, short d, ushort e, int f, uint g, long h,
            ulong i, float j, double k)
        {
            Exception ex;
            ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertDecimal((decimal)j)));
            Assert.IsType<OverflowException>(ex.InnerException);

            ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertDecimal((decimal)k)));
            Assert.IsType<OverflowException>(ex.InnerException);

            Assert.Equal(TestValue.ConvertDecimal(a), sql.Val(() => TestValue.ConvertDecimal(a)));
            Assert.Equal(TestValue.ConvertDecimal(b), sql.Val(() => TestValue.ConvertDecimal(b)));
            Assert.Equal(TestValue.ConvertDecimal(c), sql.Val(() => TestValue.ConvertDecimal(c)));
            Assert.Equal(TestValue.ConvertDecimal(d), sql.Val(() => TestValue.ConvertDecimal(d)));
            Assert.Equal(TestValue.ConvertDecimal(e), sql.Val(() => TestValue.ConvertDecimal(e)));
            Assert.Equal(TestValue.ConvertDecimal(f), sql.Val(() => TestValue.ConvertDecimal(f)));
            Assert.Equal(TestValue.ConvertDecimal(g), sql.Val(() => TestValue.ConvertDecimal(g)));
            Assert.Equal(TestValue.ConvertDecimal(h), sql.Val(() => TestValue.ConvertDecimal(h)));
            Assert.Equal(TestValue.ConvertDecimal(i), sql.Val(() => TestValue.ConvertDecimal(i)));
        }

        [Theory]
        [InlineData((char)1, (sbyte)1, (byte)1, (short)1, (ushort)1, 1, 1u, 1L, 1ul, 1f, 1d)]
        public void Convert_Value_To_Operator_ValueType(char? a, sbyte? b, byte? c, short? d, ushort? e, int? f, uint? g,
            long? h, ulong? i, float? j, double? k)
        {
            Assert.Equal(TestValue.ConvertDecimal((decimal)a), sql.Val(() => TestValue.ConvertDecimal((decimal)a)));
            Assert.Equal(TestValue.ConvertDecimal((decimal)b), sql.Val(() => TestValue.ConvertDecimal((decimal)b)));
            Assert.Equal(TestValue.ConvertDecimal((decimal)c), sql.Val(() => TestValue.ConvertDecimal((decimal)c)));
            Assert.Equal(TestValue.ConvertDecimal((decimal)d), sql.Val(() => TestValue.ConvertDecimal((decimal)d)));
            Assert.Equal(TestValue.ConvertDecimal((decimal)e), sql.Val(() => TestValue.ConvertDecimal((decimal)e)));
            Assert.Equal(TestValue.ConvertDecimal((decimal)f), sql.Val(() => TestValue.ConvertDecimal((decimal)f)));
            Assert.Equal(TestValue.ConvertDecimal((decimal)g), sql.Val(() => TestValue.ConvertDecimal((decimal)g)));
            Assert.Equal(TestValue.ConvertDecimal((decimal)h), sql.Val(() => TestValue.ConvertDecimal((decimal)h)));
            Assert.Equal(TestValue.ConvertDecimal((decimal)i), sql.Val(() => TestValue.ConvertDecimal((decimal)i)));
            Assert.Equal(TestValue.ConvertDecimal((decimal)j), sql.Val(() => TestValue.ConvertDecimal((decimal)j)));
            Assert.Equal(TestValue.ConvertDecimal((decimal)k), sql.Val(() => TestValue.ConvertDecimal((decimal)k)));
        }

        [Theory]
        [InlineData(char.MinValue, sbyte.MinValue, byte.MinValue, short.MinValue, ushort.MinValue, int.MinValue,
            uint.MinValue, long.MinValue, ulong.MinValue, float.MinValue, double.MinValue)]
        [InlineData(char.MaxValue, sbyte.MaxValue, byte.MaxValue, short.MaxValue, ushort.MaxValue, int.MaxValue,
            uint.MaxValue, long.MaxValue, ulong.MaxValue, float.MaxValue, double.MaxValue)]
        public void Convert_Value_To_Operator_ValueType_Overflow(char? a, sbyte? b, byte? c, short? d, ushort? e, int? f,
            uint? g, long? h, ulong? i, float? j, double? k)
        {
            Exception ex;
            ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertDecimal((decimal)j)));
            Assert.IsType<OverflowException>(ex.InnerException);

            ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertDecimal((decimal)k)));
            Assert.IsType<OverflowException>(ex.InnerException);

            Assert.Equal(TestValue.ConvertDecimal((decimal)a), sql.Val(() => TestValue.ConvertDecimal((decimal)a)));
            Assert.Equal(TestValue.ConvertDecimal((decimal)b), sql.Val(() => TestValue.ConvertDecimal((decimal)b)));
            Assert.Equal(TestValue.ConvertDecimal((decimal)c), sql.Val(() => TestValue.ConvertDecimal((decimal)c)));
            Assert.Equal(TestValue.ConvertDecimal((decimal)d), sql.Val(() => TestValue.ConvertDecimal((decimal)d)));
            Assert.Equal(TestValue.ConvertDecimal((decimal)e), sql.Val(() => TestValue.ConvertDecimal((decimal)e)));
            Assert.Equal(TestValue.ConvertDecimal((decimal)f), sql.Val(() => TestValue.ConvertDecimal((decimal)f)));
            Assert.Equal(TestValue.ConvertDecimal((decimal)g), sql.Val(() => TestValue.ConvertDecimal((decimal)g)));
            Assert.Equal(TestValue.ConvertDecimal((decimal)h), sql.Val(() => TestValue.ConvertDecimal((decimal)h)));
            Assert.Equal(TestValue.ConvertDecimal((decimal)i), sql.Val(() => TestValue.ConvertDecimal((decimal)i)));
        }

        [Theory]
        [InlineData(LongEnum.MinValue)]
        [InlineData(LongEnum.One)]
        [InlineData(LongEnum.MaxValue)]
        public void Convert_Value_From_Enum(LongEnum value)
        {
            Assert.Equal(TestValue.ConvertChar((char)value), sql.Val(() => TestValue.ConvertChar((char)value)));
            Assert.Equal(TestValue.ConvertSByte((sbyte)value), sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
            Assert.Equal(TestValue.ConvertByte((byte)value), sql.Val(() => TestValue.ConvertByte((byte)value)));
            Assert.Equal(TestValue.ConvertShort((short)value), sql.Val(() => TestValue.ConvertShort((short)value)));
            Assert.Equal(TestValue.ConvertUShort((ushort)value), sql.Val(() => TestValue.ConvertUShort((ushort)value)));
            Assert.Equal(TestValue.ConvertInt((int)value), sql.Val(() => TestValue.ConvertInt((int)value)));
            Assert.Equal(TestValue.ConvertUInt((uint)value), sql.Val(() => TestValue.ConvertUInt((uint)value)));
            Assert.Equal(TestValue.ConvertLong((long)value), sql.Val(() => TestValue.ConvertLong((long)value)));
            Assert.Equal(TestValue.ConvertULong((ulong)value), sql.Val(() => TestValue.ConvertULong((ulong)value)));
            Assert.Equal(TestValue.ConvertFloat((float)value), sql.Val(() => TestValue.ConvertFloat((float)value)));
            Assert.Equal(TestValue.ConvertDouble((double)value), sql.Val(() => TestValue.ConvertDouble((double)value)));
        }

        [Theory]
        [InlineData(LongEnum.One)]
        public void Convert_Value_From_Enum_Checked(LongEnum value)
        {
            checked
            {
                Assert.Equal(TestValue.ConvertChar((char)value), sql.Val(() => TestValue.ConvertChar((char)value)));
                Assert.Equal(TestValue.ConvertSByte((sbyte)value), sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
                Assert.Equal(TestValue.ConvertByte((byte)value), sql.Val(() => TestValue.ConvertByte((byte)value)));
                Assert.Equal(TestValue.ConvertShort((short)value), sql.Val(() => TestValue.ConvertShort((short)value)));
                Assert.Equal(TestValue.ConvertUShort((ushort)value), sql.Val(() => TestValue.ConvertUShort((ushort)value)));
                Assert.Equal(TestValue.ConvertInt((int)value), sql.Val(() => TestValue.ConvertInt((int)value)));
                Assert.Equal(TestValue.ConvertUInt((uint)value), sql.Val(() => TestValue.ConvertUInt((uint)value)));
                Assert.Equal(TestValue.ConvertLong((long)value), sql.Val(() => TestValue.ConvertLong((long)value)));
                Assert.Equal(TestValue.ConvertULong((ulong)value), sql.Val(() => TestValue.ConvertULong((ulong)value)));
                Assert.Equal(TestValue.ConvertFloat((float)value), sql.Val(() => TestValue.ConvertFloat((float)value)));
                Assert.Equal(TestValue.ConvertDouble((double)value), sql.Val(() => TestValue.ConvertDouble((double)value)));
            }
        }

        [Theory]
        [InlineData(LongEnum.MinValue)]
        public void Convert_Value_From_Enum_Checked_Overflow_Min(LongEnum value)
        {
            checked
            {
                Exception ex;
                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertChar((char)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertByte((byte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertShort((short)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertUShort((ushort)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertInt((int)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertUInt((uint)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertULong((ulong)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                Assert.Equal(TestValue.ConvertLong((long)value), sql.Val(() => TestValue.ConvertLong((long)value)));
                Assert.Equal(TestValue.ConvertFloat((float)value), sql.Val(() => TestValue.ConvertFloat((float)value)));
                Assert.Equal(TestValue.ConvertDouble((double)value), sql.Val(() => TestValue.ConvertDouble((double)value)));
            }
        }

        [Theory]
        [InlineData(LongEnum.MaxValue)]
        public void Convert_Value_From_Enum_Checked_Overflow_Max(LongEnum value)
        {
            checked
            {
                Exception ex;
                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertChar((char)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertByte((byte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertShort((short)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertUShort((ushort)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertInt((int)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertUInt((uint)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                Assert.Equal(TestValue.ConvertLong((long)value), sql.Val(() => TestValue.ConvertLong((long)value)));
                Assert.Equal(TestValue.ConvertULong((ulong)value), sql.Val(() => TestValue.ConvertULong((ulong)value)));
                Assert.Equal(TestValue.ConvertFloat((float)value), sql.Val(() => TestValue.ConvertFloat((float)value)));
                Assert.Equal(TestValue.ConvertDouble((double)value), sql.Val(() => TestValue.ConvertDouble((double)value)));
            }
        }

        [Theory]
        [InlineData(LongEnum.MinValue)]
        [InlineData(LongEnum.One)]
        [InlineData(LongEnum.MaxValue)]
        public void Convert_Value_From_Enum_ValueType(LongEnum? value)
        {
            Assert.Equal(TestValue.ConvertChar((char)value), sql.Val(() => TestValue.ConvertChar((char)value)));
            Assert.Equal(TestValue.ConvertSByte((sbyte)value), sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
            Assert.Equal(TestValue.ConvertByte((byte)value), sql.Val(() => TestValue.ConvertByte((byte)value)));
            Assert.Equal(TestValue.ConvertShort((short)value), sql.Val(() => TestValue.ConvertShort((short)value)));
            Assert.Equal(TestValue.ConvertUShort((ushort)value), sql.Val(() => TestValue.ConvertUShort((ushort)value)));
            Assert.Equal(TestValue.ConvertInt((int)value), sql.Val(() => TestValue.ConvertInt((int)value)));
            Assert.Equal(TestValue.ConvertUInt((uint)value), sql.Val(() => TestValue.ConvertUInt((uint)value)));
            Assert.Equal(TestValue.ConvertLong((long)value), sql.Val(() => TestValue.ConvertLong((long)value)));
            Assert.Equal(TestValue.ConvertULong((ulong)value), sql.Val(() => TestValue.ConvertULong((ulong)value)));
            Assert.Equal(TestValue.ConvertFloat((float)value), sql.Val(() => TestValue.ConvertFloat((float)value)));
            Assert.Equal(TestValue.ConvertDouble((double)value), sql.Val(() => TestValue.ConvertDouble((double)value)));
        }

        [Theory]
        [InlineData(LongEnum.One)]
        public void Convert_Value_From_Enum_ValueType_Checked(LongEnum? value)
        {
            checked
            {
                Assert.Equal(TestValue.ConvertChar((char)value), sql.Val(() => TestValue.ConvertChar((char)value)));
                Assert.Equal(TestValue.ConvertSByte((sbyte)value), sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
                Assert.Equal(TestValue.ConvertByte((byte)value), sql.Val(() => TestValue.ConvertByte((byte)value)));
                Assert.Equal(TestValue.ConvertShort((short)value), sql.Val(() => TestValue.ConvertShort((short)value)));
                Assert.Equal(TestValue.ConvertUShort((ushort)value), sql.Val(() => TestValue.ConvertUShort((ushort)value)));
                Assert.Equal(TestValue.ConvertInt((int)value), sql.Val(() => TestValue.ConvertInt((int)value)));
                Assert.Equal(TestValue.ConvertUInt((uint)value), sql.Val(() => TestValue.ConvertUInt((uint)value)));
                Assert.Equal(TestValue.ConvertLong((long)value), sql.Val(() => TestValue.ConvertLong((long)value)));
                Assert.Equal(TestValue.ConvertULong((ulong)value), sql.Val(() => TestValue.ConvertULong((ulong)value)));
                Assert.Equal(TestValue.ConvertFloat((float)value), sql.Val(() => TestValue.ConvertFloat((float)value)));
                Assert.Equal(TestValue.ConvertDouble((double)value), sql.Val(() => TestValue.ConvertDouble((double)value)));
            }
        }

        [Theory]
        [InlineData(LongEnum.MinValue)]
        public void Convert_Value_From_Enum_ValueType_Checked_Overflow_Min(LongEnum? value)
        {
            checked
            {
                Exception ex;
                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertChar((char)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertByte((byte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertShort((short)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertUShort((ushort)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertInt((int)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertUInt((uint)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertULong((ulong)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                Assert.Equal(TestValue.ConvertLong((long)value), sql.Val(() => TestValue.ConvertLong((long)value)));
                Assert.Equal(TestValue.ConvertFloat((float)value), sql.Val(() => TestValue.ConvertFloat((float)value)));
                Assert.Equal(TestValue.ConvertDouble((double)value), sql.Val(() => TestValue.ConvertDouble((double)value)));
            }
        }

        [Theory]
        [InlineData(LongEnum.MaxValue)]
        public void Convert_Value_From_Enum_ValueType_Checked_Overflow_Max(LongEnum? value)
        {
            checked
            {
                Exception ex;
                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertChar((char)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertSByte((sbyte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertByte((byte)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertShort((short)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertUShort((ushort)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertInt((int)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertUInt((uint)value)));
                Assert.IsType<OverflowException>(ex.InnerException);

                Assert.Equal(TestValue.ConvertLong((long)value), sql.Val(() => TestValue.ConvertLong((long)value)));
                Assert.Equal(TestValue.ConvertULong((ulong)value), sql.Val(() => TestValue.ConvertULong((ulong)value)));
                Assert.Equal(TestValue.ConvertFloat((float)value), sql.Val(() => TestValue.ConvertFloat((float)value)));
                Assert.Equal(TestValue.ConvertDouble((double)value), sql.Val(() => TestValue.ConvertDouble((double)value)));
            }
        }

        [Theory]
        [InlineData(char.MinValue, sbyte.MinValue, byte.MinValue, short.MinValue, ushort.MinValue, int.MinValue,
            uint.MinValue, long.MinValue, ulong.MinValue, float.MinValue, double.MinValue)]
        [InlineData((char)1, (sbyte)1, (byte)1, (short)1, (ushort)1, 1, 1u, 1L, 1ul, 1f, 1d)]
        [InlineData(char.MaxValue, sbyte.MaxValue, byte.MaxValue, short.MaxValue, ushort.MaxValue, int.MaxValue,
            uint.MaxValue, long.MaxValue, ulong.MaxValue, float.MaxValue, double.MaxValue)]
        public void Convert_Value_To_Enum(char a, sbyte b, byte c, short d, ushort e, int f, uint g,
            long h, ulong i, float j, double k)
        {
            Assert.Equal(TestValue.ConvertEnum((ByteEnum)a), sql.Val(() => TestValue.ConvertEnum((ByteEnum)a)));
            Assert.Equal(TestValue.ConvertEnum((ByteEnum)b), sql.Val(() => TestValue.ConvertEnum((ByteEnum)b)));
            Assert.Equal(TestValue.ConvertEnum((ByteEnum)c), sql.Val(() => TestValue.ConvertEnum((ByteEnum)c)));
            Assert.Equal(TestValue.ConvertEnum((ByteEnum)d), sql.Val(() => TestValue.ConvertEnum((ByteEnum)d)));
            Assert.Equal(TestValue.ConvertEnum((ByteEnum)e), sql.Val(() => TestValue.ConvertEnum((ByteEnum)e)));
            Assert.Equal(TestValue.ConvertEnum((ByteEnum)f), sql.Val(() => TestValue.ConvertEnum((ByteEnum)f)));
            Assert.Equal(TestValue.ConvertEnum((ByteEnum)g), sql.Val(() => TestValue.ConvertEnum((ByteEnum)g)));
            Assert.Equal(TestValue.ConvertEnum((ByteEnum)h), sql.Val(() => TestValue.ConvertEnum((ByteEnum)h)));
            Assert.Equal(TestValue.ConvertEnum((ByteEnum)i), sql.Val(() => TestValue.ConvertEnum((ByteEnum)i)));
            Assert.Equal(TestValue.ConvertEnum((ByteEnum)j), sql.Val(() => TestValue.ConvertEnum((ByteEnum)j)));
            Assert.Equal(TestValue.ConvertEnum((ByteEnum)k), sql.Val(() => TestValue.ConvertEnum((ByteEnum)k)));
        }

        [Theory]
        [InlineData((char)1, (sbyte)1, (byte)1, (short)1, (ushort)1, 1, 1u, 1L, 1ul, 1f, 1d)]
        public void Convert_Value_To_Enum_Checked(char a, sbyte b, byte c, short d, ushort e, int f, uint g,
            long h, ulong i, float j, double k)
        {
            checked
            {
                Assert.Equal(TestValue.ConvertEnum((ByteEnum)a), sql.Val(() => TestValue.ConvertEnum((ByteEnum)a)));
                Assert.Equal(TestValue.ConvertEnum((ByteEnum)b), sql.Val(() => TestValue.ConvertEnum((ByteEnum)b)));
                Assert.Equal(TestValue.ConvertEnum((ByteEnum)c), sql.Val(() => TestValue.ConvertEnum((ByteEnum)c)));
                Assert.Equal(TestValue.ConvertEnum((ByteEnum)d), sql.Val(() => TestValue.ConvertEnum((ByteEnum)d)));
                Assert.Equal(TestValue.ConvertEnum((ByteEnum)e), sql.Val(() => TestValue.ConvertEnum((ByteEnum)e)));
                Assert.Equal(TestValue.ConvertEnum((ByteEnum)f), sql.Val(() => TestValue.ConvertEnum((ByteEnum)f)));
                Assert.Equal(TestValue.ConvertEnum((ByteEnum)g), sql.Val(() => TestValue.ConvertEnum((ByteEnum)g)));
                Assert.Equal(TestValue.ConvertEnum((ByteEnum)h), sql.Val(() => TestValue.ConvertEnum((ByteEnum)h)));
                Assert.Equal(TestValue.ConvertEnum((ByteEnum)i), sql.Val(() => TestValue.ConvertEnum((ByteEnum)i)));
                Assert.Equal(TestValue.ConvertEnum((ByteEnum)j), sql.Val(() => TestValue.ConvertEnum((ByteEnum)j)));
                Assert.Equal(TestValue.ConvertEnum((ByteEnum)k), sql.Val(() => TestValue.ConvertEnum((ByteEnum)k)));
            }
        }

        [Theory]
        [InlineData(char.MinValue, sbyte.MinValue, byte.MinValue, short.MinValue, ushort.MinValue, int.MinValue,
            uint.MinValue, long.MinValue, ulong.MinValue, float.MinValue, double.MinValue)]
        public void Convert_Value_To_Enum_Checked_Overflow_Min(char a, sbyte b, byte c, short d, ushort e, int f, uint g,
            long h, ulong i, float j, double k)
        {
            checked
            {
                Exception ex;
                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertEnum((ByteEnum)b)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertEnum((ByteEnum)d)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertEnum((ByteEnum)f)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertEnum((ByteEnum)h)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertEnum((ByteEnum)j)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertEnum((ByteEnum)k)));
                Assert.IsType<OverflowException>(ex.InnerException);

                Assert.Equal(TestValue.ConvertEnum((ByteEnum)a), sql.Val(() => TestValue.ConvertEnum((ByteEnum)a)));
                Assert.Equal(TestValue.ConvertEnum((ByteEnum)c), sql.Val(() => TestValue.ConvertEnum((ByteEnum)c)));
                Assert.Equal(TestValue.ConvertEnum((ByteEnum)e), sql.Val(() => TestValue.ConvertEnum((ByteEnum)e)));
                Assert.Equal(TestValue.ConvertEnum((ByteEnum)g), sql.Val(() => TestValue.ConvertEnum((ByteEnum)g)));
                Assert.Equal(TestValue.ConvertEnum((ByteEnum)i), sql.Val(() => TestValue.ConvertEnum((ByteEnum)i)));
            }
        }

        [Theory]
        [InlineData(char.MaxValue, sbyte.MaxValue, byte.MaxValue, short.MaxValue, ushort.MaxValue, int.MaxValue,
             uint.MaxValue, long.MaxValue, ulong.MaxValue, float.MaxValue, double.MaxValue)]
        public void Convert_Value_To_Enum_Checked_Overflow_Max(char a, sbyte b, byte c, short d, ushort e, int f, uint g,
            long h, ulong i, float j, double k)
        {
            {
                checked
                {
                    Exception ex;
                    ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertEnum((ByteEnum)a)));
                    Assert.IsType<OverflowException>(ex.InnerException);

                    ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertEnum((ByteEnum)d)));
                    Assert.IsType<OverflowException>(ex.InnerException);

                    ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertEnum((ByteEnum)e)));
                    Assert.IsType<OverflowException>(ex.InnerException);

                    ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertEnum((ByteEnum)f)));
                    Assert.IsType<OverflowException>(ex.InnerException);

                    ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertEnum((ByteEnum)g)));
                    Assert.IsType<OverflowException>(ex.InnerException);

                    ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertEnum((ByteEnum)h)));
                    Assert.IsType<OverflowException>(ex.InnerException);

                    ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertEnum((ByteEnum)i)));
                    Assert.IsType<OverflowException>(ex.InnerException);

                    ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertEnum((ByteEnum)j)));
                    Assert.IsType<OverflowException>(ex.InnerException);

                    ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertEnum((ByteEnum)k)));
                    Assert.IsType<OverflowException>(ex.InnerException);

                    Assert.Equal(TestValue.ConvertEnum((ByteEnum)b), sql.Val(() => TestValue.ConvertEnum((ByteEnum)b)));
                    Assert.Equal(TestValue.ConvertEnum((ByteEnum)c), sql.Val(() => TestValue.ConvertEnum((ByteEnum)c)));
                }
            }
        }

        [Theory]
        [InlineData(char.MinValue, sbyte.MinValue, byte.MinValue, short.MinValue, ushort.MinValue, int.MinValue,
            uint.MinValue, long.MinValue, ulong.MinValue, float.MinValue, double.MinValue)]
        [InlineData((char)1, (sbyte)1, (byte)1, (short)1, (ushort)1, 1, 1u, 1L, 1ul, 1f, 1d)]
        [InlineData(char.MaxValue, sbyte.MaxValue, byte.MaxValue, short.MaxValue, ushort.MaxValue, int.MaxValue,
            uint.MaxValue, long.MaxValue, ulong.MaxValue, float.MaxValue, double.MaxValue)]
        public void Convert_Value_To_Enum_ValueType(char? a, sbyte? b, byte? c, short? d, ushort? e, int? f, uint? g,
            long? h, ulong? i, float? j, double? k)
        {
            Assert.Equal(TestValue.ConvertEnum((ByteEnum)a), sql.Val(() => TestValue.ConvertEnum((ByteEnum)a)));
            Assert.Equal(TestValue.ConvertEnum((ByteEnum)b), sql.Val(() => TestValue.ConvertEnum((ByteEnum)b)));
            Assert.Equal(TestValue.ConvertEnum((ByteEnum)c), sql.Val(() => TestValue.ConvertEnum((ByteEnum)c)));
            Assert.Equal(TestValue.ConvertEnum((ByteEnum)d), sql.Val(() => TestValue.ConvertEnum((ByteEnum)d)));
            Assert.Equal(TestValue.ConvertEnum((ByteEnum)e), sql.Val(() => TestValue.ConvertEnum((ByteEnum)e)));
            Assert.Equal(TestValue.ConvertEnum((ByteEnum)f), sql.Val(() => TestValue.ConvertEnum((ByteEnum)f)));
            Assert.Equal(TestValue.ConvertEnum((ByteEnum)g), sql.Val(() => TestValue.ConvertEnum((ByteEnum)g)));
            Assert.Equal(TestValue.ConvertEnum((ByteEnum)h), sql.Val(() => TestValue.ConvertEnum((ByteEnum)h)));
            Assert.Equal(TestValue.ConvertEnum((ByteEnum)i), sql.Val(() => TestValue.ConvertEnum((ByteEnum)i)));
            Assert.Equal(TestValue.ConvertEnum((ByteEnum)j), sql.Val(() => TestValue.ConvertEnum((ByteEnum)j)));
            Assert.Equal(TestValue.ConvertEnum((ByteEnum)k), sql.Val(() => TestValue.ConvertEnum((ByteEnum)k)));
        }

        [Theory]
        [InlineData((char)1, (sbyte)1, (byte)1, (short)1, (ushort)1, 1, 1u, 1L, 1ul, 1f, 1d)]
        public void Convert_Value_To_Enum_ValueType_Checked(char? a, sbyte? b, byte? c, short? d, ushort? e, int? f,
            uint? g, long? h, ulong? i, float? j, double? k)
        {
            checked
            {
                Assert.Equal(TestValue.ConvertEnum((ByteEnum)a), sql.Val(() => TestValue.ConvertEnum((ByteEnum)a)));
                Assert.Equal(TestValue.ConvertEnum((ByteEnum)b), sql.Val(() => TestValue.ConvertEnum((ByteEnum)b)));
                Assert.Equal(TestValue.ConvertEnum((ByteEnum)c), sql.Val(() => TestValue.ConvertEnum((ByteEnum)c)));
                Assert.Equal(TestValue.ConvertEnum((ByteEnum)d), sql.Val(() => TestValue.ConvertEnum((ByteEnum)d)));
                Assert.Equal(TestValue.ConvertEnum((ByteEnum)e), sql.Val(() => TestValue.ConvertEnum((ByteEnum)e)));
                Assert.Equal(TestValue.ConvertEnum((ByteEnum)f), sql.Val(() => TestValue.ConvertEnum((ByteEnum)f)));
                Assert.Equal(TestValue.ConvertEnum((ByteEnum)g), sql.Val(() => TestValue.ConvertEnum((ByteEnum)g)));
                Assert.Equal(TestValue.ConvertEnum((ByteEnum)h), sql.Val(() => TestValue.ConvertEnum((ByteEnum)h)));
                Assert.Equal(TestValue.ConvertEnum((ByteEnum)i), sql.Val(() => TestValue.ConvertEnum((ByteEnum)i)));
                Assert.Equal(TestValue.ConvertEnum((ByteEnum)j), sql.Val(() => TestValue.ConvertEnum((ByteEnum)j)));
                Assert.Equal(TestValue.ConvertEnum((ByteEnum)k), sql.Val(() => TestValue.ConvertEnum((ByteEnum)k)));
            }
        }

        [Theory]
        [InlineData(char.MinValue, sbyte.MinValue, byte.MinValue, short.MinValue, ushort.MinValue, int.MinValue,
            uint.MinValue, long.MinValue, ulong.MinValue, float.MinValue, double.MinValue)]
        public void Convert_Value_To_Enum_ValueType_Checked_Overflow_Min(char? a, sbyte? b, byte? c, short? d, ushort? e,
            int? f, uint? g, long? h, ulong? i, float? j, double? k)
        {
            checked
            {
                Exception ex;
                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertEnum((ByteEnum)b)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertEnum((ByteEnum)d)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertEnum((ByteEnum)f)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertEnum((ByteEnum)h)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertEnum((ByteEnum)j)));
                Assert.IsType<OverflowException>(ex.InnerException);

                ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertEnum((ByteEnum)k)));
                Assert.IsType<OverflowException>(ex.InnerException);

                Assert.Equal(TestValue.ConvertEnum((ByteEnum)a), sql.Val(() => TestValue.ConvertEnum((ByteEnum)a)));
                Assert.Equal(TestValue.ConvertEnum((ByteEnum)c), sql.Val(() => TestValue.ConvertEnum((ByteEnum)c)));
                Assert.Equal(TestValue.ConvertEnum((ByteEnum)e), sql.Val(() => TestValue.ConvertEnum((ByteEnum)e)));
                Assert.Equal(TestValue.ConvertEnum((ByteEnum)g), sql.Val(() => TestValue.ConvertEnum((ByteEnum)g)));
                Assert.Equal(TestValue.ConvertEnum((ByteEnum)i), sql.Val(() => TestValue.ConvertEnum((ByteEnum)i)));
            }
        }

        [Theory]
        [InlineData(char.MaxValue, sbyte.MaxValue, byte.MaxValue, short.MaxValue, ushort.MaxValue, int.MaxValue,
             uint.MaxValue, long.MaxValue, ulong.MaxValue, float.MaxValue, double.MaxValue)]
        public void Convert_Value_To_Enum_ValueType_Checked_Overflow_Max(char? a, sbyte? b, byte? c, short? d, ushort? e,
            int? f, uint? g, long? h, ulong? i, float? j, double? k)
        {
            {
                checked
                {
                    Exception ex;
                    ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertEnum((ByteEnum)a)));
                    Assert.IsType<OverflowException>(ex.InnerException);

                    ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertEnum((ByteEnum)d)));
                    Assert.IsType<OverflowException>(ex.InnerException);

                    ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertEnum((ByteEnum)e)));
                    Assert.IsType<OverflowException>(ex.InnerException);

                    ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertEnum((ByteEnum)f)));
                    Assert.IsType<OverflowException>(ex.InnerException);

                    ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertEnum((ByteEnum)g)));
                    Assert.IsType<OverflowException>(ex.InnerException);

                    ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertEnum((ByteEnum)h)));
                    Assert.IsType<OverflowException>(ex.InnerException);

                    ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertEnum((ByteEnum)i)));
                    Assert.IsType<OverflowException>(ex.InnerException);

                    ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertEnum((ByteEnum)j)));
                    Assert.IsType<OverflowException>(ex.InnerException);

                    ex = Assert.Throws<TargetInvocationException>(() => sql.Val(() => TestValue.ConvertEnum((ByteEnum)k)));
                    Assert.IsType<OverflowException>(ex.InnerException);

                    Assert.Equal(TestValue.ConvertEnum((ByteEnum)b), sql.Val(() => TestValue.ConvertEnum((ByteEnum)b)));
                    Assert.Equal(TestValue.ConvertEnum((ByteEnum)c), sql.Val(() => TestValue.ConvertEnum((ByteEnum)c)));
                }
            }
        }

        [Theory]
        [InlineData((char)1, (sbyte)1, (byte)1, (short)1, (ushort)1, 1, 1u, 1L, 1ul, 1f, 1d, 1, ByteEnum.One)]
        public void Convert_Value_Nullable(char a, sbyte b, byte c, short d, ushort e, int f, uint g, long h,
                ulong i, float j, double k, decimal l, ByteEnum m)
        {
            Assert.Equal(TestValue.ConvertCharNullable(a), sql.Val(() => TestValue.ConvertCharNullable(a)));
            Assert.Equal(TestValue.ConvertSByteNullable(b), sql.Val(() => TestValue.ConvertSByteNullable(b)));
            Assert.Equal(TestValue.ConvertByteNullable(c), sql.Val(() => TestValue.ConvertByteNullable(c)));
            Assert.Equal(TestValue.ConvertShortNullable(d), sql.Val(() => TestValue.ConvertShortNullable(d)));
            Assert.Equal(TestValue.ConvertUShortNullable(e), sql.Val(() => TestValue.ConvertUShortNullable(e)));
            Assert.Equal(TestValue.ConvertIntNullable(f), sql.Val(() => TestValue.ConvertIntNullable(f)));
            Assert.Equal(TestValue.ConvertUIntNullable(g), sql.Val(() => TestValue.ConvertUIntNullable(g)));
            Assert.Equal(TestValue.ConvertLongNullable(h), sql.Val(() => TestValue.ConvertLongNullable(h)));
            Assert.Equal(TestValue.ConvertULongNullable(i), sql.Val(() => TestValue.ConvertULongNullable(i)));
            Assert.Equal(TestValue.ConvertFloatNullable(j), sql.Val(() => TestValue.ConvertFloatNullable(j)));
            Assert.Equal(TestValue.ConvertDoubleNullable(k), sql.Val(() => TestValue.ConvertDoubleNullable(k)));
            Assert.Equal(TestValue.ConvertDecimalNullable(l), sql.Val(() => TestValue.ConvertDecimalNullable(l)));
            Assert.Equal(TestValue.ConvertEnumNullable(m), sql.Val(() => TestValue.ConvertEnumNullable(m)));
        }

        public class TestValue
        {
            public static char ConvertChar(char value) => value;

            public static sbyte ConvertSByte(sbyte value) => value;

            public static byte ConvertByte(byte value) => value;

            public static short ConvertShort(short value) => value;

            public static ushort ConvertUShort(ushort value) => value;

            public static int ConvertInt(int value) => value;

            public static uint ConvertUInt(uint value) => value;

            public static long ConvertLong(long value) => value;

            public static ulong ConvertULong(ulong value) => value;

            public static float ConvertFloat(float value) => value;

            public static double ConvertDouble(double value) => value;

            public static decimal ConvertDecimal(decimal value) => value;

            public static ByteEnum ConvertEnum(ByteEnum value) => value;

            public static char? ConvertCharNullable(char? value) => value;

            public static sbyte? ConvertSByteNullable(sbyte? value) => value;

            public static byte? ConvertByteNullable(byte? value) => value;

            public static short? ConvertShortNullable(short? value) => value;

            public static ushort? ConvertUShortNullable(ushort? value) => value;

            public static int? ConvertIntNullable(int? value) => value;

            public static uint? ConvertUIntNullable(uint? value) => value;

            public static long? ConvertLongNullable(long? value) => value;

            public static ulong? ConvertULongNullable(ulong? value) => value;

            public static float? ConvertFloatNullable(float? value) => value;

            public static double? ConvertDoubleNullable(double? value) => value;

            public static decimal? ConvertDecimalNullable(decimal? value) => value;

            public static ByteEnum? ConvertEnumNullable(ByteEnum? value) => value;
        }

        public enum ByteEnum : byte
        {
            One = 1,
        }

        public enum LongEnum : long
        {
            MinValue = long.MinValue,
            One = 1,
            MaxValue = long.MaxValue
        }
    }
}