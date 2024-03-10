using System;
using System.Collections.Generic;
using Suilder.Functions;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Expressions
{
    public class ValTest : BuilderBaseTest
    {
        [Fact]
        public void Local_Value_Struct()
        {
            int value = 1;
            Assert.Equal(value, sql.Val(() => value));
        }

        [Fact]
        public void Local_Value_Class()
        {
            string value = "abcd";
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
        public void Inline_Value_Struct()
        {
            Assert.Equal(1, sql.Val(() => 1));
        }

        [Fact]
        public void Inline_Value_Class()
        {
            Assert.Equal("abcd", sql.Val(() => "abcd"));
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
        [InlineData(1, 2)]
        public void Method_Value_Convert_Implicit(short a, int b)
        {
            Assert.Equal(TestValue.Convert(a, b), sql.Val(() => TestValue.Convert(a, b)));
        }

        [Theory]
        [InlineData(1, 2)]
        public void Method_Value_Convert_Implicit_Nullable(int a, int b)
        {
            Assert.Equal(TestValue.ConvertNullable(a, b), sql.Val(() => TestValue.ConvertNullable(a, b)));
        }

        [Theory]
        [InlineData(1, 2)]
        public void Method_Value_Convert_Implicit_Operator(short a, int b)
        {
            Assert.Equal(TestValue.ConvertOperator(a, b), sql.Val(() => TestValue.ConvertOperator(a, b)));
        }

        [Theory]
        [InlineData(1, 2)]
        public void Method_Value_Convert_Implicit_Operator_Nullable(decimal a, int b)
        {
            Assert.Equal(TestValue.ConvertOperatorNullable(a, b), sql.Val(() => TestValue.ConvertOperatorNullable(a, b)));
        }

        [Theory]
        [InlineData(1, 2)]
        public void Method_Value_Convert_Explicit(float a, double b)
        {
            Assert.Equal(TestValue.Convert((int)a, (long)b), sql.Val(() => TestValue.Convert((int)a, (long)b)));
        }

        [Theory]
        [InlineData(1, 2)]
        public void Method_Value_Convert_Explicit_Nullable(float a, double b)
        {
            Assert.Equal(TestValue.ConvertNullable((int)a, (long)b),
                sql.Val(() => TestValue.ConvertNullable((int)a, (long)b)));
        }

        [Theory]
        [InlineData(1, 2)]
        public void Method_Value_Convert_Explicit_Operator(float a, double b)
        {
            Assert.Equal(TestValue.ConvertOperator((decimal)a, (decimal)b),
                sql.Val(() => TestValue.ConvertOperator((decimal)a, (decimal)b)));
        }

        [Theory]
        [InlineData(1, 2)]
        public void Method_Value_Convert_Explicit_Operator_Nullable(float a, double b)
        {
            Assert.Equal(TestValue.ConvertOperatorNullable((decimal)a, (decimal)b),
                sql.Val(() => TestValue.ConvertOperatorNullable((decimal)a, (decimal)b)));
        }

        [Theory]
        [InlineData(1, 2)]
        public void Method_Value_Convert_Explicit_ValueType(int? a, int? b)
        {
            Assert.Equal(TestValue.Convert((int)a, (long)b), sql.Val(() => TestValue.Convert((int)a, (long)b)));
        }

        [Theory]
        [InlineData(PersonFlags.ValueA, PersonFlags.ValueB)]
        public void Method_Value_Convert_Explicit_From_Enum(PersonFlags a, PersonFlags b)
        {
            Assert.Equal(TestValue.Convert((int)a, (long)b), sql.Val(() => TestValue.Convert((int)a, (long)b)));
        }

        [Theory]
        [InlineData(PersonFlags.ValueA, PersonFlags.ValueB)]
        public void Method_Value_Convert_Explicit_From_Enum_Nullable(PersonFlags? a, PersonFlags? b)
        {
            Assert.Equal(TestValue.Convert((int)a, (long)b), sql.Val(() => TestValue.Convert((int)a, (long)b)));
        }

        [Theory]
        [InlineData(1, 2)]
        public void Method_Value_Convert_Explicit_To_Enum(int a, long b)
        {
            Assert.Equal(TestValue.ConvertEnum((PersonFlags)a, (PersonFlags)b),
                sql.Val(() => TestValue.ConvertEnum((PersonFlags)a, (PersonFlags)b)));
        }

        [Theory]
        [InlineData(1, 2)]
        public void Method_Value_Convert_Explicit_To_Enum_Nullable(int a, long b)
        {
            Assert.Equal(TestValue.ConvertEnumNullable((PersonFlags?)a, (PersonFlags?)b),
                sql.Val(() => TestValue.ConvertEnumNullable((PersonFlags?)a, (PersonFlags?)b)));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void Eq_Operator_Value(int value)
        {
            Person person = new Person() { Id = 1 };
            Assert.Equal(person.Id == value, sql.Val(() => SqlExp.Val(person.Id == value)));
        }

        [Theory]
        [InlineData(1000)]
        [InlineData(2000)]
        public void Eq_Operator_Value_Overload(decimal value)
        {
            Person person = new Person() { Salary = 1000 };
            Assert.Equal(person.Salary == value, sql.Val(() => SqlExp.Val(person.Salary == value)));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void NotEq_Operator_Value(int value)
        {
            Person person = new Person() { Id = 1 };
            Assert.Equal(person.Id != value, sql.Val(() => SqlExp.Val(person.Id != value)));
        }

        [Theory]
        [InlineData(1000)]
        [InlineData(2000)]
        public void NotEq_Operator_Value_Overload(decimal value)
        {
            Person person = new Person() { Salary = 1000 };
            Assert.Equal(person.Salary != value, sql.Val(() => SqlExp.Val(person.Salary != value)));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void Lt_Operator_Value(int value)
        {
            Person person = new Person() { Id = 1 };
            Assert.Equal(person.Id < value, sql.Val(() => SqlExp.Val(person.Id < value)));
        }

        [Theory]
        [InlineData(1000)]
        [InlineData(2000)]
        public void Lt_Operator_Value_Overload(decimal value)
        {
            Person person = new Person() { Salary = 1000 };
            Assert.Equal(person.Salary < value, sql.Val(() => SqlExp.Val(person.Salary < value)));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void Le_Operator_Value(int value)
        {
            Person person = new Person() { Id = 1 };
            Assert.Equal(person.Id <= value, sql.Val(() => SqlExp.Val(person.Id <= value)));
        }

        [Theory]
        [InlineData(1000)]
        [InlineData(2000)]
        public void Le_Operator_Value_Overload(decimal value)
        {
            Person person = new Person() { Salary = 1000 };
            Assert.Equal(person.Salary <= value, sql.Val(() => SqlExp.Val(person.Salary <= value)));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void Gt_Operator_Value(int value)
        {
            Person person = new Person() { Id = 1 };
            Assert.Equal(person.Id > value, sql.Val(() => SqlExp.Val(person.Id > value)));
        }

        [Theory]
        [InlineData(1000)]
        [InlineData(2000)]
        public void Gt_Operator_Value_Overload(decimal value)
        {
            Person person = new Person() { Salary = 1000 };
            Assert.Equal(person.Salary > value, sql.Val(() => SqlExp.Val(person.Salary > value)));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void Ge_Operator_Value(int value)
        {
            Person person = new Person() { Id = 1 };
            Assert.Equal(person.Id >= value, sql.Val(() => SqlExp.Val(person.Id >= value)));
        }

        [Theory]
        [InlineData(1000)]
        [InlineData(2000)]
        public void Ge_Operator_Value_Overload(decimal value)
        {
            Person person = new Person() { Salary = 1000 };
            Assert.Equal(person.Salary >= value, sql.Val(() => SqlExp.Val(person.Salary >= value)));
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Not_Operator_Value(bool value)
        {
            Person person = new Person() { Active = value };
            Assert.Equal(!person.Active, sql.Val(() => SqlExp.Val(!person.Active)));
        }

        [Theory]
        [InlineData(1, "abcd")]
        [InlineData(1, "efgh")]
        [InlineData(2, "abcd")]
        [InlineData(2, "efgh")]
        public void And_Operator_Value(int id, string name)
        {
            Person person = new Person() { Id = 1, Name = "abcd" };
            Assert.Equal(person.Id == id & person.Name == name,
                sql.Val(() => SqlExp.Val(person.Id == id & person.Name == name)));
        }

        [Theory]
        [InlineData(1, "abcd")]
        [InlineData(1, "efgh")]
        [InlineData(2, "abcd")]
        [InlineData(2, "efgh")]
        public void AndAlso_Operator_Value(int id, string name)
        {
            Person person = new Person() { Id = 1, Name = "abcd" };
            Assert.Equal(person.Id == id && person.Name == name,
                sql.Val(() => SqlExp.Val(person.Id == id && person.Name == name)));
        }

        [Theory]
        [InlineData(1, "abcd")]
        [InlineData(1, "efgh")]
        [InlineData(2, "abcd")]
        [InlineData(2, "efgh")]
        public void Or_Operator_Value(int id, string name)
        {
            Person person = new Person() { Id = 1, Name = "abcd" };
            Assert.Equal(person.Id == id | person.Name == name,
                sql.Val(() => SqlExp.Val(person.Id == id | person.Name == name)));
        }

        [Theory]
        [InlineData(1, "abcd")]
        [InlineData(1, "efgh")]
        [InlineData(2, "abcd")]
        [InlineData(2, "efgh")]
        public void OrElse_Operator_Value(int id, string name)
        {
            Person person = new Person() { Id = 1, Name = "abcd" };
            Assert.Equal(person.Id == id || person.Name == name,
                sql.Val(() => SqlExp.Val(person.Id == id || person.Name == name)));
        }

        [Theory]
        [MemberData(nameof(DataInt))]
        public void Add_Operator_Value(int value)
        {
            Person person = new Person() { Id = 1 };
            Assert.Equal(person.Id + value, sql.Val(() => SqlExp.Val(person.Id + value)));
        }

        [Theory]
        [MemberData(nameof(DataDecimal))]
        public void Add_Operator_Value_Overload(decimal value)
        {
            Person person = new Person() { Salary = 1000 };
            Assert.Equal(person.Salary + value, sql.Val(() => SqlExp.Val(person.Salary + value)));
        }

        [Theory]
        [MemberData(nameof(DataInt))]
        public void Add_Operator_Value_Overload_Convert(int value)
        {
            Person person = new Person() { Salary = 1000 };
            Assert.Equal(person.Salary + value, sql.Val(() => SqlExp.Val(person.Salary + value)));
        }

        [Theory]
        [MemberData(nameof(DataString))]
        public void Add_Operator_Value_String(string value)
        {
            Person person = new Person() { Name = "abcd" };
            Assert.Equal(person.Name + value, sql.Val(() => SqlExp.Val(person.Name + value)));
        }

        [Theory]
        [MemberData(nameof(DataInt))]
        public void Subtract_Operator_Value(int value)
        {
            Person person = new Person() { Id = 1 };
            Assert.Equal(person.Id - value, sql.Val(() => SqlExp.Val(person.Id - value)));
        }

        [Theory]
        [MemberData(nameof(DataDecimal))]
        public void Subtract_Operator_Value_Overload(decimal value)
        {
            Person person = new Person() { Salary = 1000 };
            Assert.Equal(person.Salary - value, sql.Val(() => SqlExp.Val(person.Salary - value)));
        }

        [Theory]
        [MemberData(nameof(DataInt))]
        public void Subtract_Operator_Value_Overload_Convert(int value)
        {
            Person person = new Person() { Salary = 1000 };
            Assert.Equal(person.Salary - value, sql.Val(() => SqlExp.Val(person.Salary - value)));
        }

        [Theory]
        [MemberData(nameof(DataInt))]
        public void Multiply_Operator_Value(int value)
        {
            Person person = new Person() { Id = 1 };
            Assert.Equal(person.Id * value, sql.Val(() => SqlExp.Val(person.Id * value)));
        }

        [Theory]
        [MemberData(nameof(DataDecimal))]
        public void Multiply_Operator_Value_Overload(decimal value)
        {
            Person person = new Person() { Salary = 1000 };
            Assert.Equal(person.Salary * value, sql.Val(() => SqlExp.Val(person.Salary * value)));
        }

        [Theory]
        [MemberData(nameof(DataInt))]
        public void Multiply_Operator_Value_Overload_Convert(int value)
        {
            Person person = new Person() { Salary = 1000 };
            Assert.Equal(person.Salary * value, sql.Val(() => SqlExp.Val(person.Salary * value)));
        }

        [Theory]
        [MemberData(nameof(DataInt))]
        public void Divide_Operator_Value(int value)
        {
            Person person = new Person() { Id = 1 };
            Assert.Equal(person.Id / value, sql.Val(() => SqlExp.Val(person.Id / value)));
        }

        [Theory]
        [MemberData(nameof(DataDecimal))]
        public void Divide_Operator_Value_Overload(decimal value)
        {
            Person person = new Person() { Salary = 1000 };
            Assert.Equal(person.Salary / value, sql.Val(() => SqlExp.Val(person.Salary / value)));
        }

        [Theory]
        [MemberData(nameof(DataInt))]
        public void Divide_Operator_Value_Overload_Convert(int value)
        {
            Person person = new Person() { Salary = 1000 };
            Assert.Equal(person.Salary / value, sql.Val(() => SqlExp.Val(person.Salary / value)));
        }

        [Theory]
        [MemberData(nameof(DataInt))]
        public void Modulo_Operator_Value(int value)
        {
            Person person = new Person() { Id = 1 };
            Assert.Equal(person.Id % value, sql.Val(() => SqlExp.Val(person.Id % value)));
        }

        [Theory]
        [MemberData(nameof(DataDecimal))]
        public void Modulo_Operator_Value_Overload(decimal value)
        {
            Person person = new Person() { Salary = 1000 };
            Assert.Equal(person.Salary % value, sql.Val(() => SqlExp.Val(person.Salary % value)));
        }

        [Theory]
        [MemberData(nameof(DataInt))]
        public void Modulo_Operator_Value_Overload_Convert(int value)
        {
            Person person = new Person() { Salary = 1000 };
            Assert.Equal(person.Salary % value, sql.Val(() => SqlExp.Val(person.Salary % value)));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(-2)]
        public void Negate_Operator_Value(int value)
        {
            Person person = new Person() { Id = value };
            Assert.Equal(-person.Id, sql.Val(() => SqlExp.Val(-person.Id)));
        }

        [Theory]
        [InlineData(1000)]
        [InlineData(-2000)]
        public void Negate_Operator_Value_Overload(decimal value)
        {
            Person person = new Person() { Salary = value };
            Assert.Equal(-person.Salary, sql.Val(() => SqlExp.Val(-person.Salary)));
        }

        [Theory]
        [MemberData(nameof(DataInt))]
        public void BitAnd_Operator_Value(uint value)
        {
            Person person = new Person() { Flags = 1 };
            Assert.Equal(person.Flags & value, sql.Val(() => SqlExp.Val(person.Flags & value)));
        }

        [Theory]
        [MemberData(nameof(DataInt))]
        public void BitOr_Operator_Value(uint value)
        {
            Person person = new Person() { Flags = 1 };
            Assert.Equal(person.Flags | value, sql.Val(() => SqlExp.Val(person.Flags | value)));
        }

        [Theory]
        [MemberData(nameof(DataInt))]
        public void BitXor_Operator_Value(uint value)
        {
            Person person = new Person() { Flags = 1 };
            Assert.Equal(person.Flags ^ value, sql.Val(() => SqlExp.Val(person.Flags ^ value)));
        }

        [Theory]
        [MemberData(nameof(DataInt))]
        public void BitNot_Operator_Value(uint value)
        {
            Person person = new Person() { Flags = value };
            Assert.Equal(~person.Flags, sql.Val(() => SqlExp.Val(~person.Flags)));
        }

        [Theory]
        [MemberData(nameof(DataInt))]
        public void LeftShift_Operator_Value(int value)
        {
            Person person = new Person() { Flags = 1 };
            Assert.Equal(person.Flags << value, sql.Val(() => SqlExp.Val(person.Flags << value)));
        }

        [Theory]
        [MemberData(nameof(DataInt))]
        public void RightShift_Operator_Value(int value)
        {
            Person person = new Person() { Flags = uint.MaxValue };
            Assert.Equal(person.Flags >> value, sql.Val(() => SqlExp.Val(person.Flags >> value)));
        }

        [Theory]
        [InlineData("abcd", "efgh")]
        [InlineData(null, "efgh")]
        public void Coalesce_Operator_Value(string name, string value)
        {
            Person person = new Person() { Name = name };
            Assert.Equal(person.Name ?? value, sql.Val(() => SqlExp.Val(person.Name ?? value)));
        }

        [Theory]
        [InlineData("abcd", "efgh")]
        [InlineData(null, "efgh")]
        public void Conditional_Operator_Value(string name, string value)
        {
            Person person = new Person() { Name = name, Surname = name };
            Assert.Equal(person.Name != null ? person.Surname : value,
                sql.Val(() => SqlExp.Val(person.Name != null ? person.Surname : value)));
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

            public static int MethodStatic() => fieldStatic * PropertyStatic;

            public static int MethodArgsStatic(int a, int b) => a * b;

            public static long Convert(int a, long b) => a + b;

            public static long? ConvertNullable(int? a, long? b) => a + b;

            public static decimal ConvertOperator(decimal a, decimal b) => a + b;

            public static decimal? ConvertOperatorNullable(decimal? a, decimal? b) => a + b;

            public static PersonFlags ConvertEnum(PersonFlags a, PersonFlags b) => a & b;

            public static PersonFlags? ConvertEnumNullable(PersonFlags? a, PersonFlags? b) => a & b;
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