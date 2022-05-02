using System;
using Suilder.Builder;
using Suilder.Engines;

namespace Suilder.Core
{
    /// <summary>
    /// Implementation of <see cref="IFrom"/>.
    /// </summary>
    public class From : IFrom
    {
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
        /// The additional options.
        /// </summary>
        /// <value>The additional options.</value>
        protected IQueryFragment OptionsValue { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="From"/> class.
        /// </summary>
        /// <param name="alias">The alias.</param>
        public From(IAlias alias)
        {
            Source = alias;
            AliasName = alias.AliasName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="From"/> class.
        /// </summary>
        /// <param name="value">The subquery.</param>
        /// <param name="aliasName">The alias name.</param>
        public From(IQueryFragment value, string aliasName)
        {
            Source = value is ICte cte ? cte.Alias : value;
            AliasName = aliasName ?? throw new ArgumentNullException(nameof(aliasName), "Alias name cannot be null.");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="From"/> class.
        /// </summary>
        /// <param name="value">The subquery.</param>
        /// <param name="alias">The alias.</param>
        public From(IQueryFragment value, IAlias alias)
        {
            Source = value is ICte cte ? cte.Alias : value;
            AliasName = alias.AliasOrTableName ?? throw new ArgumentException("Alias name cannot be null.", nameof(alias));
        }

        /// <summary>
        /// Additional options.
        /// <para>Use <see cref="IRawSql"/> to add options.</para>
        /// </summary>
        /// <param name="options">The options.</param>
        /// <returns>The "from" clause.</returns>
        public virtual IFrom Options(IQueryFragment options)
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
            Compile(queryBuilder, engine, true);
        }

        /// <summary>
        /// Compiles the fragment.
        /// </summary>
        /// <param name="queryBuilder">The query builder.</param>
        /// <param name="engine">The engine.</param>
        /// <param name="withFrom">If compile with the "from" keyword.</param>
        public virtual void Compile(QueryBuilder queryBuilder, IEngine engine, bool withFrom)
        {
            if (withFrom)
                queryBuilder.Write("FROM ");

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
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return ToStringBuilder.Build(b => b.Write("FROM ")
                .WriteFragment(Source).IfNotNull(AliasName, x => b.Write(" AS " + x))
                .IfNotNull(OptionsValue, x => b.Write(" ").WriteFragment(x)));
        }
    }
}