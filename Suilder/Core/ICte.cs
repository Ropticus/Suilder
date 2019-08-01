using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Suilder.Core
{
    /// <summary>
    /// A common table expression (CTE).
    /// </summary>
    public interface ICte : IColList
    {
        /// <summary>
        /// The name of the CTE.
        /// </summary>
        /// <value>The name of the CTE.</value>
        string Name { get; }

        /// <summary>
        /// An alias with the name of the CTE.
        /// </summary>
        /// <value>An alias with the name of the CTE.</value>
        IAlias Alias { get; }

        /// <summary>
        /// Adds a value to the <see cref="ICte"/>.
        /// </summary>
        /// <param name="value">The value to add to the <see cref="ICte"/>.</param>
        /// <returns>The CTE.</returns>
        new ICte Add(IColumn value);

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="ICte"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// <see cref="ICte"/>.</param>
        /// <returns>The CTE.</returns>
        new ICte Add(params IColumn[] values);

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="ICte"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the
        /// <see cref="ICte"/>.</param>
        /// <returns>The CTE.</returns>
        new ICte Add(IEnumerable<IColumn> values);

        /// <summary>
        /// Adds a value to the <see cref="ICte"/>.
        /// </summary>
        /// <param name="value">The value to add to the <see cref="ICte"/>.</param>
        /// <returns>The CTE.</returns>
        new ICte Add(Expression<Func<object>> value);

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="ICte"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// <see cref="ICte"/>.</param>
        /// <returns>The CTE.</returns>
        new ICte Add(params Expression<Func<object>>[] values);

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="ICte"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the
        /// <see cref="ICte"/>.</param>
        /// <returns>The CTE.</returns>
        new ICte Add(IEnumerable<Expression<Func<object>>> values);

        /// <summary>
        /// Sets the query of the CTE.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>The CTE.</returns>
        ICte As(IQuery query);

        /// <summary>
        /// Sets the query of the CTE.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>The CTE.</returns>
        ICte As(IRawQuery query);
    }
}