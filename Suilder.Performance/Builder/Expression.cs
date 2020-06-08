using System;
using System.Linq.Expressions;
using BenchmarkDotNet.Attributes;
using Suilder.Core;
using Suilder.Functions;
using Suilder.Performance.Tables;

namespace Suilder.Performance.Builder
{
    public class Expression : BaseBenchmark
    {
        [Benchmark]
        [BenchmarkCategory("Alias")]
        public IAlias<Person> Alias_Typed()
        {
            Person person = null;
            return sql.Alias(() => person);
        }

        [Benchmark]
        [BenchmarkCategory("Alias")]
        public IAlias Alias_Object()
        {
            Person person = null;
            Expression<Func<object>> expression = () => person;
            return sql.Alias(expression);
        }

        private IAlias<Person> person;

        [GlobalSetup(Targets = new[] { nameof(Column_Typed), nameof(Column_Typed_All) })]
        public void Column_Setup()
        {
            person = sql.Alias<Person>();
        }

        [Benchmark]
        [BenchmarkCategory("Alias")]
        public IColumn Column_Typed()
        {
            return person.Col(x => x.Id);
        }

        [Benchmark]
        [BenchmarkCategory("Alias")]
        public IColumn Column_Typed_All()
        {
            return person.Col(x => x);
        }

        [Benchmark]
        [BenchmarkCategory("Alias")]
        public IColumn Column_Object()
        {
            Person person = null;
            return sql.Col(() => person.Id);
        }

        [Benchmark]
        [BenchmarkCategory("Alias")]
        public IColumn Column_Object_All()
        {
            Person person = null;
            return sql.Col(() => person);
        }

        [Benchmark]
        [BenchmarkCategory("Value", "Field")]
        public object Local_Value()
        {
            int value = 1;
            return sql.Val(() => value);
        }

        [Benchmark]
        [BenchmarkCategory("Value", "Array")]
        public object Local_Value_Array()
        {
            byte[] value = new byte[] { 1, 2, 3 };
            return sql.Val(() => value);
        }

        [Benchmark]
        [BenchmarkCategory("Value", "Field")]
        public object Inline_Value()
        {
            return sql.Val(() => 1);
        }

        [Benchmark]
        [BenchmarkCategory("Value", "Array")]
        public object Inline_Value_Array()
        {
            return sql.Val(() => new byte[] { 1, 2, 3 });
        }

        [Benchmark]
        [BenchmarkCategory("Value", "New")]
        public object Inline_Value_New()
        {
            return sql.Val(() => new DateTime());
        }

        [Benchmark]
        [BenchmarkCategory("Value", "New")]
        public object Inline_Value_New_Args()
        {
            return sql.Val(() => new DateTime(2000, 1, 1));
        }

        [Benchmark]
        [BenchmarkCategory("Value", "Field")]
        public object Field_Value()
        {
            TestValue value = new TestValue();
            return sql.Val(() => value.field);
        }

        [Benchmark]
        [BenchmarkCategory("Value", "Field")]
        public object Property_Value()
        {
            TestValue value = new TestValue();
            return sql.Val(() => value.Property);
        }

        [Benchmark]
        [BenchmarkCategory("Value", "Field")]
        public object Static_Field_Value()
        {
            return sql.Val(() => TestValue.fieldStatic);
        }

        [Benchmark]
        [BenchmarkCategory("Value", "Field")]
        public object Static_Property_Value()
        {
            return sql.Val(() => TestValue.PropertyStatic);
        }

        [Benchmark]
        [BenchmarkCategory("Value", "Field")]
        public object Nested_Field_Value()
        {
            TestValue value = new TestValue() { Nested = new TestNestedValue() };
            return sql.Val(() => value.Nested.field);
        }

        [Benchmark]
        [BenchmarkCategory("Value", "Field")]
        public object Nested_Property_Value()
        {
            TestValue value = new TestValue() { Nested = new TestNestedValue() };
            return sql.Val(() => value.Nested.Property);
        }

        [Benchmark]
        [BenchmarkCategory("Value", "Field")]
        public object Alias_Value()
        {
            Person person = new Person() { Id = 1 };
            return sql.Val(() => SqlExp.Val(person.Id));
        }

        [Benchmark]
        [BenchmarkCategory("Value", "Field")]
        public object Nested_Alias_Value()
        {
            Person person = new Person()
            {
                Department = new Department() { Id = 1 }
            };
            return sql.Val(() => SqlExp.Val(person.Department.Id));
        }

        [Benchmark]
        [BenchmarkCategory("Value", "Method")]
        public object Method_Value()
        {
            TestValue value = new TestValue();
            return sql.Val(() => value.Method());
        }

        [Benchmark]
        [BenchmarkCategory("Value", "Method")]
        public object Method_Value_Args()
        {
            TestValue value = new TestValue();
            return sql.Val(() => value.MethodArgs(1, 2));
        }

        [Benchmark]
        [BenchmarkCategory("Value", "Method")]
        public object Static_Method_Value()
        {
            return sql.Val(() => TestValue.MethodStatic());
        }

        [Benchmark]
        [BenchmarkCategory("Value", "Method")]
        public object Static_Method_Value_Args()
        {
            return sql.Val(() => TestValue.MethodArgsStatic(1, 2));
        }

        [Benchmark]
        [BenchmarkCategory("Value", "Method")]
        public object Nested_Method_Value()
        {
            TestValue value = new TestValue() { Nested = new TestNestedValue() };
            return sql.Val(() => value.Nested.Method());
        }

        [Benchmark]
        [BenchmarkCategory("Value", "Method")]
        public object Nested_Method_Value_Args()
        {
            TestValue value = new TestValue() { Nested = new TestNestedValue() };
            return sql.Val(() => value.Nested.MethodArgs(1, 2));
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