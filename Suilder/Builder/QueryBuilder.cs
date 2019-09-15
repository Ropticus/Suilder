using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Suilder.Core;
using Suilder.Engines;

namespace Suilder.Builder
{
    /// <summary>
    /// A query builder to compile a query.
    /// </summary>
    public class QueryBuilder
    {
        /// <summary>
        /// The parameters of the query.
        /// </summary>
        /// <value>The parameters of the query.</value>
        protected IDictionary<string, object> Parameters { get; set; }

        /// <summary>
        /// The string builder with the SQL result.
        /// </summary>
        /// <returns>The string builder with the SQL result.</returns>
        protected StringBuilder Builder { get; set; } = new StringBuilder();

        /// <summary>
        /// The engine to compile the query.
        /// </summary>
        /// <value>The engine to compile the query.</value>
        public IEngine Engine { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryBuilder"/> class.
        /// </summary>
        /// <param name="engine">The engine to compile the query.</param>
        public QueryBuilder(IEngine engine)
        {
            Engine = engine;
            Parameters = new Dictionary<string, object>();
        }

        /// <summary>
        /// Writes the text to the query.
        /// </summary>
        /// <param name="text">The text to write.</param>
        /// <returns>The query builder.</returns>
        public QueryBuilder Write(string text)
        {
            Builder.Append(text);
            return this;
        }

        /// <summary>
        /// Writes an escaped name to the query.
        /// </summary>
        /// <param name="name">The name to write.</param>
        /// <returns>The query builder.</returns>
        public QueryBuilder WriteName(string name)
        {
            Builder.Append(Engine.EscapeName(name));
            return this;
        }

        /// <summary>
        /// Writes a <see cref="IQueryFragment"/> to the query.
        /// </summary>
        /// <param name="value">The <see cref="IQueryFragment"/> to write.</param>
        /// <returns>The query builder.</returns>
        public QueryBuilder WriteFragment(IQueryFragment value)
        {
            return WriteFragment(value, value is ISubQuery);
        }

        /// <summary>
        /// Writes a <see cref="IQueryFragment"/> to the query.
        /// </summary>
        /// <param name="value">The <see cref="IQueryFragment"/> to write.</param>
        /// <param name="addParentheses">If add parentheses to the <see cref="IQueryFragment"/>.</param>
        /// <returns>The query builder.</returns>
        public QueryBuilder WriteFragment(IQueryFragment value, bool addParentheses)
        {
            if (addParentheses)
                Builder.Append("(");

            value.Compile(this, Engine);

            if (addParentheses)
                Builder.Append(")");

            return this;
        }

        /// <summary>
        /// Writes an object to the query.
        /// </summary>
        /// <param name="value">The object to write.</param>
        /// <returns>The query builder.</returns>
        public QueryBuilder WriteValue(object value)
        {
            IQueryFragment queryFragment = value as IQueryFragment;

            if (queryFragment != null)
                WriteFragment(queryFragment);
            else
                AddParameter(value);
            return this;
        }

        /// <summary>
        /// Add a parameter to the query.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <returns>The query builder.</returns>
        public QueryBuilder AddParameter(object value)
        {
            if (!(value is String) && value is IEnumerable list)
            {
                Builder.Append("(");
                bool any = false;
                string separator = ", ";
                foreach (object item in list)
                {
                    if (item == null)
                    {
                        Builder.Append("NULL");
                    }
                    else
                    {
                        string key = Engine.Options.ParameterPrefix + (Parameters.Keys.Count);
                        Parameters[key] = item;
                        Builder.Append(key);
                    }
                    Builder.Append(separator);
                    any = true;
                }
                if (any)
                    RemoveLast(separator.Length);
                Builder.Append(")");
            }
            else
            {
                if (value == null)
                {
                    Builder.Append("NULL");
                }
                else
                {
                    string key = Engine.Options.ParameterPrefix + (Parameters.Keys.Count);
                    Parameters[key] = value;
                    Builder.Append(key);
                }
            }

            return this;
        }

        /// <summary>
        /// Removes the last characters of the query.
        /// </summary>
        /// <param name="length">The length to remove.</param>
        /// <returns>The query builder.</returns>
        public QueryBuilder RemoveLast(int length)
        {
            Builder.Length -= length;
            return this;
        }

        /// <summary>
        /// Gets the compiled query.
        /// </summary>
        /// <returns>The compiled query</returns>
        public QueryResult ToQueryResult()
        {
            return new QueryResult(Builder.ToString(), Parameters);
        }
    }
}