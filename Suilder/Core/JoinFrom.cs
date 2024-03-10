using System;
using System.Linq.Expressions;
using Suilder.Builder;
using Suilder.Engines;
using Suilder.Exceptions;

namespace Suilder.Core
{
    /// <summary>
    /// Implementation of <see cref="IJoin"/> and <see cref="IJoinFrom"/>.
    /// </summary>
    public class JoinFrom : IJoin, IJoinFrom
    {
        /// <summary>
        /// The join type.
        /// </summary>
        /// <value>The join type.</value>
        protected JoinType JoinType { get; set; }

        /// <summary>
        /// The source value.
        /// </summary>
        /// <value>The source value.</value>
        protected IQueryFragment Source { get; set; }

        /// <summary>
        /// The alias name.
        /// </summary>
        /// <value>The alias name.</value>
        public string AliasName { get; protected set; }

        /// <summary>
        /// The alias name or the table name, or null if cannot be obtained without compile.
        /// </summary>
        /// <value>The alias name or the table name, or null if cannot be obtained without compile.</value>
        public string AliasOrTableName => AliasName ?? (Source is IAlias alias ? alias.AliasOrTableName : null);

        /// <summary>
        /// The on value.
        /// </summary>
        /// <value>The on value.</value>
        protected IQueryFragment OnValue { get; set; }

        /// <summary>
        /// The additional options.
        /// </summary>
        /// <value>The additional options.</value>
        protected IQueryFragment OptionsValue { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="JoinFrom"/> class.
        /// </summary>
        /// <param name="joinType">The join type.</param>
        public JoinFrom(JoinType joinType)
        {
            JoinType = joinType;
        }

        /// <summary>
        /// Creates a "join" clause.
        /// </summary>
        /// <param name="tableName">The table name.</param>
        /// <returns>The "join" clause.</returns>
        public virtual IJoin Join(string tableName)
        {
            Source = SqlBuilder.Instance.Alias(tableName);
            return this;
        }

        /// <summary>
        /// Creates a "join" clause.
        /// </summary>
        /// <param name="tableName">The table name.</param>
        /// <param name="aliasName">The alias name.</param>
        /// <returns>The "join" clause.</returns>
        public virtual IJoin Join(string tableName, string aliasName)
        {
            IAlias alias = SqlBuilder.Instance.Alias(tableName, aliasName);
            Source = alias;
            AliasName = alias.AliasName;
            return this;
        }

        /// <summary>
        /// Creates a "join" clause.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <returns>The "join" clause.</returns>
        public virtual IJoin Join(IAlias alias)
        {
            Source = alias;
            AliasName = alias.AliasName;
            return this;
        }

        /// <summary>
        /// Creates a "join" clause with an expression
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <typeparam name="T">The type of the table.</typeparam>
        /// <returns>The "join" clause.</returns>
        public virtual IJoin Join<T>(Expression<Func<T>> alias)
        {
            IAlias aliasValue = SqlBuilder.Instance.Alias(alias);
            Source = aliasValue;
            AliasName = aliasValue.AliasName;
            return this;
        }

        /// <summary>
        /// Creates a "join" clause with a subquery.
        /// </summary>
        /// <param name="value">The subquery.</param>
        /// <param name="aliasName">The alias name.</param>
        /// <returns>The "join" clause.</returns>
        public virtual IJoin Join(IQueryFragment value, string aliasName)
        {
            Source = value is ICte cte ? cte.Alias : value;
            AliasName = aliasName ?? throw new ArgumentNullException(nameof(aliasName), "Alias name cannot be null.");

            return this;
        }

        /// <summary>
        /// Creates a "join" clause with a subquery.
        /// </summary>
        /// <param name="value">The subquery.</param>
        /// <param name="alias">The alias.</param>
        /// <returns>The "join" clause.</returns>
        public virtual IJoin Join(IQueryFragment value, IAlias alias)
        {
            Source = value is ICte cte ? cte.Alias : value;
            AliasName = alias.AliasOrTableName ?? throw new ArgumentException("Alias name cannot be null.", nameof(alias));

            return this;
        }

        /// <summary>
        /// Creates a "join" clause with a subquery.
        /// </summary>
        /// <param name="value">The subquery.</param>
        /// <param name="alias">The alias.</param>
        /// <typeparam name="T">The type of the table.</typeparam>
        /// <returns>The "join" clause.</returns>
        public virtual IJoin Join<T>(IQueryFragment value, Expression<Func<T>> alias)
        {
            return Join(value, SqlBuilder.Instance.Alias(alias).AliasName);
        }

        /// <summary>
        /// Adds an "on" clause.
        /// </summary>
        /// <param name="on">The "on" condition.</param>
        /// <returns>The "join" clause.</returns>
        public virtual IJoin On(IQueryFragment on)
        {
            OnValue = on;
            return this;
        }

        /// <summary>
        /// Adds an "on" clause.
        /// </summary>
        /// <param name="on">The "on" condition.</param>
        /// <returns>The "join" clause.</returns>
        public virtual IJoin On(Expression<Func<bool>> on)
        {
            OnValue = SqlBuilder.Instance.Op(on);
            return this;
        }

        /// <summary>
        /// Additional options.
        /// <para>Use <see cref="IRawSql"/> to add options.</para>
        /// </summary>
        /// <param name="options">The options.</param>
        /// <returns>The "join" clause.</returns>
        public virtual IJoin Options(IQueryFragment options)
        {
            OptionsValue = options;
            return this;
        }

        /// <summary>
        /// Compiles the fragment.
        /// </summary>
        /// <param name="queryBuilder">The query builder.</param>
        /// <param name="engine">The engine.</param>
        public virtual void Compile(QueryBuilder queryBuilder, IEngine engine)
        {
            switch (JoinType)
            {
                case JoinType.Inner:
                    queryBuilder.Write("INNER JOIN ");
                    break;
                case JoinType.Left:
                    queryBuilder.Write("LEFT JOIN ");
                    break;
                case JoinType.Right:
                    if (!engine.Options.RightJoinSupported)
                        throw new ClauseNotSupportedException("Right join is not supported in this engine.");
                    queryBuilder.Write("RIGHT JOIN ");
                    break;
                case JoinType.Full:
                    if (!engine.Options.FullJoinSupported)
                        throw new ClauseNotSupportedException("Full join is not supported in this engine.");
                    queryBuilder.Write("FULL JOIN ");
                    break;
                case JoinType.Cross:
                    queryBuilder.Write("CROSS JOIN ");
                    break;
                default:
                    throw new CompileException($"Invalid join type \"{JoinType}\".");
            }

            queryBuilder.WriteFragment(Source);

            if (AliasName != null)
            {
                if (engine.Options.TableAs)
                    queryBuilder.Write(" AS ");
                else
                    queryBuilder.Write(" ");
                queryBuilder.WriteName(AliasName);
            }

            if (OptionsValue != null)
            {
                queryBuilder.Write(" ").WriteFragment(OptionsValue);
            }

            if (OnValue != null)
            {
                queryBuilder.Write(" ON ").WriteFragment(OnValue);
            }
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return ToStringBuilder.Build(b => b.Write(JoinType.ToString().ToUpperInvariant()).Write(" JOIN ")
                .WriteFragment(Source).IfNotNull(AliasName, x => b.Write(" AS " + x))
                .IfNotNull(OptionsValue, x => b.Write(" ").WriteFragment(x))
                .IfNotNull(OnValue, x => b.Write(" ON ").WriteFragment(x)));
        }
    }
}