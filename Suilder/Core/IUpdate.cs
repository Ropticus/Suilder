namespace Suilder.Core
{
    /// <summary>
    /// An "update" statement.
    /// <para>Contains only the options of the "update" statement.</para>
    /// <para>Does not contain the source table and "set" clause.</para>
    /// </summary>
    public interface IUpdate : IQueryFragment
    {
        /// <summary>
        /// Adds a "top" clause.
        /// </summary>
        /// <param name="top">The "top" clause.</param>
        /// <returns>The "update" statement.</returns>
        IUpdate Top(ITop top);

        /// <summary>
        /// Adds a raw "top" clause.
        /// <para>You must write the entire clause.</para>
        /// </summary>
        /// <param name="top">The "top" clause.</param>
        /// <returns>The "update" statement.</returns>
        IUpdate Top(IRawSql top);

        /// <summary>
        /// Adds a "top" clause.
        /// </summary>
        /// <param name="fetch">The number of rows to return.</param>
        /// <returns>The "update" statement.</returns>
        IUpdateTop Top(object fetch);
    }

    /// <summary>
    /// A "update" statement with options for the "top" clause.
    /// </summary>
    public interface IUpdateTop : IUpdate
    {
        /// <summary>
        /// Return a percent of rows.
        /// </summary>
        /// <param name="percent">If return a percent of rows.</param>
        /// <returns>The "update" statement.</returns>
        IUpdateTop Percent(bool percent = true);

        /// <summary>
        /// Return two or more rows that tie for last place.
        /// </summary>
        /// <param name="withTies">If return two or more rows that tie for last place.</param>
        /// <returns>The "update" statement.</returns>
        IUpdateTop WithTies(bool withTies = true);
    }
}