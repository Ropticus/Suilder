using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Suilder.Core
{
    /// <summary>
    /// A <see cref="IQueryFragment"/> that has a list of elements.
    /// </summary>
    /// <typeparam name="TValue">The type of the values.</typeparam>
    public interface IQueryFragmentList<TValue> : IQueryFragment
    {
        /// <summary>
        /// Adds a value to the <see cref="IQueryFragmentList{T}"/>.
        /// </summary>
        /// <param name="value">The value to add to the <see cref="IQueryFragmentList{T}"/>.</param>
        void Add(TValue value);

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="IQueryFragmentList{T}"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// <see cref="IQueryFragmentList{T}"/>.</param>
        void Add(params TValue[] values);

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="IQueryFragmentList{T}"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the
        /// <see cref="IQueryFragmentList{T}"/>.</param>
        void Add(IEnumerable<TValue> values);
    }

    /// <summary>
    /// A <see cref="IQueryFragment"/> that has a list of elements.
    /// </summary>
    /// <typeparam name="TValue">The type of the values.</typeparam>
    /// <typeparam name="TExpression">The type of the expressions.</typeparam>
    public interface IQueryFragmentList<TValue, TExpression> : IQueryFragmentList<TValue>
    {
        /// <summary>
        /// Adds a value to the <see cref="IQueryFragmentList{T, T2}"/>.
        /// </summary>
        /// <param name="value">The value to add to the <see cref="IQueryFragmentList{T, T2}"/>.</param>
        void Add(Expression<Func<TExpression>> value);

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="IQueryFragmentList{T, T2}"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// <see cref="IQueryFragmentList{T, T2}"/>.</param>
        void Add(params Expression<Func<TExpression>>[] values);

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="IQueryFragmentList{T, T2}"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the
        /// <see cref="IQueryFragmentList{T, T2}"/>.</param>
        void Add(IEnumerable<Expression<Func<TExpression>>> values);
    }
}