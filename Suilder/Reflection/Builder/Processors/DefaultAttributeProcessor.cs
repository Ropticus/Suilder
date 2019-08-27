using System.Linq;
using System.Reflection;
using Suilder.Exceptions;
using static Suilder.Reflection.Builder.TableConfig;

namespace Suilder.Reflection.Builder.Processors
{
    /// <summary>
    /// The default attribute processor.
    /// <para>It reads and loads the configuration of the attributes.</para>
    /// </summary>
    public class DefaultAttributeProcessor : BaseConfigProcessor, IAttributeProcessor
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
                    LoadTableAttribute(tableConfig);

                    LoadPrimaryKeyAttribute(tableConfig);

                    LoadColumnAttribute(tableConfig);

                    LoadForeignKeyAttribute(tableConfig);
                }
            }
        }

        /// <summary>
        /// Loads the <see cref="TableAttribute"/>.
        /// </summary>
        /// <param name="tableConfig">The table configuration.</param>
        protected virtual void LoadTableAttribute(TableConfig tableConfig)
        {
            TableAttribute tableAttr = tableConfig.Type.GetCustomAttribute<TableAttribute>();

            if (tableAttr == null)
                return;

            // Get IsTable
            if (!tableConfig.IsTable.HasValue)
                tableConfig.IsTable = tableAttr.IsTable;

            // Get InheritTable
            if (!tableConfig.InheritTable.HasValue && tableAttr.InheritTableHasValue)
                tableConfig.InheritTable = tableAttr.InheritTable;

            // Get InheritColumns
            if (!tableConfig.InheritColumns.HasValue && tableAttr.InheritColumnsHasValue)
                tableConfig.InheritColumns = tableAttr.InheritColumns;

            if (string.IsNullOrEmpty(tableConfig.TableName))
                tableConfig.TableName = tableAttr.Name;
        }

        /// <summary>
        /// Loads the <see cref="PrimaryKeyAttribute"/>.
        /// </summary>
        /// <param name="tableConfig">The table configuration.</param>
        protected virtual void LoadPrimaryKeyAttribute(TableConfig tableConfig)
        {
            if (tableConfig.PrimaryKeys.Count == 0)
            {
                tableConfig.PrimaryKeys = tableConfig.Properties
                    .Where(x => !x.IsIgnored && x.IsColumn)
                    .ToDictionary(x => x, x => x.Info.GetCustomAttribute<PrimaryKeyAttribute>())
                    .Where(x => x.Value != null).OrderBy(x => x.Value.Order).Select(x => x.Key.FullName).ToList();
            }
        }

        /// <summary>
        /// Loads the <see cref="ColumnAttribute"/>.
        /// </summary>
        /// <param name="tableConfig">The table configuration.</param>
        protected virtual void LoadColumnAttribute(TableConfig tableConfig)
        {
            foreach (PropertyData property in tableConfig.Properties.Where(x => !x.IsIgnored))
            {
                if (!tableConfig.ColumnNames.ContainsKey(property.FullName))
                {
                    ColumnAttribute columnAttr = property.Info.GetCustomAttribute<ColumnAttribute>();

                    if (columnAttr != null)
                        tableConfig.ColumnNames.Add(property.FullName, columnAttr.Name);
                }
            }
        }

        /// <summary>
        /// Loads the <see cref="ForeignKeyAttribute"/>.
        /// </summary>
        /// <param name="tableConfig">The table configuration.</param>
        protected virtual void LoadForeignKeyAttribute(TableConfig tableConfig)
        {
            foreach (PropertyData property in tableConfig.Properties.Where(x => !x.IsIgnored && !x.IsNested))
            {
                if (property.IsColumn)
                {
                    ForeignKeyAttribute[] keys = property.Info.GetCustomAttributes<ForeignKeyAttribute>().ToArray();
                    if (keys.Length > 0)
                    {
                        tableConfig.ForeignKeys.Add(property.FullName);

                        if (!tableConfig.ColumnNames.ContainsKey(property.FullName)
                            && keys.Any(x => !string.IsNullOrEmpty(x.Name)))
                        {
                            if (keys.Length > 1)
                            {
                                throw new InvalidConfigurationException($"Invalid multiple foreign key for property "
                                    + $"\"{property.FullName}\" of the type \"{tableConfig.Type}\".");
                            }
                            else
                            {
                                tableConfig.ColumnNames.Add(property.FullName, keys[0].Name);
                            }
                        }
                    }
                }
                // If there are not other properties
                else if (!tableConfig.ForeignKeys.Where(x => x.StartsWith($"{property.FullName}.")).Any())
                {
                    ForeignKeyAttribute[] keys = property.Info.GetCustomAttributes<ForeignKeyAttribute>().ToArray();
                    if (keys.Length > 1 && keys.Any(x => string.IsNullOrEmpty(x.PropertyName)))
                    {
                        throw new InvalidConfigurationException($"Empty property name in multiple foreign key "
                            + $"for property \"{property.FullName}\" of the type \"{tableConfig.Type}\".");
                    }

                    foreach (ForeignKeyAttribute foreignKeyAttr in keys)
                    {
                        string column = !string.IsNullOrEmpty(foreignKeyAttr.PropertyName)
                            ? $"{property.FullName}.{foreignKeyAttr.PropertyName}" : property.FullName;

                        tableConfig.ForeignKeys.Add(column);

                        if (!string.IsNullOrEmpty(foreignKeyAttr.Name) && !tableConfig.ColumnNames.ContainsKey(column))
                        {
                            tableConfig.ColumnNames.Add(column, foreignKeyAttr.Name);
                        }
                    }
                }
            }
        }
    }
}