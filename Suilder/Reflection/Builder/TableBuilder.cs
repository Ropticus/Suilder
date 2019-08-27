using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Suilder.Builder;
using Suilder.Reflection.Builder.Processors;

namespace Suilder.Reflection.Builder
{
    /// <summary>
    /// Implementation of <see cref="ITableBuilder"/>.
    /// </summary>
    public class TableBuilder : ITableBuilder
    {
        /// <summary>
        /// The list of configuration processors.
        /// </summary>
        /// <returns>The list of configuration processors.</returns>
        protected IList<IConfigProcessor> Processors { get; set; } = new List<IConfigProcessor>();

        /// <summary>
        /// The configuration data.
        /// </summary>
        /// <returns>The configuration data.</returns>
        protected ConfigData ConfigData { get; set; } = new ConfigData();

        /// <summary>
        /// The table info of all tables.
        /// </summary>
        /// <value>The table info of all tables.</value>
        protected IList<ITableInfo> Tables { get; set; } = new List<ITableInfo>();

        /// <summary>
        /// Initializes a new instance of the <see cref="TableBuilder"/> class.
        /// </summary>
        public TableBuilder()
        {
            Processors.Add(new DefaultPropertyProcessor());
            Processors.Add(new DefaultAttributeProcessor());
            Processors.Add(new DefaultConfigProcessor());
        }

        /// <summary>
        /// Checks if the builder is already initialized.
        /// </summary>
        /// <exception cref="InvalidOperationException">The builder is already initialized.</exception>
        protected void CheckInitialized()
        {
            if (Tables.Count > 0)
                throw new InvalidOperationException("The builder is already initialized.");
        }

        /// <summary>
        /// Sets if by default the type must inherit the table name and the columns of the base type.
        /// </summary>
        /// <param name="inheritTable">If the type must inherit the table name and the columns of the base type.</param>
        /// <returns>The table builder.</returns>
        public ITableBuilder DefaultInheritTable(bool inheritTable)
        {
            if (inheritTable)
                return DefaultInheritTable(x => !x.IsAbstract && !x.BaseType.IsAbstract);
            else
                return DefaultInheritTable(x => false);
        }

        /// <summary>
        /// Sets a function to get the default "InheritTable" value.
        /// </summary>
        /// <param name="func">The function.</param>
        /// <returns>The table builder.</returns>
        public ITableBuilder DefaultInheritTable(Func<Type, bool> func)
        {
            CheckInitialized();

            ConfigData.InheritTableDefault = func;
            return this;
        }

        /// <summary>
        /// Sets a function to get the default "InheritColumns" value.
        /// </summary>
        /// <param name="func">The function.</param>
        /// <returns>The table builder.</returns>
        public ITableBuilder DefaultInheritColumns(Func<Type, bool> func)
        {
            CheckInitialized();

            ConfigData.InheritColumnsDefault = func;
            return this;
        }

        /// <summary>
        /// Sets a function to get the default table name.
        /// </summary>
        /// <param name="func">The function.</param>
        /// <returns>The table builder.</returns>
        public ITableBuilder DefaultTableName(Func<Type, string> func)
        {
            CheckInitialized();

            ConfigData.TableNameDefault = func;
            return this;
        }

        /// <summary>
        /// Sets a function to get the default primary key.
        /// </summary>
        /// <param name="func">The function.</param>
        /// <returns>The table builder.</returns>
        public ITableBuilder DefaultPrimaryKey(Func<Type, string> func)
        {
            CheckInitialized();

            ConfigData.PrimaryKeyDefault = func;
            return this;
        }

        /// <summary>
        /// Registers a type and adds its configuration.
        /// </summary>
        /// <param name="type">The type to register.</param>
        /// <returns>The entity builder.</returns>
        public IEntityBuilder Add(Type type)
        {
            CheckInitialized();

            return new EntityBuilder(ConfigData.GetConfigOrDefault(type));
        }

        /// <summary>
        /// Registers a type and adds its configuration.
        /// </summary>
        /// <param name="type">The type to register.</param>
        /// <param name="func">Function that returns an entity builder.</param>
        /// <returns>The table builder.</returns>
        public ITableBuilder Add(Type type, Func<IEntityBuilder, IEntityBuilder> func)
        {
            func(Add(type));
            return this;
        }

        /// <summary>
        /// Registers a type and adds its configuration.
        /// </summary>
        /// <typeparam name="T">The type to register.</typeparam>
        /// <returns>The entity builder.</returns>
        public IEntityBuilder<T> Add<T>()
        {
            CheckInitialized();

            return new EntityBuilder<T>(ConfigData.GetConfigOrDefault(typeof(T)));
        }

        /// <summary>
        /// Registers a type and adds its configuration.
        /// </summary>
        /// <param name="func">Function that returns an entity builder.</param>
        /// <typeparam name="T">The type to register.</typeparam>
        /// <returns>The table builder.</returns>
        public ITableBuilder Add<T>(Func<IEntityBuilder<T>, IEntityBuilder<T>> func)
        {
            func(Add<T>());
            return this;
        }

        /// <summary>
        /// Resets the configuration of a type.
        /// </summary>
        /// <param name="type">The type to reset.</param>
        /// <returns>The table builder.</returns>
        public ITableBuilder Reset(Type type)
        {
            CheckInitialized();

            ConfigData.RemoveConfig(type);
            ConfigData.GetConfigOrDefault(type);

            return this;
        }

        /// <summary>
        /// Resets the configuration of a type.
        /// </summary>
        /// <typeparam name="T">The type to reset.</typeparam>
        /// <returns>The table builder.</returns>
        public ITableBuilder Reset<T>()
        {
            return Reset(typeof(T));
        }

        /// <summary>
        /// Registers a type as nested.
        /// </summary>
        /// <param name="type">The type to register.</param>
        /// <returns>The table builder.</returns>
        public ITableBuilder AddNested(Type type)
        {
            CheckInitialized();

            ConfigData.AddNested(type);
            return this;
        }

        /// <summary>
        /// Registers a type as nested.
        /// </summary>
        /// <typeparam name="T">The type to register.</typeparam>
        /// <returns>The table builder.</returns>
        public ITableBuilder AddNested<T>() => AddNested(typeof(T));

        /// <summary>
        /// Removes a nested type.
        /// </summary>
        /// <param name="type">The type to remove.</param>
        /// <returns>The table builder.</returns>
        public ITableBuilder RemoveNested(Type type)
        {
            CheckInitialized();

            ConfigData.RemoveNested(type);
            return this;
        }

        /// <summary>
        /// Removes a nested type.
        /// </summary>
        /// <typeparam name="T">The type to remove.</typeparam>
        /// <returns>The table builder.</returns>
        public ITableBuilder RemoveNested<T>() => RemoveNested(typeof(T));

        /// <summary>
        /// Gets the registered types.
        /// <para>It includes all base types.</para>
        /// </summary>
        /// <returns>The registered types.</returns>
        public Type[] GetRegisteredTypes()
        {
            return ConfigData.ConfigTypes.Select(x => x.Value.Type).ToArray();
        }

        /// <summary>
        /// Adds a configuration processor.
        /// </summary>
        /// <param name="configProcessor">The configuration processor to add.</param>
        /// <returns>The table builder.</returns>
        public ITableBuilder AddProcessor(IConfigProcessor configProcessor)
        {
            CheckInitialized();

            Processors.Add(configProcessor);
            return this;
        }

        /// <summary>
        /// Removes all the configuration processors.
        /// </summary>
        /// <returns>The table builder.</returns>
        public ITableBuilder ClearProcessors()
        {
            CheckInitialized();

            Processors.Clear();
            return this;
        }

        /// <summary>
        /// Enables or disables the attributes processors.
        /// </summary>
        /// <param name="enable"><see langword="true"/> to enable, <see langword="false"/> to disable.</param>
        /// <returns>The table builder.</returns>
        public ITableBuilder EnableAttributes(bool enable = true)
        {
            ConfigData.EnableAttributes = enable;
            return this;
        }

        /// <summary>
        /// Disables the attributes processors.
        /// </summary>
        /// <returns>The table builder.</returns>
        public ITableBuilder DisableAttributes() => EnableAttributes(false);

        /// <summary>
        /// Enables or disables the metadata processors.
        /// </summary>
        /// <see langword="true"/> to enable, <see langword="false"/> to disable.
        /// <returns>The table builder.</returns>
        public ITableBuilder EnableMetadata(bool enable = true)
        {
            ConfigData.EnableMetadata = enable;
            return this;
        }

        /// <summary>
        /// Disables the metadata processors.
        /// </summary>
        /// <returns>The table builder.</returns>
        public ITableBuilder DisableMetadata() => EnableMetadata(false);

        /// <summary>
        /// Gets the configuration of all tables.
        /// </summary>
        /// <returns>The configuration of all tables.</returns>
        public virtual IList<ITableInfo> GetConfig()
        {
            if (Tables.Count > 0)
                return Tables;

            ResultData resultData = new ResultData(ConfigData.ConfigTypes.Select(x => x.Value.Type).ToArray());

            foreach (IConfigProcessor processor in Processors)
            {
                if ((!ConfigData.EnableAttributes && processor is IAttributeProcessor)
                    || (!ConfigData.EnableMetadata && processor is IMetadataProcessor))
                {
                    continue;
                }

                processor.Process(ConfigData, resultData);
            }

            foreach (TableInfo tableInfo in resultData.ResultTypes.Select(x => x.Value)
                .Where(x => ConfigData.GetConfig(x.Type).IsTable == true))
            {
                // Register in ExpressionProcessor
                ExpressionProcessor.AddTable(tableInfo.Type);

                // Add table and make readonly
                Tables.Add(ToReadOnly(tableInfo));
            }

            ConfigData = null;
            return Tables;
        }

        /// <summary>
        /// Converts the table info to readonly.
        /// </summary>
        /// <param name="tableInfo">The table info.</param>
        /// <returns>The table info as readonly.</returns>
        protected virtual TableInfo ToReadOnly(TableInfo tableInfo)
        {
            tableInfo.PrimaryKeys = new ReadOnlyCollection<string>(tableInfo.PrimaryKeys);
            tableInfo.Columns = new ReadOnlyCollection<string>(tableInfo.Columns);
            tableInfo.ColumnNames = new ReadOnlyCollection<string>(tableInfo.ColumnNames);
            tableInfo.ColumnNamesDic = new ReadOnlyDictionary<string, string>(tableInfo.ColumnNamesDic);
            tableInfo.ForeignKeys = new ReadOnlyCollection<string>(tableInfo.ForeignKeys);
            tableInfo.TableMetadata = new ReadOnlyDictionary<string, object>(tableInfo.TableMetadata);

            var memberMetadata = new Dictionary<string, IDictionary<string, object>>();
            foreach (var memberItem in tableInfo.MemberMetadata)
            {
                memberMetadata.Add(memberItem.Key, new ReadOnlyDictionary<string, object>(memberItem.Value));
            }
            tableInfo.MemberMetadata = new ReadOnlyDictionary<string, IDictionary<string, object>>(memberMetadata);

            return tableInfo;
        }
    }
}