using Suilder.Builder;
using Suilder.Engines;
using Suilder.Functions;
using Suilder.Operators;

namespace Suilder.Core
{
    /// <summary>
    /// Implementation of <see cref="IOperator"/> for a left query operator.
    /// </summary>
    public class LeftQueryOperator : IOperator
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
        protected IQueryFragment Value { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LeftQueryOperator"/> class.
        /// </summary>
        /// <param name="op">The operator.</param>
        /// <param name="value">The value.</param>
        public LeftQueryOperator(string op, IQueryFragment value)
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
            IOperatorInfo opInfo = engine.GetOperator(Op);

            if (opInfo?.Function == true)
            {
                FunctionHelper.UnaryOperator(queryBuilder, engine, opInfo.Op, Value);
            }
            else
            {
                queryBuilder.Write(opInfo?.Op ?? Op).Write(" ").WriteFragment(Value, true);
            }
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return ToStringBuilder.Build(b => b.Write(Op + " ").WriteFragment(Value, true));
        }
    }
}