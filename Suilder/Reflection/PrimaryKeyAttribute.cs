namespace Suilder.Reflection
{
    /// <summary>
    /// Sets the primary key.
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Field | System.AttributeTargets.Property)]
    public class PrimaryKeyAttribute : System.Attribute
    {
        /// <summary>
        /// The order of the primary key for composite keys.
        /// </summary>
        /// <value>The order of the primary key for composite keys.</value>
        public int Order { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PrimaryKeyAttribute"/> class.
        /// </summary>
        public PrimaryKeyAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PrimaryKeyAttribute"/> class.
        /// </summary>
        /// <param name="order">The order of the primary key for composite keys.</param>
        public PrimaryKeyAttribute(int order)
        {
            Order = order;
        }
    }
}