using System;
using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Functions;

namespace Suilder.Engines
{
    /// <summary>
    /// Contains the configuration of a SQL engine.
    /// </summary>
    public interface IEngine
    {
        /// <summary>
        /// The engine options.
        /// </summary>
        /// <value>The engine options.</value>
        EngineOptions Options { get; }

        /// <summary>
        /// Compiles a <see cref="IQueryFragment"/> to a <see cref="QueryResult"/>.
        /// </summary>
        /// <param name="query">The <see cref="IQueryFragment"/>.</param>
        /// <returns>The <see cref="QueryResult"/>.</returns>
        QueryResult Compile(IQueryFragment query);

        /// <summary>
        /// Register a function in the engine.
        /// </summary>
        /// <param name="name">The name of the function.</param>
        void AddFunction(string name);

        /// <summary>
        /// Register a function in the engine.
        /// </summary>
        /// <param name="name">The name of the function.</param>
        /// <param name="nameSql">The tranlated name of the function.</param>
        void AddFunction(string name, string nameSql);

        /// <summary>
        /// Register a function in the engine.
        /// </summary>
        /// <param name="name">The name of the function.</param>
        /// <param name="func">A custom delegate to compile the function.</param>
        void AddFunction(string name, FunctionCompile func);

        /// <summary>
        /// Register a function in the engine.
        /// </summary>
        /// <param name="name">The name of the function.</param>
        /// <param name="nameSql">The tranlated name of the function.</param>
        /// <param name="func">A custom delegate to compile the function.</param>
        void AddFunction(string name, string nameSql, FunctionCompile func);

        /// <summary>
        /// Removes a registered function.
        /// </summary>
        /// <param name="name">The name of the function.</param>
        void RemoveFunction(string name);

        /// <summary>
        ///  Determines if the function is registered.
        /// </summary>
        /// <param name="name">The name of the function.</param>
        /// <returns>True if the function is registered.</returns>
        bool ContainsFunction(string name);

        /// <summary>
        /// Removes all registered functions.
        /// </summary>
        void ClearFunctions();

        /// <summary>
        /// Gets the function data.
        /// </summary>
        /// <param name="name">The name of the function.</param>
        /// <returns>The function data.</returns>
        IFunctionData GetFunction(string name);

        /// <summary>
        /// Escape a name.
        /// </summary>
        /// <param name="name">The name to escape.</param>
        /// <returns>The escaped name.</returns>
        string EscapeName(string name);

        /// <summary>
        /// Checks if the <see cref="Type"/> is a table.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>True if the <see cref="Type"/> is a table.</returns>
        bool IsTable(Type type);

        /// <summary>
        /// Gets the table name of a <see cref="Type"/>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The table name.</returns>
        string GetTableName(Type type);

        /// <summary>
        /// Gets the primary key properties of a <see cref="Type"/>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The primary key properties.</returns>
        string[] GetPrimaryKeys(Type type);

        /// <summary>
        /// Gets the column properties of a <see cref="Type"/>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The column properties.</returns>
        string[] GetColumns(Type type);

        /// <summary>
        /// Gets the column names of a <see cref="Type"/>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The column names-</returns>
        string[] GetColumnNames(Type type);

        /// <summary>
        /// Gets the column name of a property.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>The column name.</returns>
        string GetColumnName(Type type, string propertyName);

        /// <summary>
        /// Gets the column names of the properties.
        /// <para>The key is the column property, the value is the column name.</para>
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The column names of the properties.</returns>
        IReadOnlyDictionary<string, string> GetColumnNamesDic(Type type);
    }
}