using System;
using System.Collections.Generic;

namespace Suilder.Core
{
    /// <summary>
    /// A set operator.
    /// </summary>
    public interface ISetOperator : IOperator, ISubQuery
    {
        /// <summary>
        /// Sets a value to write before the set operator.
        /// </summary>
        /// <param name="value">The value to write before the set operator.</param>
        /// <returns>The set operator.</returns>
        ISetOperator Before(IQueryFragment value);

        /// <summary>
        /// Sets a value to write after the set operator.
        /// </summary>
        /// <param name="value">The value to write after the set operator.</param>
        /// <returns>The set operator.</returns>
        ISetOperator After(IQueryFragment value);

        /// <summary>
        /// Sets the "with" clause.
        /// </summary>
        /// <param name="with">The "with" clause.</param>
        /// <returns>The set operator.</returns>
        ISetOperator With(IWith with);

        /// <summary>
        /// Sets a raw "with" clause.
        /// <para>You must write the entire clause including the "with" keyword.</para>
        /// </summary>
        /// <param name="with">The "with" clause.</param>
        /// <returns>The set operator.</returns>
        ISetOperator With(IRawSql with);

        /// <summary>
        /// Sets the "with" clause.
        /// </summary>
        /// <param name="func">Function that returns the "with" clause.</param>
        /// <returns>The set operator.</returns>
        ISetOperator With(Func<IWith, IWith> func);

        /// <summary>
        /// Creates a "with" clause and adds a value to the "with" clause.
        /// </summary>
        /// <param name="value">The value to add to the "with" clause.</param>
        /// <returns>The set operator.</returns>
        ISetOperator With(IQueryFragment value);

        /// <summary>
        /// Creates a "with" clause and adds the elements of the specified array to the end of the "with" clause.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the "with" clause.</param>
        /// <returns>The set operator.</returns>
        ISetOperator With(params IQueryFragment[] values);

        /// <summary>
        /// Creates a "with" clause and adds the elements of the specified collection to the end of the "with"
        /// clause.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the "with" clause.</param>
        /// <returns>The set operator.</returns>
        ISetOperator With(IEnumerable<IQueryFragment> values);

        /// <summary>
        /// Sets the "order by" clause.
        /// </summary>
        /// <param name="orderBy">The "order by" clause.</param>
        /// <returns>The set operator.</returns>
        ISetOperator OrderBy(IOrderBy orderBy);

        /// <summary>
        /// Sets a raw "order by" clause.
        /// <para>You must write the entire clause including the "order by" keyword.</para>
        /// </summary>
        /// <param name="orderBy">The "order by" clause.</param>
        /// <returns>The set operator.</returns>
        ISetOperator OrderBy(IRawSql orderBy);

        /// <summary>
        /// Sets the "order by" clause.
        /// </summary>
        /// <param name="func">Function that returns the "order by" clause.</param>
        /// <returns>The set operator.</returns>
        ISetOperator OrderBy(Func<IOrderBy, IOrderBy> func);

        /// <summary>
        /// Sets the "offset fetch" clause.
        /// </summary>
        /// <param name="offset">The "offset fetch" clause.</param>
        /// <returns>The set operator.</returns>
        ISetOperator Offset(IOffset offset);

        /// <summary>
        /// Sets a raw "offset fetch" clause.
        /// <para>You must write the entire clause.</para>
        /// </summary>
        /// <param name="offset">The "offset fetch" clause.</param>
        /// <returns>The set operator.</returns>
        ISetOperator Offset(IRawSql offset);

        /// <summary>
        /// Creates or adds an "offset" clause.
        /// </summary>
        /// <param name="offset">The number of rows to skip.</param>
        /// <returns>The set operator.</returns>
        ISetOperator Offset(object offset);

        /// <summary>
        /// Creates an "offset fetch" clause.
        /// </summary>
        /// <param name="offset">The number of rows to skip.</param>
        /// <param name="fetch">The number of rows to return.</param>
        /// <returns>The set operator.</returns>
        ISetOperator Offset(object offset, object fetch);

        /// <summary>
        /// Creates or adds "fetch" clause.
        /// </summary>
        /// <param name="fetch">The number of rows to return.</param>
        /// <returns>The set operator.</returns>
        ISetOperator Fetch(object fetch);
    }
}