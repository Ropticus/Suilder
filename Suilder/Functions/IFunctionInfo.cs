using Suilder.Builder;
using Suilder.Core;
using Suilder.Engines;

namespace Suilder.Functions
{
    /// <summary>
    /// Contains the information of a function.
    /// </summary>
    public interface IFunctionInfo
    {
        /// <summary>
        /// The function name.
        /// </summary>
        /// <value>The function name.</value>
        string Name { get; }

        /// <summary>
        /// A delegate to compile the function.
        /// </summary>
        /// <value>A delegate to compile the function.</value>
        FunctionCompile Compile { get; }
    }

    /// <summary>
    /// Compiles a function.
    /// </summary>
    /// <param name="queryBuilder">The query builder.</param>
    /// <param name="engine">The engine.</param>
    /// <param name="name">The SQL function name.</param>
    /// <param name="func">The function.</param>
    public delegate void FunctionCompile(QueryBuilder queryBuilder, IEngine engine, string name, IFunction func);
}