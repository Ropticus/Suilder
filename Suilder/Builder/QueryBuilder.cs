using System;
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
        /// The string builder with the SQL result.
        /// </summary>
        /// <returns>The string builder with the SQL result.</returns>
        protected StringBuilder Builder { get; set; } = new StringBuilder();

        /// <summary>
        /// The named parameters of the query.
        /// </summary>
        /// <value>The named parameters of the query.</value>
        protected IDictionary<string, object> Parameters { get; set; }

        /// <summary>
        /// The positional parameters of the query.
        /// </summary>
        /// <value>The positional parameters of the query.</value>
        protected IList<object> ParametersList { get; set; }

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

            if (Engine.Options.ParameterIndex)
                Parameters = new Dictionary<string, object>();
            else
                ParametersList = new List<object>();
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
        /// Writes an <see cref="IQueryFragment"/> to the query.
        /// </summary>
        /// <param name="value">The <see cref="IQueryFragment"/> to write.</param>
        /// <returns>The query builder.</returns>
        public QueryBuilder WriteFragment(IQueryFragment value)
        {
            return WriteFragment(value, value is ISubQuery);
        }

        /// <summary>
        /// Writes an <see cref="IQueryFragment"/> to the query.
        /// </summary>
        /// <param name="value">The <see cref="IQueryFragment"/> to write.</param>
        /// <param name="parentheses">When to add parentheses to the <see cref="IQueryFragment"/>.</param>
        /// <returns>The query builder.</returns>
        public QueryBuilder WriteFragment(IQueryFragment value, Parentheses parentheses)
        {
            switch (parentheses)
            {
                case Parentheses.Never:
                    return WriteFragment(value, false);
                case Parentheses.SubFragment:
                    return WriteFragment(value, value is ISubFragment);
                case Parentheses.SubQuery:
                    return WriteFragment(value, value is ISubQuery);
                case Parentheses.Always:
                    return WriteFragment(value, true);
                default:
                    throw new ArgumentException("Invalid value.", nameof(parentheses));
            }
        }

        /// <summary>
        /// Writes an <see cref="IQueryFragment"/> to the query.
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
        /// Writes a value to the query.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <returns>The query builder.</returns>
        public QueryBuilder WriteValue(object value)
        {
            if (value is IQueryFragment queryFragment)
                return WriteFragment(queryFragment);
            else
                return AddParameter(value);
        }

        /// <summary>
        /// Writes a value to the query.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <param name="parentheses">When to add parentheses to the value.</param>
        /// <returns>The query builder.</returns>
        public QueryBuilder WriteValue(object value, Parentheses parentheses)
        {
            if (value is IQueryFragment queryFragment)
                return WriteFragment(queryFragment, parentheses);
            else
                return AddParameter(value, parentheses);
        }

        /// <summary>
        /// Writes a value to the query.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <param name="addParentheses">If add parentheses to the value.</param>
        /// <returns>The query builder.</returns>
        public QueryBuilder WriteValue(object value, bool addParentheses)
        {
            if (value is IQueryFragment queryFragment)
                return WriteFragment(queryFragment, addParentheses);
            else
                return AddParameter(value, addParentheses);
        }

        /// <summary>
        /// Adds a parameter to the query.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="parentheses">When to add parentheses to the parameter.</param>
        /// <returns>The query builder.</returns>
        public QueryBuilder AddParameter(object value, Parentheses parentheses)
        {
            switch (parentheses)
            {
                case Parentheses.Never:
                case Parentheses.SubFragment:
                case Parentheses.SubQuery:
                    return AddParameter(value);
                case Parentheses.Always:
                    return AddParameter(value, true);
                default:
                    throw new ArgumentException("Invalid value.", nameof(parentheses));
            }
        }

        /// <summary>
        /// Adds a parameter to the query.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="addParentheses">If add parentheses to the parameter.</param>
        /// <returns>The query builder.</returns>
        public QueryBuilder AddParameter(object value, bool addParentheses)
        {
            if (addParentheses)
                Builder.Append("(");

            AddParameter(value);

            if (addParentheses)
                Builder.Append(")");

            return this;
        }

        /// <summary>
        /// Adds a parameter to the query.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <returns>The query builder.</returns>
        public QueryBuilder AddParameter(object value)
        {
            if (Parameters != null)
            {
                string key = Engine.Options.ParameterPrefix + Parameters.Keys.Count;
                Builder.Append(key);
                Parameters[key] = value;
            }
            else
            {
                Builder.Append(Engine.Options.ParameterPrefix);
                ParametersList.Add(value);
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
            Builder.Length = Math.Max(Builder.Length - length, 0);
            return this;
        }

        /// <summary>
        /// Gets the compiled query.
        /// </summary>
        /// <returns>The compiled query</returns>
        public QueryResult ToQueryResult()
        {
            if (Parameters != null)
                return new QueryResult(Builder.ToString(), Parameters);
            else
                return new QueryResult(Builder.ToString(), ParametersList);
        }
    }

    /// <summary>
    /// When to add parentheses to an <see cref="IQueryFragment"/>.
    /// </summary>
    public enum Parentheses
    {
        /// <summary>
        /// Do not add parentheses.
        /// </summary>
        Never,

        /// <summary>
        /// When the value implements the <see cref="ISubFragment"/> interface.
        /// </summary>
        SubFragment,

        /// <summary>
        /// When the value implements the <see cref="ISubQuery"/> interface.
        /// </summary>
        SubQuery,

        /// <summary>
        /// Add parentheses.
        /// </summary>
        Always
    }
}