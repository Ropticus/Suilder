using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Suilder.Core
{
    /// <summary>
    /// A "select" statement.
    /// </summary>
    public interface ISelect : IValList
    {
        /// <summary>
        /// Sets a distinct result.
        /// </summary>
        /// <param name="distinct">If the select is distinct.</param>
        /// <returns>The "select" statement.</returns>
        ISelect Distinct(bool distinct = true);

        /// <summary>
        /// Sets a "distinct on" clause.
        /// </summary>
        /// <param name="value">The value to add to the "distinct on".</param>
        /// <returns>The "select" statement.</returns>
        ISelect DistinctOn(object value);

        /// <summary>
        /// Sets a "distinct on" clause.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the "distinct on".</param>
        /// <returns>The "select" statement.</returns>
        ISelect DistinctOn(params object[] values);

        /// <summary>
        /// Sets a "distinct on" clause.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the "distinct on".</param>
        /// <returns>The "select" statement.</returns>
        ISelect DistinctOn(IEnumerable<object> values);

        /// <summary>
        /// Sets a "distinct on" clause.
        /// </summary>
        /// <param name="value">The value to add to the "distinct on".</param>
        /// <returns>The "select" statement.</returns>
        ISelect DistinctOn(Expression<Func<object>> value);

        /// <summary>
        /// Sets a "distinct on" clause.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the "distinct on".</param>
        /// <returns>The "select" statement.</returns>
        ISelect DistinctOn(params Expression<Func<object>>[] values);

        /// <summary>
        /// Sets a "distinct on" clause.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the "distinct on".</param>
        /// <returns>The "select" statement.</returns>
        ISelect DistinctOn(IEnumerable<Expression<Func<object>>> values);

        /// <summary>
        /// Sets a "distinct on" clause.
        /// </summary>
        /// <param name="value">The list of columns.</param>
        /// <returns>The "select" statement.</returns>
        ISelect DistinctOn(IValList value);

        /// <summary>
        /// Sets a "distinct on" clause.
        /// </summary>
        /// <param name="func">Function that returns the list of columns.</param>
        /// <returns>The "select" statement.</returns>
        ISelect DistinctOn(Func<IValList, IValList> func);

        /// <summary>
        /// Adds a "top" clause.
        /// </summary>
        /// <param name="top">The "top" clause.</param>
        /// <returns>The "select" statement.</returns>
        ISelect Top(ITop top);

        /// <summary>
        /// Adds a raw "top" clause.
        /// <para>You must write the entire clause.</para>
        /// </summary>
        /// <param name="top">The "top" clause.</param>
        /// <returns>The "select" statement.</returns>
        ISelect Top(IRawSql top);

        /// <summary>
        /// Adds a "top" clause.
        /// </summary>
        /// <param name="fetch">The number of rows to return.</param>
        /// <returns>The "select" statement.</returns>
        ISelectTop Top(object fetch);

        /// <summary>
        /// Adds a value to the end of the <see cref="ISelect"/>.
        /// </summary>
        /// <param name="value">The value to add to the end of the <see cref="ISelect"/>.</param>
        /// <returns>The "select" statement.</returns>
        new ISelect Add(object value);

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="ISelect"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// <see cref="ISelect"/>.</param>
        /// <returns>The "select" statement.</returns>
        new ISelect Add(params object[] values);

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="ISelect"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the
        /// <see cref="ISelect"/>.</param>
        /// <returns>The "select" statement.</returns>
        new ISelect Add(IEnumerable<object> values);

        /// <summary>
        /// Adds a value to the end of the <see cref="ISelect"/>.
        /// </summary>
        /// <param name="value">The value to add to the end of the <see cref="ISelect"/>.</param>
        /// <returns>The "select" statement.</returns>
        new ISelect Add(Expression<Func<object>> value);

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="ISelect"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// <see cref="ISelect"/>.</param>
        /// <returns>The "select" statement.</returns>
        new ISelect Add(params Expression<Func<object>>[] values);

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="ISelect"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the
        /// <see cref="ISelect"/>.</param>
        /// <returns>The "select" statement.</returns>
        new ISelect Add(IEnumerable<Expression<Func<object>>> values);

        /// <summary>
        /// Adds an alias to the latest column added with <see cref="o:Add"/> method.
        /// </summary>
        /// <param name="aliasName">The alias name.</param>
        /// <returns>The "select" statement.</returns>
        /// <exception cref="InvalidOperationException">The list is empty or there is a select all column.</exception>
        ISelect As(string aliasName);

        /// <summary>
        /// Adds an "over" clause to the latest column added with <see cref="o:Add"/> method.
        /// </summary>
        /// <returns>The "select" statement.</returns>
        /// <exception cref="InvalidOperationException">The list is empty or there is a select all column.</exception>
        ISelect Over();

        /// <summary>
        /// Adds an "over" clause to the latest column added with <see cref="o:Add"/> method.
        /// </summary>
        /// <param name="over">The over value.</param>
        /// <returns>The "select" statement.</returns>
        /// <exception cref="InvalidOperationException">The list is empty or there is a select all column.</exception>
        ISelect Over(IQueryFragment over);

        /// <summary>
        /// Adds an "over" clause to the latest column added with <see cref="o:Add"/> method.
        /// </summary>
        /// <param name="func">Function with the <see cref="IOver"/> value.</param>
        /// <returns>The "select" statement.</returns>
        /// <exception cref="InvalidOperationException">The list is empty or there is a select all column.</exception>
        ISelect Over(Func<IOver, IOver> func);
    }

    /// <summary>
    /// A "select" statement with options for the "top" clause.
    /// </summary>
    public interface ISelectTop : ISelect
    {
        /// <summary>
        /// Return a percent of rows.
        /// </summary>
        /// <param name="percent">If return a percent of rows.</param>
        /// <returns>The "select" statement.</returns>
        ISelectTop Percent(bool percent = true);

        /// <summary>
        /// Return two or more rows that tie for last place.
        /// </summary>
        /// <param name="withTies">If return two or more rows that tie for last place.</param>
        /// <returns>The "select" statement.</returns>
        ISelectTop WithTies(bool withTies = true);
    }
}