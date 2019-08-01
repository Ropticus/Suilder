using System;

namespace Suilder.Functions
{
    /// <summary>
    /// Utility class to create operators and common functions in expressions.
    /// </summary>
    public static partial class SqlExp
    {
        /// <summary>
        /// Returns the absolute value of a number.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <returns>The absolute value of a number.</returns>
        public static T Abs<T>(T value)
        {
            throw new InvalidOperationException("Only for expressions");
        }

        /// <summary>
        /// Returns the average of the values in a group.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <returns>The average of the values in a group.</returns>
        public static T Avg<T>(T value)
        {
            throw new InvalidOperationException("Only for expressions");
        }

        /// <summary>
        /// Returns the average of the distinct values in a group.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <returns>The average of the distinct values in a group</returns>
        public static T AvgDistinct<T>(T value)
        {
            throw new InvalidOperationException("Only for expressions");
        }

        /// <summary>
        /// Converts a value of one data type to another.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="type">The type to cast.</param>
        /// <returns>The value converted to the data type.</returns>
        public static object Cast(object value, object type)
        {
            throw new InvalidOperationException("Only for expressions");
        }

        /// <summary>
        /// Returns the smallest integer greater than or equal to the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <returns>The smallest integer greater than or equal to the specified value.</returns>
        public static T Ceiling<T>(T value)
        {
            throw new InvalidOperationException("Only for expressions");
        }

        /// <summary>
        /// Returns the first non null value in a list.
        /// </summary>
        /// <param name="values"></param>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <returns>The first non null value.</returns>
        public static T Coalesce<T>(params T[] values)
        {
            throw new InvalidOperationException("Only for expressions");
        }

        /// <summary>
        /// Returns the first non null value in a list.
        /// </summary>
        /// <param name="values"></param>
        /// <returns>The first non null value.</returns>
        public static object Coalesce(params object[] values)
        {
            throw new InvalidOperationException("Only for expressions");
        }

        /// <summary>
        /// Adds two or more strings together.
        /// </summary>
        /// <param name="values"></param>
        /// <returns>The concatenation of the values.</returns>
        public static string Concat(params object[] values)
        {
            throw new InvalidOperationException("Only for expressions");
        }

        /// <summary>
        /// Returns the number of items found in a group.
        /// </summary>
        /// <returns>The number of items found in a group.</returns>
        public static long Count()
        {
            throw new InvalidOperationException("Only for expressions");
        }

        /// <summary>
        /// Returns the number of items found in a group.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The number of items found in a group.</returns>
        public static long Count(object value)
        {
            throw new InvalidOperationException("Only for expressions");
        }

        /// <summary>
        /// Returns the number of distinct items found in a group.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The number of distinct items found in a group.</returns>
        public static long CountDistinct(object value)
        {
            throw new InvalidOperationException("Only for expressions");
        }

        /// <summary>
        /// Returns the largest integer less than or equal to the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <returns>The largest integer less than or equal to the specified value.</returns>
        public static T Floor<T>(T value)
        {
            throw new InvalidOperationException("Only for expressions");
        }

        /// <summary>
        /// Returns the last inserted id.
        /// </summary>
        /// <returns>The last inserted id.</returns>
        public static object LastInsertId()
        {
            throw new InvalidOperationException("Only for expressions");
        }

        /// <summary>
        /// Returns the length of a string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The length of a string.</returns>
        public static int Length(object value)
        {
            throw new InvalidOperationException("Only for expressions");
        }

        /// <summary>
        /// Converts a string to lowercase.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The lowercase string.</returns>
        public static string Lower(object value)
        {
            throw new InvalidOperationException("Only for expressions");
        }

        /// <summary>
        /// Removes the spaces from the start of a string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The string with the removed spaces from the start and end.</returns>
        public static string LTrim(object value)
        {
            throw new InvalidOperationException("Only for expressions");
        }

        /// <summary>
        /// Removes the specified characters from the start of a string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="trimString">The characters to remove.</param>
        /// <returns>The string with the removed characters from the start.</returns>
        public static string LTrim(object value, object trimString)
        {
            throw new InvalidOperationException("Only for expressions");
        }

        /// <summary>
        /// Returns the maximum value in a group.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <returns>The maximum value of the group.</returns>
        public static T Max<T>(T value)
        {
            throw new InvalidOperationException("Only for expressions");
        }

        /// <summary>
        /// Returns the minimum value in a group.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <returns>The minimum value of the group.</returns>
        public static T Min<T>(T value)
        {
            throw new InvalidOperationException("Only for expressions");
        }

        /// <summary>
        /// Returns null if the two values equal, otherwise it returns the first value.
        /// </summary>
        /// <param name="value1">First value.</param>
        /// <param name="value2">Second value.</param>
        /// <returns>Null if the two values equal, otherwise the first value.</returns>
        public static object NullIf(object value1, object value2)
        {
            throw new InvalidOperationException("Only for expressions");
        }

        /// <summary>
        /// Replaces all occurrences of a specified string value with another string value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="oldString">The string to search.</param>
        /// <param name="newString">The replacement string.</param>
        /// <returns>The string with all occurrences replaced.</returns>
        public static string Replace(object value, object oldString, object newString)
        {
            throw new InvalidOperationException("Only for expressions");
        }

        /// <summary>
        /// Rounds a number to the nearest integer.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <returns>The number rounded to the nearest integer.</returns>
        public static T Round<T>(T value)
        {
            throw new InvalidOperationException("Only for expressions");
        }

        /// <summary>
        /// Rounds a number to a specified number of decimal places.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="decimalPlaces">The number of decimal places.</param>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <returns>The number rounded to the specified number of decimal places.</returns>
        public static T Round<T>(T value, object decimalPlaces)
        {
            throw new InvalidOperationException("Only for expressions");
        }

        /// <summary>
        /// Removes the spaces from the end of a string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The string with the removed spaces from the end.</returns>
        public static string RTrim(object value)
        {
            throw new InvalidOperationException("Only for expressions");
        }

        /// <summary>
        /// Removes the specified characters from the end of a string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="trimString">The characters to remove.</param>
        /// <returns>The string with the removed characters from the end.</returns>
        public static string RTrim(object value, object trimString)
        {
            throw new InvalidOperationException("Only for expressions");
        }

        /// <summary>
        /// Extracts a substring from a string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="start">The start position, the first position is 1.</param>
        /// <param name="length">The number of characters to extract.</param>
        /// <returns>The extracted string.</returns>
        public static string Substring(object value, object start, object length)
        {
            throw new InvalidOperationException("Only for expressions");
        }

        /// <summary>
        /// Returns the sum of the values in a group.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <returns>The sum of the values.</returns>
        public static T Sum<T>(T value)
        {
            throw new InvalidOperationException("Only for expressions");
        }

        /// <summary>
        /// Returns the sum of the distinct values in a group.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <returns>The sum of the distinct values.</returns>
        public static T SumDistinct<T>(T value)
        {
            throw new InvalidOperationException("Only for expressions");
        }

        /// <summary>
        /// Removes the spaces from the start and end of a string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The string with the removed spaces from the start and end.</returns>
        public static string Trim(object value)
        {
            throw new InvalidOperationException("Only for expressions");
        }

        /// <summary>
        /// Removes the specified characters from the start and end of a string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="trimString">The characters to remove.</param>
        /// <returns>The string with the removed characters from the start and end.</returns>
        public static string Trim(object value, object trimString)
        {
            throw new InvalidOperationException("Only for expressions");
        }

        /// <summary>
        /// Converts a string to uppercase.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The uppercase string.</returns>
        public static string Upper(object value)
        {
            throw new InvalidOperationException("Only for expressions");
        }
    }
}