using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Suilder.Builder;
using Suilder.Exceptions;
using static Suilder.Reflection.Builder.TableConfig;

namespace Suilder.Reflection.Builder.Processors
{
    /// <summary>
    /// The default configuration processor.
    /// <para>It processes the configuration of the tables.</para>
    /// <para>This does not include metadata.</para>
    /// </summary>
    public class DefaultConfigProcessor : BaseConfigProcessor, IConfigProcessor
    {
        /// <summary>
        /// Process the configuration.
        /// </summary>
        protected override void ProcessData()
        {
            var levels = GroupByInheritanceLevel(ConfigData.ConfigTypes.Values);

            foreach (var level in levels)
            {
                foreach (TableConfig tableConfig in level)
                {
                    TableInfo tableInfo = ResultData.GetConfig(tableConfig.Type);
                    TableInfo parentInfo = ResultData.GetParentConfig(tableConfig.Type);

                    LoadInheritConfig(tableConfig, tableInfo, parentInfo);

                    LoadTableName(tableConfig, tableInfo, parentInfo);

                    LoadPrimaryKeys(tableConfig, tableInfo, parentInfo);
                }
            }

            foreach (var level in levels)
            {
                foreach (TableConfig tableConfig in level)
                {
                    TableInfo tableInfo = ResultData.GetConfig(tableConfig.Type);
                    TableInfo parentInfo = ResultData.GetParentConfig(tableConfig.Type);

                    LoadColumns(tableConfig, tableInfo, parentInfo);

                    LoadForeignKeys(tableConfig, tableInfo, parentInfo);

                    LoadColumnNames(tableConfig, tableInfo, parentInfo);
                }
            }
        }

        /// <summary>
        /// Loads the inherit configuration.
        /// </summary>
        /// <param name="tableConfig">The table configuration.</param>
        /// <param name="tableInfo">The table information.</param>
        /// <param name="parentInfo">The parent class table information.</param>
        protected virtual void LoadInheritConfig(TableConfig tableConfig, TableInfo tableInfo, TableInfo parentInfo)
        {
            // Get IsTable
            if (!tableConfig.IsTable.HasValue)
                tableConfig.IsTable = !tableConfig.Type.IsAbstract;

            // Get InheritTable
            if (!tableConfig.InheritTable.HasValue)
                tableConfig.InheritTable = ConfigData.InheritTableDefault(tableConfig.Type);

            // Get InheritColumns
            if (tableConfig.InheritTable == true)
            {
                tableConfig.InheritColumns = true;
            }
            else if (!tableConfig.InheritColumns.HasValue)
            {
                tableConfig.InheritColumns = ConfigData.InheritColumnsDefault(tableConfig.Type);
            }

            if (tableConfig.InheritColumns == true && parentInfo == null)
            {
                throw new InvalidConfigurationException(
                    $"The type \"{tableConfig.Type}\" does not have a base type to inherit.");
            }
        }

        /// <summary>
        /// Loads the table name.
        /// </summary>
        /// <param name="tableConfig">The table configuration.</param>
        /// <param name="tableInfo">The table information.</param>
        /// <param name="parentInfo">The parent class table information.</param>
        protected virtual void LoadTableName(TableConfig tableConfig, TableInfo tableInfo, TableInfo parentInfo)
        {
            if (tableConfig.InheritTable == true)
            {
                tableInfo.Schema = parentInfo.Schema;
                tableInfo.TableName = parentInfo.TableName;
            }
            else
            {
                if (!string.IsNullOrEmpty(tableConfig.Schema))
                    tableInfo.Schema = tableConfig.Schema;
                else
                    tableInfo.Schema = ConfigData.SchemaDefault(tableConfig.Type);

                if (!string.IsNullOrEmpty(tableConfig.TableName))
                    tableInfo.TableName = tableConfig.TableName;
                else
                    tableInfo.TableName = ConfigData.TableNameDefault(tableConfig.Type);
            }
        }

        /// <summary>
        /// Loads the primary keys.
        /// </summary>
        /// <param name="tableConfig">The table configuration.</param>
        /// <param name="tableInfo">The table information.</param>
        /// <param name="parentInfo">The parent class table information.</param>
        protected virtual void LoadPrimaryKeys(TableConfig tableConfig, TableInfo tableInfo, TableInfo parentInfo)
        {
            foreach (string primaryKey in tableConfig.PrimaryKeys.Distinct().Where(x => !IsIgnored(tableConfig, x)))
            {
                tableInfo.PrimaryKeys.Add(primaryKey);
            }

            if (tableInfo.PrimaryKeys.Count > 0)
                return;

            if (parentInfo != null && parentInfo.PrimaryKeys.Count > 0)
            {
                foreach (string primaryKey in parentInfo.PrimaryKeys.Where(x => !IsIgnored(tableConfig, x)))
                {
                    tableInfo.PrimaryKeys.Add(primaryKey);
                }
            }
            else
            {
                string primaryKey = ConfigData.PrimaryKeyDefault(tableConfig.Type);
                if (!string.IsNullOrEmpty(primaryKey))
                {
                    if (ExpressionProcessor.GetProperty(tableConfig.Type, primaryKey) == null)
                    {
                        throw new InvalidConfigurationException($"The type \"{tableConfig.Type}\" does not have "
                            + $"property \"{primaryKey}\".");
                    }

                    if (!IsIgnored(tableConfig, primaryKey))
                        tableInfo.PrimaryKeys.Add(primaryKey);
                }
            }
        }

        /// <summary>
        /// Loads the columns.
        /// </summary>
        /// <param name="tableConfig">The table configuration.</param>
        /// <param name="tableInfo">The table information.</param>
        /// <param name="parentInfo">The parent class table information.</param>
        protected virtual void LoadColumns(TableConfig tableConfig, TableInfo tableInfo, TableInfo parentInfo)
        {
            foreach (PropertyData property in tableConfig.Properties.Where(x => !x.IsIgnored && !x.IsNested))
            {
                if (property.IsColumn)
                {
                    tableInfo.Columns.Add(property.FullName);
                    tableConfig.ColumnProperties.Add(property.FullName, property);
                }
                else // It is another table
                {
                    string[] columns = (tableConfig.InheritColumns != true ? Enumerable.Empty<string>()
                            : parentInfo.ForeignKeys.Where(x => x.StartsWith($"{property.FullName}.")))
                        .Union(tableConfig.ForeignKeys.Where(x => x.StartsWith($"{property.FullName}."))).ToArray();

                    if (columns.Length > 0)
                    {
                        foreach (PropertyData tableProperty in columns.Where(x => !IsIgnored(tableConfig, x))
                            .Select(x => GetTableProperty(property, x)))
                        {
                            tableInfo.Columns.Add(tableProperty.FullName);
                            tableConfig.ColumnProperties.Add(tableProperty.FullName, tableProperty);
                        }
                    }
                    else
                    {
                        TableInfo tableInfoFK = ResultData.GetConfig(property.Info.PropertyType);

                        if (tableInfoFK.PrimaryKeys.Count == 0)
                        {
                            throw new InvalidConfigurationException($"Foreign key property not specified "
                                + $"for property \"{property.FullName}\" of the type \"{tableConfig.Type}\", and the "
                                + $"type \"{property.Info.PropertyType}\" does not have a primary key.");
                        }

                        // Add all primary keys
                        foreach (PropertyData tableProperty in tableInfoFK.PrimaryKeys
                            .Select(x => $"{property.FullName}.{x}")
                            .Where(x => !IsIgnored(tableConfig, x)).Select(x => GetTableProperty(property, x)))
                        {
                            tableInfo.Columns.Add(tableProperty.FullName);
                            tableConfig.ForeignKeys.Add(tableProperty.FullName);
                            tableConfig.ColumnProperties.Add(tableProperty.FullName, tableProperty);
                        }
                    }
                }
            }

            // Add parent columns
            if (parentInfo != null)
            {
                tableInfo.Columns = (tableConfig.InheritColumns == true ? parentInfo.Columns
                    : parentInfo.Columns.Where(x => tableInfo.PrimaryKeys.Contains(x)))
                    .Where(x => !IsIgnored(tableConfig, x))
                    .Union(tableInfo.Columns).ToList();

                TableConfig parentConfig = ConfigData.GetParentConfig(tableConfig.Type);

                foreach (string column in tableInfo.Columns.Where(x => !tableConfig.ColumnProperties.ContainsKey(x)))
                {
                    tableConfig.ColumnProperties.Add(column, parentConfig.ColumnProperties[column]);
                }
            }

            foreach (string primaryKey in tableInfo.PrimaryKeys)
            {
                if (!tableInfo.Columns.Contains(primaryKey))
                {
                    throw new InvalidConfigurationException($"Primary key does not exists as a column "
                        + $"for property \"{primaryKey}\" of the type \"{tableConfig.Type}\".");
                }
            }

            // Add primary keys first
            tableInfo.Columns = tableInfo.PrimaryKeys.Union(tableInfo.Columns).ToList();
        }

        /// <summary>
        /// Gets the property of another table.
        /// </summary>
        /// <param name="parent">The parent property.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>The property of another table.</returns>
        protected virtual PropertyData GetTableProperty(PropertyData parent, string propertyName)
        {
            foreach (string property in propertyName.Substring(parent.FullName.Length + 1).Split('.'))
            {
                PropertyInfo propertyInfo = parent.Info.PropertyType.GetProperty(property);

                PropertyData propertyData = new PropertyData
                {
                    Type = parent.Type,
                    FullName = $"{parent.FullName}.{propertyInfo.Name}",
                    Info = propertyInfo,
                    Parent = parent
                };

                parent = propertyData;
            }

            parent.IsColumn = true;
            return parent;
        }

        /// <summary>
        /// Loads the foreign keys.
        /// </summary>
        /// <param name="tableConfig">The table configuration.</param>
        /// <param name="tableInfo">The table information.</param>
        /// <param name="parentInfo">The parent class table information.</param>
        protected virtual void LoadForeignKeys(TableConfig tableConfig, TableInfo tableInfo, TableInfo parentInfo)
        {
            foreach (string column in tableInfo.Columns)
            {
                if (tableConfig.ForeignKeys.Contains(column)
                    || (parentInfo != null && parentInfo.ForeignKeys.Contains(column)))
                {
                    tableInfo.ForeignKeys.Add(column);
                }
            }
        }

        /// <summary>
        /// Loads the column names.
        /// </summary>
        /// <param name="tableConfig">The table configuration.</param>
        /// <param name="tableInfo">The table information.</param>
        /// <param name="parentInfo">The parent class table information.</param>
        protected virtual void LoadColumnNames(TableConfig tableConfig, TableInfo tableInfo, TableInfo parentInfo)
        {
            foreach (var table in tableConfig.ColumnProperties.Values
                .GroupBy(p => GetParentProperties(p).Where(x => x.IsTable).Select(x => x.FullName).FirstOrDefault())
                .Where(x => x.Key != null))
            {
                // If the property has a column name
                if (table.Any(x => !tableConfig.ColumnNames.ContainsKey(x.FullName))
                    && tableConfig.ColumnNames.TryGetValue(table.Key, out string columnName))
                {
                    if (table.Count() > 1)
                    {
                        throw new InvalidConfigurationException($"Foreign key property not specified for column name "
                            + $"of property \"{table.Key}\" of the type \"{tableConfig.Type}\", and the "
                            + $"property have multiple foreign keys.");
                    }

                    // Change column name
                    tableConfig.ColumnNames.Remove(table.Key);
                    tableConfig.ColumnNames.Add(table.First().FullName, columnName);

                    if (tableConfig.PartialNames.Remove(table.Key))
                    {
                        tableConfig.PartialNames.Add(table.First().FullName);
                    }
                }
            }

            foreach (string column in tableInfo.Columns)
            {
                PropertyData property = tableConfig.ColumnProperties[column];

                if (tableConfig.ColumnNames.TryGetValue(column, out string columnName))
                {
                    if (property.Parent == null || !tableConfig.PartialNames.Contains(property.FullName))
                    {
                        tableInfo.ColumnNamesDic.Add(column, columnName);
                        continue;
                    }
                }

                if (property.Parent != null)
                {
                    bool hasName = columnName != null;
                    List<string> columnNames = new List<string> { columnName };

                    foreach (PropertyData parent in GetParentProperties(property))
                    {
                        if (!parent.IsNested)
                        {
                            columnNames.Add("");
                            continue;
                        }

                        if (tableConfig.ColumnNames.TryGetValue(parent.FullName, out columnName))
                        {
                            hasName = true;
                            columnNames.Add(columnName);

                            if (!tableConfig.PartialNames.Contains(parent.FullName))
                                break;
                        }
                        else
                        {
                            columnNames.Add(null);
                        }
                    }

                    if (hasName)
                    {
                        IReadOnlyList<PropertyInfo> properties = null;
                        columnNames.Reverse();

                        for (int i = 0; i < columnNames.Count; i++)
                        {
                            if (columnNames[i] == null)
                            {
                                if (properties == null)
                                {
                                    properties = Array.AsReadOnly(GetProperties(property)
                                        .Select(x => x.Info).Reverse().ToArray());
                                }

                                columnNames[i] = ConfigData.ColumnNameDefault(tableConfig.Type, properties,
                                    i + properties.Count - columnNames.Count);
                            }
                        }

                        tableInfo.ColumnNamesDic.Add(column, string.Join("", columnNames));
                        continue;
                    }
                }

                if (parentInfo != null && parentInfo.ColumnNamesDic.TryGetValue(column, out columnName))
                {
                    tableInfo.ColumnNamesDic.Add(column, columnName);
                }
                else
                {
                    IReadOnlyList<PropertyInfo> properties = Array.AsReadOnly(GetProperties(property)
                        .Select(x => x.Info).Reverse().ToArray());

                    tableInfo.ColumnNamesDic.Add(column, string.Join("", properties
                        .Select((x, i) => ConfigData.ColumnNameDefault(tableConfig.Type, properties, i))));
                }
            }

            tableInfo.ColumnNames = tableInfo.Columns.Select(x => tableInfo.ColumnNamesDic[x]).Distinct().ToList();
        }

        /// <summary>
        /// Checks if the property must be ignored.
        /// </summary>
        /// <param name="tableConfig">The table configuration.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns><see langword="true"/> if the property is ignored, otherwise, <see langword="false"/>.</returns>
        protected virtual bool IsIgnored(TableConfig tableConfig, string propertyName)
        {
            return tableConfig.Ignore.Contains(propertyName)
                || tableConfig.Ignore.Any(x => propertyName.StartsWith($"{x}."));
        }
    }
}