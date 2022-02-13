using System;
using Suilder.Core;

namespace Suilder.Functions
{
    public static partial class SqlExp
    {
        /// <summary>
        /// Creates an "equal to" operator.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <exception cref="NotSupportedException">The method is called outside an expression.</exception>
        public static bool Eq(object left, object right)
        {
            throw new NotSupportedException("Only for expressions.");
        }

        /// <summary>
        /// Creates a "not equal to" operator.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <exception cref="NotSupportedException">The method is called outside an expression.</exception>
        public static bool NotEq(object left, object right)
        {
            throw new NotSupportedException("Only for expressions.");
        }

        /// <summary>
        /// Creates a "like" operator.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <exception cref="NotSupportedException">The method is called outside an expression.</exception>
        public static bool Like(object left, object right)
        {
            throw new NotSupportedException("Only for expressions.");
        }

        /// <summary>
        /// Creates a "not like" operator.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <exception cref="NotSupportedException">The method is called outside an expression.</exception>
        public static bool NotLike(object left, object right)
        {
            throw new NotSupportedException("Only for expressions.");
        }

        /// <summary>
        /// Creates a "less than" operator.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <exception cref="NotSupportedException">The method is called outside an expression.</exception>
        public static bool Lt(object left, object right)
        {
            throw new NotSupportedException("Only for expressions.");
        }

        /// <summary>
        /// Creates a "less than or equal to" operator.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <exception cref="NotSupportedException">The method is called outside an expression.</exception>
        public static bool Le(object left, object right)
        {
            throw new NotSupportedException("Only for expressions.");
        }

        /// <summary>
        /// Creates a "greater than" operator.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <exception cref="NotSupportedException">The method is called outside an expression.</exception>
        public static bool Gt(object left, object right)
        {
            throw new NotSupportedException("Only for expressions.");
        }

        /// <summary>
        /// Creates a "greater than or equal to" operator.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <exception cref="NotSupportedException">The method is called outside an expression.</exception>
        public static bool Ge(object left, object right)
        {
            throw new NotSupportedException("Only for expressions.");
        }

        /// <summary>
        /// Creates an "in" operator.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <exception cref="NotSupportedException">The method is called outside an expression.</exception>
        public static bool In(object left, object right)
        {
            throw new NotSupportedException("Only for expressions.");
        }

        /// <summary>
        /// Creates a "not in" operator.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <exception cref="NotSupportedException">The method is called outside an expression.</exception>
        public static bool NotIn(object left, object right)
        {
            throw new NotSupportedException("Only for expressions.");
        }

        /// <summary>
        /// Creates a "not" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <exception cref="NotSupportedException">The method is called outside an expression.</exception>
        public static bool Not(object value)
        {
            throw new NotSupportedException("Only for expressions.");
        }

        /// <summary>
        /// Creates an "is null" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <exception cref="NotSupportedException">The method is called outside an expression.</exception>
        public static bool IsNull(object value)
        {
            throw new NotSupportedException("Only for expressions.");
        }

        /// <summary>
        /// Creates an "is not null" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <exception cref="NotSupportedException">The method is called outside an expression.</exception>
        public static bool IsNotNull(object value)
        {
            throw new NotSupportedException("Only for expressions.");
        }

        /// <summary>
        /// Creates a "between" operator.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="min">The min value.</param>
        /// <param name="max">The max value.</param>
        /// <exception cref="NotSupportedException">The method is called outside an expression.</exception>
        public static bool Between(object left, object min, object max)
        {
            throw new NotSupportedException("Only for expressions.");
        }

        /// <summary>
        /// Creates a "not between" operator.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="min">The min value.</param>
        /// <param name="max">The max value.</param>
        /// <exception cref="NotSupportedException">The method is called outside an expression.</exception>
        public static bool NotBetween(object left, object min, object max)
        {
            throw new NotSupportedException("Only for expressions.");
        }

        /// <summary>
        /// Creates an "all" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <exception cref="NotSupportedException">The method is called outside an expression.</exception>
        public static bool All(IQueryFragment value)
        {
            throw new NotSupportedException("Only for expressions.");
        }

        /// <summary>
        /// Creates an "any" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <exception cref="NotSupportedException">The method is called outside an expression.</exception>
        public static bool Any(IQueryFragment value)
        {
            throw new NotSupportedException("Only for expressions.");
        }

        /// <summary>
        /// Creates an "exists" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <exception cref="NotSupportedException">The method is called outside an expression.</exception>
        public static bool Exists(IQueryFragment value)
        {
            throw new NotSupportedException("Only for expressions.");
        }

        /// <summary>
        /// Creates a "some" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <exception cref="NotSupportedException">The method is called outside an expression.</exception>
        public static bool Some(IQueryFragment value)
        {
            throw new NotSupportedException("Only for expressions.");
        }

        /// <summary>
        /// Creates a function.
        /// </summary>
        /// <param name="name">The name of the function.</param>
        /// <exception cref="NotSupportedException">The method is called outside an expression.</exception>
        public static object Function(string name)
        {
            throw new NotSupportedException("Only for expressions.");
        }

        /// <summary>
        /// Creates a function.
        /// </summary>
        /// <param name="name">The name of the function.</param>
        /// <param name="values">The arguments of the function.</param>
        /// <exception cref="NotSupportedException">The method is called outside an expression.</exception>
        public static object Function(string name, params object[] values)
        {
            throw new NotSupportedException("Only for expressions.");
        }

        /// <summary>
        /// Creates a column without the table name or alias.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <exception cref="NotSupportedException">The method is called outside an expression.</exception>
        public static T ColName<T>(T value)
        {
            throw new NotSupportedException("Only for expressions.");
        }

        /// <summary>
        /// Changes the type of a value.
        /// <para>The type is only changed within the expression, but does not alter the original value.</para>
        /// </summary>
        /// <param name="value">The value.</param>
        /// <typeparam name="T">The new type of the value.</typeparam>
        /// <exception cref="NotSupportedException">The method is called outside an expression.</exception>
        /// <seealso cref="Cast{T}"/>
        public static T As<T>(object value)
        {
            throw new NotSupportedException("Only for expressions.");
        }

        /// <summary>
        /// Returns the passed value.
        /// <para>Prevents the value from being compiled into an <see cref="IQueryFragment"/> even if it is an alias
        /// or a registered function.</para>
        /// </summary>
        /// <param name="value">The value.</param>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <exception cref="NotSupportedException">The method is called outside an expression.</exception>
        public static T Val<T>(T value)
        {
            throw new NotSupportedException("Only for expressions.");
        }
    }
}