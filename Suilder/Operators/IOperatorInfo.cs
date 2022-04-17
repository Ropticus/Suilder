namespace Suilder.Operators
{
    /// <summary>
    /// Contains the information of an operator.
    /// </summary>
    public interface IOperatorInfo
    {
        /// <summary>
        /// The operator.
        /// </summary>
        /// <value>The operator.</value>
        string Op { get; }

        /// <summary>
        /// If the operator is a function.
        /// </summary>
        /// <value>If the operator is a function.</value>
        bool Function { get; }
    }
}