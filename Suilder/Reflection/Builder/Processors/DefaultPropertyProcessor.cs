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
            var levels = GroupByInheranceLevel(ConfigData.ConfigTypes.Values);

            foreach (var level in levels)
            {
                foreach (TableConfig tableConfig in level)
                {
                    AddProperties(tableConfig.Type, tableConfig);
                }
            }
        }

        /// <summary>
        /// Adds the properties of the type to the configuration.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="tableConfig">The table configuration.</param>
        /// <param name="prefix">A prefix added to the property path for nested properties.</param>
        /// <param name="stack">The stack of nested types.</param>
        protected virtual void AddProperties(Type type, TableConfig tableConfig, string prefix = "",
            Stack<Type> stack = null)
        {
            BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;

            // It is not a nested property
            if (prefix == "")
                flags |= BindingFlags.DeclaredOnly;

            foreach (PropertyInfo propertyInfo in type.GetProperties(flags))
            {
                AddProperty(propertyInfo, tableConfig, prefix, stack);
            }
        }

        /// <summary>
        /// Adds the property to the configuration.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        /// <param name="tableConfig">The table configuration.</param>
        /// <param name="prefix">A prefix added to the property path for nested properties.</param>
        /// <param name="stack">The stack of nested types.</param>
        protected virtual void AddProperty(PropertyInfo propertyInfo, TableConfig tableConfig, string prefix,
            Stack<Type> stack = null)
        {
            PropertyData propertyData = new PropertyData()
            {
                FullName = $"{prefix}{propertyInfo.Name}",
                Info = propertyInfo
            };

            tableConfig.Properties.Add(propertyData);

            // It is another table
            propertyData.IsTable = ConfigData.GetConfig(propertyInfo.PropertyType) != null;
            propertyData.IsNested = IsNested(propertyData, tableConfig);

            if (propertyData.IsTable && propertyData.IsNested)
            {
                throw new InvalidConfigurationException($"The type \"{propertyInfo.PropertyType}\" registered as a table, "
                    + "cannot be marked as nested.");
            }

            propertyData.IsIgnored = IsIgnored(propertyData, tableConfig);

            if (propertyData.IsIgnored)
                tableConfig.Ignore.Add(propertyData.FullName);

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

                AddProperties(propertyInfo.PropertyType, tableConfig, $"{propertyData.FullName}.", stack);

                stack.Pop();
            }
        }

        /// <summary>
        /// Checks if the property is a nested type.
        /// </summary>
        /// <param name="propertyData">The property data.</param>
        /// <param name="tableConfig">The table configuration.</param>
        /// <returns><see langword="true"/> if the property is a nested type, otherwise, <see langword="false"/>.</returns>
        protected virtual bool IsNested(PropertyData propertyData, TableConfig tableConfig)
        {
            return ConfigData.NestedTypes.Contains(propertyData.Info.PropertyType.FullName)
                || propertyData.Info.PropertyType.GetCustomAttribute<NestedAttribute>() != null;
        }

        /// <summary>
        /// Checks if the property must be ignored.
        /// </summary>
        /// <param name="propertyData">The property data.</param>
        /// <param name="tableConfig">The table configuration.</param>
        /// <returns><see langword="true"/> if the property is ignored, otherwise, <see langword="false"/>.</returns>
        protected virtual bool IsIgnored(PropertyData propertyData, TableConfig tableConfig)
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