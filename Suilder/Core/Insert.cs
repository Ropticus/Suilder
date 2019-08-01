using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Suilder.Builder;
using Suilder.Engines;
using Suilder.Exceptions;

namespace Suilder.Core
{
    /// <summary>
    /// Implementation of <see cref="IInsert"/>.
    /// </summary>
    public class Insert : ColList, IInsert
    {
        /// <summary>
        /// The into value.
        /// </summary>
        /// <value>The into value.</value>
        protected IAlias IntoValue { get; set; }

        /// <summary>
        /// Sets the "into" value.
        /// </summary>
        /// <param name="tableName">The table name.</param>
        /// <returns>The "insert" statement.</returns>
        public virtual IInsert Into(string tableName)
        {
            return Into(SqlBuilder.Instance.Alias(tableName));
        }

        /// <summary>
        /// Sets the "into" value.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <returns>The "insert" statement.</returns>
        public virtual IInsert Into(IAlias alias)
        {
            IntoValue = alias;
            return this;
        }

        /// <summary>
        /// Sets the "into" value.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <typeparam name="T">The type of the table.</typeparam>
        /// <returns>The "insert" statement.</returns>
        public virtual IInsert Into<T>(Expression<Func<T>> alias)
        {
            return Into(SqlBuilder.Instance.Alias<T>(alias));
        }

        /// <summary>
        /// Adds a value to the <see cref="IInsert"/>.
        /// </summary>
        /// <param name="value">The value to add to the <see cref="IInsert"/>.</param>
        /// <returns>The "insert" statement.</returns>
        IInsert IInsert.Add(IColumn value)
        {
            Add(value);
            return this;
        }

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="IInsert"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// <see cref="IInsert"/>.</param>
        /// <returns>The "insert" statement.</returns>
        IInsert IInsert.Add(params IColumn[] values)
        {
            Add(values);
            return this;
        }

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="IInsert"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the
        /// <see cref="IInsert"/>.</param>
        /// <returns>The "insert" statement.</returns>
        IInsert IInsert.Add(IEnumerable<IColumn> values)
        {
            Add(values);
            return this;
        }

        /// <summary>
        /// Adds a value to the <see cref="IInsert"/>.
        /// </summary>
        /// <param name="value">The value to add to the <see cref="IInsert"/>.</param>
        /// <returns>The "insert" statement.</returns>
        IInsert IInsert.Add(Expression<Func<object>> value)
        {
            Add(value);
            return this;
        }

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="IInsert"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// <see cref="IInsert"/>.</param>
        /// <returns>The "insert" statement.</returns>
        IInsert IInsert.Add(params Expression<Func<object>>[] values)
        {
            Add(values);
            return this;
        }

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="IInsert"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the
        /// <see cref="IInsert"/>.</param>
        /// <returns>The "insert" statement.</returns>
        IInsert IInsert.Add(IEnumerable<Expression<Func<object>>> values)
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
            if (IntoValue == null)
                throw new CompileException("Into value is null.");

            queryBuilder.Write("INSERT INTO ").WriteFragment(IntoValue);

            if (Values.Count > 0)
            {
                queryBuilder.Write(" (");
                base.Compile(queryBuilder, engine, false);
                queryBuilder.Write(")");
            }
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return ToStringBuilder.Build(b => b.Write("INSERT INTO")
                .IfNotNull(IntoValue, x => b.Write(" ").WriteFragment(x))
                .If(Values.Count > 0, () => b.Write(" (").Write(base.ToString()).Write(")")));
        }
    }
}