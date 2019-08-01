using System;
using System.Linq.Expressions;

namespace Suilder.Core
{
    /// <summary>
    /// A "case" statement.
    /// </summary>
    public interface ICase : IQueryFragment
    {
        /// <summary>
        /// Adds a "when" clause.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="value">The value returned when the condition is true.</param>
        /// <returns>The case statement.</returns>
        ICase When(IQueryFragment condition, object value);

        /// <summary>
        /// Adds a "when" clause.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="value">The value returned when the condition is true.</param>
        /// <returns>The case statement.</returns>
        ICase When(Expression<Func<bool>> condition, object value);

        /// <summary>
        /// Adds a "when" clause.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="value">The value returned when the condition is true.</param>
        /// <returns>The case statement.</returns>
        ICase When(Expression<Func<bool>> condition, Expression<Func<object>> value);

        /// <summary>
        /// Adds a "else" value.
        /// </summary>
        /// <param name="value">The else value.</param>
        /// <returns>The case statement.</returns>
        ICase Else(object value);

        /// <summary>
        /// Adds a "else" value.
        /// </summary>
        /// <param name="value">The else value.</param>
        /// <returns>The case statement.</returns>
        ICase Else(Expression<Func<object>> value);
    }
}