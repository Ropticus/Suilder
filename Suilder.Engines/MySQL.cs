using Suilder.Functions;
using Suilder.Reflection;

namespace Suilder.Engines
{
    /// <summary>
    /// Implementation of <see cref="IEngine"/> for MySQL.
    /// </summary>
    public class MySQL : Engine, IEngine
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MySQL"/> class.
        /// </summary>
        public MySQL() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MySQL"/> class.
        /// </summary>
        /// <param name="tableBuilder">The table builder.</param>
        public MySQL(ITableBuilder tableBuilder) : base(tableBuilder)
        {
        }

        /// <summary>
        /// Initializes the engine options.
        /// </summary>
        /// <returns>The engine options.</returns>
        protected override EngineOptions InitOptions()
        {
            EngineOptions options = new EngineOptions();
            options.EscapeStart = '`';
            options.EscapeEnd = '`';

            options.WithRecursive = true;
            options.TopSupported = false;
            options.DistinctOnSupported = false;
            options.RightJoinSupported = true;
            options.FullJoinSupported = false;
            options.OffsetStyle = OffsetStyle.Limit;

            options.UpdateSetWithTableName = true;
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
            AddFunction(FunctionName.Cast);
            AddFunction(FunctionName.Ceiling);
            AddFunction(FunctionName.Coalesce);
            AddFunction(FunctionName.Concat);
            AddFunction(FunctionName.Count);
            AddFunction(FunctionName.Floor);
            AddFunction(FunctionName.LastInsertId, "LAST_INSERT_ID");
            AddFunction(FunctionName.Length);
            AddFunction(FunctionName.Lower);
            AddFunction(FunctionName.LTrim, FunctionHelper.TrimLeading);
            AddFunction(FunctionName.Max);
            AddFunction(FunctionName.Min);
            AddFunction(FunctionName.Now);
            AddFunction(FunctionName.NullIf);
            AddFunction(FunctionName.Replace);
            AddFunction(FunctionName.Round);
            AddFunction(FunctionName.RTrim, FunctionHelper.TrimTrailing);
            AddFunction(FunctionName.Substring);
            AddFunction(FunctionName.Sum);
            AddFunction(FunctionName.Trim, FunctionHelper.TrimBoth);
            AddFunction(FunctionName.Upper);
        }
    }
}