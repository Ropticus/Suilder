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
        /// <param name="right">The right value.</param>
        /// <returns>The "equal to" operator.</returns>
        public static IOperator Eq(this IQueryFragment left, object right)
        {
            return SqlBuilder.Instance.Eq(left, right);
        }

        /// <summary>
        /// Creates an "equal to" operator.
        /// </summary>
        /// <param name="left">The <see cref="IQueryFragment"/>.</param>
        /// <param name="right">The right value.</param>
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
        /// <param name="right">The right value.</param>
        /// <returns>The "not equal to" operator.</returns>
        public static IOperator NotEq(this IQueryFragment left, object right)
        {
            return SqlBuilder.Instance.NotEq(left, right);
        }

        /// <summary>
        /// Creates an "not equal to" operator.
        /// </summary>
        /// <param name="left">The <see cref="IQueryFragment"/>.</param>
        /// <param name="right">The right value.</param>
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
        /// <param name="right">The right value.</param>
        /// <returns>The "like" operator.</returns>
        public static IOperator Like(this IQueryFragment left, object right)
        {
            return SqlBuilder.Instance.Like(left, right);
        }

        /// <summary>
        /// Creates an "like" operator.
        /// </summary>
        /// <param name="left">The <see cref="IQueryFragment"/>.</param>
        /// <param name="right">The right value.</param>
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
        /// <param name="right">The right value.</param>
        /// <returns>The "not like" operator.</returns>
        public static IOperator NotLike(this IQueryFragment left, object right)
        {
            return SqlBuilder.Instance.NotLike(left, right);
        }

        /// <summary>
        /// Creates an "not like" operator.
        /// </summary>
        /// <param name="left">The <see cref="IQueryFragment"/>.</param>
        /// <param name="right">The right value.</param>
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
        /// <param name="right">The right value.</param>
        /// <returns>The "less than" operator.</returns>
        public static IOperator Lt(this IQueryFragment left, object right)
        {
            return SqlBuilder.Instance.Lt(left, right);
        }

        /// <summary>
        /// Creates an "less than" operator.
        /// </summary>
        /// <param name="left">The <see cref="IQueryFragment"/>.</param>
        /// <param name="right">The right value.</param>
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
        /// <param name="right">The right value.</param>
        /// <returns>The "less than or equal to" operator.</returns>
        public static IOperator Le(this IQueryFragment left, object right)
        {
            return SqlBuilder.Instance.Le(left, right);
        }

        /// <summary>
        /// Creates an "less than or equal to" operator.
        /// </summary>
        /// <param name="left">The <see cref="IQueryFragment"/>.</param>
        /// <param name="right">The right value.</param>
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
        /// <param name="right">The right value.</param>
        /// <returns>The "greater than" operator.</returns>
        public static IOperator Gt(this IQueryFragment left, object right)
        {
            return SqlBuilder.Instance.Gt(left, right);
        }

        /// <summary>
        /// Creates an "greater than" operator.
        /// </summary>
        /// <param name="left">The <see cref="IQueryFragment"/>.</param>
        /// <param name="right">The right value.</param>
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
        /// <param name="right">The right value.</param>
        /// <returns>The "greater than or equal to" operator.</returns>
        public static IOperator Ge(this IQueryFragment left, object right)
        {
            return SqlBuilder.Instance.Ge(left, right);
        }

        /// <summary>
        /// Creates an "greater than or equal to" operator.
        /// </summary>
        /// <param name="left">The <see cref="IQueryFragment"/>.</param>
        /// <param name="right">The right value.</param>
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
        /// <param name="right">The right value. An <see cref="IEnumerable"/> is divided into multiple values.</param>
        /// <returns>The "in" operator.</returns>
        public static IOperator In(this IQueryFragment left, object right)
        {
            return SqlBuilder.Instance.In(left, right);
        }

        /// <summary>
        /// Creates an "in" operator.
        /// </summary>
        /// <param name="left">The <see cref="IQueryFragment"/>.</param>
        /// <param name="right">The right value. An <see cref="IEnumerable"/> is divided into multiple values.</param>
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
        /// <param name="right">The right value. An <see cref="IEnumerable"/> is divided into multiple values.</param>
        /// <returns>The "not in" operator.</returns>
        public static IOperator NotIn(this IQueryFragment left, object right)
        {
            return SqlBuilder.Instance.NotIn(left, right);
        }

        /// <summary>
        /// Creates an "not in" operator.
        /// </summary>
        /// <param name="left">The <see cref="IQueryFragment"/>.</param>
        /// <param name="right">The right value. An <see cref="IEnumerable"/> is divided into multiple values.</param>
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
        /// Creates a "between" operator.
        /// </summary>
        /// <param name="left">The <see cref="IQueryFragment"/>.</param>
        /// <param name="min">The min value.</param>
        /// <param name="max">The max value.</param>
        /// <returns>The "between" operator.</returns>
        public static IOperator Between(this IQueryFragment left, object min, object max)
        {
            return SqlBuilder.Instance.Between(left, min, max);
        }

        /// <summary>
        /// Creates a "between" operator.
        /// </summary>
        /// <param name="left">The <see cref="IQueryFragment"/>.</param>
        /// <param name="min">The min value.</param>
        /// <param name="max">The max value.</param>
        /// <returns>The "between" operator.</returns>
        public static IOperator Between(this IQueryFragment left, Expression<Func<object>> min,
            Expression<Func<object>> max)
        {
            if (min == null && max == null)
                return Between(left, min, (object)max);

            return SqlBuilder.Instance.Between(left, SqlBuilder.Instance.Val(min), SqlBuilder.Instance.Val(max));
        }

        /// <summary>
        /// Creates a "not between" operator.
        /// </summary>
        /// <param name="left">The <see cref="IQueryFragment"/>.</param>
        /// <param name="min">The min value.</param>
        /// <param name="max">The max value.</param>
        /// <returns>The "not between" operator.</returns>
        public static IOperator NotBetween(this IQueryFragment left, object min, object max)
        {
            return SqlBuilder.Instance.NotBetween(left, min, max);
        }

        /// <summary>
        /// Creates a "not between" operator.
        /// </summary>
        /// <param name="left">The <see cref="IQueryFragment"/>.</param>
        /// <param name="min">The min value.</param>
        /// <param name="max">The max value.</param>
        /// <returns>The "not between" operator.</returns>
        public static IOperator NotBetween(this IQueryFragment left, Expression<Func<object>> min,
            Expression<Func<object>> max)
        {
            if (min == null && max == null)
                return NotBetween(left, min, (object)max);

            return SqlBuilder.Instance.NotBetween(left, SqlBuilder.Instance.Val(min), SqlBuilder.Instance.Val(max));
        }

        /// <summary>
        /// Creates an "add" operator.
        /// </summary>
        /// <param name="left">The <see cref="IQueryFragment"/>.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The "add" operator.</returns>
        public static IArithOperator Plus(this IQueryFragment left, object right)
        {
            return SqlBuilder.Instance.Add.Add(left).Add(right);
        }

        /// <summary>
        /// Creates an "add" operator.
        /// </summary>
        /// <param name="left">The <see cref="IQueryFragment"/>.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The "add" operator.</returns>
        public static IArithOperator Plus(this IQueryFragment left, Expression<Func<object>> right)
        {
            if (right == null)
                return Plus(left, (object)right);

            return SqlBuilder.Instance.Add.Add(left).Add(right);
        }

        /// <summary>
        /// Creates a "subtract" operator.
        /// </summary>
        /// <param name="left">The <see cref="IQueryFragment"/>.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The "subtract" operator.</returns>
        public static IArithOperator Minus(this IQueryFragment left, object right)
        {
            return SqlBuilder.Instance.Subtract.Add(left).Add(right);
        }

        /// <summary>
        /// Creates a "subtract" operator.
        /// </summary>
        /// <param name="left">The <see cref="IQueryFragment"/>.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The "subtract" operator.</returns>
        public static IArithOperator Minus(this IQueryFragment left, Expression<Func<object>> right)
        {
            if (right == null)
                return Minus(left, (object)right);

            return SqlBuilder.Instance.Subtract.Add(left).Add(right);
        }

        /// <summary>
        /// Creates a "multiply" operator.
        /// </summary>
        /// <param name="left">The <see cref="IQueryFragment"/>.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The "multiply" operator.</returns>
        public static IArithOperator Multiply(this IQueryFragment left, object right)
        {
            return SqlBuilder.Instance.Multiply.Add(left).Add(right);
        }

        /// <summary>
        /// Creates a "multiply" operator.
        /// </summary>
        /// <param name="left">The <see cref="IQueryFragment"/>.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The "multiply" operator.</returns>
        public static IArithOperator Multiply(this IQueryFragment left, Expression<Func<object>> right)
        {
            if (right == null)
                return Multiply(left, (object)right);

            return SqlBuilder.Instance.Multiply.Add(left).Add(right);
        }

        /// <summary>
        /// Creates a "divide" operator.
        /// </summary>
        /// <param name="left">The <see cref="IQueryFragment"/>.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The "divide" operator.</returns>
        public static IArithOperator Divide(this IQueryFragment left, object right)
        {
            return SqlBuilder.Instance.Divide.Add(left).Add(right);
        }

        /// <summary>
        /// Creates a "divide" operator.
        /// </summary>
        /// <param name="left">The <see cref="IQueryFragment"/>.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The "divide" operator.</returns>
        public static IArithOperator Divide(this IQueryFragment left, Expression<Func<object>> right)
        {
            if (right == null)
                return Divide(left, (object)right);

            return SqlBuilder.Instance.Divide.Add(left).Add(right);
        }

        /// <summary>
        /// Creates a "modulo" operator.
        /// </summary>
        /// <param name="left">The <see cref="IQueryFragment"/>.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The "modulo" operator.</returns>
        public static IArithOperator Modulo(this IQueryFragment left, object right)
        {
            return SqlBuilder.Instance.Modulo.Add(left).Add(right);
        }

        /// <summary>
        /// Creates a "modulo" operator.
        /// </summary>
        /// <param name="left">The <see cref="IQueryFragment"/>.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The "modulo" operator.</returns>
        public static IArithOperator Modulo(this IQueryFragment left, Expression<Func<object>> right)
        {
            if (right == null)
                return Modulo(left, (object)right);

            return SqlBuilder.Instance.Modulo.Add(left).Add(right);
        }

        /// <summary>
        /// Creates a "negate" operator.
        /// </summary>
        /// <param name="value">The <see cref="IQueryFragment"/>.</param>
        /// <returns>The "negate" operator.</returns>
        public static IOperator Negate(this IQueryFragment value)
        {
            return SqlBuilder.Instance.Negate(value);
        }

        /// <summary>
        /// Creates a bitwise "and" operator.
        /// </summary>
        /// <param name="left">The <see cref="IQueryFragment"/>.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The bitwise "and" operator.</returns>
        public static IBitOperator BitAnd(this IQueryFragment left, object right)
        {
            return SqlBuilder.Instance.BitAnd.Add(left).Add(right);
        }

        /// <summary>
        /// Creates a bitwise "and" operator.
        /// </summary>
        /// <param name="left">The <see cref="IQueryFragment"/>.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The bitwise "and" operator.</returns>
        public static IBitOperator BitAnd(this IQueryFragment left, Expression<Func<object>> right)
        {
            if (right == null)
                return BitAnd(left, (object)right);

            return SqlBuilder.Instance.BitAnd.Add(left).Add(right);
        }

        /// <summary>
        /// Creates a bitwise "or" operator.
        /// </summary>
        /// <param name="left">The <see cref="IQueryFragment"/>.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The bitwise "or" operator.</returns>
        public static IBitOperator BitOr(this IQueryFragment left, object right)
        {
            return SqlBuilder.Instance.BitOr.Add(left).Add(right);
        }

        /// <summary>
        /// Creates a bitwise "or" operator.
        /// </summary>
        /// <param name="left">The <see cref="IQueryFragment"/>.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The bitwise "or" operator.</returns>
        public static IBitOperator BitOr(this IQueryFragment left, Expression<Func<object>> right)
        {
            if (right == null)
                return BitOr(left, (object)right);

            return SqlBuilder.Instance.BitOr.Add(left).Add(right);
        }

        /// <summary>
        /// Creates a bitwise "xor" operator.
        /// </summary>
        /// <param name="left">The <see cref="IQueryFragment"/>.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The bitwise "xor" operator.</returns>
        public static IBitOperator BitXor(this IQueryFragment left, object right)
        {
            return SqlBuilder.Instance.BitXor.Add(left).Add(right);
        }

        /// <summary>
        /// Creates a bitwise "xor" operator.
        /// </summary>
        /// <param name="left">The <see cref="IQueryFragment"/>.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The bitwise "xor" operator.</returns>
        public static IBitOperator BitXor(this IQueryFragment left, Expression<Func<object>> right)
        {
            if (right == null)
                return BitXor(left, (object)right);

            return SqlBuilder.Instance.BitXor.Add(left).Add(right);
        }

        /// <summary>
        /// Creates a bitwise "not" operator.
        /// </summary>
        /// <param name="value">The <see cref="IQueryFragment"/>.</param>
        /// <returns>The bitwise "not" operator.</returns>
        public static IOperator BitNot(this IQueryFragment value)
        {
            return SqlBuilder.Instance.BitNot(value);
        }

        /// <summary>
        /// Creates a "left shift" operator.
        /// </summary>
        /// <param name="left">The <see cref="IQueryFragment"/>.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The "left shift" operator.</returns>
        public static IBitOperator LeftShift(this IQueryFragment left, object right)
        {
            return SqlBuilder.Instance.LeftShift.Add(left).Add(right);
        }

        /// <summary>
        /// Creates a "left shift" operator.
        /// </summary>
        /// <param name="left">The <see cref="IQueryFragment"/>.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The "left shift" operator.</returns>
        public static IBitOperator LeftShift(this IQueryFragment left, Expression<Func<object>> right)
        {
            if (right == null)
                return LeftShift(left, (object)right);

            return SqlBuilder.Instance.LeftShift.Add(left).Add(right);
        }

        /// <summary>
        /// Creates a "right shift" operator.
        /// </summary>
        /// <param name="left">The <see cref="IQueryFragment"/>.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The "right shift" operator.</returns>
        public static IBitOperator RightShift(this IQueryFragment left, object right)
        {
            return SqlBuilder.Instance.RightShift.Add(left).Add(right);
        }

        /// <summary>
        /// Creates a "right shift" operator.
        /// </summary>
        /// <param name="left">The <see cref="IQueryFragment"/>.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The "right shift" operator.</returns>
        public static IBitOperator RightShift(this IQueryFragment left, Expression<Func<object>> right)
        {
            if (right == null)
                return RightShift(left, (object)right);

            return SqlBuilder.Instance.RightShift.Add(left).Add(right);
        }

        /// <summary>
        /// Creates a "like" operator in an expression.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <exception cref="NotSupportedException">The method is called outside an expression.</exception>
        public static bool Like(this string left, string right)
        {
            throw new NotSupportedException("Only for expressions.");
        }

        /// <summary>
        /// Creates a "not like" operator in an expression.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <exception cref="NotSupportedException">The method is called outside an expression.</exception>
        public static bool NotLike(this string left, string right)
        {
            throw new NotSupportedException("Only for expressions.");
        }

        /// <summary>
        /// Creates a "in" operator in an expression.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right values.</param>
        /// <exception cref="NotSupportedException">The method is called outside an expression.</exception>
        public static bool In<T>(this T left, IEnumerable<T> right) where T : IComparable
        {
            throw new NotSupportedException("Only for expressions.");
        }

        /// <summary>
        /// Creates a "not in" operator in an expression.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right values.</param>
        /// <exception cref="NotSupportedException">The method is called outside an expression.</exception>
        public static bool NotIn<T>(this T left, IEnumerable<T> right) where T : IComparable
        {
            throw new NotSupportedException("Only for expressions.");
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