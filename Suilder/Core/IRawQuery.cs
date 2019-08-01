namespace Suilder.Core
{
    /// <summary>
    /// A raw SQL query.
    /// </summary>
    public interface IRawQuery : IRawSql, ISubQuery
    {
        /// <summary>
        /// Sets a value to write before the query.
        /// </summary>
        /// <param name="value">The value to write before the query.</param>
        /// <returns>The raw query.</returns>
        IRawQuery Before(IQueryFragment value);

        /// <summary>
        /// Sets a value to write after the query.
        /// </summary>
        /// <param name="value">The value to write after the query.</param>
        /// <returns>The raw query.</returns>
        IRawQuery After(IQueryFragment value);

        /// <summary>
        /// Sets the "offset" clause.
        /// </summary>
        /// <param name="offset">The "offset" clause.</param>
        /// <returns>The raw query.</returns>
        IRawQuery Offset(IOffset offset);

        /// <summary>
        /// Sets a raw "offset" clause.
        /// <para>You must write the entire clause.</para>
        /// </summary>
        /// <param name="offset">The "offset" clause.</param>
        /// <returns>The raw query.</returns>
        IRawQuery Offset(IRawSql offset);

        /// <summary>
        /// Creates or adds an "offset" clause.
        /// </summary>
        /// <param name="offset">The number of rows to skip.</param>
        /// <returns>The raw query.</returns>
        IRawQuery Offset(object offset);

        /// <summary>
        /// Creates an "offset fetch" clause.
        /// </summary>
        /// <param name="offset">The number of rows to skip.</param>
        /// <param name="fetch">The number of rows to return.</param>
        /// <returns>The raw query.</returns>
        IRawQuery Offset(object offset, object fetch);

        /// <summary>
        /// Creates or adds "fetch" clause.
        /// </summary>
        /// <param name="fetch">The number of rows to return.</param>
        /// <returns>The raw query.</returns>
        IRawQuery Fetch(object fetch);
    }
}