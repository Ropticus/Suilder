using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Suilder.Builder;
using Suilder.Exceptions;

namespace Suilder.Reflection
{
    /// <summary>
    /// A builder to register and set the configuration of the tables.
    /// </summary>
    public class TableBuilder : ITableBuilder
    {
        /// <summary>
        /// The configuration of all types.
        /// </summary>
        /// <returns>The configuration of all types.</returns>
        protected IDictionary<string, ConfigData> Config { get; set; } = new Dictionary<string, ConfigData>();

        /// <summary>
        /// Contains the information of all tables.
        /// </summary>
        /// <value>Contains the information of all tables.</value>
        protected IList<TableInfo> Tables { get; set; } = new List<TableInfo>();

        /// <summary>
        /// Delegate to get the default primary key.
        /// </summary>
        /// <value>Delegate to get the default primary key.</value>
        protected Func<Type, string> PrimaryKeyDefault { get; set; } = type =>
        {
            return type.GetProperty("Id") != null ? "Id" : null;
        };

        /// <summary>
        /// Custom function to get the default primary key.
        /// </summary>
        /// <param name="func">The function.</param>
        /// <returns>The builder.</returns>
        public TableBuilder PrimaryKey(Func<Type, string> func)
        {
            if (Tables.Count > 0)
                throw new InvalidOperationException("The builder is already initialized.");

            PrimaryKeyDefault = func;
            return this;
        }

        /// <summary>
        /// Delegate to get the table name.
        /// </summary>
        /// <value>Delegate to get the table name.</value>
        protected Func<Type, string> TableNameDefault { get; set; } = type =>
        {
            return type.Name;
        };

        /// <summary>
        /// Custom function to get the default table name.
        /// </summary>
        /// <param name="func">The function.</param>
        /// <returns>The builder.</returns>
        public TableBuilder TableName(Func<Type, string> func)
        {
            if (Tables.Count > 0)
                throw new InvalidOperationException("The builder is already initialized.");

            TableNameDefault = func;
            return this;
        }

        /// <summary>
        /// Adds a type to the builder.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The builder.</returns>
        public TableBuilder Add(Type type)
        {
            if (Tables.Count > 0)
                throw new InvalidOperationException("The builder is already initialized.");

            Type configType = typeof(TableConfig<>).MakeGenericType(type);
            Config.Add(type.FullName, ((TableConfig)Activator.CreateInstance(configType)).GetConfig());
            return this;
        }

        /// <summary>
        /// Adds a type to the builder.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <returns>The builder.</returns>
        public TableBuilder Add<T>()
        {
            if (Tables.Count > 0)
                throw new InvalidOperationException("The builder is already initialized.");

            Config.Add(typeof(T).FullName, new TableConfig<T>().GetConfig());
            return this;
        }

        /// <summary>
        /// Adds a type to the builder with custom configuration.
        /// <para>If the type is abstract, by default, is only used to inherit the configuration and is not registered
        /// as a table.</para>
        /// </summary>
        /// <param name="func">Function that returns the configuration.</param>
        /// <typeparam name="T">The type.</typeparam>
        /// <returns>The builder.</returns>
        public TableBuilder Add<T>(Func<TableConfig<T>, TableConfig<T>> func)
        {
            if (Tables.Count > 0)
                throw new InvalidOperationException("The builder is already initialized.");

            Config.Add(typeof(T).FullName, func(new TableConfig<T>()).GetConfig());
            return this;
        }

        /// <summary>
        /// Gets the config data grouping by inherance level.
        /// <para>Also registers the missing parent types.</para>
        /// </summary>
        /// <returns>The config data grouping by inherance level.</returns>
        protected IGrouping<int, ConfigData>[] GetLevels()
        {
            foreach (var item in Config.ToArray())
            {
                Type parentType = item.Value.Type;
                while (parentType.BaseType != null)
                {
                    parentType = parentType.BaseType;
                    if (parentType != typeof(object) && !Config.ContainsKey(parentType.FullName))
                    {
                        Add(parentType);
                    }
                }
            }

            return Config.Select(x => x.Value).OrderBy(x => x.InheritLevel).GroupBy(x => x.InheritLevel).ToArray();
        }

        /// <summary>
        /// Gets the config of all tables.
        /// </summary>
        /// <returns>The config of all tables.</returns>
        public IList<TableInfo> GetConfig()
        {
            if (Tables.Count > 0)
                return Tables;

            IDictionary<string, TableInfo> tables = new Dictionary<string, TableInfo>();
            var levels = GetLevels();

            foreach (var level in levels)
            {
                foreach (ConfigData config in level)
                {
                    // Get parent config
                    TableInfo parentInfo = null;
                    Type parentType = config.Type.BaseType;
                    if (parentType != null && parentType != typeof(object))
                    {
                        tables.TryGetValue(parentType.FullName, out parentInfo);
                    }

                    TableInfo tableInfo = new TableInfo();
                    tableInfo.Type = config.Type;

                    // Get IsTable
                    TableAttribute tableAttr = config.Type.GetCustomAttribute<TableAttribute>();
                    if (!config.IsTable.HasValue)
                    {
                        if (tableAttr != null)
                            config.IsTable = tableAttr.IsTable;
                        else
                            config.IsTable = !config.Type.IsAbstract;
                    }

                    // Get InheritTable
                    if (!config.InheritTable.HasValue && tableAttr != null)
                        config.InheritTable = tableAttr.InheritTable;

                    // Get InheritColumns
                    if (config.InheritTable == true)
                    {
                        config.InheritColumns = true;
                    }
                    else if (!config.InheritColumns.HasValue)
                    {
                        if (tableAttr != null && tableAttr.InheritColumnsHasValue)
                            config.InheritColumns = tableAttr.InheritColumns;
                        else
                            config.InheritColumns = config.Type.BaseType.IsAbstract == true;
                    }

                    if ((config.InheritTable == true || config.InheritColumns == true) && parentInfo == null)
                    {
                        throw new InvalidConfigurationException(
                            $"The type \"{config.Type}\" does not have a base type to inherit");
                    }

                    // Get table name
                    if (config.InheritTable == true)
                    {
                        tableInfo.TableName = parentInfo.TableName;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(config.TableName))
                            tableInfo.TableName = tableAttr?.Name ?? TableNameDefault(config.Type);
                        else
                            tableInfo.TableName = config.TableName;
                    }

                    // Get primary keys
                    List<string> primaryKeys = new List<string>();
                    if (config.PrimaryKeys.Count == 0)
                    {
                        config.PrimaryKeys.AddRange(config.Properties.ToDictionary(x => x,
                            x => x.GetCustomAttribute<PrimaryKeyAttribute>())
                            .Where(x => x.Value != null).OrderBy(x => x.Value.Order).Select(x => x.Key.Name));
                    }

                    if (config.PrimaryKeys.Count > 0)
                    {
                        tableInfo.PrimaryKeys = config.PrimaryKeys.ToArray();
                    }
                    else if (parentInfo?.PrimaryKeys != null)
                    {
                        tableInfo.PrimaryKeys = parentInfo.PrimaryKeys;
                    }
                    else
                    {
                        string key = PrimaryKeyDefault(config.Type);
                        if (key != null)
                        {
                            if (config.Type.GetProperty(key) == null)
                            {
                                throw new InvalidConfigurationException($"Type \"{config.Type}\" does not have "
                                    + $"property \"{key}\".");
                            }
                            tableInfo.PrimaryKeys = new string[] { key };
                        }
                        else
                        {
                            tableInfo.PrimaryKeys = Array.Empty<string>();
                        }
                    }

                    // Get ignore
                    config.Ignore.AddRange(config.Properties.Where(x => x.GetCustomAttribute<IgnoreAttribute>() != null)
                        .Select(x => x.Name));
                    config.Ignore = config.Ignore.Distinct().ToList();

                    tables.Add(tableInfo.Type.FullName, tableInfo);
                }
            }

            foreach (var level in levels)
            {
                foreach (ConfigData config in level)
                {
                    // Get parent config
                    TableInfo parentInfo = null;
                    Type parentType = config.Type.BaseType;
                    if (parentType != null && parentType != typeof(object))
                    {
                        tables.TryGetValue(parentType.FullName, out parentInfo);
                    }

                    TableInfo tableInfo = tables[config.Type.FullName];
                    tableInfo.Type = config.Type;

                    // Get columns
                    foreach (PropertyInfo property in config.Properties)
                    {
                        if (typeof(IEnumerable).IsAssignableFrom(property.PropertyType)
                            && property.PropertyType != typeof(string))
                        {
                            continue;
                        }
                        else if (config.ForeingKeys.ContainsKey(property.Name))
                        {
                            foreach (var key in config.ForeingKeys[property.Name])
                            {
                                config.Columns.Add(key.PropertyName);
                                config.ColumnsNames.Add(key.PropertyName, key.ColumnName);
                            }
                        }
                        else if (!Config.ContainsKey(property.PropertyType.FullName))
                        {
                            config.Columns.Add(property.Name);

                            if (!config.ColumnsNames.ContainsKey(property.Name))
                            {
                                ForeignKeyAttribute[] keys = property.GetCustomAttributes<ForeignKeyAttribute>().ToArray();
                                if (keys.Length > 1)
                                {
                                    throw new InvalidConfigurationException($"Invalid multiple foreign key "
                                        + $"for property \"{property.Name}\" in type \"{config.Type}\".");
                                }
                                else if (keys.Length == 1 && !string.IsNullOrEmpty(keys[0].Name))
                                {
                                    config.ColumnsNames.Add(property.Name, keys[0].Name);
                                }
                                else
                                {
                                    ColumnAttribute column = property.GetCustomAttribute<ColumnAttribute>();

                                    config.ColumnsNames.Add(property.Name, !string.IsNullOrEmpty(column?.Name)
                                        ? column.Name : property.Name);
                                }
                            }
                        }
                        else
                        {
                            ForeignKeyAttribute[] keys = property.GetCustomAttributes<ForeignKeyAttribute>().ToArray();

                            if (keys.Length == 0)
                            {
                                TableInfo tableInfoFK = tables[property.PropertyType.FullName];

                                foreach (string keyFk in tableInfoFK.PrimaryKeys)
                                {
                                    string column = property.Name + "." + keyFk;
                                    config.Columns.Add(column);
                                    config.ColumnsNames.Add(column, property.Name + keyFk);
                                }
                            }
                            else if (keys.Length == 1)
                            {
                                TableInfo tableInfoFK = tables[property.PropertyType.FullName];

                                string keyFk = keys[0].PropertyName;
                                if (!string.IsNullOrEmpty(keyFk))
                                {
                                    string column = property.Name + "." + keyFk;
                                    config.Columns.Add(column);
                                    config.ColumnsNames.Add(column, keys[0].Name);
                                }
                                else if (tableInfoFK.PrimaryKeys.Length > 1)
                                {
                                    throw new InvalidConfigurationException($"Foreign key propertyName not specified "
                                        + $"for property \"{property.Name}\" in type \"{config.Type}\" and the "
                                        + $"type {property.PropertyType} have multiple primary keys.");
                                }
                                else if (tableInfoFK.PrimaryKeys.Length == 0)
                                {
                                    throw new InvalidConfigurationException($"Foreign key propertyName not specified "
                                        + $"for property \"{property.Name}\" in type \"{config.Type}\" and the "
                                        + $"type {property.PropertyType} does not have primary key.");
                                }
                                else
                                {
                                    string column = property.Name + "." + tableInfoFK.PrimaryKeys[0];
                                    config.Columns.Add(column);
                                    config.ColumnsNames.Add(column, keys[0].Name);
                                }
                            }
                            else
                            {
                                if (keys.Any(x => string.IsNullOrEmpty(x.PropertyName)))
                                {
                                    throw new InvalidConfigurationException($"Foreign key propertyName not specified "
                                        + $"for property \"{property.Name}\" in type \"{config.Type}\".");
                                }

                                foreach (var key in keys)
                                {
                                    string column = property.Name + "." + key.PropertyName;
                                    config.Columns.Add(column);
                                    config.ColumnsNames.Add(column, !string.IsNullOrEmpty(key.Name) ? key.Name
                                        : property.Name + key.PropertyName);
                                }
                            }
                        }
                    }

                    List<string> columns = new List<string>();
                    if (config.InheritColumns == true)
                    {
                        columns.AddRange(parentInfo.Columns);
                    }
                    columns.AddRange(config.Columns);

                    tableInfo.Columns = tableInfo.PrimaryKeys.Union(columns).Distinct()
                        .Where(x => !config.Ignore.Contains(x)).ToArray();

                    // Get column names
                    IDictionary<string, string> columnsNames = new Dictionary<string, string>();
                    if (parentInfo != null)
                    {
                        foreach (var item in parentInfo.ColumnNamesDic.Where(x => tableInfo.Columns.Contains(x.Key)))
                            columnsNames[item.Key] = item.Value;
                    }

                    foreach (var item in config.ColumnsNames.Where(x => tableInfo.Columns.Contains(x.Key)))
                        columnsNames[item.Key] = item.Value;

                    tableInfo.ColumnNamesDic = columnsNames;
                    tableInfo.ColumnNames = tableInfo.Columns.Select(x => tableInfo.ColumnNamesDic[x])
                        .Distinct().ToArray();
                }
            }

            foreach (TableInfo tableInfo in tables.Where(x => Config[x.Key].IsTable == true).Select(x => x.Value))
            {
                Tables.Add(tableInfo);
                // Register in ExpressionProcessor
                ExpressionProcessor.AddTable(tableInfo.Type);
            }

            return Tables;
        }
    }
}