using System;
using System.Collections;
using System.Linq.Expressions;
using Suilder.Core;

namespace Suilder.Builder
{
    /// <summary>
    /// The SQL builder.
    /// </summary>
    public interface ISqlBuilder
    {
        /// <summary>
        /// Creates an alias.
        /// </summary>
        /// <param name="tableName">The table and alias name.</param>
        /// <returns>The alias.</returns>
        IAlias Alias(string tableName);

        /// <summary>
        /// Creates an alias.
        /// </summary>
        /// <param name="tableName">The table name.</param>
        /// <param name="aliasName">The alias name.</param>
        /// <returns>The alias.</returns>
        IAlias Alias(string tableName, string aliasName);

        /// <summary>
        /// Creates an alias.
        /// </summary>
        /// <param name="aliasType">The type of the table.</param>
        /// <param name="aliasName">The alias name.</param>
        /// <returns>The alias.</returns>
        IAlias Alias(Type aliasType, string aliasName);

        /// <summary>
        /// Creates a typed alias.
        /// </summary>
        /// <typeparam name="T">The type of the table.</typeparam>
        /// <returns>The alias.</returns>
        IAlias<T> Alias<T>();

        /// <summary>
        /// Creates a typed alias.
        /// </summary>
        /// <param name="aliasName">The alias name.</param>
        /// <typeparam name="T">The type of the table.</typeparam>
        /// <returns>The alias.</returns>
        IAlias<T> Alias<T>(string aliasName);

        /// <summary>
        /// Creates an alias with an expression.
        /// </summary>
        /// <param name="expression">The alias.</param>
        /// <typeparam name="T">The type of the table.</typeparam>
        /// <returns>The alias.</returns>
        IAlias<T> Alias<T>(Expression<Func<T>> expression);

        /// <summary>
        /// Creates an alias with an expression.
        /// </summary>
        /// <param name="expression">The alias.</param>
        /// <typeparam name="T">The type of the table.</typeparam>
        /// <returns>The alias.</returns>
        IAlias<T> Alias<T>(Expression expression);

        /// <summary>
        /// Creates an alias with an expression.
        /// </summary>
        /// <param name="expression">The alias.</param>
        /// <returns>The alias.</returns>
        IAlias Alias(Expression<Func<object>> expression);

        /// <summary>
        /// Creates an alias with an expression.
        /// </summary>
        /// <param name="expression">The alias.</param>
        /// <returns>The alias.</returns>
        IAlias Alias(Expression expression);

        /// <summary>
        /// Creates a column.
        /// </summary>
        /// <param name="columnName">The column name.</param>
        /// <returns>The column.</returns>
        IColumn Col(string columnName);

        /// <summary>
        /// Creates a column.
        /// </summary>
        /// <param name="tableName">The table name or his alias.</param>
        /// <param name="columnName">The column name.</param>
        /// <returns>The column.</returns>
        IColumn Col(string tableName, string columnName);

        /// <summary>
        /// Creates a column.
        /// </summary>
        /// <param name="columnName">The column name.</param>
        /// <typeparam name="T">The type of the table.</typeparam>
        /// <returns>The column.</returns>
        IColumn Col<T>(string columnName);

        /// <summary>
        /// Creates a column.
        /// </summary>
        /// <param name="tableName">The table name or his alias.</param>
        /// <param name="columnName">The column name.</param>
        /// <typeparam name="T">The type of the table.</typeparam>
        /// <returns>The column.</returns>
        IColumn Col<T>(string tableName, string columnName);

        /// <summary>
        /// Creates a column.
        /// </summary>
        /// <param name="expression">The column.</param>
        /// <typeparam name="T">The type of the table.</typeparam>
        /// <returns>The column.</returns>
        IColumn Col<T>(Expression<Func<T, object>> expression);

        /// <summary>
        /// Creates a column.
        /// </summary>
        /// <param name="tableName">The table name or his alias.</param>
        /// <param name="expression">The column.</param>
        /// <typeparam name="T">The type of the table.</typeparam>
        /// <returns>The column.</returns>
        IColumn Col<T>(string tableName, Expression<Func<T, object>> expression);

        /// <summary>
        /// Creates a column.
        /// </summary>
        /// <param name="aliasType">The type of the table.</param>
        /// <param name="tableName">The table name or his alias.</param>
        /// <param name="columnName">The column name.</param>
        /// <returns>The column.</returns>
        IColumn Col(Type aliasType, string tableName, string columnName);

        /// <summary>
        /// Creates a column with an expression.
        /// </summary>
        /// <param name="expression">The column.</param>
        /// <returns>The column.</returns>
        IColumn Col(Expression<Func<object>> expression);

        /// <summary>
        /// Creates a column with an expression.
        /// </summary>
        /// <param name="expression">The column.</param>
        /// <returns>The column.</returns>
        IColumn Col(Expression expression);

        /// <summary>
        /// Creates a value with an expression.
        /// <para>The value can be a literal value or an <see cref="IQueryFragment"/> that represents a value,
        /// like a column, a function or an arithmetic operator.</para>
        /// </summary>
        /// <param name="expression">The value.</param>
        /// <returns>The value</returns>
        object Val(Expression<Func<object>> expression);

        /// <summary>
        /// Creates a value with an expression.
        /// <para>The value can be a literal value or an <see cref="IQueryFragment"/> that represents a value,
        /// like a column, a function or an arithmetic operator.</para>
        /// </summary>
        /// <param name="expression">The value.</param>
        /// <returns>The value</returns>
        object Val(Expression expression);

        /// <summary>
        /// Creates an operator with an expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The operator.</returns>
        IOperator Op(Expression<Func<bool>> expression);

        /// <summary>
        /// Creates an operator with an expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The operator.</returns>
        IOperator Op(Expression expression);

        /// <summary>
        /// Creates an "equal to" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "equal to" operator.</returns>
        IOperator Eq(object left, object right);

        /// <summary>
        /// Creates an "equal to" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "equal to" operator.</returns>
        IOperator Eq(Expression<Func<object>> left, object right);

        /// <summary>
        /// Creates an "equal to" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "equal to" operator.</returns>
        IOperator Eq(Expression<Func<object>> left, Expression<Func<object>> right);

        /// <summary>
        /// Creates a "not equal to" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "not equal to" operator.</returns>
        IOperator NotEq(object left, object right);

        /// <summary>
        /// Creates a "not equal to" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "not equal to" operator.</returns>
        IOperator NotEq(Expression<Func<object>> left, object right);

        /// <summary>
        /// Creates a "not equal to" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "not equal to" operator.</returns>
        IOperator NotEq(Expression<Func<object>> left, Expression<Func<object>> right);

        /// <summary>
        /// Creates a "like" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "like" operator.</returns>
        IOperator Like(object left, object right);

        /// <summary>
        /// Creates a "like" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "like" operator.</returns>
        IOperator Like(Expression<Func<object>> left, object right);

        /// <summary>
        /// Creates a "like" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "like" operator.</returns>
        IOperator Like(Expression<Func<object>> left, Expression<Func<object>> right);

        /// <summary>
        /// Creates a "not like" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "not like" operator.</returns>
        IOperator NotLike(object left, object right);

        /// <summary>
        /// Creates a "not like" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "not like" operator.</returns>
        IOperator NotLike(Expression<Func<object>> left, object right);

        /// <summary>
        /// Creates a "not like" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "not like" operator.</returns>
        IOperator NotLike(Expression<Func<object>> left, Expression<Func<object>> right);

        /// <summary>
        /// Creates a "less than" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "less than" operator.</returns>
        IOperator Lt(object left, object right);

        /// <summary>
        /// Creates a "less than" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "less than" operator.</returns>
        IOperator Lt(Expression<Func<object>> left, object right);

        /// <summary>
        /// Creates a "less than" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "less than" operator.</returns>
        IOperator Lt(Expression<Func<object>> left, Expression<Func<object>> right);

        /// <summary>
        /// Creates a "less than or equal to" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "less than or equal to" operator.</returns>
        IOperator Le(object left, object right);

        /// <summary>
        /// Creates a "less than or equal to" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "less than or equal to" operator.</returns>
        IOperator Le(Expression<Func<object>> left, object right);

        /// <summary>
        /// Creates a "less than or equal to" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "less than or equal to" operator.</returns>
        IOperator Le(Expression<Func<object>> left, Expression<Func<object>> right);

        /// <summary>
        /// Creates a "greater than" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "greater than" operator.</returns>
        IOperator Gt(object left, object right);

        /// <summary>
        /// Creates a "greater than" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "greater than" operator.</returns>
        IOperator Gt(Expression<Func<object>> left, object right);

        /// <summary>
        /// Creates a "greater than" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "greater than" operator.</returns>
        IOperator Gt(Expression<Func<object>> left, Expression<Func<object>> right);

        /// <summary>
        /// Creates a "greater than or equal to" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "greater than or equal to" operator.</returns>
        IOperator Ge(object left, object right);

        /// <summary>
        /// Creates a "greater than or equal to" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "greater than or equal to" operator.</returns>
        IOperator Ge(Expression<Func<object>> left, object right);

        /// <summary>
        /// Creates a "greater than or equal to" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "greater than or equal to" operator.</returns>
        IOperator Ge(Expression<Func<object>> left, Expression<Func<object>> right);

        /// <summary>
        /// Creates an "in" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value. An <see cref="IEnumerable"/> is divided into multiple values.</param>
        /// <returns>The "in" operator.</returns>
        IOperator In(object left, object right);

        /// <summary>
        /// Creates an "in" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value. An <see cref="IEnumerable"/> is divided into multiple values.</param>
        /// <returns>The "in" operator.</returns>
        IOperator In(Expression<Func<object>> left, object right);

        /// <summary>
        /// Creates an "in" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value. An <see cref="IEnumerable"/> is divided into multiple values.</param>
        /// <returns>The "in" operator.</returns>
        IOperator In(Expression<Func<object>> left, Expression<Func<object>> right);

        /// <summary>
        /// Creates a "not in" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value. An <see cref="IEnumerable"/> is divided into multiple values.</param>
        /// <returns>The "not in" operator.</returns>
        IOperator NotIn(object left, object right);

        /// <summary>
        /// Creates a "not in" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value. An <see cref="IEnumerable"/> is divided into multiple values.</param>
        /// <returns>The "not in" operator.</returns>
        IOperator NotIn(Expression<Func<object>> left, object right);

        /// <summary>
        /// Creates a "not in" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value. An <see cref="IEnumerable"/> is divided into multiple values.</param>
        /// <returns>The "not in" operator.</returns>
        IOperator NotIn(Expression<Func<object>> left, Expression<Func<object>> right);

        /// <summary>
        /// Creates a "not" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The "not" operator.</returns>
        IOperator Not(object value);

        /// <summary>
        /// Creates a "not" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The "not" operator.</returns>
        IOperator Not(Expression<Func<bool>> value);

        /// <summary>
        /// Creates an "is null" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The "is null" operator.</returns>
        IOperator IsNull(object value);

        /// <summary>
        /// Creates an "is null" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The "is null" operator.</returns>
        IOperator IsNull(Expression<Func<object>> value);

        /// <summary>
        /// Creates an "is not null" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The "is not null" operator.</returns>
        IOperator IsNotNull(object value);

        /// <summary>
        /// Creates an "is not null" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The "is not null" operator.</returns>
        IOperator IsNotNull(Expression<Func<object>> value);

        /// <summary>
        /// Creates an "all" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The "all" operator.</returns>
        IOperator All(object value);

        /// <summary>
        /// Creates an "any" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The "any" operator.</returns>
        IOperator Any(object value);

        /// <summary>
        /// Creates an "exists" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The "exists" operator.</returns>
        IOperator Exists(object value);

        /// <summary>
        /// Creates a "some" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The "some" operator.</returns>
        IOperator Some(object value);

        /// <summary>
        /// Creates an "and" operator.
        /// </summary>
        /// <value>The "and" operator.</value>
        ILogicalOperator And { get; }

        /// <summary>
        /// Creates an "or" operator.
        /// </summary>
        /// <value>The "or" operator.</value>
        ILogicalOperator Or { get; }

        /// <summary>
        /// Creates an "add" operator.
        /// </summary>
        /// <value>The "add" operator.</value>
        IArithOperator Add { get; }

        /// <summary>
        /// Creates a "subtract" operator.
        /// </summary>
        /// <value>The "subtract" operator.</value>
        IArithOperator Subtract { get; }

        /// <summary>
        /// Creates a "multiply" operator.
        /// </summary>
        /// <value>The "multiply" operator.</value>
        IArithOperator Multiply { get; }

        /// <summary>
        /// Creates a "divide" operator.
        /// </summary>
        /// <value>The "divide" operator.</value>
        IArithOperator Divide { get; }

        /// <summary>
        /// Creates a "modulo" operator.
        /// </summary>
        /// <value>The "modulo" operator.</value>
        IArithOperator Modulo { get; }

        /// <summary>
        /// Creates a bitwise "and" operator.
        /// </summary>
        /// <value>The bitwise "and" operator.</value>
        IBitOperator BitAnd { get; }

        /// <summary>
        /// Creates a bitwise "or" operator.
        /// </summary>
        /// <value>The bitwise "or" operator.</value>
        IBitOperator BitOr { get; }

        /// <summary>
        /// Creates a bitwise "xor" operator.
        /// </summary>
        /// <value>The bitwise "xor" operator.</value>
        IBitOperator BitXor { get; }

        /// <summary>
        /// Creates an "union" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "union" operator.</returns>
        IOperator Union(IQueryFragment left, IQueryFragment right);

        /// <summary>
        /// Creates an "union all" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "union all" operator.</returns>
        IOperator UnionAll(IQueryFragment left, IQueryFragment right);

        /// <summary>
        /// Creates an "except" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "except" operator.</returns>
        IOperator Except(IQueryFragment left, IQueryFragment right);

        /// <summary>
        /// Creates an "intersect" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "intersect" operator.</returns>
        IOperator Intersect(IQueryFragment left, IQueryFragment right);

        /// <summary>
        /// Creates a "case" statement.
        /// </summary>
        /// <value>The "case" statement.</value>
        ICase Case { get; }

        /// <summary>
        /// Creates a function.
        /// </summary>
        /// <param name="name">The name of the function.</param>
        /// <returns>The function.</returns>
        IFunction Function(string name);

        /// <summary>
        /// Creates a list of columns.
        /// </summary>
        /// <value>The list of columns.</value>
        IColList ColList { get; }

        /// <summary>
        /// Creates a list of values.
        /// </summary>
        /// <value>The list of values.</value>
        IValList ValList { get; }

        /// <summary>
        /// Creates a sublist of values.
        /// </summary>
        /// <value>The sublist of values.</value>
        ISubList SubList { get; }

        /// <summary>
        /// Creates an "over" clause.
        /// </summary>
        /// <value>The "over" clause.</value>
        IOver Over { get; }

        /// <summary>
        /// Creates a CTE.
        /// </summary>
        /// <param name="name">The name of the CTE.</param>
        /// <returns>The CTE.</returns>
        ICte Cte(string name);

        /// <summary>
        /// Creates a "with" clause.
        /// </summary>
        /// <value>The "with" clause.</value>
        IWith With { get; }

        /// <summary>
        /// Creates a "select" statement.
        /// </summary>
        /// <value>The "select" statement.</value>
        ISelect Select();

        /// <summary>
        /// Creates an "insert" statement.
        /// </summary>
        /// <returns>The "insert" statement.</returns>
        IInsert Insert();

        /// <summary>
        /// Creates an "update" statement.
        /// </summary>
        /// <value>The "update" statement.</value>
        IUpdate Update();

        /// <summary>
        /// Creates a "delete" statement.
        /// </summary>
        /// <value>The "delete" statement.</value>
        IDelete Delete();

        /// <summary>
        /// Creates a "from" clause.
        /// </summary>
        /// <param name="tableName">The table name.</param>
        /// <returns>The "from" clause.</returns>
        IFrom From(string tableName);

        /// <summary>
        /// Creates a "from" clause.
        /// </summary>
        /// <param name="tableName">The table name.</param>
        /// <param name="aliasName">The alias name.</param>
        /// <returns>The "from" clause.</returns>
        IFrom From(string tableName, string aliasName);

        /// <summary>
        /// Creates a "from" clause.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <returns>The "from" clause.</returns>
        IFrom From(IAlias alias);

        /// <summary>
        /// Creates a "from" clause with an expression.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <typeparam name="T">The type of the table.</typeparam>
        /// <returns>The "from" clause.</returns>
        IFrom From<T>(Expression<Func<T>> alias);

        /// <summary>
        /// Creates a "from" clause with a subquery.
        /// </summary>
        /// <param name="value">The subquery.</param>
        /// <param name="aliasName">The alias name.</param>
        /// <returns>The "from" clause.</returns>
        IFrom From(IQueryFragment value, string aliasName);

        /// <summary>
        /// Creates a "from" clause with a subquery.
        /// </summary>
        /// <param name="value">The subquery.</param>
        /// <param name="alias">The alias.</param>
        /// <returns>The "from" clause.</returns>
        IFrom From(IQueryFragment value, IAlias alias);

        /// <summary>
        /// Creates a "from" clause with a subquery.
        /// </summary>
        /// <param name="value">The subquery.</param>
        /// <param name="alias">The alias.</param>
        /// <typeparam name="T">The type of the table.</typeparam>
        /// <returns>The "from" clause.</returns>
        IFrom From<T>(IQueryFragment value, Expression<Func<T>> alias);

        /// <summary>
        /// Creates a "from" clause with a dummy table.
        /// <para>If the engine does not need a dummy table, writes nothing.</para>
        /// </summary>
        /// <returns>The "from" clause.</returns>
        IRawSql FromDummy { get; }

        /// <summary>
        /// Creates an "inner join" clause.
        /// </summary>
        /// <value>The "inner join" clause.</value>
        IJoinFrom Inner { get; }

        /// <summary>
        /// Creates a "left join" clause.
        /// </summary>
        /// <value>The "left join" clause.</value>
        IJoinFrom Left { get; }

        /// <summary>
        /// Creates a "right join" clause.
        /// </summary>
        /// <value>The "right join" clause.</value>
        IJoinFrom Right { get; }

        /// <summary>
        /// Creates a "full join" clause.
        /// </summary>
        /// <value>The "full join" clause.</value>
        IJoinFrom Full { get; }

        /// <summary>
        /// Creates a "cross join" clause.
        /// </summary>
        /// <value>The "cross join" clause.</value>
        IJoinFrom Cross { get; }

        /// <summary>
        /// Creates a "join" clause.
        /// </summary>
        /// <param name="tableName">The table name.</param>
        /// <param name="joinType">The type of join, by default inner join.</param>
        /// <returns>The "join" clause.</returns>
        IJoin Join(string tableName, JoinType joinType = JoinType.Inner);

        /// <summary>
        /// Creates a "join" clause.
        /// </summary>
        /// <param name="tableName">The table name.</param>
        /// <param name="aliasName">The alias name.</param>
        /// <param name="joinType">The type of join, by default inner join.</param>
        /// <returns>The "join" clause.</returns>
        IJoin Join(string tableName, string aliasName, JoinType joinType = JoinType.Inner);

        /// <summary>
        /// Creates a "join" clause.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <param name="joinType">The type of join, by default inner join.</param>
        /// <returns>The "join" clause.</returns>
        IJoin Join(IAlias alias, JoinType joinType = JoinType.Inner);

        /// <summary>
        /// Creates a "join" clause with an expression.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <param name="joinType">The type of join, by default inner join.</param>
        /// <typeparam name="T">The type of the table.</typeparam>
        /// <returns>The "join" clause.</returns>
        IJoin Join<T>(Expression<Func<T>> alias, JoinType joinType = JoinType.Inner);

        /// <summary>
        /// Creates a "join" clause with a subquery.
        /// </summary>
        /// <param name="value">The subquery.</param>
        /// <param name="aliasName">The alias name.</param>
        /// <param name="joinType">The type of join, by default inner join.</param>
        /// <returns>The "join" clause.</returns>
        IJoin Join(IQueryFragment value, string aliasName, JoinType joinType = JoinType.Inner);

        /// <summary>
        /// Creates a "join" clause with a subquery.
        /// </summary>
        /// <param name="value">The subquery.</param>
        /// <param name="alias">The alias.</param>
        /// <param name="joinType">The type of join, by default inner join.</param>
        /// <returns>The "join" clause.</returns>
        IJoin Join(IQueryFragment value, IAlias alias, JoinType joinType = JoinType.Inner);

        /// <summary>
        /// Creates a "join" clause with a subquery.
        /// </summary>
        /// <param name="value">The subquery.</param>
        /// <param name="alias">The alias.</param>
        /// <param name="joinType">The type of join, by default inner join.</param>
        /// <typeparam name="T">The type of the table.</typeparam>
        /// <returns>The "join" clause.</returns>
        IJoin Join<T>(IQueryFragment value, Expression<Func<T>> alias, JoinType joinType = JoinType.Inner);

        /// <summary>
        /// Creates an "order by" clause.
        /// </summary>
        /// <value>The "order by" clause.</value>
        IOrderBy OrderBy();

        /// <summary>
        /// Creates a "top" clause.
        /// </summary>
        /// <param name="fetch">The number of rows to return.</param>
        /// <returns>The "top" clause.</returns>
        ITop Top(object fetch);

        /// <summary>
        /// Creates an "offset" clause.
        /// </summary>
        /// <param name="offset">The number of rows to skip.</param>
        /// <returns>The "offset fetch" clause.</returns>
        IOffset Offset(object offset);

        /// <summary>
        /// Creates an "offset fetch" clause.
        /// </summary>
        /// <param name="offset">The number of rows to skip.</param>
        /// <param name="fetch">The number of rows to return.</param>
        /// <returns>The "offset fetch" clause.</returns>
        IOffset Offset(object offset, object fetch);

        /// <summary>
        /// Creates a "fetch" clause.
        /// </summary>
        /// <param name="fetch">The number of rows to return.</param>
        /// <returns>The "offset fetch" clause.</returns>
        IOffset Fetch(object fetch);

        /// <summary>
        /// Creates a query object.
        /// </summary>
        /// <value>The query.</value>
        IQuery Query { get; }

        /// <summary>
        /// Creates a raw SQL fragment.
        /// </summary>
        /// <param name="sql">The raw SQL.</param>
        /// <returns>The raw fragment.</returns>
        IRawSql Raw(string sql);

        /// <summary>
        /// Creates a raw SQL fragment.
        /// <para>The values can be any object even other <see cref="IQueryFragment"/>.</para>
        /// <para>For escaped table and column names use an <see cref="IAlias"/> or an <see cref="IColumn"/> value.</para>
        /// </summary>
        /// <param name="sql">A composite string, each item takes the following form: {index}.</param>
        /// <param name="values">An object array that contains zero or more objects to add to the raw SQL.</param>
        /// <returns>The raw fragment.</returns>
        /// <exception cref="FormatException">The format of <paramref name="sql"/> is invalid or the index of a format item
        /// is less than zero, or greater than or equal to the length of the <paramref name="values"/>  array.</exception>
        IRawSql Raw(string sql, params object[] values);

        /// <summary>
        /// Creates a raw SQL query.
        /// </summary>
        /// <param name="sql">The raw SQL.</param>
        /// <returns>The raw query.</returns>
        IRawQuery RawQuery(string sql);

        /// <summary>
        /// Creates a raw SQL query.
        /// <para>The values can be any object even other <see cref="IQueryFragment"/>.</para>
        /// <para>For escaped table and column names use an <see cref="IAlias"/> or an <see cref="IColumn"/> value.</para>
        /// </summary>
        /// <param name="sql">A composite string, each item takes the following form: {index}.</param>
        /// <param name="values">An object array that contains zero or more objects to add to the raw SQL.</param>
        /// <returns>The raw fragment.</returns>
        /// <exception cref="FormatException">The format of <paramref name="sql"/> is invalid or the index of a format item
        /// is less than zero, or greater than or equal to the length of the <paramref name="values"/>  array.</exception>
        IRawQuery RawQuery(string sql, params object[] values);

        /// <summary>
        /// Creates a SQL type.
        /// </summary>
        /// <param name="name">The type name.</param>
        /// <returns>The SQL type.</returns>
        ISqlType Type(string name);

        /// <summary>
        /// Creates a SQL type.
        /// </summary>
        /// <param name="name">The type name.</param>
        /// <param name="length">The length of the type or precision for numbers.</param>
        /// <returns>The SQL type.</returns>
        ISqlType Type(string name, int length);

        /// <summary>
        /// Creates a SQL type.
        /// </summary>
        /// <param name="name">The type name.</param>
        /// <param name="precision">The precision of the type.</param>
        /// <param name="scale">The scale of the type.</param>
        /// <returns>The SQL type.</returns>
        ISqlType Type(string name, int precision, int scale);

        /// <summary>
        /// Creates a pattern that match the start of the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The like start pattern.</returns>
        string ToLikeStart(string value);

        /// <summary>
        /// Creates a pattern that match the end of the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The like end pattern.</returns>
        string ToLikeEnd(string value);

        /// <summary>
        /// Creates a pattern that match anywhere of the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The like anywhere value.</returns>
        string ToLikeAny(string value);
    }
}