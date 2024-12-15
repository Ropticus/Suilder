using System;
using System.Linq.Expressions;
using BenchmarkDotNet.Attributes;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Performance.Tables;

namespace Suilder.Performance.Builder
{
    public class Alias : BaseBenchmark
    {
        [Benchmark(Baseline = true)]
        [BenchmarkCategory("Alias")]
        public QueryResult String_Alias()
        {
            IAlias alias = sql.Alias("person");

            return engine.Compile(alias);
        }

        [Benchmark]
        [BenchmarkCategory("Alias")]
        public QueryResult Typed_Alias()
        {
            IAlias<Person> alias = sql.Alias<Person>();

            return engine.Compile(alias);
        }

        [Benchmark]
        [BenchmarkCategory("Alias")]
        public QueryResult Expression()
        {
            Person person = null;
            IAlias<Person> alias = sql.Alias(() => person);

            return engine.Compile(alias);
        }

        [Benchmark]
        [BenchmarkCategory("Alias")]
        public QueryResult Expression_Object()
        {
            Person person = null;
            Expression<Func<object>> expression = () => person;
            IAlias alias = sql.Alias(expression);

            return engine.Compile(alias);
        }
    }
}