using System;
using Suilder.Builder;
using Suilder.Engines;

namespace Suilder.Core
{
    /// <summary>
    /// Implementation of <see cref="IUpdate"/>.
    /// </summary>
    public class Update : IUpdate, IUpdateTop
    {
        /// <summary>
        /// The "top" value.
        /// </summary>
        /// <value>The "top" value.</value>
        protected IQueryFragment TopValue { get; set; }

        /// <summary>
        /// Adds a "top" clause.
        /// </summary>
        /// <param name="top">The "top" clause.</param>
        /// <returns>The "update" statement.</returns>
        public virtual IUpdate Top(ITop top)
        {
            TopValue = top;
            return this;
        }

        /// <summary>
        /// Adds a raw "top" clause.
        /// <para>You must write the entire clause.</para>
        /// </summary>
        /// <param name="top">The "top" clause.</param>
        /// <returns>The "update" statement.</returns>
        public virtual IUpdate Top(IRawSql top)
        {
            TopValue = top;
            return this;
        }

        /// <summary>
        /// Adds a "top" clause.
        /// </summary>
        /// <param name="fetch">The number of rows to return.</param>
        /// <returns>The "update" statement.</returns>
        public virtual IUpdateTop Top(object fetch)
        {
            Top(SqlBuilder.Instance.Top(fetch));
            return this;
        }

        /// <summary>
        /// Return a percent of rows.
        /// </summary>
        /// <param name="percent">If return a percent of rows.</param>
        /// <returns>The "update" statement.</returns>
        public virtual IUpdateTop Percent(bool percent = true)
        {
            if (TopValue is ITop top)
            {
                top.Percent(percent);
                return this;
            }

            throw new InvalidOperationException($"Top value must be a \"{typeof(ITop)}\" instance.");
        }

        /// <summary>
        /// Return two or more rows that tie for last place.
        /// </summary>
        /// <param name="withTies">If return two or more rows that tie for last place.</param>
        /// <returns>The "update" statement.</returns>
        public virtual IUpdateTop WithTies(bool withTies = true)
        {
            if (TopValue is ITop top)
            {
                top.WithTies(withTies);
                return this;
            }

            throw new InvalidOperationException($"Top value must be a \"{typeof(ITop)}\" instance.");
        }

        /// <summary>
        /// Compiles the fragment.
        /// </summary>
        /// <param name="queryBuilder">The query builder.</param>
        /// <param name="engine">The engine.</param>
        public virtual void Compile(QueryBuilder queryBuilder, IEngine engine)
        {
            queryBuilder.Write("UPDATE");

            if (TopValue != null)
                queryBuilder.Write(" ").WriteFragment(TopValue);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return ToStringBuilder.Build(b => b.Write("UPDATE").IfNotNull(TopValue, x => b.Write(" ").WriteFragment(x)));
        }
    }
}