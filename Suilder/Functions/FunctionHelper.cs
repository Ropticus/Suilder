using System.Collections.Generic;
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
        /// Writes a function for a unary operator.
        /// </summary>
        /// <param name="queryBuilder">The query builder.</param>
        /// <param name="engine">The engine.</param>
        /// <param name="name">The SQL function name.</param>
        /// <param name="value">The value.</param>
        public static void UnaryOperator(QueryBuilder queryBuilder, IEngine engine, string name, object value)
        {
            queryBuilder.Write(name + "(").WriteValue(value).Write(")");
        }

        /// <summary>
        /// Writes a function for a binary operator.
        /// </summary>
        /// <param name="queryBuilder">The query builder.</param>
        /// <param name="engine">The engine.</param>
        /// <param name="name">The SQL function name.</param>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        public static void BinaryOperator(QueryBuilder queryBuilder, IEngine engine, string name, object left, object right)
        {
            queryBuilder.Write(name + "(").WriteValue(left).Write(", ").WriteValue(right).Write(")");
        }

        /// <summary>
        /// Writes a function for a ternary operator.
        /// </summary>
        /// <param name="queryBuilder">The query builder.</param>
        /// <param name="engine">The engine.</param>
        /// <param name="name">The SQL function name.</param>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <param name="value3">The third value.</param>
        public static void TernaryOperator(QueryBuilder queryBuilder, IEngine engine, string name, object value1,
            object value2, object value3)
        {
            queryBuilder.Write(name + "(").WriteValue(value1).Write(", ").WriteValue(value2).Write(", ").WriteValue(value3)
                .Write(")");
        }

        /// <summary>
        /// Writes functions for a binary operator with a list of values.
        /// </summary>
        /// <param name="queryBuilder">The query builder.</param>
        /// <param name="engine">The engine.</param>
        /// <param name="name">The SQL function name.</param>
        /// <param name="values">The values.</param>
        /// <typeparam name="T">The type of the values.</typeparam>
        public static void BinaryOperator<T>(QueryBuilder queryBuilder, IEngine engine, string name, IEnumerable<T> values)
        {
            using (IEnumerator<T> enumerator = values.GetEnumerator())
            {
                enumerator.MoveNext();

                while (enumerator.MoveNext())
                {
                    queryBuilder.Write(name + "(");
                }

                enumerator.Reset();
                enumerator.MoveNext();
                queryBuilder.WriteValue(enumerator.Current);

                while (enumerator.MoveNext())
                {
                    queryBuilder.Write(", ").WriteValue(enumerator.Current).Write(")");
                }
            }
        }

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
            for (int i = 0; i < func.Args.Count; i++)
            {
                if (i != 0)
                    queryBuilder.Write(separator);

                queryBuilder.WriteValue(func.Args[i]);
            }

            queryBuilder.Write(")");
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