using Suilder.Functions;
using Suilder.Reflection.Builder;

namespace Suilder.Engines
{
    /// <summary>
    /// Implementation of <see cref="IEngine"/> for OracleSQL.
    /// </summary>
    public class OracleDBEngine : Engine, IEngine
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OracleDBEngine"/> class.
        /// </summary>
        public OracleDBEngine()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OracleDBEngine"/> class.
        /// </summary>
        /// <param name="configBuilder">The config builder.</param>
        public OracleDBEngine(IConfigBuilder configBuilder) : base(configBuilder)
        {
        }

        /// <summary>
        /// Initializes the engine options.
        /// </summary>
        /// <returns>The engine options.</returns>
        protected override EngineOptions InitOptions()
        {
            EngineOptions options = new EngineOptions();
            options.Name = EngineName.OracleDB;
            options.EscapeStart = '"';
            options.EscapeEnd = '"';
            options.UpperCaseNames = true;
            options.ParameterPrefix = ":p";

            options.WithRecursive = false;
            options.TopSupported = false;
            options.DistinctOnSupported = false;
            options.RightJoinSupported = true;
            options.FullJoinSupported = true;
            options.OffsetStyle = OffsetStyle.Offset;

            options.TableAs = false;
            options.FromDummyName = "DUAL";
            options.InsertWithUnion = true;

            return options;
        }

        /// <summary>
        /// Initializes the functions of the engine.
        /// </summary>
        protected override void InitFunctions()
        {
            AddFunction(FunctionName.Abs);
            AddFunction(FunctionName.Avg);
            AddFunction(FunctionName.Cast, FunctionHelper.Cast);
            AddFunction(FunctionName.Ceiling, "CEIL");
            AddFunction(FunctionName.Coalesce);
            AddFunction(FunctionName.Concat, FunctionHelper.ConcatOr);
            AddFunction(FunctionName.Count);
            AddFunction(FunctionName.Floor);
            AddFunction(FunctionName.LastInsertId, FunctionHelper.NotSupported);
            AddFunction(FunctionName.Length);
            AddFunction(FunctionName.Lower);
            AddFunction(FunctionName.LTrim);
            AddFunction(FunctionName.Max);
            AddFunction(FunctionName.Min);
            AddFunction(FunctionName.Now, "SYSDATE", FunctionHelper.NameOnly);
            AddFunction(FunctionName.NullIf);
            AddFunction(FunctionName.Replace);
            AddFunction(FunctionName.Round);
            AddFunction(FunctionName.RTrim);
            AddFunction(FunctionName.Substring, "SUBSTR");
            AddFunction(FunctionName.Sum);
            AddFunction(FunctionName.Trim, FunctionHelper.TrimBoth);
            AddFunction(FunctionName.Upper);
        }
    }
}