using System.Collections.Generic;
using Suilder.Engines;

namespace Suilder.Builder
{
    /// <summary>
    /// The result of a compiled query.
    /// </summary>
    public class QueryResult
    {
        /// <summary>
        /// The SQL string.
        /// </summary>
        /// <value>The SQL string.</value>
        public string Sql { get; private set; }

        /// <summary>
        /// The named parameters of the query.
        /// <para>This property is <see langword="null"/> if <see cref="EngineOptions.ParameterIndex"/>
        /// is <see langword="false"/>.</para>
        /// <seealso cref="ParametersList"/>
        /// </summary>
        /// <value>The named parameters of the query.</value>
        public IDictionary<string, object> Parameters { get; private set; }

        /// <summary>
        /// The positional parameters of the query.
        /// <para>This property is <see langword="null"/> if <see cref="EngineOptions.ParameterIndex"/>
        /// is <see langword="true"/>.</para>
        /// <seealso cref="Parameters"/>
        /// </summary>
        /// <value>The positional parameters of the query.</value>
        public IList<object> ParametersList { get; private set; }

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
        /// Initializes a new instance of the <see cref="QueryResult"/> class.
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters">The parameters of the query.</param>
        public QueryResult(string sql, IList<object> parameters)
        {
            Sql = sql;
            ParametersList = parameters;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return Sql + "; " + (Parameters != null ? string.Join(", ", Parameters) : string.Join(", ", ParametersList));
        }
    }
}