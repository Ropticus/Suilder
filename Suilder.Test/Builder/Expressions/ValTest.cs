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
            int[] value = new int[] { 1, 2, 3 };
            Assert.Equal(value, sql.Val(() => value));
        }

        [Fact]
        public void Local_Value_Array_Index()
        {
            int[] value = new int[] { 1, 2, 3 };
            Assert.Equal(value[1], sql.Val(() => value[1]));
        }

        [Fact]
        public void Local_Value_Array_Length()
        {
            int[] value = new int[] { 1, 2, 3 };
            Assert.Equal(value.Length, sql.Val(() => value.Length));
        }

        [Fact]
        public void Local_Value_Array_Nested()
        {
            int[][] value = new int[][] { new int[] { 1, 2, 3 }, new int[] { 4, 5, 6 } };
            Assert.Equal(value, sql.Val(() => value));
        }

        [Fact]
        public void Local_Value_Array_Nested_Index()
        {
            int[][] value = new int[][] { new int[] { 1, 2, 3 }, new int[] { 4, 5, 6 } };
            Assert.Equal(value[1][2], sql.Val(() => value[1][2]));
        }

        [Fact]
        public void Local_Value_Array_Nested_Length()
        {
            int[][] value = new int[][] { new int[] { 1, 2, 3 }, new int[] { 4, 5, 6 } };
            Assert.Equal(value.Length, sql.Val(() => value.Length));
            Assert.Equal(value[1].Length, sql.Val(() => value[1].Length));
        }

        [Fact]
        public void Local_Value_Array_Multi()
        {
            int[,] value = new int[,] { { 1, 2, 3 }, { 4, 5, 6 } };
            Assert.Equal(value, sql.Val(() => value));
        }

        [Fact]
        public void Local_Value_Array_Multi_Index()
        {
            int[,] value = new int[,] { { 1, 2, 3 }, { 4, 5, 6 } };
            Assert.Equal(value[1, 2], sql.Val(() => value[1, 2]));
        }

        [Fact]
        public void Local_Value_Array_Multi_Length()
        {
            int[,] value = new int[,] { { 1, 2, 3 }, { 4, 5, 6 } };
            Assert.Equal(value.Length, sql.Val(() => value.Length));
            Assert.Equal(value.GetLength(1), sql.Val(() => value.GetLength(1)));
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
        public void Local_Value_List_Count()
        {
            List<int> value = new List<int> { 1, 2, 3 };
            Assert.Equal(value.Count, sql.Val(() => value.Count));
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
        public void Local_Value_Dic_Count()
        {
            Dictionary<string, int> value = new Dictionary<string, int> { { "a", 1 }, { "b", 2 }, { "c", 3 } };
            Assert.Equal(value.Count, sql.Val(() => value.Count));
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
        [MemberData(nameof(DataInt))]
        public void Add_Operator_Value_Checked(int value)
        {
            Person person = new Person() { Id = 1 };
            Assert.Equal(checked(person.Id + value), sql.Val(() => checked(SqlExp.Val(person.Id + value))));
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
        [MemberData(nameof(DataInt))]
        public void Subtract_Operator_Value_Checked(int value)
        {
            Person person = new Person() { Id = 1 };
            Assert.Equal(checked(person.Id - value), sql.Val(() => checked(SqlExp.Val(person.Id - value))));
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
        [MemberData(nameof(DataInt))]
        public void Multiply_Operator_Value_Checked(int value)
        {
            Person person = new Person() { Id = 1 };
            Assert.Equal(checked(person.Id * value), sql.Val(() => checked(SqlExp.Val(person.Id * value))));
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
        [InlineData(1)]
        [InlineData(-2)]
        public void Negate_Operator_Value_Checked(int value)
        {
            Person person = new Person() { Id = value };
            Assert.Equal(checked(-person.Id), sql.Val(() => checked(SqlExp.Val(-person.Id))));
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