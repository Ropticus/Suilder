using System;
using System.Diagnostics;
using System.Linq.Expressions;
using Suilder.Core;

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
        /// <param name="force">If true, it overrides the registered builder.</param>
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
            Type type = typeof(Alias<>).MakeGenericType(aliasType);
            return (IAlias)Activator.CreateInstance(type, new object[] { aliasName });
        }

        /// <summary>
        /// Creates a typed alias.
        /// </summary>
        /// <typeparam name="T">The type of the table.</typeparam>
        /// <returns>The alias.</returns>
        public virtual IAlias<T> Alias<T>()
        {
            return new Alias<T>();
        }

        /// <summary>
        /// Creates a typed alias.
        /// </summary>
        /// <param name="aliasName">The alias name.</param>
        /// <typeparam name="T">The type of the table.</typeparam>
        /// <returns>The alias.</returns>
        public virtual IAlias<T> Alias<T>(string aliasName)
        {
            return new Alias<T>(aliasName);
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
        /// <typeparam name="T">The type of the table.</typeparam>
        /// <returns>The alias.</returns>
        public virtual IAlias<T> Alias<T>(Expression expression)
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
            int index = columnName.LastIndexOf('.');
            if (index >= 0)
                return new Column<T>(columnName.Substring(0, index), columnName.Substring(index + 1));
            else
                return new Column<T>(null, columnName);
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
            return new Column<T>(tableName, columnName);
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
            Type columType = typeof(Column<>).MakeGenericType(aliasType);
            return (IColumn)Activator.CreateInstance(columType, new object[] { tableName, columnName });
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
        public virtual IColumn Col(Expression expression)
        {
            return ExpressionProcessor.ParseColumn(expression);
        }

        /// <summary>
        /// Creates a value with an expression.
        /// <para>The value can be a literal value or a <see cref="IQueryFragment"/> that represent a value
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
        /// <para>The value can be a literal value or a <see cref="IQueryFragment"/> that represent a value
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
        public virtual IOperator Op(Expression expression)
        {
            return ExpressionProcessor.ParseBoolOperator(expression);
        }

        /// <summary>
        /// Creates an "equal to" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "equal to" operator.</returns>
        public virtual IOperator Eq(object left, object right)
        {
            if (left == null || right == null)
                return IsNull(left ?? right);

            return new Operator("=", left, right);
        }

        /// <summary>
        /// Creates an "equal to" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "equal to" operator.</returns>
        public virtual IOperator Eq(Expression<Func<object>> left, object right)
        {
            return Eq(Val(left), right);
        }

        /// <summary>
        /// Creates an "equal to" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
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
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "not equal to" operator.</returns>
        public virtual IOperator NotEq(object left, object right)
        {
            if (left == null || right == null)
                return IsNotNull(left ?? right);

            return new Operator("<>", left, right);
        }

        /// <summary>
        /// Creates a "not equal to" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "not equal to" operator.</returns>
        public virtual IOperator NotEq(Expression<Func<object>> left, object right)
        {
            return NotEq(Val(left), right);
        }

        /// <summary>
        /// Creates a "not equal to" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
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
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "like" operator.</returns>
        public virtual IOperator Like(object left, object right)
        {
            return new Operator("LIKE", left, right);
        }

        /// <summary>
        /// Creates a "like" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "like" operator.</returns>
        public virtual IOperator Like(Expression<Func<object>> left, object right)
        {
            return Like(Val(left), right);
        }

        /// <summary>
        /// Creates a "like" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
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
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "not like" operator.</returns>
        public virtual IOperator NotLike(object left, object right)
        {
            return new Operator("NOT LIKE", left, right);
        }

        /// <summary>
        /// Creates a "not like" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "not like" operator.</returns>
        public virtual IOperator NotLike(Expression<Func<object>> left, object right)
        {
            return NotLike(Val(left), right);
        }

        /// <summary>
        /// Creates a "not like" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
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
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "less than" operator.</returns>
        public virtual IOperator Lt(object left, object right)
        {
            return new Operator("<", left, right);
        }

        /// <summary>
        /// Creates a "less than" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "less than" operator.</returns>
        public virtual IOperator Lt(Expression<Func<object>> left, object right)
        {
            return Lt(Val(left), right);
        }

        /// <summary>
        /// Creates a "less than" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
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
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "less than or equal to" operator.</returns>
        public virtual IOperator Le(object left, object right)
        {
            return new Operator("<=", left, right);
        }

        /// <summary>
        /// Creates a "less than or equal to" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "less than or equal to" operator.</returns>
        public virtual IOperator Le(Expression<Func<object>> left, object right)
        {
            return Le(Val(left), right);
        }

        /// <summary>
        /// Creates a "less than or equal to" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
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
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "greater than" operator.</returns>
        public virtual IOperator Gt(object left, object right)
        {
            return new Operator(">", left, right);
        }

        /// <summary>
        /// Creates a "greater than" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "greater than" operator.</returns>
        public virtual IOperator Gt(Expression<Func<object>> left, object right)
        {
            return Gt(Val(left), right);
        }

        /// <summary>
        /// Creates a "greater than" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
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
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "greater than or equal to" operator.</returns>
        public virtual IOperator Ge(object left, object right)
        {
            return new Operator(">=", left, right);
        }

        /// <summary>
        /// Creates a "greater than or equal to" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "greater than or equal to" operator.</returns>
        public virtual IOperator Ge(Expression<Func<object>> left, object right)
        {
            return Ge(Val(left), right);
        }

        /// <summary>
        /// Creates a "greater than or equal to" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
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
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "in" operator.</returns>
        public virtual IOperator In(object left, object right)
        {
            return new Operator("IN", left, right);
        }

        /// <summary>
        /// Creates an "in" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "in" operator.</returns>
        public virtual IOperator In(Expression<Func<object>> left, object right)
        {
            return In(Val(left), right);
        }

        /// <summary>
        /// Creates an "in" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
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
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "not in" operator.</returns>
        public virtual IOperator NotIn(object left, object right)
        {
            return new Operator("NOT IN", left, right);
        }

        /// <summary>
        /// Creates a "not in" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "not in" operator.</returns>
        public virtual IOperator NotIn(Expression<Func<object>> left, object right)
        {
            return NotIn(Val(left), right);
        }

        /// <summary>
        /// Creates a "not in" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
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
            return new LeftOperator("NOT", value);
        }

        /// <summary>
        /// Creates a "not" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The "not" operator.</returns>
        public virtual IOperator Not(Expression<Func<bool>> value)
        {
            if (value == null)
                return IsNull((object)value);

            return new LeftOperator("NOT", Op(value));
        }

        /// <summary>
        /// Creates an "is null" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The "is null" operator.</returns>
        public virtual IOperator IsNull(object value)
        {
            return new RightOperator("IS NULL", value);
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

            return new RightOperator("IS NULL", Val(value));
        }

        /// <summary>
        /// Creates an "is not null" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The "is not null" operator.</returns>
        public virtual IOperator IsNotNull(object value)
        {
            return new RightOperator("IS NOT NULL", value);
        }

        /// <summary>
        /// Creates an "is not null" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The "is not null" operator.</returns>
        public virtual IOperator IsNotNull(Expression<Func<object>> value)
        {
            if (value == null)
                return IsNull((object)value);

            return new RightOperator("IS NOT NULL", Val(value));
        }

        /// <summary>
        /// Creates an "all" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The "all" operator.</returns>
        public virtual IOperator All(object value)
        {
            return new LeftOperator("ALL", value);
        }

        /// <summary>
        /// Creates an "any" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The "any" operator.</returns>
        public virtual IOperator Any(object value)
        {
            return new LeftOperator("ANY", value);
        }

        /// <summary>
        /// Creates an "exists" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The "exists" operator.</returns>
        public virtual IOperator Exists(object value)
        {
            return new LeftOperator("EXISTS", value);
        }

        /// <summary>
        /// Creates a "some" operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The "some" operator.</returns>
        public virtual IOperator Some(object value)
        {
            return new LeftOperator("SOME", value);
        }

        /// <summary>
        /// Creates an "and" operator.
        /// </summary>
        /// <value>The "and" operator.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public virtual ILogicalOperator And => new LogicalOperator("AND");

        /// <summary>
        /// Creates an "or" operator.
        /// </summary>
        /// <value>The "or" operator.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public virtual ILogicalOperator Or => new LogicalOperator("OR");

        /// <summary>
        /// Creates an "add" operator.
        /// </summary>
        /// <value>The "add" operator.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public virtual IArithOperator Add => new ArithOperator("+");

        /// <summary>
        /// Creates a "subtract" operator.
        /// </summary>
        /// <value>The "subtract" operator.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public virtual IArithOperator Subtract => new ArithOperator("-");

        /// <summary>
        /// Creates a "multiply" operator.
        /// </summary>
        /// <value>The "multiply" operator.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public virtual IArithOperator Multiply => new ArithOperator("*");

        /// <summary>
        /// Creates a "divide" operator.
        /// </summary>
        /// <value>The "divide" operator.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public virtual IArithOperator Divide => new ArithOperator("/");

        /// <summary>
        /// Creates a "modulo" operator.
        /// </summary>
        /// <value>The "modulo" operator.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public virtual IArithOperator Modulo => new ArithOperator("%");

        /// <summary>
        /// Creates a bitwise "and" operator.
        /// </summary>
        /// <value>The bitwise "and" operator.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public virtual IBitOperator BitAnd => new BitOperator("&");

        /// <summary>
        /// Creates a bitwise "or" operator.
        /// </summary>
        /// <value>The bitwise "or" operator.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public virtual IBitOperator BitOr => new BitOperator("|");

        /// <summary>
        /// Creates a bitwise "xor" operator.
        /// </summary>
        /// <value>The bitwise "xor" operator.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public virtual IBitOperator BitXor => new BitOperator("^");

        /// <summary>
        /// Creates an "union" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "union" operator.</returns>
        public virtual IOperator Union(IQueryFragment left, IQueryFragment right)
        {
            return new QueryOperator("UNION", left, right);
        }

        /// <summary>
        /// Creates an "union all" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "union all" operator.</returns>
        public virtual IOperator UnionAll(IQueryFragment left, IQueryFragment right)
        {
            return new QueryOperator("UNION ALL", left, right);
        }

        /// <summary>
        /// Creates an "except" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "except" operator.</returns>
        public virtual IOperator Except(IQueryFragment left, IQueryFragment right)
        {
            return new QueryOperator("EXCEPT", left, right);
        }

        /// <summary>
        /// Creates an "intersect" operator.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>The "intersect" operator.</returns>
        public virtual IOperator Intersect(IQueryFragment left, IQueryFragment right)
        {
            return new QueryOperator("INTERSECT", left, right);
        }

        /// <summary>
        /// Creates a "case" statement.
        /// </summary>
        /// <value>The "case" statement.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public virtual ICase Case => new Case();

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
        /// Creates a "insert" statement.
        /// </summary>
        /// <returns>The "insert" statement.</returns>
        public virtual IInsert Insert()
        {
            return new Insert();
        }

        /// <summary>
        /// Creates a "update" statement.
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
        /// <para>For escaped table and column names use a <see cref="IAlias"/> or a <see cref="IColumn"/> value.</para>
        /// </summary>
        /// <param name="sql">A composite string, each item takes the following form: {index}.</param>
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
        ///<para>The values can be any object even other <see cref="IQueryFragment"/>.</para>
        /// <para>For escaped table and column names use a <see cref="IAlias"/> or a <see cref="IColumn"/> value.</para>
        /// </summary>
        /// <param name="sql">A composite string, each item takes the following form: {index}.</param>
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
                value = value + "%";
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
                value = value + "%";
            return value;
        }
    }
}