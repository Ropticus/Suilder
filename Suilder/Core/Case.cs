using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Suilder.Builder;
using Suilder.Engines;
using Suilder.Exceptions;

namespace Suilder.Core
{
    /// <summary>
    /// Implementation of <see cref="ICase"/>.
    /// </summary>
    public class Case : ICase
    {
        /// <summary>
        /// The list of conditions.
        /// </summary>
        /// <returns>The list of conditions.</returns>
        protected List<IQueryFragment> Conditions { get; set; } = new List<IQueryFragment>();

        /// <summary>
        /// The list of values.
        /// </summary>
        /// <returns>The list of values.</returns>
        protected List<object> Values { get; set; } = new List<object>();

        /// <summary>
        /// If has a else condition.
        /// </summary>
        /// <value>If has a else condition.</value>
        protected bool HasElse { get; set; }

        /// <summary>
        /// The else value.
        /// </summary>
        /// <value>The else value.</value>
        protected object ElseValue { get; set; }

        /// <summary>
        /// Adds a "when" clause.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="value">The value returned when the condition is true.</param>
        /// <returns>The case statement.</returns>
        public virtual ICase When(IQueryFragment condition, object value)
        {
            Conditions.Add(condition);
            Values.Add(value);
            return this;
        }

        /// <summary>
        /// Adds a "when" clause.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="value">The value returned when the condition is true.</param>
        /// <returns>The case statement.</returns>
        public virtual ICase When(Expression<Func<bool>> condition, object value)
        {
            return When(SqlBuilder.Instance.Op(condition), value);
        }

        /// <summary>
        /// Adds a "when" clause.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="value">The value returned when the condition is true.</param>
        /// <returns>The case statement.</returns>
        public virtual ICase When(Expression<Func<bool>> condition, Expression<Func<object>> value)
        {
            if (value == null)
                return When(condition, (object)null);

            return When(SqlBuilder.Instance.Op(condition), SqlBuilder.Instance.Val(value));
        }

        /// <summary>
        /// Adds a "else" value.
        /// </summary>
        /// <param name="value">The else value.</param>
        /// <returns>The case statement.</returns>
        public virtual ICase Else(object value)
        {
            HasElse = true;
            ElseValue = value;
            return this;
        }

        /// <summary>
        /// Adds a "else" value.
        /// </summary>
        /// <param name="value">The else value.</param>
        /// <returns>The case statement.</returns>
        public virtual ICase Else(Expression<Func<object>> value)
        {
            if (value == null)
                return Else((object)value);

            return Else(SqlBuilder.Instance.Val(value));
        }

        /// <summary>
        /// Compiles the fragment.
        /// </summary>
        /// <param name="queryBuilder">The query builder.</param>
        /// <param name="engine">The engine.</param>
        public virtual void Compile(QueryBuilder queryBuilder, IEngine engine)
        {
            if (Conditions.Count == 0)
                throw new CompileException("Add at least one \"when\" clause.");

            queryBuilder.Write("CASE");

            for (int i = 0; i < Conditions.Count; i++)
            {
                queryBuilder.Write(" WHEN ").WriteFragment(Conditions[i]);
                queryBuilder.Write(" THEN ").WriteValue(Values[i]);
            }

            if (HasElse)
            {
                queryBuilder.Write(" ELSE ").WriteValue(ElseValue);
            }

            queryBuilder.Write(" END");
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return ToStringBuilder.Build(b => b.Write("CASE")
                .ForEach(Conditions, (x, i) => b
                    .Write(" WHEN ").WriteFragment(Conditions[i])
                    .Write(" THEN ").WriteValue(Values[i]))
                .IfNotNull(ElseValue, x => b.Write(" ELSE ").WriteValue(x)));
        }
    }
}