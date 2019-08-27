namespace Suilder.Reflection.Builder
{
    /// <summary>
    /// Implementation of <see cref="IPropertyBuilder"/>.
    /// </summary>
    public class PropertyBuilder : IPropertyBuilder
    {
        /// <summary>
        /// The entity builder.
        /// </summary>
        /// <value>The entity builder.</value>
        protected EntityBuilder EntityBuilder { get; set; }

        /// <summary>
        /// The property name.
        /// </summary>
        /// <value>The property name.</value>
        protected string PropertyName { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyBuilder"/> class.
        /// </summary>
        /// <param name="entityBuilder">The entity builder.</param>
        /// <param name="propertyName">The property name.</param>
        public PropertyBuilder(EntityBuilder entityBuilder, string propertyName)
        {
            EntityBuilder = entityBuilder;
            PropertyName = propertyName;
        }

        /// <summary>
        /// Sets the primary key of the table.
        /// <para>Call multiple times for composite keys.</para>
        /// </summary>
        /// <returns>The property builder.</returns>
        public IPropertyBuilder PrimaryKey()
        {
            EntityBuilder.AddPrimaryKey(PropertyName);
            return this;
        }

        /// <summary>
        /// Sets the property as foreign key.
        /// <para>Call multiple times for composite keys.</para>
        /// </summary>
        /// <returns>The property builder.</returns>
        public IPropertyBuilder ForeignKey()
        {
            EntityBuilder.AddForeignKey(PropertyName);
            return this;
        }

        /// <summary>
        /// Sets the property as foreign key and its column name.
        /// <para>Call multiple times for composite keys.</para>
        /// </summary>
        /// <param name="columnName">The column name.</param>
        /// <returns>The property builder.</returns>
        public IPropertyBuilder ForeignKey(string columnName)
        {
            EntityBuilder.AddForeignKey(PropertyName, columnName);
            return this;
        }

        /// <summary>
        /// Sets the column name of the property.
        /// </summary>
        /// <param name="columnName">The column name.</param>
        /// <returns>The property builder.</returns>
        public IPropertyBuilder ColumnName(string columnName)
        {
            EntityBuilder.AddColumnName(PropertyName, columnName);
            return this;
        }

        /// <summary>
        /// Ignores the property.
        /// </summary>
        /// <returns>The property builder.</returns>
        public IPropertyBuilder Ignore()
        {
            EntityBuilder.AddIgnore(PropertyName);
            return this;
        }

        /// <summary>
        /// Adds metadata with the specified key to the member.
        /// </summary>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value">The value of the element to add.</param>
        /// <returns>The property builder.</returns>
        public IPropertyBuilder AddMetadata(string key, object value)
        {
            EntityBuilder.AddMetadata(PropertyName, key, value);
            return this;
        }

        /// <summary>
        /// Removes the metadata with the specified key from the member.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns>The property builder.</returns>

        public IPropertyBuilder RemoveMetadata(string key)
        {
            EntityBuilder.RemoveMetadata(PropertyName, key);
            return this;
        }
    }

    /// <summary>
    /// Implementation of <see cref="IPropertyBuilder{TTable, TProperty}"/>.
    /// </summary>
    /// <typeparam name="TTable">The type of the table.</typeparam>
    /// <typeparam name="TProperty">The type of the property</typeparam>
    public class PropertyBuilder<TTable, TProperty> : PropertyBuilder, IPropertyBuilder<TTable, TProperty>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyBuilder{TTable, TProperty}"/> class.
        /// </summary>
        /// <param name="entityBuilder">The entity builder.</param>
        /// <param name="propertyName">The property name.</param>
        public PropertyBuilder(EntityBuilder entityBuilder, string propertyName) : base(entityBuilder, propertyName)
        {
        }

        /// <summary>
        /// Sets the primary key of the table.
        /// <para>Call multiple times for composite keys.</para>
        /// </summary>
        /// <returns>The property builder.</returns>
        public new IPropertyBuilder<TTable, TProperty> PrimaryKey()
        {
            base.PrimaryKey();
            return this;
        }

        /// <summary>
        /// Sets the property as foreign key.
        /// <para>Call multiple times for composite keys.</para>
        /// </summary>
        /// <returns>The property builder.</returns>
        public new IPropertyBuilder<TTable, TProperty> ForeignKey()
        {
            base.ForeignKey();
            return this;
        }

        /// <summary>
        /// Sets the property as foreign key and its column name.
        /// <para>Call multiple times for composite keys.</para>
        /// </summary>
        /// <param name="columnName">The column name.</param>
        /// <returns>The property builder.</returns>
        public new IPropertyBuilder<TTable, TProperty> ForeignKey(string columnName)
        {
            base.ForeignKey(columnName);
            return this;
        }

        /// <summary>
        /// Sets the column name of the property.
        /// </summary>
        /// <param name="columnName">The column name.</param>
        /// <returns>The property builder.</returns>
        public new IPropertyBuilder<TTable, TProperty> ColumnName(string columnName)
        {
            base.ColumnName(columnName);
            return this;
        }

        /// <summary>
        /// Ignores the property.
        /// </summary>
        /// <returns>The property builder.</returns>
        public new IPropertyBuilder<TTable, TProperty> Ignore()
        {
            base.Ignore();
            return this;
        }

        /// <summary>
        /// Adds metadata with the specified key to the member.
        /// </summary>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value">The value of the element to add.</param>
        /// <returns>The property builder.</returns>
        public new IPropertyBuilder<TTable, TProperty> AddMetadata(string key, object value)
        {
            base.AddMetadata(key, value);
            return this;
        }

        /// <summary>
        /// Removes the metadata with the specified key from the member.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns>The property builder.</returns>
        public new IPropertyBuilder<TTable, TProperty> RemoveMetadata(string key)
        {
            base.RemoveMetadata(key);
            return this;
        }
    }
}