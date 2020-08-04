using System;
using Suilder.Reflection.Builder.Processors;

namespace Suilder.Reflection.Builder
{
    /// <summary>
    /// A builder to register and set the configuration of the tables.
    /// </summary>
    public interface ITableBuilder : IConfigBuilder
    {
        /// <summary>
        /// Sets if by default the type must inherit the table name and the columns of the base type.
        /// </summary>
        /// <param name="inheritTable">If the type must inherit the table name and the columns of the base type.</param>
        /// <returns>The table builder.</returns>
        ITableBuilder DefaultInheritTable(bool inheritTable);

        /// <summary>
        /// Sets a function to get the default "InheritTable" value.
        /// </summary>
        /// <param name="func">The function.</param>
        /// <returns>The table builder.</returns>
        ITableBuilder DefaultInheritTable(Func<Type, bool> func);

        /// <summary>
        /// Sets a function to get the default "InheritColumns" value.
        /// </summary>
        /// <param name="func">The function.</param>
        /// <returns>The table builder.</returns>
        ITableBuilder DefaultInheritColumns(Func<Type, bool> func);

        /// <summary>
        /// Sets a function to get the default schema name.
        /// </summary>
        /// <param name="func">The function.</param>
        /// <returns>The table builder.</returns>
        ITableBuilder DefaultSchema(Func<Type, string> func);

        /// <summary>
        /// Sets a function to get the default table name.
        /// </summary>
        /// <param name="func">The function.</param>
        /// <returns>The table builder.</returns>
        ITableBuilder DefaultTableName(Func<Type, string> func);

        /// <summary>
        /// Sets a function to get the default primary key.
        /// </summary>
        /// <param name="func">The function.</param>
        /// <returns>The table builder.</returns>
        ITableBuilder DefaultPrimaryKey(Func<Type, string> func);

        /// <summary>
        /// Registers a type and adds its configuration.
        /// </summary>
        /// <param name="type">The type to register.</param>
        /// <returns>The entity builder.</returns>
        IEntityBuilder Add(Type type);

        /// <summary>
        /// Registers a type and adds its configuration.
        /// </summary>
        /// <param name="type">The type to register.</param>
        /// <param name="func">Function that returns an entity builder.</param>
        /// <returns>The table builder.</returns>
        ITableBuilder Add(Type type, Func<IEntityBuilder, IEntityBuilder> func);

        /// <summary>
        /// Registers a type and adds its configuration.
        /// </summary>
        /// <typeparam name="T">The type to register.</typeparam>
        /// <returns>The entity builder.</returns>
        IEntityBuilder<T> Add<T>();

        /// <summary>
        /// Registers a type and adds its configuration.
        /// </summary>
        /// <param name="func">Function that returns an entity builder.</param>
        /// <typeparam name="T">The type to register.</typeparam>
        /// <returns>The table builder.</returns>
        ITableBuilder Add<T>(Func<IEntityBuilder<T>, IEntityBuilder<T>> func);

        /// <summary>
        /// Resets the configuration of a type.
        /// </summary>
        /// <param name="type">The type to reset.</param>
        /// <returns>The table builder.</returns>
        ITableBuilder Reset(Type type);

        /// <summary>
        /// Resets the configuration of a type.
        /// </summary>
        /// <typeparam name="T">The type to reset.</typeparam>
        /// <returns>The table builder.</returns>
        ITableBuilder Reset<T>();

        /// <summary>
        /// Registers a type as nested.
        /// </summary>
        /// <param name="type">The type to register.</param>
        /// <returns>The table builder.</returns>
        ITableBuilder AddNested(Type type);

        /// <summary>
        /// Registers a type as nested.
        /// </summary>
        /// <typeparam name="T">The type to register.</typeparam>
        /// <returns>The table builder.</returns>
        ITableBuilder AddNested<T>();

        /// <summary>
        /// Removes a nested type.
        /// </summary>
        /// <param name="type">The type to remove.</param>
        /// <returns>The table builder.</returns>
        ITableBuilder RemoveNested(Type type);

        /// <summary>
        /// Removes a nested type.
        /// </summary>
        /// <typeparam name="T">The type to remove.</typeparam>
        /// <returns>The table builder.</returns>
        ITableBuilder RemoveNested<T>();

        /// <summary>
        /// Gets the registered types.
        /// <para>It includes all base types.</para>
        /// </summary>
        /// <returns>The registered types.</returns>
        Type[] GetRegisteredTypes();

        /// <summary>
        /// Adds a configuration processor.
        /// </summary>
        /// <param name="configProcessor">The configuration processor to add.</param>
        /// <returns>The table builder.</returns>
        ITableBuilder AddProcessor(IConfigProcessor configProcessor);

        /// <summary>
        /// Removes all the configuration processors.
        /// </summary>
        /// <returns>The table builder.</returns>
        ITableBuilder ClearProcessors();

        /// <summary>
        /// Enables or disables the attribute processors.
        /// </summary>
        /// <param name="enable"><see langword="true"/> to enable, <see langword="false"/> to disable.</param>
        /// <returns>The table builder.</returns>
        ITableBuilder EnableAttributes(bool enable = true);

        /// <summary>
        /// Disables the attribute processors.
        /// </summary>
        /// <returns>The table builder.</returns>
        ITableBuilder DisableAttributes();

        /// <summary>
        /// Enables or disables the metadata processors.
        /// </summary>
        /// <see langword="true"/> to enable, <see langword="false"/> to disable.
        /// <returns>The table builder.</returns>
        ITableBuilder EnableMetadata(bool enable = true);

        /// <summary>
        /// Disables the metadata processors.
        /// </summary>
        /// <returns>The table builder.</returns>
        ITableBuilder DisableMetadata();
    }
}