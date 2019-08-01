using System;

namespace Suilder.Core
{
    /// <summary>
    /// An "over" clause.
    /// </summary>
    public interface IOver : IQueryFragment
    {
        /// <summary>
        /// Adds a "partition by" clause.
        /// </summary>
        /// <param name="partitionBy">The list of columns.</param>
        /// <returns>The "over" clause.</returns>
        IOver PartitionBy(IValList partitionBy);

        /// <summary>
        /// Adds a "partition by" clause.
        /// </summary>
        /// <param name="partitionBy">The "partition by" value.</param>
        /// <returns>The "over" clause.</returns>
        IOver PartitionBy(IRawSql partitionBy);

        /// <summary>
        /// Adds a "partition by" clause.
        /// </summary>
        /// <param name="func">Function that returns the list of columns.</param>
        /// <returns>The "over" clause.</returns>
        IOver PartitionBy(Func<IValList, IValList> func);

        /// <summary>
        /// Adds a "order by" clause.
        /// </summary>
        /// <param name="orderBy">The "order by" value.</param>
        /// <returns>The "over" clause.</returns>
        IOver OrderBy(IOrderBy orderBy);

        /// <summary>
        /// Adds a "order by" clause.
        /// </summary>
        /// <param name="orderBy">The "order by" value.</param>
        /// <returns>The "over" clause.</returns>
        IOver OrderBy(IRawSql orderBy);

        /// <summary>
        /// Adds a "order by" clause.
        /// </summary>
        /// <param name="func">Function that returns the "order by" value.</param>
        /// <returns>The "over" clause.</returns>
        IOver OrderBy(Func<IOrderBy, IOrderBy> func);

        /// <summary>
        /// Adds a "range" clause.
        /// <para>Use <see cref="IRawSql"/> to add the value.</para>
        /// </summary>
        /// <param name="range">The range value.</param>
        /// <returns>The "over" clause.</returns>
        IOver Range(IQueryFragment range);
    }
}