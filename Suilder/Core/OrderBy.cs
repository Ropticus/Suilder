using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using Suilder.Builder;
using Suilder.Engines;
using Suilder.Exceptions;

namespace Suilder.Core
{
    /// <summary>
    /// Implementation of <see cref="IOrderBy"/>.
    /// </summary>
    public class OrderBy : ValList, IOrderBy
    {
        /// <summary>
        /// The index to start adding a direction order.
        /// </summary>
        /// <value>The index to start adding a direction order.</value>
        protected int IndexStartAdd { get; set; } = -1;

        /// <summary>
        /// Contains the direction order of the values.
        /// </summary>
        /// <value>The direction order of the values.</value>
        protected IDictionary<int, bool> OrderValues { get; set; } = new Dictionary<int, bool>();

        /// <summary>
        /// Adds a value to the <see cref="IOrderBy"/>.
        /// </summary>
        /// <param name="value">The value to add to the <see cref="IOrderBy"/>.</param>
        public override void Add(object value)
        {
            IndexStartAdd = Values.Count;
            base.Add(value);
        }

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="IOrderBy"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// <see cref="IOrderBy"/>.</param>
        public override void Add(params object[] values)
        {
            IndexStartAdd = Values.Count;
            base.Add(values);
        }

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="IOrderBy"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the
        /// <see cref="IOrderBy"/>.</param>
        public override void Add(IEnumerable<object> values)
        {
            IndexStartAdd = Values.Count;
            base.Add(values);
        }

        /// <summary>
        /// Adds a value to the <see cref="IOrderBy"/>.
        /// </summary>
        /// <param name="value">The value to add to the <see cref="IOrderBy"/>.</param>
        public override void Add(Expression<Func<object>> value)
        {
            IndexStartAdd = Values.Count;
            base.Add(value);
        }

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="IOrderBy"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// <see cref="IOrderBy"/>.</param>
        public override void Add(params Expression<Func<object>>[] values)
        {
            IndexStartAdd = Values.Count;
            base.Add(values);
        }

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="IOrderBy"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the
        /// <see cref="IOrderBy"/>.</param>
        public override void Add(IEnumerable<Expression<Func<object>>> values)
        {
            IndexStartAdd = Values.Count;
            base.Add(values);
        }

        /// <summary>
        /// Adds a value to the <see cref="IOrderBy"/>.
        /// </summary>
        /// <param name="value">The value to add to the <see cref="IOrderBy"/>.</param>
        /// <returns>The "order by" clause.</returns>
        IOrderBy IOrderBy.Add(object value)
        {
            Add(value);
            return this;
        }

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="IOrderBy"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// <see cref="IOrderBy"/>.</param>
        /// <returns>The "order by" clause.</returns>
        IOrderBy IOrderBy.Add(params object[] values)
        {
            Add(values);
            return this;
        }

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="IOrderBy"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the
        /// <see cref="IOrderBy"/>.</param>
        /// <returns>The "order by" clause.</returns>
        IOrderBy IOrderBy.Add(IEnumerable<object> values)
        {
            Add(values);
            return this;
        }

        /// <summary>
        /// Adds a value to the <see cref="IOrderBy"/>.
        /// </summary>
        /// <param name="value">The value to add to the <see cref="IOrderBy"/>.</param>
        /// <returns>The "order by" clause.</returns>
        IOrderBy IOrderBy.Add(Expression<Func<object>> value)
        {
            Add(value);
            return this;
        }

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="IOrderBy"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// <see cref="IOrderBy"/>.</param>
        /// <returns>The "order by" clause.</returns>
        IOrderBy IOrderBy.Add(params Expression<Func<object>>[] values)
        {
            Add(values);
            return this;
        }

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="IOrderBy"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the
        /// <see cref="IOrderBy"/>.</param>
        /// <returns>The "order by" clause.</returns>
        IOrderBy IOrderBy.Add(IEnumerable<Expression<Func<object>>> values)
        {
            Add(values);
            return this;
        }

        /// <summary>
        /// Sets ascending order for all columns added with the latest <see cref="o:Add"/> method call.
        /// </summary>
        /// <value>The "order by".</value>
        /// <exception cref="InvalidOperationException">The list is empty or there is a select all column.</exception>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public virtual IOrderBy Asc => SetOrder(true);

        /// <summary>
        /// Sets descending order for all columns added with the latest <see cref="o:Add"/> method call.
        /// </summary>
        /// <value>The "order by".</value>
        /// <exception cref="InvalidOperationException">The list is empty or there is a select all column.</exception>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public virtual IOrderBy Desc => SetOrder(false);

        /// <summary>
        /// Sets the specified  order for all columns added with the latest <see cref="o:Add"/> method call.
        /// </summary>
        /// <param name="ascending">If the order is ascending.</param>
        /// <returns>The "order by" clause.</returns>
        /// <exception cref="InvalidOperationException">The list is empty or there is a select all column.</exception>
        public virtual IOrderBy SetOrder(bool ascending = true)
        {
            if (IndexStartAdd < 0)
                throw new InvalidOperationException("List is empty.");

            for (int i = IndexStartAdd; i < Values.Count; i++)
            {
                if (Values[i] is IColumn column && column.SelectAll)
                    throw new InvalidOperationException("Cannot add order for select all column.");
                OrderValues.Add(i, ascending);
            }

            return this;
        }

        /// <summary>
        /// Compiles the fragment.
        /// </summary>
        /// <param name="queryBuilder">The query builder.</param>
        /// <param name="engine">The engine.</param>
        public override void Compile(QueryBuilder queryBuilder, IEngine engine)
        {
            if (Values.Count == 0)
                throw new CompileException("List is empty.");

            queryBuilder.Write("ORDER BY ");

            string separator = ", ";
            for (int i = 0; i < Values.Count; i++)
            {
                queryBuilder.WriteValue(Values[i]);
                if (OrderValues.TryGetValue(i, out bool asc))
                {
                    queryBuilder.Write(asc ? " ASC" : " DESC");
                }
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
            return ToStringBuilder.Build(b => b.Write("ORDER BY ")
                .Join(", ", Values, (x, i) => b.WriteValue(x)
                    .If(OrderValues.TryGetValue(i, out bool asc), () => b.Write(asc ? " ASC" : " DESC"))));
        }
    }
}