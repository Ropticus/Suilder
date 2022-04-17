using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Suilder.Core;
using Suilder.Functions;

namespace Suilder.Builder
{
    /// <summary>
    /// Utility class to compile an expression to an <see cref="IQueryFragment"/>.
    /// </summary>
    public static class ExpressionProcessor
    {
        /// <summary>
        /// Contains the types registered as a table.
        /// </summary>
        private static ISet<string> Tables { get; set; } = new HashSet<string>();

        /// <summary>
        /// The registered functions.
        /// </summary>
        /// <returns>The registered functions.</returns>
        private static IDictionary<string, Func<MethodCallExpression, object>> Functions { get; set; }
            = new ConcurrentDictionary<string, Func<MethodCallExpression, object>>();

        /// <summary>
        /// Determines if the <see cref="MemberExpression"/> is an alias.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns><see langword="true"/> if the expression is an alias, otherwise, <see langword="false"/>.</returns>
        public static bool IsAlias(MemberExpression expression)
        {
            while (expression.Expression is MemberExpression nextExp)
            {
                expression = nextExp;
            }

            return Tables.Contains(expression.Type.FullName);
        }

        /// <summary>
        /// Compiles a <see cref="LambdaExpression"/> to an <see cref="IAlias"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <typeparam name="T">The type of the alias.</typeparam>
        /// <returns>The alias.</returns>
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
        public static IAlias<T> ParseAlias<T>(Expression expression)
        {
            MemberExpression memberExp = expression as MemberExpression;
            if (memberExp == null)
                throw new ArgumentException("Invalid expression.");

            if (memberExp.Expression is MemberExpression)
                throw new ArgumentException("Invalid expression.");

            return SqlBuilder.Instance.Alias<T>(memberExp.Member.Name);
        }

        /// <summary>
        /// Compiles a <see cref="LambdaExpression"/> to an <see cref="IAlias"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The alias.</returns>
        public static IAlias ParseAlias(LambdaExpression expression)
        {
            return ParseAlias(expression.Body);
        }

        /// <summary>
        /// Compiles an <see cref="Expression"/> to an <see cref="IAlias"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The alias.</returns>
        public static IAlias ParseAlias(Expression expression)
        {
            MemberExpression memberExp = expression as MemberExpression;
            if (memberExp == null)
                throw new ArgumentException("Invalid expression.");

            if (memberExp.Expression is MemberExpression)
                throw new ArgumentException("Invalid expression.");

            return SqlBuilder.Instance.Alias(memberExp.Type, memberExp.Member.Name);
        }

        /// <summary>
        /// Compiles a <see cref="LambdaExpression"/> to an <see cref="IColumn"/>.
        /// </summary>
        /// <param name="tableName">The table name or his alias.</param>
        /// <param name="expression">The expression.</param>
        /// <typeparam name="T">The type of the alias.</typeparam>
        /// <returns>The column.</returns>
        public static IColumn ParseColumn<T>(string tableName, LambdaExpression expression)
        {
            return ParseColumn<T>(tableName, expression.Body);
        }

        /// <summary>
        /// Compiles a <see cref="Expression"/> to an <see cref="IColumn"/>.
        /// </summary>
        /// <param name="tableName">The table name or his alias.</param>
        /// <param name="expression">The expression.</param>
        /// <typeparam name="T">The type of the alias.</typeparam>
        /// <returns>The column.</returns>
        public static IColumn ParseColumn<T>(string tableName, Expression expression)
        {
            if (expression.NodeType == ExpressionType.Convert)
                return ParseColumn<T>(tableName, ((UnaryExpression)expression).Operand);

            if (expression is MemberExpression memberExp)
            {
                // Nested property
                if (memberExp.Expression is MemberExpression lastExp)
                {
                    List<string> list = new List<string>
                    {
                        memberExp.Member.Name
                    };

                    do
                    {
                        list.Add(lastExp.Member.Name);
                        lastExp = lastExp.Expression as MemberExpression;
                    } while (lastExp != null);

                    list.Reverse();
                    return SqlBuilder.Instance.Col<T>(tableName, string.Join(".", list));
                }
                else
                {
                    return SqlBuilder.Instance.Col<T>(tableName, memberExp.Member.Name);
                }
            }
            else
            {
                return SqlBuilder.Instance.Col<T>(tableName, "*");
            }
        }

        /// <summary>
        /// Compiles a <see cref="LambdaExpression"/> to an <see cref="IColumn"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The column.</returns>
        public static IColumn ParseColumn(LambdaExpression expression)
        {
            return ParseColumn(expression.Body);
        }

        /// <summary>
        /// Compiles an <see cref="Expression"/> to an <see cref="IColumn"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The column.</returns>
        public static IColumn ParseColumn(Expression expression)
        {
            if (expression.NodeType == ExpressionType.Convert)
                return ParseColumn(((UnaryExpression)expression).Operand);

            if (expression is MemberExpression memberExp)
                return ParseColumn(memberExp);

            throw new ArgumentException("Invalid expression.");
        }

        /// <summary>
        /// Compiles a <see cref="MemberExpression"/> to an <see cref="IColumn"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The column.</returns>
        public static IColumn ParseColumn(MemberExpression expression)
        {
            if (expression.Expression is MemberExpression memberExp)
            {
                // Nested property
                if (memberExp.Expression is MemberExpression lastExp)
                {
                    List<string> list = new List<string>
                    {
                        expression.Member.Name,
                        memberExp.Member.Name
                    };

                    while (lastExp.Expression is MemberExpression nextExp)
                    {
                        list.Add(lastExp.Member.Name);
                        lastExp = nextExp;
                    }

                    list.Reverse();
                    return SqlBuilder.Instance.Col(lastExp.Type, lastExp.Member.Name, string.Join(".", list));
                }
                else
                {
                    return SqlBuilder.Instance.Col(memberExp.Type, memberExp.Member.Name, expression.Member.Name);
                }
            }
            else
            {
                return SqlBuilder.Instance.Col(expression.Type, expression.Member.Name, "*");
            }
        }

        /// <summary>
        /// Compiles a <see cref="LambdaExpression"/> to a value.
        /// <para>The value can be a literal value or an <see cref="IQueryFragment"/> that represents a value,
        /// like a column, a function or an arithmetic operator.</para>
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The value.</returns>
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
        public static object ParseValue(UnaryExpression expression)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.Convert:
                case ExpressionType.UnaryPlus:
                    return ParseValue(expression.Operand);
                case ExpressionType.Not:
                    if (expression.Type != typeof(bool))
                        return SqlBuilder.Instance.BitNot(ParseValue(expression.Operand));
                    break;
                case ExpressionType.Negate:
                    return SqlBuilder.Instance.Negate(ParseValue(expression.Operand));
            }

            return ParseBoolOperator(expression);
        }

        /// <summary>
        /// Compiles a <see cref="BinaryExpression"/> to a value.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The value.</returns>
        public static object ParseValue(BinaryExpression expression)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.Add:
                    if (expression.Type != typeof(string))
                        return ParseArithmeticOperator(expression);
                    else
                        return ParseFunction(expression);
                case ExpressionType.Subtract:
                case ExpressionType.Multiply:
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
        public static IOperator ParseBoolOperator(LambdaExpression expression)
        {
            return ParseBoolOperator(expression.Body);
        }

        /// <summary>
        /// Compiles an <see cref="Expression"/> to an <see cref="IOperator"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The operator.</returns>
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
        public static IArithOperator ParseArithmeticOperator(BinaryExpression expression)
        {
            IArithOperator arithOperator = null;
            Expression left = expression.Left;

            while (left.NodeType == ExpressionType.Convert)
            {
                left = ((UnaryExpression)left).Operand;
            }

            if (left.NodeType != expression.NodeType)
            {
                switch (expression.NodeType)
                {
                    case ExpressionType.Add:
                        arithOperator = SqlBuilder.Instance.Add;
                        break;
                    case ExpressionType.Subtract:
                        arithOperator = SqlBuilder.Instance.Subtract;
                        break;
                    case ExpressionType.Multiply:
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
        public static IBitOperator ParseBitOperator(BinaryExpression expression)
        {
            IBitOperator bitOperator = null;
            Expression left = expression.Left;

            while (left.NodeType == ExpressionType.Convert)
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

        /// <summary>
        /// Compiles a <see cref="LambdaExpression"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The result of the expression.</returns>
        public static object Compile(LambdaExpression expression)
        {
            return Compile(expression.Body);
        }

        /// <summary>
        /// Compiles an <see cref="Expression"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The result of the expression.</returns>
        public static object Compile(Expression expression)
        {
            switch (expression)
            {
                case ConstantExpression constantExpression:
                    return Compile(constantExpression);
                case MemberExpression memberExpression:
                    return Compile(memberExpression);
                case NewExpression newExpression:
                    return Compile(newExpression);
                case NewArrayExpression newArrayExpression:
                    return Compile(newArrayExpression);
                case ListInitExpression listInitExpression:
                    return Compile(listInitExpression);
                case MethodCallExpression methodCallExpression:
                    return Compile(methodCallExpression);
                case UnaryExpression unaryExpression:
                    return Compile(unaryExpression);
                case BinaryExpression binaryExpression:
                    return Compile(binaryExpression);
                case ConditionalExpression conditionalExpression:
                    return Compile(conditionalExpression);
                default:
                    return CompileDynamicInvoke(expression);
            }
        }

        /// <summary>
        /// Compiles a <see cref="ConstantExpression"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The result of the expression.</returns>
        public static object Compile(ConstantExpression expression)
        {
            return expression.Value;
        }

        /// <summary>
        /// Compiles a <see cref="MemberExpression"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The result of the expression.</returns>
        public static object Compile(MemberExpression expression)
        {
            object value = expression.Expression != null ? Compile(expression.Expression) : null;

            if (expression.Member is FieldInfo fieldInfo)
            {
                return fieldInfo.GetValue(value);
            }
            else
            {
                PropertyInfo propertyInfo = (PropertyInfo)expression.Member;
                return propertyInfo.GetValue(value);
            }
        }

        /// <summary>
        /// Compiles a <see cref="NewExpression"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The result of the expression.</returns>
        public static object Compile(NewExpression expression)
        {
            if (expression.Constructor == null)
                return Activator.CreateInstance(expression.Type);

            object[] args = null;
            if (expression.Arguments.Count > 0)
            {
                args = new object[expression.Arguments.Count];
                for (int i = 0; i < expression.Arguments.Count; i++)
                {
                    args[i] = Compile(expression.Arguments[i]);
                }
            }

            return expression.Constructor.Invoke(args);
        }

        /// <summary>
        /// Compiles a <see cref="NewArrayExpression"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The result of the expression.</returns>
        public static object Compile(NewArrayExpression expression)
        {
            Array value = Array.CreateInstance(expression.Type.GetElementType(), expression.Expressions.Count);

            for (int i = 0; i < expression.Expressions.Count; i++)
            {
                value.SetValue(Compile(expression.Expressions[i]), i);
            }

            return value;
        }

        /// <summary>
        /// Compiles a <see cref="ListInitExpression"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The result of the expression.</returns>
        public static object Compile(ListInitExpression expression)
        {
            object value = Compile(expression.NewExpression);

            foreach (var item in expression.Initializers)
            {
                object[] args = null;
                if (item.Arguments.Count > 0)
                {
                    args = new object[item.Arguments.Count];
                    for (int i = 0; i < item.Arguments.Count; i++)
                    {
                        args[i] = Compile(item.Arguments[i]);
                    }
                }

                item.AddMethod.Invoke(value, args);
            }

            return value;
        }

        /// <summary>
        /// Compiles a <see cref="MethodCallExpression"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The result of the expression.</returns>
        public static object Compile(MethodCallExpression expression)
        {
            object value = expression.Object != null ? Compile(expression.Object) : null;

            object[] args = null;
            if (expression.Arguments.Count > 0)
            {
                args = new object[expression.Arguments.Count];
                for (int i = 0; i < expression.Arguments.Count; i++)
                {
                    args[i] = Compile(expression.Arguments[i]);
                }
            }

            return expression.Method.Invoke(value, args);
        }

        /// <summary>
        /// Compiles a <see cref="UnaryExpression"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The result of the expression.</returns>
        public static object Compile(UnaryExpression expression)
        {
            if (expression.Method != null)
                return expression.Method.Invoke(null, new object[] { Compile(expression.Operand) });

            switch (expression.NodeType)
            {
                case ExpressionType.Convert:
                    if (expression.Type == typeof(object) || expression.Type.IsAssignableFrom(expression.Operand.Type))
                    {
                        return Compile(expression.Operand);
                    }
                    else if (expression.Type.IsPrimitive)
                    {
                        if (typeof(IConvertible).IsAssignableFrom(expression.Operand.Type))
                        {
                            return Convert.ChangeType(Compile(expression.Operand), expression.Type);
                        }
                        else
                        {
                            Type operandType = Nullable.GetUnderlyingType(expression.Operand.Type);
                            if (operandType != null && typeof(IConvertible).IsAssignableFrom(operandType))
                                return Convert.ChangeType(Compile(expression.Operand), expression.Type);
                        }
                    }
                    break;
            }

            return CompileDynamicInvoke(expression);
        }

        /// <summary>
        /// Compiles a <see cref="BinaryExpression"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The result of the expression.</returns>
        public static object Compile(BinaryExpression expression)
        {
            if (expression.Method != null)
                return expression.Method.Invoke(null, new object[] { Compile(expression.Left), Compile(expression.Right) });

            switch (expression.NodeType)
            {
                case ExpressionType.ArrayIndex:
                    Array array = (Array)Compile(expression.Left);
                    int index = (int)Compile(expression.Right);
                    return array.GetValue(index);
                case ExpressionType.Coalesce:
                    return Compile(expression.Left) ?? Compile(expression.Right);
                default:
                    return CompileDynamicInvoke(expression);
            }
        }

        /// <summary>
        /// Compiles a <see cref="ConditionalExpression"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The result of the expression.</returns>
        public static object Compile(ConditionalExpression expression)
        {
            return (bool)Compile(expression.Test) ? Compile(expression.IfTrue) : Compile(expression.IfFalse);
        }

        /// <summary>
        /// Compiles an <see cref="Expression"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The result of the expression.</returns>
        private static object CompileDynamicInvoke(Expression expression)
        {
            return Expression.Lambda(expression).Compile(true).DynamicInvoke();
        }

        /// <summary>
        /// Gets the full method name.
        /// </summary>
        /// <param name="type">The type of the class of the method.</param>
        /// <param name="methodName">The method name.</param>
        /// <returns>The full method name.</returns>
        private static string GetMethodFullName(Type type, string methodName)
        {
            return type.FullName + "." + methodName;
        }

        /// <summary>
        /// Gets the full method name of a <see cref="MethodCallExpression"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The full method name.</returns>
        private static string GetMethodFullName(MethodCallExpression expression)
        {
            return GetMethodFullName(expression.Method.DeclaringType, expression.Method.Name);
        }

        /// <summary>
        /// Gets the specified property.
        /// <para>The property can be a nested property.</para>
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>The property info.</returns>
        public static PropertyInfo GetProperty(Type type, string propertyName)
        {
            PropertyInfo propertyInfo = null;

            foreach (var property in propertyName.Split('.'))
            {
                propertyInfo = type.GetProperty(property);
                if (propertyInfo == null)
                    return null;

                type = propertyInfo.PropertyType;
            }

            return propertyInfo;
        }

        /// <summary>
        /// Gets the property path of a <see cref="LambdaExpression"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The property path.</returns>
        public static string GetPropertyPath(LambdaExpression expression)
        {
            return GetPropertyPath(expression.Body);
        }

        /// <summary>
        /// Gets the property path of an <see cref="Expression"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The property path.</returns>
        public static string GetPropertyPath(Expression expression)
        {
            return string.Join(".", GetProperties(expression).Select(x => x.Name));
        }

        /// <summary>
        /// Gets all the nested members of a <see cref="LambdaExpression"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>A list with the <see cref="MemberInfo"/> of all members.</returns>
        public static IList<MemberInfo> GetProperties(LambdaExpression expression)
        {
            return GetProperties(expression.Body);
        }

        /// <summary>
        /// Gets all the nested members of an <see cref="Expression"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>A list with the <see cref="MemberInfo"/> of all members.</returns>
        public static IList<MemberInfo> GetProperties(Expression expression)
        {
            if (expression.NodeType == ExpressionType.Convert)
                return GetProperties(((UnaryExpression)expression).Operand);

            if (expression is MemberExpression memberExp)
                return GetMemberInfoList(memberExp);

            throw new ArgumentException("Invalid expression.");
        }

        /// <summary>
        /// Gets all the nested members of a <see cref="MemberExpression"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>A list with the <see cref="MemberInfo"/> of all members.</returns>
        public static IList<MemberInfo> GetMemberInfoList(MemberExpression expression)
        {
            List<MemberInfo> list = new List<MemberInfo>();

            do
            {
                list.Add(expression.Member);
                expression = expression.Expression as MemberExpression;
            } while (expression != null);

            list.Reverse();
            return list;
        }

        /// <summary>
        /// Registers a function to compile it into an <see cref="IQueryFragment"/>.
        /// </summary>
        /// <param name="type">The type of the class of the method.</param>
        /// <param name="methodName">The method name.</param>
        public static void AddFunction(Type type, string methodName)
        {
            AddFunction(type, methodName, methodName);
        }

        /// <summary>
        /// Registers a function to compile it into an <see cref="IQueryFragment"/>.
        /// </summary>
        /// <param name="type">The type of the class of the method.</param>
        /// <param name="methodName">The method name.</param>
        /// <param name="nameSql">The method name in SQL</param>
        public static void AddFunction(Type type, string methodName, string nameSql)
        {
            Functions[GetMethodFullName(type, methodName)] =
                x => ExpressionHelper.Function(x, nameSql.ToUpperInvariant());
        }

        /// <summary>
        /// Registers a function to compile it into an <see cref="IQueryFragment"/>.
        /// </summary>
        /// <param name="type">The type of the class of the method.</param>
        /// <param name="methodName">The method name.</param>
        /// <param name="func">A custom delegate to compile the expression.</param>
        public static void AddFunction(Type type, string methodName, Func<MethodCallExpression, object> func)
        {
            Functions[GetMethodFullName(type, methodName)] = func;
        }

        /// <summary>
        /// Registers a function to compile it into an <see cref="IQueryFragment"/>.
        /// </summary>
        /// <param name="type">The type of the class of the method.</param>
        /// <param name="methodName">The method name.</param>
        /// <param name="func">A custom delegate to compile the expression.</param>
        public static void AddFunction(Type type, string methodName,
            Func<MethodCallExpression, string, IQueryFragment> func)
        {
            Functions[GetMethodFullName(type, methodName)] = x => func(x, methodName.ToUpperInvariant());
        }

        /// <summary>
        /// Registers a function to compile it into an <see cref="IQueryFragment"/>.
        /// </summary>
        /// <param name="type">The type of the class of the method.</param>
        /// <param name="methodName">The method name.</param>
        /// <param name="nameSql">The method name in SQL</param>
        /// <param name="func">A custom delegate to compile the expression.</param>
        public static void AddFunction(Type type, string methodName, string nameSql,
            Func<MethodCallExpression, string, IQueryFragment> func)
        {
            Functions[GetMethodFullName(type, methodName)] = x => func(x, nameSql);
        }

        /// <summary>
        /// Removes a registered function.
        /// </summary>
        /// <param name="type">The type of the class of the method.</param>
        /// <param name="methodName">The method name.</param>
        public static void RemoveFunction(Type type, string methodName)
        {
            Functions.Remove(GetMethodFullName(type, methodName));
        }

        /// <summary>
        /// Determines if the function is registered.
        /// </summary>
        /// <param name="type">The type of the class of the method.</param>
        /// <param name="methodName">The method name.</param>
        /// <returns><see langword="true"/> if the function is registered, otherwise, <see langword="false"/>.</returns>
        public static bool ContainsFunction(Type type, string methodName)
        {
            return Functions.ContainsKey(GetMethodFullName(type, methodName));
        }

        /// <summary>
        /// Removes all registered functions.
        /// </summary>
        public static void ClearFunctions()
        {
            Functions.Clear();
        }

        /// <summary>
        /// Registers a type as a table.
        /// </summary>
        /// <param name="type">The type.</param>
        public static void AddTable(Type type)
        {
            lock (Tables)
            {
                Tables.Add(type.FullName);
            }
        }

        /// <summary>
        /// Removes a registered type.
        /// </summary>
        /// <param name="type">The type.</param>
        public static void RemoveTable(Type type)
        {
            lock (Tables)
            {
                Tables.Remove(type.FullName);
            }
        }

        /// <summary>
        /// Determines if the type is registered.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><see langword="true"/> if the type is registered, otherwise, <see langword="false"/>.</returns>
        public static bool ContainsTable(Type type)
        {
            return Tables.Contains(type.FullName);
        }

        /// <summary>
        /// Removes all registered types.
        /// </summary>
        public static void ClearTables()
        {
            lock (Tables)
            {
                Tables.Clear();
            }
        }
    }
}