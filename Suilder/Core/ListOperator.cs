using System.Collections;
using Suilder.Builder;

namespace Suilder.Core
{
    /// <summary>
    /// Implementation of <see cref="IOperator"/> with a list of values.
    /// </summary>
    public class ListOperator : Operator, IOperator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListOperator"/> class.
        /// </summary>
        /// <param name="op">The operator.</param>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value. An <see cref="IEnumerable"/> is divided into multiple values.</param>
        public ListOperator(string op, object left, object right) : base(op, left, right)
        {
            if (right is IEnumerable list && !(right is IQueryFragment))
            {
                ISubList subList = SqlBuilder.Instance.SubList;
                foreach (object value in list)
                {
                    subList.Add(value);
                }

                Right = subList;
            }
        }
    }
}