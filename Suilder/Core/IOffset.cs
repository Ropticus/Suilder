namespace Suilder.Core
{
    /// <summary>
    /// An "offset fetch" clause.
    /// </summary>
    public interface IOffset : IQueryFragment
    {
        /// <summary>
        /// Adds an "offset" clause.
        /// </summary>
        /// <param name="offset">The number of rows to skip.</param>
        /// <returns>The "offset fetch" clause.</returns>
        IOffset Offset(object offset);

        /// <summary>
        /// Adds a "fetch" clause.
        /// </summary>
        /// <param name="fetch">The number of rows to return.</param>
        /// <returns>The "offset fetch" clause.</returns>
        IOffset Fetch(object fetch);
    }
}