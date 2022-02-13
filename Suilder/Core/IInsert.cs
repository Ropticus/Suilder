using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Suilder.Core
{
    /// <summary>
    /// An "insert" statement.
    /// <para>Contains only the options of the "insert" statement.</para>
    /// <para>Does not contain the "values" clause.</para>
    /// </summary>
    public interface IInsert : IColList
    {
        /// <summary>
        /// Sets the "into" value.
        /// </summary>
        /// <param name="tableName">The table name.</param>
        /// <returns>The "insert" statement.</returns>
        IInsert Into(string tableName);

        /// <summary>
        /// Sets the "into" value.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <returns>The "insert" statement.</returns>
        IInsert Into(IAlias alias);

        /// <summary>
        /// Sets the "into" value.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <typeparam name="T">The type of the table.</typeparam>
        /// <returns>The "insert" statement.</returns>
        IInsert Into<T>(Expression<Func<T>> alias);

        /// <summary>
        /// Adds a value to the end of the <see cref="IInsert"/>.
        /// </summary>
        /// <param name="value">The value to add to the end of the <see cref="IInsert"/>.</param>
        /// <returns>The "insert" statement.</returns>
        new IInsert Add(IColumn value);

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="IInsert"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// <see cref="IInsert"/>.</param>
        /// <returns>The "insert" statement.</returns>
        new IInsert Add(params IColumn[] values);

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="IInsert"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the
        /// <see cref="IInsert"/>.</param>
        /// <returns>The "insert" statement.</returns>
        new IInsert Add(IEnumerable<IColumn> values);

        /// <summary>
        /// Adds a value to the end of the <see cref="IInsert"/>.
        /// </summary>
        /// <param name="value">The value to add to the end of the <see cref="IInsert"/>.</param>
        /// <returns>The "insert" statement.</returns>
        new IInsert Add(Expression<Func<object>> value);

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="IInsert"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// <see cref="IInsert"/>.</param>
        /// <returns>The "insert" statement.</returns>
        new IInsert Add(params Expression<Func<object>>[] values);

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="IInsert"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the
        /// <see cref="IInsert"/>.</param>
        /// <returns>The "insert" statement.</returns>
        new IInsert Add(IEnumerable<Expression<Func<object>>> values);
    }
}