using Suilder.Functions;
using Suilder.Reflection;

namespace Suilder.Engines
{
    /// <summary>
    /// Implementation of <see cref="IEngine"/> for SQLite.
    /// </summary>
    public class SQLite : Engine, IEngine
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SQLite"/> class.
        /// </summary>
        public SQLite()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SQLite"/> class.
        /// </summary>
        /// <param name="tableBuilder">The table builder.</param>
        public SQLite(ITableBuilder tableBuilder) : base(tableBuilder)
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

            options.WithRecursive = true;
            options.TopSupported = false;
            options.DistinctOnSupported = false;
            options.RightJoinSupported = false;
            options.FullJoinSupported = false;
            options.OffsetStyle = OffsetStyle.Limit;

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
            AddFunction(FunctionName.LastInsertId, "LAST_INSERT_ROWID");
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
            AddFunction(FunctionName.Substring, "SUBSTR");
            AddFunction(FunctionName.Sum);
            AddFunction(FunctionName.Trim);
            AddFunction(FunctionName.Upper);
        }
    }
}