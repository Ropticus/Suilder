using Suilder.Builder;
using Suilder.Engines;
using Suilder.Exceptions;

namespace Suilder.Core
{
    /// <summary>
    /// Implementation of <see cref="ITop"/>.
    /// </summary>
    public class Top : ITop
    {
        /// <summary>
        /// The fetch value.
        /// </summary>
        /// <value>The fetch value.</value>
        protected object FetchValue { get; set; }

        /// <summary>
        /// If is percent.
        /// </summary>
        /// <value>If is percent.</value>
        protected bool PercentValue { get; set; }

        /// <summary>
        /// If is with ties.
        /// </summary>
        /// <value>If is with ties.</value>
        protected bool WithTiesValue { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Top"/> class.
        /// </summary>
        /// <param name="fetch">The number of rows to return.</param>
        public Top(object fetch)
        {
            FetchValue = fetch;
        }

        /// <summary>
        /// Return a percent of rows.
        /// </summary>
        /// <param name="percent">If return a percent of rows.</param>
        /// <returns>The "top" clause.</returns>
        public virtual ITop Percent(bool percent = true)
        {
            PercentValue = percent;
            return this;
        }

        /// <summary>
        /// Return two or more rows that tie for last place.
        /// </summary>
        /// <param name="withTies">If return two or more rows that tie for last place.</param>
        /// <returns>The "top" clause.</returns>
        public virtual ITop WithTies(bool withTies = true)
        {
            WithTiesValue = withTies;
            return this;
        }

        /// <summary>
        /// Compiles the fragment.
        /// </summary>
        /// <param name="queryBuilder">The query builder.</param>
        /// <param name="engine">The engine.</param>
        public virtual void Compile(QueryBuilder queryBuilder, IEngine engine)
        {
            if (!engine.Options.TopSupported)
                throw new ClauseNotSupportedException("Top clause is not supported in this engine.");

            queryBuilder.Write("TOP(");

            if (engine.Options.TopAsParameters || FetchValue is IQueryFragment)
                queryBuilder.WriteValue(FetchValue);
            else
                queryBuilder.Write(FetchValue.ToString());

            queryBuilder.Write(")");

            if (PercentValue)
                queryBuilder.Write(" PERCENT");

            if (WithTiesValue)
                queryBuilder.Write(" WITH TIES");
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return ToStringBuilder.Build(b => b.Write("TOP(").WriteValue(FetchValue).Write(")")
                .If(PercentValue, () => b.Write(" PERCENT"))
                .If(WithTiesValue, () => b.Write(" WITH TIES")));
        }
    }
}