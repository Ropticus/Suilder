using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Suilder.Core
{
    /// <summary>
    /// A function.
    /// </summary>
    public interface IFunction : IValList
    {
        /// <summary>
        /// The name of the function.
        /// </summary>
        /// <value>The name of the function.</value>
        string Name { get; }

        /// <summary>
        /// The value to write before the arguments of the function.
        /// </summary>
        /// <value>The value to write before the arguments of the function.</value>
        IQueryFragment BeforeArgs { get; }

        /// <summary>
        /// The arguments of the function.
        /// </summary>
        /// <value>The arguments of the function.</value>
        IReadOnlyList<object> Args { get; }

        /// <summary>
        /// Sets a value to write before the arguments of the function.
        /// </summary>
        /// <param name="value">The value to write before the arguments of the function.</param>
        /// <returns>The function.</returns>
        IFunction Before(IQueryFragment value);

        /// <summary>
        /// Adds a value to the end of the <see cref="IFunction"/>.
        /// </summary>
        /// <param name="value">The value to add to the end of the <see cref="IFunction"/>.</param>
        /// <returns>The function.</returns>
        new IFunction Add(object value);

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="IFunction"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// <see cref="IFunction"/>.</param>
        /// <returns>The function.</returns>
        new IFunction Add(params object[] values);

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="IFunction"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the
        /// <see cref="IFunction"/>.</param>
        /// <returns>The function.</returns>
        new IFunction Add(IEnumerable<object> values);

        /// <summary>
        /// Adds a value to the end of the <see cref="IFunction"/>.
        /// </summary>
        /// <param name="value">The value to add to the end of the <see cref="IFunction"/>.</param>
        /// <returns>The function.</returns>
        new IFunction Add(Expression<Func<object>> value);

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="IFunction"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// <see cref="IFunction"/>.</param>
        /// <returns>The function.</returns>
        new IFunction Add(params Expression<Func<object>>[] values);

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="IFunction"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the
        /// <see cref="IFunction"/>.</param>
        /// <returns>The function.</returns>
        new IFunction Add(IEnumerable<Expression<Func<object>>> values);
    }
}