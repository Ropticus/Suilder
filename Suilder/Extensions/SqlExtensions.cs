using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Suilder.Builder;
using Suilder.Core;

namespace Suilder.Extensions
{
    /// <summary>
    /// Extensions of the builder.
    /// </summary>
    public static class SqlExtensions
    {
        /// <summary>
        /// Creates an "equal to" operator.
        /// </summary>
        /// <param name="left">The <see cref="IQueryFragment"/>.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "equal to" operator.</returns>
        public static IOperator Eq(this IQueryFragment left, object right)
        {
            return SqlBuilder.Instance.Eq(left, right);
        }

        /// <summary>
        /// Creates an "equal to" operator.
        /// </summary>
        /// <param name="left">The <see cref="IQueryFragment"/>.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "equal to" operator.</returns>
        public static IOperator Eq(this IQueryFragment left, Expression<Func<object>> right)
        {
            if (right == null)
                return Eq(left, (object)right);

            return SqlBuilder.Instance.Eq(left, SqlBuilder.Instance.Val(right));
        }

        /// <summary>
        /// Creates an "not equal to" operator.
        /// </summary>
        /// <param name="left">The <see cref="IQueryFragment"/>.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "not equal to" operator.</returns>
        public static IOperator NotEq(this IQueryFragment left, object right)
        {
            return SqlBuilder.Instance.NotEq(left, right);
        }

        /// <summary>
        /// Creates an "not equal to" operator.
        /// </summary>
        /// <param name="left">The <see cref="IQueryFragment"/>.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "not equal to" operator.</returns>
        public static IOperator NotEq(this IQueryFragment left, Expression<Func<object>> right)
        {
            if (right == null)
                return NotEq(left, (object)right);

            return SqlBuilder.Instance.NotEq(left, SqlBuilder.Instance.Val(right));
        }

        /// <summary>
        /// Creates an "like" operator.
        /// </summary>
        /// <param name="left">The <see cref="IQueryFragment"/>.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "like" operator.</returns>
        public static IOperator Like(this IQueryFragment left, object right)
        {
            return SqlBuilder.Instance.Like(left, right);
        }

        /// <summary>
        /// Creates an "like" operator.
        /// </summary>
        /// <param name="left">The <see cref="IQueryFragment"/>.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "like" operator.</returns>
        public static IOperator Like(this IQueryFragment left, Expression<Func<object>> right)
        {
            if (right == null)
                return Like(left, (object)right);

            return SqlBuilder.Instance.Like(left, SqlBuilder.Instance.Val(right));
        }

        /// <summary>
        /// Creates an "not like" operator.
        /// </summary>
        /// <param name="left">The <see cref="IQueryFragment"/>.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "not like" operator.</returns>
        public static IOperator NotLike(this IQueryFragment left, object right)
        {
            return SqlBuilder.Instance.NotLike(left, right);
        }

        /// <summary>
        /// Creates an "not like" operator.
        /// </summary>
        /// <param name="left">The <see cref="IQueryFragment"/>.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "not like" operator.</returns>
        public static IOperator NotLike(this IQueryFragment left, Expression<Func<object>> right)
        {
            if (right == null)
                return NotLike(left, (object)right);

            return SqlBuilder.Instance.NotLike(left, SqlBuilder.Instance.Val(right));
        }

        /// <summary>
        /// Creates an "less than" operator.
        /// </summary>
        /// <param name="left">The <see cref="IQueryFragment"/>.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "less than" operator.</returns>
        public static IOperator Lt(this IQueryFragment left, object right)
        {
            return SqlBuilder.Instance.Lt(left, right);
        }

        /// <summary>
        /// Creates an "less than" operator.
        /// </summary>
        /// <param name="left">The <see cref="IQueryFragment"/>.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "less than" operator.</returns>
        public static IOperator Lt(this IQueryFragment left, Expression<Func<object>> right)
        {
            if (right == null)
                return Lt(left, (object)right);

            return SqlBuilder.Instance.Lt(left, SqlBuilder.Instance.Val(right));
        }

        /// <summary>
        /// Creates an "less than or equal to" operator.
        /// </summary>
        /// <param name="left">The <see cref="IQueryFragment"/>.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "less than or equal to" operator.</returns>
        public static IOperator Le(this IQueryFragment left, object right)
        {
            return SqlBuilder.Instance.Le(left, right);
        }

        /// <summary>
        /// Creates an "less than or equal to" operator.
        /// </summary>
        /// <param name="left">The <see cref="IQueryFragment"/>.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "less than or equal to" operator.</returns>
        public static IOperator Le(this IQueryFragment left, Expression<Func<object>> right)
        {
            if (right == null)
                return Le(left, (object)right);

            return SqlBuilder.Instance.Le(left, SqlBuilder.Instance.Val(right));
        }

        /// <summary>
        /// Creates an "greater than" operator.
        /// </summary>
        /// <param name="left">The <see cref="IQueryFragment"/>.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "greater than" operator.</returns>
        public static IOperator Gt(this IQueryFragment left, object right)
        {
            return SqlBuilder.Instance.Gt(left, right);
        }

        /// <summary>
        /// Creates an "greater than" operator.
        /// </summary>
        /// <param name="left">The <see cref="IQueryFragment"/>.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "greater than" operator.</returns>
        public static IOperator Gt(this IQueryFragment left, Expression<Func<object>> right)
        {
            if (right == null)
                return Gt(left, (object)right);

            return SqlBuilder.Instance.Gt(left, SqlBuilder.Instance.Val(right));
        }

        /// <summary>
        /// Creates an "greater than or equal to" operator.
        /// </summary>
        /// <param name="left">The <see cref="IQueryFragment"/>.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "greater than or equal to" operator.</returns>
        public static IOperator Ge(this IQueryFragment left, object right)
        {
            return SqlBuilder.Instance.Ge(left, right);
        }

        /// <summary>
        /// Creates an "greater than or equal to" operator.
        /// </summary>
        /// <param name="left">The <see cref="IQueryFragment"/>.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "greater than or equal to" operator.</returns>
        public static IOperator Ge(this IQueryFragment left, Expression<Func<object>> right)
        {
            if (right == null)
                return Ge(left, (object)right);

            return SqlBuilder.Instance.Ge(left, SqlBuilder.Instance.Val(right));
        }

        /// <summary>
        /// Creates an "in" operator.
        /// </summary>
        /// <param name="left">The <see cref="IQueryFragment"/>.</param>
        /// <param name="right">Right value. An <see cref="IEnumerable"/> is divided into multiple values.</param>
        /// <returns>The "in" operator.</returns>
        public static IOperator In(this IQueryFragment left, object right)
        {
            return SqlBuilder.Instance.In(left, right);
        }

        /// <summary>
        /// Creates an "in" operator.
        /// </summary>
        /// <param name="left">The <see cref="IQueryFragment"/>.</param>
        /// <param name="right">Right value. An <see cref="IEnumerable"/> is divided into multiple values.</param>
        /// <returns>The "in" operator.</returns>
        public static IOperator In(this IQueryFragment left, Expression<Func<object>> right)
        {
            if (right == null)
                return In(left, (object)right);

            return SqlBuilder.Instance.In(left, SqlBuilder.Instance.Val(right));
        }

        /// <summary>
        /// Creates an "not in" operator.
        /// </summary>
        /// <param name="left">The <see cref="IQueryFragment"/>.</param>
        /// <param name="right">Right value. An <see cref="IEnumerable"/> is divided into multiple values.</param>
        /// <returns>The "not in" operator.</returns>
        public static IOperator NotIn(this IQueryFragment left, object right)
        {
            return SqlBuilder.Instance.NotIn(left, right);
        }

        /// <summary>
        /// Creates an "not in" operator.
        /// </summary>
        /// <param name="left">The <see cref="IQueryFragment"/>.</param>
        /// <param name="right">Right value. An <see cref="IEnumerable"/> is divided into multiple values.</param>
        /// <returns>The "not in" operator.</returns>
        public static IOperator NotIn(this IQueryFragment left, Expression<Func<object>> right)
        {
            if (right == null)
                return NotIn(left, (object)right);

            return SqlBuilder.Instance.NotIn(left, SqlBuilder.Instance.Val(right));
        }

        /// <summary>
        /// Creates a "not" operator.
        /// </summary>
        /// <param name="value">The <see cref="IQueryFragment"/>.</param>
        /// <returns>The "not" operator.</returns>
        public static IOperator Not(this IQueryFragment value)
        {
            return SqlBuilder.Instance.Not(value);
        }

        /// <summary>
        /// Creates an "is null" operator.
        /// </summary>
        /// <param name="value">The <see cref="IQueryFragment"/>.</param>
        /// <returns>The "is null" operator.</returns>
        public static IOperator IsNull(this IQueryFragment value)
        {
            return SqlBuilder.Instance.IsNull(value);
        }

        /// <summary>
        /// Creates an "is not null" operator.
        /// </summary>
        /// <param name="value">The <see cref="IQueryFragment"/>.</param>
        /// <returns>The "is not null" operator.</returns>
        public static IOperator IsNotNull(this IQueryFragment value)
        {
            return SqlBuilder.Instance.IsNotNull(value);
        }

        /// <summary>
        /// Creates a "like" operator in an expression.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <exception cref="InvalidOperationException">The method is called outside an expression.</exception>
        public static bool Like(this string left, string right)
        {
            throw new InvalidOperationException("Only for expressions.");
        }

        /// <summary>
        /// Creates a "not like" operator in an expression.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <exception cref="InvalidOperationException">The method is called outside an expression.</exception>
        public static bool NotLike(this string left, string right)
        {
            throw new InvalidOperationException("Only for expressions.");
        }

        /// <summary>
        /// Creates a "in" operator in an expression.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right values.</param>
        /// <exception cref="InvalidOperationException">The method is called outside an expression.</exception>
        public static bool In<T>(this T left, IEnumerable<T> right) where T : IComparable
        {
            throw new InvalidOperationException("Only for expressions.");
        }

        /// <summary>
        /// Creates a "not in" operator in an expression.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right values.</param>
        /// <exception cref="InvalidOperationException">The method is called outside an expression.</exception>
        public static bool NotIn<T>(this T left, IEnumerable<T> right) where T : IComparable
        {
            throw new InvalidOperationException("Only for expressions.");
        }

        /// <summary>
        /// Creates a pattern that match the start of the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The like start pattern.</returns>
        public static string ToLikeStart(this string value)
        {
            return SqlBuilder.Instance.ToLikeStart(value);
        }

        /// <summary>
        /// Creates a pattern that match the end of the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The like end pattern.</returns>
        public static string ToLikeEnd(this string value)
        {
            return SqlBuilder.Instance.ToLikeEnd(value);
        }

        /// <summary>
        /// Creates a pattern that match anywhere of the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The like anywhere value.</returns>
        public static string ToLikeAny(this string value)
        {
            return SqlBuilder.Instance.ToLikeAny(value);
        }
    }
}