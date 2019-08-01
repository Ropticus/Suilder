using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Engines;

namespace Suilder.Core
{
    /// <summary>
    /// Implementation of <see cref="IWith"/>.
    /// </summary>
    public class With : QueryFragmentList<IQueryFragment>, IWith
    {
        /// <summary>
        /// Adds a value to the <see cref="IWith"/>.
        /// </summary>
        /// <param name="value">The value to add to the <see cref="IWith"/>.</param>
        /// <returns>The "with" clause.</returns>
        IWith IWith.Add(IQueryFragment value)
        {
            Add(value);
            return this;
        }

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="IWith"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// <see cref="IWith"/>.</param>
        /// <returns>The "with" clause.</returns>
        IWith IWith.Add(params IQueryFragment[] values)
        {
            Add(values);
            return this;
        }

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="IWith"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the
        /// <see cref="IWith"/>.</param>
        /// <returns>The "with" clause.</returns>
        IWith IWith.Add(IEnumerable<IQueryFragment> values)
        {
            Add(values);
            return this;
        }

        /// <summary>
        /// Compiles the fragment.
        /// </summary>
        /// <param name="queryBuilder">The query builder.</param>
        /// <param name="engine">The engine.</param>
        public override void Compile(QueryBuilder queryBuilder, IEngine engine)
        {
            queryBuilder.Write("WITH ");

            if (engine.Options.WithRecursive)
                queryBuilder.Write("RECURSIVE ");

            string separator = ", ";
            foreach (IQueryFragment value in Values)
            {
                queryBuilder.WriteFragment(value).Write(separator);
            }
            queryBuilder.RemoveLast(separator.Length);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return ToStringBuilder.Build(b => b.Write("WITH ").Join(", ", Values, (x) => b.WriteValue(x)));
        }
    }
}