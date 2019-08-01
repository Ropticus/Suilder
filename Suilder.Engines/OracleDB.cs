using Suilder.Functions;
using Suilder.Reflection;

namespace Suilder.Engines
{
    /// <summary>
    /// Implementation of <see cref="IEngine"/> for OracleSQL.
    /// </summary>
    public class OracleDB : Engine, IEngine
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OracleDB"/> class.
        /// </summary>
        public OracleDB() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OracleDB"/> class.
        /// </summary>
        /// <param name="tableBuilder">The table builder.</param>
        public OracleDB(ITableBuilder tableBuilder) : base(tableBuilder)
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
            options.UpperCaseNames = true;

            options.WithRecursive = false;
            options.TopSupported = false;
            options.DistinctOnSupported = false;
            options.RightJoinSupported = true;
            options.FullJoinSupported = true;
            options.OffsetStyle = OffsetStyle.Offset;

            options.TableAs = false;

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