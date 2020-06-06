using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Exceptions;
using Suilder.Functions;
using Suilder.Reflection.Builder;

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
        protected IDictionary<string, ITableInfo> Tables { get; set; }

        /// <summary>
        /// The registered functions.
        /// </summary>
        /// <value>The registered functions.</value>
        protected IDictionary<string, IFunctionData> Functions { get; set; } = new Dictionary<string, IFunctionData>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Engine"/> class.
        /// </summary>
        public Engine()
        {
            Tables = new Dictionary<string, ITableInfo>();
            Options = InitOptions();
            InitFunctions();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Engine"/> class.
        /// </summary>
        /// <param name="configBuilder">The config builder.</param>
        public Engine(IConfigBuilder configBuilder) : this()
        {
            Tables = configBuilder.GetConfig().ToDictionary(x => x.Type.FullName, x => x);
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
            AddFunction(FunctionName.Cast, FunctionHelper.Cast);
        }

        /// <summary>
        /// Compiles an <see cref="IQueryFragment"/> to a <see cref="QueryResult"/>.
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
        /// <returns><see langword="true"/> if the function is registered, otherwise, <see langword="false"/>.</returns>
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
            Functions.TryGetValue(name, out IFunctionData func);
            return func;
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
        /// Gets the registered types.
        /// </summary>
        /// <returns>The registered types.</returns>
        public virtual Type[] GetRegisteredTypes()
        {
            return Tables.Select(x => x.Value.Type).ToArray();
        }

        /// <summary>
        /// Gets the information of the table.
        /// </summary>
        /// <param name="type">The table type.</param>
        /// <returns>The table info.</returns>
        /// <exception cref="InvalidConfigurationException">The type is not registered.</exception>
        public virtual ITableInfo GetInfo(Type type)
        {
            if (Tables.TryGetValue(type.FullName, out ITableInfo tableInfo))
                return tableInfo;

            throw new InvalidConfigurationException($"The type \"{type}\" is not registered.");
        }

        /// <summary>
        /// Gets the information of the table.
        /// </summary>
        /// <typeparam name="T">The table type.</typeparam>
        /// <returns>The table info.</returns>
        /// <exception cref="InvalidConfigurationException">The type is not registered.</exception>
        public virtual ITableInfo<T> GetInfo<T>() => (ITableInfo<T>)GetInfo(typeof(T));

        /// <summary>
        /// Gets the information of the table or <see langword="null"/> if no element is found.
        /// </summary>
        /// <param name="type">The table type.</param>
        /// <returns>The table info.</returns>
        public virtual ITableInfo GetInfoOrDefault(Type type)
        {
            Tables.TryGetValue(type.FullName, out ITableInfo tableInfo);
            return tableInfo;
        }

        /// <summary>
        /// Gets the information of the table or <see langword="null"/> if no element is found.
        /// </summary>
        /// <typeparam name="T">The table type.</typeparam>
        /// <returns>The table info.</returns>
        public virtual ITableInfo<T> GetInfoOrDefault<T>() => (ITableInfo<T>)GetInfoOrDefault(typeof(T));

        /// <summary>
        /// Checks if the type is a table.
        /// </summary>
        /// <param name="type">The table type.</param>
        /// <returns><see langword="true"/> if the type is a table, otherwise, <see langword="false"/>.</returns>
        public virtual bool IsTable(Type type)
        {
            return Tables.ContainsKey(type.FullName);
        }

        /// <summary>
        /// Checks if the type is a table.
        /// </summary>
        /// <typeparam name="T">The table type.</typeparam>
        /// <returns><see langword="true"/> if the type is a table, otherwise, <see langword="false"/>.</returns>
        public virtual bool IsTable<T>() => IsTable(typeof(T));
    }
}