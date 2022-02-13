using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Suilder.Builder;
using Suilder.Engines;

namespace Suilder.Core
{
    /// <summary>
    /// Implementation of <see cref="ICte"/>.
    /// </summary>
    public class Cte : ColList, ICte
    {
        /// <summary>
        /// The name of the CTE.
        /// </summary>
        /// <value>The name of the CTE.</value>
        public string Name => Alias.AliasOrTableName;

        /// <summary>
        /// An alias with the name of the CTE.
        /// </summary>
        /// <value>An alias with the name of the CTE.</value>
        public IAlias Alias { get; protected set; }

        /// <summary>
        /// The query.
        /// </summary>
        /// <value>The query.</value>
        protected IQueryFragment Query { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cte"/> class.
        /// </summary>
        /// <param name="name">The name of the CTE.</param>
        public Cte(string name)
        {
            Alias = SqlBuilder.Instance.Alias(name);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cte"/> class.
        /// </summary>
        /// <param name="alias">The alias.</param>
        public Cte(IAlias alias)
        {
            Alias = SqlBuilder.Instance.Alias(alias.AliasOrTableName
                ?? throw new ArgumentException("Alias name is null.", nameof(alias)));
        }

        /// <summary>
        /// Adds a value to the end of the <see cref="ICte"/>.
        /// </summary>
        /// <param name="value">The value to add to the end of the <see cref="ICte"/>.</param>
        /// <returns>The CTE.</returns>
        ICte ICte.Add(IColumn value)
        {
            Add(value);
            return this;
        }

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="ICte"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// <see cref="ICte"/>.</param>
        /// <returns>The CTE.</returns>
        ICte ICte.Add(params IColumn[] values)
        {
            Add(values);
            return this;
        }

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="ICte"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the
        /// <see cref="ICte"/>.</param>
        /// <returns>The CTE.</returns>
        ICte ICte.Add(IEnumerable<IColumn> values)
        {
            Add(values);
            return this;
        }

        /// <summary>
        /// Adds a value to the end of the <see cref="ICte"/>.
        /// </summary>
        /// <param name="value">The value to add to the end of the <see cref="ICte"/>.</param>
        /// <returns>The CTE.</returns>
        ICte ICte.Add(Expression<Func<object>> value)
        {
            Add(value);
            return this;
        }

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="ICte"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// <see cref="ICte"/>.</param>
        /// <returns>The CTE.</returns>
        ICte ICte.Add(params Expression<Func<object>>[] values)
        {
            Add(values);
            return this;
        }

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="ICte"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the
        /// <see cref="ICte"/>.</param>
        /// <returns>The CTE.</returns>
        ICte ICte.Add(IEnumerable<Expression<Func<object>>> values)
        {
            Add(values);
            return this;
        }

        /// <summary>
        /// Sets the query of the CTE.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>The CTE.</returns>
        public virtual ICte As(IQueryFragment query)
        {
            Query = query;
            return this;
        }

        /// <summary>
        /// Compiles the fragment.
        /// </summary>
        /// <param name="queryBuilder">The query builder.</param>
        /// <param name="engine">The engine.</param>
        public override void Compile(QueryBuilder queryBuilder, IEngine engine)
        {
            queryBuilder.WriteFragment(Alias);

            if (Values.Count > 0)
            {
                queryBuilder.Write(" (");
                base.Compile(queryBuilder, engine, false);
                queryBuilder.Write(")");
            }

            queryBuilder.Write(" AS ").WriteFragment(Query);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return ToStringBuilder.Build(b => b.WriteFragment(Alias)
                .If(Values.Count > 0, () => b.Write(" (").Write(base.ToString()).Write(")"))
                .IfNotNull(Query, x => b.Write(" AS ").WriteFragment(x)));
        }
    }
}