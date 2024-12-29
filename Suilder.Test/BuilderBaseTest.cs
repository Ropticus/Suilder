using System;
using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Engines;
using Suilder.Reflection.Builder;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test
{
    public abstract class BuilderBaseTest
    {
        protected IEngine engine;

        protected ISqlBuilder sql;

        public BuilderBaseTest()
        {
            sql = SqlBuilder.Instance;

            engine = GetEngine(GetTableBuilder());
        }

        public virtual ITableBuilder GetTableBuilder()
        {
            ITableBuilder tableBuilder = new TableBuilder();
            tableBuilder.Add<Person>();
            tableBuilder.Add<Department>();

            tableBuilder.Add<Person2>();
            tableBuilder.Add<Department2>();

            return tableBuilder;
        }

        public virtual IEngine GetEngine(ITableBuilder tableBuilder)
        {
            return new Engine(tableBuilder);
        }

        public static IEnumerable<object[]> DataObject => new object[][]
        {
            new object[] { true },
            new object[] { false },
            new object[] { 1 },
            new object[] { 'a' },
            new object[] { "abcd" },
            new object[] { new DateTime(2001, 1, 1) },
            new object[] { PersonFlags.ValueA }
        };

        public static IEnumerable<object[]> DataArray => new object[][]
        {
            new object[] { new byte[] { 1, 2, 3 } },
            new object[] { new int[] { 4, 5, 6 } },
            new object[] { new char[] { 'a', 'b', 'c' } },
            new object[] { new string[] { "abcd", "efgh", "ijkl" } }
        };

        public static IEnumerable<object[]> DataList => new object[][]
        {
            new object[] { new List<byte> { 1, 2, 3 } },
            new object[] { new List<int> { 4, 5, 6 } },
            new object[] { new List<char> { 'a', 'b', 'c' } },
            new object[] { new List<string> { "abcd", "efgh", "ijkl" } }
        };

        public static TheoryData<bool> DataBool => new TheoryData<bool>
        {
            true,
            false
        };

        public static TheoryData<int> DataInt => new TheoryData<int>
        {
            1,
            2,
            3
        };

        public static TheoryData<uint> DataUInt => new TheoryData<uint>
        {
            1u,
            2u,
            3u
        };

        public static TheoryData<long> DataLong => new TheoryData<long>
        {
            1L,
            2L,
            3L
        };

        public static TheoryData<decimal> DataDecimal => new TheoryData<decimal>
        {
            1.5m,
            2.5m,
            3.5m
        };

        public static TheoryData<string> DataString => new TheoryData<string>
        {
            "abcd",
            "efgh",
            "ijkl"
        };

        public static TheoryData<PersonFlags> DataEnum => new TheoryData<PersonFlags>
        {
            PersonFlags.ValueA,
            PersonFlags.ValueB,
            PersonFlags.ValueC
        };

        public static TheoryData<byte[]> DataByteArray => new TheoryData<byte[]>
        {
            new byte[] { 1, 2, 3 },
            new byte[] { 4, 5, 6 },
            new byte[] { 7, 8, 9 }
        };

        public static TheoryData<int[]> DataIntArray => new TheoryData<int[]>
        {
            new int[] { 1, 2, 3 },
            new int[] { 4, 5, 6 },
            new int[] { 7, 8, 9 }
        };

        public static TheoryData<string[]> DataStringArray => new TheoryData<string[]>
        {

            new string[] { "ab", "cd", "ef" },
            new string[] { "gh", "ij", "kl" },
            new string[] { "mn", "op", "qr" }
        };

        public static TheoryData<List<string>> DataStringList => new TheoryData<List<string>>
        {
            new List<string> { "ab", "cd", "ef" },
            new List<string> { "gh", "ij", "kl" },
            new List<string> { "mn", "op", "qr" }
        };

        public static IEnumerable<object[]> DataObjectAll => new object[][]
        {
            new object[] { true },
            new object[] { false },
            new object[] { (byte)1 },
            new object[] { (sbyte)1 },
            new object[] { (short)1 },
            new object[] { (ushort)1 },
            new object[] { 1 },
            new object[] { 1u },
            new object[] { 1L },
            new object[] { 1ul },
            new object[] { 1.5f },
            new object[] { 1.5d },
            new object[] { 1.5m },
            new object[] { 'a' },
            new object[] { "abcd" },
            new object[] { new TimeSpan(1, 1, 1) },
            new object[] { new TimeOnly(1, 1, 1) },
            new object[] { new DateOnly(2001, 1, 1) },
            new object[] { new DateTime(2001, 1, 1) },
            new object[] { new DateTimeOffset(2001, 1, 1, 0, 0, 0, new TimeSpan(1, 0, 0)) },
            new object[] { PersonFlags.ValueA },
            new object[] { new Guid("11111111-1111-1111-1111-111111111111") }
        };

        public static IEnumerable<object[]> DataArrayAll => new object[][]
        {
            new object[] { new bool[] { true, false, true } },
            new object[] { new byte[] { 1, 2, 3 } },
            new object[] { new sbyte[] { 1, 2, 3 } },
            new object[] { new short[] { 1, 2, 3 } },
            new object[] { new ushort[] { 1, 2, 3 } },
            new object[] { new int[] { 1, 2, 3 } },
            new object[] { new uint[] { 1, 2, 3 } },
            new object[] { new long[] { 1, 2, 3 } },
            new object[] { new ulong[] { 1, 2, 3 } },
            new object[] { new float[] { 1.5f, 2.5f, 3.5f } },
            new object[] { new double[] { 1.5, 2.5, 3.5 } },
            new object[] { new decimal[] { 1.5m, 2.5m, 3.5m } },
            new object[] { new char[] { 'a', 'b', 'c' } },
            new object[] { new string[] { "abcd", "efgh", "ijkl" } },
            new object[] { new TimeSpan[] { new TimeSpan(1, 1, 1), new TimeSpan(2, 2, 2), new TimeSpan(3, 3, 3) } },
            new object[] { new TimeOnly[] { new TimeOnly(1, 1, 1), new TimeOnly(2, 2, 2), new TimeOnly(3, 3, 3) } },
            new object[] { new DateOnly[] { new DateOnly(2001, 1, 1), new DateOnly(2002, 2, 2),
                new DateOnly(2003, 3, 3) } },
            new object[] { new DateTime[] { new DateTime(2001, 1, 1), new DateTime(2002, 2, 2),
                new DateTime(2003, 3, 3) } },
            new object[] { new DateTimeOffset[] { new DateTimeOffset(2001, 1, 1, 0, 0, 0, new TimeSpan(1, 0, 0)),
                new DateTimeOffset(2002, 2, 2, 0, 0, 0, new TimeSpan(2, 0, 0)),
                new DateTimeOffset(2003, 3, 3, 0, 0, 0, new TimeSpan(3, 0, 0)) } },
            new object[] { new PersonFlags[] { PersonFlags.ValueA, PersonFlags.ValueB, PersonFlags.ValueC } },
            new object[] { new Guid[] { new Guid("11111111-1111-1111-1111-111111111111"),
                new Guid("22222222-2222-2222-2222-222222222222"),
                new Guid("33333333-3333-3333-3333-333333333333") } }
        };

        public static IEnumerable<object[]> DataListAll => new object[][]
        {
            new object[] { new List<bool> { true, false, true } },
            new object[] { new List<byte> { 1, 2, 3 } },
            new object[] { new List<sbyte> { 1, 2, 3 } },
            new object[] { new List<short> { 1, 2, 3 } },
            new object[] { new List<ushort> { 1, 2, 3 } },
            new object[] { new List<int> { 1, 2, 3 } },
            new object[] { new List<uint> { 1, 2, 3 } },
            new object[] { new List<long> { 1, 2, 3 } },
            new object[] { new List<ulong> { 1, 2, 3 } },
            new object[] { new List<float> { 1.5f, 2.5f, 3.5f } },
            new object[] { new List<double> { 1.5, 2.5, 3.5 } },
            new object[] { new List<decimal> { 1.5m, 2.5m, 3.5m } },
            new object[] { new List<char> { 'a', 'b', 'c' } },
            new object[] { new List<string> { "abcd", "efgh", "ijkl" } },
            new object[] { new List<TimeSpan> { new TimeSpan(1, 1, 1), new TimeSpan(2, 2, 2),
                new TimeSpan(3, 3, 3) } },
            new object[] { new List<TimeOnly> { new TimeOnly(1, 1, 1), new TimeOnly(2, 2, 2),
                new TimeOnly(3, 3, 3) } },
            new object[] { new List<DateOnly> { new DateOnly(2001, 1, 1), new DateOnly(2002, 2, 2),
                new DateOnly(2003, 3, 3) } },
            new object[] { new List<DateTime> { new DateTime(2001, 1, 1), new DateTime(2002, 2, 2),
                new DateTime(2003, 3, 3) } },
            new object[] { new List<DateTimeOffset> { new DateTimeOffset(2001, 1, 1, 0, 0, 0, new TimeSpan(1, 0, 0)),
                new DateTimeOffset(2002, 2, 2, 0, 0, 0, new TimeSpan(2, 0, 0)),
                new DateTimeOffset(2003, 3, 3, 0, 0, 0, new TimeSpan(3, 0, 0)) } },
            new object[] { new List<PersonFlags> { PersonFlags.ValueA, PersonFlags.ValueB, PersonFlags.ValueC } },
            new object[] { new List<Guid> { new Guid("11111111-1111-1111-1111-111111111111"),
                new Guid("22222222-2222-2222-2222-222222222222"),
                new Guid("33333333-3333-3333-3333-333333333333") } }
        };

        public static IEnumerable<object[]> DataObject2 => new object[][]
        {
            new object[] { true, false },
            new object[] { false, true },
            new object[] { 1, 2 },
            new object[] { 'a', 'b' },
            new object[] { "abcd", "efgh" },
            new object[] { new DateTime(2001, 1, 1), new DateTime(2002, 2, 2) },
            new object[] { PersonFlags.ValueA, PersonFlags.ValueB }
        };

        public static IEnumerable<object[]> DataArray2 => new object[][]
        {
            new object[] { new byte[] { 1, 2, 3 }, new byte[] { 4, 5, 6 } },
            new object[] { new int[] { 4, 5, 6 }, new int[] { 7, 8, 9 } },
            new object[] { new char[] { 'a', 'b', 'c' }, new char[] { 'd', 'e', 'f' } },
            new object[] { new string[] { "abcd", "efgh", "ijkl" }, new string[] { "mnop", "qrst", "uvwx" } }
        };

        public static IEnumerable<object[]> DataList2 => new object[][]
        {
            new object[] { new List<byte> { 1, 2, 3 }, new List<byte> { 1, 2, 3 } },
            new object[] { new List<int> { 4, 5, 6 }, new List<int> { 1, 2, 3 } },
            new object[] { new List<char> { 'a', 'b', 'c' }, new List<char> { 'd', 'e', 'f' } },
            new object[] { new List<string> { "abcd", "efgh", "ijkl" }, new List<string> { "mnop", "qrst", "uvwx" } }
        };

        public static TheoryData<decimal> DataDecimalOne => new TheoryData<decimal>
        {
            1m
        };

        public static TheoryData<decimal> DataDecimalMinMax => new TheoryData<decimal>
        {
            decimal.MinValue,
            decimal.MaxValue
        };

        public static TheoryData<decimal?> DataDecimalOneNullable => new TheoryData<decimal?>
        {
            (decimal?)1m
        };

        public static TheoryData<decimal?> DataDecimalMinMaxNullable => new TheoryData<decimal?>
        {
            (decimal?)decimal.MinValue,
            (decimal?)decimal.MaxValue
        };
    }
}