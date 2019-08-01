using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Suilder.Core
{
    /// <summary>
    /// A query.
    /// </summary>
    public interface IQuery : IQueryFragment, ISubQuery
    {
        /// <summary>
        /// Sets a value to write before the query.
        /// </summary>
        /// <param name="value">The value to write before the query.</param>
        /// <returns>The query.</returns>
        IQuery Before(IQueryFragment value);

        /// <summary>
        /// Sets a value to write after the query.
        /// </summary>
        /// <param name="value">The value to write after the query.</param>
        /// <returns>The query.</returns>
        IQuery After(IQueryFragment value);

        /// <summary>
        /// Sets the "with" clause.
        /// </summary>
        /// <param name="with">The "with" clause.</param>
        /// <returns>The query.</returns>
        IQuery With(IWith with);

        /// <summary>
        /// Sets a raw "with" clause.
        /// <para>You must write the entire clause including the "with" keyword.</para>
        /// </summary>
        /// <param name="with">The "with" clause.</param>
        /// <returns>The query.</returns>
        IQuery With(IRawSql with);

        /// <summary>
        /// Sets the "with" clause.
        /// </summary>
        /// <param name="func">Function that returns the "with" clause.</param>
        /// <returns>The query.</returns>
        IQuery With(Func<IWith, IWith> func);

        /// <summary>
        /// Creates a "with" clause and adds a value to the "with" clause.
        /// </summary>
        /// <param name="value">The value to add to the "with" clause.</param>
        /// <returns>The query.</returns>
        IQuery With(IQueryFragment value);

        /// <summary>
        /// Creates a "with" clause and adds the elements of the specified array to the end of the "with" clause.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the "with" clause.</param>
        /// <returns>The query.</returns>
        IQuery With(params IQueryFragment[] values);

        /// <summary>
        /// Creates a "with" clause and adds the elements of the specified collection to the end of the "with"
        /// clause.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the "with" clause.</param>
        /// <returns>The query.</returns>
        IQuery With(IEnumerable<IQueryFragment> values);

        /// <summary>
        /// Sets the "select" statement.
        /// </summary>
        /// <param name="select">The "select" statement.</param>
        /// <returns>The query.</returns>
        IQuery Select(ISelect select);

        /// <summary>
        /// Sets a raw "select" statement.
        /// <para>You must write the entire statement including the "select" keyword.</para>
        /// </summary>
        /// <param name="select">The "select" statement.</param>
        /// <returns>The query.</returns>
        IQuery Select(IRawSql select);

        /// <summary>
        /// Sets the "select" statement.
        /// </summary>
        /// <param name="func">Function that returns the "select" statement.</param>
        /// <returns>The query.</returns>
        IQuery Select(Func<ISelect, ISelect> func);

        /// <summary>
        /// Creates a "select" statement and adds a value to the "select" statement.
        /// </summary>
        /// <param name="value">The value to add to the "select" statement.</param>
        /// <returns>The query.</returns>
        IQuery Select(object value);

        /// <summary>
        /// Creates a "select" statement and adds the elements of the specified array to the end of the "select" statement.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the "select" statement.</param>
        /// <returns>The query.</returns>
        IQuery Select(params object[] values);

        /// <summary>
        /// Creates a "select" statement and adds the elements of the specified collection to the end of the "select"
        /// statement.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the "select" statement.</param>
        /// <returns>The query.</returns>
        IQuery Select(IEnumerable<object> values);

        /// <summary>
        /// Creates a "select" statement and adds a value to the "select" statement.
        /// </summary>
        /// <param name="value">The value to add to the "select" statement.</param>
        /// <returns>The query.</returns>
        IQuery Select(Expression<Func<object>> value);

        /// <summary>
        /// Creates a "select" statement and adds the elements of the specified array to the end of the "select" statement.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the "select" statement.</param>
        /// <returns>The query.</returns>
        IQuery Select(params Expression<Func<object>>[] values);

        /// <summary>
        /// Creates a "select" statement and adds the elements of the specified collection to the end of the "select"
        /// statement.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the "select" statement.</param>
        /// <returns>The query.</returns>
        IQuery Select(IEnumerable<Expression<Func<object>>> values);

        /// <summary>
        /// Sets the "insert" statement.
        /// </summary>
        /// <param name="insert">The "insert" statement.</param>
        /// <returns>The query.</returns>
        IQuery Insert(IInsert insert);

        /// <summary>
        /// Sets a raw "insert" statement.
        /// <para>You must write the entire statement including the "insert" keyword.</para>
        /// </summary>
        /// <param name="insert">The "insert" statement.</param>
        /// <returns>The query.</returns>
        IQuery Insert(IRawSql insert);

        /// <summary>
        /// Sets the "insert" statement.
        /// </summary>
        /// <param name="func">Function that returns the "insert" statement.</param>
        /// <returns>The query.</returns>
        IQuery Insert(Func<IInsert, IInsert> func);

        /// <summary>
        /// Creates a "insert" statement.
        /// </summary>
        /// <param name="tableName">The table name.</param>
        /// <returns>The query.</returns>
        IQuery Insert(string tableName);

        /// <summary>
        /// Creates a "insert" statement.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <returns>The query.</returns>
        IQuery Insert(IAlias alias);

        /// <summary>
        /// Creates a "insert" statement.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <typeparam name="T">The type of the table.</typeparam>
        /// <returns>The query.</returns>
        IQuery Insert<T>(Expression<Func<T>> alias);

        /// <summary>
        /// Adds a row with the values to the "insert" statement.
        /// </summary>
        /// <param name="values">The array with the values</param>
        /// <returns>The query.</returns>
        IQuery Values(params object[] values);

        /// <summary>
        /// Adds a row with the values to the "insert" statement.
        /// </summary>
        /// <param name="values">The values</param>
        /// <returns>The query.</returns>
        IQuery Values(IValList values);

        /// <summary>
        /// Sets the "update" statement.
        /// <para>Use the <see cref="o:From"/> method to add the source table.</para>
        /// </summary>
        /// <param name="update">The "update" statement.</param>
        /// <returns>The query.</returns>
        IQuery Update(IUpdate update);

        /// <summary>
        /// Sets a raw "update" statement.
        /// <para>You must write the entire statement including the "update" keyword.</para>
        /// <para>Use the <see cref="o:From"/> method to add the source table.</para>
        /// </summary>
        /// <param name="update">The "update" statement.</param>
        /// <returns>The query.</returns>
        IQuery Update(IRawSql update);

        /// <summary>
        /// Sets the "update" statement.
        /// <para>Use the <see cref="o:From"/> method to add the source table.</para>
        /// </summary>
        /// <param name="func">Function that returns the "update" statement.</param>
        /// <returns>The query.</returns>
        IQuery Update(Func<IUpdate, IUpdate> func);

        /// <summary>
        /// Creates a "update" statement.
        /// <para>Use the <see cref="o:From"/> method to add the source table.</para>
        /// </summary>
        /// <returns>The query.</returns>
        IQuery Update();

        /// <summary>
        /// Adds a "set" clause to the "update" statement.
        /// </summary>
        /// <param name="column">The column name.</param>
        /// <param name="value">The value.</param>
        /// <returns>The query.</returns>
        IQuery Set(string column, object value);

        /// <summary>
        /// Adds a "set" clause to the "update" statement.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <param name="value">The value.</param>
        /// <returns>The query.</returns>
        IQuery Set(IColumn column, object value);

        /// <summary>
        /// Adds a "set" clause to the "update" statement.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <param name="value">The value.</param>
        /// <returns>The query.</returns>
        IQuery Set(Expression<Func<object>> column, object value);

        /// <summary>
        /// Adds a "set" clause to the "update" statement.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <param name="value">The value.</param>
        /// <returns>The query.</returns>
        IQuery Set(Expression<Func<object>> column, Expression<Func<object>> value);

        /// <summary>
        /// Sets the "delete" statement.
        /// <para>Use the <see cref="o:From"/> method to add the source table.</para>
        /// </summary>
        /// <param name="delete">The "delete" statement.</param>
        /// <returns>The query.</returns>
        IQuery Delete(IDelete delete);

        /// <summary>
        /// Sets a raw "delete" statement.
        /// <para>You must write the entire statement including the "delete" keyword.</para>
        /// <para>Use the <see cref="o:From"/> method to add the source table.</para>
        /// </summary>
        /// <param name="delete">The "delete" statement.</param>
        /// <returns>The query.</returns>
        IQuery Delete(IRawSql delete);

        /// <summary>
        /// Sets the "delete" statement.
        /// <para>Use the <see cref="o:From"/> method to add the source table.</para>
        /// </summary>
        /// <param name="func">Function that returns the "delete" statement.</param>
        /// <returns>The query.</returns>
        IQuery Delete(Func<IDelete, IDelete> func);

        /// <summary>
        /// Creates a "delete" statement.
        /// <para>Use the <see cref="o:From"/> method to add the source table.</para>
        /// </summary>
        /// <returns>The query.</returns>
        IQuery Delete();

        /// <summary>
        /// Sets the "from" clause.
        /// </summary>
        /// <param name="from">The "from" clause.</param>
        /// <returns>The query.</returns>
        IQuery From(IFrom from);

        /// <summary>
        /// Sets a raw "from" clause.
        /// <para>You must write the entire clause including the "from" keyword.</para>
        /// </summary>
        /// <param name="from">The "from" clause.</param>
        /// <returns>The query.</returns>
        IQuery From(IRawSql from);

        /// <summary>
        /// Creates a "from" clause.
        /// </summary>
        /// <param name="tableName">The table name.</param>
        /// <returns>The query.</returns>
        IQueryFrom From(string tableName);

        /// <summary>
        /// Creates a "from" clause.
        /// </summary>
        /// <param name="tableName">The table name.</param>
        /// <param name="aliasName">The alias name.</param>
        /// <returns>The query.</returns>
        IQueryFrom From(string tableName, string aliasName);

        /// <summary>
        /// Creates a "from" clause.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <returns>The query.</returns>
        IQueryFrom From(IAlias alias);

        /// <summary>
        /// Creates a "from" clause with an expression.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <typeparam name="T">The type of the table.</typeparam>
        /// <returns>The query.</returns>
        IQueryFrom From<T>(Expression<Func<T>> alias);

        /// <summary>
        /// Creates a "from" clause with a subquery.
        /// </summary>
        /// <param name="value">The subquery.</param>
        /// <param name="aliasName">The alias name.</param>
        /// <returns>The query.</returns>
        IQueryFrom From(IQueryFragment value, string aliasName);

        /// <summary>
        /// Creates a "from" clause with a subquery.
        /// </summary>
        /// <param name="value">The subquery.</param>
        /// <param name="alias">The alias.</param>
        /// <returns>The query.</returns>
        IQueryFrom From(IQueryFragment value, IAlias alias);

        /// <summary>
        /// Creates a "from" clause with a subquery.
        /// </summary>
        /// <param name="value">The subquery.</param>
        /// <param name="alias">The alias.</param>
        /// <typeparam name="T">The type of the table.</typeparam>
        /// <returns>The query.</returns>
        IQueryFrom From<T>(IQueryFragment value, Expression<Func<T>> alias);

        /// <summary>
        /// Creates an "inner join" clause.
        /// </summary>
        /// <value>The query.</value>
        IQueryJoinFrom Inner { get; }

        /// <summary>
        /// Creates a "left join" clause.
        /// </summary>
        /// <value>The query.</value>
        IQueryJoinFrom Left { get; }

        /// <summary>
        /// Creates a "right join" clause.
        /// </summary>
        /// <value>The query.</value>
        IQueryJoinFrom Right { get; }

        /// <summary>
        /// Creates a "full join" clause.
        /// </summary>
        /// <value>The query.</value>
        IQueryJoinFrom Full { get; }

        /// <summary>
        /// Creates a "cross join" clause.
        /// </summary>
        /// <value>The query.</value>
        IQueryJoinFrom Cross { get; }

        /// <summary>
        /// Adds a "join" clause.
        /// </summary>
        /// <param name="join">The "join" clause.</param>
        /// <returns>The query.</returns>
        IQuery Join(IJoin join);

        /// <summary>
        /// Adds a raw "join" clause.
        /// <para>You must write the entire clause including the "join" keyword.</para>
        /// </summary>
        /// <param name="join">The "join" clause.</param>
        /// <returns>The query.</returns>
        IQuery Join(IRawSql join);

        /// <summary>
        /// Creates a "join" clause.
        /// </summary>
        /// <param name="tableName">The table name.</param>
        /// <param name="joinType">The type of join, by default inner join.</param>
        /// <returns>The query.</returns>
        IQueryJoin Join(string tableName, JoinType joinType = JoinType.Inner);

        /// <summary>
        /// Creates a "join" clause.
        /// </summary>
        /// <param name="tableName">The table name.</param>
        /// <param name="aliasName">The alias name.</param>
        /// <param name="joinType">The type of join, by default inner join.</param>
        /// <returns>The query.</returns>
        IQueryJoin Join(string tableName, string aliasName, JoinType joinType = JoinType.Inner);

        /// <summary>
        /// Creates a "join" clause.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <param name="joinType">The type of join, by default inner join.</param>
        /// <returns>The query.</returns>
        IQueryJoin Join(IAlias alias, JoinType joinType = JoinType.Inner);

        /// <summary>
        /// Creates a "join" clause with an expression
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <param name="joinType">The type of join, by default inner join.</param>
        /// <typeparam name="T">The type of the table.</typeparam>
        /// <returns>The query.</returns>
        IQueryJoin Join<T>(Expression<Func<T>> alias, JoinType joinType = JoinType.Inner);

        /// <summary>
        /// Creates a "join" clause with a subquery.
        /// </summary>
        /// <param name="value">The subquery.</param>
        /// <param name="aliasName">The alias name.</param>
        /// <param name="joinType">The type of join, by default inner join.</param>
        /// <returns>The query.</returns>
        IQueryJoin Join(IQueryFragment value, string aliasName, JoinType joinType = JoinType.Inner);

        /// <summary>
        /// Creates a "join" clause with a subquery.
        /// </summary>
        /// <param name="value">The subquery.</param>
        /// <param name="alias">The alias.</param>
        /// <param name="joinType">The type of join, by default inner join.</param>
        /// <returns>The query.</returns>
        IQueryJoin Join(IQueryFragment value, IAlias alias, JoinType joinType = JoinType.Inner);

        /// <summary>
        /// Creates a "join" clause with a subquery.
        /// </summary>
        /// <param name="value">The subquery.</param>
        /// <param name="alias">The alias.</param>
        /// <param name="joinType">The type of join, by default inner join.</param>
        /// <typeparam name="T">The type of the table.</typeparam>
        /// <returns>The query.</returns>
        IQueryJoin Join<T>(IQueryFragment value, Expression<Func<T>> alias, JoinType joinType = JoinType.Inner);

        /// <summary>
        /// Sets the "where" clause.
        /// </summary>
        /// <param name="where">The "where" clause.</param>
        /// <returns>The query.</returns>
        IQuery Where(IOperator where);

        /// <summary>
        /// Sets the "where" clause.
        /// <para>You must write the entire clause including the "where" keyword.</para>
        /// </summary>
        /// <param name="where">The "where" clause.</param>
        /// <returns>The query.</returns>
        IQuery Where(IRawSql where);

        /// <summary>
        /// Sets the "where" clause.
        /// </summary>
        /// <param name="where">The "where" clause.</param>
        /// <returns>The query.</returns>
        IQuery Where(Expression<Func<bool>> where);

        /// <summary>
        /// Sets the "group by" clause.
        /// </summary>
        /// <param name="groupBy">The "group by" clause.</param>
        /// <returns>The query.</returns>
        IQuery GroupBy(IValList groupBy);

        /// <summary>
        /// Sets a raw "group by" clause.
        /// <para>You must write the entire clause including the "group by" keyword.</para>
        /// </summary>
        /// <param name="groupBy">The "group by" clause.</param>
        /// <returns>The query.</returns>
        IQuery GroupBy(IRawSql groupBy);

        /// <summary>
        /// Sets the "group by" clause.
        /// </summary>
        /// <param name="func">Function that returns the "group by" clause.</param>
        /// <returns>The query.</returns>
        IQuery GroupBy(Func<IValList, IValList> func);

        /// <summary>
        /// Creates a "group by" clause and adds a value to the "group by" clause.
        /// </summary>
        /// <param name="value">The value to add to the "group by" clause.</param>
        /// <returns>The query.</returns>
        IQuery GroupBy(object value);

        /// <summary>
        /// Creates a "group by" clause and adds the elements of the specified array to the end of the "group by" clause.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// "group by" clause.</param>
        /// <returns>The query.</returns>
        IQuery GroupBy(params object[] values);

        /// <summary>
        /// Creates a "group by" clause and adds the elements of the specified collection to the end of the "group by"
        /// clause.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the "group by" clause.</param>
        /// <returns>The query.</returns>
        IQuery GroupBy(IEnumerable<object> values);

        /// <summary>
        /// Creates a "group by" clause and adds a value to the "group by" clause.
        /// </summary>
        /// <param name="value">The value to add to the "group by" clause.</param>
        /// <returns>The query.</returns>
        IQuery GroupBy(Expression<Func<object>> value);

        /// <summary>
        /// Creates a "group by" clause and adds the elements of the specified array to the end of the "group by" clause.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// "group by" clause.</param>
        /// <returns>The query.</returns>
        IQuery GroupBy(params Expression<Func<object>>[] values);

        /// <summary>
        /// Creates a "group by" clause and adds the elements of the specified collection to the end of the "group by"
        /// clause.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the "group by" clause.</param>
        /// <returns>The query.</returns>
        IQuery GroupBy(IEnumerable<Expression<Func<object>>> values);

        /// <summary>
        /// Sets the "having" clause.
        /// </summary>
        /// <param name="having">The "having" clause.</param>
        /// <returns>The query.</returns>
        IQuery Having(IOperator having);

        /// <summary>
        /// Sets a raw "having" clause.
        /// <para>You must write the entire clause including the "having" keyword.</para>
        /// </summary>
        /// <param name="having">The "having" clause.</param>
        /// <returns>The query.</returns>
        IQuery Having(IRawSql having);

        /// <summary>
        /// Sets the "having" clause.
        /// </summary>
        /// <param name="having">The "having" clause.</param>
        /// <returns>The query.</returns>
        IQuery Having(Expression<Func<bool>> having);

        /// <summary>
        /// Sets the "order by" clause.
        /// </summary>
        /// <param name="orderBy">The "order by" clause.</param>
        /// <returns>The query.</returns>
        IQuery OrderBy(IOrderBy orderBy);

        /// <summary>
        /// Sets a raw "order by" clause.
        /// <para>You must write the entire clause including the "order by" keyword.</para>
        /// </summary>
        /// <param name="orderBy">The "order by" clause.</param>
        /// <returns>The query.</returns>
        IQuery OrderBy(IRawSql orderBy);

        /// <summary>
        /// Sets the "order by" clause.
        /// </summary>
        /// <param name="func">Function that returns the "order by" clause.</param>
        /// <returns>The query.</returns>
        IQuery OrderBy(Func<IOrderBy, IOrderBy> func);

        /// <summary>
        /// Sets the "offset fetch" clause.
        /// </summary>
        /// <param name="offset">The "offset fetch" clause.</param>
        /// <returns>The query.</returns>
        IQuery Offset(IOffset offset);

        /// <summary>
        /// Sets a raw "offset fetch" clause.
        /// <para>You must write the entire clause.</para>
        /// </summary>
        /// <param name="offset">The "offset fetch" clause.</param>
        /// <returns>The query.</returns>
        IQuery Offset(IRawSql offset);

        /// <summary>
        /// Creates or adds an "offset" clause.
        /// </summary>
        /// <param name="offset">The number of rows to skip.</param>
        /// <returns>The query.</returns>
        IQuery Offset(object offset);

        /// <summary>
        /// Creates an "offset fetch" clause.
        /// </summary>
        /// <param name="offset">The number of rows to skip.</param>
        /// <param name="fetch">The number of rows to return.</param>
        /// <returns>The query.</returns>
        IQuery Offset(object offset, object fetch);

        /// <summary>
        /// Creates or adds "fetch" clause.
        /// </summary>
        /// <param name="fetch">The number of rows to return.</param>
        /// <returns>The query.</returns>
        IQuery Fetch(object fetch);

        /// <summary>
        /// Sets a "select count(*)" and removes the "order by" and "offset fetch" of the query.
        /// </summary>
        /// <returns>The query</returns>
        IQuery Count();
    }

    /// <summary>
    /// A query with options for the "from" clause.
    /// </summary>
    public interface IQueryFrom : IQuery
    {
        /// <summary>
        /// Additional options.
        /// <para>Use <see cref="IRawSql"/> to add options.</para>
        /// </summary>
        /// <param name="options">The options.</param>
        /// <returns>The query.</returns>
        IQuery Options(IQueryFragment options);
    }

    /// <summary>
    /// A query with options for the "join" clause.
    /// </summary>
    public interface IQueryJoin : IQuery
    {
        /// <summary>
        /// Adds an "on" clause.
        /// </summary>
        /// <param name="on">The "on" condition.</param>
        /// <returns>The query.</returns>
        IQueryJoin On(IQueryFragment on);

        /// <summary>
        /// Adds an "on" clause.
        /// </summary>
        /// <param name="on">The "on" condition.</param>
        /// <returns>The query.</returns>
        IQueryJoin On(Expression<Func<bool>> on);

        /// <summary>
        /// Additional options.
        /// <para>Use <see cref="IRawSql"/> to add options.</para>
        /// </summary>
        /// <param name="options">The options.</param>
        /// <returns>The query.</returns>
        IQueryJoin Options(IQueryFragment options);
    }

    /// <summary>
    /// A query with options for the "join" clause without a source value.
    /// </summary>
    public interface IQueryJoinFrom
    {
        /// <summary>
        /// Creates a "join" clause.
        /// </summary>
        /// <param name="tableName">The table name.</param>
        /// <returns>The query.</returns>
        IQueryJoin Join(string tableName);

        /// <summary>
        /// Creates a "join" clause.
        /// </summary>
        /// <param name="tableName">The table name.</param>
        /// <param name="aliasName">The alias name.</param>
        /// <returns>The query.</returns>
        IQueryJoin Join(string tableName, string aliasName);

        /// <summary>
        /// Creates a "join" clause.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <returns>The query.</returns>
        IQueryJoin Join(IAlias alias);

        /// <summary>
        /// Creates a "join" clause with an expression.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <typeparam name="T">The type of the table.</typeparam>
        /// <returns>The query.</returns>
        IQueryJoin Join<T>(Expression<Func<T>> alias);

        /// <summary>
        /// Creates a "join" clause with a subquery.
        /// </summary>
        /// <param name="value">The subquery.</param>
        /// <param name="aliasName">The alias name.</param>
        /// <returns>The query.</returns>
        IQueryJoin Join(IQueryFragment value, string aliasName);

        /// <summary>
        /// Creates a "join" clause with a subquery.
        /// </summary>
        /// <param name="value">The subquery.</param>
        /// <param name="alias">The alias.</param>
        /// <returns>The query.</returns>
        IQueryJoin Join(IQueryFragment value, IAlias alias);

        /// <summary>
        /// Creates a "join" clause with a subquery.
        /// </summary>
        /// <param name="value">The subquery.</param>
        /// <param name="alias">The alias.</param>
        /// <typeparam name="T">The type of the table.</typeparam>
        /// <returns>The query.</returns>
        IQueryJoin Join<T>(IQueryFragment value, Expression<Func<T>> alias);
    }
}