namespace Suilder.Operators
{
    /// <summary>
    /// Implementation of <see cref="IOperatorInfo"/>.
    /// </summary>
    public class OperatorInfo : IOperatorInfo
    {
        /// <summary>
        /// The operator.
        /// </summary>
        /// <value>The operator.</value>
        public string Op { get; set; }

        /// <summary>
        /// If the operator is a function.
        /// </summary>
        /// <value>If the operator is a function.</value>
        public bool Function { get; set; }
    }
}