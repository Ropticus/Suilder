using System.Linq;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;

namespace Suilder.Performance
{
    public class Config : ManualConfig
    {
        public Config(string[] args)
        {
            IConfig config = DefaultConfig.Instance;

            if (!args.Contains("-e") && !args.Contains("--exporters"))
                AddExporter(MarkdownExporter.GitHub);

            AddColumnProvider(config.GetColumnProviders().ToArray());
            AddLogger(config.GetLoggers().ToArray());
            AddDiagnoser(config.GetDiagnosers().ToArray());
            AddAnalyser(config.GetAnalysers().ToArray());
            AddJob(config.GetJobs().ToArray());
            AddValidator(config.GetValidators().ToArray());
            AddHardwareCounters(config.GetHardwareCounters().ToArray());
            AddFilter(config.GetFilters().ToArray());
            AddLogicalGroupRules(config.GetLogicalGroupRules().ToArray());

            Orderer = config.Orderer ?? Orderer;
            ArtifactsPath = config.ArtifactsPath ?? ArtifactsPath;
            CultureInfo = config.CultureInfo ?? CultureInfo;
            SummaryStyle = config.SummaryStyle ?? SummaryStyle;
            Options |= config.Options;
        }
    }
}