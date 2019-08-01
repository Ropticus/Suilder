using System.Collections.Generic;

namespace Suilder.Builder
{
    /// <summary>
    /// The result of a compiled query.
    /// </summary>
    public class QueryResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryResult"/> class.
        /// </summary>
        /// <param name="sql">The SQL string.</param>
        /// <param name="parameters">The parameters of the query.</param>
        public QueryResult(string sql, IDictionary<string, object> parameters)
        {
            Sql = sql;
            Parameters = parameters;
        }

        /// <summary>
        /// The SQL string.
        /// </summary>
        /// <value>The SQL string.</value>
        public string Sql { get; private set; }

        /// <summary>
        /// The parameters of the query.
        /// </summary>
        /// <value>The parameters of the query.</value>
        public IDictionary<string, object> Parameters { get; private set; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return Sql + "; " + string.Join(", ", Parameters);
        }
    }
}