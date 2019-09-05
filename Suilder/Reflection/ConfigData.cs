using System;
using System.Collections.Generic;
using System.Reflection;

namespace Suilder.Reflection
{
    /// <summary>
    /// Contains the configuration of a table.
    /// </summary>
    public class ConfigData
    {
        /// <summary>
        /// The type of the class.
        /// </summary>
        /// <value></value>
        public Type Type { get; set; }

        /// <summary>
        /// The inheritance level.
        /// </summary>
        /// <value></value>
        public int InheritLevel { get; set; }

        /// <summary>
        /// If the type is a table.
        /// <para>If false the type is only used for inherit the configuration.</para>
        /// <para>By default is false for abstract classes.</para>
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
        /// <para>By default is true for types whose base type is abstract.</para>
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
        public List<string> PrimaryKeys { get; set; } = new List<string>();

        /// <summary>
        /// The column properties.
        /// </summary>
        /// <value>The column properties.</value>
        public List<string> Columns { get; set; } = new List<string>();

        /// <summary>
        /// The ignored properties.
        /// </summary>
        /// <value>The ignored properties.</value>
        public List<string> Ignore { get; set; } = new List<string>();

        /// <summary>
        /// The translations of the properties.
        /// </summary>
        /// <value>The translations of the properties.</value>
        public IDictionary<string, string> ColumnsNames { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// The foreign keys properties.
        /// </summary>
        /// <value>The ignored properties.</value>
        public IDictionary<string, List<ColumnData>> ForeignKeys { get; set; } = new Dictionary<string, List<ColumnData>>();

        /// <summary>
        /// The properties of the type that can be a column.
        /// </summary>
        /// <value>The properties of the type that can be a column.</value>
        public PropertyInfo[] Properties { get; set; }

        /// <summary>
        /// Contains the data of a property column.
        /// </summary>
        public struct ColumnData
        {
            /// <summary>
            /// The property.
            /// </summary>
            /// <value>The property.</value>
            public string PropertyName { get; set; }

            /// <summary>
            /// The translation of the property.
            /// </summary>
            /// <value>The translation of the property.</value>
            public string ColumnName { get; set; }
        }
    }
}