using BenchmarkDotNet.Attributes;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Performance.Tables;

namespace Suilder.Performance.Builder
{
    public class ColumnCompile : BaseBenchmark
    {
        private IColumn column;

        [GlobalSetup(Target = nameof(String_Alias_All))]
        public void String_Alias_All_Setup()
        {
            IAlias person = sql.Alias("person");
            column = person.All;
        }

        [Benchmark(Baseline = true)]
        [BenchmarkCategory("Column")]
        public QueryResult String_Alias_All()
        {
            return engine.Compile(column);
        }

        [GlobalSetup(Target = nameof(String_Alias_Column))]
        public void String_Alias_Column_Setup()
        {
            IAlias person = sql.Alias("person");
            column = person["Id"];
        }

        [Benchmark]
        [BenchmarkCategory("Column")]
        public QueryResult String_Alias_Column()
        {
            return engine.Compile(column);
        }

        [GlobalSetup(Target = nameof(Typed_Alias_All))]
        public void Typed_Alias_All_Setup()
        {
            IAlias<Person> person = sql.Alias<Person>();
            column = person.All;
        }

        [Benchmark]
        [BenchmarkCategory("Column")]
        public QueryResult Typed_Alias_All()
        {
            return engine.Compile(column);
        }

        [GlobalSetup(Target = nameof(Typed_Alias_Column))]
        public void Typed_Alias_Column_Setup()
        {
            IAlias<Person> person = sql.Alias<Person>();
            column = person["Id"];
        }

        [Benchmark]
        [BenchmarkCategory("Column")]
        public QueryResult Typed_Alias_Column()
        {
            return engine.Compile(column);
        }

        [GlobalSetup(Target = nameof(Typed_Alias_Expression_All))]
        public void Typed_Alias_Expression_All_Setup()
        {
            IAlias<Person> person = sql.Alias<Person>();
            column = person[x => x];
        }

        [Benchmark]
        [BenchmarkCategory("Column")]
        public QueryResult Typed_Alias_Expression_All()
        {
            return engine.Compile(column);
        }

        [GlobalSetup(Target = nameof(Typed_Alias_Expression_Column))]
        public void Typed_Alias_Expression_Column_Setup()
        {
            IAlias<Person> person = sql.Alias<Person>();
            column = person[x => x.Id];
        }

        [Benchmark]
        [BenchmarkCategory("Column")]
        public QueryResult Typed_Alias_Expression_Column()
        {
            return engine.Compile(column);
        }

        [GlobalSetup(Target = nameof(Expression_All))]
        public void Expression_All_Setup()
        {
            Person person = null;
            column = (IColumn)sql.Val(() => person);
        }

        [Benchmark]
        [BenchmarkCategory("Column")]
        public QueryResult Expression_All()
        {
            return engine.Compile(column);
        }

        [GlobalSetup(Target = nameof(Expression_Column))]
        public void Expression_Column_Setup()
        {
            Person person = null;
            column = (IColumn)sql.Val(() => person.Id);
        }

        [Benchmark]
        [BenchmarkCategory("Column")]
        public QueryResult Expression_Column()
        {
            return engine.Compile(column);
        }
    }
}