using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Suilder.Core
{
    /// <summary>
    /// A logical operator.
    /// </summary>
    public interface ILogicalOperator : IOperator, ISubQuery, IQueryFragmentList<IQueryFragment, bool>
    {
        /// <summary>
        /// Adds a value to the <see cref="ILogicalOperator"/>.
        /// </summary>
        /// <param name="value">The value to add to the <see cref="ILogicalOperator"/>.</param>
        /// <returns>The logical operator.</returns>
        new ILogicalOperator Add(IQueryFragment value);

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="ILogicalOperator"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the 
        /// <see cref="ILogicalOperator"/>.</param>
        /// <returns>The logical operator.</returns>
        new ILogicalOperator Add(params IQueryFragment[] values);

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="ILogicalOperator"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the 
        /// <see cref="ILogicalOperator"/>.</param>
        /// <returns>The logical operator.</returns>
        new ILogicalOperator Add(IEnumerable<IQueryFragment> values);

        /// <summary>
        /// Adds a value to the <see cref="ILogicalOperator"/>.
        /// </summary>
        /// <param name="value">The value to add to the <see cref="ILogicalOperator"/>.</param>
        /// <returns>The logical operator.</returns>
        new ILogicalOperator Add(Expression<Func<bool>> value);

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="ILogicalOperator"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the 
        /// <see cref="ILogicalOperator"/>.</param>
        /// <returns>The logical operator.</returns>
        new ILogicalOperator Add(params Expression<Func<bool>>[] values);

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="ILogicalOperator"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the 
        /// <see cref="ILogicalOperator"/>.</param>
        /// <returns>The logical operator.</returns>
        new ILogicalOperator Add(IEnumerable<Expression<Func<bool>>> values);
    }
}