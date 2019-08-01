using System;
using Suilder.Builder;
using Suilder.Engines;

namespace Suilder.Core
{
    /// <summary>
    /// Implementation of <see cref="IOperator"/> for a query operator.
    /// </summary>
    public class QueryOperator : IOperator
    {
        /// <summary>
        /// The operator.
        /// </summary>
        /// <value>The operator.</value>
        public string Op { get; protected set; }

        /// <summary>
        /// The left value.
        /// </summary>
        /// <value>The left value.</value>
        protected IQueryFragment Left { get; set; }

        /// <summary>
        /// The right value.
        /// </summary>
        /// <value>The right value.</value>
        protected IQueryFragment Right { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryOperator"/> class.
        /// </summary>
        /// <param name="op">The operator.</param>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        public QueryOperator(string op, IQueryFragment left, IQueryFragment right)
        {
            if (left == null)
                throw new ArgumentNullException(nameof(left), "Left value is null.");
            if (right == null)
                throw new ArgumentNullException(nameof(right), "Right value is null.");

            Op = op;
            Left = left;
            Right = right;
        }

        /// <summary>
        /// Compiles the fragment.
        /// </summary>
        /// <param name="queryBuilder">The query builder.</param>
        /// <param name="engine">The engine.</param>
        public virtual void Compile(QueryBuilder queryBuilder, IEngine engine)
        {
            queryBuilder.WriteFragment(Left, false).Write(" " + Op + " ").WriteFragment(Right, false);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return ToStringBuilder.Build(b => b.WriteFragment(Left, false).Write(" " + Op + " ")
                .WriteFragment(Right, false));
        }
    }
}