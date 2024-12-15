using System;
using System.Linq.Expressions;
using BenchmarkDotNet.Attributes;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Performance.Tables;

namespace Suilder.Performance.Builder
{
    public class AliasCompile : BaseBenchmark
    {
        private IAlias alias;

        [GlobalSetup(Target = nameof(String_Alias))]
        public void String_Alias_Setup()
        {
            alias = sql.Alias("person");
        }

        [Benchmark(Baseline = true)]
        [BenchmarkCategory("Alias")]
        public QueryResult String_Alias()
        {
            return engine.Compile(alias);
        }

        [GlobalSetup(Target = nameof(Typed_Alias))]
        public void Typed_Alias_Setup()
        {
            alias = sql.Alias<Person>();
        }

        [Benchmark]
        [BenchmarkCategory("Alias")]
        public QueryResult Typed_Alias()
        {
            return engine.Compile(alias);
        }

        [GlobalSetup(Target = nameof(Expression))]
        public void Expression_Setup()
        {
            Person person = null;
            alias = sql.Alias(() => person);
        }

        [Benchmark]
        [BenchmarkCategory("Alias")]
        public QueryResult Expression()
        {
            return engine.Compile(alias);
        }

        [GlobalSetup(Target = nameof(Expression_Object))]
        public void Expression_Object_Setup()
        {
            Person person = null;
            Expression<Func<object>> expression = () => person;
            alias = sql.Alias(expression);
        }

        [Benchmark]
        [BenchmarkCategory("Alias")]
        public QueryResult Expression_Object()
        {
            return engine.Compile(alias);
        }
    }
}