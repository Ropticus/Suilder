using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Suilder.Core
{
    /// <summary>
    /// A "order by" clause.
    /// </summary>
    public interface IOrderBy : IValList
    {
        /// <summary>
        /// Adds a value to the <see cref="IOrderBy"/>.
        /// </summary>
        /// <param name="value">The value to add to the <see cref="IOrderBy"/>.</param>
        /// <returns>The "order by" clause.</returns>
        new IOrderBy Add(object value);

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="IOrderBy"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// <see cref="IOrderBy"/>.</param>
        /// <returns>The "order by" clause.</returns>
        new IOrderBy Add(params object[] values);

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="IOrderBy"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the
        /// <see cref="IOrderBy"/>.</param>
        /// <returns>The "order by" clause.</returns>
        new IOrderBy Add(IEnumerable<object> values);

        /// <summary>
        /// Adds a value to the <see cref="IOrderBy"/>.
        /// </summary>
        /// <param name="value">The value to add to the <see cref="IOrderBy"/>.</param>
        /// <returns>The "order by" clause.</returns>
        new IOrderBy Add(Expression<Func<object>> value);

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="IOrderBy"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// <see cref="IOrderBy"/>.</param>
        /// <returns>The "order by" clause.</returns>
        new IOrderBy Add(params Expression<Func<object>>[] values);

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="IOrderBy"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the
        /// <see cref="IOrderBy"/>.</param>
        /// <returns>The "order by" clause.</returns>
        new IOrderBy Add(IEnumerable<Expression<Func<object>>> values);

        /// <summary>
        /// Sets ascending order for all columns added with the latest <see cref="o:Add"/> method call.
        /// </summary>
        /// <value>The "order by".</value>
        /// <exception cref="InvalidOperationException">The list is empty or there is a select all column.</exception>
        IOrderBy Asc { get; }

        /// <summary>
        /// Sets descending order for all columns added with the latest <see cref="o:Add"/> method call.
        /// </summary>
        /// <value>The "order by".</value>
        /// <exception cref="InvalidOperationException">The list is empty or there is a select all column.</exception>
        IOrderBy Desc { get; }

        /// <summary>
        /// Sets the specified  order for all columns added with the latest <see cref="o:Add"/> method call.
        /// </summary>
        /// <param name="ascending">If the order is ascending.</param>
        /// <returns>The "order by" clause.</returns>
        /// <exception cref="InvalidOperationException">The list is empty or there is a select all column.</exception>
        IOrderBy SetOrder(bool ascending = true);
    }
}