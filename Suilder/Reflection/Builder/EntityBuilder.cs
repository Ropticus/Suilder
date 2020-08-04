using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Suilder.Builder;

namespace Suilder.Reflection.Builder
{
    /// <summary>
    /// Implementation of <see cref="IEntityBuilder"/>.
    /// </summary>
    public class EntityBuilder : IEntityBuilder
    {
        /// <summary>
        /// The configuration data.
        /// </summary>
        /// <returns>The configuration data.</returns>
        protected TableConfig Config { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityBuilder"/> class.
        /// </summary>
        /// <param name="config">The configuration data.</param>
        public EntityBuilder(TableConfig config)
        {
            Config = config;
        }

        /// <summary>
        /// If the type is a table.
        /// <para>If <see langword="false"/> the type is only used for inherit the configuration.</para>
        /// <para>By default is <see langword="false"/> for <see langword="abstract"/> classes.</para>
        /// </summary>
        /// <param name="isTable">If the type is a table.</param>
        /// <returns>The entity builder.</returns>
        public IEntityBuilder IsTable(bool isTable)
        {
            Config.IsTable = isTable;
            return this;
        }

        /// <summary>
        /// If the type must inherit the table name and the columns of the base type.
        /// </summary>
        /// <param name="inherit">If the type must inherit the table name and the columns of the base type.</param>
        /// <returns>The entity builder.</returns>
        public IEntityBuilder InheritTable(bool inherit)
        {
            Config.InheritTable = inherit;
            return this;
        }

        /// <summary>
        /// If the type must inherit the columns of the base type.
        /// <para>By default is <see langword="true"/> for types whose base type is <see langword="abstract"/>.</para>
        /// </summary>
        /// <param name="inherit">If the type must inherit the columns of the base type.</param>
        /// <returns>The entity builder.</returns>
        public IEntityBuilder InheritColumns(bool inherit)
        {
            Config.InheritColumns = inherit;
            return this;
        }

        /// <summary>
        /// Sets the schema of the table.
        /// </summary>
        /// <param name="schema">The schema name.</param>
        /// <returns>The entity builder.</returns>
        public IEntityBuilder Schema(string schema)
        {
            Config.Schema = schema;
            return this;
        }

        /// <summary>
        /// Sets the name of the table.
        /// </summary>
        /// <param name="tableName">The table name.</param>
        /// <returns>The entity builder.</returns>
        public IEntityBuilder TableName(string tableName)
        {
            Config.TableName = tableName;
            return this;
        }

        /// <summary>
        /// Adds a primary key.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        protected internal void AddPrimaryKey(string propertyName)
        {
            Config.PrimaryKeys.Add(propertyName);
        }

        /// <summary>
        /// Sets the primary key of the table.
        /// <para>Call multiple times for composite keys.</para>
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <returns>The entity builder.</returns>
        public IEntityBuilder PrimaryKey(string propertyName)
        {
            AddPrimaryKey(GetProperty(propertyName));
            return this;
        }

        /// <summary>
        /// Adds a foreign key.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        protected internal void AddForeignKey(string propertyName)
        {
            Config.ForeignKeys.Add(propertyName);
        }

        /// <summary>
        /// Adds a foreign key.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <param name="columnName">The column name.</param>
        protected internal void AddForeignKey(string propertyName, string columnName)
        {
            AddForeignKey(propertyName);
            AddColumnName(propertyName, columnName);
        }

        /// <summary>
        /// Sets the property as foreign key.
        /// <para>Call multiple times for composite keys.</para>
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <returns>The entity builder.</returns>
        public IEntityBuilder ForeignKey(string propertyName)
        {
            AddForeignKey(GetProperty(propertyName));
            return this;
        }

        /// <summary>
        /// Sets the property as foreign key and its column name.
        /// <para>Call multiple times for composite keys.</para>
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <param name="columnName">The column name.</param>
        /// <returns>The entity builder.</returns>
        public IEntityBuilder ForeignKey(string propertyName, string columnName)
        {
            AddForeignKey(GetProperty(propertyName), columnName);
            return this;
        }

        /// <summary>
        /// Adds a column name.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <param name="columnName">The column name.</param>
        protected internal void AddColumnName(string propertyName, string columnName)
        {
            Config.ColumnNames[propertyName] = columnName;
        }

        /// <summary>
        /// Sets the column name of the property.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <param name="columnName">The column name.</param>
        /// <returns>The entity builder.</returns>
        public IEntityBuilder ColumnName(string propertyName, string columnName)
        {
            AddColumnName(GetProperty(propertyName), columnName);
            return this;
        }

        /// <summary>
        /// Adds an ignored property.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        protected internal void AddIgnore(string propertyName)
        {
            Config.Ignore.Add(propertyName);
        }

        /// <summary>
        /// Ignores the property.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <returns>The entity builder.</returns>
        public IEntityBuilder Ignore(string propertyName)
        {
            AddIgnore(GetProperty(propertyName));
            return this;
        }

        /// <summary>
        /// Adds metadata with the specified key to the table.
        /// </summary>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value">The value of the element to add.</param>
        /// <returns>The entity builder.</returns>
        public IEntityBuilder AddTableMetadata(string key, object value)
        {
            Config.TableMetadata[key] = value;
            return this;
        }

        /// <summary>
        /// Removes the metadata with the specified key from the table.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns>The entity builder.</returns>
        public IEntityBuilder RemoveTableMetadata(string key)
        {
            Config.TableMetadata.Remove(key);
            return this;
        }

        /// <summary>
        /// Adds metadata with the specified key to the member.
        /// </summary>
        /// <param name="memberName">The member name, it can be any key, even members that do not exist.</param>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value">The value of the element to add.</param>
        /// <returns>The entity builder.</returns>
        public IEntityBuilder AddMetadata(string memberName, string key, object value)
        {
            if (!Config.MemberMetadata.TryGetValue(memberName, out var metadata))
            {
                metadata = new Dictionary<string, object>();
                Config.MemberMetadata.Add(memberName, metadata);
            }

            metadata[key] = value;
            return this;
        }

        /// <summary>
        /// Removes the metadata with the specified key from the member.
        /// </summary>
        /// <param name="memberName">The member name, it can be any key, even members that do not exist.</param>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns>The entity builder.</returns>
        public IEntityBuilder RemoveMetadata(string memberName, string key)
        {
            if (Config.MemberMetadata.TryGetValue(memberName, out var metadata))
                metadata.Remove(key);

            return this;
        }

        /// <summary>
        /// Returns a property builder.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <returns>The property builder.</returns>
        public IPropertyBuilder Property(string propertyName)
        {
            return new PropertyBuilder(this, propertyName);
        }

        /// <summary>
        /// Sets the configuration of the property.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="func">Function that returns a property builder.</param>
        /// <returns>The entity builder.</returns>
        public IEntityBuilder Property(string propertyName, Func<IPropertyBuilder, IPropertyBuilder> func)
        {
            func(Property(propertyName));
            return this;
        }

        /// <summary>
        /// Checks if a property exists and return it.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <returns>The property name.</returns>
        protected string GetProperty(string propertyName)
        {
            if (ExpressionProcessor.GetProperty(Config.Type, propertyName) == null)
            {
                throw new ArgumentException($"The type \"{Config.Type}\" does not have property \"{propertyName}\".",
                    nameof(propertyName));
            }

            return propertyName;
        }
    }

    /// <summary>
    /// Implementation of <see cref="IEntityBuilder{TTable}"/>.
    /// </summary>
    /// <typeparam name="TTable">The type of the table.</typeparam>
    public class EntityBuilder<TTable> : EntityBuilder, IEntityBuilder<TTable>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityBuilder{TTable}"/> class.
        /// </summary>
        /// <param name="config">The configuration data.</param>
        public EntityBuilder(TableConfig config) : base(config)
        {
        }

        /// <summary>
        /// If the type is a table.
        /// <para>If <see langword="false"/> the type is only used for inherit the configuration.</para>
        /// <para>By default is <see langword="false"/> for <see langword="abstract"/> classes.</para>
        /// </summary>
        /// <param name="isTable">If the type is a table.</param>
        /// <returns>The entity builder.</returns>
        public new IEntityBuilder<TTable> IsTable(bool isTable)
        {
            base.IsTable(isTable);
            return this;
        }

        /// <summary>
        /// If the type must inherit the table name and the columns of the base type.
        /// </summary>
        /// <param name="inherit">If the type must inherit the table name and the columns of the base type.</param>
        /// <returns>The entity builder.</returns>
        public new IEntityBuilder<TTable> InheritTable(bool inherit)
        {
            base.InheritTable(inherit);
            return this;
        }

        /// <summary>
        /// If the type must inherit the columns of the base type.
        /// <para>By default is <see langword="true"/> for types whose base type is <see langword="abstract"/>.</para>
        /// </summary>
        /// <param name="inherit">If the type must inherit the columns of the base type.</param>
        /// <returns>The entity builder.</returns>
        public new IEntityBuilder<TTable> InheritColumns(bool inherit)
        {
            base.InheritColumns(inherit);
            return this;
        }

        /// <summary>
        /// Sets the schema of the table.
        /// </summary>
        /// <param name="schema">The schema name.</param>
        /// <returns>The entity builder.</returns>
        public new IEntityBuilder<TTable> Schema(string schema)
        {
            base.Schema(schema);
            return this;
        }

        /// <summary>
        /// Sets the name of the table.
        /// </summary>
        /// <param name="tableName">The table name.</param>
        /// <returns>The entity builder.</returns>
        public new IEntityBuilder<TTable> TableName(string tableName)
        {
            base.TableName(tableName);
            return this;
        }

        /// <summary>
        /// Sets the primary key of the table.
        /// <para>Call multiple times for composite keys.</para>
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <returns>The entity builder.</returns>
        public new IEntityBuilder<TTable> PrimaryKey(string propertyName)
        {
            base.PrimaryKey(propertyName);
            return this;
        }

        /// <summary>
        /// Sets the primary key of the table.
        /// <para>Call multiple times for composite keys.</para>
        /// </summary>
        /// <param name="expression">The property.</param>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <returns>The entity builder.</returns>
        public IEntityBuilder<TTable> PrimaryKey<TProperty>(Expression<Func<TTable, TProperty>> expression)
        {
            AddPrimaryKey(ExpressionProcessor.GetPropertyPath(expression));
            return this;
        }

        /// <summary>
        /// Sets the property as foreign key.
        /// <para>Call multiple times for composite keys.</para>
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <returns>The entity builder.</returns>
        public new IEntityBuilder<TTable> ForeignKey(string propertyName)
        {
            base.ForeignKey(propertyName);
            return this;
        }

        /// <summary>
        /// Sets the property as foreign key and its column name.
        /// <para>Call multiple times for composite keys.</para>
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <param name="columnName">The column name.</param>
        /// <returns>The entity builder.</returns>
        public new IEntityBuilder<TTable> ForeignKey(string propertyName, string columnName)
        {
            base.ForeignKey(propertyName, columnName);
            return this;
        }

        /// <summary>
        /// Sets the property as foreign key.
        /// <para>Call multiple times for composite keys.</para>
        /// </summary>
        /// <param name="expression">The property.</param>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <returns>The entity builder.</returns>
        public IEntityBuilder<TTable> ForeignKey<TProperty>(Expression<Func<TTable, TProperty>> expression)
        {
            AddForeignKey(ExpressionProcessor.GetPropertyPath(expression));
            return this;
        }

        /// <summary>
        /// Sets the property as foreign key and its column name.
        /// <para>Call multiple times for composite keys.</para>
        /// </summary>
        /// <param name="expression">The property.</param>
        /// <param name="columnName">The column name.</param>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <returns>The entity builder.</returns>
        public IEntityBuilder<TTable> ForeignKey<TProperty>(Expression<Func<TTable, TProperty>> expression,
            string columnName)
        {
            AddForeignKey(ExpressionProcessor.GetPropertyPath(expression), columnName);
            return this;
        }

        /// <summary>
        /// Sets the column name of the property.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <param name="columnName">The column name.</param>
        /// <returns>The entity builder.</returns>
        public new IEntityBuilder<TTable> ColumnName(string propertyName, string columnName)
        {
            base.ColumnName(propertyName, columnName);
            return this;
        }

        /// <summary>
        /// Sets the column name of the property.
        /// </summary>
        /// <param name="expression">The property.</param>
        /// <param name="columnName">The column name.</param>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <returns>The entity builder.</returns>
        public IEntityBuilder<TTable> ColumnName<TProperty>(Expression<Func<TTable, TProperty>> expression,
            string columnName)
        {
            AddColumnName(ExpressionProcessor.GetPropertyPath(expression), columnName);
            return this;
        }

        /// <summary>
        /// Ignores the property.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <returns>The entity builder.</returns>
        public new IEntityBuilder<TTable> Ignore(string propertyName)
        {
            base.Ignore(propertyName);
            return this;
        }

        /// <summary>
        /// Ignores the property.
        /// </summary>
        /// <param name="expression">The property.</param>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <returns>The entity builder.</returns>
        public IEntityBuilder<TTable> Ignore<TProperty>(Expression<Func<TTable, TProperty>> expression)
        {
            AddIgnore(ExpressionProcessor.GetPropertyPath(expression));
            return this;
        }

        /// <summary>
        /// Adds metadata with the specified key to the table.
        /// </summary>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value">The value of the element to add.</param>
        /// <returns>The entity builder.</returns>
        public new IEntityBuilder<TTable> AddTableMetadata(string key, object value)
        {
            base.AddTableMetadata(key, value);
            return this;
        }

        /// <summary>
        /// Removes the metadata with the specified key from the table.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns>The entity builder.</returns>
        public new IEntityBuilder<TTable> RemoveTableMetadata(string key)
        {
            base.RemoveTableMetadata(key);
            return this;
        }

        /// <summary>
        /// Adds metadata with the specified key to the member.
        /// </summary>
        /// <param name="memberName">The member name, it can be any key, even members that do not exist.</param>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value">The value of the element to add.</param>
        /// <returns>The entity builder.</returns>
        public new IEntityBuilder<TTable> AddMetadata(string memberName, string key, object value)
        {
            base.AddMetadata(memberName, key, value);
            return this;
        }

        /// <summary>
        /// Adds metadata with the specified key to the member.
        /// </summary>
        /// <param name="expression">The property.</param>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value">The value of the element to add.</param>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <returns>The entity builder.</returns>
        public IEntityBuilder<TTable> AddMetadata<TProperty>(Expression<Func<TTable, TProperty>> expression, string key,
            object value)
        {
            base.AddMetadata(ExpressionProcessor.GetPropertyPath(expression), key, value);
            return this;
        }

        /// <summary>
        /// Removes the metadata with the specified key from the member.
        /// </summary>
        /// <param name="memberName">The member name, it can be any key, even members that do not exist.</param>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns>The entity builder.</returns>

        public new IEntityBuilder<TTable> RemoveMetadata(string memberName, string key)
        {
            base.RemoveMetadata(memberName, key);
            return this;
        }

        /// <summary>
        /// Removes the metadata with the specified key from the member.
        /// </summary>
        /// <param name="expression">The property.</param>
        /// <param name="key">The key of the element to remove.</param>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <returns>The entity builder.</returns>
        public IEntityBuilder<TTable> RemoveMetadata<TProperty>(Expression<Func<TTable, TProperty>> expression, string key)
        {
            base.RemoveMetadata(ExpressionProcessor.GetPropertyPath(expression), key);
            return this;
        }

        /// <summary>
        /// Returns a property builder.
        /// </summary>
        /// <param name="expression">The property.</param>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <returns>The property builder.</returns>
        public IPropertyBuilder<TTable, TProperty> Property<TProperty>(Expression<Func<TTable, TProperty>> expression)
        {
            return new PropertyBuilder<TTable, TProperty>(this, ExpressionProcessor.GetPropertyPath(expression));
        }

        /// <summary>
        /// Returns a property builder.
        /// </summary>
        /// <param name="expression">The property.</param>
        /// <param name="func">Function that returns a property builder.</param>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <returns>The entity builder.</returns>
        public IEntityBuilder<TTable> Property<TProperty>(Expression<Func<TTable, TProperty>> expression,
            Func<IPropertyBuilder<TTable, TProperty>, IPropertyBuilder<TTable, TProperty>> func)
        {
            func(Property(expression));
            return this;
        }
    }
}