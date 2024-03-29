using System;
using System.Collections;
using System.Diagnostics;
using System.Linq.Expressions;
using Suilder.Core;
using Suilder.Operators;

namespace Suilder.Builder
{
    /// <summary>
    /// Implementation of <see cref="ISqlBuilder"/>.
    /// </summary>
    public class SqlBuilder : ISqlBuilder
    {
        /// <summary>
        /// The registered builder.
        /// <para>It can be null if there is no registered builder.</para>
        /// </summary>
        /// <value>The registered builder.</value>
        public static ISqlBuilder Instance { get; private set; }

        /// <summary>
        /// Register a builder, you can only have one builder per application.
        /// </summary>
        /// <param name="sqlBuilder">The builder.</param>
        /// <returns>The builder.</returns>
        public static ISqlBuilder Register(ISqlBuilder sqlBuilder)
        {
            return Register(sqlBuilder, false);
        }

        /// <summary>
        /// Register a builder, you can only have one builder per application.
        /// </summary>
        /// <param name="sqlBuilder">The builder.</param>
        /// <param name="force">If <see langword="true"/>, it overrides the registered builder.</param>
        /// <returns>The builder.</returns>
        public static ISqlBuilder Register(ISqlBuilder sqlBuilder, bool force)
        {
            if (!force && Instance != null)
                throw new InvalidOperationException("There is already another builder registered.");

            Instance = sqlBuilder;

            return Instance;
        }

        /// <summary>
        /// Creates an alias.
        /// </summary>
        /// <param name="tableName">The table and alias name.</param>
        /// <returns>The alias.</returns>
        public virtual IAlias Alias(string tableName)
        {
            return new Alias(tableName);
        }

        /// <summary>
        /// Creates an alias.
        /// </summary>
        /// <param name="tableName">The table name.</param>
        /// <param name="aliasName">The alias name.</param>
        /// <returns>The alias.</returns>
        public virtual IAlias Alias(string tableName, string aliasName)
        {
            return new Alias(tableName, aliasName);
        }

        /// <summary>
        /// Creates an alias.
        /// </summary>
        /// <param name="aliasType">The type of the table.</param>
        /// <param name="aliasName">The alias name.</param>
        /// <returns>The alias.</returns>
        public virtual IAlias Alias(Type aliasType, string aliasName)
        {
            return new EntityAlias(aliasType, aliasName);
        }

        /// <summary>
        /// Creates a typed alias.
        /// </summary>
        /// <typeparam name="T">The type of the table.</typeparam>
        /// <returns>The alias.</returns>
        public virtual IAlias<T> Alias<T>()
        {
            return new EntityAlias<T>();
        }

        /// <summary>
        /// Creates a typed alias.
        /// </summary>
        /// <param name="aliasName">The alias name.</param>
        /// <typeparam name="T">The type of the table.</typeparam>
        /// <returns>The alias.</returns>
        public virtual IAlias<T> Alias<T>(string aliasName)
        {
            return new EntityAlias<T>(aliasName);
        }

        /// <summary>
        /// Creates an alias with an expression.
        /// </summary>
        /// <param name="expression">The alias.</param>
        /// <typeparam name="T">The type of the table.</typeparam>
        /// <returns>The alias.</returns>
        public virtual IAlias<T> Alias<T>(Expression<Func<T>> expression)
        {
            return ExpressionProcessor.ParseAlias<T>(expression);
        }

        /// <summary>
        /// Creates an alias with an expression.
        /// </summary>
        /// <param name="expression">The alias.</param>
        /// <returns>The alias.</returns>
        public virtual IAlias Alias(Expression<Func<object>> expression)
        {
            return ExpressionProcessor.ParseAlias(expression);
        }

        /// <summary>
        /// Creates an alias with an expression.
        /// </summary>
        /// <param name="expression">The alias.</param>
        /// <returns>The alias.</returns>
        public virtual IAlias Alias(LambdaExpression expression)
        {
            return ExpressionProcessor.ParseAlias(expression);
        }

        /// <summary>
        /// Creates an alias with an expression.
        /// </summary>
        /// <param name="expression">The alias.</param>
        /// <returns>The alias.</returns>
        public virtual IAlias Alias(Expression expression)
        {
            return ExpressionProcessor.ParseAlias(expression);
        }

        /// <summary>
        /// Creates a column.
        /// </summary>
        /// <param name="columnName">The column name.</param>
        /// <returns>The column.</returns>
        public virtual IColumn Col(string columnName)
        {
            int index = columnName.LastIndexOf('.');
            if (index >= 0)
                return new Column(columnName.Substring(0, index), columnName.Substring(index + 1));
            else
                return new Column(null, columnName);
        }

        /// <summary>
        /// Creates a column.
        /// </summary>
        /// <param name="tableName">The table name or his alias.</param>
        /// <param name="columnName">The column name.</param>
        /// <returns>The column.</returns>

        public virtual IColumn Col(string tableName, string columnName)
        {
            return new Column(tableName, columnName);
        }

        /// <summary>
        /// Creates a column.
        /// </summary>
        /// <param name="columnName">The column name.</param>
        /// <typeparam name="T">The type of the table.</typeparam>
        /// <returns>The column.</returns>
        public virtual IColumn Col<T>(string columnName)
        {
            Type type = typeof(T);
            string tableName = type.Name;
            tableName = char.ToLowerInvariant(tableName[0]) + tableName.Substring(1);
            return new EntityColumn(type, tableName, columnName);
        }

        /// <summary>
        /// Creates a column.
        /// </summary>
        /// <param name="tableName">The table name or his alias.</param>
        /// <param name="columnName">The column name.</param>
        /// <typeparam name="T">The type of the table.</typeparam>
        /// <returns>The column.</returns>
        public virtual IColumn Col<T>(string tableName, string columnName)
        {
            return new EntityColumn(typeof(T), tableName, columnName);
        }

        /// <summary>
        /// Creates a column.
        /// </summary>
        /// <param name="expression">The column.</param>
        /// <typeparam name="T">The type of the table.</typeparam>
        /// <returns>The column.</returns>
        public virtual IColumn Col<T>(Expression<Func<T, object>> expression)
        {
            string tableName = typeof(T).Name;
            tableName = char.ToLowerInvariant(tableName[0]) + tableName.Substring(1);
            return ExpressionProcessor.ParseColumn<T>(tableName, expression);
        }

        /// <summary>
        /// Creates a column.
        /// </summary>
        /// <param name="tableName">The table name or his alias.</param>
        /// <param name="expression">The column.</param>
        /// <typeparam name="T">The type of the table.</typeparam>
        /// <returns>The column.</returns>
        public IColumn Col<T>(string tableName, Expression<Func<T, object>> expression)
        {
            return ExpressionProcessor.ParseColumn<T>(tableName, expression);
        }

        /// <summary>
        /// Creates a column.
        /// </summary>
        /// <param name="aliasType">The type of the table.</param>
        /// <param name="tableName">The table name or his alias.</param>
        /// <param name="columnName">The column name.</param>
        /// <returns>The column.</returns>
        public IColumn Col(Type aliasType, string tableName, string columnName)
        {
            return new EntityColumn(aliasType, tableName, columnName);
        }

        /// <summary>
        /// Creates a column with an expression.
        /// </summary>
        /// <param name="expression">The column.</param>
        /// <returns>The column.</returns>
        public virtual IColumn Col(Expression<Func<object>> expression)
        {
            return ExpressionProcessor.ParseColumn(expression);
        }

        /// <summary>
        /// Creates a column with an expression.
        /// </summary>
        /// <param name="expression">The column.</param>
        /// <returns>The column.</returns>
        public virtual IColumn Col(LambdaExpression expression)
        {
            return ExpressionProcessor.ParseColumn(expression);
        }

        /// <summary>
        /// Creates a column with an expression.
        /// </summary>
        /// <param name="expression">The column.</param>
        /// <returns>The column.</returns>
        public virtual IColumn Col(Expression expression)
        {
            return ExpressionProcessor.ParseColumn(expression);
        }

        /// <summary>
        /// Creates a value with an expression.
        /// <para>The value can be a literal value or an <see cref="IQueryFragment"/> that represents a value,
        /// like a column, a function or an arithmetic operator.</para>
        /// </summary>
        /// <param name="expression">The value.</param>
        /// <returns>The value</returns>
        public virtual object Val(Expression<Func<object>> expression)
        {
            return ExpressionProcessor.ParseValue(expression);
        }

        /// <summary>
        /// Creates a value with an expression.
        /// <para>The value can be a literal value or an <see cref="IQueryFragment"/> that represents a value,
        /// like a column, a function or an arithmetic operator.</para>
        /// </summary>
        /// <param name="expression">The value.</param>
        /// <returns>The value</returns>
        public virtual object Val(LambdaExpression expression)
        {
            return ExpressionProcessor.ParseValue(expression);
        }

        /// <summary>
        /// Creates a value with an expression.
        /// <para>The value can be a literal value or an <see cref="IQueryFragment"/> that represents a value,
        /// like a column, a function or an arithmetic operator.</para>
        /// </summary>
        /// <param name="expression">The value.</param>
        /// <returns>The value</returns>
        public virtual object Val(Expression expression)
        {
            return ExpressionProcessor.ParseValue(expression);
        }

        /// <summary>
        /// Creates an operator with an expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The operator.</returns>
        public virtual IOperator Op(Expression<Func<bool>> expression)
        {
            return ExpressionProcessor.ParseBoolOperator(expression);
        }

        /// <summary>
        /// Creates an operator with an expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The operator.</returns>
        public virtual IOperator Op(LambdaExpression expression)
        {
            return ExpressionProcessor.ParseBoolOperator(expression);
        }

        /// <summary>
        /// Creates an operator with an expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The operator.</returns>
        public virtual IOperator Op(Expression expression)
        {
            return ExpressionProcessor.ParseBoolOperator(expression);
        }

        /// <summary>
        /// Creates an "equal to" operator.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The "equal to" operator.</returns>
        public virtual IOperator Eq(object left, object right)
        {
            if (left == null || right == null)
                return IsNull(left ?? right);

            return new Operator(OperatorName.Eq, left, right);
        }

        /// <summary>
        /// Creates an "equal to" operator.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The "equal to" operator.</returns>
        public virtual IOperator Eq(Expression<Func<object>> left, object right)
        {
            return Eq(Val(left), right);
        }

        /// <summary>
        /// Creates an "equal to" operator.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The "equal to" operator.</returns>
        public virtual IOperator Eq(Expression<Func<object>> left, Expression<Func<object>> right)
        {
            if (right == null)
                return Eq(left, (object)right);

            return Eq(Val(left), Val(right));
        }

        /// <summary>
        /// Creates a "not equal to" operator.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The "not equal to" operator.</returns>
        public virtual IOperator NotEq(object left, object right)
        {
            if (left == null || right == null)
                return IsNotNull(left ?? right);

            return new Operator(OperatorName.NotEq, left, right);
        }

        /// <summary>
        /// Creates a "not equal to" operator.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The "not equal to" operator.</returns>
        public virtual IOperator NotEq(Expression<Func<object>> left, object right)
        {
            return NotEq(Val(left), right);
        }

        /// <summary>
        /// Creates a "not equal to" operator.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The "not equal to" operator.</returns>
        public virtual IOperator NotEq(Expression<Func<object>> left, Expression<Func<object>> right)
        {
            if (right == null)
                return NotEq(left, (object)right);

            return NotEq(Val(left), Val(right));
        }

        /// <summary>
        /// Creates a "like" operator.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The "like" operator.</returns>
        public virtual IOperator Like(object left, object right)
        {
            return new Operator(OperatorName.Like, left, right);
        }

        /// <summary>
        /// Creates a "like" operator.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The "like" operator.</returns>
        public virtual IOperator Like(Expression<Func<object>> left, object right)
        {
            return Like(Val(left), right);
        }

        /// <summary>
        /// Creates a "like" operator.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The "like" operator.</returns>
        public virtual IOperator Like(Expression<Func<object>> left, Expression<Func<object>> right)
        {
            if (right == null)
                return Like(left, (object)right);

            return Like(Val(left), Val(right));
        }

        /// <summary>
        /// Creates a "not like" operator.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The "not like" operator.</returns>
        public virtual IOperator NotLike(object left, object right)
        {
            return new Operator(OperatorName.NotLike, left, right);
        }

        /// <summary>
        /// Creates a "not like" operator.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The "not like" operator.</returns>
        public virtual IOperator NotLike(Expression<Func<object>> left, object right)
        {
            return NotLike(Val(left), right);
        }

        /// <summary>
        /// Creates a "not like" operator.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The "not like" operator.</returns>
        public virtual IOperator NotLike(Expression<Func<object>> left, Expression<Func<object>> right)
        {
            if (right == null)
                return NotLike(left, (object)right);

            return NotLike(Val(left), Val(right));
        }

        /// <summary>
        /// Creates a "less than" operator.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The "less than" operator.</returns>
        public virtual IOperator Lt(object left, object right)
        {
            return new Operator(OperatorName.Lt, left, right);
        }

        /// <summary>
        /// Creates a "less than" operator.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The "less than" operator.</returns>
        public virtual IOperator Lt(Expression<Func<object>> left, object right)
        {
            return Lt(Val(left), right);
        }

        /// <summary>
        /// Creates a "less than" operator.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The "less than" operator.</returns>
        public virtual IOperator Lt(Expression<Func<object>> left, Expression<Func<object>> right)
        {
            if (right == null)
                return Lt(left, (object)right);

            return Lt(Val(left), Val(right));
        }

        /// <summary>
        /// Creates a "less than or equal to" operator.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The "less than or equal to" operator.</returns>
        public virtual IOperator Le(object left, object right)
        {
            return new Operator(OperatorName.Le, left, right);
        }

        /// <summary>
        /// Creates a "less than or equal to" operator.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The "less than or equal to" operator.</returns>
        public virtual IOperator Le(Expression<Func<object>> left, object right)
        {
            return Le(Val(left), right);
        }

        /// <summary>
        /// Creates a "less than or equal to" operator.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The "less than or equal to" operator.</returns>
        public virtual IOperator Le(Expression<Func<object>> left, Expression<Func<object>> right)
        {
            if (right == null)
                return Le(left, (object)right);

            return Le(Val(left), Val(right));
        }

        /// <summary>
        /// Creates a "greater than" operator.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The "greater than" operator.</returns>
        public virtual IOperator Gt(object left, object right)
        {
            return new Operator(OperatorName.Gt, left, right);
        }

        /// <summary>
        /// Creates a "greater than" operator.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The "greater than" operator.</returns>
        public virtual IOperator Gt(Expression<Func<object>> left, object right)
        {
            return Gt(Val(left), right);
        }

        /// <summary>
        /// Creates a "greater than" operator.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The "greater than" operator.</returns>
        public virtual IOperator Gt(Expression<Func<object>> left, Expression<Func<object>> right)
        {
            if (right == null)
                return Gt(left, (object)right);

            return Gt(Val(left), Val(right));
        }

        /// <summary>
        /// Creates a "greater than or equal to" operator.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The "greater than or equal to" operator.</returns>
        public virtual IOperator Ge(object left, object right)
        {
            return new Operator(OperatorName.Ge, left, right);
        }

        /// <summary>
        /// Creates a "greater than or equal to" operator.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The "greater than or equal to" operator.</returns>
        public virtual IOperator Ge(Expression<Func<object>> left, object right)
        {
            return Ge(Val(left), right);
        }

        /// <summary>
        /// Creates a "greater than or equal to" operator.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The "greater than or equal to" operator.</returns>
        public virtual IOperator Ge(Expression<Func<object>> left, Expression<Func<object>> right)
        {
            if (right == null)
                return Ge(left, (object)right);

            return Ge(Val(left), Val(right));
        }

        /// <summary>
        /// Creates an "in" operator.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value. An <see cref="IEnumerable"/> is divided into multiple values.</param>
        /// <returns>The "in" operator.</returns>
        public virtual IOperator In(object left, object right)
        {
            return new ListOperator(OperatorName.In, left, right);
        }

        /// <summary>
        /// Creates an "in" operator.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value. An <see cref="IEnumerable"/> is divided into multiple values.</param>
        /// <returns>The "in" operator.</returns>
        public virtual IOperator In(Expression<Func<object>> left, object right)
        {
            return In(Val(left), right);
        }

        /// <summary>
        /// Creates an "in" operator.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value. An <see cref="IEnumerable"/> is divided into multiple values.</param>
        /// <returns>The "in" operator.</returns>
        public virtual IOperator In(Expression<Func<object>> left, Expression<Func<object>> right)
        {
            if (right == null)
                return In(left, (object)right);

            return In(Val(left), Val(right));
        }

        /// <summary>
        /// Creates a "not in" operator.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value. An <see cref="IEnumerable"/> is divided into multiple values.</param>
        /// <returns>The "not in" operator.</returns>
        public virtual IOperator NotIn(object left, object right)
        {
            return new ListOperator(OperatorName.NotIn, left, right);
        }

        /// <summary>
        /// Creates a "not in" operator.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value. An <see cref="IEnumerable"/> is divided into multiple values.</param>
        /// <returns>The "not in" operator.</returns>
        public virtual IOperator NotIn(Expression<Func<object>> left, object right)
        {
            return NotIn(Val(left), right);
        }

        /// <summary>
        /// Creates a "not in" operator.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value. An <see cref="IEnumerable"/> is divided into multiple values.</param>
        /// <returns>The "not in" operator.</returns>
        public virtual IOperator NotIn(Expression<Func<object>> left, Expression<Func<object>> right)
        {
            if (right == null)
                return NotIn(left, (object)right);

            return NotIn(Val(left), Val(right));
        }

        /// <summary>
        /// Creates a "not" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The "not" operator.</returns>
        public virtual IOperator Not(object value)
        {
            return new LeftOperator(OperatorName.Not, value);
        }

        /// <summary>
        /// Creates a "not" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The "not" operator.</returns>
        public virtual IOperator Not(Expression<Func<bool>> value)
        {
            if (value == null)
                return Not((object)value);

            return Not(Op(value));
        }

        /// <summary>
        /// Creates an "is null" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The "is null" operator.</returns>
        public virtual IOperator IsNull(object value)
        {
            return new RightOperator(OperatorName.IsNull, value);
        }

        /// <summary>
        /// Creates an "is null" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The "is null" operator.</returns>
        public virtual IOperator IsNull(Expression<Func<object>> value)
        {
            if (value == null)
                return IsNull((object)value);

            return IsNull(Val(value));
        }

        /// <summary>
        /// Creates an "is not null" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The "is not null" operator.</returns>
        public virtual IOperator IsNotNull(object value)
        {
            return new RightOperator(OperatorName.IsNotNull, value);
        }

        /// <summary>
        /// Creates an "is not null" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The "is not null" operator.</returns>
        public virtual IOperator IsNotNull(Expression<Func<object>> value)
        {
            if (value == null)
                return IsNotNull((object)value);

            return IsNotNull(Val(value));
        }

        /// <summary>
        /// Creates a "between" operator.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="min">The min value.</param>
        /// <param name="max">The max value.</param>
        /// <returns>The "between" operator.</returns>
        public virtual IOperator Between(object left, object min, object max)
        {
            return new TernaryOperator(OperatorName.Between, OperatorName.And, left, min, max);
        }

        /// <summary>
        /// Creates a "between" operator.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="min">The min value.</param>
        /// <param name="max">The max value.</param>
        /// <returns>The "between" operator.</returns>
        public virtual IOperator Between(Expression<Func<object>> left, object min, object max)
        {
            return Between(Val(left), min, max);
        }

        /// <summary>
        /// Creates a "between" operator.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="min">The min value.</param>
        /// <param name="max">The max value.</param>
        /// <returns>The "between" operator.</returns>
        public virtual IOperator Between(Expression<Func<object>> left, Expression<Func<object>> min,
            Expression<Func<object>> max)
        {
            if (min == null && max == null)
                return Between(left, min, (object)max);

            return Between(Val(left), Val(min), Val(max));
        }

        /// <summary>
        /// Creates a "not between" operator.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="min">The min value.</param>
        /// <param name="max">The max value.</param>
        /// <returns>The "not between" operator.</returns>
        public virtual IOperator NotBetween(object left, object min, object max)
        {
            return new TernaryOperator(OperatorName.NotBetween, OperatorName.And, left, min, max);
        }

        /// <summary>
        /// Creates a "not between" operator.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="min">The min value.</param>
        /// <param name="max">The max value.</param>
        /// <returns>The "not between" operator.</returns>
        public virtual IOperator NotBetween(Expression<Func<object>> left, object min, object max)
        {
            return NotBetween(Val(left), min, max);
        }

        /// <summary>
        /// Creates a "not between" operator.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="min">The min value.</param>
        /// <param name="max">The max value.</param>
        /// <returns>The "not between" operator.</returns>
        public virtual IOperator NotBetween(Expression<Func<object>> left, Expression<Func<object>> min,
            Expression<Func<object>> max)
        {
            if (min == null && max == null)
                return NotBetween(left, min, (object)max);

            return NotBetween(Val(left), Val(min), Val(max));
        }

        /// <summary>
        /// Creates an "all" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The "all" operator.</returns>
        public virtual IOperator All(IQueryFragment value)
        {
            return new LeftQueryOperator(OperatorName.All, value);
        }

        /// <summary>
        /// Creates an "any" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The "any" operator.</returns>
        public virtual IOperator Any(IQueryFragment value)
        {
            return new LeftQueryOperator(OperatorName.Any, value);
        }

        /// <summary>
        /// Creates an "exists" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The "exists" operator.</returns>
        public virtual IOperator Exists(IQueryFragment value)
        {
            return new LeftQueryOperator(OperatorName.Exists, value);
        }

        /// <summary>
        /// Creates a "some" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The "some" operator.</returns>
        public virtual IOperator Some(IQueryFragment value)
        {
            return new LeftQueryOperator(OperatorName.Some, value);
        }

        /// <summary>
        /// Creates an "and" operator.
        /// </summary>
        /// <value>The "and" operator.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public virtual ILogicalOperator And => new LogicalOperator(OperatorName.And);

        /// <summary>
        /// Creates an "or" operator.
        /// </summary>
        /// <value>The "or" operator.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public virtual ILogicalOperator Or => new LogicalOperator(OperatorName.Or);

        /// <summary>
        /// Creates an "add" operator.
        /// </summary>
        /// <value>The "add" operator.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public virtual IArithOperator Add => new ArithOperator(OperatorName.Add);

        /// <summary>
        /// Creates a "subtract" operator.
        /// </summary>
        /// <value>The "subtract" operator.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public virtual IArithOperator Subtract => new ArithOperator(OperatorName.Subtract);

        /// <summary>
        /// Creates a "multiply" operator.
        /// </summary>
        /// <value>The "multiply" operator.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public virtual IArithOperator Multiply => new ArithOperator(OperatorName.Multiply);

        /// <summary>
        /// Creates a "divide" operator.
        /// </summary>
        /// <value>The "divide" operator.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public virtual IArithOperator Divide => new ArithOperator(OperatorName.Divide);

        /// <summary>
        /// Creates a "modulo" operator.
        /// </summary>
        /// <value>The "modulo" operator.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public virtual IArithOperator Modulo => new ArithOperator(OperatorName.Modulo);

        /// <summary>
        /// Creates a "negate" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The "negate" operator.</returns>
        public virtual IOperator Negate(object value)
        {
            return new LeftOperator(OperatorName.Negate, value);
        }

        /// <summary>
        /// Creates a "negate" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The "negate" operator.</returns>
        public virtual IOperator Negate(Expression<Func<object>> value)
        {
            if (value == null)
                return Negate((object)value);

            return Negate(Val(value));
        }

        /// <summary>
        /// Creates a bitwise "and" operator.
        /// </summary>
        /// <value>The bitwise "and" operator.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public virtual IBitOperator BitAnd => new BitOperator(OperatorName.BitAnd);

        /// <summary>
        /// Creates a bitwise "or" operator.
        /// </summary>
        /// <value>The bitwise "or" operator.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public virtual IBitOperator BitOr => new BitOperator(OperatorName.BitOr);

        /// <summary>
        /// Creates a bitwise "xor" operator.
        /// </summary>
        /// <value>The bitwise "xor" operator.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public virtual IBitOperator BitXor => new BitOperator(OperatorName.BitXor);

        /// <summary>
        /// Creates a bitwise "not" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The bitwise "not" operator.</returns>
        public virtual IOperator BitNot(object value)
        {
            return new LeftOperator(OperatorName.BitNot, value);
        }

        /// <summary>
        /// Creates a bitwise "not" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The bitwise "not" operator.</returns>
        public virtual IOperator BitNot(Expression<Func<object>> value)
        {
            if (value == null)
                return BitNot((object)value);

            return BitNot(Val(value));
        }

        /// <summary>
        /// Creates a "left shift" operator.
        /// </summary>
        /// <value>The "left shift" operator.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public virtual IBitOperator LeftShift => new BitOperator(OperatorName.LeftShift);

        /// <summary>
        /// Creates a "right shift" operator.
        /// </summary>
        /// <value>The "right shift" operator.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public virtual IBitOperator RightShift => new BitOperator(OperatorName.RightShift);

        /// <summary>
        /// Creates an "union" operator.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The "union" operator.</returns>
        public virtual ISetOperator Union(IQueryFragment left, IQueryFragment right)
        {
            return new SetOperator(OperatorName.Union, left, right);
        }

        /// <summary>
        /// Creates an "union all" operator.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The "union all" operator.</returns>
        public virtual ISetOperator UnionAll(IQueryFragment left, IQueryFragment right)
        {
            return new SetOperator(OperatorName.UnionAll, left, right);
        }

        /// <summary>
        /// Creates an "except" operator.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The "except" operator.</returns>
        public virtual ISetOperator Except(IQueryFragment left, IQueryFragment right)
        {
            return new SetOperator(OperatorName.Except, left, right);
        }

        /// <summary>
        /// Creates an "except all" operator.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The "except all" operator.</returns>
        public virtual ISetOperator ExceptAll(IQueryFragment left, IQueryFragment right)
        {
            return new SetOperator(OperatorName.ExceptAll, left, right);
        }

        /// <summary>
        /// Creates an "intersect" operator.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The "intersect" operator.</returns>
        public virtual ISetOperator Intersect(IQueryFragment left, IQueryFragment right)
        {
            return new SetOperator(OperatorName.Intersect, left, right);
        }

        /// <summary>
        /// Creates an "intersect all" operator.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The "intersect all" operator.</returns>
        public virtual ISetOperator IntersectAll(IQueryFragment left, IQueryFragment right)
        {
            return new SetOperator(OperatorName.IntersectAll, left, right);
        }

        /// <summary>
        /// Creates a "case" statement.
        /// </summary>
        /// <value>The "case" statement.</value>
        public virtual ICase Case()
        {
            return new Case();
        }

        /// <summary>
        /// Creates a "case" statement.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <value>The "case" statement.</value>
        public virtual ICase Case(object value)
        {
            return new Case(value);
        }

        /// <summary>
        /// Creates a "case" statement.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <value>The "case" statement.</value>
        public virtual ICase Case(Expression<Func<object>> value)
        {
            if (value == null)
                return Case((object)null);

            return new Case(Val(value));
        }

        /// <summary>
        /// Creates a function.
        /// </summary>
        /// <param name="name">The name of the function.</param>
        /// <returns>The function.</returns>
        public virtual IFunction Function(string name)
        {
            return new Function(name);
        }

        /// <summary>
        /// Creates a list of columns.
        /// </summary>
        /// <value>The list of columns.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public virtual IColList ColList => new ColList();

        /// <summary>
        /// Creates a list of values.
        /// </summary>
        /// <value>The list of values.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public virtual IValList ValList => new ValList();

        /// <summary>
        /// Creates a sublist of values.
        /// </summary>
        /// <value>The sublist of values.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public virtual ISubList SubList => new SubList();

        /// <summary>
        /// Creates an "over" clause.
        /// </summary>
        /// <value>The "over" clause.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public virtual IOver Over => new Over();

        /// <summary>
        /// Creates a CTE.
        /// </summary>
        /// <param name="name">The name of the CTE.</param>
        /// <returns>The CTE.</returns>
        public virtual ICte Cte(string name)
        {
            return new Cte(name);
        }

        /// <summary>
        /// Creates a CTE.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <returns>The CTE.</returns>
        public virtual ICte Cte(IAlias alias)
        {
            return new Cte(alias);
        }

        /// <summary>
        /// Creates a CTE.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <typeparam name="T">The type of the table.</typeparam>
        /// <returns>The CTE.</returns>
        public virtual ICte Cte<T>(Expression<Func<T>> alias)
        {
            return new Cte(Alias<T>(alias));
        }

        /// <summary>
        /// Creates a "with" clause.
        /// </summary>
        /// <value>The "with" clause.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public virtual IWith With => new With();

        /// <summary>
        /// Creates a "select" statement.
        /// </summary>
        /// <value>The "select" statement.</value>
        public virtual ISelect Select()
        {
            return new Select();
        }

        /// <summary>
        /// Creates an "insert" statement.
        /// </summary>
        /// <returns>The "insert" statement.</returns>
        public virtual IInsert Insert()
        {
            return new Insert();
        }

        /// <summary>
        /// Creates an "update" statement.
        /// </summary>
        /// <value>The "update" statement.</value>
        public virtual IUpdate Update()
        {
            return new Update();
        }

        /// <summary>
        /// Creates a "delete" statement.
        /// </summary>
        /// <value>The "delete" statement.</value>
        public virtual IDelete Delete()
        {
            return new Delete();
        }

        /// <summary>
        /// Creates a "from" clause.
        /// </summary>
        /// <param name="tableName">The table name.</param>
        /// <returns>The "from" clause.</returns>
        public virtual IFrom From(string tableName)
        {
            return From(Alias(tableName));
        }

        /// <summary>
        /// Creates a "from" clause.
        /// </summary>
        /// <param name="tableName">The table name.</param>
        /// <param name="aliasName">The alias name.</param>
        /// <returns>The "from" clause.</returns>
        public virtual IFrom From(string tableName, string aliasName)
        {
            return From(Alias(tableName, aliasName));
        }

        /// <summary>
        /// Creates a "from" clause.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <returns>The "from" clause.</returns>
        public virtual IFrom From(IAlias alias)
        {
            return new From(alias);
        }

        /// <summary>
        /// Creates a "from" clause with an expression.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <typeparam name="T">The type of the table.</typeparam>
        /// <returns>The "from" clause.</returns>
        public virtual IFrom From<T>(Expression<Func<T>> alias)
        {
            return new From(Alias<T>(alias));
        }

        /// <summary>
        /// Creates a "from" clause with a subquery.
        /// </summary>
        /// <param name="value">The subquery.</param>
        /// <param name="aliasName">The alias name.</param>
        /// <returns>The "from" clause.</returns>
        public virtual IFrom From(IQueryFragment value, string aliasName)
        {
            return new From(value, aliasName);
        }

        /// <summary>
        /// Creates a "from" clause with a subquery.
        /// </summary>
        /// <param name="value">The subquery.</param>
        /// <param name="alias">The alias.</param>
        /// <returns>The "from" clause.</returns>
        public virtual IFrom From(IQueryFragment value, IAlias alias)
        {
            return new From(value, alias);
        }

        /// <summary>
        /// Creates a "from" clause with a subquery.
        /// </summary>
        /// <param name="value">The subquery.</param>
        /// <param name="alias">The alias.</param>
        /// <typeparam name="T">The type of the table.</typeparam>
        /// <returns>The "from" clause.</returns>
        public virtual IFrom From<T>(IQueryFragment value, Expression<Func<T>> alias)
        {
            return new From(value, Alias<T>(alias));
        }

        /// <summary>
        /// Creates a "from" clause with a dummy table.
        /// <para>If the engine does not need a dummy table, writes nothing.</para>
        /// </summary>
        /// <returns>The "from" clause.</returns>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public virtual IRawSql FromDummy => Suilder.Core.FromDummy.Instance;

        /// <summary>
        /// Creates an "inner join" clause.
        /// </summary>
        /// <value>The "inner join" clause.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public virtual IJoinFrom Inner => new JoinFrom(JoinType.Inner);

        /// <summary>
        /// Creates a "left join" clause.
        /// </summary>
        /// <value>The "left join" clause.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public virtual IJoinFrom Left => new JoinFrom(JoinType.Left);

        /// <summary>
        /// Creates a "right join" clause.
        /// </summary>
        /// <value>The "right join" clause.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public virtual IJoinFrom Right => new JoinFrom(JoinType.Right);

        /// <summary>
        /// Creates a "full join" clause.
        /// <para>Some engines do not support full join.</para>
        /// </summary>
        /// <value>The "full join" clause.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public virtual IJoinFrom Full => new JoinFrom(JoinType.Full);

        /// <summary>
        /// Creates a "cross join" clause.
        /// </summary>
        /// <value>The "cross join" clause.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public virtual IJoinFrom Cross => new JoinFrom(JoinType.Cross);

        /// <summary>
        /// Creates a "join" clause.
        /// </summary>
        /// <param name="tableName">The table name.</param>
        /// <param name="joinType">The type of join, by default inner join.</param>
        /// <returns>The "join" clause.</returns>
        public virtual IJoin Join(string tableName, JoinType joinType = JoinType.Inner)
        {
            return new JoinFrom(joinType).Join(tableName);
        }

        /// <summary>
        /// Creates a "join" clause.
        /// </summary>
        /// <param name="tableName">The table name.</param>
        /// <param name="aliasName">The alias name.</param>
        /// <param name="joinType">The type of join, by default inner join.</param>
        /// <returns>The "join" clause.</returns>
        public virtual IJoin Join(string tableName, string aliasName, JoinType joinType = JoinType.Inner)
        {
            return new JoinFrom(joinType).Join(tableName, aliasName);
        }

        /// <summary>
        /// Creates a "join" clause.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <param name="joinType">The type of join, by default inner join.</param>
        /// <returns>The "join" clause.</returns>
        public virtual IJoin Join(IAlias alias, JoinType joinType = JoinType.Inner)
        {
            return new JoinFrom(joinType).Join(alias);
        }

        /// <summary>
        /// Creates a "join" clause with an expression.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <param name="joinType">The type of join, by default inner join.</param>
        /// <typeparam name="T">The type of the table.</typeparam>
        /// <returns>The "join" clause.</returns>
        public virtual IJoin Join<T>(Expression<Func<T>> alias, JoinType joinType = JoinType.Inner)
        {
            return new JoinFrom(joinType).Join(alias);
        }

        /// <summary>
        /// Creates a "join" clause with a subquery.
        /// </summary>
        /// <param name="value">The subquery.</param>
        /// <param name="aliasName">The alias name.</param>
        /// <param name="joinType">The type of join, by default inner join.</param>
        /// <returns>The "join" clause.</returns>
        public virtual IJoin Join(IQueryFragment value, string aliasName, JoinType joinType = JoinType.Inner)
        {
            return new JoinFrom(joinType).Join(value, aliasName);
        }

        /// <summary>
        /// Creates a "join" clause with a subquery.
        /// </summary>
        /// <param name="value">The subquery.</param>
        /// <param name="alias">The alias.</param>
        /// <param name="joinType">The type of join, by default inner join.</param>
        /// <returns>The "join" clause.</returns>
        public virtual IJoin Join(IQueryFragment value, IAlias alias, JoinType joinType = JoinType.Inner)
        {
            return new JoinFrom(joinType).Join(value, alias);
        }

        /// <summary>
        /// Creates a "join" clause with a subquery.
        /// </summary>
        /// <param name="value">The subquery.</param>
        /// <param name="alias">The alias.</param>
        /// <param name="joinType">The type of join, by default inner join.</param>
        /// <typeparam name="T">The type of the table.</typeparam>
        /// <returns>The "join" clause.</returns>
        public virtual IJoin Join<T>(IQueryFragment value, Expression<Func<T>> alias, JoinType joinType = JoinType.Inner)
        {
            return new JoinFrom(joinType).Join(value, alias);
        }

        /// <summary>
        /// Creates an "order by" clause.
        /// </summary>
        /// <value>The "order by" clause.</value>
        public virtual IOrderBy OrderBy()
        {
            return new OrderBy();
        }

        /// <summary>
        /// Creates a "top" clause.
        /// </summary>
        /// <param name="fetch">The number of rows to return.</param>
        /// <returns>The "top" clause.</returns>
        public virtual ITop Top(object fetch)
        {
            return new Top(fetch);
        }

        /// <summary>
        /// Creates an "offset" clause.
        /// </summary>
        /// <param name="offset">The number of rows to skip.</param>
        /// <returns>The "offset fetch" clause.</returns>
        public virtual IOffset Offset(object offset)
        {
            return new OffsetFetch().Offset(offset);
        }

        /// <summary>
        /// Creates an "offset fetch" clause.
        /// </summary>
        /// <param name="offset">The number of rows to skip.</param>
        /// <param name="fetch">The number of rows to return.</param>
        /// <returns>The "offset fetch" clause.</returns>
        public virtual IOffset Offset(object offset, object fetch)
        {
            return new OffsetFetch().Offset(offset).Fetch(fetch);
        }

        /// <summary>
        /// Creates a "fetch" clause.
        /// </summary>
        /// <param name="fetch">The number of rows to return.</param>
        /// <returns>The "offset fetch" clause.</returns>
        public virtual IOffset Fetch(object fetch)
        {
            return new OffsetFetch().Fetch(fetch);
        }

        /// <summary>
        /// Creates a query object.
        /// </summary>
        /// <value>The query.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public virtual IQuery Query => new Query();

        /// <summary>
        /// Creates a raw SQL fragment.
        /// </summary>
        /// <param name="sql">The raw SQL.</param>
        /// <returns>The raw fragment.</returns>
        public virtual IRawSql Raw(string sql)
        {
            return new RawSql(sql);
        }

        /// <summary>
        /// Creates a raw SQL fragment.
        /// <para>The values can be any object even other <see cref="IQueryFragment"/>.</para>
        /// <para>For escaped table and column names use an <see cref="IAlias"/> or an <see cref="IColumn"/> value.</para>
        /// </summary>
        /// <param name="sql">A composite format string, each item takes the following form: {index}.</param>
        /// <param name="values">An object array that contains zero or more objects to add to the raw SQL.</param>
        /// <returns>The raw fragment.</returns>
        /// <exception cref="FormatException">The format of <paramref name="sql"/> is invalid or the index of a format item
        /// is less than zero, or greater than or equal to the length of the <paramref name="values"/>  array.</exception>
        public virtual IRawSql Raw(string sql, params object[] values)
        {
            return new RawSql(sql, values);
        }

        /// <summary>
        /// Creates a raw SQL query.
        /// </summary>
        /// <param name="sql">The raw SQL.</param>
        /// <returns>The raw query.</returns>
        public virtual IRawQuery RawQuery(string sql)
        {
            return new RawQuery(sql);
        }

        /// <summary>
        /// Creates a raw SQL query.
        /// <para>The values can be any object even other <see cref="IQueryFragment"/>.</para>
        /// <para>For escaped table and column names use an <see cref="IAlias"/> or an <see cref="IColumn"/> value.</para>
        /// </summary>
        /// <param name="sql">A composite format string, each item takes the following form: {index}.</param>
        /// <param name="values">An object array that contains zero or more objects to add to the raw SQL.</param>
        /// <returns>The raw fragment.</returns>
        /// <exception cref="FormatException">The format of <paramref name="sql"/> is invalid or the index of a format item
        /// is less than zero, or greater than or equal to the length of the <paramref name="values"/>  array.</exception>
        public virtual IRawQuery RawQuery(string sql, params object[] values)
        {
            return new RawQuery(sql, values);
        }

        /// <summary>
        /// Creates a SQL type.
        /// </summary>
        /// <param name="name">The type name.</param>
        /// <returns>The SQL type.</returns>
        public virtual ISqlType Type(string name)
        {
            return new SqlType(name, null, null);
        }

        /// <summary>
        /// Creates a SQL type.
        /// </summary>
        /// <param name="name">The type name.</param>
        /// <param name="length">The length of the type or precision for numbers.</param>
        /// <returns>The SQL type.</returns>
        public virtual ISqlType Type(string name, int length)
        {
            return new SqlType(name, length, null);
        }

        /// <summary>
        /// Creates a SQL type.
        /// </summary>
        /// <param name="name">The type name.</param>
        /// <param name="precision">The precision of the type.</param>
        /// <param name="scale">The scale of the type.</param>
        /// <returns>The SQL type.</returns>
        public virtual ISqlType Type(string name, int precision, int scale)
        {
            return new SqlType(name, precision, scale);
        }

        /// <summary>
        /// Creates a pattern that match the start of the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The like start pattern.</returns>
        public virtual string ToLikeStart(string value)
        {
            if (value.Length == 0 || value[value.Length - 1] != '%')
                value += "%";
            return value;
        }

        /// <summary>
        /// Creates a pattern that match the end of the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The like end pattern.</returns>
        public virtual string ToLikeEnd(string value)
        {
            if (value.Length == 0 || value[0] != '%')
                value = "%" + value;
            return value;
        }

        /// <summary>
        /// Creates a pattern that match anywhere of the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The like anywhere value.</returns>
        public virtual string ToLikeAny(string value)
        {
            if (value.Length == 0 || value[0] != '%')
                value = "%" + value;
            if (value[value.Length - 1] != '%')
                value += "%";
            return value;
        }
    }
}