using System;
using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Engines;
using Suilder.Reflection.Builder;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test
{
    [Collection("SqlBuilder")]
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

        public static IEnumerable<object[]> DataObject
        {
            get
            {
                return new List<object[]>
                {
                    new object[] { true },
                    new object[] { false },
                    new object[] { 1 },
                    new object[] { 'a' },
                    new object[] { "abcd" },
                    new object[] { new DateTime(2000, 1, 1) }
                };
            }
        }

        public static IEnumerable<object[]> DataArray
        {
            get
            {
                return new List<object[]>
                {
                    new object[] { new byte[] { 1, 2, 3 } },
                    new object[] { new int[] { 4, 5, 6 } },
                    new object[] { new char[] { 'a', 'b', 'c' } },
                    new object[] { new string[] { "abcd", "efgh", "ijkl" } }
                };
            }
        }

        public static IEnumerable<object[]> DataInt
        {
            get
            {
                return new List<object[]>
                {
                    new object[] { 1 },
                    new object[] { 2 },
                    new object[] { 3 }
                };
            }
        }

        public static IEnumerable<object[]> DataString
        {
            get
            {
                return new List<object[]>
                {
                    new object[] { "abcd" },
                    new object[] { "efgh" },
                    new object[] { "ijkl" }
                };
            }
        }

        public static IEnumerable<object[]> DataBool
        {
            get
            {
                return new List<object[]>
                {
                    new object[] { true },
                    new object[] { false }
                };
            }
        }

        public static IEnumerable<object[]> DataIntArray
        {
            get
            {
                return new List<object[]>
                {
                    new object[] { new int[] { 1, 2, 3 } },
                    new object[] { new int[] { 4, 5, 6 } },
                    new object[] { new int[] { 7, 8, 9 } }
                };
            }
        }

        public static IEnumerable<object[]> DataStringArray
        {
            get
            {
                return new List<object[]>
                {
                    new object[] { new string[] { "ab", "cd", "ef" } },
                    new object[] { new string[] { "gh", "ij", "kl" } },
                    new object[] { new string[] { "mn", "op", "qr" } }
                };
            }
        }

        public static IEnumerable<object[]> DataByteArray
        {
            get
            {
                return new List<object[]>
                {
                    new object[] { new byte[] { 1, 2, 3 } },
                    new object[] { new byte[] { 4, 5, 6 } },
                    new object[] { new byte[] { 7, 8, 9 } }
                };
            }
        }

        public static IEnumerable<object[]> DataObjectAll
        {
            get
            {
                return new List<object[]>
                {
                    new object[] { true },
                    new object[] { false },
                    new object[] { (byte)1 },
                    new object[] { (sbyte)1 },
                    new object[] { (short)1 },
                    new object[] { (ushort)1 },
                    new object[] { 1 },
                    new object[] { (uint)1 },
                    new object[] { 1L },
                    new object[] { (ulong)1 },
                    new object[] { 1.5f },
                    new object[] { 1.5d },
                    new object[] { 1.5m },
                    new object[] { 'a' },
                    new object[] { "abcd" },
                    new object[] { new DateTime(2000, 1, 1) }
                };
            }
        }

        public static IEnumerable<object[]> DataArrayAll
        {
            get
            {
                return new List<object[]>
                {
                    new object[] { new bool[] { true, false, true } },
                    new object[] { new byte[] { 1, 2, 3 } },
                    new object[] { new sbyte[] { 1, 2, 3 } },
                    new object[] { new short[] { 1, 2, 3 } },
                    new object[] { new ushort[] { 1, 2, 3 } },
                    new object[] { new int[] { 1, 2, 3 } },
                    new object[] { new uint[] { 1, 2, 3 } },
                    new object[] { new ulong[] { 1, 2, 3 } },
                    new object[] { new float[] { 1.5f, 2.5f, 3.5f } },
                    new object[] { new double[] { 1.5, 2.5, 3.5 } },
                    new object[] { new decimal[] { 1.5m, 2.5m, 3.5m } },
                    new object[] { new char[] { 'a', 'b', 'c' } },
                    new object[] { new string[] { "abcd", "efgh", "ijkl" } }
                };
            }
        }
    }
}