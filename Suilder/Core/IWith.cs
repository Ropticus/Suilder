using System.Collections.Generic;

namespace Suilder.Core
{
    /// <summary>
    /// A "with" clause.
    /// </summary>
    public interface IWith : IQueryFragmentList<IQueryFragment>
    {
        /// <summary>
        /// Adds a value to the <see cref="IWith"/>.
        /// </summary>
        /// <param name="value">The value to add to the <see cref="IWith"/>.</param>
        /// <returns>The "with" clause.</returns>
        new IWith Add(IQueryFragment value);

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="IWith"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// <see cref="IWith"/>.</param>
        /// <returns>The "with" clause.</returns>
        new IWith Add(params IQueryFragment[] values);

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="IWith"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the
        /// <see cref="IWith"/>.</param>
        /// <returns>The "with" clause.</returns>
        new IWith Add(IEnumerable<IQueryFragment> values);
    }
}