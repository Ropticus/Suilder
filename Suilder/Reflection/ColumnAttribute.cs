namespace Suilder.Reflection
{
    /// <summary>
    /// Override the column name.
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Field | System.AttributeTargets.Property)]
    public sealed class ColumnAttribute : System.Attribute
    {
        /// <summary>
        /// The column name.
        /// </summary>
        /// <value>The column name.</value>
        public string Name { get; set; }

        /// <summary>
        /// If it is a partial column name.
        /// </summary>
        /// <value>If it is a partial column name.</value>
        public bool PartialName { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnAttribute"/> class.
        /// </summary>
        /// <param name="name">The column name.</param>
        public ColumnAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnAttribute"/> class.
        /// </summary>
        /// <param name="name">The column name.</param>
        /// <param name="partialName">If it is a partial column name.</param>
        public ColumnAttribute(string name, bool partialName)
        {
            Name = name;
            PartialName = partialName;
        }
    }
}