using System;
using System.Linq.Expressions;
using BenchmarkDotNet.Attributes;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Functions;
using Suilder.Performance.Tables;

namespace Suilder.Performance.Builder
{
    public class Builder : BaseBenchmark
    {
        [Benchmark]
        [BenchmarkCategory("Alias", "AliasTyped")]
        public QueryResult Alias_Typed()
        {
            Person person = null;
            IAlias alias = sql.Alias(() => person);

            return engine.Compile(alias);
        }

        [Benchmark]
        [BenchmarkCategory("Alias", "AliasObject")]
        public QueryResult Alias_Object()
        {
            Person person = null;
            Expression<Func<object>> expression = () => person;
            IAlias alias = sql.Alias(expression);

            return engine.Compile(alias);
        }

        private IAlias<Person> person;

        [GlobalSetup(Targets = new[] { nameof(Column_Typed_All), nameof(Column_Typed), nameof(Column_Typed_Nested),
            nameof(Column_Typed_Nested_Deep) })]
        public void Column_Setup()
        {
            person = sql.Alias<Person>();
        }

        [Benchmark]
        [BenchmarkCategory("Column", "AliasTyped")]
        public QueryResult Column_Typed_All()
        {
            IColumn column = person.Col(x => x);

            return engine.Compile(column);
        }

        [Benchmark]
        [BenchmarkCategory("Column", "AliasTyped")]
        public QueryResult Column_Typed()
        {
            IColumn column = person.Col(x => x.Id);

            return engine.Compile(column);
        }

        [Benchmark]
        [BenchmarkCategory("Column", "AliasTyped")]
        public QueryResult Column_Typed_Nested()
        {
            IColumn column = person.Col(x => x.Address.Street);

            return engine.Compile(column);
        }

        [Benchmark]
        [BenchmarkCategory("Column", "AliasTyped")]
        public QueryResult Column_Typed_Nested_Deep()
        {
            IColumn column = person.Col(x => x.Address.City.Country.Name);

            return engine.Compile(column);
        }

        [Benchmark]
        [BenchmarkCategory("Column", "AliasObject")]
        public QueryResult Column_Object_All()
        {
            Person person = null;
            IColumn column = sql.Col(() => person);

            return engine.Compile(column);
        }

        [Benchmark]
        [BenchmarkCategory("Column", "AliasObject")]
        public QueryResult Column_Object()
        {
            Person person = null;
            IColumn column = sql.Col(() => person.Id);

            return engine.Compile(column);
        }

        [Benchmark]
        [BenchmarkCategory("Column", "AliasObject")]
        public QueryResult Column_Object_Nested()
        {
            Person person = null;
            IColumn column = sql.Col(() => person.Address.Street);

            return engine.Compile(column);
        }

        [Benchmark]
        [BenchmarkCategory("Column", "AliasObject")]
        public QueryResult Column_Object_Nested_Deep()
        {
            Person person = null;
            IColumn column = sql.Col(() => person.Address.City.Country.Name);

            return engine.Compile(column);
        }

        [Benchmark]
        [BenchmarkCategory("Column", "AliasVal")]
        public QueryResult Column_Val_All()
        {
            Person person = null;
            IColumn column = (IColumn)sql.Val(() => person);

            return engine.Compile(column);
        }

        [Benchmark]
        [BenchmarkCategory("Column", "AliasVal")]
        public QueryResult Column_Val()
        {
            Person person = null;
            IColumn column = (IColumn)sql.Val(() => person.Id);

            return engine.Compile(column);
        }

        [Benchmark]
        [BenchmarkCategory("Column", "AliasVal")]
        public QueryResult Column_Val_Nested()
        {
            Person person = null;
            IColumn column = (IColumn)sql.Val(() => person.Address.Street);

            return engine.Compile(column);
        }

        [Benchmark]
        [BenchmarkCategory("Column", "AliasVal")]
        public QueryResult Column_Val_Nested_Deep()
        {
            Person person = null;
            IColumn column = (IColumn)sql.Val(() => person.Address.City.Country.Name);

            return engine.Compile(column);
        }

        [Benchmark]
        [BenchmarkCategory("Operator")]
        public QueryResult Comparison_Operator()
        {
            int id = 1;

            Person person = null;
            IOperator op = sql.Op(() => person.Id == id);

            return engine.Compile(op);
        }

        [Benchmark]
        [BenchmarkCategory("Operator")]
        public QueryResult Logical_Operator()
        {
            int id = 1;
            string name = "abcd";

            Person person = null;
            IOperator op = sql.Op(() => person.Id == id || person.Name == name);

            return engine.Compile(op);
        }

        [Benchmark]
        [BenchmarkCategory("Operator")]
        public QueryResult Arith_Operator()
        {
            decimal value = 100;

            Person person = null;
            IOperator op = (IOperator)sql.Val(() => person.Salary + value);

            return engine.Compile(op);
        }

        [Benchmark]
        [BenchmarkCategory("Operator")]
        public QueryResult Bit_Operator()
        {
            int value = 1;

            Person person = null;
            IOperator op = (IOperator)sql.Val(() => person.Id & value);

            return engine.Compile(op);
        }

        [Benchmark]
        [BenchmarkCategory("Operator")]
        public QueryResult Concat_Operator()
        {
            string value = "abcd";

            Person person = null;
            IFunction func = (IFunction)sql.Val(() => person.Name + value);

            return engine.Compile(func);
        }

        [Benchmark]
        [BenchmarkCategory("Operator")]
        public QueryResult Coalesce_Operator()
        {
            string name = "abcd";

            Person person = null;
            IFunction func = (IFunction)sql.Val(() => person.Name ?? name);

            return engine.Compile(func);
        }

        [Benchmark]
        [BenchmarkCategory("Operator")]
        public QueryResult Conditional_Operator()
        {
            string name = "abcd";

            Person person = null;
            ICase caseWhen = (ICase)sql.Val(() => person.Name != null ? person.Surname : name);

            return engine.Compile(caseWhen);
        }

        [Benchmark]
        [BenchmarkCategory("Method")]
        public QueryResult Method()
        {
            IFunction func = (IFunction)sql.Val(() => SqlExp.Count());

            return engine.Compile(func);
        }

        [Benchmark]
        [BenchmarkCategory("Method")]
        public QueryResult Method_Args()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Sum(person.Salary));

            return engine.Compile(func);
        }

        [Benchmark]
        [BenchmarkCategory("Method")]
        public QueryResult Method_Args_Params()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Concat(person.Name, person.Surname));

            return engine.Compile(func);
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