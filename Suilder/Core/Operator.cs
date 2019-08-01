using Suilder.Builder;
using Suilder.Engines;

namespace Suilder.Core
{
    /// <summary>
    /// Implementation of <see cref="IOperator"/>.
    /// </summary>
    public class Operator : IOperator
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
        /// The right value.
        /// </summary>
        /// <value>The right value.</value>
        protected object Right { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Operator"/> class.
        /// </summary>
        /// <param name="op">The operator.</param>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        public Operator(string op, object left, object right)
        {
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
            queryBuilder.WriteValue(Left).Write(" ").Write(Op).Write(" ").WriteValue(Right);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return ToStringBuilder.Build(b => b.WriteValue(Left).Write(" " + Op + " ").WriteValue(Right));
        }
    }
}