using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Suilder.Core
{
    /// <summary>
    /// A sublist of values.
    /// </summary>
    public interface ISubList : IValList, ISubQuery
    {
        /// <summary>
        /// Adds a value to the end of the <see cref="ISubList"/>.
        /// </summary>
        /// <param name="value">The value to add to the end of the <see cref="ISubList"/>.</param>
        /// <returns>The sublist of values.</returns>
        new ISubList Add(object value);

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="ISubList"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// <see cref="ISubList"/>.</param>
        /// <returns>The sublist of values.</returns>
        new ISubList Add(params object[] values);

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="ISubList"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the
        /// <see cref="ISubList"/>.</param>
        /// <returns>The sublist of values.</returns>
        new ISubList Add(IEnumerable<object> values);

        /// <summary>
        /// Adds a value to the end of the <see cref="ISubList"/>.
        /// </summary>
        /// <param name="value">The value to add to the end of the <see cref="ISubList"/>.</param>
        /// <returns>The sublist of values.</returns>
        new ISubList Add(Expression<Func<object>> value);

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="ISubList"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// <see cref="ISubList"/>.</param>
        /// <returns>The sublist of values.</returns>
        new ISubList Add(params Expression<Func<object>>[] values);

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="ISubList"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the
        /// <see cref="ISubList"/>.</param>
        /// <returns>The sublist of values.</returns>
        new ISubList Add(IEnumerable<Expression<Func<object>>> values);
    }
}