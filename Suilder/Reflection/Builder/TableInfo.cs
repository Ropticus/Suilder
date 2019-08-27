using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using Suilder.Builder;
using Suilder.Exceptions;

namespace Suilder.Reflection.Builder
{
    /// <summary>
    /// Contains the information of a table.
    /// </summary>
    public class TableInfo : ITableInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TableInfo{T}"/> class.
        /// </summary>
        public TableInfo(Type type)
        {
            Type = type;
        }

        /// <summary>
        /// The type of the table.
        /// </summary>
        /// <value>The type of the table.</value>
        public Type Type { get; private set; }

        /// <summary>
        /// The table name.
        /// </summary>
        /// <value>The table name.</value>
        public string TableName { get; set; }

        /// <summary>
        /// The primary key properties.
        /// </summary>
        /// <value>The primary key properties.</value>
        public IList<string> PrimaryKeys { get; set; } = new List<string>();

        /// <summary>
        /// The column properties.
        /// </summary>
        /// <value>The column properties.</value>
        public IList<string> Columns { get; set; } = new List<string>();

        /// <summary>
        /// The list of column names.
        /// </summary>
        /// <value>The list of column names.</value>
        public IList<string> ColumnNames { get; set; } = new List<string>();

        /// <summary>
        /// The column names of the properties.
        /// <para>The key is the column property, the value is the column name.</para>
        /// </summary>
        /// <value>The column names of the properties.</value>
        public IDictionary<string, string> ColumnNamesDic { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// The foreign keys properties.
        /// </summary>
        /// <value>The foreign keys properties.</value>
        public IList<string> ForeignKeys { get; set; } = new List<string>();

        /// <summary>
        /// The metadata of the table.
        /// </summary>
        /// <value>The metadata of the table.</value>
        public IDictionary<string, object> TableMetadata { get; set; } = new Dictionary<string, object>();

        /// <summary>
        /// The metadata of the members.
        /// <para>The key is the member name, the value is the metadata.</para>
        /// </summary>
        /// <value>The metadata of the members.</value>
        public IDictionary<string, IDictionary<string, object>> MemberMetadata { get; set; }
            = new Dictionary<string, IDictionary<string, object>>();

        /// <summary>
        /// Cached empty metadata.
        /// </summary>
        /// <value>Cached empty metadata.</value>
        protected static IDictionary<string, object> MetadataEmpty { get; set; }
            = new ReadOnlyDictionary<string, object>(new Dictionary<string, object>());

        /// <summary>
        /// Gets the column name of the property.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <returns>The column name.</returns>
        /// <exception cref="InvalidConfigurationException">The property is not registered.</exception>
        public virtual string GetColumnName(string propertyName)
        {
            if (ColumnNamesDic.TryGetValue(propertyName, out string columnName))
                return columnName;

            throw new InvalidConfigurationException(
                $"The property \"{propertyName}\" for type \"{Type}\" is not registered.");
        }

        /// <summary>
        /// Gets the metadata of the table associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value with the specified key.</returns>
        public virtual object GetTableMetadata(string key)
        {
            TableMetadata.TryGetValue(key, out object value);
            return value;
        }

        /// <summary>
        /// Gets the metadata of the table associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the value to get.</param>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <returns>The value with the specified key.</returns>
        public TValue GetTableMetadata<TValue>(string key)
        {
            object value = GetTableMetadata(key);
            return value == null ? default(TValue) : (TValue)value;
        }

        /// <summary>
        /// Gets the metadata of the table associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <returns>The value with the specified key.</returns>
        public TValue GetTableMetadata<TValue>(string key, TValue defaultValue)
        {
            object value = GetTableMetadata(key);
            return value == null ? defaultValue : (TValue)value;
        }

        /// <summary>
        /// Gets the metadata of the member.
        /// </summary>
        /// <param name="memberName">The member name.</param>
        /// <returns>The metadata of the member.</returns>
        public virtual IDictionary<string, object> GetMetadata(string memberName)
        {
            if (MemberMetadata.TryGetValue(memberName, out var memberMetadata))
                return memberMetadata;

            return MetadataEmpty;
        }

        /// <summary>
        /// Gets the metadata of the member associated with the specified key.
        /// </summary>
        /// <param name="memberName">The member name.</param>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value with the specified key.</returns>
        public virtual object GetMetadata(string memberName, string key)
        {
            if (MemberMetadata.TryGetValue(memberName, out var memberMetadata))
            {
                memberMetadata.TryGetValue(key, out object value);
                return value;
            }

            return null;
        }

        /// <summary>
        /// Gets the metadata of the member associated with the specified key.
        /// </summary>
        /// <param name="memberName">The member name.</param>
        /// <param name="key">The key of the value to get.</param>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <returns>The value with the specified key.</returns>
        public virtual TValue GetMetadata<TValue>(string memberName, string key)
        {
            object value = GetMetadata(memberName, key);
            return value == null ? default(TValue) : (TValue)value;
        }

        /// <summary>
        /// Gets the metadata of the member associated with the specified key.
        /// </summary>
        /// <param name="memberName">The member name.</param>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <returns>The value with the specified key.</returns>
        public TValue GetMetadata<TValue>(string memberName, string key, TValue defaultValue)
        {
            object value = GetMetadata(memberName, key);
            return value == null ? defaultValue : (TValue)value;
        }
    }

    /// <summary>
    /// Contains the information of a table.
    /// </summary>
    /// <typeparam name="T">The type of the table.</typeparam>
    public class TableInfo<T> : TableInfo, ITableInfo<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TableInfo{T}"/> class.
        /// </summary>
        public TableInfo() : base(typeof(T))
        {
        }

        /// <summary>
        /// Gets the column name of the property.
        /// </summary>
        /// <param name="expression">The property.</param>
        /// <returns>The column name.</returns>
        /// <exception cref="InvalidConfigurationException">The type or property is not registered.</exception>
        public virtual string GetColumnName(Expression<Func<T, object>> expression)
        {
            return GetColumnName(ExpressionProcessor.GetPropertyPath(expression));
        }

        /// <summary>
        /// Gets the metadata of the member.
        /// </summary>
        /// <param name="expression">The property.</param>
        /// <returns>The metadata of the member.</returns>
        public virtual IDictionary<string, object> GetMetadata(Expression<Func<T, object>> expression)
        {
            return GetMetadata(ExpressionProcessor.GetPropertyPath(expression));
        }

        /// <summary>
        /// Gets the metadata of the member associated with the specified key.
        /// </summary>
        /// <param name="expression">The property.</param>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value with the specified key.</returns>
        public virtual object GetMetadata(Expression<Func<T, object>> expression, string key)
        {
            return GetMetadata(ExpressionProcessor.GetPropertyPath(expression), key);
        }

        /// <summary>
        /// Gets the metadata of the member associated with the specified key.
        /// </summary>
        /// <param name="expression">The property.</param>
        /// <param name="key">The key of the value to get.</param>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <returns>The value with the specified key.</returns>
        public TValue GetMetadata<TValue>(Expression<Func<T, object>> expression, string key)
        {
            object value = GetMetadata(expression, key);
            return value == null ? default(TValue) : (TValue)value;
        }

        /// <summary>
        /// Gets the metadata of the member associated with the specified key.
        /// </summary>
        /// <param name="expression">The property.</param>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <returns>The value with the specified key.</returns>
        public TValue GetMetadata<TValue>(Expression<Func<T, object>> expression, string key, TValue defaultValue)
        {
            object value = GetMetadata(expression, key);
            return value == null ? defaultValue : (TValue)value;
        }
    }
}