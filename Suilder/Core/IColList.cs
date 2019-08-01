using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Suilder.Builder;
using Suilder.Engines;

namespace Suilder.Core
{
    /// <summary>
    /// A list of columns.
    /// </summary>
    public interface IColList : IQueryFragmentList<IColumn, object>
    {
        /// <summary>
        /// Adds a value to the <see cref="IColList"/>.
        /// </summary>
        /// <param name="value">The value to add to the <see cref="IColList"/>.</param>
        /// <returns>The list of columns.</returns>
        new IColList Add(IColumn value);

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="IColList"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// <see cref="IColList"/>.</param>
        /// <returns>The list of columns.</returns>
        new IColList Add(params IColumn[] values);

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="IColList"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the
        /// <see cref="IColList"/>.</param>
        /// <returns>The list of columns.</returns>
        new IColList Add(IEnumerable<IColumn> values);

        /// <summary>
        /// Adds a value to the <see cref="IColList"/>.
        /// </summary>
        /// <param name="value">The value to add to the <see cref="IColList"/>.</param>
        /// <returns>The list of columns.</returns>
        new IColList Add(Expression<Func<object>> value);

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="IColList"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// <see cref="IColList"/>.</param>
        /// <returns>The list of columns.</returns>
        new IColList Add(params Expression<Func<object>>[] values);

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="IColList"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the
        /// <see cref="IColList"/>.</param>
        /// <returns>The list of columns.</returns>
        new IColList Add(IEnumerable<Expression<Func<object>>> values);

        /// <summary>
        /// Compiles the fragment.
        /// </summary>
        /// <param name="queryBuilder">The query builder.</param>
        /// <param name="engine">The engine.</param>
        /// <param name="withTableName">If compile with the table name.</param>
        void Compile(QueryBuilder queryBuilder, IEngine engine, bool withTableName);
    }
}