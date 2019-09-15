using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Suilder.Builder;
using Suilder.Engines;
using Suilder.Exceptions;

namespace Suilder.Core
{
    /// <summary>
    /// Implementation of <see cref="ISelect"/>.
    /// </summary>
    public class Select : ValList, ISelect, ISelectTop
    {
        /// <summary>
        /// If the query is distinct.
        /// </summary>
        private bool isDistinct;

        /// <summary>
        /// If the query is distinct.
        /// </summary>
        /// <value>If the query is distinct.</value>
        protected bool IsDistinct
        {
            get => isDistinct;
            set
            {
                isDistinct = value;
                if (isDistinct == false)
                    distinctOnValues = null;
            }
        }

        /// <summary>
        /// The values of the distinct on.
        /// </summary>
        private IValList distinctOnValues;

        /// <summary>
        /// The values of the distinct on.
        /// </summary>
        /// <value>The values of the distinct on.</value>
        protected IValList DistinctOnValues
        {
            get => distinctOnValues;
            set
            {
                distinctOnValues = value;
                isDistinct = distinctOnValues != null;
            }
        }

        /// <summary>
        /// The "top" value.
        /// </summary>
        /// <value>The "top" value.</value>
        protected IQueryFragment TopValue { get; set; }

        /// <summary>
        /// Contains the alias of the values.
        /// </summary>
        /// <value>The alias of the values.</value>
        protected IDictionary<int, string> Alias { get; set; } = new Dictionary<int, string>();

        /// <summary>
        /// Contains the over clause of the values.
        /// </summary>
        /// <value>The over clause of the values.</value>
        protected IDictionary<int, IQueryFragment> OverValues { get; set; } = new Dictionary<int, IQueryFragment>();

        /// <summary>
        /// Sets a distinct result.
        /// </summary>
        /// <param name="distinct">If the select is distinct.</param>
        /// <returns>The "select" statement.</returns>
        public virtual ISelect Distinct(bool distinct = true)
        {
            IsDistinct = distinct;
            return this;
        }

        /// <summary>
        /// Sets a "distinct on" clause.
        /// </summary>
        /// <param name="value">The value to add to the "distinct on".</param>
        /// <returns>The "select" statement.</returns>
        public virtual ISelect DistinctOn(object value)
        {
            return DistinctOn(SqlBuilder.Instance.ValList.Add(value));
        }

        /// <summary>
        /// Sets a "distinct on" clause.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the "distinct on".</param>
        /// <returns>The "select" statement.</returns>
        public virtual ISelect DistinctOn(params object[] values)
        {
            return DistinctOn(SqlBuilder.Instance.ValList.Add(values));
        }

        /// <summary>
        /// Sets a "distinct on" clause.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the "distinct on".</param>
        /// <returns>The "select" statement.</returns>
        public virtual ISelect DistinctOn(IEnumerable<object> values)
        {
            return DistinctOn(SqlBuilder.Instance.ValList.Add(values));
        }

        /// <summary>
        /// Sets a "distinct on" clause.
        /// </summary>
        /// <param name="value">The value to add to the "distinct on".</param>
        /// <returns>The "select" statement.</returns>
        public virtual ISelect DistinctOn(Expression<Func<object>> value)
        {
            return DistinctOn(SqlBuilder.Instance.ValList.Add(value));
        }

        /// <summary>
        /// Sets a "distinct on" clause.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the "distinct on".</param>
        /// <returns>The "select" statement.</returns>
        public virtual ISelect DistinctOn(params Expression<Func<object>>[] values)
        {
            return DistinctOn(SqlBuilder.Instance.ValList.Add(values));
        }

        /// <summary>
        /// Sets a "distinct on" clause.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the "distinct on".</param>
        /// <returns>The "select" statement.</returns>

        public virtual ISelect DistinctOn(IEnumerable<Expression<Func<object>>> values)
        {
            return DistinctOn(SqlBuilder.Instance.ValList.Add(values));
        }

        /// <summary>
        /// Sets a "distinct on" clause.
        /// </summary>
        /// <param name="value">The list of columns.</param>
        /// <returns>The "select" statement.</returns>
        public virtual ISelect DistinctOn(IValList value)
        {
            DistinctOnValues = value;
            return this;
        }

        /// <summary>
        /// Sets a "distinct on" clause.
        /// </summary>
        /// <param name="func">Function that returns the list of columns.</param>
        /// <returns>The "select" statement.</returns>
        public virtual ISelect DistinctOn(Func<IValList, IValList> func)
        {
            return DistinctOn(func(SqlBuilder.Instance.ValList));
        }

        /// <summary>
        /// Adds a "top" clause.
        /// </summary>
        /// <param name="top">The "top" clause.</param>
        /// <returns>The "select" statement.</returns>
        public virtual ISelect Top(ITop top)
        {
            TopValue = top;
            return this;
        }

        /// <summary>
        /// Adds a raw "top" clause.
        /// <para>You must write the entire clause.</para>
        /// </summary>
        /// <param name="top">The "top" clause.</param>
        /// <returns>The "select" statement.</returns>
        public virtual ISelect Top(IRawSql top)
        {
            TopValue = top;
            return this;
        }

        /// <summary>
        /// Adds a "top" clause.
        /// </summary>
        /// <param name="fetch">The number of rows to return.</param>
        /// <returns>The "select" statement.</returns>
        public virtual ISelectTop Top(object fetch)
        {
            Top(SqlBuilder.Instance.Top(fetch));
            return this;
        }

        /// <summary>
        /// Return a percent of rows.
        /// </summary>
        /// <param name="percent">If return a percent of rows.</param>
        /// <returns>The "select" statement.</returns>
        public virtual ISelectTop Percent(bool percent = true)
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
        /// <returns>The "select" statement.</returns>
        public virtual ISelectTop WithTies(bool withTies = true)
        {
            if (TopValue is ITop top)
            {
                top.WithTies(withTies);
                return this;
            }

            throw new InvalidOperationException($"Top value must be a \"{typeof(ITop)}\" instance.");
        }

        /// <summary>
        /// Adds a value to the <see cref="ISelect"/>.
        /// </summary>
        /// <param name="value">The value to add to the <see cref="ISelect"/>.</param>
        /// <returns>The "select" statement.</returns>
        ISelect ISelect.Add(object value)
        {
            Add(value);
            return this;
        }

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="ISelect"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// <see cref="ISelect"/>.</param>
        /// <returns>The "select" statement.</returns>
        ISelect ISelect.Add(params object[] values)
        {
            Add(values);
            return this;
        }

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="ISelect"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the
        /// <see cref="ISelect"/>.</param>
        /// <returns>The "select" statement.</returns>
        ISelect ISelect.Add(IEnumerable<object> values)
        {
            Add(values);
            return this;
        }

        /// <summary>
        /// Adds a value to the <see cref="ISelect"/>.
        /// </summary>
        /// <param name="value">The value to add to the <see cref="ISelect"/>.</param>
        /// <returns>The "select" statement.</returns>
        ISelect ISelect.Add(Expression<Func<object>> value)
        {
            Add(value);
            return this;
        }

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="ISelect"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// <see cref="ISelect"/>.</param>
        /// <returns>The "select" statement.</returns>
        ISelect ISelect.Add(params Expression<Func<object>>[] values)
        {
            Add(values);
            return this;
        }

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="ISelect"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the
        /// <see cref="ISelect"/>.</param>
        /// <returns>The "select" statement.</returns>
        ISelect ISelect.Add(IEnumerable<Expression<Func<object>>> values)
        {
            Add(values);
            return this;
        }

        /// <summary>
        /// Adds an alias to the latest column added with <see cref="o:Add"/> method.
        /// </summary>
        /// <param name="aliasName">The alias name.</param>
        /// <returns>The "select" statement.</returns>
        /// <exception cref="InvalidOperationException">The list is empty or there is a select all column.</exception>
        public virtual ISelect As(string aliasName)
        {
            int index = Values.Count - 1;
            if (index < 0)
                throw new InvalidOperationException("List is empty.");

            if (Values[index] is IColumn column && column.SelectAll)
                throw new InvalidOperationException("Cannot add alias for select all column.");

            Alias[index] = aliasName;
            return this;
        }

        /// <summary>
        /// Adds an "over" clause to the latest column added with <see cref="o:Add"/> method.
        /// </summary>
        /// <returns>The "select" statement.</returns>
        /// <exception cref="InvalidOperationException">The list is empty or there is a select all column.</exception>
        public virtual ISelect Over()
        {
            return Over(SqlBuilder.Instance.Over);
        }

        /// <summary>
        /// Adds an "over" clause to the latest column added with <see cref="o:Add"/> method.
        /// </summary>
        /// <param name="over">The over value.</param>
        /// <returns>The "select" statement.</returns>
        /// <exception cref="InvalidOperationException">The list is empty or there is a select all column.</exception>
        public virtual ISelect Over(IQueryFragment over)
        {
            int index = Values.Count - 1;
            if (index < 0)
                throw new InvalidOperationException("List is empty.");

            if (Values[index] is IColumn column && column.SelectAll)
                throw new InvalidOperationException("Cannot add over clause for select all column.");

            OverValues[index] = over;
            return this;
        }

        /// <summary>
        /// Adds an "over" clause to the latest column added with <see cref="o:Add"/> method.
        /// </summary>
        /// <param name="func">Function with the <see cref="IOver"/> value.</param>
        /// <returns>The "select" statement.</returns>
        /// <exception cref="InvalidOperationException">The list is empty or there is a select all column.</exception>
        public virtual ISelect Over(Func<IOver, IOver> func)
        {
            return Over(func(SqlBuilder.Instance.Over));
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

            queryBuilder.Write("SELECT ");

            if (IsDistinct)
            {
                queryBuilder.Write("DISTINCT ");
                if (DistinctOnValues != null)
                {
                    if (!engine.Options.DistinctOnSupported)
                        throw new ClauseNotSupportedException("Distinct on clause is not supported in this engine.");

                    queryBuilder.Write("ON(").WriteFragment(DistinctOnValues).Write(") ");
                }
            }

            if (TopValue != null)
                queryBuilder.WriteFragment(TopValue).Write(" ");

            string separator = ", ";
            for (int i = 0; i < Values.Count; i++)
            {
                queryBuilder.WriteValue(Values[i]);
                if (Alias.TryGetValue(i, out string alias))
                {
                    queryBuilder.Write(" AS ").WriteName(alias);
                }
                if (OverValues.TryGetValue(i, out IQueryFragment over))
                {
                    queryBuilder.Write(" ").WriteFragment(over);
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
            return ToStringBuilder.Build(b => b.Write("SELECT ")
                .If(IsDistinct, () => b.Write("DISTINCT ")
                    .IfNotNull(DistinctOnValues, x => b.Write("ON(").WriteFragment(x).Write(") ")))
                .IfNotNull(TopValue, x => b.WriteFragment(x).Write(" "))
                .Join(", ", Values, (x, i) => b.WriteValue(x)
                    .If(Alias.TryGetValue(i, out string alias), () => b.Write(" AS " + alias))
                    .If(OverValues.TryGetValue(i, out var over), () => b.Write(" ").WriteFragment(over))));
        }
    }
}