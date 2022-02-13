using Suilder.Builder;
using Suilder.Core;
using Suilder.Engines;
using Suilder.Exceptions;

namespace Suilder.Functions
{
    /// <summary>
    /// Helper with implemented functions.
    /// </summary>
    public class FunctionHelper
    {
        /// <summary>
        /// Throws a <see cref="ClauseNotSupportedException"/> exception.
        /// </summary>
        /// <param name="queryBuilder">The query builder.</param>
        /// <param name="engine">The engine.</param>
        /// <param name="name">The SQL function name.</param>
        /// <param name="func">The function.</param>
        public static void NotSupported(QueryBuilder queryBuilder, IEngine engine, string name, IFunction func)
        {
            throw new ClauseNotSupportedException($"Function \"{func.Name}\" is not supported in this engine.");
        }

        /// <summary>
        /// Writes the function without parentheses or parameters.
        /// </summary>
        /// <param name="queryBuilder">The query builder.</param>
        /// <param name="engine">The engine.</param>
        /// <param name="name">The SQL function name.</param>
        /// <param name="func">The function.</param>
        public static void NameOnly(QueryBuilder queryBuilder, IEngine engine, string name, IFunction func)
        {
            if (func.Args.Count > 0)
                throw new CompileException($"Invalid function \"{func.Name}\", wrong number of parameters.");

            queryBuilder.Write(name);
        }

        /// <summary>
        /// Cast function.
        /// </summary>
        /// <param name="queryBuilder">The query builder.</param>
        /// <param name="engine">The engine.</param>
        /// <param name="name">The SQL function name.</param>
        /// <param name="func">The function.</param>
        public static void Cast(QueryBuilder queryBuilder, IEngine engine, string name, IFunction func)
        {
            if (func.Args.Count != 2)
                throw new CompileException($"Invalid function \"{func.Name}\", wrong number of parameters.");

            queryBuilder.Write(name + "(").WriteValue(func.Args[0]).Write(" AS ").WriteValue(func.Args[1]).Write(")");
        }

        /// <summary>
        /// Concat function with || operator.
        /// </summary>
        /// <param name="queryBuilder">The query builder.</param>
        /// <param name="engine">The engine.</param>
        /// <param name="name">The SQL function name.</param>
        /// <param name="func">The function.</param>
        public static void ConcatOr(QueryBuilder queryBuilder, IEngine engine, string name, IFunction func)
        {
            if (func.Args.Count == 0)
                throw new CompileException($"Invalid function \"{func.Name}\", wrong number of parameters.");

            queryBuilder.Write("(");

            string separator = " || ";
            foreach (object value in func.Args)
            {
                queryBuilder.WriteValue(value).Write(separator);
            }
            queryBuilder.RemoveLast(separator.Length).Write(")");
        }

        /// <summary>
        /// Trim leading function.
        /// </summary>
        /// <param name="queryBuilder">The query builder.</param>
        /// <param name="engine">The engine.</param>
        /// <param name="name">The SQL function name.</param>
        /// <param name="func">The function.</param>
        public static void TrimLeading(QueryBuilder queryBuilder, IEngine engine, string name, IFunction func)
        {
            if (func.Args.Count > 2)
                throw new CompileException($"Invalid function \"{func.Name}\", wrong number of parameters.");

            queryBuilder.Write("TRIM(LEADING ");

            if (func.Args.Count > 1)
                queryBuilder.WriteValue(func.Args[1]).Write(" ");

            queryBuilder.Write("FROM ").WriteValue(func.Args[0]).Write(")");
        }

        /// <summary>
        /// Trim trailing function.
        /// </summary>
        /// <param name="queryBuilder">The query builder.</param>
        /// <param name="engine">The engine.</param>
        /// <param name="name">The SQL function name.</param>
        /// <param name="func">The function.</param>
        public static void TrimTrailing(QueryBuilder queryBuilder, IEngine engine, string name, IFunction func)
        {
            if (func.Args.Count > 2)
                throw new CompileException($"Invalid function \"{func.Name}\", wrong number of parameters.");

            queryBuilder.Write("TRIM(TRAILING ");

            if (func.Args.Count > 1)
                queryBuilder.WriteValue(func.Args[1]).Write(" ");

            queryBuilder.Write("FROM ").WriteValue(func.Args[0]).Write(")");
        }

        /// <summary>
        /// Trim both function.
        /// </summary>
        /// <param name="queryBuilder">The query builder.</param>
        /// <param name="engine">The engine.</param>
        /// <param name="name">The SQL function name.</param>
        /// <param name="func">The function.</param>
        public static void TrimBoth(QueryBuilder queryBuilder, IEngine engine, string name, IFunction func)
        {
            if (func.Args.Count > 2)
                throw new CompileException($"Invalid function \"{func.Name}\", wrong number of parameters.");

            queryBuilder.Write("TRIM(");

            if (func.Args.Count > 1)
                queryBuilder.WriteValue(func.Args[1]).Write(" FROM ");

            queryBuilder.WriteValue(func.Args[0]).Write(")");
        }
    }
}