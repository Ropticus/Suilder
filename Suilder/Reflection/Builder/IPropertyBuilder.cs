namespace Suilder.Reflection.Builder
{
    /// <summary>
    /// A builder to set the configuration of a property.
    /// </summary>
    public interface IPropertyBuilder
    {
        /// <summary>
        /// Sets the primary key of the table.
        /// <para>Call multiple times for composite keys.</para>
        /// </summary>
        /// <returns>The property builder.</returns>
        IPropertyBuilder PrimaryKey();

        /// <summary>
        /// Sets the property as foreign key.
        /// <para>Call multiple times for composite keys.</para>
        /// </summary>
        /// <returns>The property builder.</returns>
        IPropertyBuilder ForeignKey();

        /// <summary>
        /// Sets the property as foreign key and its column name.
        /// <para>Call multiple times for composite keys.</para>
        /// </summary>
        /// <param name="columnName">The column name.</param>
        /// <returns>The property builder.</returns>
        IPropertyBuilder ForeignKey(string columnName);

        /// <summary>
        /// Sets the property as foreign key and its column name.
        /// <para>Call multiple times for composite keys.</para>
        /// </summary>
        /// <param name="columnName">The column name.</param>
        /// <param name="partialName">If it is a partial column name.</param>
        /// <returns>The property builder.</returns>
        IPropertyBuilder ForeignKey(string columnName, bool partialName);

        /// <summary>
        /// Sets the column name of the property.
        /// </summary>
        /// <param name="columnName">The column name.</param>
        /// <returns>The property builder.</returns>
        IPropertyBuilder ColumnName(string columnName);

        /// <summary>
        /// Sets the column name of the property.
        /// </summary>
        /// <param name="columnName">The column name.</param>
        /// <param name="partialName">If it is a partial column name.</param>
        /// <returns>The property builder.</returns>
        IPropertyBuilder ColumnName(string columnName, bool partialName);

        /// <summary>
        /// Ignores the property.
        /// </summary>
        /// <returns>The property builder.</returns>
        IPropertyBuilder Ignore();

        /// <summary>
        /// Adds metadata with the specified key to the member.
        /// </summary>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value">The value of the element to add.</param>
        /// <returns>The property builder.</returns>
        IPropertyBuilder AddMetadata(string key, object value);

        /// <summary>
        /// Removes the metadata with the specified key from the member.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns>The property builder.</returns>
        IPropertyBuilder RemoveMetadata(string key);
    }

    /// <summary>
    /// A builder to set the configuration of a property.
    /// </summary>
    /// <typeparam name="TTable">The type of the table.</typeparam>
    /// <typeparam name="TProperty">the type of the property.</typeparam>
    public interface IPropertyBuilder<TTable, TProperty> : IPropertyBuilder
    {
        /// <summary>
        /// Sets the primary key of the table.
        /// <para>Call multiple times for composite keys.</para>
        /// </summary>
        /// <returns>The property builder.</returns>
        new IPropertyBuilder<TTable, TProperty> PrimaryKey();

        /// <summary>
        /// Sets the property as foreign key.
        /// <para>Call multiple times for composite keys.</para>
        /// </summary>
        /// <returns>The property builder.</returns>
        new IPropertyBuilder<TTable, TProperty> ForeignKey();

        /// <summary>
        /// Sets the property as foreign key and its column name.
        /// <para>Call multiple times for composite keys.</para>
        /// </summary>
        /// <param name="columnName">The column name.</param>
        /// <returns>The property builder.</returns>
        new IPropertyBuilder<TTable, TProperty> ForeignKey(string columnName);

        /// <summary>
        /// Sets the property as foreign key and its column name.
        /// <para>Call multiple times for composite keys.</para>
        /// </summary>
        /// <param name="columnName">The column name.</param>
        /// <param name="partialName">If it is a partial column name.</param>
        /// <returns>The property builder.</returns>
        new IPropertyBuilder<TTable, TProperty> ForeignKey(string columnName, bool partialName);

        /// <summary>
        /// Sets the column name of the property.
        /// </summary>
        /// <param name="columnName">The column name.</param>
        /// <returns>The property builder.</returns>
        new IPropertyBuilder<TTable, TProperty> ColumnName(string columnName);

        /// <summary>
        /// Sets the column name of the property.
        /// </summary>
        /// <param name="columnName">The column name.</param>
        /// <param name="partialName">If it is a partial column name.</param>
        /// <returns>The property builder.</returns>
        new IPropertyBuilder<TTable, TProperty> ColumnName(string columnName, bool partialName);

        /// <summary>
        /// Ignores the property.
        /// </summary>
        /// <returns>The property builder.</returns>
        new IPropertyBuilder<TTable, TProperty> Ignore();

        /// <summary>
        /// Adds metadata with the specified key to the member.
        /// </summary>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value">The value of the element to add.</param>
        /// <returns>The property builder.</returns>
        new IPropertyBuilder<TTable, TProperty> AddMetadata(string key, object value);

        /// <summary>
        /// Removes the metadata with the specified key from the member.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns>The property builder.</returns>
        new IPropertyBuilder<TTable, TProperty> RemoveMetadata(string key);
    }
}