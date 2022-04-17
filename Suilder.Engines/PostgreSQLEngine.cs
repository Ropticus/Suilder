using Suilder.Functions;
using Suilder.Operators;
using Suilder.Reflection.Builder;

namespace Suilder.Engines
{
    /// <summary>
    /// Implementation of <see cref="IEngine"/> for PostgreSQL.
    /// </summary>
    public class PostgreSQLEngine : Engine, IEngine
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLEngine"/> class.
        /// </summary>
        public PostgreSQLEngine()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQLEngine"/> class.
        /// </summary>
        /// <param name="configBuilder">The config builder.</param>
        public PostgreSQLEngine(IConfigBuilder configBuilder) : base(configBuilder)
        {
        }

        /// <summary>
        /// Initializes the engine options.
        /// </summary>
        /// <returns>The engine options.</returns>
        protected override EngineOptions InitOptions()
        {
            EngineOptions options = new EngineOptions();
            options.Name = EngineName.PostgreSQL;
            options.EscapeStart = '"';
            options.EscapeEnd = '"';
            options.LowerCaseNames = true;

            options.WithRecursive = true;
            options.TopSupported = false;
            options.DistinctOnSupported = true;
            options.RightJoinSupported = true;
            options.FullJoinSupported = true;
            options.OffsetStyle = OffsetStyle.Offset;

            return options;
        }

        /// <summary>
        /// Initializes the operators of the engine.
        /// </summary>
        protected override void InitOperators()
        {
            AddOperator(OperatorName.BitXor, "#");
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
            AddFunction(FunctionName.LastInsertId, "LASTVAL");
            AddFunction(FunctionName.Length);
            AddFunction(FunctionName.Lower);
            AddFunction(FunctionName.LTrim);
            AddFunction(FunctionName.Max);
            AddFunction(FunctionName.Min);
            AddFunction(FunctionName.Now);
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