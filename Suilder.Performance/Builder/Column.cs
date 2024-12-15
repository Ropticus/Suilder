using BenchmarkDotNet.Attributes;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Performance.Tables;

namespace Suilder.Performance.Builder
{
    public class Column : BaseBenchmark
    {
        [Benchmark(Baseline = true)]
        [BenchmarkCategory("Column")]
        public QueryResult String_Alias_All()
        {
            IAlias person = sql.Alias("person");
            IColumn column = person.All;

            return engine.Compile(column);
        }

        [Benchmark]
        [BenchmarkCategory("Column")]
        public QueryResult String_Alias_Column()
        {
            IAlias person = sql.Alias("person");
            IColumn column = person["Id"];

            return engine.Compile(column);
        }

        [Benchmark]
        [BenchmarkCategory("Column")]
        public QueryResult Typed_Alias_All()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person.All;

            return engine.Compile(column);
        }

        [Benchmark]
        [BenchmarkCategory("Column")]
        public QueryResult Typed_Alias_Column()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person["Id"];

            return engine.Compile(column);
        }

        [Benchmark]
        [BenchmarkCategory("Column")]
        public QueryResult Typed_Alias_Expression_All()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person[x => x];

            return engine.Compile(column);
        }

        [Benchmark]
        [BenchmarkCategory("Column")]
        public QueryResult Typed_Alias_Expression_Column()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IColumn column = person[x => x.Id];

            return engine.Compile(column);
        }

        [Benchmark]
        [BenchmarkCategory("Column")]
        public QueryResult Expression_All()
        {
            Person person = null;
            IColumn column = (IColumn)sql.Val(() => person);

            return engine.Compile(column);
        }

        [Benchmark]
        [BenchmarkCategory("Column")]
        public QueryResult Expression_Column()
        {
            Person person = null;
            IColumn column = (IColumn)sql.Val(() => person.Id);

            return engine.Compile(column);
        }
    }
}