using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Suilder.Core
{
    /// <summary>
    /// Implementation of <see cref="ISubList"/>.
    /// </summary>
    public class SubList : ValList, ISubList
    {
        /// <summary>
        /// Adds a value to the <see cref="ISubList"/>.
        /// </summary>
        /// <param name="value">The value to add to the <see cref="ISubList"/>.</param>
        /// <returns>The sublist of values.</returns>
        ISubList ISubList.Add(object value)
        {
            Add(value);
            return this;
        }

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="ISubList"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// <see cref="ISubList"/>.</param>
        /// <returns>The sublist of values.</returns>
        ISubList ISubList.Add(params object[] values)
        {
            Add(values);
            return this;
        }

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="ISubList"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the
        /// <see cref="ISubList"/>.</param>
        /// <returns>The sublist of values.</returns>
        ISubList ISubList.Add(IEnumerable<object> values)
        {
            Add(values);
            return this;
        }

        /// <summary>
        /// Adds a value to the <see cref="ISubList"/>.
        /// </summary>
        /// <param name="value">The value to add to the <see cref="ISubList"/>.</param>
        /// <returns>The sublist of values.</returns>
        ISubList ISubList.Add(Expression<Func<object>> value)
        {
            Add(value);
            return this;
        }

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="ISubList"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// <see cref="ISubList"/>.</param>
        /// <returns>The sublist of values.</returns>
        ISubList ISubList.Add(params Expression<Func<object>>[] values)
        {
            Add(values);
            return this;
        }

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="ISubList"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the
        /// <see cref="ISubList"/>.</param>
        /// <returns>The sublist of values.</returns>
        ISubList ISubList.Add(IEnumerable<Expression<Func<object>>> values)
        {
            Add(values);
            return this;
        }
    }
}