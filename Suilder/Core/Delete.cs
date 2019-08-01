using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Suilder.Builder;
using Suilder.Engines;

namespace Suilder.Core
{
    /// <summary>
    /// Implementation of <see cref="IDelete"/>.
    /// </summary>
    public class Delete : QueryFragmentList<IAlias, object>, IDelete, IDeleteTop
    {
        /// <summary>
        /// The alias to delete.
        /// </summary>
        /// <value>The alias to delete.</value>
        public IEnumerable<IAlias> Alias => Values;

        /// <summary>
        /// The "top" value.
        /// </summary>
        /// <value>The "top" value.</value>
        protected IQueryFragment TopValue { get; set; }

        /// <summary>
        /// Adds a "top" clause.
        /// </summary>
        /// <param name="fetch">The number of rows to return.</param>
        /// <returns>The "delete" statement.</returns>
        public virtual IDeleteTop Top(object fetch)
        {
            Top(SqlBuilder.Instance.Top(fetch));
            return this;
        }

        /// <summary>
        /// Adds a "top" clause.
        /// </summary>
        /// <param name="top">The "top" clause.</param>
        /// <returns>The "delete" statement.</returns>
        public virtual IDelete Top(ITop top)
        {
            TopValue = top;
            return this;
        }

        /// <summary>
        /// Return a percent of rows.
        /// </summary>
        /// <param name="percent">If return a percent of rows.</param>
        /// <returns>The "delete" statement.</returns>
        public virtual IDeleteTop Percent(bool percent = true)
        {
            if (TopValue is ITop top)
            {
                top.Percent(percent);
                return this;
            }

            throw new InvalidOperationException($"Top value must be a \"{typeof(ITop)}\" instance.");
        }

        /// <summary>
        /// Return two or more rows that tie for last place.
        /// </summary>
        /// <param name="withTies">If return two or more rows that tie for last place.</param>
        /// <returns>The "delete" statement.</returns>
        public virtual IDeleteTop WithTies(bool withTies = true)
        {
            if (TopValue is ITop top)
            {
                top.WithTies(withTies);
                return this;
            }

            throw new InvalidOperationException($"Top value must be a \"{typeof(ITop)}\" instance.");
        }

        /// <summary>
        /// Adds a value to the <see cref="IDelete"/>.
        /// </summary>
        /// <param name="value">The value to add to the <see cref="IDelete"/>.</param>
        public override void Add(Expression<Func<object>> value)
        {
            Values.Add(SqlBuilder.Instance.Alias(value));
        }

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="IDelete"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// <see cref="IDelete"/>.</param>
        public override void Add(params Expression<Func<object>>[] values)
        {
            Values.AddRange(values.Select(x => SqlBuilder.Instance.Alias(x)));
        }

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="IDelete"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the
        /// <see cref="IDelete"/>.</param>
        public override void Add(IEnumerable<Expression<Func<object>>> values)
        {
            Values.AddRange(values.Select(x => SqlBuilder.Instance.Alias(x)));
        }

        /// <summary>
        /// Adds a value to the <see cref="IDelete"/>.
        /// </summary>
        /// <param name="value">The value to add to the <see cref="IDelete"/>.</param>
        /// <returns>The "delete" statement.</returns>
        IDelete IDelete.Add(IAlias value)
        {
            Add(value);
            return this;
        }

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="IDelete"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// <see cref="IDelete"/>.</param>
        /// <returns>The "delete" statement.</returns>
        IDelete IDelete.Add(params IAlias[] values)
        {
            Add(values);
            return this;
        }

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="IDelete"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the
        /// <see cref="IDelete"/>.</param>
        /// <returns>The "delete" statement.</returns>
        IDelete IDelete.Add(IEnumerable<IAlias> values)
        {
            Add(values);
            return this;
        }

        /// <summary>
        /// Adds a value to the <see cref="IDelete"/>.
        /// </summary>
        /// <param name="value">The value to add to the <see cref="IDelete"/>.</param>
        /// <returns>The "delete" statement.</returns>
        IDelete IDelete.Add(Expression<Func<object>> value)
        {
            Add(value);
            return this;
        }

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="IDelete"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// <see cref="IDelete"/>.</param>
        /// <returns>The "delete" statement.</returns>
        IDelete IDelete.Add(params Expression<Func<object>>[] values)
        {
            Add(values);
            return this;
        }

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="IDelete"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the
        /// <see cref="IDelete"/>.</param>
        /// <returns>The "delete" statement.</returns>
        IDelete IDelete.Add(IEnumerable<Expression<Func<object>>> values)
        {
            Add(values);
            return this;
        }

        /// <summary>
        /// Compiles the fragment.
        /// </summary>
        /// <param name="queryBuilder">The query builder.</param>
        /// <param name="engine">The engine.</param>
        public override void Compile(QueryBuilder queryBuilder, IEngine engine)
        {
            queryBuilder.Write("DELETE");

            if (TopValue != null)
                queryBuilder.Write(" ").WriteFragment(TopValue);

            if (Values.Count > 0)
            {
                queryBuilder.Write(" ");

                string separator = ", ";
                foreach (IAlias value in Values)
                {
                    queryBuilder.WriteName(value.AliasOrTableName).Write(separator);
                }
                queryBuilder.RemoveLast(separator.Length);
            }
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return ToStringBuilder.Build(b => b.Write("DELETE")
                .IfNotNull(TopValue, x => b.Write(" ").WriteFragment(x))
                .If(Values.Count > 0, () => b.Write(" ").Join(", ", Values, x => b.WriteFragment(x))));
        }
    }
}