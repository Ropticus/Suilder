using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Suilder.Builder;
using Suilder.Engines;
using Suilder.Exceptions;
using Suilder.Functions;
using Suilder.Operators;

namespace Suilder.Core
{
    /// <summary>
    /// Implementation of <see cref="ILogicalOperator"/>.
    /// </summary>
    public class LogicalOperator : QueryFragmentList<IQueryFragment, bool>, ILogicalOperator
    {
        /// <summary>
        /// The operator.
        /// </summary>
        /// <value>The operator.</value>
        public string Op { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogicalOperator"/> class.
        /// </summary>
        /// <param name="op">The operator.</param>
        public LogicalOperator(string op)
        {
            this.Op = op;
        }

        /// <summary>
        /// Adds a value to the end of the <see cref="ILogicalOperator"/>.
        /// </summary>
        /// <param name="value">The value to add to the end of the <see cref="ILogicalOperator"/>.</param>
        public override void Add(Expression<Func<bool>> value)
        {
            Values.Add(SqlBuilder.Instance.Op(value));
        }

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="ILogicalOperator"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// <see cref="ILogicalOperator"/>.</param>
        public override void Add(params Expression<Func<bool>>[] values)
        {
            Values.AddRange(values.Select(x => SqlBuilder.Instance.Op(x)));
        }

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="ILogicalOperator"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the
        /// <see cref="ILogicalOperator"/>.</param>
        public override void Add(IEnumerable<Expression<Func<bool>>> values)
        {
            Values.AddRange(values.Select(x => SqlBuilder.Instance.Op(x)));
        }

        /// <summary>
        /// Adds a value to the end of the <see cref="ILogicalOperator"/>.
        /// </summary>
        /// <param name="value">The value to add to the end of the <see cref="ILogicalOperator"/>.</param>
        /// <returns>The logical operator.</returns>
        ILogicalOperator ILogicalOperator.Add(IQueryFragment value)
        {
            Add(value);
            return this;
        }

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="ILogicalOperator"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// <see cref="ILogicalOperator"/>.</param>
        /// <returns>The logical operator.</returns>
        ILogicalOperator ILogicalOperator.Add(params IQueryFragment[] values)
        {
            Add(values);
            return this;
        }

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="ILogicalOperator"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the
        /// <see cref="ILogicalOperator"/>.</param>
        /// <returns>The logical operator.</returns>
        ILogicalOperator ILogicalOperator.Add(IEnumerable<IQueryFragment> values)
        {
            Add(values);
            return this;
        }

        /// <summary>
        /// Adds a value to the end of the <see cref="ILogicalOperator"/>.
        /// </summary>
        /// <param name="value">The value to add to the end of the <see cref="ILogicalOperator"/>.</param>
        /// <returns>The logical operator.</returns>
        ILogicalOperator ILogicalOperator.Add(Expression<Func<bool>> value)
        {
            Add(value);
            return this;
        }

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="ILogicalOperator"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// <see cref="ILogicalOperator"/>.</param>
        /// <returns>The logical operator.</returns>
        ILogicalOperator ILogicalOperator.Add(params Expression<Func<bool>>[] values)
        {
            Add(values);
            return this;
        }

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="ILogicalOperator"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the
        /// <see cref="ILogicalOperator"/>.</param>
        /// <returns>The logical operator.</returns>
        ILogicalOperator ILogicalOperator.Add(IEnumerable<Expression<Func<bool>>> values)
        {
            Add(values);
            return this;
        }

        /// <summary>
        /// Compiles the fragment.
        /// </summary>
        /// <param name="queryBuilder">The query builder.</param>
        /// <param name="engine">The engine.</param>
        public override void Compile(QueryBuilder queryBuilder, IEngine engine)
        {
            if (Values.Count == 0)
                throw new CompileException("List is empty.");

            IOperatorInfo opInfo = engine.GetOperator(Op);

            if (opInfo?.Function == true)
            {
                FunctionHelper.BinaryOperator(queryBuilder, engine, opInfo.Op, Values);
            }
            else
            {
                string separator = " " + (opInfo?.Op ?? Op) + " ";
                for (int i = 0; i < Values.Count; i++)
                {
                    if (i != 0)
                        queryBuilder.Write(separator);

                    IQueryFragment value = Values[i];
                    queryBuilder.WriteFragment(value, value is ILogicalOperator || value is ISubQuery);
                }
            }
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return ToStringBuilder.Build(b => b
                .Join(" " + Op + " ", Values, (x) => b.WriteFragment(x, x is ILogicalOperator || x is ISubQuery)));
        }
    }
}