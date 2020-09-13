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
        /// If has a case value.
        /// </summary>
        /// <value>If has a case value.</value>
        protected bool HasCaseValue { get; set; }

        /// <summary>
        /// The case value.
        /// </summary>
        /// <value>The case value.</value>
        protected object CaseValue { get; set; }

        /// <summary>
        /// The list of conditions.
        /// </summary>
        /// <returns>The list of conditions.</returns>
        protected List<object> Conditions { get; set; } = new List<object>();

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
        /// Initializes a new instance of the <see cref="Case"/> class.
        /// </summary>
        public Case()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Case"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public Case(object value)
        {
            HasCaseValue = true;
            CaseValue = value;
        }

        /// <summary>
        /// Adds a "when" clause.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="value">The value returned when the condition is true.</param>
        /// <returns>The case statement.</returns>
        public virtual ICase When(object condition, object value)
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
        public virtual ICase When(object condition, Expression<Func<object>> value)
        {
            if (value == null)
                return When(condition, (object)null);

            return When(condition, SqlBuilder.Instance.Val(value));
        }

        /// <summary>
        /// Adds a "when" clause.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="value">The value returned when the condition is true.</param>
        /// <returns>The case statement.</returns>
        public virtual ICase When(Expression<Func<object>> condition, object value)
        {
            if (condition == null)
                return When((object)null, value);

            if (HasCaseValue)
                return When(SqlBuilder.Instance.Val(condition), value);
            else
                return When(SqlBuilder.Instance.Op(condition), value);
        }

        /// <summary>
        /// Adds a "when" clause.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="value">The value returned when the condition is true.</param>
        /// <returns>The case statement.</returns>
        public virtual ICase When(Expression<Func<object>> condition, Expression<Func<object>> value)
        {
            if (condition == null && value == null)
                return When((object)null, (object)null);
            else if (condition == null)
                return When((object)null, value);
            else if (value == null)
                return When(condition, (object)null);

            if (HasCaseValue)
                return When(SqlBuilder.Instance.Val(condition), SqlBuilder.Instance.Val(value));
            else
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

            if (HasCaseValue)
                queryBuilder.Write(" ").WriteValue(CaseValue);

            for (int i = 0; i < Conditions.Count; i++)
            {
                queryBuilder.Write(" WHEN ").WriteValue(Conditions[i]);
                queryBuilder.Write(" THEN ").WriteValue(Values[i]);
            }

            if (HasElse)
                queryBuilder.Write(" ELSE ").WriteValue(ElseValue);

            queryBuilder.Write(" END");
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return ToStringBuilder.Build(b => b.Write("CASE")
                .IfNotNull(CaseValue, x => b.Write(" ").WriteValue(x))
                .ForEach(Conditions, (x, i) => b
                    .Write(" WHEN ").WriteValue(Conditions[i])
                    .Write(" THEN ").WriteValue(Values[i]))
                .IfNotNull(ElseValue, x => b.Write(" ELSE ").WriteValue(x))
                .Write(" END"));
        }
    }
}