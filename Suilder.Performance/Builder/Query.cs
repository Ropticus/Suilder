using BenchmarkDotNet.Attributes;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Extensions;
using Suilder.Performance.Tables;

namespace Suilder.Performance.Builder
{
    public class Query : BaseBenchmark
    {
        [Benchmark(Baseline = true)]
        [BenchmarkCategory("Query")]
        public QueryResult String_Alias()
        {
            IAlias person = sql.Alias("person");
            IQuery query = sql.Query
                .Select(person.All)
                .From(person)
                .Where(person["Id"].Eq(1));

            return engine.Compile(query);
        }

        [Benchmark]
        [BenchmarkCategory("Query")]
        public QueryResult Typed_Alias()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IQuery query = sql.Query
                .Select(person.All)
                .From(person)
                .Where(person["Id"].Eq(1));

            return engine.Compile(query);
        }

        [Benchmark]
        [BenchmarkCategory("Query")]
        public QueryResult Typed_Alias_Expression()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IQuery query = sql.Query
                .Select(person.All)
                .From(person)
                .Where(person[x => x.Id].Eq(1));

            return engine.Compile(query);
        }

        [Benchmark]
        [BenchmarkCategory("Query")]
        public QueryResult Expression()
        {
            int id = 1;
            Person person = null;
            IQuery query = sql.Query
                .Select(() => person)
                .From(() => person)
                .Where(() => person.Id == id);

            return engine.Compile(query);
        }

        [Benchmark]
        [BenchmarkCategory("Query")]
        public QueryResult Raw_Format()
        {
            IAlias person = sql.Alias("person");
            IRawQuery query = sql.RawQuery("SELECT {0} FROM {1} WHERE {2} = {3}", person.All, person, person["Id"], 1);

            return engine.Compile(query);
        }
    }
}