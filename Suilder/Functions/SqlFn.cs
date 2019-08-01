using Suilder.Builder;
using Suilder.Core;

namespace Suilder.Functions
{
    /// <summary>
    /// Utility class to create common functions.
    /// </summary>
    public static class SqlFn
    {
        /// <summary>
        /// Returns the absolute value of a number.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The function.</returns>
        public static IFunction Abs(object value)
        {
            return SqlBuilder.Instance.Function(FunctionName.Abs).Add(value);
        }

        /// <summary>
        /// Returns the average of the values in a group.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The function.</returns>
        public static IFunction Avg(object value)
        {
            return SqlBuilder.Instance.Function(FunctionName.Avg).Add(value);
        }

        /// <summary>
        /// Returns the average of the distinct values in a group.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The function.</returns>
        public static IFunction AvgDistinct(object value)
        {
            return SqlBuilder.Instance.Function(FunctionName.Avg).Before(SqlBuilder.Instance.Raw("DISTINCT"))
                .Add(value);
        }

        /// <summary>
        /// Converts a value of one data type to another.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="type">The type to cast.</param>
        /// <returns>The function.</returns>
        public static IFunction Cast(object value, object type)
        {
            return SqlBuilder.Instance.Function(FunctionName.Cast).Add(value).Add(type);
        }

        /// <summary>
        /// Returns the smallest integer greater than or equal to the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The function.</returns>
        public static IFunction Ceiling(object value)
        {
            return SqlBuilder.Instance.Function(FunctionName.Ceiling).Add(value);
        }

        /// <summary>
        /// Returns the first non null value in a list.
        /// </summary>
        /// <param name="values"></param>
        /// <returns>The function.</returns>
        public static IFunction Coalesce(params object[] values)
        {
            return SqlBuilder.Instance.Function(FunctionName.Coalesce).Add(values);
        }

        /// <summary>
        /// Adds two or more strings together.
        /// </summary>
        /// <param name="values"></param>
        /// <returns>The function.</returns>
        public static IFunction Concat(params object[] values)
        {
            return SqlBuilder.Instance.Function(FunctionName.Concat).Add(values);
        }

        /// <summary>
        /// Returns the number of items found in a group.
        /// </summary>
        /// <returns>The function.</returns>
        public static IFunction Count()
        {
            return SqlBuilder.Instance.Function(FunctionName.Count).Add(SqlBuilder.Instance.Col("*"));
        }

        /// <summary>
        /// Returns the number of items found in a group.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The function.</returns>
        public static IFunction Count(object value)
        {
            return SqlBuilder.Instance.Function(FunctionName.Count).Add(value);
        }

        /// <summary>
        /// Returns the number of distinct items found in a group.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The function.</returns>
        public static IFunction CountDistinct(object value)
        {
            return SqlBuilder.Instance.Function(FunctionName.Count).Before(SqlBuilder.Instance.Raw("DISTINCT")).Add(value);
        }

        /// <summary>
        /// Returns the largest integer less than or equal to the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The function.</returns>
        public static IFunction Floor(object value)
        {
            return SqlBuilder.Instance.Function(FunctionName.Floor).Add(value);
        }

        /// <summary>
        /// Returns the last inserted id.
        /// </summary>
        /// <returns>The function.</returns>
        public static IFunction LastInsertId()
        {
            return SqlBuilder.Instance.Function(FunctionName.LastInsertId);
        }

        /// <summary>
        /// Returns the length of a string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The function.</returns>
        public static IFunction Length(object value)
        {
            return SqlBuilder.Instance.Function(FunctionName.Length).Add(value);
        }

        /// <summary>
        /// Converts a string to lowercase.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The function.</returns>
        public static IFunction Lower(object value)
        {
            return SqlBuilder.Instance.Function(FunctionName.Lower).Add(value);
        }

        /// <summary>
        /// Removes the spaces from the start of a string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The function.</returns>
        public static IFunction LTrim(object value)
        {
            return SqlBuilder.Instance.Function(FunctionName.LTrim).Add(value);
        }

        /// <summary>
        /// Removes the specified characters from the start of a string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="trimString">The characters to remove.</param>
        /// <returns>The function.</returns>
        public static IFunction LTrim(object value, object trimString)
        {
            return SqlBuilder.Instance.Function(FunctionName.LTrim).Add(value).Add(trimString);
        }

        /// <summary>
        /// Returns the maximum value in a group.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The function.</returns>
        public static IFunction Max(object value)
        {
            return SqlBuilder.Instance.Function(FunctionName.Max).Add(value);
        }

        /// <summary>
        /// Returns the minimum value in a group.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The function.</returns>
        public static IFunction Min(object value)
        {
            return SqlBuilder.Instance.Function(FunctionName.Min).Add(value);
        }

        /// <summary>
        /// Returns null if the two values equal, otherwise it returns the first value.
        /// </summary>
        /// <param name="value1">First value.</param>
        /// <param name="value2">Second value.</param>
        /// <returns>The function.</returns>
        public static IFunction NullIf(object value1, object value2)
        {
            return SqlBuilder.Instance.Function(FunctionName.NullIf).Add(value1).Add(value2);
        }

        /// <summary>
        /// Replaces all occurrences of a specified string value with another string value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="oldString">The string to search.</param>
        /// <param name="newString">The replacement string.</param>
        /// <returns>The function.</returns>
        public static IFunction Replace(object value, object oldString, object newString)
        {
            return SqlBuilder.Instance.Function(FunctionName.Replace).Add(value).Add(oldString).Add(newString);
        }

        /// <summary>
        /// Rounds a number to the nearest integer.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The function.</returns>
        public static IFunction Round(object value)
        {
            return SqlBuilder.Instance.Function(FunctionName.Round).Add(value);
        }

        /// <summary>
        /// Rounds a number to a specified number of decimal places.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="decimalPlaces">The number of decimal places.</param>
        /// <returns>The function.</returns>
        public static IFunction Round(object value, object decimalPlaces)
        {
            return SqlBuilder.Instance.Function(FunctionName.Round).Add(value).Add(decimalPlaces);
        }

        /// <summary>
        /// Removes the spaces from the end of a string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The function.</returns>
        public static IFunction RTrim(object value)
        {
            return SqlBuilder.Instance.Function(FunctionName.RTrim).Add(value);
        }

        /// <summary>
        /// Removes the specified characters from the end of a string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="trimString">The characters to remove.</param>
        /// <returns>The function.</returns>
        public static IFunction RTrim(object value, object trimString)
        {
            return SqlBuilder.Instance.Function(FunctionName.RTrim).Add(value).Add(trimString);
        }

        /// <summary>
        /// Extracts a substring from a string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="start">The start position, the first position is 1.</param>
        /// <param name="length">The number of characters to extract.</param>
        /// <returns>The function.</returns>
        public static IFunction Substring(object value, object start, object length)
        {
            return SqlBuilder.Instance.Function(FunctionName.Substring).Add(value).Add(start).Add(length);
        }

        /// <summary>
        /// Returns the sum of the values in a group.
        /// </summary>
        /// <param name="number"></param>
        /// <returns>The function.</returns>
        public static IFunction Sum(object number)
        {
            return SqlBuilder.Instance.Function(FunctionName.Sum).Add(number);
        }

        /// <summary>
        /// Returns the sum of the distinct values in a group.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The function.</returns>
        public static IFunction SumDistinct(object value)
        {
            return SqlBuilder.Instance.Function(FunctionName.Sum).Before(SqlBuilder.Instance.Raw("DISTINCT"))
                .Add(value);
        }

        /// <summary>
        /// Removes the spaces from the start and end of a string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The function.</returns>
        public static IFunction Trim(object value)
        {
            return SqlBuilder.Instance.Function(FunctionName.Trim).Add(value);
        }

        /// <summary>
        /// Removes the specified characters from the start and end of a string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="trimString">The characters to remove.</param>
        /// <returns>The function.</returns>
        public static IFunction Trim(object value, object trimString)
        {
            return SqlBuilder.Instance.Function(FunctionName.Trim).Add(value).Add(trimString);
        }

        /// <summary>
        /// Converts a string to uppercase.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The function.</returns>
        public static IFunction Upper(object value)
        {
            return SqlBuilder.Instance.Function(FunctionName.Upper).Add(value);
        }
    }
}