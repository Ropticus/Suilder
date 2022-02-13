using System;
using System.Diagnostics;
using System.Linq.Expressions;
using Suilder.Builder;
using Suilder.Engines;
using Suilder.Reflection.Builder;

namespace Suilder.Core
{
    /// <summary>
    /// Implementation of <see cref="IAlias"/> for an entity type.
    /// </summary>
    public class EntityAlias : IAlias
    {
        /// <summary>
        /// The table type.
        /// </summary>
        /// <value>The table type.</value>
        protected Type Type { get; set; }

        /// <summary>
        /// The alias name.
        /// </summary>
        /// <value>The alias name.</value>
        public string AliasName { get; protected set; }

        /// <summary>
        /// The alias name or null.
        /// </summary>
        /// <value>The alias name or null.</value>
        public string AliasOrTableName => AliasName;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityAlias"/> class.
        /// </summary>
        /// <param name="type">The table type.</param>
        public EntityAlias(Type type)
        {
            Type = type;
            AliasName = type.Name;
            AliasName = char.ToLowerInvariant(AliasName[0]) + AliasName.Substring(1);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityAlias"/> class.
        /// </summary>
        /// <param name="type">The table type.</param>
        /// <param name="aliasName">The alias name.</param>
        public EntityAlias(Type type, string aliasName)
        {
            Type = type;
            AliasName = aliasName;
        }

        /// <summary>
        /// Creates a column with the alias.
        /// </summary>
        /// <param name="columnName">The column name.</param>
        /// <returns>The column.</returns>
        public virtual IColumn Col(string columnName)
        {
            return SqlBuilder.Instance.Col(Type, AliasName, columnName);
        }

        /// <summary>
        /// Creates a column with the alias.
        /// </summary>
        /// <param name="columnName">The column name.</param>
        /// <returns>The column.</returns>
        public virtual IColumn this[string columnName] => Col(columnName);

        /// <summary>
        /// Creates a select all column with the alias.
        /// </summary>
        /// <value>The column.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public virtual IColumn All => Col("*");

        /// <summary>
        /// Compiles the fragment.
        /// </summary>
        /// <param name="queryBuilder">The query builder.</param>
        /// <param name="engine">The engine.</param>
        public virtual void Compile(QueryBuilder queryBuilder, IEngine engine)
        {
            ITableInfo tableInfo = engine.GetInfo(Type);

            if (!string.IsNullOrEmpty(tableInfo.Schema))
                queryBuilder.WriteName(tableInfo.Schema).Write(".");

            queryBuilder.WriteName(tableInfo.TableName);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return ToStringBuilder.Build(b => b.Write(Type.Name));
        }
    }

    /// <summary>
    /// Implementation of <see cref="IAlias{T}"/>.
    /// </summary>
    /// <typeparam name="T">The table type.</typeparam>
    public class EntityAlias<T> : EntityAlias, IAlias<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityAlias{T}"/> class.
        /// </summary>
        public EntityAlias() : base(typeof(T))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityAlias{T}"/> class.
        /// </summary>
        /// <param name="aliasName">The alias name.</param>
        public EntityAlias(string aliasName) : base(typeof(T), aliasName)
        {
        }

        /// <summary>
        /// Creates a column with the alias.
        /// </summary>
        /// <param name="column">The column property.</param>
        /// <returns>The column.</returns>
        public virtual IColumn Col(Expression<Func<T, object>> column)
        {
            return SqlBuilder.Instance.Col(AliasName, column);
        }

        /// <summary>
        /// Creates a column with the alias.
        /// </summary>
        /// <param name="column">The column property.</param>
        /// <returns>The column.</returns>
        public virtual IColumn this[Expression<Func<T, object>> column] => Col(column);
    }
}