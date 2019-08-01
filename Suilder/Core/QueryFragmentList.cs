using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Suilder.Builder;
using Suilder.Engines;

namespace Suilder.Core
{
    /// <summary>
    /// Abstract implementation of <see cref="IQueryFragmentList{T}"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the values.</typeparam>
    public abstract class QueryFragmentList<TValue> : IQueryFragmentList<TValue>
    {
        /// <summary>
        /// The list with the values.
        /// </summary>
        /// <value>The list with the values.</value>
        protected List<TValue> Values { get; set; } = new List<TValue>();

        /// <summary>
        /// Adds a value to the <see cref="IQueryFragmentList{T}"/>.
        /// </summary>
        /// <param name="value">The value to add to the <see cref="IQueryFragmentList{T}"/>.</param>
        public virtual void Add(TValue value)
        {
            Values.Add(value);
        }

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="IQueryFragmentList{T}"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// <see cref="IQueryFragmentList{T}"/>.</param>
        public virtual void Add(params TValue[] values)
        {
            Values.AddRange(values);
        }

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="IQueryFragmentList{T}"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the
        /// <see cref="IQueryFragmentList{T}"/>.</param>
        public virtual void Add(IEnumerable<TValue> values)
        {
            Values.AddRange(values);
        }

        /// <summary>
        /// Compiles the fragment.
        /// </summary>
        /// <param name="queryBuilder">The query builder.</param>
        /// <param name="engine">The engine.</param>
        public abstract void Compile(QueryBuilder queryBuilder, IEngine engine);
    }

    /// <summary>
    /// Abstract implementation of <see cref="IQueryFragmentList{T, T2}"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the values.</typeparam>
    /// <typeparam name="TExpression">The type of the expressions.</typeparam>
    public abstract class QueryFragmentList<TValue, TExpression> : QueryFragmentList<TValue>,
        IQueryFragmentList<TValue, TExpression>
    {
        /// <summary>
        /// Adds a value to the <see cref="IQueryFragmentList{T, T2}"/>.
        /// </summary>
        /// <param name="value">The value to add to the <see cref="IQueryFragmentList{T, T2}"/>.</param>
        public abstract void Add(Expression<Func<TExpression>> value);

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="IQueryFragmentList{T, T2}"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// <see cref="IQueryFragmentList{T, T2}"/>.</param>
        public abstract void Add(params Expression<Func<TExpression>>[] values);

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="IQueryFragmentList{T, T2}"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the
        /// <see cref="IQueryFragmentList{T, T2}"/>.</param>
        public abstract void Add(IEnumerable<Expression<Func<TExpression>>> values);
    }
}