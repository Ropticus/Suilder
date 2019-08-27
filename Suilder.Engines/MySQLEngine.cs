using Suilder.Functions;
using Suilder.Reflection.Builder;

namespace Suilder.Engines
{
    /// <summary>
    /// Implementation of <see cref="IEngine"/> for MySQL.
    /// </summary>
    public class MySQLEngine : Engine, IEngine
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MySQLEngine"/> class.
        /// </summary>
        public MySQLEngine()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MySQLEngine"/> class.
        /// </summary>
        /// <param name="configBuilder">The config builder.</param>
        public MySQLEngine(IConfigBuilder configBuilder) : base(configBuilder)
        {
        }

        /// <summary>
        /// Initializes the engine options.
        /// </summary>
        /// <returns>The engine options.</returns>
        protected override EngineOptions InitOptions()
        {
            EngineOptions options = new EngineOptions();
            options.Name = EngineName.MySQL;
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
            AddFunction(FunctionName.Cast, FunctionHelper.Cast);
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