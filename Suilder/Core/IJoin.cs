using System;
using System.Linq.Expressions;

namespace Suilder.Core
{
    /// <summary>
    /// A "join" clause.
    /// </summary>
    public interface IJoin : IQueryFragment
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
        /// Adds an "on" clause.
        /// </summary>
        /// <param name="on">The "on" condition.</param>
        /// <returns>The "join" clause.</returns>
        IJoin On(IQueryFragment on);

        /// <summary>
        /// Adds an "on" clause.
        /// </summary>
        /// <param name="on">The "on" condition.</param>
        /// <returns>The "join" clause.</returns>
        IJoin On(Expression<Func<bool>> on);

        /// <summary>
        /// Additional options.
        /// <para>Use <see cref="IRawSql"/> to add options.</para>
        /// </summary>
        /// <param name="options">The options.</param>
        /// <returns>The "join" clause.</returns>
        IJoin Options(IQueryFragment options);
    }

    /// <summary>
    /// A "join" clause without a source value.
    /// </summary>
    public interface IJoinFrom : IQueryFragment
    {
        /// <summary>
        /// Creates a "join" clause.
        /// </summary>
        /// <param name="tableName">The table name.</param>
        /// <returns>The "join" clause.</returns>
        IJoin Join(string tableName);

        /// <summary>
        /// Creates a "join" clause.
        /// </summary>
        /// <param name="tableName">The table name.</param>
        /// <param name="aliasName">The alias name.</param>
        /// <returns>The "join" clause.</returns>
        IJoin Join(string tableName, string aliasName);

        /// <summary>
        /// Creates a "join" clause.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <returns>The "join" clause.</returns>
        IJoin Join(IAlias alias);

        /// <summary>
        /// Creates a "join" clause with an expression
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <typeparam name="T">The type of the table.</typeparam>
        /// <returns>The "join" clause.</returns>
        IJoin Join<T>(Expression<Func<T>> alias);

        /// <summary>
        /// Creates a "join" clause with a subquery.
        /// </summary>
        /// <param name="value">The subquery.</param>
        /// <param name="aliasName">The alias name.</param>
        /// <returns>The "join" clause.</returns>
        IJoin Join(IQueryFragment value, string aliasName);

        /// <summary>
        /// Creates a "join" clause with a subquery.
        /// </summary>
        /// <param name="value">The subquery.</param>
        /// <param name="alias">The alias.</param>
        /// <returns>The "join" clause.</returns>
        IJoin Join(IQueryFragment value, IAlias alias);

        /// <summary>
        /// Creates a "join" clause with a subquery.
        /// </summary>
        /// <param name="value">The subquery.</param>
        /// <param name="alias">The alias.</param>
        /// <typeparam name="T">The type of the table.</typeparam>
        /// <returns>The "join" clause.</returns>
        IJoin Join<T>(IQueryFragment value, Expression<Func<T>> alias);
    }
}