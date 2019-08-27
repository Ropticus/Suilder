using System;
using System.Collections.Generic;
using System.Reflection;
using Suilder.Exceptions;

namespace Suilder.Reflection.Builder
{
    /// <summary>
    /// Contains the configuration of a table.
    /// </summary>
    public class TableConfig
    {
        /// <summary>
        /// The type of the table.
        /// </summary>
        /// <value>The type of the table.</value>
        public Type Type { get; protected set; }

        /// <summary>
        /// The inheritance level.
        /// </summary>
        /// <value>The inheritance level</value>
        public int InheritLevel { get; protected set; }

        /// <summary>
        /// The properties of the type.
        /// <para>It includes nested and ignored properties.</para>
        /// </summary>
        /// <value>The properties of the type.</value>
        public IList<PropertyData> Properties { get; set; } = new List<PropertyData>();

        /// <summary>
        /// If the type is a table.
        /// <para>If <see langword="false"/> the type is only used for inherit the configuration.</para>
        /// <para>By default is <see langword="false"/> for <see langword="abstract"/> classes.</para>
        /// </summary>
        /// <value>If the type is a table.</value>
        public bool? IsTable { get; set; }

        /// <summary>
        /// If the type must inherit the table name and columns of the base type.
        /// </summary>
        /// <value>If the type must inherit the the table name and columns of the base type.</value>
        public bool? InheritTable { get; set; }

        /// <summary>
        /// If the type must inherit the columns of the base type.
        /// <para>By default is <see langword="true"/> for types whose base type is <see langword="abstract"/>.</para>
        /// </summary>
        /// <value>If the type must inherit the columns of the base type.</value>
        public bool? InheritColumns { get; set; }

        /// <summary>
        /// The table name.
        /// </summary>
        /// <value>The table name.</value>
        public string TableName { get; set; }

        /// <summary>
        /// The primary key properties.
        /// </summary>
        /// <value>The primary key properties.</value>
        public IList<string> PrimaryKeys { get; set; } = new List<string>();

        /// <summary>
        /// The translations of the properties.
        /// </summary>
        /// <value>The translations of the properties.</value>
        public IDictionary<string, string> ColumnNames { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// The foreign keys properties.
        /// </summary>
        /// <value>The ignored properties.</value>
        public ISet<string> ForeignKeys { get; set; } = new HashSet<string>();

        /// <summary>
        /// The ignored properties.
        /// </summary>
        /// <value>The ignored properties.</value>
        public ISet<string> Ignore { get; set; } = new HashSet<string>();

        /// <summary>
        /// The metadata of the table.
        /// </summary>
        /// <value>The metadata of the table.</value>
        public IDictionary<string, object> TableMetadata { get; set; } = new Dictionary<string, object>();

        /// <summary>
        /// The metadata of the members.
        /// <para>The key is the member name, the value is the metadata of the member.</para>
        /// </summary>
        /// <value>The metadata of the members.</value>
        public IDictionary<string, IDictionary<string, object>> MemberMetadata { get; set; }
            = new Dictionary<string, IDictionary<string, object>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="TableConfig"/> class.
        /// </summary>
        /// <param name="type">The type of the table.</param>
        public TableConfig(Type type)
        {
            Type parentType = type.BaseType;
            if (parentType == null)
                throw new InvalidConfigurationException($"Invalid type \"{type}\".");

            Type = type;

            // Get inherit level
            while (parentType.BaseType != null)
            {
                parentType = parentType.BaseType;
                InheritLevel++;
            }
        }

        /// <summary>
        /// The data of a property.
        /// </summary>
        public class PropertyData
        {
            /// <summary>
            /// The full property name.
            /// </summary>
            /// <value>The full property name.</value>
            public string FullName { get; set; }

            /// <summary>
            /// The property info.
            /// </summary>
            /// <value>The property info.</value>
            public PropertyInfo Info { get; set; }

            /// <summary>
            /// If the property is a column.
            /// </summary>
            /// <value><see langword="true"/> if the property is a column, otherwise, <see langword="false"/>.</value>
            public bool IsColumn => !IsTable && !IsNested;

            /// <summary>
            /// If the property is another table.
            /// </summary>
            /// <value><see langword="true"/> if the property is another table, otherwise, <see langword="false"/>.</value>
            public bool IsTable { get; set; }

            /// <summary>
            /// If the property is a nested type.
            /// </summary>
            /// <value><see langword="true"/> if the property is a nested type, otherwise, <see langword="false"/>.</value>
            public bool IsNested { get; set; }

            /// <summary>
            /// If the property is ignored.
            /// </summary>
            /// <value><see langword="true"/> if the property is ignored, otherwise, <see langword="false"/>.</value>
            public bool IsIgnored { get; set; }
        }
    }
}