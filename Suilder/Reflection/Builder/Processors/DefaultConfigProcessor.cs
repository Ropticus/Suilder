using System.Collections.Generic;
using System.Linq;
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
            var levels = GroupByInheranceLevel(ConfigData.ConfigTypes.Values);

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

            if ((tableConfig.InheritTable == true || tableConfig.InheritColumns == true) && parentInfo == null)
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
                tableInfo.TableName = parentInfo.TableName;
            }
            else if (!string.IsNullOrEmpty(tableConfig.TableName))
            {
                tableInfo.TableName = tableConfig.TableName;
            }
            else
            {
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
            if (tableConfig.PrimaryKeys.Count > 0)
            {
                foreach (var item in tableConfig.PrimaryKeys.Distinct().Where(x => !IsIgnored(x, tableConfig)))
                    tableInfo.PrimaryKeys.Add(item);
            }
            else if (parentInfo != null && parentInfo.PrimaryKeys.Count > 0)
            {
                foreach (var item in parentInfo.PrimaryKeys.Where(x => !IsIgnored(x, tableConfig)))
                    tableInfo.PrimaryKeys.Add(item);
            }
            else
            {
                string primaryKey = ConfigData.PrimaryKeyDefault(tableConfig.Type);
                if (!string.IsNullOrEmpty(primaryKey) && !IsIgnored(primaryKey, tableConfig))
                {
                    if (ExpressionProcessor.GetProperty(tableConfig.Type, primaryKey) == null)
                    {
                        throw new InvalidConfigurationException($"The type \"{tableConfig.Type}\" does not have "
                            + $"property \"{primaryKey}\".");
                    }
                    tableInfo.PrimaryKeys.Add(primaryKey);
                }
            }
        }

        /// <summary>
        /// Loads the columns, foreign keys and column names.
        /// </summary>
        /// <param name="tableConfig">The table configuration.</param>
        /// <param name="tableInfo">The table information.</param>
        /// <param name="parentInfo">The parent class table information.</param>
        protected virtual void LoadColumns(TableConfig tableConfig, TableInfo tableInfo, TableInfo parentInfo)
        {
            tableInfo.ColumnNamesDic = new Dictionary<string, string>(tableConfig.ColumnNames);

            foreach (PropertyData property in tableConfig.Properties.Where(x => !x.IsIgnored && !x.IsNested))
            {
                if (property.IsColumn)
                {
                    tableInfo.Columns.Add(property.FullName);

                    if (tableConfig.ForeignKeys.Contains(property.FullName))
                        tableInfo.ForeignKeys.Add(property.FullName);
                }
                else // It is another table
                {
                    TableInfo tableInfoFK = ResultData.GetConfig(property.Info.PropertyType);
                    string[] keys = tableConfig.ForeignKeys.Where(x => x.StartsWith($"{property.FullName}.")).ToArray();

                    if (keys.Length > 0)
                    {
                        foreach (string column in keys.Where(x => !IsIgnored(x, tableConfig)))
                        {
                            tableInfo.Columns.Add(column);
                            tableInfo.ForeignKeys.Add(column);
                        }
                    }
                    else
                    {
                        if (tableInfoFK.PrimaryKeys.Count == 0)
                        {
                            throw new InvalidConfigurationException($"Foreign key property not specified "
                                + $"for property \"{property.FullName}\" of the type \"{tableConfig.Type}\", and the "
                                + $"type \"{property.Info.PropertyType}\" does not have a primary key.");
                        }

                        List<string> columns = new List<string>(tableInfoFK.PrimaryKeys.Count);
                        // Add all primary keys
                        foreach (string primaryKey in tableInfoFK.PrimaryKeys)
                        {
                            string column = $"{property.FullName}.{primaryKey}";

                            if (!IsIgnored(column, tableConfig))
                            {
                                columns.Add(column);
                                tableInfo.Columns.Add(column);
                                tableInfo.ForeignKeys.Add(column);
                            }
                        }

                        // If the property has a column name
                        if (tableConfig.ColumnNames.TryGetValue(property.FullName, out string columnName))
                        {
                            if (columns.Count > 1)
                            {
                                throw new InvalidConfigurationException($"Foreign key property not specified "
                                    + $"for property \"{property.FullName}\" of the type \"{tableConfig.Type}\", and the "
                                    + $"type \"{property.Info.PropertyType}\" have multiple primary keys.");
                            }

                            string column = columns[0];
                            // Change column name
                            tableConfig.ColumnNames.Remove(property.FullName);
                            tableConfig.ColumnNames.Add(column, columnName);
                        }
                    }
                }
            }

            // Add parent columns
            if (tableConfig.InheritColumns == true)
            {
                tableInfo.Columns = parentInfo.Columns.Where(x => !IsIgnored(x, tableConfig))
                    .Union(tableInfo.Columns).ToList();
            }

            // Add primary keys first
            tableInfo.Columns = tableInfo.PrimaryKeys.Union(tableInfo.Columns).Distinct().ToList();

            // Add column names
            foreach (string column in tableInfo.Columns)
            {
                if (tableConfig.ColumnNames.TryGetValue(column, out string columnName))
                {
                    tableInfo.ColumnNamesDic[column] = columnName;
                }
                else
                {
                    var columnsSplit = column.Split('.');
                    for (int i = columnsSplit.Length - 1; i > 0; i--)
                    {
                        string key = string.Join(".", columnsSplit.Take(i));
                        if (tableConfig.ColumnNames.TryGetValue(key, out columnName))
                        {
                            tableInfo.ColumnNamesDic[column] = $"{columnName}{string.Join("", columnsSplit.Skip(i))}";
                            break;
                        }
                    }
                }
            }

            // Add parent column names and foreign keys
            if (parentInfo != null)
            {
                foreach (var item in parentInfo.ColumnNamesDic)
                {
                    if (!tableInfo.ColumnNamesDic.ContainsKey(item.Key) && tableInfo.Columns.Contains(item.Key))
                        tableInfo.ColumnNamesDic.Add(item.Key, item.Value);
                }

                foreach (var foreignKey in parentInfo.ForeignKeys)
                {
                    if (tableInfo.Columns.Contains(foreignKey))
                        tableInfo.ForeignKeys.Add(foreignKey);
                }
            }

            // Add default column names
            foreach (string column in tableInfo.Columns)
            {
                if (!tableInfo.ColumnNamesDic.ContainsKey(column))
                    tableInfo.ColumnNamesDic.Add(column, column.Replace(".", ""));
            }

            tableInfo.ColumnNamesDic = tableInfo.Columns.ToDictionary(x => x, x => tableInfo.ColumnNamesDic[x]);
            tableInfo.ColumnNames = tableInfo.Columns.Select(x => tableInfo.ColumnNamesDic[x]).Distinct().ToArray();
        }

        /// <summary>
        /// Checks if the property must be ignored.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <param name="tableConfig">The table configuration.</param>
        /// <returns><see langword="true"/> if the property is ignored, otherwise, <see langword="false"/>.</returns>
        protected virtual bool IsIgnored(string propertyName, TableConfig tableConfig)
        {
            return tableConfig.Ignore.Contains(propertyName)
                || tableConfig.Ignore.Any(x => propertyName.StartsWith($"{x}."));
        }
    }
}