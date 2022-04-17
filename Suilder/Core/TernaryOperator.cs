using Suilder.Builder;
using Suilder.Engines;
using Suilder.Functions;
using Suilder.Operators;

namespace Suilder.Core
{
    /// <summary>
    /// Implementation of <see cref="IOperator"/> for a ternary operator.
    /// </summary>
    public class TernaryOperator : IOperator, ISubFragment
    {
        /// <summary>
        /// The operator.
        /// </summary>
        /// <value>The operator.</value>
        public string Op { get; protected set; }

        /// <summary>
        /// The second operator.
        /// </summary>
        /// <value>The second operator.</value>
        public string Op2 { get; protected set; }

        /// <summary>
        /// The first value.
        /// </summary>
        /// <value>The first value.</value>
        protected object Value1 { get; set; }

        /// <summary>
        /// The second value.
        /// </summary>
        /// <value>The second value.</value>
        protected object Value2 { get; set; }

        /// <summary>
        /// The third value.
        /// </summary>
        /// <value>The third value.</value>
        protected object Value3 { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TernaryOperator"/> class.
        /// </summary>
        /// <param name="op">The operator.</param>
        /// <param name="op2">The second operator.</param>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <param name="value3">The third value.</param>
        public TernaryOperator(string op, string op2, object value1, object value2, object value3)
        {
            Op = op;
            Op2 = op2;
            Value1 = value1;
            Value2 = value2;
            Value3 = value3;
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
                FunctionHelper.TernaryOperator(queryBuilder, engine, opInfo.Op, Value1, Value2, Value3);
            }
            else
            {
                queryBuilder.WriteValue(Value1, Parentheses.SubFragment)
                    .Write(" " + (opInfo?.Op ?? Op) + " ").WriteValue(Value2, Parentheses.SubFragment)
                    .Write(" " + (engine.GetOperator(Op2)?.Op ?? Op2) + " ").WriteValue(Value3, Parentheses.SubFragment);
            }
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return ToStringBuilder.Build(b => b.WriteValue(Value1, Parentheses.SubFragment)
                .Write(" " + Op + " ").WriteValue(Value2, Parentheses.SubFragment)
                .Write(" " + Op2 + " ").WriteValue(Value3, Parentheses.SubFragment));
        }
    }
}