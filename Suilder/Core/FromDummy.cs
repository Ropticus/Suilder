using Suilder.Builder;
using Suilder.Engines;

namespace Suilder.Core
{
    /// <summary>
    /// Writes a "from" clause with a dummy table if needed.
    /// </summary>
    public class FromDummy : IRawSql
    {
        /// <summary>
        /// The cached value.
        /// </summary>
        private static FromDummy fromDummy;

        /// <summary>
        /// Gets or initializes a new instance of the <see cref="FromDummy"/> class.
        /// </summary>
        /// <returns>The <see cref="FromDummy"/> instance.</returns>
        public static FromDummy Instance
        {
            get
            {
                if (fromDummy == null)
                    fromDummy = new FromDummy();

                return fromDummy;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FromDummy"/> class.
        /// </summary>
        protected FromDummy()
        {
        }

        /// <summary>
        /// Compiles the fragment.
        /// </summary>
        /// <param name="queryBuilder">The query builder.</param>
        /// <param name="engine">The engine.</param>
        public virtual void Compile(QueryBuilder queryBuilder, IEngine engine)
        {
            if (engine.Options.FromDummyName != null)
                queryBuilder.Write("FROM " + engine.Options.FromDummyName);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return "FROM dummy_table";
        }
    }
}