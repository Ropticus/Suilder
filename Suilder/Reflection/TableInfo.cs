using System;
using System.Collections.Generic;

namespace Suilder.Reflection
{
    /// <summary>
    /// Contains the information of a table.
    /// </summary>
    public class TableInfo
    {
        /// <summary>
        /// The type of the class.
        /// </summary>
        /// <value></value>
        public Type Type { get; set; }

        /// <summary>
        /// The table name.
        /// </summary>
        /// <value>The table name.</value>
        public string TableName { get; set; }

        /// <summary>
        /// The primary key properties.
        /// </summary>
        /// <value>The primary key properties.</value>
        public string[] PrimaryKeys { get; set; }

        /// <summary>
        /// The column properties.
        /// </summary>
        /// <value>The column properties.</value>
        public string[] Columns { get; set; }

        /// <summary>
        /// The column names of the properties.
        /// <para>The key is the column property, the value is the column name.</para>
        /// </summary>
        /// <value>The column names of the properties.</value>
        public IDictionary<string, string> ColumnNamesDic { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// The list of column names.
        /// </summary>
        /// <value>The list of column names.</value>
        public string[] ColumnNames { get; set; }
    }
}