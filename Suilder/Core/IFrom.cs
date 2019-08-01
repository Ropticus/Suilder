using Suilder.Builder;
using Suilder.Engines;

namespace Suilder.Core
{
    /// <summary>
    /// A "from" clause.
    /// </summary>
    public interface IFrom : IQueryFragment
    {
        /// <summary>
        /// The alias name.
        /// </summary>
        /// <value>The alias name.</value>
        string AliasName { get; }

        /// <summary>
        /// The alias name or the table name, or null if cannot be obtained without compile.
        /// </summary>
        /// <value>The alias name or the table name, or null if cannot be obtained without compile.</value>
        string AliasOrTableName { get; }

        /// <summary>
        /// Additional options.
        /// <para>Use <see cref="IRawSql"/> to add options.</para>
        /// </summary>
        /// <param name="options">The options.</param>
        /// <returns>The "from" clause.</returns>
        IFrom Options(IQueryFragment options);

        /// <summary>
        /// Compiles the fragment.
        /// </summary>
        /// <param name="queryBuilder">The query builder.</param>
        /// <param name="engine">The engine.</param>
        /// <param name="withFrom">If compile with the "from" keyword.</param>
        void Compile(QueryBuilder queryBuilder, IEngine engine, bool withFrom);
    }
}