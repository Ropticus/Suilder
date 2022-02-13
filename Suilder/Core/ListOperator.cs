using System.Collections;
using Suilder.Builder;
using Suilder.Engines;

namespace Suilder.Core
{
    /// <summary>
    /// Implementation of <see cref="IOperator"/> with a list of values.
    /// </summary>
    public class ListOperator : Operator, IOperator, ISubFragment
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListOperator"/> class.
        /// </summary>
        /// <param name="op">The operator.</param>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value. An <see cref="IEnumerable"/> is divided into multiple values.</param>
        public ListOperator(string op, object left, object right) : base(op, left, right)
        {
            if (right is IEnumerable list && !(right is string) && !(right is IQueryFragment))
            {
                IValList valList = SqlBuilder.Instance.ValList;
                foreach (object value in list)
                {
                    valList.Add(value);
                }

                Right = valList;
            }
        }

        /// <summary>
        /// Compiles the fragment.
        /// </summary>
        /// <param name="queryBuilder">The query builder.</param>
        /// <param name="engine">The engine.</param>
        public override void Compile(QueryBuilder queryBuilder, IEngine engine)
        {
            queryBuilder.WriteValue(Left, Parentheses.SubFragment).Write(" " + Op + " ").WriteValue(Right, true);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return ToStringBuilder.Build(b => b.WriteValue(Left, Parentheses.SubFragment)
                .Write(" " + Op + " ").WriteValue(Right, true));
        }
    }
}