using Suilder.Functions;
using Suilder.Reflection.Builder;

namespace Suilder.Engines
{
    /// <summary>
    /// Implementation of <see cref="IEngine"/> for SQLServer.
    /// </summary>
    public class SQLServerEngine : Engine, IEngine
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SQLServerEngine"/> class.
        /// </summary>
        public SQLServerEngine()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SQLServerEngine"/> class.
        /// </summary>
        /// <param name="configBuilder">The config builder.</param>
        public SQLServerEngine(IConfigBuilder configBuilder) : base(configBuilder)
        {
        }

        /// <summary>
        /// Initializes the engine options.
        /// </summary>
        /// <returns>The engine options.</returns>
        protected override EngineOptions InitOptions()
        {
            EngineOptions options = new EngineOptions();
            options.Name = EngineName.SQLServer;
            options.EscapeStart = '[';
            options.EscapeEnd = ']';

            options.WithRecursive = false;
            options.TopSupported = true;
            options.DistinctOnSupported = false;
            options.RightJoinSupported = true;
            options.FullJoinSupported = true;
            options.OffsetStyle = OffsetStyle.Offset;

            options.UpdateWithFrom = true;
            options.DeleteWithAlias = true;

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
            AddFunction(FunctionName.Ceiling);
            AddFunction(FunctionName.Coalesce);
            AddFunction(FunctionName.Concat);
            AddFunction(FunctionName.Count);
            AddFunction(FunctionName.Floor);
            AddFunction(FunctionName.LastInsertId, "SCOPE_IDENTITY");
            AddFunction(FunctionName.Length, "LEN");
            AddFunction(FunctionName.Lower);
            AddFunction(FunctionName.LTrim);
            AddFunction(FunctionName.Max);
            AddFunction(FunctionName.Min);
            AddFunction(FunctionName.Now, "SYSDATETIME");
            AddFunction(FunctionName.NullIf);
            AddFunction(FunctionName.Replace);
            AddFunction(FunctionName.Round);
            AddFunction(FunctionName.RTrim);
            AddFunction(FunctionName.Substring);
            AddFunction(FunctionName.Sum);
            AddFunction(FunctionName.Trim, FunctionHelper.TrimBoth);
            AddFunction(FunctionName.Upper);
        }
    }
}