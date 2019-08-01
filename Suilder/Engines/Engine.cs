using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Exceptions;
using Suilder.Functions;
using Suilder.Reflection;

namespace Suilder.Engines
{
    /// <summary>
    /// Implementation of <see cref="IEngine"/>.
    /// </summary>
    public class Engine : IEngine
    {
        /// <summary>
        /// The engine options.
        /// </summary>
        /// <value>The engine options.</value>
        public EngineOptions Options { get; set; }

        /// <summary>
        /// Contains the information of all tables.
        /// </summary>
        /// <value>Contains the information of all tables.</value>
        protected IDictionary<string, TableInfo> Tables { get; set; }

        /// <summary>
        /// The registered functions.
        /// </summary>
        /// <value>The registered functions.</value>
        protected IDictionary<string, FunctionData> Functions { get; set; } = new Dictionary<string, FunctionData>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Engine"/> class.
        /// </summary>
        public Engine()
        {
            Tables = new Dictionary<string, TableInfo>();
            Options = InitOptions();
            InitFunctions();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Engine"/> class.
        /// </summary>
        /// <param name="tableBuilder">The table builder.</param>
        public Engine(ITableBuilder tableBuilder) : this()
        {
            Tables = tableBuilder.GetConfig().ToDictionary(x => x.Type.FullName, x => x);
        }

        /// <summary>
        /// Initializes the engine options.
        /// </summary>
        /// <returns>The engine options.</returns>
        protected virtual EngineOptions InitOptions()
        {
            EngineOptions options = new EngineOptions();
            options.EscapeStart = '"';
            options.EscapeEnd = '"';
            options.OffsetStyle = OffsetStyle.Offset;
            options.TopSupported = true;
            options.DistinctOnSupported = true;
            options.FullJoinSupported = true;

            return options;
        }

        /// <summary>
        /// Initializes the functions of the engine.
        /// </summary>
        protected virtual void InitFunctions()
        {
        }

        /// <summary>
        /// Compiles a <see cref="IQueryFragment"/> to a <see cref="QueryResult"/>.
        /// </summary>
        /// <param name="query">The <see cref="IQueryFragment"/>.</param>
        /// <returns>The <see cref="QueryResult"/>.</returns>
        public virtual QueryResult Compile(IQueryFragment query)
        {
            QueryBuilder queryBuilder = new QueryBuilder(this);
            query.Compile(queryBuilder, this);
            return queryBuilder.ToQueryResult();
        }

        /// <summary>
        /// Register a function in the engine.
        /// </summary>
        /// <param name="name">The name of the function.</param>
        public virtual void AddFunction(string name)
        {
            AddFunction(name, name, null);
        }

        /// <summary>
        /// Register a function in the engine.
        /// </summary>
        /// <param name="name">The name of the function.</param>
        /// <param name="nameSql">The tranlated name of the function.</param>
        public virtual void AddFunction(string name, string nameSql)
        {
            AddFunction(name, nameSql, null);
        }

        /// <summary>
        /// Register a function in the engine.
        /// </summary>
        /// <param name="name">The name of the function.</param>
        /// <param name="func">A custom delegate to compile the function.</param>
        public virtual void AddFunction(string name, FunctionCompile func)
        {
            AddFunction(name, name, func);
        }

        /// <summary>
        /// Register a function in the engine.
        /// </summary>
        /// <param name="name">The name of the function.</param>
        /// <param name="nameSql">The tranlated name of the function.</param>
        /// <param name="func">A custom delegate to compile the function.</param>
        public virtual void AddFunction(string name, string nameSql, FunctionCompile func)
        {
            Functions[name.ToUpperInvariant()] = new FunctionData()
            {
                Name = nameSql,
                Compile = func
            };
        }

        /// <summary>
        /// Removes a registered function.
        /// </summary>
        /// <param name="name">The name of the function.</param>
        public virtual void RemoveFunction(string name)
        {
            Functions.Remove(name.ToUpperInvariant());
        }

        /// <summary>
        ///  Determines if the function is registered.
        /// </summary>
        /// <param name="name">The name of the function.</param>
        /// <returns>True if the function is registered.</returns>
        public virtual bool ContainsFunction(string name)
        {
            return Functions.ContainsKey(name.ToUpperInvariant());
        }

        /// <summary>
        /// Removes all registered functions.
        /// </summary>
        public virtual void ClearFunctions()
        {
            Functions.Clear();
        }

        /// <summary>
        /// Gets the function data.
        /// </summary>
        /// <param name="name">The name of the function.</param>
        /// <returns>The function data.</returns>
        public virtual IFunctionData GetFunction(string name)
        {
            name = name.ToUpperInvariant();
            return Functions.ContainsKey(name) ? Functions[name] : null;
        }

        /// <summary>
        /// Escape a name.
        /// </summary>
        /// <param name="name">The name to escape.</param>
        /// <returns>The escaped name.</returns>
        public virtual string EscapeName(string name)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(Options.EscapeStart);
            foreach (char ch in name)
            {
                if (ch == '.')
                {
                    builder.Append(Options.EscapeEnd);
                    builder.Append(ch);
                    builder.Append(Options.EscapeStart);
                }
                else if (ch != Options.EscapeStart && ch != Options.EscapeEnd)
                {
                    builder.Append(ch);
                }
            }
            builder.Append(Options.EscapeEnd);

            if (Options.UpperCaseNames)
                return builder.ToString().ToUpperInvariant();
            else if (Options.LowerCaseNames)
                return builder.ToString().ToLowerInvariant();
            else
                return builder.ToString();
        }

        /// <summary>
        /// Checks if the <see cref="Type"/> is a table.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>True if the <see cref="Type"/> is a table.</returns>
        public virtual bool IsTable(Type type)
        {
            return Tables.ContainsKey(type.FullName);
        }

        /// <summary>
        /// Gets the table name of a <see cref="Type"/>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The table name.</returns>
        public virtual string GetTableName(Type type)
        {
            if (Tables.TryGetValue(type.FullName, out TableInfo tableInfo))
                return tableInfo.TableName;

            throw new InvalidConfigurationException($"Type \"{type.FullName}\" is not registered.");
        }

        /// <summary>
        /// Gets the primary key properties of a <see cref="Type"/>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The primary key properties.</returns>
        public virtual string[] GetPrimaryKeys(Type type)
        {
            if (Tables.TryGetValue(type.FullName, out TableInfo tableInfo))
                return tableInfo.PrimaryKeys;

            throw new InvalidConfigurationException($"Type \"{type.FullName}\" is not registered.");
        }

        /// <summary>
        /// Gets the column properties of a <see cref="Type"/>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The column properties.</returns>
        public virtual string[] GetColumns(Type type)
        {
            if (Tables.TryGetValue(type.FullName, out TableInfo tableInfo))
                return tableInfo.Columns;

            throw new InvalidConfigurationException($"Type \"{type.FullName}\" is not registered.");
        }

        /// <summary>
        /// Gets the column names of a <see cref="Type"/>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The column names-</returns>
        public virtual string[] GetColumnNames(Type type)
        {
            if (Tables.TryGetValue(type.FullName, out TableInfo tableInfo))
                return tableInfo.ColumnNames;

            throw new InvalidConfigurationException($"Type \"{type.FullName}\" is not registered.");
        }

        /// <summary>
        /// Gets the column name of a property.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>The column name.</returns>
        public virtual string GetColumnName(Type type, string propertyName)
        {
            if (Tables.TryGetValue(type.FullName, out TableInfo tableInfo))
            {
                if (tableInfo.ColumnNamesDic.TryGetValue(propertyName, out string columnName))
                    return columnName;

                throw new InvalidConfigurationException(
                    $"Property \"{propertyName}\" for type \"{type.FullName}\" is not registered.");
            }

            throw new InvalidConfigurationException($"Type \"{type.FullName}\" is not registered.");
        }

        /// <summary>
        /// Gets the column names of the properties.
        /// <para>The key is the column property, the value is the column name.</para>
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The column names of the properties.</returns>
        public virtual IReadOnlyDictionary<string, string> GetColumnNamesDic(Type type)
        {
            if (Tables.TryGetValue(type.FullName, out TableInfo tableInfo))
            {
                return tableInfo.ColumnNamesDic as IReadOnlyDictionary<string, string>
                    ?? tableInfo.ColumnNamesDic.ToDictionary(x => x.Key, x => x.Value);
            }

            throw new InvalidConfigurationException($"Type \"{type.FullName}\" is not registered.");
        }
    }
}