using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Suilder.Core;

namespace Suilder.Builder
{
    /// <summary>
    /// Builder for the <see cref="object.ToString"/> method of an <see cref="IQueryFragment"/>.
    /// </summary>
    public class ToStringBuilder
    {
        /// <summary>
        /// The string builder with the string result.
        /// </summary>
        /// <returns>The string builder with the string result.</returns>
        protected StringBuilder Builder { get; set; } = new StringBuilder();

        /// <summary>
        /// Initializes a new instance of the <see cref="ToStringBuilder"/> class.
        /// </summary>
        protected ToStringBuilder()
        {
        }

        /// <summary>
        /// Gets a builder instance and returns the result.
        /// </summary>
        /// <param name="func">Function to build the result.</param>
        /// <returns>The result value.</returns>
        public static string Build(Func<ToStringBuilder, ToStringBuilder> func)
        {
            return func(new ToStringBuilder()).ToString();
        }

        /// <summary>
        /// Writes the text to the builder.
        /// </summary>
        /// <param name="text">The text to write.</param>
        /// <returns>The string builder.</returns>
        public ToStringBuilder Write(string text)
        {
            Builder.Append(text);
            return this;
        }

        /// <summary>
        /// Writes a <see cref="IQueryFragment"/> to the builder.
        /// </summary>
        /// <param name="value">The <see cref="IQueryFragment"/> to write.</param>
        /// <returns>The string builder.</returns>
        public ToStringBuilder WriteFragment(IQueryFragment value)
        {
            return WriteFragment(value, value is ISubQuery);
        }

        /// <summary>
        /// Writes a <see cref="IQueryFragment"/> to the builder.
        /// </summary>
        /// <param name="value">The <see cref="IQueryFragment"/> to write.</param>
        /// <param name="addParentheses">If add parentheses to the <see cref="IQueryFragment"/>.</param>
        /// <returns>The string builder.</returns>
        public ToStringBuilder WriteFragment(IQueryFragment value, bool addParentheses)
        {
            if (addParentheses)
                Builder.Append("(");

            Builder.Append(value.ToString());

            if (addParentheses)
                Builder.Append(")");

            return this;
        }

        /// <summary>
        /// Writes an object to the query.
        /// </summary>
        /// <param name="value">The object to write.</param>
        /// <returns>The string builder.</returns>
        public ToStringBuilder WriteValue(object value)
        {
            IQueryFragment queryFragment = value as IQueryFragment;

            if (queryFragment != null)
                WriteFragment(queryFragment);
            else
                WriteParameter(value);
            return this;
        }

        /// <summary>
        /// Add a parameter to the query.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <returns>The string builder.</returns>
        public ToStringBuilder WriteParameter(object value)
        {
            if (!(value is String) && value is IEnumerable list)
            {
                Builder.Append("(");
                bool any = false;
                string separator = ", ";
                foreach (object item in list)
                {
                    if (item == null)
                        Builder.Append("NULL");
                    else
                        Builder.Append(item);

                    Builder.Append(separator);
                    any = true;
                }
                if (any)
                    RemoveLast(separator.Length);
                Builder.Append(")");
            }
            else if (value == null)
            {
                Builder.Append("NULL");
            }
            else if (value is String)
            {
                Builder.Append("\"" + value + "\"");
            }
            else if (value is bool valueBool)
            {
                Builder.Append(valueBool ? "true" : "false");
            }
            else
            {
                Builder.Append(value);
            }

            return this;
        }

        /// <summary>
        /// Removes the last characters of the query.
        /// </summary>
        /// <param name="length">The length to remove.</param>
        /// <returns>The string builder.</returns>
        public ToStringBuilder RemoveLast(int length)
        {
            Builder.Length -= length;
            return this;
        }

        /// <summary>
        /// Executes a function if the value is not null.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="func">The function.</param>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <returns>The string builder.</returns>
        public ToStringBuilder IfNotNull<T>(T value, Func<T, ToStringBuilder> func)
        {
            return value != null ? func(value) : this;
        }

        /// <summary>
        /// Executes a function if the value is not null.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="func">The function.</param>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <returns>The string builder.</returns>
        public ToStringBuilder IfNotNull<T>(T value, Func<ToStringBuilder> func)
        {
            return value != null ? func() : this;
        }

        /// <summary>
        /// Executes a function if the condition is true.
        /// </summary>
        /// <param name="condition">The condition value.</param>
        /// <param name="func">The function.</param>
        /// <returns>The string builder.</returns>
        public ToStringBuilder If(bool condition, Func<ToStringBuilder> func)
        {
            return condition ? func() : this;
        }

        /// <summary>
        /// Executes a function if the condition is false.
        /// </summary>
        /// <param name="condition">The condition value.</param>
        /// <param name="func">The function.</param>
        /// <returns>The string builder.</returns>
        public ToStringBuilder IfNot(bool condition, Func<ToStringBuilder> func)
        {
            return !condition ? func() : this;
        }

        /// <summary>
        /// Executes a function for each item of the <see cref="IEnumerable"/>.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <param name="func">The function.</param>
        /// <typeparam name="T">The type of the values.</typeparam>
        /// <returns>The string builder.</returns>
        public ToStringBuilder ForEach<T>(IEnumerable<T> values, Func<T, ToStringBuilder> func)
        {
            foreach (T value in values)
            {
                func(value);
            }
            return this;
        }

        /// <summary>
        /// Executes a function for each item of the <see cref="IEnumerable"/>.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <param name="func">The function.</param>
        /// <typeparam name="T">The type of the values.</typeparam>
        /// <returns>The string builder.</returns>
        public ToStringBuilder ForEach<T>(IEnumerable<T> values, Func<T, int, ToStringBuilder> func)
        {
            int index = 0;
            foreach (T value in values)
            {
                func(value, index++);
            }
            return this;
        }

        /// <summary>
        /// Concatenates the values using the specified separator.
        /// </summary>
        /// <param name="separator">The string to use as a separator.</param>
        /// <param name="values">The values.</param>
        /// <param name="func">The function.</param>
        /// <typeparam name="T">The type of the values.</typeparam>
        /// <returns>The string builder.</returns>
        public ToStringBuilder Join<T>(string separator, IEnumerable<T> values, Func<T, ToStringBuilder> func)
        {
            bool any = false;
            foreach (T value in values)
            {
                func(value);
                Builder.Append(separator);
                any = true;
            }
            if (any)
                RemoveLast(separator.Length);

            return this;
        }

        /// <summary>
        /// Concatenates the values using the specified separator.
        /// </summary>
        /// <param name="separator">The string to use as a separator.</param>
        /// <param name="values">The values.</param>
        /// <param name="func">The function.</param>
        /// <typeparam name="T">The type of the values.</typeparam>
        /// <returns>The string builder.</returns>
        public ToStringBuilder Join<T>(string separator, IEnumerable<T> values, Func<T, int, ToStringBuilder> func)
        {
            bool any = false;
            int index = 0;
            foreach (T value in values)
            {
                func(value, index++);
                Builder.Append(separator);
                any = true;
            }
            if (any)
                RemoveLast(separator.Length);

            return this;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return Builder.ToString();
        }
    }
}