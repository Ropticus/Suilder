using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using Suilder.Builder;
using Suilder.Engines;
using Suilder.Exceptions;
using Suilder.Functions;

namespace Suilder.Core
{
    /// <summary>
    /// Implementation of <see cref="IQuery"/>.
    /// </summary>
    public class Query : IQuery, IQueryFrom, IQueryJoin, IQueryJoinFrom
    {
        /// <summary>
        /// The value to write before the query.
        /// </summary>
        /// <value>The value to write before the query.</value>
        protected IQueryFragment BeforeValue { get; set; }

        /// <summary>
        /// The value to write after the query.
        /// </summary>
        /// <value>The value to write after the query.</value>
        protected IQueryFragment AfterValue { get; set; }

        /// <summary>
        /// The "with" value.
        /// </summary>
        /// <value>The "with" value.</value>
        protected IQueryFragment WithValue { get; set; }

        /// <summary>
        /// The "select" value.
        /// </summary>
        private IQueryFragment selectValue;

        /// <summary>
        /// The "select" value.
        /// </summary>
        /// <value>The "select" value.</value>
        protected IQueryFragment SelectValue
        {
            get => selectValue;
            set
            {
                selectValue = value;
                if (value != null)
                {
                    UpdateValue = null;
                    DeleteValue = null;
                }
            }
        }

        /// <summary>
        /// The "insert" value.
        /// </summary>
        private IQueryFragment insertValue;

        /// <summary>
        /// The "insert" value.
        /// </summary>
        /// <value>The "insert" value.</value>
        protected IQueryFragment InsertValue
        {
            get => insertValue;
            set
            {
                insertValue = value;
                if (value != null)
                {
                    UpdateValue = null;
                    DeleteValue = null;
                }
                else
                {
                    InsertValues = null;
                }
            }
        }

        /// <summary>
        /// The list of rows to insert.
        /// </summary>
        private List<IValList> insertValues;

        /// <summary>
        /// The list of rows to insert.
        /// </summary>
        /// <value>The list of rows to insert.</value>
        protected List<IValList> InsertValues
        {
            get => insertValues;
            set
            {
                insertValues = value;
                if (value != null)
                {
                    SelectValue = null;
                    FromValue = null;
                    Joins.Clear();
                    WhereValue = null;
                    GroupByValue = null;
                    HavingValue = null;
                    OrderByValue = null;
                    OffsetValue = null;
                }
            }
        }

        /// <summary>
        /// The "update" value.
        /// </summary>
        private IQueryFragment updateValue;

        /// <summary>
        /// The "update" value.
        /// </summary>
        /// <value>The "update" value.</value>
        protected IQueryFragment UpdateValue
        {
            get => updateValue;
            set
            {
                updateValue = value;
                if (value != null)
                {
                    SelectValue = null;
                    InsertValue = null;
                    DeleteValue = null;
                }
                else
                {
                    SetValues = null;
                }
            }
        }

        /// <summary>
        /// The "set" values.
        /// </summary>
        /// <value>The "set" values.</value>
        protected List<Tuple<IColumn, object>> SetValues { get; set; }

        /// <summary>
        /// The "delete" value.
        /// </summary>
        private IQueryFragment deleteValue;

        /// <summary>
        /// The "delete" value.
        /// </summary>
        /// <value>The "delete" value.</value>
        protected IQueryFragment DeleteValue
        {
            get => deleteValue;
            set
            {
                deleteValue = value;
                if (value != null)
                {
                    SelectValue = null;
                    InsertValue = null;
                    UpdateValue = null;
                }
            }
        }

        /// <summary>
        /// The "from" value.
        /// </summary>
        /// <value>The "from" value.</value>
        protected IQueryFragment FromValue { get; set; }

        /// <summary>
        /// The joins list.
        /// </summary>
        /// <value>The joins list.</value>
        protected List<IQueryFragment> Joins { get; set; } = new List<IQueryFragment>();

        /// <summary>
        /// The last join added without a source value.
        /// </summary>
        /// <value>The last join added without a source value.</value>
        protected IJoinFrom JoinFrom { get; set; }

        /// <summary>
        /// The "where" value.
        /// </summary>
        /// <value>The "where" value.</value>
        protected IQueryFragment WhereValue { get; set; }

        /// <summary>
        /// The "group by" value.
        /// </summary>
        /// <value>The "group by" value.</value>
        protected IQueryFragment GroupByValue { get; set; }

        /// <summary>
        /// The "having" value.
        /// </summary>
        /// <value>The "having" value.</value>
        protected IQueryFragment HavingValue { get; set; }

        /// <summary>
        /// The "order by" value.
        /// </summary>
        /// <value>The "order by" value.</value>
        protected IQueryFragment OrderByValue { get; set; }

        /// <summary>
        /// The "offset" value.
        /// </summary>
        /// <value>The "offset" value.</value>
        protected IQueryFragment OffsetValue { get; set; }

        /// <summary>
        /// Sets a value to write before the query.
        /// </summary>
        /// <param name="value">The value to write before the query.</param>
        /// <returns>The query.</returns>
        public virtual IQuery Before(IQueryFragment value)
        {
            BeforeValue = value;
            return this;
        }

        /// <summary>
        /// Sets a value to write after the query.
        /// </summary>
        /// <param name="value">The value to write after the query.</param>
        /// <returns>The query.</returns>
        public virtual IQuery After(IQueryFragment value)
        {
            AfterValue = value;
            return this;
        }

        /// <summary>
        /// Sets the "with" clause.
        /// </summary>
        /// <param name="with">The "with" clause.</param>
        /// <returns>The query.</returns>
        public virtual IQuery With(IWith with)
        {
            WithValue = with;
            return this;
        }

        /// <summary>
        /// Sets a raw "with" clause.
        /// <para>You must write the entire clause including the "with" keyword.</para>
        /// </summary>
        /// <param name="with">The "with" clause.</param>
        /// <returns>The query.</returns>
        public virtual IQuery With(IRawSql with)
        {
            WithValue = with;
            return this;
        }

        /// <summary>
        /// Sets the "with" clause.
        /// </summary>
        /// <param name="func">Function that returns the "with" clause.</param>
        /// <returns>The query.</returns>
        public virtual IQuery With(Func<IWith, IWith> func)
        {
            return With(func(SqlBuilder.Instance.With));
        }

        /// <summary>
        /// Creates a "with" clause and adds a value to the "with" clause.
        /// </summary>
        /// <param name="value">The value to add to the "with" clause.</param>
        /// <returns>The query.</returns>
        public virtual IQuery With(IQueryFragment value)
        {
            return With(SqlBuilder.Instance.With.Add(value));
        }

        /// <summary>
        /// Creates a "with" clause and adds the elements of the specified array to the end of the "with" clause.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the "with" clause.</param>
        /// <returns>The query.</returns>
        public virtual IQuery With(params IQueryFragment[] values)
        {
            return With(SqlBuilder.Instance.With.Add(values));
        }

        /// <summary>
        /// Creates a "with" clause and adds the elements of the specified collection to the end of the "with"
        /// clause.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the "with" clause.</param>
        /// <returns>The query.</returns>
        public virtual IQuery With(IEnumerable<IQueryFragment> values)
        {
            return With(SqlBuilder.Instance.With.Add(values));
        }

        /// <summary>
        /// Sets the "select" statement.
        /// </summary>
        /// <param name="select">The "select" statement.</param>
        /// <returns>The query.</returns>
        public virtual IQuery Select(ISelect select)
        {
            SelectValue = select;
            return this;
        }

        /// <summary>
        /// Sets a raw "select" statement.
        /// <para>You must write the entire statement including the "select" keyword.</para>
        /// </summary>
        /// <param name="select">The "select" statement.</param>
        /// <returns>The query.</returns>
        public virtual IQuery Select(IRawSql select)
        {
            SelectValue = select;
            return this;
        }

        /// <summary>
        /// Sets the "select" statement.
        /// </summary>
        /// <param name="func">Function that returns the "select" statement.</param>
        /// <returns>The query.</returns>
        public virtual IQuery Select(Func<ISelect, ISelect> func)
        {
            return Select(func(SqlBuilder.Instance.Select()));
        }

        /// <summary>
        /// Creates a "select" statement and adds a value to the "select" statement.
        /// </summary>
        /// <param name="value">The value to add to the "select" statement.</param>
        /// <returns>The query.</returns>
        public virtual IQuery Select(object value)
        {
            return Select(SqlBuilder.Instance.Select().Add(value));
        }

        /// <summary>
        /// Creates a "select" statement and adds the elements of the specified array to the end of the "select" statement.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the "select" statement.</param>
        /// <returns>The query.</returns>
        public virtual IQuery Select(params object[] values)
        {
            return Select(SqlBuilder.Instance.Select().Add(values));
        }

        /// <summary>
        /// Creates a "select" statement and adds the elements of the specified collection to the end of the "select"
        /// statement.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the "select" statement.</param>
        /// <returns>The query.</returns>
        public virtual IQuery Select(IEnumerable<object> values)
        {
            return Select(SqlBuilder.Instance.Select().Add(values));
        }

        /// <summary>
        /// Creates a "select" statement and adds a value to the "select" statement.
        /// </summary>
        /// <param name="value">The value to add to the "select" statement.</param>
        /// <returns>The query.</returns>
        public virtual IQuery Select(Expression<Func<object>> value)
        {
            return Select(SqlBuilder.Instance.Select().Add(value));
        }

        /// <summary>
        /// Creates a "select" statement and adds the elements of the specified array to the end of the "select" statement.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the "select" statement.</param>
        /// <returns>The query.</returns>
        public virtual IQuery Select(params Expression<Func<object>>[] values)
        {
            return Select(SqlBuilder.Instance.Select().Add(values));
        }

        /// <summary>
        /// Creates a "select" statement and adds the elements of the specified collection to the end of the "select"
        /// statement.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the "select" statement.</param>
        /// <returns>The query.</returns>
        public virtual IQuery Select(IEnumerable<Expression<Func<object>>> values)
        {
            return Select(SqlBuilder.Instance.Select().Add(values));
        }

        /// <summary>
        /// Sets the "insert" statement.
        /// </summary>
        /// <param name="insert">The "insert" statement.</param>
        /// <returns>The query.</returns>
        public virtual IQuery Insert(IInsert insert)
        {
            InsertValue = insert;
            return this;
        }

        /// <summary>
        /// Sets a raw "insert" statement.
        /// <para>You must write the entire statement including the "insert" keyword.</para>
        /// </summary>
        /// <param name="insert">The "insert" statement.</param>
        /// <returns>The query.</returns>
        public virtual IQuery Insert(IRawSql insert)
        {
            InsertValue = insert;
            return this;
        }

        /// <summary>
        /// Sets the "insert" statement.
        /// </summary>
        /// <param name="func">Function that returns the "insert" statement.</param>
        /// <returns>The query.</returns>
        public virtual IQuery Insert(Func<IInsert, IInsert> func)
        {
            return Insert(func(SqlBuilder.Instance.Insert()));
        }

        /// <summary>
        /// Creates a "insert" statement.
        /// </summary>
        /// <param name="tableName">The table name.</param>
        /// <returns>The query.</returns>
        public virtual IQuery Insert(string tableName)
        {
            return Insert(SqlBuilder.Instance.Insert().Into(tableName));
        }

        /// <summary>
        /// Creates a "insert" statement.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <returns>The query.</returns>
        public virtual IQuery Insert(IAlias alias)
        {
            return Insert(SqlBuilder.Instance.Insert().Into(alias));
        }

        /// <summary>
        /// Creates a "insert" statement.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <typeparam name="T">The type of the table.</typeparam>
        /// <returns>The query.</returns>
        public virtual IQuery Insert<T>(Expression<Func<T>> alias)
        {
            return Insert(SqlBuilder.Instance.Insert().Into(alias));
        }

        /// <summary>
        /// Adds a row with the values to the "insert" statement.
        /// </summary>
        /// <param name="values">The array with the values</param>
        /// <returns>The query.</returns>
        public virtual IQuery Values(params object[] values)
        {
            return Values(SqlBuilder.Instance.ValList.Add(values));
        }

        /// <summary>
        /// Adds a row with the values to the "insert" statement.
        /// </summary>
        /// <param name="values">The values</param>
        /// <returns>The query.</returns>
        public virtual IQuery Values(IValList values)
        {
            if (InsertValues == null)
                InsertValues = new List<IValList>();

            InsertValues.Add(values);
            return this;
        }

        /// <summary>
        /// Sets the "update" statement.
        /// <para>Use the <see cref="o:From"/> method to add the source table.</para>
        /// </summary>
        /// <param name="update">The "update" statement.</param>
        /// <returns>The query.</returns>
        public virtual IQuery Update(IUpdate update)
        {
            UpdateValue = update;
            return this;
        }

        /// <summary>
        /// Sets a raw "update" statement.
        /// <para>You must write the entire statement including the "update" keyword.</para>
        /// <para>Use the <see cref="o:From"/> method to add the source table.</para>
        /// </summary>
        /// <param name="update">The "update" statement.</param>
        /// <returns>The query.</returns>
        public virtual IQuery Update(IRawSql update)
        {
            UpdateValue = update;
            return this;
        }

        /// <summary>
        /// Sets the "update" statement.
        /// <para>Use the <see cref="o:From"/> method to add the source table.</para>
        /// </summary>
        /// <param name="func">Function that returns the "update" statement.</param>
        /// <returns>The query.</returns>
        public virtual IQuery Update(Func<IUpdate, IUpdate> func)
        {
            return Update(func(SqlBuilder.Instance.Update()));
        }

        /// <summary>
        /// Creates a "update" statement.
        /// <para>Use the <see cref="o:From"/> method to add the source table.</para>
        /// </summary>
        /// <returns>The query.</returns>
        public virtual IQuery Update()
        {
            return Update(SqlBuilder.Instance.Update());
        }

        /// <summary>
        /// Adds a "set" clause to the "update" statement.
        /// </summary>
        /// <param name="column">The column name.</param>
        /// <param name="value">The value.</param>
        /// <returns>The query.</returns>
        public virtual IQuery Set(string column, object value)
        {
            return Set(SqlBuilder.Instance.Col(column), value);
        }

        /// <summary>
        /// Adds a "set" clause to the "update" statement.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <param name="value">The value.</param>
        /// <returns>The query.</returns>
        public virtual IQuery Set(IColumn column, object value)
        {
            if (SetValues == null)
                SetValues = new List<Tuple<IColumn, object>>();

            SetValues.Add(Tuple.Create(column, value));
            return this;
        }

        /// <summary>
        /// Adds a "set" clause to the "update" statement.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <param name="value">The value.</param>
        /// <returns>The query.</returns>
        public virtual IQuery Set(Expression<Func<object>> column, object value)
        {
            return Set(SqlBuilder.Instance.Col(column), value);
        }

        /// <summary>
        /// Adds a "set" clause to the "update" statement.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <param name="value">The value.</param>
        /// <returns>The query.</returns>
        public virtual IQuery Set(Expression<Func<object>> column, Expression<Func<object>> value)
        {
            return Set(SqlBuilder.Instance.Col(column), SqlBuilder.Instance.Val(value));
        }

        /// <summary>
        /// Sets the "delete" statement.
        /// <para>Use the <see cref="o:From"/> method to add the source table.</para>
        /// </summary>
        /// <param name="delete">The "delete" statement.</param>
        /// <returns>The query.</returns>
        public virtual IQuery Delete(IDelete delete)
        {
            DeleteValue = delete;
            return this;
        }

        /// <summary>
        /// Sets a raw "delete" statement.
        /// <para>You must write the entire statement including the "delete" keyword.</para>
        /// <para>Use the <see cref="o:From"/> method to add the source table.</para>
        /// </summary>
        /// <param name="delete">The "delete" statement.</param>
        /// <returns>The query.</returns>
        public virtual IQuery Delete(IRawSql delete)
        {
            DeleteValue = delete;
            return this;
        }

        /// <summary>
        /// Sets the "delete" statement.
        /// <para>Use the <see cref="o:From"/> method to add the source table.</para>
        /// </summary>
        /// <param name="func">Function that returns the "delete" statement.</param>
        /// <returns>The query.</returns>
        public virtual IQuery Delete(Func<IDelete, IDelete> func)
        {
            return Delete(func(SqlBuilder.Instance.Delete()));
        }

        /// <summary>
        /// Creates a "delete" statement.
        /// <para>Use the <see cref="o:From"/> method to add the source table.</para>
        /// </summary>
        /// <returns>The query.</returns>
        public virtual IQuery Delete()
        {
            return Delete(SqlBuilder.Instance.Delete());
        }

        /// <summary>
        /// Sets the "from" clause.
        /// </summary>
        /// <param name="from">The "from" clause.</param>
        /// <returns>The query.</returns>
        public virtual IQuery From(IFrom from)
        {
            FromValue = from;
            return this;
        }

        /// <summary>
        /// Sets a raw "from" clause.
        /// <para>You must write the entire clause including the "from" keyword.</para>
        /// </summary>
        /// <param name="from">The "from" clause.</param>
        /// <returns>The query.</returns>
        public virtual IQuery From(IRawSql from)
        {
            FromValue = from;
            return this;
        }

        /// <summary>
        /// Creates a "from" clause.
        /// </summary>
        /// <param name="tableName">The table name.</param>
        /// <returns>The query.</returns>
        public virtual IQueryFrom From(string tableName)
        {
            From(SqlBuilder.Instance.From(tableName));
            return this;
        }

        /// <summary>
        /// Creates a "from" clause.
        /// </summary>
        /// <param name="tableName">The table name.</param>
        /// <param name="aliasName">The alias name.</param>
        /// <returns>The query.</returns>
        public virtual IQueryFrom From(string tableName, string aliasName)
        {
            From(SqlBuilder.Instance.From(tableName, aliasName));
            return this;
        }

        /// <summary>
        /// Creates a "from" clause.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <returns>The query.</returns>
        public virtual IQueryFrom From(IAlias alias)
        {
            From(SqlBuilder.Instance.From(alias));
            return this;
        }

        /// <summary>
        /// Creates a "from" clause with an expression.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <typeparam name="T">The type of the table.</typeparam>
        /// <returns>The query.</returns>
        public virtual IQueryFrom From<T>(Expression<Func<T>> alias)
        {
            From(SqlBuilder.Instance.From(alias));
            return this;
        }

        /// <summary>
        /// Creates a "from" clause with a subquery.
        /// </summary>
        /// <param name="value">The subquery.</param>
        /// <param name="aliasName">The alias name.</param>
        /// <returns>The query.</returns>
        public virtual IQueryFrom From(IQueryFragment value, string aliasName)
        {
            From(SqlBuilder.Instance.From(value, aliasName));
            return this;
        }

        /// <summary>
        /// Creates a "from" clause with a subquery.
        /// </summary>
        /// <param name="value">The subquery.</param>
        /// <param name="alias">The alias.</param>
        /// <returns>The query.</returns>
        public virtual IQueryFrom From(IQueryFragment value, IAlias alias)
        {
            From(SqlBuilder.Instance.From(value, alias));
            return this;
        }

        /// <summary>
        /// Creates a "from" clause with a subquery.
        /// </summary>
        /// <param name="value">The subquery.</param>
        /// <param name="alias">The alias.</param>
        /// <typeparam name="T">The type of the table.</typeparam>
        /// <returns>The query.</returns>
        public virtual IQueryFrom From<T>(IQueryFragment value, Expression<Func<T>> alias)
        {
            From(SqlBuilder.Instance.From(value, alias));
            return this;
        }

        /// <summary>
        /// Additional options.
        /// <para>Use <see cref="IRawSql"/> to add options.</para>
        /// </summary>
        /// <param name="options">The options.</param>
        /// <returns>The query.</returns>
        IQuery IQueryFrom.Options(IQueryFragment options)
        {
            ((IFrom)FromValue).Options(options);
            return this;
        }

        /// <summary>
        /// Creates an "inner join" clause.
        /// </summary>
        /// <value>The query.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public virtual IQueryJoinFrom Inner
        {
            get
            {
                JoinFrom = SqlBuilder.Instance.Inner;
                return this;
            }
        }

        /// <summary>
        /// Creates a "left join" clause.
        /// </summary>
        /// <value>The query.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public virtual IQueryJoinFrom Left
        {
            get
            {
                JoinFrom = SqlBuilder.Instance.Left;
                return this;
            }
        }

        /// <summary>
        /// Creates a "right join" clause.
        /// </summary>
        /// <value>The query.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public virtual IQueryJoinFrom Right
        {
            get
            {
                JoinFrom = SqlBuilder.Instance.Right;
                return this;
            }
        }

        /// <summary>
        /// Creates a "full join" clause.
        /// </summary>
        /// <value>The query.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public virtual IQueryJoinFrom Full
        {
            get
            {
                JoinFrom = SqlBuilder.Instance.Full;
                return this;
            }
        }

        /// <summary>
        /// Creates a "cross join" clause.
        /// </summary>
        /// <value>The query.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public virtual IQueryJoinFrom Cross
        {
            get
            {
                JoinFrom = SqlBuilder.Instance.Cross;
                return this;
            }
        }

        /// <summary>
        /// Adds a "join" clause.
        /// </summary>
        /// <param name="join">The "join" clause.</param>
        /// <returns>The query.</returns>
        public virtual IQuery Join(IJoin join)
        {
            Joins.Add(join);
            return this;
        }

        /// <summary>
        /// Adds a raw "join" clause.
        /// <para>You must write the entire clause including the "join" keyword.</para>
        /// </summary>
        /// <param name="join">The "join" clause.</param>
        /// <returns>The query.</returns>
        public virtual IQuery Join(IRawSql join)
        {
            Joins.Add(join);
            return this;
        }

        /// <summary>
        /// Creates a "join" clause.
        /// </summary>
        /// <param name="tableName">The table name.</param>
        /// <param name="joinType">The type of join, by default inner join.</param>
        /// <returns>The query.</returns>
        public virtual IQueryJoin Join(string tableName, JoinType joinType = JoinType.Inner)
        {
            Joins.Add(SqlBuilder.Instance.Join(tableName, joinType));
            return this;
        }

        /// <summary>
        /// Creates a "join" clause.
        /// </summary>
        /// <param name="tableName">The table name.</param>
        /// <param name="aliasName">The alias name.</param>
        /// <param name="joinType">The type of join, by default inner join.</param>
        /// <returns>The query.</returns>
        public virtual IQueryJoin Join(string tableName, string aliasName, JoinType joinType = JoinType.Inner)
        {
            Joins.Add(SqlBuilder.Instance.Join(tableName, aliasName, joinType));
            return this;
        }

        /// <summary>
        /// Creates a "join" clause.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <param name="joinType">The type of join, by default inner join.</param>
        /// <returns>The query.</returns>
        public virtual IQueryJoin Join(IAlias alias, JoinType joinType = JoinType.Inner)
        {
            Joins.Add(SqlBuilder.Instance.Join(alias, joinType));
            return this;
        }

        /// <summary>
        /// Creates a "join" clause with an expression.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <param name="joinType">The type of join, by default inner join.</param>
        /// <typeparam name="T">The type of the table.</typeparam>
        /// <returns>The query.</returns>
        public virtual IQueryJoin Join<T>(Expression<Func<T>> alias, JoinType joinType = JoinType.Inner)
        {
            Joins.Add(SqlBuilder.Instance.Join(alias, joinType));
            return this;
        }

        /// <summary>
        /// Creates a "join" clause with a subquery.
        /// </summary>
        /// <param name="value">The subquery.</param>
        /// <param name="aliasName">The alias name.</param>
        /// <param name="joinType">The type of join, by default inner join.</param>
        /// <returns>The query.</returns>
        public virtual IQueryJoin Join(IQueryFragment value, string aliasName, JoinType joinType = JoinType.Inner)
        {
            Joins.Add(SqlBuilder.Instance.Join(value, aliasName, joinType));
            return this;
        }

        /// <summary>
        /// Creates a "join" clause with a subquery.
        /// </summary>
        /// <param name="value">The subquery.</param>
        /// <param name="alias">The alias.</param>
        /// <param name="joinType">The type of join, by default inner join.</param>
        /// <returns>The query.</returns>
        public virtual IQueryJoin Join(IQueryFragment value, IAlias alias, JoinType joinType = JoinType.Inner)
        {
            Joins.Add(SqlBuilder.Instance.Join(value, alias, joinType));
            return this;
        }

        /// <summary>
        /// Creates a "join" clause with a subquery.
        /// </summary>
        /// <param name="value">The subquery.</param>
        /// <param name="alias">The alias.</param>
        /// <param name="joinType">The type of join, by default inner join.</param>
        /// <typeparam name="T">The type of the table.</typeparam>
        /// <returns>The query.</returns>
        public virtual IQueryJoin Join<T>(IQueryFragment value, Expression<Func<T>> alias,
            JoinType joinType = JoinType.Inner)
        {
            Joins.Add(SqlBuilder.Instance.Join(value, alias, joinType));
            return this;
        }

        /// <summary>
        /// Creates a "join" clause.
        /// </summary>
        /// <param name="tableName">The table name.</param>
        /// <returns>The query.</returns>
        public virtual IQueryJoin Join(string tableName)
        {
            Joins.Add(JoinFrom.Join(tableName));
            return this;
        }

        /// <summary>
        /// Creates a "join" clause.
        /// </summary>
        /// <param name="tableName">The table name.</param>
        /// <param name="aliasName">The alias name.</param>
        /// <returns>The query.</returns>
        public virtual IQueryJoin Join(string tableName, string aliasName)
        {
            Joins.Add(JoinFrom.Join(tableName, aliasName));
            return this;
        }

        /// <summary>
        /// Creates a "join" clause.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <returns>The query.</returns>
        public virtual IQueryJoin Join(IAlias alias)
        {
            Joins.Add(JoinFrom.Join(alias));
            return this;
        }

        /// <summary>
        /// Creates a "join" clause with an expression.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <typeparam name="T">The type of the table.</typeparam>
        /// <returns>The query.</returns>
        public virtual IQueryJoin Join<T>(Expression<Func<T>> alias)
        {
            Joins.Add(JoinFrom.Join(alias));
            return this;
        }

        /// <summary>
        /// Creates a "join" clause with a subquery.
        /// </summary>
        /// <param name="value">The subquery.</param>
        /// <param name="aliasName">The alias name.</param>
        /// <returns>The query.</returns>
        public virtual IQueryJoin Join(IQueryFragment value, string aliasName)
        {
            Joins.Add(JoinFrom.Join(value, aliasName));
            return this;
        }

        /// <summary>
        /// Creates a "join" clause with a subquery.
        /// </summary>
        /// <param name="value">The subquery.</param>
        /// <param name="alias">The alias.</param>
        /// <returns>The query.</returns>
        public virtual IQueryJoin Join(IQueryFragment value, IAlias alias)
        {
            Joins.Add(JoinFrom.Join(value, alias));
            return this;
        }

        /// <summary>
        /// Creates a "join" clause with a subquery.
        /// </summary>
        /// <param name="value">The subquery.</param>
        /// <param name="alias">The alias.</param>
        /// <typeparam name="T">The type of the table.</typeparam>
        /// <returns>The query.</returns>
        public virtual IQueryJoin Join<T>(IQueryFragment value, Expression<Func<T>> alias)
        {
            Joins.Add(JoinFrom.Join(value, alias));
            return this;
        }

        /// <summary>
        /// Adds an "on" clause.
        /// </summary>
        /// <param name="on">The "on" condition.</param>
        /// <returns>The query.</returns>
        public virtual IQueryJoin On(IQueryFragment on)
        {
            ((IJoin)Joins.Last()).On(on);
            return this;
        }

        /// <summary>
        /// Adds an "on" clause.
        /// </summary>
        /// <param name="on">The "on" condition.</param>
        /// <returns>The query.</returns>
        public virtual IQueryJoin On(Expression<Func<bool>> on)
        {
            ((IJoin)Joins.Last()).On(on);
            return this;
        }

        /// <summary>
        /// Additional options.
        /// <para>Use <see cref="IRawSql"/> to add options.</para>
        /// </summary>
        /// <param name="options">The options.</param>
        /// <returns>The query.</returns>
        IQueryJoin IQueryJoin.Options(IQueryFragment options)
        {
            ((IJoin)Joins.Last()).Options(options);
            return this;
        }

        /// <summary>
        /// Sets the "where" clause.
        /// </summary>
        /// <param name="where">The "where" clause.</param>
        /// <returns>The query.</returns>
        public virtual IQuery Where(IOperator where)
        {
            WhereValue = where;
            return this;
        }

        /// <summary>
        /// Sets the "where" clause.
        /// <para>You must write the entire clause including the "where" keyword.</para>
        /// </summary>
        /// <param name="where">The "where" clause.</param>
        /// <returns>The query.</returns>
        public virtual IQuery Where(IRawSql where)
        {
            WhereValue = where;
            return this;
        }

        /// <summary>
        /// Sets the "where" clause.
        /// </summary>
        /// <param name="where">The "where" clause.</param>
        /// <returns>The query.</returns>
        public virtual IQuery Where(Expression<Func<bool>> where)
        {
            return Where(SqlBuilder.Instance.Op(where));
        }

        /// <summary>
        /// Sets the "group by" clause.
        /// </summary>
        /// <param name="groupBy">The "group by" clause.</param>
        /// <returns>The query.</returns>
        public virtual IQuery GroupBy(IValList groupBy)
        {
            GroupByValue = groupBy;
            return this;
        }

        /// <summary>
        /// Sets a raw "group by" clause.
        /// <para>You must write the entire clause including the "group by" keyword.</para>
        /// </summary>
        /// <param name="groupBy">The "group by" clause.</param>
        /// <returns>The query.</returns>
        public virtual IQuery GroupBy(IRawSql groupBy)
        {
            GroupByValue = groupBy;
            return this;
        }

        /// <summary>
        /// Sets the "group by" clause.
        /// </summary>
        /// <param name="func">Function that returns the "group by" clause.</param>
        /// <returns>The query.</returns>
        public virtual IQuery GroupBy(Func<IValList, IValList> func)
        {
            return GroupBy(func(SqlBuilder.Instance.ValList));
        }

        /// <summary>
        /// Creates a "group by" clause and adds a value to the "group by" clause.
        /// </summary>
        /// <param name="value">The value to add to the "group by" clause.</param>
        /// <returns>The query.</returns>
        public virtual IQuery GroupBy(object value)
        {
            return GroupBy(SqlBuilder.Instance.ValList.Add(value));
        }

        /// <summary>
        /// Creates a "group by" clause and adds the elements of the specified array to the end of the "group by" clause.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// "group by" clause.</param>
        /// <returns>The query.</returns>
        public virtual IQuery GroupBy(params object[] values)
        {
            return GroupBy(SqlBuilder.Instance.ValList.Add(values));
        }

        /// <summary>
        /// Creates a "group by" clause and adds the elements of the specified collection to the end of the "group by"
        /// clause.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the "group by" clause.</param>
        /// <returns>The query.</returns>
        public virtual IQuery GroupBy(IEnumerable<object> values)
        {
            return GroupBy(SqlBuilder.Instance.ValList.Add(values));
        }

        /// <summary>
        /// Creates a "group by" clause and adds a value to the "group by" clause.
        /// </summary>
        /// <param name="value">The value to add to the "group by" clause.</param>
        /// <returns>The query.</returns>
        public virtual IQuery GroupBy(Expression<Func<object>> value)
        {
            return GroupBy(SqlBuilder.Instance.ValList.Add(value));
        }

        /// <summary>
        /// Creates a "group by" clause and adds the elements of the specified array to the end of the "group by" clause.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// "group by" clause.</param>
        /// <returns>The query.</returns>
        public virtual IQuery GroupBy(params Expression<Func<object>>[] values)
        {
            return GroupBy(SqlBuilder.Instance.ValList.Add(values));
        }

        /// <summary>
        /// Creates a "group by" clause and adds the elements of the specified collection to the end of the "group by"
        /// clause.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the "group by" clause.</param>
        /// <returns>The query.</returns>
        public virtual IQuery GroupBy(IEnumerable<Expression<Func<object>>> values)
        {
            return GroupBy(SqlBuilder.Instance.ValList.Add(values));
        }

        /// <summary>
        /// Sets the "having" clause.
        /// </summary>
        /// <param name="having">The "having" clause.</param>
        /// <returns>The query.</returns>
        public virtual IQuery Having(IOperator having)
        {
            HavingValue = having;
            return this;
        }

        /// <summary>
        /// Sets a raw "having" clause.
        /// <para>You must write the entire clause including the "having" keyword.</para>
        /// </summary>
        /// <param name="having">The "having" clause.</param>
        /// <returns>The query.</returns>
        public virtual IQuery Having(IRawSql having)
        {
            HavingValue = having;
            return this;
        }

        /// <summary>
        /// Sets the "having" clause.
        /// </summary>
        /// <param name="having">The "having" clause.</param>
        /// <returns>The query.</returns>
        public virtual IQuery Having(Expression<Func<bool>> having)
        {
            return Having(SqlBuilder.Instance.Op(having));
        }

        /// <summary>
        /// Sets the "order by" clause.
        /// </summary>
        /// <param name="orderBy">The "order by" clause.</param>
        /// <returns>The query.</returns>
        public virtual IQuery OrderBy(IOrderBy orderBy)
        {
            OrderByValue = orderBy;
            return this;
        }

        /// <summary>
        /// Sets a raw "order by" clause.
        /// <para>You must write the entire clause including the "order by" keyword.</para>
        /// </summary>
        /// <param name="orderBy">The "order by" clause.</param>
        /// <returns>The query.</returns>
        public virtual IQuery OrderBy(IRawSql orderBy)
        {
            OrderByValue = orderBy;
            return this;
        }

        /// <summary>
        /// Sets the "order by" clause.
        /// </summary>
        /// <param name="func">Function that returns the "order by" clause.</param>
        /// <returns>The query.</returns>
        public virtual IQuery OrderBy(Func<IOrderBy, IOrderBy> func)
        {
            return OrderBy(func(SqlBuilder.Instance.OrderBy()));
        }

        /// <summary>
        /// Sets the "offset" clause.
        /// </summary>
        /// <param name="offset">The "offset" clause.</param>
        /// <returns>The query.</returns>
        public virtual IQuery Offset(IOffset offset)
        {
            OffsetValue = offset;
            return this;
        }

        /// <summary>
        /// Sets a raw "offset" clause.
        /// <para>You must write the entire clause.</para>
        /// </summary>
        /// <param name="offset">The "offset" clause.</param>
        /// <returns>The query.</returns>
        public virtual IQuery Offset(IRawSql offset)
        {
            OffsetValue = offset;
            return this;
        }

        /// <summary>
        /// Creates or adds an "offset" clause.
        /// </summary>
        /// <param name="offset">The number of rows to skip.</param>
        /// <returns>The query.</returns>
        public virtual IQuery Offset(object offset)
        {
            if (OffsetValue is IOffset offsetValue)
                offsetValue.Offset(offset);
            else
                OffsetValue = SqlBuilder.Instance.Offset(offset);
            return this;
        }

        /// <summary>
        /// Creates an "offset fetch" clause.
        /// </summary>
        /// <param name="offset">The number of rows to skip.</param>
        /// <param name="fetch">The number of rows to return.</param>
        /// <returns>The query.</returns>
        public virtual IQuery Offset(object offset, object fetch)
        {
            OffsetValue = SqlBuilder.Instance.Offset(offset, fetch);
            return this;
        }

        /// <summary>
        /// Creates or adds "fetch" clause.
        /// </summary>
        /// <param name="fetch">The number of rows to return.</param>
        /// <returns>The query.</returns>
        public virtual IQuery Fetch(object fetch)
        {
            if (OffsetValue is IOffset offsetValue)
                offsetValue.Fetch(fetch);
            else
                OffsetValue = SqlBuilder.Instance.Fetch(fetch);
            return this;
        }

        /// <summary>
        /// Sets a "select count(*)" and removes the "order by" and "offset fetch" of the query.
        /// </summary>
        /// <returns>The query</returns>
        public virtual IQuery Count()
        {
            SelectValue = SqlBuilder.Instance.Select().Add(SqlFn.Count());
            OrderByValue = null;
            OffsetValue = null;
            return this;
        }

        /// <summary>
        /// Compiles the fragment.
        /// </summary>
        /// <param name="queryBuilder">The query builder.</param>
        /// <param name="engine">The engine.</param>
        public virtual void Compile(QueryBuilder queryBuilder, IEngine engine)
        {
            if (BeforeValue != null)
                queryBuilder.WriteFragment(BeforeValue).Write(" ");

            if (WithValue != null)
                queryBuilder.WriteFragment(WithValue).Write(" ");

            if (InsertValue != null)
                WriteInsert(queryBuilder, engine);

            if (SelectValue != null)
            {
                queryBuilder.WriteFragment(SelectValue).Write(" ");
            }
            else if (DeleteValue != null)
            {
                WriteDelete(queryBuilder, engine);
            }

            if (UpdateValue != null)
            {
                WriteUpdate(queryBuilder, engine);
            }
            else
            {
                if (FromValue != null)
                    queryBuilder.WriteFragment(FromValue).Write(" ");

                foreach (IQueryFragment join in Joins)
                {
                    queryBuilder.WriteFragment(join).Write(" ");
                }
            }

            if (WhereValue != null)
            {
                if (!(WhereValue is IRawSql))
                    queryBuilder.Write("WHERE ");

                queryBuilder.WriteFragment(WhereValue, false).Write(" ");
            }

            if (GroupByValue != null)
            {
                if (!(GroupByValue is IRawSql))
                    queryBuilder.Write("GROUP BY ");

                queryBuilder.WriteFragment(GroupByValue).Write(" ");
            }

            if (HavingValue != null)
            {
                if (!(HavingValue is IRawSql))
                    queryBuilder.Write("HAVING ");

                queryBuilder.WriteFragment(HavingValue, false).Write(" ");
            }

            if (OrderByValue != null)
                queryBuilder.WriteFragment(OrderByValue).Write(" ");

            if (OffsetValue != null)
                queryBuilder.WriteFragment(OffsetValue).Write(" ");

            if (AfterValue != null)
                queryBuilder.WriteFragment(AfterValue).Write(" ");

            queryBuilder.RemoveLast(1);
        }

        /// <summary>
        /// Writes the "insert" statement.
        /// </summary>
        /// <param name="queryBuilder">The query builder.</param>
        /// <param name="engine">The engine.</param>
        protected virtual void WriteInsert(QueryBuilder queryBuilder, IEngine engine)
        {
            queryBuilder.WriteFragment(InsertValue).Write(" ");

            if (InsertValues != null && InsertValues.Count > 0)
            {
                if (InsertValues.Count > 1 && engine.Options.InsertWithUnion)
                {
                    string separator = " UNION ALL ";
                    foreach (IValList row in InsertValues)
                    {
                        queryBuilder.Write("SELECT ").WriteFragment(row);

                        if (engine.Options.FromDummyName != null)
                            queryBuilder.Write(" ").WriteFragment(SqlBuilder.Instance.FromDummy);

                        queryBuilder.Write(separator);
                    }
                    queryBuilder.RemoveLast(separator.Length).Write(" ");
                }
                else
                {
                    queryBuilder.Write("VALUES ");
                    string separator = ", ";
                    foreach (IValList row in InsertValues)
                    {
                        queryBuilder.Write("(").WriteFragment(row).Write(")").Write(separator);
                    }
                    queryBuilder.RemoveLast(separator.Length).Write(" ");
                }
            }
        }

        /// <summary>
        /// Writes the "update" statement.
        /// </summary>
        /// <param name="queryBuilder">The query builder.</param>
        /// <param name="engine">The engine.</param>
        protected virtual void WriteUpdate(QueryBuilder queryBuilder, IEngine engine)
        {
            queryBuilder.WriteFragment(UpdateValue).Write(" ");

            if (engine.Options.UpdateWithFrom)
            {
                if (SetValues != null)
                {
                    if (SetValues[0].Item1.TableName == null)
                    {
                        throw new CompileException("The set column must have the table name.");
                    }

                    queryBuilder.WriteName(SetValues[0].Item1.TableName).Write(" ");
                    WriteSet(queryBuilder, engine);
                }

                if (FromValue != null)
                    queryBuilder.WriteFragment(FromValue).Write(" ");

                foreach (IQueryFragment join in Joins)
                {
                    queryBuilder.WriteFragment(join).Write(" ");
                }
            }
            else
            {
                if (FromValue != null)
                {
                    IFrom from = FromValue as IFrom;

                    if (from == null)
                        throw new CompileException($"The \"from\" value must be a \"{typeof(IFrom)}\" instance.");

                    from.Compile(queryBuilder, engine, false);
                    queryBuilder.Write(" ");
                }

                foreach (IQueryFragment join in Joins)
                {
                    queryBuilder.WriteFragment(join).Write(" ");
                }

                if (SetValues != null)
                {
                    WriteSet(queryBuilder, engine);
                }
            }
        }

        /// <summary>
        /// Writes the "set" clause.
        /// </summary>
        /// <param name="queryBuilder">The query builder.</param>
        /// <param name="engine">The engine.</param>
        protected virtual void WriteSet(QueryBuilder queryBuilder, IEngine engine)
        {
            queryBuilder.Write("SET ");

            string separator = ", ";
            foreach (var value in SetValues)
            {
                value.Item1.Compile(queryBuilder, engine, engine.Options.UpdateSetWithTableName);
                queryBuilder.Write(" = ").WriteValue(value.Item2).Write(separator);
            }
            queryBuilder.RemoveLast(separator.Length).Write(" ");
        }

        /// <summary>
        /// Writes the "delete" statement.
        /// </summary>
        /// <param name="queryBuilder">The query builder.</param>
        /// <param name="engine">The engine.</param>
        protected virtual void WriteDelete(QueryBuilder queryBuilder, IEngine engine)
        {
            queryBuilder.WriteFragment(DeleteValue).Write(" ");

            if (engine.Options.DeleteWithAlias && DeleteValue is IDelete delete && delete.Alias.Count() == 0)
            {
                IFrom from = FromValue as IFrom;
                if (from == null)
                {
                    throw new CompileException(
                        $"The \"from\" value must be a \"{typeof(IFrom)}\" instance or specify the alias to delete.");
                }

                string aliasName = from.AliasOrTableName;
                if (aliasName == null)
                    throw new CompileException("The \"from\" must have an alias or a table name.");

                queryBuilder.WriteName(aliasName).Write(" ");
            }
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return ToStringBuilder.Build(b => b
                .IfNotNull(BeforeValue, x => b.WriteFragment(x).Write(" "))
                .IfNotNull(WithValue, x => b.WriteFragment(x).Write(" "))
                .IfNotNull(InsertValue, x => b.WriteFragment(x).Write(" "))
                .IfNotNull(InsertValues, x => b.Write("VALUES ")
                    .Join(", ", x, item => b.Write("(").WriteFragment(item).Write(")")).Write(" "))
                .IfNotNull(SelectValue, x => b.WriteFragment(x).Write(" "))
                .IfNotNull(UpdateValue, x => b.WriteFragment(x).Write(" "))
                .IfNotNull(SetValues, x => b.Write("SET ")
                    .Join(", ", x, item => b.WriteFragment(item.Item1).Write(" = ").WriteValue(item.Item2)).Write(" "))
                .IfNotNull(DeleteValue, x => b.WriteFragment(x).Write(" "))
                .IfNotNull(FromValue, x => b.WriteFragment(x).Write(" "))
                .IfNotNull(DeleteValue, x => b.WriteFragment(x).Write(" "))
                .ForEach(Joins, x => b.WriteFragment(x).Write(" "))
                .IfNotNull(WhereValue, x => b.IfNot(x is IRawSql, () => b.Write("WHERE ")).WriteFragment(x).Write(" "))
                .IfNotNull(GroupByValue, x => b.IfNot(x is IRawSql, () => b.Write("GROUP BY ")).WriteFragment(x).Write(" "))
                .IfNotNull(HavingValue, x => b.IfNot(x is IRawSql, () => b.Write("HAVING ")).WriteFragment(x).Write(" "))
                .IfNotNull(OrderByValue, x => b.WriteFragment(x).Write(" "))
                .IfNotNull(OffsetValue, x => b.WriteFragment(x).Write(" "))
                .IfNotNull(AfterValue, x => b.WriteFragment(x).Write(" "))
                .RemoveLast(1));
        }
    }
}