using System;
using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Engines;
using Suilder.Exceptions;
using Suilder.Operators;

namespace Suilder.Core
{
    /// <summary>
    /// Implementation of <see cref="ISetOperator"/>.
    /// </summary>
    public class SetOperator : ISetOperator
    {
        /// <summary>
        /// The value to write before the set operator.
        /// </summary>
        /// <value>The value to write before the set operator.</value>
        protected IQueryFragment BeforeValue { get; set; }

        /// <summary>
        /// The value to write after the set operator.
        /// </summary>
        /// <value>The value to write after the set operator.</value>
        protected IQueryFragment AfterValue { get; set; }

        /// <summary>
        /// The "with" value.
        /// </summary>
        /// <value>The "with" value.</value>
        protected IQueryFragment WithValue { get; set; }

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
        /// The "order by" value.
        /// </summary>
        /// <value>The "order by" value.</value>
        protected IQueryFragment OrderByValue { get; set; }

        /// <summary>
        /// The "offset" value.
        /// </summary>
        /// <value>The "offset" value.</value>
        protected IQueryFragment OffsetValue { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SetOperator"/> class.
        /// </summary>
        /// <param name="op">The operator.</param>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        public SetOperator(string op, IQueryFragment left, IQueryFragment right)
        {
            Op = op;
            Left = left ?? throw new ArgumentNullException(nameof(left), "Left value is null.");
            Right = right ?? throw new ArgumentNullException(nameof(right), "Right value is null.");
        }

        /// <summary>
        /// Sets a value to write before the set operator.
        /// </summary>
        /// <param name="value">The value to write before the set operator.</param>
        /// <returns>The set operator.</returns>
        public virtual ISetOperator Before(IQueryFragment value)
        {
            BeforeValue = value;
            return this;
        }

        /// <summary>
        /// Sets a value to write after the set operator.
        /// </summary>
        /// <param name="value">The value to write after the set operator.</param>
        /// <returns>The set operator.</returns>
        public virtual ISetOperator After(IQueryFragment value)
        {
            AfterValue = value;
            return this;
        }

        /// <summary>
        /// Sets the "with" clause.
        /// </summary>
        /// <param name="with">The "with" clause.</param>
        /// <returns>The set operator.</returns>
        public virtual ISetOperator With(IWith with)
        {
            WithValue = with;
            return this;
        }

        /// <summary>
        /// Sets a raw "with" clause.
        /// <para>You must write the entire clause including the "with" keyword.</para>
        /// </summary>
        /// <param name="with">The "with" clause.</param>
        /// <returns>The set operator.</returns>
        public virtual ISetOperator With(IRawSql with)
        {
            WithValue = with;
            return this;
        }

        /// <summary>
        /// Sets the "with" clause.
        /// </summary>
        /// <param name="func">Function that returns the "with" clause.</param>
        /// <returns>The set operator.</returns>
        public virtual ISetOperator With(Func<IWith, IWith> func)
        {
            return With(func(SqlBuilder.Instance.With));
        }

        /// <summary>
        /// Creates a "with" clause and adds a value to the "with" clause.
        /// </summary>
        /// <param name="value">The value to add to the "with" clause.</param>
        /// <returns>The set operator.</returns>
        public virtual ISetOperator With(IQueryFragment value)
        {
            return With(SqlBuilder.Instance.With.Add(value));
        }

        /// <summary>
        /// Creates a "with" clause and adds the elements of the specified array to the end of the "with" clause.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the "with" clause.</param>
        /// <returns>The set operator.</returns>
        public virtual ISetOperator With(params IQueryFragment[] values)
        {
            return With(SqlBuilder.Instance.With.Add(values));
        }

        /// <summary>
        /// Creates a "with" clause and adds the elements of the specified collection to the end of the "with"
        /// clause.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the "with" clause.</param>
        /// <returns>The set operator.</returns>
        public virtual ISetOperator With(IEnumerable<IQueryFragment> values)
        {
            return With(SqlBuilder.Instance.With.Add(values));
        }

        /// <summary>
        /// Sets the "order by" clause.
        /// </summary>
        /// <param name="orderBy">The "order by" clause.</param>
        /// <returns>The set operator.</returns>
        public virtual ISetOperator OrderBy(IOrderBy orderBy)
        {
            OrderByValue = orderBy;
            return this;
        }

        /// <summary>
        /// Sets a raw "order by" clause.
        /// <para>You must write the entire clause including the "order by" keyword.</para>
        /// </summary>
        /// <param name="orderBy">The "order by" clause.</param>
        /// <returns>The set operator.</returns>
        public virtual ISetOperator OrderBy(IRawSql orderBy)
        {
            OrderByValue = orderBy;
            return this;
        }

        /// <summary>
        /// Sets the "order by" clause.
        /// </summary>
        /// <param name="func">Function that returns the "order by" clause.</param>
        /// <returns>The set operator.</returns>
        public virtual ISetOperator OrderBy(Func<IOrderBy, IOrderBy> func)
        {
            return OrderBy(func(SqlBuilder.Instance.OrderBy()));
        }

        /// <summary>
        /// Sets the "offset" clause.
        /// </summary>
        /// <param name="offset">The "offset" clause.</param>
        /// <returns>The set operator.</returns>
        public virtual ISetOperator Offset(IOffset offset)
        {
            OffsetValue = offset;
            return this;
        }

        /// <summary>
        /// Sets a raw "offset" clause.
        /// <para>You must write the entire clause.</para>
        /// </summary>
        /// <param name="offset">The "offset" clause.</param>
        /// <returns>The set operator.</returns>
        public virtual ISetOperator Offset(IRawSql offset)
        {
            OffsetValue = offset;
            return this;
        }

        /// <summary>
        /// Creates or adds an "offset" clause.
        /// </summary>
        /// <param name="offset">The number of rows to skip.</param>
        /// <returns>The set operator.</returns>
        public virtual ISetOperator Offset(object offset)
        {
            if (OffsetValue is IOffset offsetValue)
                offsetValue.Offset(offset);
            else
                OffsetValue = SqlBuilder.Instance.Offset(offset);
            return this;
        }

        /// <summary>
        /// Creates an "offset fetch" clause.
        /// </summary>
        /// <param name="offset">The number of rows to skip.</param>
        /// <param name="fetch">The number of rows to return.</param>
        /// <returns>The set operator.</returns>
        public virtual ISetOperator Offset(object offset, object fetch)
        {
            OffsetValue = SqlBuilder.Instance.Offset(offset, fetch);
            return this;
        }

        /// <summary>
        /// Creates or adds "fetch" clause.
        /// </summary>
        /// <param name="fetch">The number of rows to return.</param>
        /// <returns>The set operator.</returns>
        public virtual ISetOperator Fetch(object fetch)
        {
            if (OffsetValue is IOffset offsetValue)
                offsetValue.Fetch(fetch);
            else
                OffsetValue = SqlBuilder.Instance.Fetch(fetch);
            return this;
        }

        /// <summary>
        /// Compiles the fragment.
        /// </summary>
        /// <param name="queryBuilder">The query builder.</param>
        /// <param name="engine">The engine.</param>
        public virtual void Compile(QueryBuilder queryBuilder, IEngine engine)
        {
            if (BeforeValue != null)
                queryBuilder.WriteFragment(BeforeValue).Write(" ");

            if (WithValue != null)
                queryBuilder.WriteFragment(WithValue).Write(" ");

            if (engine.Options.SetOperatorWithSubQuery)
            {
                if (Left is ISetOperator)
                    queryBuilder.Write("SELECT * FROM ").WriteFragment(Left, true);
                else
                    queryBuilder.WriteFragment(Left, engine.Options.SetOperatorWrapQuery);
            }
            else
            {
                queryBuilder.WriteFragment(Left, engine.Options.SetOperatorWrapQuery || Left is ISetOperator);
            }

            IOperatorInfo opInfo = engine.GetOperator(Op);

            if (opInfo?.Function == true)
                throw new InvalidConfigurationException("The set operator cannot be a function.");

            queryBuilder.Write(" " + (opInfo?.Op ?? Op) + " ");

            if (engine.Options.SetOperatorWithSubQuery)
            {
                if (Right is ISetOperator)
                    queryBuilder.Write("SELECT * FROM ").WriteFragment(Right, true);
                else
                    queryBuilder.WriteFragment(Right, engine.Options.SetOperatorWrapQuery);
            }
            else
            {
                queryBuilder.WriteFragment(Right, engine.Options.SetOperatorWrapQuery || Right is ISetOperator);
            }

            if (OrderByValue != null)
                queryBuilder.Write(" ").WriteFragment(OrderByValue);

            if (OffsetValue != null)
                queryBuilder.Write(" ").WriteFragment(OffsetValue);

            if (AfterValue != null)
                queryBuilder.Write(" ").WriteFragment(AfterValue);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return ToStringBuilder.Build(b => b
                .IfNotNull(BeforeValue, x => b.WriteFragment(x).Write(" "))
                .IfNotNull(WithValue, x => b.WriteFragment(x).Write(" "))
                .WriteFragment(Left).Write(" " + Op + " ").WriteFragment(Right)
                .IfNotNull(OrderByValue, x => b.Write(" ").WriteFragment(x))
                .IfNotNull(OffsetValue, x => b.Write(" ").WriteFragment(x))
                .IfNotNull(AfterValue, x => b.Write(" ").WriteFragment(x)));
        }
    }
}