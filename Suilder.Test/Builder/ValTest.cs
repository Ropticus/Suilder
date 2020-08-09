using System;
using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Functions;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder
{
    public class ValTest : BuilderBaseTest
    {
        [Fact]
        public void Column_All()
        {
            Person person = null;
            IColumn column = (IColumn)sql.Val(() => person);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\", \"person\".\"Active\", \"person\".\"Name\", \"person\".\"SurName\", "
                + "\"person\".\"AddressStreet\", \"person\".\"AddressNumber\", \"person\".\"AddressCity\", "
                + "\"person\".\"Salary\", \"person\".\"DateCreated\", \"person\".\"DepartmentId\", \"person\".\"Image\"",
                result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Column()
        {
            Person person = null;
            IColumn column = (IColumn)sql.Val(() => person.Id);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Column_ForeignKey()
        {
            Person person = null;
            IColumn column = (IColumn)sql.Val(() => person.Department.Id);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"DepartmentId\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Column_Nested()
        {
            Person person = null;
            IColumn column = (IColumn)sql.Val(() => person.Address.Street);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"AddressStreet\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Column_Nested_Deep()
        {
            Person2 person = null;
            IColumn column = (IColumn)sql.Val(() => person.Address.City.Country.Name);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"AddressCityCountryName\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Local_Value()
        {
            int value = 1;
            Assert.Equal(value, sql.Val(() => value));
        }

        [Fact]
        public void Local_Value_Null()
        {
            string value = null;
            Assert.Null(sql.Val(() => value));
        }

        [Fact]
        public void Local_Value_Array()
        {
            byte[] value = new byte[] { 1, 2, 3 };
            Assert.Equal(value, sql.Val(() => value));
        }

        [Fact]
        public void Local_Value_Array_Index()
        {
            int[] value = new int[] { 1, 2, 3 };
            Assert.Equal(value[1], sql.Val(() => value[1]));
        }

        [Fact]
        public void Local_Value_List()
        {
            List<int> value = new List<int> { 1, 2, 3 };
            Assert.Equal(value, sql.Val(() => value));
        }

        [Fact]
        public void Local_Value_List_Index()
        {
            List<int> value = new List<int> { 1, 2, 3 };
            Assert.Equal(value[1], sql.Val(() => value[1]));
        }

        [Fact]
        public void Local_Value_Array_Nested_Index()
        {
            int[][] value = new int[][] { new int[] { 1, 2, 3 }, new int[] { 4, 5, 6 } };
            Assert.Equal(value[1][2], sql.Val(() => value[1][2]));
        }

        [Fact]
        public void Local_Value_Array_Multi_Index()
        {
            int[,] value = new int[,] { { 1, 2, 3 }, { 4, 5, 6 } };
            Assert.Equal(value[1, 2], sql.Val(() => value[1, 2]));
        }

        [Fact]
        public void Local_Value_Dic()
        {
            Dictionary<string, int> value = new Dictionary<string, int> { { "a", 1 }, { "b", 2 }, { "c", 3 } };
            Assert.Equal(value, sql.Val(() => value));
        }

        [Fact]
        public void Local_Value_Dic_Index()
        {
            Dictionary<string, int> value = new Dictionary<string, int> { { "a", 1 }, { "b", 2 }, { "c", 3 } };
            Assert.Equal(value["b"], sql.Val(() => value["b"]));
        }

        [Fact]
        public void Inline_Value()
        {
            Assert.Equal(1, sql.Val(() => 1));
        }

        [Fact]
        public void Inline_Value_Null()
        {
            Assert.Null(sql.Val(() => null));
        }

        [Fact]
        public void Inline_Value_Array()
        {
            Assert.Equal(new byte[] { 1, 2, 3 }, sql.Val(() => new byte[] { 1, 2, 3 }));
        }

        [Fact]
        public void Inline_Value_List()
        {
            Assert.Equal(new List<int> { 1, 2, 3 }, sql.Val(() => new List<int> { 1, 2, 3 }));
        }

        [Fact]
        public void Inline_Value_Dic()
        {
            Assert.Equal(new Dictionary<string, int> { { "a", 1 }, { "b", 2 }, { "c", 3 } },
                sql.Val(() => new Dictionary<string, int> { { "a", 1 }, { "b", 2 }, { "c", 3 } }));
        }

        [Fact]
        public void Inline_Value_New()
        {
            Assert.Equal(new DateTime(), sql.Val(() => new DateTime()));
        }

        [Fact]
        public void Inline_Value_New_Args()
        {
            Assert.Equal(new DateTime(2000, 1, 1), sql.Val(() => new DateTime(2000, 1, 1)));
        }

        [Fact]
        public void Field_Value()
        {
            TestValue value = new TestValue();
            Assert.Equal(value.field, sql.Val(() => value.field));
        }

        [Fact]
        public void Property_Value()
        {
            TestValue value = new TestValue();
            Assert.Equal(value.Property, sql.Val(() => value.Property));
        }

        [Fact]
        public void Static_Field_Value()
        {
            Assert.Equal(TestValue.fieldStatic, sql.Val(() => TestValue.fieldStatic));
        }

        [Fact]
        public void Static_Property_Value()
        {
            Assert.Equal(TestValue.PropertyStatic, sql.Val(() => TestValue.PropertyStatic));
        }

        [Fact]
        public void Nested_Field_Value()
        {
            TestValue value = new TestValue() { Nested = new TestNestedValue() };
            Assert.Equal(value.Nested.field, sql.Val(() => value.Nested.field));
        }

        [Fact]
        public void Nested_Property_Value()
        {
            TestValue value = new TestValue() { Nested = new TestNestedValue() };
            Assert.Equal(value.Nested.Property, sql.Val(() => value.Nested.Property));
        }

        [Fact]
        public void Alias_Value()
        {
            Person person = new Person() { Id = 1 };
            Assert.Equal(person.Id, sql.Val(() => SqlExp.Val(person.Id)));
        }

        [Fact]
        public void Nested_Alias_Value()
        {
            Person person = new Person()
            {
                Department = new Department() { Id = 1 }
            };
            Assert.Equal(person.Department.Id, sql.Val(() => SqlExp.Val(person.Department.Id)));
        }

        [Fact]
        public void Method_Value()
        {
            TestValue value = new TestValue();
            Assert.Equal(value.Method(), sql.Val(() => value.Method()));
        }

        [Fact]
        public void Method_Value_Args()
        {
            TestValue value = new TestValue();
            Assert.Equal(value.MethodArgs(1, 2), sql.Val(() => value.MethodArgs(1, 2)));
        }

        [Theory]
        [InlineData(1, 2)]
        public void Method_Value_Args_Convert_Implicit(short a, short b)
        {
            TestValue value = new TestValue();
            Assert.Equal(value.MethodArgs(a, b), sql.Val(() => value.MethodArgs(a, b)));
        }

        [Theory]
        [InlineData(1, 2)]
        public void Method_Value_Args_Convert_Implicit_Operator(int a, int b)
        {
            TestValue value = new TestValue();
            Assert.Equal(value.MethodArgsConvert(a, b), sql.Val(() => value.MethodArgsConvert(a, b)));
        }

        [Theory]
        [InlineData(1f, 2f)]
        public void Method_Value_Args_Convert_Explicit(float a, float b)
        {
            TestValue value = new TestValue();
            Assert.Equal(value.MethodArgs((int)a, (int)b), sql.Val(() => value.MethodArgs((int)a, (int)b)));
        }

        [Fact]
        public void Static_Method_Value()
        {
            Assert.Equal(TestValue.MethodStatic(), sql.Val(() => TestValue.MethodStatic()));
        }

        [Fact]
        public void Static_Method_Value_Args()
        {
            Assert.Equal(TestValue.MethodArgsStatic(1, 2), sql.Val(() => TestValue.MethodArgsStatic(1, 2)));
        }

        [Theory]
        [InlineData(1, 2)]
        public void Static_Method_Value_Args_Convert_Implicit(short a, short b)
        {
            Assert.Equal(TestValue.MethodArgsStatic(a, b), sql.Val(() => TestValue.MethodArgsStatic(a, b)));
        }

        [Theory]
        [InlineData(1, 2)]
        public void Static_Method_Value_Args_Convert_Implicit_Operator(int a, int b)
        {
            Assert.Equal(TestValue.MethodArgsConvertStatic(a, b), sql.Val(() => TestValue.MethodArgsConvertStatic(a, b)));
        }

        [Theory]
        [InlineData(1f, 2f)]
        public void Static_Method_Value_Args_Convert_Explicit(float a, float b)
        {
            Assert.Equal(TestValue.MethodArgsStatic((int)a, (int)b),
                sql.Val(() => TestValue.MethodArgsStatic((int)a, (int)b)));
        }

        [Fact]
        public void Nested_Method_Value()
        {
            TestValue value = new TestValue() { Nested = new TestNestedValue() };
            Assert.Equal(value.Nested.Method(), sql.Val(() => value.Nested.Method()));
        }

        [Fact]
        public void Nested_Method_Value_Args()
        {
            TestValue value = new TestValue() { Nested = new TestNestedValue() };
            Assert.Equal(value.Nested.MethodArgs(1, 2), sql.Val(() => value.Nested.MethodArgs(1, 2)));
        }

        [Theory]
        [InlineData(200)]
        public void Add_Value(decimal value)
        {
            Person person = new Person() { Salary = 1000 };
            Assert.Equal(person.Salary + 100m + value, sql.Val(() => SqlExp.Val(person.Salary + 100m + value)));
        }

        [Theory]
        [InlineData(200)]
        public void Add_Value_Convert(int value)
        {
            Person person = new Person() { Salary = 1000 };
            Assert.Equal(person.Salary + 100 + value, sql.Val(() => SqlExp.Val(person.Salary + 100 + value)));
        }

        [Theory]
        [InlineData(200)]
        public void Subtract_Value(decimal value)
        {
            Person person = new Person() { Salary = 1000 };
            Assert.Equal(person.Salary - 100m - value, sql.Val(() => SqlExp.Val(person.Salary - 100m - value)));
        }

        [Theory]
        [InlineData(200)]
        public void Subtract_Value_Convert(int value)
        {
            Person person = new Person() { Salary = 1000 };
            Assert.Equal(person.Salary - 100 - value, sql.Val(() => SqlExp.Val(person.Salary - 100 - value)));
        }

        [Theory]
        [InlineData(200)]
        public void Multiply_Value(decimal value)
        {
            Person person = new Person() { Salary = 1000 };
            Assert.Equal(person.Salary * 100m * value, sql.Val(() => SqlExp.Val(person.Salary * 100m * value)));
        }

        [Theory]
        [InlineData(200)]
        public void Multiply_Value_Convert(int value)
        {
            Person person = new Person() { Salary = 1000 };
            Assert.Equal(person.Salary * 100 * value, sql.Val(() => SqlExp.Val(person.Salary * 100 * value)));
        }

        [Theory]
        [InlineData(200)]
        public void Divide_Value(decimal value)
        {
            Person person = new Person() { Salary = 1000 };
            Assert.Equal(person.Salary / 100m / value, sql.Val(() => SqlExp.Val(person.Salary / 100m / value)));
        }

        [Theory]
        [InlineData(200)]
        public void Divide_Value_Convert(int value)
        {
            Person person = new Person() { Salary = 1000 };
            Assert.Equal(person.Salary / 100 / value, sql.Val(() => SqlExp.Val(person.Salary / 100 / value)));
        }

        [Theory]
        [InlineData(200)]
        public void Modulo_Value(decimal value)
        {
            Person person = new Person() { Salary = 1000 };
            Assert.Equal(person.Salary % 100m % value, sql.Val(() => SqlExp.Val(person.Salary % 100m % value)));
        }

        [Theory]
        [InlineData(200)]
        public void Modulo_Value_Convert(int value)
        {
            Person person = new Person() { Salary = 1000 };
            Assert.Equal(person.Salary % 100 % value, sql.Val(() => SqlExp.Val(person.Salary % 100 % value)));
        }

        [Theory]
        [InlineData("abcd")]
        public void Coalesce_Value(string value)
        {
            Person person = new Person() { Name = null };
            Assert.Equal(value, sql.Val(() => SqlExp.Val(person.Name ?? value)));
        }

        public class TestValue
        {
            public int field = 1;

            public int Property { get; set; } = 2;

            public static int fieldStatic = 3;

            public static int PropertyStatic { get; set; } = 4;

            public TestNestedValue Nested { get; set; }

            public int Method() => field + Property;

            public int MethodArgs(int a, int b) => a + b;

            public decimal MethodArgsConvert(decimal a, decimal b) => a + b;

            public static int MethodStatic() => fieldStatic * PropertyStatic;

            public static int MethodArgsStatic(int a, int b) => a * b;

            public static decimal MethodArgsConvertStatic(decimal a, decimal b) => a * b;
        }

        public class TestNestedValue
        {
            public int field = 1;

            public int Property { get; set; } = 2;

            public int Method() => field + Property;

            public int MethodArgs(int a, int b) => a + b;
        }
    }
}