using Suilder.Builder;
using Suilder.Engines;

namespace Suilder.Core
{
    /// <summary>
    /// Implementation of <see cref="IOperator"/> for a right operator.
    /// </summary>
    public class RightOperator : IOperator
    {
        /// <summary>
        /// The operator.
        /// </summary>
        /// <value>The operator.</value>
        public string Op { get; protected set; }

        /// <summary>
        /// The value.
        /// </summary>
        /// <value>The value.</value>
        protected object Value { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RightOperator"/> class.
        /// </summary>
        /// <param name="op">The operator.</param>
        /// <param name="value">The value.</param>
        public RightOperator(string op, object value)
        {
            Op = op;
            Value = value;
        }

        /// <summary>
        /// Compiles the fragment.
        /// </summary>
        /// <param name="queryBuilder">The query builder.</param>
        /// <param name="engine">The engine.</param>
        public virtual void Compile(QueryBuilder queryBuilder, IEngine engine)
        {
            queryBuilder.WriteValue(Value).Write(" ").Write(Op);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return ToStringBuilder.Build(b => b.WriteValue(Value).Write(" " + Op));
        }
    }
}