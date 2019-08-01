namespace Suilder.Core
{
    /// <summary>
    /// An operator.
    /// </summary>
    public interface IOperator : IQueryFragment
    {
        /// <summary>
        /// The operator.
        /// </summary>
        /// <value>The operator.</value>
        string Op { get; }
    }
}