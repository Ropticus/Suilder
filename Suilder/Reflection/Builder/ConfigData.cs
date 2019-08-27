using System;
using System.Collections.Generic;
using Suilder.Exceptions;

namespace Suilder.Reflection.Builder
{
    /// <summary>
    /// The configuration data.
    /// </summary>
    public class ConfigData
    {
        /// <summary>
        /// If attributes must be enabled.
        /// </summary>
        /// <value>If the attributes must be enabled.</value>
        public bool EnableAttributes { get; set; } = true;

        /// <summary>
        /// If the metadata must be enabled.
        /// </summary>
        /// <value>If the metadata must be enabled.</value>
        public bool EnableMetadata { get; set; } = true;

        /// <summary>
        /// Delegate to get the default "InheritTable" value.
        /// </summary>
        /// <value>Delegate to get the default "InheritTable" value.</value>
        public Func<Type, bool> InheritTableDefault { get; set; } = type => false;

        /// <summary>
        /// Delegate to get the default "InheritColumns" value.
        /// </summary>
        /// <value>Delegate to get the default "InheritColumns" value.</value>
        public Func<Type, bool> InheritColumnsDefault { get; set; } = type => type.BaseType.IsAbstract;

        /// <summary>
        /// Delegate to get the default table name.
        /// </summary>
        /// <value>Delegate to get the default table name.</value>
        public Func<Type, string> TableNameDefault { get; set; } = type => type.Name;

        /// <summary>
        /// Delegate to get the default primary key.
        /// </summary>
        /// <value>Delegate to get the default primary key.</value>
        public Func<Type, string> PrimaryKeyDefault { get; set; } = type => type.GetProperty("Id")?.Name;

        /// <summary>
        /// The configuration of registered types.
        /// </summary>
        protected Dictionary<string, TableConfig> configTypes = new Dictionary<string, TableConfig>();

        /// <summary>
        /// The configuration of registered types.
        /// </summary>
        /// <value>The configuration of registered types.</value>
        public IReadOnlyDictionary<string, TableConfig> ConfigTypes => configTypes;

        /// <summary>
        /// The registered nested types.
        /// </summary>
        /// <returns>The registered nested types.</returns>
        protected HashSet<string> nestedTypes = new HashSet<string>();

        /// <summary>
        /// The registered nested types.
        /// </summary>
        public IReadOnlyCollection<string> NestedTypes => nestedTypes;

        /// <summary>
        /// Gets or creates the configuration of a type.
        /// <para>It also creates the configuration of all base types.</para>
        /// </summary>
        /// <param name="type">The type to get the configuration.</param>
        /// <returns>The configuration.</returns>
        public TableConfig GetConfigOrDefault(Type type)
        {
            ConfigTypes.TryGetValue(type.FullName, out TableConfig config);

            if (config == null)
            {
                config = new TableConfig(type);

                // Add parent configuration
                if (type.BaseType != typeof(object))
                    GetConfigOrDefault(type.BaseType);

                configTypes.Add(type.FullName, config);
            }

            return config;
        }

        /// <summary>
        /// Gets the configuration of a type.
        /// </summary>
        /// <param name="type">The type to get the configuration.</param>
        /// <returns>The configuration.</returns>
        public TableConfig GetConfig(Type type)
        {
            ConfigTypes.TryGetValue(type.FullName, out TableConfig config);
            return config;
        }

        /// <summary>
        /// Gets the parent configuration of a type.
        /// </summary>
        /// <param name="type">The type to get the parent configuration.</param>
        /// <returns>The configuration.</returns>
        public TableConfig GetParentConfig(Type type)
        {
            ConfigTypes.TryGetValue(type.BaseType.FullName, out TableConfig config);
            return config;
        }

        /// <summary>
        /// Removes the configuration of a type.
        /// </summary>
        /// <param name="type">The type to remove.</param>
        public void RemoveConfig(Type type)
        {
            configTypes.Remove(type.FullName);
        }

        /// <summary>
        /// Registers a type as nested.
        /// </summary>
        /// <param name="type">The type to register.</param>
        public void AddNested(Type type)
        {
            Type parentType = type.BaseType;
            if (parentType == null)
                throw new InvalidConfigurationException($"Invalid type \"{type}\".");

            nestedTypes.Add(type.FullName);
        }

        /// <summary>
        /// Removes a nested type.
        /// </summary>
        /// <param name="type">The type to remove.</param>
        public void RemoveNested(Type type)
        {
            nestedTypes.Remove(type.FullName);
        }
    }
}