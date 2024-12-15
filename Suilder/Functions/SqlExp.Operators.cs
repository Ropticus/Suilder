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
        /// <returns><see langword="true"/> if <paramref name="left"/> and <paramref name="right"/> are equal, otherwise,
        /// <see langword="false"/>.</returns>
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
        /// <returns><see langword="true"/> if <paramref name="left"/> and <paramref name="right"/> are not equal,
        /// otherwise, <see langword="false"/>.</returns>
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
        /// <returns><see langword="true"/> if <paramref name="left"/> matches <paramref name="right"/>, otherwise,
        /// <see langword="false"/>.</returns>
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
        /// <returns><see langword="true"/> if <paramref name="left"/> does not match <paramref name="right"/>, otherwise,
        /// <see langword="false"/>.</returns>
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
        /// <returns><see langword="true"/> if <paramref name="left"/> is less than <paramref name="right"/>, otherwise,
        /// <see langword="false"/>.</returns>
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
        /// <returns><see langword="true"/> if <paramref name="left"/> is less than or equal to <paramref name="right"/>,
        /// otherwise, <see langword="false"/>.</returns>
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
        /// <returns><see langword="true"/> if <paramref name="left"/> is greater than <paramref name="right"/>, otherwise,
        /// <see langword="false"/>.</returns>
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
        /// <returns><see langword="true"/> if <paramref name="left"/> is greater than or equal to <paramref name="right"/>,
        /// otherwise, <see langword="false"/>.</returns>
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
        /// <returns><see langword="true"/> if <paramref name="left"/> is equal to any value of <paramref name="right"/>,
        /// otherwise, <see langword="false"/>.</returns>
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
        /// <returns><see langword="true"/> if <paramref name="left"/> is not equal to any value of <paramref name="right"/>,
        /// otherwise, <see langword="false"/>.</returns>
        /// <exception cref="NotSupportedException">The method is called outside an expression.</exception>
        public static bool NotIn(object left, object right)
        {
            throw new NotSupportedException("Only for expressions.");
        }

        /// <summary>
        /// Creates a "not" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><see langword="true"/> if <paramref name="value"/> evaluates to <see langword="false"/>, otherwise,
        /// <see langword="false"/>.</returns>
        /// <exception cref="NotSupportedException">The method is called outside an expression.</exception>
        public static bool Not(object value)
        {
            throw new NotSupportedException("Only for expressions.");
        }

        /// <summary>
        /// Creates an "is null" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><see langword="true"/> if <paramref name="value"/> is <see langword="null"/>, otherwise,
        /// <see langword="false"/>.</returns>
        /// <exception cref="NotSupportedException">The method is called outside an expression.</exception>
        public static bool IsNull(object value)
        {
            throw new NotSupportedException("Only for expressions.");
        }

        /// <summary>
        /// Creates an "is not null" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><see langword="true"/> if <paramref name="value"/> is not <see langword="null"/>, otherwise,
        /// <see langword="false"/>.</returns>
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
        /// <returns><see langword="true"/> if <paramref name="left"/> is greater than or equal to <paramref name="min"/>
        /// and less than or equal to <paramref name="max"/>, otherwise, <see langword="false"/>.</returns>
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
        /// <returns><see langword="true"/> if <paramref name="left"/> is less than <paramref name="min"/> or greater than
        /// <paramref name="max"/>, otherwise, <see langword="false"/>.</returns>
        /// <exception cref="NotSupportedException">The method is called outside an expression.</exception>
        public static bool NotBetween(object left, object min, object max)
        {
            throw new NotSupportedException("Only for expressions.");
        }

        /// <summary>
        /// Creates an "all" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><see langword="true"/> if all of the values of the rows of <paramref name="value"/> meet the condition,
        /// otherwise, <see langword="false"/>.</returns>
        /// <exception cref="NotSupportedException">The method is called outside an expression.</exception>
        public static bool All(IQueryFragment value)
        {
            throw new NotSupportedException("Only for expressions.");
        }

        /// <summary>
        /// Creates an "all" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <returns><see langword="true"/> if all of the values of the rows of <paramref name="value"/> meet the condition,
        /// otherwise, <see langword="false"/>.</returns>
        /// <exception cref="NotSupportedException">The method is called outside an expression.</exception>
        public static T All<T>(IQueryFragment value)
        {
            throw new NotSupportedException("Only for expressions.");
        }

        /// <summary>
        /// Creates an "any" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><see langword="true"/> if any of the values of the rows of <paramref name="value"/> meet the condition,
        /// otherwise, <see langword="false"/>.</returns>
        /// <exception cref="NotSupportedException">The method is called outside an expression.</exception>
        public static bool Any(IQueryFragment value)
        {
            throw new NotSupportedException("Only for expressions.");
        }

        /// <summary>
        /// Creates an "any" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <returns><see langword="true"/> if any of the values of the rows of <paramref name="value"/> meet the condition,
        /// otherwise, <see langword="false"/>.</returns>
        /// <exception cref="NotSupportedException">The method is called outside an expression.</exception>
        public static T Any<T>(IQueryFragment value)
        {
            throw new NotSupportedException("Only for expressions.");
        }

        /// <summary>
        /// Creates an "exists" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><see langword="true"/> if <paramref name="value"/> contains any rows, otherwise,
        /// <see langword="false"/>.</returns>
        /// <exception cref="NotSupportedException">The method is called outside an expression.</exception>
        public static bool Exists(IQueryFragment value)
        {
            throw new NotSupportedException("Only for expressions.");
        }

        /// <summary>
        /// Creates a "some" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><see langword="true"/> if any of the values of the rows of <paramref name="value"/> meet the condition,
        /// otherwise, <see langword="false"/>.</returns>
        /// <exception cref="NotSupportedException">The method is called outside an expression.</exception>
        public static bool Some(IQueryFragment value)
        {
            throw new NotSupportedException("Only for expressions.");
        }

        /// <summary>
        /// Creates a "some" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <returns><see langword="true"/> if any of the values of the rows of <paramref name="value"/> meet the condition,
        /// otherwise, <see langword="false"/>.</returns>
        /// <exception cref="NotSupportedException">The method is called outside an expression.</exception>
        public static T Some<T>(IQueryFragment value)
        {
            throw new NotSupportedException("Only for expressions.");
        }

        /// <summary>
        /// Creates a function.
        /// </summary>
        /// <param name="name">The name of the function.</param>
        /// <returns>The result of the function.</returns>
        /// <exception cref="NotSupportedException">The method is called outside an expression.</exception>
        public static object Function(string name)
        {
            throw new NotSupportedException("Only for expressions.");
        }

        /// <summary>
        /// Creates a function.
        /// </summary>
        /// <param name="name">The name of the function.</param>
        /// <typeparam name="T">The type of the return value.</typeparam>
        /// <returns>The result of the function.</returns>
        /// <exception cref="NotSupportedException">The method is called outside an expression.</exception>
        public static T Function<T>(string name)
        {
            throw new NotSupportedException("Only for expressions.");
        }

        /// <summary>
        /// Creates a function.
        /// </summary>
        /// <param name="name">The name of the function.</param>
        /// <param name="values">The arguments of the function.</param>
        /// <returns>The result of the function.</returns>
        /// <exception cref="NotSupportedException">The method is called outside an expression.</exception>
        public static object Function(string name, params object[] values)
        {
            throw new NotSupportedException("Only for expressions.");
        }

        /// <summary>
        /// Creates a function.
        /// </summary>
        /// <param name="name">The name of the function.</param>
        /// <param name="values">The arguments of the function.</param>
        /// <typeparam name="T">The type of the return value.</typeparam>
        /// <returns>The result of the function.</returns>
        /// <exception cref="NotSupportedException">The method is called outside an expression.</exception>
        public static T Function<T>(string name, params object[] values)
        {
            throw new NotSupportedException("Only for expressions.");
        }

        /// <summary>
        /// Creates a column with an alias.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <param name="columnName">The column name.</param>
        /// <returns>The column.</returns>
        /// <exception cref="NotSupportedException">The method is called outside an expression.</exception>
        public static object Col(object alias, string columnName)
        {
            throw new NotSupportedException("Only for expressions.");
        }

        /// <summary>
        /// Creates a column with an alias.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <param name="columnName">The column name.</param>
        /// <typeparam name="T">The type of the column.</typeparam>
        /// <returns>The column.</returns>
        /// <exception cref="NotSupportedException">The method is called outside an expression.</exception>
        public static T Col<T>(object alias, string columnName)
        {
            throw new NotSupportedException("Only for expressions.");
        }

        /// <summary>
        /// Creates a column without the table name or alias.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <returns>The column without the table name or alias.</returns>
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
        /// <returns>The value.</returns>
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
        /// <returns>The value.</returns>
        /// <exception cref="NotSupportedException">The method is called outside an expression.</exception>
        public static T Val<T>(T value)
        {
            throw new NotSupportedException("Only for expressions.");
        }
    }
}