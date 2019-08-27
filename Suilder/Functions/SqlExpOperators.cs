using System;
using Suilder.Core;

namespace Suilder.Functions
{
    public static partial class SqlExp
    {
        /// <summary>
        /// Creates an "equal to" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <exception cref="InvalidOperationException">The method is called outside an expression.</exception>
        public static bool Eq(object left, object right)
        {
            throw new InvalidOperationException("Only for expressions.");
        }

        /// <summary>
        /// Creates a "not equal to" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <exception cref="InvalidOperationException">The method is called outside an expression.</exception>
        public static bool NotEq(object left, object right)
        {
            throw new InvalidOperationException("Only for expressions.");
        }

        /// <summary>
        /// Creates a "like" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <exception cref="InvalidOperationException">The method is called outside an expression.</exception>
        public static bool Like(object left, object right)
        {
            throw new InvalidOperationException("Only for expressions.");
        }

        /// <summary>
        /// Creates a "not like" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <exception cref="InvalidOperationException">The method is called outside an expression.</exception>
        public static bool NotLike(object left, object right)
        {
            throw new InvalidOperationException("Only for expressions.");
        }

        /// <summary>
        /// Creates a "less than" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <exception cref="InvalidOperationException">The method is called outside an expression.</exception>
        public static bool Lt(object left, object right)
        {
            throw new InvalidOperationException("Only for expressions.");
        }

        /// <summary>
        /// Creates a "less than or equal to" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <exception cref="InvalidOperationException">The method is called outside an expression.</exception>
        public static bool Le(object left, object right)
        {
            throw new InvalidOperationException("Only for expressions.");
        }

        /// <summary>
        /// Creates a "greater than" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <exception cref="InvalidOperationException">The method is called outside an expression.</exception>
        public static bool Gt(object left, object right)
        {
            throw new InvalidOperationException("Only for expressions.");
        }

        /// <summary>
        /// Creates a "greater than or equal to" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <exception cref="InvalidOperationException">The method is called outside an expression.</exception>
        public static bool Ge(object left, object right)
        {
            throw new InvalidOperationException("Only for expressions.");
        }

        /// <summary>
        /// Creates an "in" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <exception cref="InvalidOperationException">The method is called outside an expression.</exception>
        public static bool In(object left, object right)
        {
            throw new InvalidOperationException("Only for expressions.");
        }

        /// <summary>
        /// Creates a "not in" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <exception cref="InvalidOperationException">The method is called outside an expression.</exception>
        public static bool NotIn(object left, object right)
        {
            throw new InvalidOperationException("Only for expressions.");
        }

        /// <summary>
        /// Creates a "not" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <exception cref="InvalidOperationException">The method is called outside an expression.</exception>
        public static bool Not(object value)
        {
            throw new InvalidOperationException("Only for expressions.");
        }

        /// <summary>
        /// Creates an "is null" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <exception cref="InvalidOperationException">The method is called outside an expression.</exception>
        public static bool IsNull(object value)
        {
            throw new InvalidOperationException("Only for expressions.");
        }

        /// <summary>
        /// Creates an "is not null" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <exception cref="InvalidOperationException">The method is called outside an expression.</exception>
        public static bool IsNotNull(object value)
        {
            throw new InvalidOperationException("Only for expressions.");
        }

        /// <summary>
        /// Creates an "all" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <exception cref="InvalidOperationException">The method is called outside an expression.</exception>
        public static bool All(object value)
        {
            throw new InvalidOperationException("Only for expressions.");
        }

        /// <summary>
        /// Creates an "any" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <exception cref="InvalidOperationException">The method is called outside an expression.</exception>
        public static bool Any(object value)
        {
            throw new InvalidOperationException("Only for expressions.");
        }

        /// <summary>
        /// Creates an "exists" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <exception cref="InvalidOperationException">The method is called outside an expression.</exception>
        public static bool Exists(object value)
        {
            throw new InvalidOperationException("Only for expressions.");
        }

        /// <summary>
        /// Creates a "some" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <exception cref="InvalidOperationException">The method is called outside an expression.</exception>
        public static bool Some(object value)
        {
            throw new InvalidOperationException("Only for expressions.");
        }

        /// <summary>
        /// Creates a function.
        /// </summary>
        /// <param name="name">The name of the function.</param>
        /// <exception cref="InvalidOperationException">The method is called outside an expression.</exception>
        public static object Function(string name)
        {
            throw new InvalidOperationException("Only for expressions.");
        }

        /// <summary>
        /// Creates a function.
        /// </summary>
        /// <param name="name">The name of the function.</param>
        /// <param name="values">The arguments of the function.</param>
        /// <exception cref="InvalidOperationException">The method is called outside an expression.</exception>
        public static object Function(string name, params object[] values)
        {
            throw new InvalidOperationException("Only for expressions.");
        }

        /// <summary>
        /// Returns the passed value.
        /// <para>It prevents the value from being compiled into an <see cref="IQueryFragment"/> even if is an alias
        /// or a registered function.</para>
        /// </summary>
        /// <param name="value">The value.</param>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <exception cref="InvalidOperationException">The method is called outside an expression.</exception>
        public static T Val<T>(T value)
        {
            throw new InvalidOperationException("Only for expressions.");
        }
    }
}