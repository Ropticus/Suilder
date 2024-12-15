using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using Suilder.Core;

namespace Suilder.Builder
{
    public static partial class ExpressionProcessor
    {
        /// <summary>
        /// Contains the types registered as a table.
        /// </summary>
        private static ISet<string> Tables { get; set; } = new HashSet<string>();

        /// <summary>
        /// The registered functions.
        /// </summary>
        /// <returns>The registered functions.</returns>
        private static IDictionary<string, Func<MethodCallExpression, object>> Functions { get; set; }
            = new ConcurrentDictionary<string, Func<MethodCallExpression, object>>();

        /// <summary>
        /// Gets the full method name.
        /// </summary>
        /// <param name="type">The type of the class of the method.</param>
        /// <param name="methodName">The method name.</param>
        /// <returns>The full method name.</returns>
        private static string GetMethodFullName(Type type, string methodName)
        {
            return type.FullName + "." + methodName;
        }

        /// <summary>
        /// Gets the full method name of a <see cref="MethodCallExpression"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The full method name.</returns>
        private static string GetMethodFullName(MethodCallExpression expression)
        {
            return GetMethodFullName(expression.Method.DeclaringType, expression.Method.Name);
        }

        /// <summary>
        /// Registers a function to compile it into an <see cref="IQueryFragment"/>.
        /// </summary>
        /// <param name="type">The type of the class of the method.</param>
        /// <param name="methodName">The method name.</param>
        public static void AddFunction(Type type, string methodName)
        {
            AddFunction(type, methodName, methodName);
        }

        /// <summary>
        /// Registers a function to compile it into an <see cref="IQueryFragment"/>.
        /// </summary>
        /// <param name="type">The type of the class of the method.</param>
        /// <param name="methodName">The method name.</param>
        /// <param name="nameSql">The method name in SQL.</param>
        public static void AddFunction(Type type, string methodName, string nameSql)
        {
            Functions[GetMethodFullName(type, methodName)] =
                x => ExpressionHelper.Function(x, nameSql.ToUpperInvariant());
        }

        /// <summary>
        /// Registers a function to compile it into an <see cref="IQueryFragment"/>.
        /// </summary>
        /// <param name="type">The type of the class of the method.</param>
        /// <param name="methodName">The method name.</param>
        /// <param name="func">A custom delegate to compile the expression.</param>
        public static void AddFunction(Type type, string methodName, Func<MethodCallExpression, object> func)
        {
            Functions[GetMethodFullName(type, methodName)] = func;
        }

        /// <summary>
        /// Registers a function to compile it into an <see cref="IQueryFragment"/>.
        /// </summary>
        /// <param name="type">The type of the class of the method.</param>
        /// <param name="methodName">The method name.</param>
        /// <param name="func">A custom delegate to compile the expression.</param>
        public static void AddFunction(Type type, string methodName,
            Func<MethodCallExpression, string, IQueryFragment> func)
        {
            Functions[GetMethodFullName(type, methodName)] = x => func(x, methodName.ToUpperInvariant());
        }

        /// <summary>
        /// Registers a function to compile it into an <see cref="IQueryFragment"/>.
        /// </summary>
        /// <param name="type">The type of the class of the method.</param>
        /// <param name="methodName">The method name.</param>
        /// <param name="nameSql">The method name in SQL.</param>
        /// <param name="func">A custom delegate to compile the expression.</param>
        public static void AddFunction(Type type, string methodName, string nameSql,
            Func<MethodCallExpression, string, IQueryFragment> func)
        {
            Functions[GetMethodFullName(type, methodName)] = x => func(x, nameSql);
        }

        /// <summary>
        /// Removes a registered function.
        /// </summary>
        /// <param name="type">The type of the class of the method.</param>
        /// <param name="methodName">The method name.</param>
        public static void RemoveFunction(Type type, string methodName)
        {
            Functions.Remove(GetMethodFullName(type, methodName));
        }

        /// <summary>
        /// Determines if the function is registered.
        /// </summary>
        /// <param name="type">The type of the class of the method.</param>
        /// <param name="methodName">The method name.</param>
        /// <returns><see langword="true"/> if the function is registered, otherwise, <see langword="false"/>.</returns>
        public static bool ContainsFunction(Type type, string methodName)
        {
            return Functions.ContainsKey(GetMethodFullName(type, methodName));
        }

        /// <summary>
        /// Removes all registered functions.
        /// </summary>
        public static void ClearFunctions()
        {
            Functions.Clear();
        }

        /// <summary>
        /// Registers a type as a table.
        /// </summary>
        /// <param name="type">The type.</param>
        public static void AddTable(Type type)
        {
            lock (Tables)
            {
                Tables.Add(type.FullName);
            }
        }

        /// <summary>
        /// Removes a registered type.
        /// </summary>
        /// <param name="type">The type.</param>
        public static void RemoveTable(Type type)
        {
            lock (Tables)
            {
                Tables.Remove(type.FullName);
            }
        }

        /// <summary>
        /// Determines if the type is registered.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><see langword="true"/> if the type is registered, otherwise, <see langword="false"/>.</returns>
        public static bool ContainsTable(Type type)
        {
            return Tables.Contains(type.FullName);
        }

        /// <summary>
        /// Removes all registered types.
        /// </summary>
        public static void ClearTables()
        {
            lock (Tables)
            {
                Tables.Clear();
            }
        }
    }
}