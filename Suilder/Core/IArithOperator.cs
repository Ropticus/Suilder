using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Suilder.Core
{
    /// <summary>
    /// An arithmetic operator.
    /// </summary>
    public interface IArithOperator : IOperator, ISubQuery, IQueryFragmentList<object, object>
    {
        /// <summary>
        /// Adds a value to the <see cref="IArithOperator"/>.
        /// </summary>
        /// <param name="value">The value to add to the <see cref="IArithOperator"/>.</param>
        /// <returns>The arithmetic operator.</returns>
        new IArithOperator Add(object value);

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="IArithOperator"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the 
        /// <see cref="IArithOperator"/>.</param>
        /// <returns>The arithmetic operator.</returns>
        new IArithOperator Add(params object[] values);

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="IArithOperator"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the 
        /// <see cref="IArithOperator"/>.</param>
        /// <returns>The arithmetic operator.</returns>
        new IArithOperator Add(IEnumerable<object> values);

        /// <summary>
        /// Adds a value to the <see cref="IArithOperator"/>.
        /// </summary>
        /// <param name="value">The value to add to the <see cref="IArithOperator"/>.</param>
        /// <returns>The arithmetic operator.</returns>
        new IArithOperator Add(Expression<Func<object>> value);

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="IArithOperator"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the 
        /// <see cref="IArithOperator"/>.</param>
        /// <returns>The arithmetic operator.</returns>
        new IArithOperator Add(params Expression<Func<object>>[] values);

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="IArithOperator"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the 
        /// <see cref="IArithOperator"/>.</param>
        /// <returns>The arithmetic operator.</returns>
        new IArithOperator Add(IEnumerable<Expression<Func<object>>> values);
    }
}