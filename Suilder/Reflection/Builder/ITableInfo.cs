using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Suilder.Exceptions;

namespace Suilder.Reflection.Builder
{
    /// <summary>
    /// Contains the information of a table.
    /// </summary>
    public interface ITableInfo
    {
        /// <summary>
        /// The type of the table.
        /// </summary>
        /// <value>The type of the table.</value>
        Type Type { get; }

        /// <summary>
        /// The table name.
        /// </summary>
        /// <value>The table name.</value>
        string TableName { get; }

        /// <summary>
        /// The primary key properties.
        /// </summary>
        /// <value>The primary key properties.</value>
        IList<string> PrimaryKeys { get; }

        /// <summary>
        /// The column properties.
        /// </summary>
        /// <value>The column properties.</value>
        IList<string> Columns { get; }

        /// <summary>
        /// The list of column names.
        /// </summary>
        /// <value>The list of column names.</value>
        IList<string> ColumnNames { get; }

        /// <summary>
        /// The column names of the properties.
        /// <para>The key is the column property, the value is the column name.</para>
        /// </summary>
        /// <value>The column names of the properties.</value>
        IDictionary<string, string> ColumnNamesDic { get; }

        /// <summary>
        /// The foreign keys properties.
        /// </summary>
        /// <value>The foreign keys properties.</value>
        IList<string> ForeignKeys { get; }

        /// <summary>
        /// The metadata of the table.
        /// </summary>
        /// <value>The metadata of the table.</value>
        IDictionary<string, object> TableMetadata { get; }

        /// <summary>
        /// The metadata of the members.
        /// <para>The key is the member name, the value is the metadata.</para>
        /// </summary>
        /// <value>The metadata of the members.</value>
        IDictionary<string, IDictionary<string, object>> MemberMetadata { get; }

        /// <summary>
        /// Gets the column name of the property.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <returns>The column name.</returns>
        /// <exception cref="InvalidConfigurationException">The property is not registered.</exception>
        string GetColumnName(string propertyName);

        /// <summary>
        /// Gets the metadata of the table associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value with the specified key.</returns>
        object GetTableMetadata(string key);

        /// <summary>
        /// Gets the metadata of the table associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the value to get.</param>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <returns>The value with the specified key.</returns>
        TValue GetTableMetadata<TValue>(string key);

        /// <summary>
        /// Gets the metadata of the table associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <returns>The value with the specified key.</returns>
        TValue GetTableMetadata<TValue>(string key, TValue defaultValue);

        /// <summary>
        /// Gets the metadata of the member.
        /// </summary>
        /// <param name="memberName">The member name.</param>
        /// <returns>The metadata of the member.</returns>
        IDictionary<string, object> GetMetadata(string memberName);

        /// <summary>
        /// Gets the metadata of the member associated with the specified key.
        /// </summary>
        /// <param name="memberName">The member name.</param>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value with the specified key.</returns>
        object GetMetadata(string memberName, string key);

        /// <summary>
        /// Gets the metadata of the member associated with the specified key.
        /// </summary>
        /// <param name="memberName">The member name.</param>
        /// <param name="key">The key of the value to get.</param>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <returns>The value with the specified key.</returns>
        TValue GetMetadata<TValue>(string memberName, string key);

        /// <summary>
        /// Gets the metadata of the member associated with the specified key.
        /// </summary>
        /// <param name="memberName">The member name.</param>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <returns>The value with the specified key.</returns>
        TValue GetMetadata<TValue>(string memberName, string key, TValue defaultValue);
    }

    /// <summary>
    /// Contains the information of a table.
    /// </summary>
    /// <typeparam name="T">The type of the table.</typeparam>
    public interface ITableInfo<T> : ITableInfo
    {
        /// <summary>
        /// Gets the column name of the property.
        /// </summary>
        /// <param name="expression">The property.</param>
        /// <returns>The column name.</returns>
        /// <exception cref="InvalidConfigurationException">The type or property is not registered.</exception>
        string GetColumnName(Expression<Func<T, object>> expression);

        /// <summary>
        /// Gets the metadata of the member.
        /// </summary>
        /// <param name="expression">The property.</param>
        /// <returns>The metadata of the member.</returns>
        IDictionary<string, object> GetMetadata(Expression<Func<T, object>> expression);

        /// <summary>
        /// Gets the metadata of the member associated with the specified key.
        /// </summary>
        /// <param name="expression">The property.</param>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value with the specified key.</returns>
        object GetMetadata(Expression<Func<T, object>> expression, string key);

        /// <summary>
        /// Gets the metadata of the member associated with the specified key.
        /// </summary>
        /// <param name="expression">The property.</param>
        /// <param name="key">The key of the value to get.</param>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <returns>The value with the specified key.</returns>
        TValue GetMetadata<TValue>(Expression<Func<T, object>> expression, string key);

        /// <summary>
        /// Gets the metadata of the member associated with the specified key.
        /// </summary>
        /// <param name="expression">The property.</param>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <returns>The value with the specified key.</returns>
        TValue GetMetadata<TValue>(Expression<Func<T, object>> expression, string key, TValue defaultValue);
    }
}