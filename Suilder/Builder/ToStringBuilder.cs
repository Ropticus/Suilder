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
        /// Writes an <see cref="IQueryFragment"/> to the builder.
        /// </summary>
        /// <param name="value">The <see cref="IQueryFragment"/> to write.</param>
        /// <returns>The string builder.</returns>
        public ToStringBuilder WriteFragment(IQueryFragment value)
        {
            return WriteFragment(value, value is ISubQuery);
        }

        /// <summary>
        /// Writes an <see cref="IQueryFragment"/> to the builder.
        /// </summary>
        /// <param name="value">The <see cref="IQueryFragment"/> to write.</param>
        /// <param name="parentheses">When to add parentheses to the <see cref="IQueryFragment"/>.</param>
        /// <returns>The string builder.</returns>
        public ToStringBuilder WriteFragment(IQueryFragment value, Parentheses parentheses)
        {
            switch (parentheses)
            {
                case Parentheses.Never:
                    return WriteFragment(value, false);
                case Parentheses.SubFragment:
                    return WriteFragment(value, value is ISubFragment);
                case Parentheses.SubQuery:
                    return WriteFragment(value, value is ISubQuery);
                case Parentheses.Always:
                    return WriteFragment(value, true);
                default:
                    throw new ArgumentException("Invalid value.", nameof(parentheses));
            }
        }

        /// <summary>
        /// Writes an <see cref="IQueryFragment"/> to the builder.
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
        /// Writes a value to the builder.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <returns>The string builder.</returns>
        public ToStringBuilder WriteValue(object value)
        {
            if (value is IQueryFragment queryFragment)
                return WriteFragment(queryFragment);
            else
                return WriteParameter(value);
        }

        /// <summary>
        /// Writes a value to the builder.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <param name="parentheses">When to add parentheses to the value.</param>
        /// <returns>The string builder.</returns>
        public ToStringBuilder WriteValue(object value, Parentheses parentheses)
        {
            if (value is IQueryFragment queryFragment)
                return WriteFragment(queryFragment, parentheses);
            else
                return WriteParameter(value, parentheses);
        }

        /// <summary>
        /// Writes a value to the builder.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <param name="addParentheses">If add parentheses to the value.</param>
        /// <returns>The string builder.</returns>
        public ToStringBuilder WriteValue(object value, bool addParentheses)
        {
            if (value is IQueryFragment queryFragment)
                return WriteFragment(queryFragment, addParentheses);
            else
                return WriteParameter(value, addParentheses);
        }

        /// <summary>
        /// Writes a parameter to the builder.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="parentheses">When to add parentheses to the parameter.</param>
        /// <returns>The string builder.</returns>
        public ToStringBuilder WriteParameter(object value, Parentheses parentheses)
        {
            switch (parentheses)
            {
                case Parentheses.Never:
                case Parentheses.SubFragment:
                case Parentheses.SubQuery:
                    return WriteParameter(value);
                case Parentheses.Always:
                    return WriteParameter(value, true);
                default:
                    throw new ArgumentException("Invalid value.", nameof(parentheses));
            }
        }

        /// <summary>
        /// Writes a parameter to the builder.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="addParentheses">If add parentheses to the parameter.</param>
        /// <returns>The string builder.</returns>
        public ToStringBuilder WriteParameter(object value, bool addParentheses)
        {
            if (addParentheses)
                Builder.Append("(");

            WriteParameter(value);

            if (addParentheses)
                Builder.Append(")");

            return this;
        }

        /// <summary>
        /// Writes a parameter to the builder.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <returns>The string builder.</returns>
        public ToStringBuilder WriteParameter(object value)
        {
            switch (value)
            {
                case string valueString:
                    Builder.Append("\"" + valueString + "\"");
                    break;
                case bool valueBool:
                    Builder.Append(valueBool ? "true" : "false");
                    break;
                case IEnumerable list:
                    Builder.Append("[");
                    IEnumerator enumerator = list.GetEnumerator();
                    if (enumerator.MoveNext())
                    {
                        int i = 1, max = 5;
                        string separator = ", ";
                        WriteParameter(enumerator.Current);

                        while (enumerator.MoveNext())
                        {
                            Builder.Append(separator);

                            if (i++ < max)
                            {
                                WriteParameter(enumerator.Current);
                            }
                            else
                            {
                                Builder.Append("...");
                                break;
                            }
                        }
                    }
                    Builder.Append("]");
                    break;
                case null:
                    Builder.Append("null");
                    break;
                default:
                    Builder.Append(value);
                    break;
            }

            return this;
        }

        /// <summary>
        /// Removes the last characters of the builder.
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
        /// Executes a function if the condition is <see langword="true"/>.
        /// </summary>
        /// <param name="condition">The condition value.</param>
        /// <param name="func">The function.</param>
        /// <returns>The string builder.</returns>
        public ToStringBuilder If(bool condition, Func<ToStringBuilder> func)
        {
            return condition ? func() : this;
        }

        /// <summary>
        /// Executes a function if the condition is <see langword="false"/>.
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
            using (IEnumerator<T> enumerator = values.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                    return this;

                func(enumerator.Current);

                while (enumerator.MoveNext())
                {
                    Builder.Append(separator);
                    func(enumerator.Current);
                }
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
        public ToStringBuilder Join<T>(string separator, IEnumerable<T> values, Func<T, int, ToStringBuilder> func)
        {
            int index = 0;
            using (IEnumerator<T> enumerator = values.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                    return this;

                func(enumerator.Current, index++);

                while (enumerator.MoveNext())
                {
                    Builder.Append(separator);
                    func(enumerator.Current, index++);
                }
            }

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