using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Suilder.Core
{
    /// <summary>
    /// A bitwise operator.
    /// </summary>
    public interface IBitOperator : IOperator, ISubFragment, IQueryFragmentList<object, object>
    {
        /// <summary>
        /// Adds a value to the end of the <see cref="IBitOperator"/>.
        /// </summary>
        /// <param name="value">The value to add to the end of the <see cref="IBitOperator"/>.</param>
        /// <returns>The bitwise operator.</returns>
        new IBitOperator Add(object value);

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="IBitOperator"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// <see cref="IBitOperator"/>.</param>
        /// <returns>The bitwise operator.</returns>
        new IBitOperator Add(params object[] values);

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="IBitOperator"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the
        /// <see cref="IBitOperator"/>.</param>
        /// <returns>The bitwise operator.</returns>
        new IBitOperator Add(IEnumerable<object> values);

        /// <summary>
        /// Adds a value to the end of the <see cref="IBitOperator"/>.
        /// </summary>
        /// <param name="value">The value to add to the end of the <see cref="IBitOperator"/>.</param>
        /// <returns>The bitwise operator.</returns>
        new IBitOperator Add(Expression<Func<object>> value);

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="IBitOperator"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// <see cref="IBitOperator"/>.</param>
        /// <returns>The bitwise operator.</returns>
        new IBitOperator Add(params Expression<Func<object>>[] values);

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="IBitOperator"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the
        /// <see cref="IBitOperator"/>.</param>
        /// <returns>The bitwise operator.</returns>
        new IBitOperator Add(IEnumerable<Expression<Func<object>>> values);
    }
}