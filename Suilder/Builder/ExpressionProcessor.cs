using System;
using System.Linq.Expressions;
using System.Text;
using Suilder.Core;
using Suilder.Functions;

namespace Suilder.Builder
{
    /// <summary>
    /// Utility class to compile an expression to an <see cref="IQueryFragment"/>.
    /// </summary>
    public static partial class ExpressionProcessor
    {
        /// <summary>
        /// Determines if the <see cref="MemberExpression"/> is an alias.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns><see langword="true"/> if the expression is an alias, otherwise, <see langword="false"/>.</returns>
        public static bool IsAlias(MemberExpression expression)
        {
            while (true)
            {
                switch (expression.Expression)
                {
                    case MemberExpression memberExpression:
                        expression = memberExpression;
                        break;
                    case ConstantExpression _:
                    case null:
                        return Tables.Contains(expression.Type.FullName);
                    default:
                        return false;
                }
            }
        }

        /// <summary>
        /// Compiles a <see cref="LambdaExpression"/> to an <see cref="IAlias"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <typeparam name="T">The type of the alias.</typeparam>
        /// <returns>The alias.</returns>
        /// <exception cref="ArgumentException">The expression is invalid.</exception>
        public static IAlias<T> ParseAlias<T>(LambdaExpression expression)
        {
            return ParseAlias<T>(expression.Body);
        }

        /// <summary>
        /// Compiles an <see cref="Expression"/> to an <see cref="IAlias"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <typeparam name="T">The type of the alias.</typeparam>
        /// <returns>The alias.</returns>
        /// <exception cref="ArgumentException">The expression is invalid.</exception>
        public static IAlias<T> ParseAlias<T>(Expression expression)
        {
            if (!(expression is MemberExpression memberExpression))
                throw new ArgumentException("Invalid expression.");

            if (memberExpression.Expression is MemberExpression)
                throw new ArgumentException("Invalid expression.");

            return SqlBuilder.Instance.Alias<T>(memberExpression.Member.Name);
        }

        /// <summary>
        /// Compiles a <see cref="LambdaExpression"/> to an <see cref="IAlias"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The alias.</returns>
        /// <exception cref="ArgumentException">The expression is invalid.</exception>
        public static IAlias ParseAlias(LambdaExpression expression)
        {
            return ParseAlias(expression.Body);
        }

        /// <summary>
        /// Compiles an <see cref="Expression"/> to an <see cref="IAlias"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The alias.</returns>
        /// <exception cref="ArgumentException">The expression is invalid.</exception>
        public static IAlias ParseAlias(Expression expression)
        {
            if (!(expression is MemberExpression memberExpression))
                throw new ArgumentException("Invalid expression.");

            if (memberExpression.Expression is MemberExpression)
                throw new ArgumentException("Invalid expression.");

            return SqlBuilder.Instance.Alias(memberExpression.Type, memberExpression.Member.Name);
        }

        /// <summary>
        /// Compiles a <see cref="LambdaExpression"/> to an <see cref="IColumn"/>.
        /// </summary>
        /// <param name="tableName">The table name or his alias.</param>
        /// <param name="expression">The expression.</param>
        /// <typeparam name="T">The type of the alias.</typeparam>
        /// <returns>The column.</returns>
        /// <exception cref="ArgumentException">The expression is invalid.</exception>
        public static IColumn ParseColumn<T>(string tableName, LambdaExpression expression)
        {
            return ParseColumn<T>(tableName, expression.Body);
        }

        /// <summary>
        /// Compiles an <see cref="Expression"/> to an <see cref="IColumn"/>.
        /// </summary>
        /// <param name="tableName">The table name or his alias.</param>
        /// <param name="expression">The expression.</param>
        /// <typeparam name="T">The type of the alias.</typeparam>
        /// <returns>The column.</returns>
        /// <exception cref="ArgumentException">The expression is invalid.</exception>
        public static IColumn ParseColumn<T>(string tableName, Expression expression)
        {
            switch (expression)
            {
                case UnaryExpression unaryExpression:
                    if (unaryExpression.NodeType == ExpressionType.Convert
                        || unaryExpression.NodeType == ExpressionType.ConvertChecked)
                    {
                        return ParseColumn<T>(tableName, unaryExpression.Operand);
                    }
                    break;
                case MemberExpression firstExp:
                    string column = firstExp.Member.Name;

                    switch (firstExp.Expression)
                    {
                        case MemberExpression lastExp:
                            column = ParseColumn(lastExp, column.Length).Append(column).ToString();
                            return SqlBuilder.Instance.Col<T>(tableName, column);
                        case ParameterExpression _:
                            return SqlBuilder.Instance.Col<T>(tableName, column);
                        default:
                            throw new ArgumentException("Invalid expression.");
                    }
                case ParameterExpression _:
                    return SqlBuilder.Instance.Col<T>(tableName, "*");
            }

            throw new ArgumentException("Invalid expression.");
        }

        /// <summary>
        /// Gets the full column name of a <see cref="MemberExpression"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="capacity">The suggested starting size of the returned <see cref="StringBuilder"/>.</param>
        /// <returns>A <see cref="StringBuilder"/> that contains the full column name.</returns>
        /// <exception cref="ArgumentException">The expression is invalid.</exception>
        private static StringBuilder ParseColumn(MemberExpression expression, int capacity)
        {
            string column = expression.Member.Name;
            capacity += column.Length + 1;

            switch (expression.Expression)
            {
                case MemberExpression memberExpression:
                    return ParseColumn(memberExpression, capacity).Append(column).Append('.');
                case ParameterExpression _:
                    return new StringBuilder(capacity).Append(column).Append('.');
                default:
                    throw new ArgumentException("Invalid expression.");
            }
        }

        /// <summary>
        /// Compiles a <see cref="LambdaExpression"/> to an <see cref="IColumn"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The column.</returns>
        /// <exception cref="ArgumentException">The expression is invalid.</exception>
        public static IColumn ParseColumn(LambdaExpression expression)
        {
            return ParseColumn(expression.Body);
        }

        /// <summary>
        /// Compiles an <see cref="Expression"/> to an <see cref="IColumn"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The column.</returns>
        /// <exception cref="ArgumentException">The expression is invalid.</exception>
        public static IColumn ParseColumn(Expression expression)
        {
            switch (expression)
            {
                case UnaryExpression unaryExpression:
                    if (unaryExpression.NodeType == ExpressionType.Convert
                        || unaryExpression.NodeType == ExpressionType.ConvertChecked)
                    {
                        return ParseColumn(unaryExpression.Operand);
                    }
                    break;
                case MemberExpression memberExpression:
                    return ParseColumn(memberExpression);
            }

            throw new ArgumentException("Invalid expression.");
        }

        /// <summary>
        /// Compiles a <see cref="MemberExpression"/> to an <see cref="IColumn"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The column.</returns>
        /// <exception cref="ArgumentException">The expression is invalid.</exception>
        public static IColumn ParseColumn(MemberExpression expression)
        {
            switch (expression.Expression)
            {
                case MemberExpression firstExp:
                    string column = expression.Member.Name;

                    switch (firstExp.Expression)
                    {
                        case MemberExpression lastExp:
                            column = ParseColumn(ref lastExp, firstExp.Member.Name, column.Length).Append(column)
                                .ToString();
                            return SqlBuilder.Instance.Col(lastExp.Type, lastExp.Member.Name, column);
                        case ConstantExpression _:
                        case null:
                            return SqlBuilder.Instance.Col(firstExp.Type, firstExp.Member.Name, column);
                        default:
                            throw new ArgumentException("Invalid expression.");
                    }
                case ConstantExpression _:
                case null:
                    return SqlBuilder.Instance.Col(expression.Type, expression.Member.Name, "*");
                default:
                    throw new ArgumentException("Invalid expression.");
            }
        }

        /// <summary>
        /// Gets the full column name of a <see cref="MemberExpression"/>.
        /// </summary>
        /// <param name="expression">The expression. When this method returns, contains the last
        /// <see cref="MemberExpression"/>.</param>
        /// <param name="column">The column name to append to the returned <see cref="StringBuilder"/>.</param>
        /// <param name="capacity">The suggested starting size of the returned <see cref="StringBuilder"/>.</param>
        /// <returns>A <see cref="StringBuilder"/> that contains the full column name.</returns>
        /// <exception cref="ArgumentException">The expression is invalid.</exception>
        private static StringBuilder ParseColumn(ref MemberExpression expression, string column, int capacity)
        {
            capacity += column.Length + 1;

            switch (expression.Expression)
            {
                case MemberExpression memberExpression:
                    StringBuilder builder = ParseColumn(ref memberExpression, expression.Member.Name, capacity);
                    expression = memberExpression;
                    return builder.Append(column).Append('.');
                case ConstantExpression _:
                case null:
                    return new StringBuilder(capacity).Append(column).Append('.');
                default:
                    throw new ArgumentException("Invalid expression.");
            }
        }

        /// <summary>
        /// Compiles a <see cref="LambdaExpression"/> to a value.
        /// <para>The value can be a literal value or an <see cref="IQueryFragment"/> that represents a value,
        /// like a column, a function or an arithmetic operator.</para>
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The value.</returns>
        /// <exception cref="ArgumentException">The expression is invalid.</exception>
        public static object ParseValue(LambdaExpression expression)
        {
            return ParseValue(expression.Body);
        }

        /// <summary>
        /// Compiles an <see cref="Expression"/> to a value.
        /// <para>The value can be a literal value or an <see cref="IQueryFragment"/> that represents a value,
        /// like a column, a function or an arithmetic operator.</para>
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The value.</returns>
        /// <exception cref="ArgumentException">The expression is invalid.</exception>
        public static object ParseValue(Expression expression)
        {
            switch (expression)
            {
                case ConstantExpression constantExpression:
                    return Compile(constantExpression);
                case MemberExpression memberExpression:
                    return ParseValue(memberExpression);
                case NewExpression newExpression:
                    return Compile(newExpression);
                case NewArrayExpression newArrayExpression:
                    return Compile(newArrayExpression);
                case ListInitExpression listInitExpression:
                    return Compile(listInitExpression);
                case MethodCallExpression methodCallExpression:
                    return ParseMethod(methodCallExpression);
                case UnaryExpression unaryExpression:
                    return ParseValue(unaryExpression);
                case BinaryExpression binaryExpression:
                    return ParseValue(binaryExpression);
                case ConditionalExpression conditionalExpression:
                    return ParseCase(conditionalExpression);
                default:
                    throw new ArgumentException("Invalid expression.");
            }
        }

        /// <summary>
        /// Compiles a <see cref="MemberExpression"/> to a value.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The value.</returns>
        /// <exception cref="ArgumentException">The expression is invalid.</exception>
        public static object ParseValue(MemberExpression expression)
        {
            return IsAlias(expression) ? ParseColumn(expression) : Compile(expression);
        }

        /// <summary>
        /// Compiles a <see cref="MethodCallExpression"/> to a value.
        /// <para>If the method is registered it returns an <see cref="IQueryFragment"/>.</para>
        /// <para>Else, it invoke the method and return his value.</para>
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The value.</returns>
        /// <exception cref="ArgumentException">The expression is invalid.</exception>
        public static object ParseMethod(MethodCallExpression expression)
        {
            string methodName = GetMethodFullName(expression);

            if (Functions.TryGetValue(methodName, out var method))
            {
                return method(expression);
            }
            else
            {
                return Compile(expression);
            }
        }

        /// <summary>
        /// Compiles a <see cref="UnaryExpression"/> to a value.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The value.</returns>
        /// <exception cref="ArgumentException">The expression is invalid.</exception>
        public static object ParseValue(UnaryExpression expression)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                case ExpressionType.UnaryPlus:
                    return ParseValue(expression.Operand);
                case ExpressionType.Not:
                    if (expression.Type != typeof(bool))
                        return SqlBuilder.Instance.BitNot(ParseValue(expression.Operand));
                    break;
                case ExpressionType.Negate:
                case ExpressionType.NegateChecked:
                    return SqlBuilder.Instance.Negate(ParseValue(expression.Operand));
                case ExpressionType.ArrayLength:
                    return Compile(expression);
            }

            return ParseBoolOperator(expression);
        }

        /// <summary>
        /// Compiles a <see cref="BinaryExpression"/> to a value.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The value.</returns>
        /// <exception cref="ArgumentException">The expression is invalid.</exception>
        public static object ParseValue(BinaryExpression expression)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.Add:
                    if (expression.Type != typeof(string))
                        return ParseArithmeticOperator(expression);
                    else
                        return ParseFunction(expression);
                case ExpressionType.AddChecked:
                case ExpressionType.Subtract:
                case ExpressionType.SubtractChecked:
                case ExpressionType.Multiply:
                case ExpressionType.MultiplyChecked:
                case ExpressionType.Divide:
                case ExpressionType.Modulo:
                    return ParseArithmeticOperator(expression);
                case ExpressionType.And:
                case ExpressionType.Or:
                    if (expression.Type != typeof(bool))
                        return ParseBitOperator(expression);
                    break;
                case ExpressionType.ExclusiveOr:
                case ExpressionType.LeftShift:
                case ExpressionType.RightShift:
                    return ParseBitOperator(expression);
                case ExpressionType.Coalesce:
                    return SqlFn.Coalesce(ParseValue(expression.Left), ParseValue(expression.Right));
                case ExpressionType.ArrayIndex:
                    return Compile(expression);
            }

            return ParseBoolOperator(expression);
        }

        /// <summary>
        /// Compiles a <see cref="LambdaExpression"/> to an <see cref="IOperator"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The operator.</returns>
        /// <exception cref="ArgumentException">The expression is invalid.</exception>
        public static IOperator ParseBoolOperator(LambdaExpression expression)
        {
            return ParseBoolOperator(expression.Body);
        }

        /// <summary>
        /// Compiles an <see cref="Expression"/> to an <see cref="IOperator"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The operator.</returns>
        /// <exception cref="ArgumentException">The expression is invalid.</exception>
        public static IOperator ParseBoolOperator(Expression expression)
        {
            switch (expression)
            {
                case MemberExpression memberExpression:
                    return ParseBoolOperator(memberExpression);
                case MethodCallExpression methodCallExpression:
                    return ParseBoolOperator(methodCallExpression);
                case UnaryExpression unaryExpression:
                    return ParseBoolOperator(unaryExpression);
                case BinaryExpression binaryExpression:
                    return ParseBoolOperator(binaryExpression);
                default:
                    throw new ArgumentException("Invalid expression.");
            }
        }

        /// <summary>
        /// Compiles a <see cref="MemberExpression"/> to an <see cref="IOperator"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The operator.</returns>
        /// <exception cref="ArgumentException">The expression is invalid.</exception>
        public static IOperator ParseBoolOperator(MemberExpression expression)
        {
            if (expression.Type == typeof(bool))
                return SqlBuilder.Instance.Eq(ParseValue(expression), true);

            throw new ArgumentException("Invalid expression.");
        }

        /// <summary>
        /// Compiles a <see cref="MethodCallExpression"/> to an <see cref="IOperator"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The operator.</returns>
        /// <exception cref="ArgumentException">The expression is invalid.</exception>
        public static IOperator ParseBoolOperator(MethodCallExpression expression)
        {
            if (expression.Type == typeof(bool))
            {
                object value = ParseMethod(expression);
                switch (value)
                {
                    case IOperator op:
                        return op;
                    case IQueryFragment queryFragment:
                        return SqlBuilder.Instance.Eq(queryFragment, true);
                }
            }

            throw new ArgumentException("Invalid expression.");
        }

        /// <summary>
        /// Compiles a <see cref="UnaryExpression"/> to an <see cref="IOperator"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The operator.</returns>
        /// <exception cref="ArgumentException">The expression is invalid.</exception>
        public static IOperator ParseBoolOperator(UnaryExpression expression)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.Convert:
                    return ParseBoolOperator(expression.Operand);
                case ExpressionType.Not:
                    if (expression.Type == typeof(bool))
                    {
                        switch (expression.Operand)
                        {
                            case MemberExpression memberExpression:
                                return SqlBuilder.Instance.Eq(ParseValue(memberExpression), false);
                            case MethodCallExpression methodCallExpression:
                                object value = ParseMethod(methodCallExpression);
                                switch (value)
                                {
                                    case IOperator op:
                                        return SqlBuilder.Instance.Not(op);
                                    case IQueryFragment queryFragment:
                                        return SqlBuilder.Instance.Eq(queryFragment, false);
                                }
                                break;
                            default:
                                return SqlBuilder.Instance.Not(ParseBoolOperator(expression.Operand));
                        }
                    }
                    break;
            }

            throw new ArgumentException("Invalid expression.");
        }

        /// <summary>
        /// Compiles a <see cref="BinaryExpression"/> to an <see cref="IOperator"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The operator.</returns>
        /// <exception cref="ArgumentException">The expression is invalid.</exception>
        public static IOperator ParseBoolOperator(BinaryExpression expression)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.Equal:
                    return SqlBuilder.Instance.Eq(ParseValue(expression.Left), ParseValue(expression.Right));
                case ExpressionType.NotEqual:
                    return SqlBuilder.Instance.NotEq(ParseValue(expression.Left), ParseValue(expression.Right));
                case ExpressionType.LessThan:
                    return SqlBuilder.Instance.Lt(ParseValue(expression.Left), ParseValue(expression.Right));
                case ExpressionType.LessThanOrEqual:
                    return SqlBuilder.Instance.Le(ParseValue(expression.Left), ParseValue(expression.Right));
                case ExpressionType.GreaterThan:
                    return SqlBuilder.Instance.Gt(ParseValue(expression.Left), ParseValue(expression.Right));
                case ExpressionType.GreaterThanOrEqual:
                    return SqlBuilder.Instance.Ge(ParseValue(expression.Left), ParseValue(expression.Right));
                case ExpressionType.And:
                case ExpressionType.Or:
                    if (expression.Type == typeof(bool))
                        return ParseLogicalOperator(expression);
                    break;
                case ExpressionType.AndAlso:
                case ExpressionType.OrElse:
                    return ParseLogicalOperator(expression);
            }

            throw new ArgumentException("Invalid expression.");
        }

        /// <summary>
        /// Compiles a <see cref="BinaryExpression"/> to an <see cref="ILogicalOperator"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The logical operator.</returns>
        /// <exception cref="ArgumentException">The expression is invalid.</exception>
        public static ILogicalOperator ParseLogicalOperator(BinaryExpression expression)
        {
            ILogicalOperator logicalOperator = null;
            Expression left = expression.Left;

            if (left.NodeType != expression.NodeType)
            {
                switch (expression.NodeType)
                {
                    case ExpressionType.And:
                        if (left.NodeType != ExpressionType.AndAlso)
                            logicalOperator = SqlBuilder.Instance.And;
                        break;
                    case ExpressionType.AndAlso:
                        if (left.NodeType != ExpressionType.And)
                            logicalOperator = SqlBuilder.Instance.And;
                        break;
                    case ExpressionType.Or:
                        if (left.NodeType != ExpressionType.OrElse)
                            logicalOperator = SqlBuilder.Instance.Or;
                        break;
                    case ExpressionType.OrElse:
                        if (left.NodeType != ExpressionType.Or)
                            logicalOperator = SqlBuilder.Instance.Or;
                        break;
                    default:
                        throw new ArgumentException("Invalid expression.");
                }
            }

            if (logicalOperator != null)
            {
                logicalOperator.Add(ParseBoolOperator(left));
            }
            else
            {
                logicalOperator = (ILogicalOperator)ParseBoolOperator(left);
            }

            return logicalOperator.Add(ParseBoolOperator(expression.Right));
        }

        /// <summary>
        /// Compiles a <see cref="BinaryExpression"/> to an <see cref="IArithOperator"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The arithmetic operator.</returns>
        /// <exception cref="ArgumentException">The expression is invalid.</exception>
        public static IArithOperator ParseArithmeticOperator(BinaryExpression expression)
        {
            IArithOperator arithOperator = null;
            Expression left = expression.Left;

            while (left.NodeType == ExpressionType.Convert || left.NodeType == ExpressionType.ConvertChecked)
            {
                left = ((UnaryExpression)left).Operand;
            }

            if (left.NodeType != expression.NodeType)
            {
                switch (expression.NodeType)
                {
                    case ExpressionType.Add:
                        if (left.NodeType != ExpressionType.AddChecked)
                            arithOperator = SqlBuilder.Instance.Add;
                        break;
                    case ExpressionType.AddChecked:
                        if (left.NodeType != ExpressionType.Add)
                            arithOperator = SqlBuilder.Instance.Add;
                        break;
                    case ExpressionType.Subtract:
                        if (left.NodeType != ExpressionType.SubtractChecked)
                            arithOperator = SqlBuilder.Instance.Subtract;
                        break;
                    case ExpressionType.SubtractChecked:
                        if (left.NodeType != ExpressionType.Subtract)
                            arithOperator = SqlBuilder.Instance.Subtract;
                        break;
                    case ExpressionType.Multiply:
                        if (left.NodeType != ExpressionType.MultiplyChecked)
                            arithOperator = SqlBuilder.Instance.Multiply;
                        break;
                    case ExpressionType.MultiplyChecked:
                        if (left.NodeType != ExpressionType.Multiply)
                            arithOperator = SqlBuilder.Instance.Multiply;
                        break;
                    case ExpressionType.Divide:
                        arithOperator = SqlBuilder.Instance.Divide;
                        break;
                    case ExpressionType.Modulo:
                        arithOperator = SqlBuilder.Instance.Modulo;
                        break;
                    default:
                        throw new ArgumentException("Invalid expression.");
                }
            }

            if (arithOperator != null)
            {
                arithOperator.Add(ParseValue(left));
            }
            else
            {
                arithOperator = (IArithOperator)ParseValue(left);
            }

            return arithOperator.Add(ParseValue(expression.Right));
        }

        /// <summary>
        /// Compiles a <see cref="BinaryExpression"/> to an <see cref="IBitOperator"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The bitwise operator.</returns>
        /// <exception cref="ArgumentException">The expression is invalid.</exception>
        public static IBitOperator ParseBitOperator(BinaryExpression expression)
        {
            IBitOperator bitOperator = null;
            Expression left = expression.Left;

            while (left.NodeType == ExpressionType.Convert || left.NodeType == ExpressionType.ConvertChecked)
            {
                left = ((UnaryExpression)left).Operand;
            }

            if (left.NodeType != expression.NodeType)
            {
                switch (expression.NodeType)
                {
                    case ExpressionType.And:
                        bitOperator = SqlBuilder.Instance.BitAnd;
                        break;
                    case ExpressionType.Or:
                        bitOperator = SqlBuilder.Instance.BitOr;
                        break;
                    case ExpressionType.ExclusiveOr:
                        bitOperator = SqlBuilder.Instance.BitXor;
                        break;
                    case ExpressionType.LeftShift:
                        bitOperator = SqlBuilder.Instance.LeftShift;
                        break;
                    case ExpressionType.RightShift:
                        bitOperator = SqlBuilder.Instance.RightShift;
                        break;
                    default:
                        throw new ArgumentException("Invalid expression.");
                }
            }

            if (bitOperator != null)
            {
                bitOperator.Add(ParseValue(left));
            }
            else
            {
                bitOperator = (IBitOperator)ParseValue(left);
            }

            return bitOperator.Add(ParseValue(expression.Right));
        }

        /// <summary>
        /// Compiles a <see cref="BinaryExpression"/> to an <see cref="IFunction"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The function.</returns>
        /// <exception cref="ArgumentException">The expression is invalid.</exception>
        public static IFunction ParseFunction(BinaryExpression expression)
        {
            IFunction func = null;
            Expression left = expression.Left;

            while (left.NodeType == ExpressionType.Convert)
            {
                left = ((UnaryExpression)left).Operand;
            }

            switch (expression.NodeType)
            {
                case ExpressionType.Add:
                    if (left.NodeType != expression.NodeType || left.Type != typeof(string))
                        func = SqlBuilder.Instance.Function(FunctionName.Concat);
                    break;
                default:
                    throw new ArgumentException("Invalid expression.");
            }

            if (func != null)
            {
                func.Add(ParseValue(left));
            }
            else
            {
                func = (IFunction)ParseValue(left);
            }

            return func.Add(ParseValue(expression.Right));
        }

        /// <summary>
        /// Compiles a <see cref="ConditionalExpression"/> to an <see cref="ICase"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The "case" statement.</returns>
        /// <exception cref="ArgumentException">The expression is invalid.</exception>
        public static ICase ParseCase(ConditionalExpression expression)
        {
            return ParseCase(expression, null);
        }

        /// <summary>
        /// Compiles a <see cref="ConditionalExpression"/> to an <see cref="ICase"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="caseWhen">The "case" to append conditions.</param>
        /// <returns>The "case" statement.</returns>
        /// <exception cref="ArgumentException">The expression is invalid.</exception>
        private static ICase ParseCase(ConditionalExpression expression, ICase caseWhen)
        {
            if (caseWhen == null)
                caseWhen = SqlBuilder.Instance.Case();

            caseWhen.When(ParseBoolOperator(expression.Test), ParseValue(expression.IfTrue));

            if (expression.IfFalse.NodeType == ExpressionType.Conditional)
            {
                ParseCase((ConditionalExpression)expression.IfFalse, caseWhen);
            }
            else
            {
                caseWhen.Else(ParseValue(expression.IfFalse));
            }

            return caseWhen;
        }
    }
}