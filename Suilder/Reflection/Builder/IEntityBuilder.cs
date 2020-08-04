using System;
using System.Linq.Expressions;

namespace Suilder.Reflection.Builder
{
    /// <summary>
    /// A builder to set the configuration of a table.
    /// </summary>
    public interface IEntityBuilder
    {
        /// <summary>
        /// If the type is a table.
        /// <para>If <see langword="false"/> the type is only used for inherit the configuration.</para>
        /// <para>By default is <see langword="false"/> for <see langword="abstract"/> classes.</para>
        /// </summary>
        /// <param name="isTable">If the type is a table.</param>
        /// <returns>The entity builder.</returns>
        IEntityBuilder IsTable(bool isTable);

        /// <summary>
        /// If the type must inherit the table name and the columns of the base type.
        /// </summary>
        /// <param name="inherit">If the type must inherit the table name and the columns of the base type.</param>
        /// <returns>The entity builder.</returns>
        IEntityBuilder InheritTable(bool inherit);

        /// <summary>
        /// If the type must inherit the columns of the base type.
        /// <para>By default is <see langword="true"/> for types whose base type is <see langword="abstract"/>.</para>
        /// </summary>
        /// <param name="inherit">If the type must inherit the columns of the base type.</param>
        /// <returns>The entity builder.</returns>
        IEntityBuilder InheritColumns(bool inherit);

        /// <summary>
        /// Sets the schema of the table.
        /// </summary>
        /// <param name="schema">The schema name.</param>
        /// <returns>The entity builder.</returns>
        IEntityBuilder Schema(string schema);

        /// <summary>
        /// Sets the name of the table.
        /// </summary>
        /// <param name="tableName">The table name.</param>
        /// <returns>The entity builder.</returns>
        IEntityBuilder TableName(string tableName);

        /// <summary>
        /// Sets the primary key of the table.
        /// <para>Call multiple times for composite keys.</para>
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <returns>The entity builder.</returns>
        IEntityBuilder PrimaryKey(string propertyName);

        /// <summary>
        /// Sets the property as foreign key.
        /// <para>Call multiple times for composite keys.</para>
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <returns>The entity builder.</returns>
        IEntityBuilder ForeignKey(string propertyName);

        /// <summary>
        /// Sets the property as foreign key and its column name.
        /// <para>Call multiple times for composite keys.</para>
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <param name="columnName">The column name.</param>
        /// <returns>The entity builder.</returns>
        IEntityBuilder ForeignKey(string propertyName, string columnName);

        /// <summary>
        /// Sets the column name of the property.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <param name="columnName">The column name.</param>
        /// <returns>The entity builder.</returns>
        IEntityBuilder ColumnName(string propertyName, string columnName);

        /// <summary>
        /// Ignores the property.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <returns>The entity builder.</returns>
        IEntityBuilder Ignore(string propertyName);

        /// <summary>
        /// Adds metadata with the specified key to the table.
        /// </summary>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value">The value of the element to add.</param>
        /// <returns>The entity builder.</returns>
        IEntityBuilder AddTableMetadata(string key, object value);

        /// <summary>
        /// Removes the metadata with the specified key from the table.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns>The entity builder.</returns>
        IEntityBuilder RemoveTableMetadata(string key);

        /// <summary>
        /// Adds metadata with the specified key to the member.
        /// </summary>
        /// <param name="memberName">The member name, it can be any key, even members that do not exist.</param>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value">The value of the element to add.</param>
        /// <returns>The entity builder.</returns>
        IEntityBuilder AddMetadata(string memberName, string key, object value);

        /// <summary>
        /// Removes the metadata with the specified key from the member.
        /// </summary>
        /// <param name="memberName">The member name, it can be any key, even members that do not exist.</param>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns>The entity builder.</returns>
        IEntityBuilder RemoveMetadata(string memberName, string key);

        /// <summary>
        /// Returns a property builder.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <returns>The property builder.</returns>
        IPropertyBuilder Property(string propertyName);

        /// <summary>
        /// Sets the configuration of the property.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="func">Function that returns a property builder.</param>
        /// <returns>The entity builder.</returns>
        IEntityBuilder Property(string propertyName, Func<IPropertyBuilder, IPropertyBuilder> func);
    }

    /// <summary>
    /// A builder to set the configuration of a table.
    /// </summary>
    /// <typeparam name="TTable">The type of the table.</typeparam>
    public interface IEntityBuilder<TTable> : IEntityBuilder
    {
        /// <summary>
        /// If the type is a table.
        /// <para>If <see langword="false"/> the type is only used for inherit the configuration.</para>
        /// <para>By default is <see langword="false"/> for <see langword="abstract"/> classes.</para>
        /// </summary>
        /// <param name="isTable">If the type is a table.</param>
        /// <returns>The entity builder.</returns>
        new IEntityBuilder<TTable> IsTable(bool isTable);

        /// <summary>
        /// If the type must inherit the table name and the columns of the base type.
        /// </summary>
        /// <param name="inherit">If the type must inherit the table name and the columns of the base type.</param>
        /// <returns>The entity builder.</returns>
        new IEntityBuilder<TTable> InheritTable(bool inherit);

        /// <summary>
        /// If the type must inherit the columns of the base type.
        /// <para>By default is <see langword="true"/> for types whose base type is <see langword="abstract"/>.</para>
        /// </summary>
        /// <param name="inherit">If the type must inherit the columns of the base type.</param>
        /// <returns>The entity builder.</returns>
        new IEntityBuilder<TTable> InheritColumns(bool inherit);

        /// <summary>
        /// Sets the schema of the table.
        /// </summary>
        /// <param name="schema">The schema name.</param>
        /// <returns>The entity builder.</returns>
        new IEntityBuilder<TTable> Schema(string schema);

        /// <summary>
        /// Sets the name of the table.
        /// </summary>
        /// <param name="tableName">The table name.</param>
        /// <returns>The entity builder.</returns>
        new IEntityBuilder<TTable> TableName(string tableName);

        /// <summary>
        /// Sets the primary key of the table.
        /// <para>Call multiple times for composite keys.</para>
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <returns>The entity builder.</returns>
        new IEntityBuilder<TTable> PrimaryKey(string propertyName);

        /// <summary>
        /// Sets the primary key of the table.
        /// <para>Call multiple times for composite keys.</para>
        /// </summary>
        /// <param name="expression">The property.</param>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <returns>The entity builder.</returns>
        IEntityBuilder<TTable> PrimaryKey<TProperty>(Expression<Func<TTable, TProperty>> expression);

        /// <summary>
        /// Sets the property as foreign key.
        /// <para>Call multiple times for composite keys.</para>
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <returns>The entity builder.</returns>
        new IEntityBuilder<TTable> ForeignKey(string propertyName);

        /// <summary>
        /// Sets the property as foreign key and its column name.
        /// <para>Call multiple times for composite keys.</para>
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <param name="columnName">The column name.</param>
        /// <returns>The entity builder.</returns>
        new IEntityBuilder<TTable> ForeignKey(string propertyName, string columnName);

        /// <summary>
        /// Sets the property as foreign key.
        /// <para>Call multiple times for composite keys.</para>
        /// </summary>
        /// <param name="expression">The property.</param>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <returns>The entity builder.</returns>
        IEntityBuilder<TTable> ForeignKey<TProperty>(Expression<Func<TTable, TProperty>> expression);

        /// <summary>
        /// Sets the property as foreign key and its column name.
        /// <para>Call multiple times for composite keys.</para>
        /// </summary>
        /// <param name="expression">The property.</param>
        /// <param name="columnName">The column name.</param>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <returns>The entity builder.</returns>
        IEntityBuilder<TTable> ForeignKey<TProperty>(Expression<Func<TTable, TProperty>> expression, string columnName);

        /// <summary>
        /// Sets the column name of the property.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <param name="columnName">The column name.</param>
        /// <returns>The entity builder.</returns>
        new IEntityBuilder<TTable> ColumnName(string propertyName, string columnName);

        /// <summary>
        /// Sets the column name of the property.
        /// </summary>
        /// <param name="expression">The property.</param>
        /// <param name="columnName">The column name.</param>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <returns>The entity builder.</returns>
        IEntityBuilder<TTable> ColumnName<TProperty>(Expression<Func<TTable, TProperty>> expression, string columnName);

        /// <summary>
        /// Ignores the property.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <returns>The entity builder.</returns>
        new IEntityBuilder<TTable> Ignore(string propertyName);

        /// <summary>
        /// Ignores the property.
        /// </summary>
        /// <param name="expression">The property.</param>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <returns>The entity builder.</returns>
        IEntityBuilder<TTable> Ignore<TProperty>(Expression<Func<TTable, TProperty>> expression);

        /// <summary>
        /// Adds metadata with the specified key to the table.
        /// </summary>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value">The value of the element to add.</param>
        /// <returns>The entity builder.</returns>
        new IEntityBuilder<TTable> AddTableMetadata(string key, object value);

        /// <summary>
        /// Removes the metadata with the specified key from the table.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns>The entity builder.</returns>
        new IEntityBuilder<TTable> RemoveTableMetadata(string key);

        /// <summary>
        /// Adds metadata with the specified key to the member.
        /// </summary>
        /// <param name="memberName">The member name, it can be any key, even members that do not exist.</param>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value">The value of the element to add.</param>
        /// <returns>The entity builder.</returns>
        new IEntityBuilder<TTable> AddMetadata(string memberName, string key, object value);

        /// <summary>
        /// Adds metadata with the specified key to the member.
        /// </summary>
        /// <param name="expression">The property.</param>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value">The value of the element to add.</param>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <returns>The entity builder.</returns>
        IEntityBuilder<TTable> AddMetadata<TProperty>(Expression<Func<TTable, TProperty>> expression, string key,
            object value);

        /// <summary>
        /// Removes the metadata with the specified key from the member.
        /// </summary>
        /// <param name="memberName">The member name, it can be any key, even members that do not exist.</param>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns>The entity builder.</returns>
        new IEntityBuilder<TTable> RemoveMetadata(string memberName, string key);

        /// <summary>
        /// Removes the metadata with the specified key from the member.
        /// </summary>
        /// <param name="expression">The property.</param>
        /// <param name="key">The key of the element to remove.</param>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <returns>The entity builder.</returns>
        IEntityBuilder<TTable> RemoveMetadata<TProperty>(Expression<Func<TTable, TProperty>> expression, string key);

        /// <summary>
        /// Returns a property builder.
        /// </summary>
        /// <param name="expression">The property.</param>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <returns>The property builder.</returns>
        IPropertyBuilder<TTable, TProperty> Property<TProperty>(Expression<Func<TTable, TProperty>> expression);

        /// <summary>
        /// Returns a property builder.
        /// </summary>
        /// <param name="expression">The property.</param>
        /// <param name="func">Function that returns a property builder.</param>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <returns>The entity builder.</returns>
        IEntityBuilder<TTable> Property<TProperty>(Expression<Func<TTable, TProperty>> expression,
            Func<IPropertyBuilder<TTable, TProperty>, IPropertyBuilder<TTable, TProperty>> func);
    }
}