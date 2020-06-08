using System;
using System.Linq.Expressions;

namespace Suilder.Core
{
    /// <summary>
    /// A table or a view and his alias.
    /// <para>Can be also used as an alias of a subquery.</para>
    /// <para>Allows you to create an <see cref="IColumn"/> instance with the same alias.</para>
    /// <para>When compiled only writes the table name.</para>
    /// </summary>
    public interface IAlias : IQueryFragment
    {
        /// <summary>
        /// The alias name.
        /// </summary>
        /// <value>The alias name.</value>
        string AliasName { get; }

        /// <summary>
        /// The alias name or the table name, or null if cannot be obtained without compile.
        /// </summary>
        /// <value>The alias name or the table name, or null if cannot be obtained without compile.</value>
        string AliasOrTableName { get; }

        /// <summary>
        /// Creates a column with the alias.
        /// </summary>
        /// <param name="columnName">The column name.</param>
        /// <returns>The column.</returns>
        IColumn Col(string columnName);

        /// <summary>
        /// Creates a column with the alias.
        /// </summary>
        /// <param name="columnName">The column name.</param>
        /// <returns>The column.</returns>
        IColumn this[string columnName] { get; }

        /// <summary>
        /// Creates a select all column with the alias.
        /// </summary>
        /// <value>The column.</value>
        IColumn All { get; }
    }

    /// <summary>
    /// A table or a view and his alias.
    /// <para>Can be also used as an alias of a subquery.</para>
    /// <para>Allows you to create an <see cref="IColumn"/> instance with the same alias.</para>
    /// <para>When compiled only writes the table name.</para>
    /// </summary>
    /// <typeparam name="T">The table type.</typeparam>
    public interface IAlias<T> : IAlias
    {
        /// <summary>
        /// Creates a column with the alias.
        /// </summary>
        /// <param name="column">The column property.</param>
        /// <returns>The column.</returns>
        IColumn Col(Expression<Func<T, object>> column);

        /// <summary>
        /// Creates a column with the alias.
        /// </summary>
        /// <param name="column">The column property.</param>
        /// <returns>The column.</returns>
        IColumn this[Expression<Func<T, object>> column] { get; }
    }
}