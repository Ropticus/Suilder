using Suilder.Functions;
using Suilder.Reflection;

namespace Suilder.Engines
{
    /// <summary>
    /// Implementation of <see cref="IEngine"/> for PostgreSQL.
    /// </summary>
    public class PostgreSQL : Engine, IEngine
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQL"/> class.
        /// </summary>
        public PostgreSQL() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSQL"/> class.
        /// </summary>
        /// <param name="tableBuilder">The table builder.</param>
        public PostgreSQL(ITableBuilder tableBuilder) : base(tableBuilder)
        {
        }

        /// <summary>
        /// Initializes the engine options.
        /// </summary>
        /// <returns>The engine options.</returns>
        protected override EngineOptions InitOptions()
        {
            EngineOptions options = new EngineOptions();
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
        /// Initializes the functions of the engine.
        /// </summary>
        protected override void InitFunctions()
        {
            AddFunction(FunctionName.Abs);
            AddFunction(FunctionName.Avg);
            AddFunction(FunctionName.Cast);
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