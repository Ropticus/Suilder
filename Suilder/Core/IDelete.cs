using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Suilder.Core
{
    /// <summary>
    /// A "delete" statement.
    /// <para>Contains only the options of the "delete" statement.</para>
    /// <para>Does not contain the "from" clause.</para>
    /// </summary>
    public interface IDelete : IQueryFragmentList<IAlias, object>
    {
        /// <summary>
        /// The alias to delete.
        /// </summary>
        /// <value>The alias to delete.</value>
        IEnumerable<IAlias> Alias { get; }

        /// <summary>
        /// Adds a "top" clause.
        /// </summary>
        /// <param name="top">The "top" clause.</param>
        /// <returns>The "delete" statement.</returns>
        IDelete Top(ITop top);

        /// <summary>
        /// Adds a raw "top" clause.
        /// <para>You must write the entire clause.</para>
        /// </summary>
        /// <param name="top">The "top" clause.</param>
        /// <returns>The "delete" statement.</returns>
        IDelete Top(IRawSql top);

        /// <summary>
        /// Adds a "top" clause.
        /// </summary>
        /// <param name="fetch">The number of rows to return.</param>
        /// <returns>The "delete" statement.</returns>
        IDeleteTop Top(object fetch);

        /// <summary>
        /// Adds a value to the <see cref="IDelete"/>.
        /// </summary>
        /// <param name="value">The value to add to the <see cref="IDelete"/>.</param>
        /// <returns>The "delete" statement.</returns>
        new IDelete Add(IAlias value);

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="IDelete"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// <see cref="IDelete"/>.</param>
        /// <returns>The "delete" statement.</returns>
        new IDelete Add(params IAlias[] values);

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="IDelete"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the
        /// <see cref="IDelete"/>.</param>
        /// <returns>The "delete" statement.</returns>
        new IDelete Add(IEnumerable<IAlias> values);

        /// <summary>
        /// Adds a value to the <see cref="IDelete"/>.
        /// </summary>
        /// <param name="value">The value to add to the <see cref="IDelete"/>.</param>
        /// <returns>The "delete" statement.</returns>
        new IDelete Add(Expression<Func<object>> value);

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="IDelete"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// <see cref="IDelete"/>.</param>
        /// <returns>The "delete" statement.</returns>
        new IDelete Add(params Expression<Func<object>>[] values);

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="IDelete"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the
        /// <see cref="IDelete"/>.</param>
        /// <returns>The "delete" statement.</returns>
        new IDelete Add(IEnumerable<Expression<Func<object>>> values);
    }

    /// <summary>
    /// A "delete" statement with options for the "top" clause.
    /// </summary>
    public interface IDeleteTop : IDelete
    {
        /// <summary>
        /// Return a percent of rows.
        /// </summary>
        /// <param name="percent">If return a percent of rows.</param>
        /// <returns>The "delete" statement.</returns>
        IDeleteTop Percent(bool percent = true);

        /// <summary>
        /// Return two or more rows that tie for last place.
        /// </summary>
        /// <param name="withTies">If return two or more rows that tie for last place.</param>
        /// <returns>The "delete" statement.</returns>
        IDeleteTop WithTies(bool withTies = true);
    }
}