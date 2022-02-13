using Suilder.Builder;
using Suilder.Engines;

namespace Suilder.Core
{
    /// <summary>
    /// Implementation of <see cref="IOperator"/> for a range operator.
    /// </summary>
    public class RangeOperator : IOperator, ISubFragment
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
        protected object Left { get; set; }

        /// <summary>
        /// The min value.
        /// </summary>
        /// <value>The min value.</value>
        protected object Min { get; set; }

        /// <summary>
        /// The max value.
        /// </summary>
        /// <value>The max value.</value>
        protected object Max { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeOperator"/> class.
        /// </summary>
        /// <param name="op">The operator.</param>
        /// <param name="left">The left value.</param>
        /// <param name="min">The min value.</param>
        /// <param name="max">The max value.</param>
        public RangeOperator(string op, object left, object min, object max)
        {
            Op = op;
            Left = left;
            Min = min;
            Max = max;
        }

        /// <summary>
        /// Compiles the fragment.
        /// </summary>
        /// <param name="queryBuilder">The query builder.</param>
        /// <param name="engine">The engine.</param>
        public virtual void Compile(QueryBuilder queryBuilder, IEngine engine)
        {
            queryBuilder.WriteValue(Left, Parentheses.SubFragment)
                .Write(" " + Op + " ").WriteValue(Min, Parentheses.SubFragment)
                .Write(" AND ").WriteValue(Max, Parentheses.SubFragment);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return ToStringBuilder.Build(b => b.WriteValue(Left, Parentheses.SubFragment)
                .Write(" " + Op + " ").WriteValue(Min, Parentheses.SubFragment)
                .Write(" AND ").WriteValue(Max, Parentheses.SubFragment));
        }
    }
}