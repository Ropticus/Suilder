using System;
using Suilder.Builder;
using Suilder.Engines;

namespace Suilder.Core
{
    /// <summary>
    /// Implementation of <see cref="IOver"/>.
    /// </summary>
    public class Over : IOver
    {
        /// <summary>
        /// The "partition by" value.
        /// </summary>
        /// <value>The "partition by" value.</value>
        protected IQueryFragment PartitionByValue { get; set; }

        /// <summary>
        /// The "order by" value.
        /// </summary>
        /// <value>The "order by" value.</value>
        protected IQueryFragment OrderByValue { get; set; }

        /// <summary>
        /// The range value.
        /// </summary>
        /// <value>The range value.</value>
        protected IQueryFragment RangeValue { get; set; }

        /// <summary>
        /// Adds a "partition by" clause.
        /// </summary>
        /// <param name="partitionBy">The list of columns.</param>
        /// <returns>The "over" clause.</returns>
        public virtual IOver PartitionBy(IValList partitionBy)
        {
            PartitionByValue = partitionBy;
            return this;
        }

        /// <summary>
        /// Adds a "partition by" clause.
        /// </summary>
        /// <param name="partitionBy">The "partition by" value.</param>
        /// <returns>The "over" clause.</returns>
        public virtual IOver PartitionBy(IRawSql partitionBy)
        {
            PartitionByValue = partitionBy;
            return this;
        }

        /// <summary>
        /// Adds a "partition by" clause.
        /// </summary>
        /// <param name="func">Function that returns the list of columns.</param>
        /// <returns>The "over" clause.</returns>
        public virtual IOver PartitionBy(Func<IValList, IValList> func)
        {
            return PartitionBy(func(SqlBuilder.Instance.ValList));
        }

        /// <summary>
        /// Adds a "order by" clause.
        /// </summary>
        /// <param name="orderBy">The "order by" value.</param>
        /// <returns>The "over" clause.</returns>
        public virtual IOver OrderBy(IOrderBy orderBy)
        {
            OrderByValue = orderBy;
            return this;
        }

        /// <summary>
        /// Adds a "order by" clause.
        /// </summary>
        /// <param name="orderBy">The "order by" value.</param>
        /// <returns>The "over" clause.</returns>
        public virtual IOver OrderBy(IRawSql orderBy)
        {
            OrderByValue = orderBy;
            return this;
        }

        /// <summary>
        /// Adds a "order by" clause.
        /// </summary>
        /// <param name="func">Function that returns the "order by" value.</param>
        /// <returns>The "over" clause.</returns>
        public virtual IOver OrderBy(Func<IOrderBy, IOrderBy> func)
        {
            return OrderBy(func(SqlBuilder.Instance.OrderBy()));
        }

        /// <summary>
        /// Adds a "range" clause.
        /// <para>Use <see cref="IRawSql"/> to add the value.</para>
        /// </summary>
        /// <param name="range">The range value.</param>
        /// <returns>The "over" clause.</returns>

        public virtual IOver Range(IQueryFragment range)
        {
            RangeValue = range;
            return this;
        }

        /// <summary>
        /// Compiles the fragment.
        /// </summary>
        /// <param name="queryBuilder">The query builder.</param>
        /// <param name="engine">The engine.</param>
        public virtual void Compile(QueryBuilder queryBuilder, IEngine engine)
        {
            queryBuilder.Write("OVER(");
            if (PartitionByValue != null)
            {
                queryBuilder.Write("PARTITION BY ");
                queryBuilder.WriteFragment(PartitionByValue);
            }
            if (OrderByValue != null)
            {
                queryBuilder.Write(" ").WriteFragment(OrderByValue);
            }
            if (RangeValue != null)
            {
                queryBuilder.Write(" ").WriteFragment(RangeValue);
            }
            queryBuilder.Write(")");
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return ToStringBuilder.Build(b => b.Write("OVER(")
                .IfNotNull(PartitionByValue, x => b.Write("PARTITION BY ").WriteFragment(x))
                .IfNotNull(OrderByValue, x => b.Write(" ").WriteFragment(x))
                .IfNotNull(RangeValue, x => b.Write(" ").WriteFragment(x)).Write(")"));
        }
    }
}