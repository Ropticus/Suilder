namespace Suilder.Core
{
    /// <summary>
    /// A "top" clause.
    /// </summary>
    public interface ITop : IQueryFragment
    {
        /// <summary>
        /// Return a percent of rows.
        /// </summary>
        /// <param name="percent">If return a percent of rows.</param>
        /// <returns>The "top" clause.</returns>
        ITop Percent(bool percent = true);

        /// <summary>
        /// Return two or more rows that tie for last place.
        /// </summary>
        /// <param name="withTies">If return two or more rows that tie for last place.</param>
        /// <returns>The "top" clause.</returns>
        ITop WithTies(bool withTies = true);
    }
}