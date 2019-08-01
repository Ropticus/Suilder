using Suilder.Builder;
using Suilder.Engines;

namespace Suilder.Core
{
    /// <summary>
    /// The column of a table, view or subquery.
    /// </summary>
    public interface IColumn : IQueryFragment
    {
        /// <summary>
        /// The table name or his alias.
        /// </summary>
        /// <value>The table name or his alias.</value>
        string TableName { get; }

        /// <summary>
        /// If the column is a select all.
        /// </summary>
        /// <value>True if the column is a select all.</value>
        bool SelectAll { get; }

        /// <summary>
        /// Compiles the fragment.
        /// </summary>
        /// <param name="queryBuilder">The query builder.</param>
        /// <param name="engine">The engine.</param>
        /// <param name="withTableName">If compile with the table name.</param>
        void Compile(QueryBuilder queryBuilder, IEngine engine, bool withTableName);
    }
}