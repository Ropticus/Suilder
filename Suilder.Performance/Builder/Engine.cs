using BenchmarkDotNet.Attributes;

namespace Suilder.Performance.Builder
{
    public class Engine : BaseBenchmark
    {
        [Benchmark(Baseline = true)]
        [BenchmarkCategory("Escape")]
        public string Escape_Name()
        {
            return engine.EscapeName("person.AddressStreet");
        }

        [GlobalSetup(Target = nameof(Escape_Name_UpperCase))]
        public void Escape_Name_UpperCase_Setup()
        {
            engine.Options.UpperCaseNames = true;
        }

        [Benchmark]
        [BenchmarkCategory("Escape")]
        public string Escape_Name_UpperCase()
        {
            return engine.EscapeName("person.AddressStreet");
        }

        [GlobalSetup(Target = nameof(Escape_Name_LowerCase))]
        public void Escape_Name_LowerCase_Setup()
        {
            engine.Options.LowerCaseNames = true;
        }

        [Benchmark]
        [BenchmarkCategory("Escape")]
        public string Escape_Name_LowerCase()
        {
            return engine.EscapeName("person.AddressStreet");
        }
    }
}