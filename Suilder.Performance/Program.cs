using System;
using System.IO;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

namespace Suilder.Performance
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, new Config(args)
                .WithArtifactsPath(Path.Combine(Environment.CurrentDirectory, "benchmark"))
                .WithOptions(ConfigOptions.StopOnFirstError)
                .WithOptions(ConfigOptions.DontOverwriteResults)
                .WithOptions(ConfigOptions.DisableLogFile));
        }
    }
}
