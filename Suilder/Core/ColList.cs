using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Suilder.Builder;
using Suilder.Engines;
using Suilder.Exceptions;

namespace Suilder.Core
{
    /// <summary>
    /// Implementation of <see cref="IColList"/>.
    /// </summary>
    public class ColList : QueryFragmentList<IColumn, object>, IColList
    {
        /// <summary>
        /// Adds a value to the end of the <see cref="IColList"/>.
        /// </summary>
        /// <param name="value">The value to add to the end of the <see cref="IColList"/>.</param>
        public override void Add(Expression<Func<object>> value)
        {
            Values.Add(SqlBuilder.Instance.Col(value));
        }

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="IColList"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// <see cref="IColList"/>.</param>
        public override void Add(params Expression<Func<object>>[] values)
        {
            Values.AddRange(values.Select(x => SqlBuilder.Instance.Col(x)));
        }

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="IColList"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the
        /// <see cref="IColList"/>.</param>
        public override void Add(IEnumerable<Expression<Func<object>>> values)
        {
            Values.AddRange(values.Select(x => SqlBuilder.Instance.Col(x)));
        }

        /// <summary>
        /// Adds a value to the end of the <see cref="IColList"/>.
        /// </summary>
        /// <param name="value">The value to add to the end of the <see cref="IColList"/>.</param>
        /// <returns>The list of columns.</returns>
        IColList IColList.Add(IColumn value)
        {
            Add(value);
            return this;
        }

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="IColList"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// <see cref="IColList"/>.</param>
        /// <returns>The list of columns.</returns>
        IColList IColList.Add(params IColumn[] values)
        {
            Add(values);
            return this;
        }

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="IColList"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the
        /// <see cref="IColList"/>.</param>
        /// <returns>The list of columns.</returns>
        IColList IColList.Add(IEnumerable<IColumn> values)
        {
            Add(values);
            return this;
        }

        /// <summary>
        /// Adds a value to the end of the <see cref="IColList"/>.
        /// </summary>
        /// <param name="value">The value to add to the end of the <see cref="IColList"/>.</param>
        /// <returns>The list of columns.</returns>
        IColList IColList.Add(Expression<Func<object>> value)
        {
            Add(value);
            return this;
        }

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="IColList"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// <see cref="IColList"/>.</param>
        /// <returns>The list of columns.</returns>
        IColList IColList.Add(params Expression<Func<object>>[] values)
        {
            Add(values);
            return this;
        }

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="IColList"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the
        /// <see cref="IColList"/>.</param>
        /// <returns>The list of columns.</returns>
        IColList IColList.Add(IEnumerable<Expression<Func<object>>> values)
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
            Compile(queryBuilder, engine, true);
        }

        /// <summary>
        /// Compiles the fragment.
        /// </summary>
        /// <param name="queryBuilder">The query builder.</param>
        /// <param name="engine">The engine.</param>
        /// <param name="withTableName">If compile with the table name.</param>
        public virtual void Compile(QueryBuilder queryBuilder, IEngine engine, bool withTableName)
        {
            if (Values.Count == 0)
                throw new CompileException("List is empty.");

            string separator = ", ";
            foreach (IColumn value in Values)
            {
                value.Compile(queryBuilder, engine, withTableName);
                queryBuilder.Write(separator);
            }
            queryBuilder.RemoveLast(separator.Length);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return ToStringBuilder.Build(b => b.Join(", ", Values, (x) => b.WriteFragment(x)));
        }
    }
}