using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Suilder.Core
{
    /// <summary>
    /// A list of values.
    /// </summary>
    public interface IValList : IQueryFragmentList<object, object>
    {
        /// <summary>
        /// Adds a value to the <see cref="IValList"/>.
        /// </summary>
        /// <param name="value">The value to add to the <see cref="IValList"/>.</param>
        /// <returns>The list of values.</returns>
        new IValList Add(object value);

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="IValList"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// <see cref="IValList"/>.</param>
        /// <returns>The list of values.</returns>
        new IValList Add(params object[] values);

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="IValList"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the
        /// <see cref="IValList"/>.</param>
        /// <returns>The list of values.</returns>
        new IValList Add(IEnumerable<object> values);

        /// <summary>
        /// Adds a value to the <see cref="IValList"/>.
        /// </summary>
        /// <param name="value">The value to add to the <see cref="IValList"/>.</param>
        /// <returns>The list of values.</returns>
        new IValList Add(Expression<Func<object>> value);

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="IValList"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// <see cref="IValList"/>.</param>
        /// <returns>The list of values.</returns>
        new IValList Add(params Expression<Func<object>>[] values);

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="IValList"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the
        /// <see cref="IValList"/>.</param>
        /// <returns>The list of values.</returns>
        new IValList Add(IEnumerable<Expression<Func<object>>> values);
    }
}