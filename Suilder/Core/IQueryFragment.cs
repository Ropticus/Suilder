using Suilder.Builder;
using Suilder.Engines;

namespace Suilder.Core
{
    /// <summary>
    /// A fragment of a query that can be compiled.
    /// </summary>
    public interface IQueryFragment
    {
        /// <summary>
        /// Compiles the fragment.
        /// </summary>
        /// <param name="queryBuilder">The query builder.</param>
        /// <param name="engine">The engine.</param>
        void Compile(QueryBuilder queryBuilder, IEngine engine);
    }
}