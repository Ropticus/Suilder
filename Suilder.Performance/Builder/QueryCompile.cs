using BenchmarkDotNet.Attributes;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Extensions;
using Suilder.Performance.Tables;

namespace Suilder.Performance.Builder
{
    public class QueryCompile : BaseBenchmark
    {
        private IQueryFragment query;

        [GlobalSetup(Target = nameof(String_Alias))]
        public void String_Alias_Setup()
        {
            IAlias person = sql.Alias("person");
            query = sql.Query
                .Select(person.All)
                .From(person)
                .Where(person["Id"].Eq(1));
        }

        [Benchmark(Baseline = true)]
        [BenchmarkCategory("Query")]
        public QueryResult String_Alias()
        {
            return engine.Compile(query);
        }

        [GlobalSetup(Target = nameof(Typed_Alias))]
        public void Typed_Alias_Setup()
        {
            IAlias<Person> person = sql.Alias<Person>();
            query = sql.Query
                .Select(person.All)
                .From(person)
                .Where(person["Id"].Eq(1));
        }

        [Benchmark]
        [BenchmarkCategory("Query")]
        public QueryResult Typed_Alias()
        {
            return engine.Compile(query);
        }

        [GlobalSetup(Target = nameof(Typed_Alias_Expression))]
        public void Typed_Alias_Expression_Setup()
        {
            IAlias<Person> person = sql.Alias<Person>();
            query = sql.Query
                .Select(person.All)
                .From(person)
                .Where(person[x => x.Id].Eq(1));
        }

        [Benchmark]
        [BenchmarkCategory("Query")]
        public QueryResult Typed_Alias_Expression()
        {
            return engine.Compile(query);
        }

        [GlobalSetup(Target = nameof(Expression))]
        public void Expression_Setup()
        {
            int id = 1;
            Person person = null;
            query = sql.Query
                .Select(() => person)
                .From(() => person)
                .Where(() => person.Id == id);
        }

        [Benchmark]
        [BenchmarkCategory("Query")]
        public QueryResult Expression()
        {
            return engine.Compile(query);
        }

        [GlobalSetup(Target = nameof(Raw_Format))]
        public void Raw_Format_Setup()
        {
            IAlias person = sql.Alias("person");
            query = sql.RawQuery("SELECT {0} FROM {1} WHERE {2} = {3}", person.All, person, person["Id"], 1);
        }

        [Benchmark]
        [BenchmarkCategory("Query")]
        public QueryResult Raw_Format()
        {
            return engine.Compile(query);
        }
    }
}