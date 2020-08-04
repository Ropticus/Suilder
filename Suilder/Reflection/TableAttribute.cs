namespace Suilder.Reflection
{
    /// <summary>
    /// Override the table name.
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Struct)]
    public sealed class TableAttribute : System.Attribute
    {
        /// <summary>
        /// The schema name.
        /// </summary>
        /// <value>The schema name.</value>
        public string Schema { get; set; }

        /// <summary>
        /// The table name.
        /// </summary>
        /// <value>The table name.</value>
        public string Name { get; set; }

        /// <summary>
        /// If the type is a table.
        /// </summary>
        /// <value>If the type is a table.</value>
        public bool IsTable { get; set; } = true;

        /// <summary>
        /// If the type must inherit the table name and columns of the base type.
        /// </summary>
        private bool? inheritTable;

        /// <summary>
        /// If the type must inherit the table name and columns of the base type.
        /// </summary>
        /// <value>If the type must inherit the the table name and columns of the base type.</value>
        public bool InheritTable
        {
            get => inheritTable.GetValueOrDefault();
            set => inheritTable = value;
        }

        /// <summary>
        /// If the property <see cref="InheritTable"/> has value.
        /// </summary>
        /// <value>If the property <see cref="InheritTable"/> has value.</value>
        public bool InheritTableHasValue { get => inheritTable.HasValue; }

        /// <summary>
        /// If the type must inherit the columns of the base type.
        /// <para>By default is <see langword="true"/> for types whose base type is <see langword="abstract"/>.</para>
        /// </summary>
        private bool? inheritColumns;

        /// <summary>
        /// If the type must inherit the columns of the base type.
        /// <para>By default is <see langword="true"/> for types whose base type is <see langword="abstract"/>.</para>
        /// </summary>
        /// <value>If the type must inherit the columns of the base type.</value>
        public bool InheritColumns
        {
            get => inheritColumns.GetValueOrDefault();
            set => inheritColumns = value;
        }

        /// <summary>
        /// If the property <see cref="InheritColumns"/> has value.
        /// </summary>
        /// <value>If the property <see cref="InheritColumns"/> has value.</value>
        public bool InheritColumnsHasValue { get => inheritColumns.HasValue; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TableAttribute"/> class.
        /// </summary>
        public TableAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TableAttribute"/> class.
        /// </summary>
        /// <param name="name">The table name.</param>
        public TableAttribute(string name)
        {
            Name = name;
        }
    }
}