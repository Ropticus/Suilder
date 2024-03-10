namespace Suilder.Reflection
{
    /// <summary>
    /// Override the foreign key name.
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Field | System.AttributeTargets.Property, AllowMultiple = true)]
    public sealed class ForeignKeyAttribute : System.Attribute
    {
        /// <summary>
        /// The property name.
        /// </summary>
        /// <value>The property name.</value>
        public string PropertyName { get; set; }

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
        /// Initializes a new instance of the <see cref="ForeignKeyAttribute"/> class.
        /// </summary>
        public ForeignKeyAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ForeignKeyAttribute"/> class.
        /// </summary>
        /// <param name="name">The column name.</param>
        public ForeignKeyAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ForeignKeyAttribute"/> class.
        /// </summary>
        /// <param name="name">The column name.</param>
        /// <param name="partialName">If it is a partial column name.</param>
        public ForeignKeyAttribute(string name, bool partialName)
        {
            Name = name;
            PartialName = partialName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ForeignKeyAttribute"/> class.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <param name="name">The column name.</param>
        public ForeignKeyAttribute(string propertyName, string name)
        {
            PropertyName = propertyName;
            Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ForeignKeyAttribute"/> class.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <param name="name">The column name.</param>
        /// <param name="partialName">If it is a partial column name.</param>
        public ForeignKeyAttribute(string propertyName, string name, bool partialName)
        {
            PropertyName = propertyName;
            Name = name;
            PartialName = partialName;
        }
    }
}