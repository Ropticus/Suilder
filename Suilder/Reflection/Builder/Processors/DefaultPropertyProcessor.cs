using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Suilder.Exceptions;
using static Suilder.Reflection.Builder.TableConfig;

namespace Suilder.Reflection.Builder.Processors
{
    /// <summary>
    /// The default property processor.
    /// <para>It reads and loads the properties of all registered types.</para>
    /// </summary>
    public class DefaultPropertyProcessor : BaseConfigProcessor, IConfigProcessor
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
                    AddProperties(tableConfig);

                    AddIgnore(tableConfig);
                }
            }
        }

        /// <summary>
        /// Adds the properties of the type to the configuration.
        /// </summary>
        /// <param name="tableConfig">The table configuration.</param>
        protected virtual void AddProperties(TableConfig tableConfig)
        {
            BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly;

            foreach (PropertyInfo propertyInfo in tableConfig.Type.GetProperties(flags).OrderBy(x => x.MetadataToken))
            {
                AddProperty(tableConfig, propertyInfo, null, null);
            }
        }

        /// <summary>
        /// Adds the properties of the type to the configuration.
        /// </summary>
        /// <param name="tableConfig">The table configuration.</param>
        /// <param name="type">The type.</param>
        /// <param name="parent">The parent property.</param>
        /// <param name="stack">The stack of nested types.</param>
        protected virtual void AddProperties(TableConfig tableConfig, Type type, PropertyData parent, Stack<Type> stack)
        {
            BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;

            foreach (PropertyInfo propertyInfo in type.GetProperties(flags).OrderBy(x => x.MetadataToken))
            {
                AddProperty(tableConfig, propertyInfo, parent, stack);
            }
        }

        /// <summary>
        /// Adds the property to the configuration.
        /// </summary>
        /// <param name="tableConfig">The table configuration.</param>
        /// <param name="propertyInfo">The property info.</param>
        /// <param name="parent">The parent property.</param>
        /// <param name="stack">The stack of nested types.</param>
        protected virtual void AddProperty(TableConfig tableConfig, PropertyInfo propertyInfo, PropertyData parent,
            Stack<Type> stack = null)
        {
            PropertyData propertyData = new PropertyData
            {
                Type = tableConfig.Type,
                FullName = parent != null ? $"{parent.FullName}.{propertyInfo.Name}" : propertyInfo.Name,
                Info = propertyInfo,
                Parent = parent
            };

            tableConfig.Properties.Add(propertyData);

            propertyData.IsTable = ConfigData.GetConfig(propertyInfo.PropertyType) != null;
            propertyData.IsNested = IsNested(tableConfig, propertyData);
            propertyData.IsColumn = !propertyData.IsTable && !propertyData.IsNested;
            propertyData.IsIgnored = IsIgnored(tableConfig, propertyData);

            if (propertyData.IsTable && propertyData.IsNested)
            {
                throw new InvalidConfigurationException($"The type \"{propertyInfo.PropertyType}\" registered as a table, "
                    + "cannot be marked as nested.");
            }

            if (!propertyData.IsIgnored && propertyData.IsNested)
            {
                if (stack == null)
                {
                    stack = new Stack<Type>();
                }
                else if (stack.Contains(propertyInfo.PropertyType))
                {
                    throw new InvalidConfigurationException($"The type \"{tableConfig.Type}\" has nested types with "
                        + $"circular references that must be removed or ignored: \"{propertyData.FullName}\".");
                }

                stack.Push(propertyInfo.PropertyType);

                AddProperties(tableConfig, propertyInfo.PropertyType, propertyData, stack);

                stack.Pop();
            }
        }

        /// <summary>
        /// Adds the ignore configuration.
        /// </summary>
        /// <param name="tableConfig">The table configuration.</param>
        protected virtual void AddIgnore(TableConfig tableConfig)
        {
            foreach (PropertyData propertyData in tableConfig.Properties.Where(x => x.IsIgnored))
            {
                tableConfig.Ignore.Add(propertyData.FullName);
            }

            TableConfig parentConfig = ConfigData.GetParentConfig(tableConfig.Type);

            if (parentConfig != null)
            {
                foreach (string ignore in parentConfig.Ignore)
                {
                    if (!tableConfig.Properties.Any(x => ignore.StartsWith($"{x.FullName}.")))
                    {
                        tableConfig.Ignore.Add(ignore);
                    }
                }
            }
        }

        /// <summary>
        /// Checks if the property is a nested type.
        /// </summary>
        /// <param name="tableConfig">The table configuration.</param>
        /// <param name="propertyData">The property data.</param>
        /// <returns><see langword="true"/> if the property is a nested type, otherwise, <see langword="false"/>.</returns>
        protected virtual bool IsNested(TableConfig tableConfig, PropertyData propertyData)
        {
            return ConfigData.NestedTypes.Contains(propertyData.Info.PropertyType.FullName)
                || propertyData.Info.PropertyType.GetCustomAttribute<NestedAttribute>() != null;
        }

        /// <summary>
        /// Checks if the property must be ignored.
        /// </summary>
        /// <param name="tableConfig">The table configuration.</param>
        /// <param name="propertyData">The property data.</param>
        /// <returns><see langword="true"/> if the property is ignored, otherwise, <see langword="false"/>.</returns>
        protected virtual bool IsIgnored(TableConfig tableConfig, PropertyData propertyData)
        {
            // Ignore properties without a getter and setter
            if (!propertyData.Info.CanRead || !propertyData.Info.CanWrite)
                return true;

            Type propertyType = propertyData.Info.PropertyType;

            // Ignore lists of tables
            if (propertyType.IsGenericType && typeof(IEnumerable).IsAssignableFrom(propertyType)
                && propertyType.GenericTypeArguments.Any(x => ConfigData.GetConfig(x) != null))
            {
                return true;
            }

            return tableConfig.Ignore.Contains(propertyData.FullName)
                || (ConfigData.EnableAttributes && propertyData.Info.GetCustomAttribute<IgnoreAttribute>() != null);
        }
    }
}