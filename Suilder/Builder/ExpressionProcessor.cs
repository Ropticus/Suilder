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
        /// Determines if the expression is an alias.
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
        /// Compile an expression to an <see cref="IAlias"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <typeparam name="T">The type of the alias.</typeparam>
        /// <returns>The alias.</returns>
        public static IAlias<T> ParseAlias<T>(Expression<Func<T>> expression)
        {
            return ParseAlias<T>(expression.Body);
        }

        /// <summary>
        /// Compile an expression to an <see cref="IAlias"/>.
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
        /// Compile an expression to an <see cref="IAlias"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The alias.</returns>
        public static IAlias ParseAlias(Expression<Func<object>> expression)
        {
            return ParseAlias(expression.Body);
        }

        /// <summary>
        /// Compile an expression to an <see cref="IAlias"/>.
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
        /// Compile an expression to an <see cref="IColumn"/>.
        /// </summary>
        /// <param name="tableName">The table name or his alias.</param>
        /// <param name="expression">The expression.</param>
        /// <typeparam name="T">The type of the alias.</typeparam>
        /// <returns>The column.</returns>
        public static IColumn ParseColumn<T>(string tableName, Expression<Func<T, object>> expression)
        {
            return ParseColumn<T>(tableName, expression.Body);
        }

        /// <summary>
        /// Compile an expression to an <see cref="IColumn"/>.
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
        /// Compile an expression to an <see cref="IColumn"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The column.</returns>
        public static IColumn ParseColumn(Expression<Func<object>> expression)
        {
            return ParseColumn(expression.Body);
        }

        /// <summary>
        /// Compile an expression to an <see cref="IColumn"/>.
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
        /// Compile an expression to an <see cref="IColumn"/>.
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
        /// Compile an expression to a literal value or an <see cref="IQueryFragment"/> that represent a value
        /// like a column, a function or an arithmetic operator.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The value.</returns>
        public static object ParseValue(Expression<Func<object>> expression)
        {
            return ParseValue(expression.Body);
        }

        /// <summary>
        /// Compile an expression to a literal value or an <see cref="IQueryFragment"/> that represent a value
        /// like a column, a function or an arithmetic operator.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The value.</returns>
        public static object ParseValue(Expression expression)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.Convert:
                    return ParseValue(((UnaryExpression)expression).Operand);
                case ExpressionType.Constant:
                    return ((ConstantExpression)expression).Value;
                case ExpressionType.MemberAccess:
                    MemberExpression memberExp = (MemberExpression)expression;
                    return IsAlias(memberExp) ? ParseColumn(memberExp) : Compile(memberExp);
                case ExpressionType.New:
                    return Compile((NewExpression)expression);
                case ExpressionType.NewArrayInit:
                    return Compile((NewArrayExpression)expression);
                case ExpressionType.ArrayIndex:
                    return Compile((BinaryExpression)expression);
                case ExpressionType.Call:
                    return ParseMethod((MethodCallExpression)expression);
                case ExpressionType.Add:
                case ExpressionType.Subtract:
                case ExpressionType.Multiply:
                case ExpressionType.Divide:
                case ExpressionType.Modulo:
                    return ParseArithmeticOperator((BinaryExpression)expression);
                case ExpressionType.And:
                case ExpressionType.Or:
                case ExpressionType.ExclusiveOr:
                    return ParseBitOperator((BinaryExpression)expression);
                case ExpressionType.Coalesce:
                    return ParseFunctionOperator((BinaryExpression)expression);
                default:
                    throw new ArgumentException("Invalid expression.");
            }
        }

        /// <summary>
        /// Compile a method expression to a value.
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
        /// Compile an expression to an <see cref="IOperator"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The operator.</returns>
        public static IOperator ParseBoolOperator(Expression<Func<bool>> expression)
        {
            return ParseBoolOperator(expression.Body);
        }

        /// <summary>
        /// Compile an expression to an <see cref="IOperator"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The operator.</returns>
        public static IOperator ParseBoolOperator(Expression expression)
        {
            if (expression is BinaryExpression binaryExp)
                return ParseBoolOperator(binaryExp);

            switch (expression.NodeType)
            {
                case ExpressionType.Convert:
                    return ParseBoolOperator(((UnaryExpression)expression).Operand);
                case ExpressionType.MemberAccess:
                    if (expression.Type == typeof(bool))
                        return SqlBuilder.Instance.Eq(ParseValue((MemberExpression)expression), true);
                    break;
                case ExpressionType.Not:
                    {
                        UnaryExpression unaryExp = (UnaryExpression)expression;
                        if (unaryExp.Operand is MemberExpression memberExp && memberExp.Type == typeof(bool))
                            return SqlBuilder.Instance.Eq(ParseValue(memberExp), false);
                        else
                            return SqlBuilder.Instance.Not(ParseBoolOperator(unaryExp.Operand));
                    }
                case ExpressionType.Call:
                    {
                        object value = ParseMethod((MethodCallExpression)expression);
                        if (value is IOperator op)
                            return op;
                        else if (value is IFunction function)
                            return SqlBuilder.Instance.Eq(function, true);
                        break;
                    }
            }

            throw new ArgumentException("Invalid expression.");
        }

        /// <summary>
        /// Compile a binary expression to an <see cref="IOperator"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The operator.</returns>
        public static IOperator ParseBoolOperator(BinaryExpression expression)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    return ParseLogicalOperator(expression);
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
                default:
                    throw new ArgumentException("Invalid expression.");
            }
        }

        /// <summary>
        /// Compile a binary expression to an <see cref="ILogicalOperator"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The logical operator.</returns>
        public static ILogicalOperator ParseLogicalOperator(BinaryExpression expression)
        {
            Func<ILogicalOperator> opDel = null;

            switch (expression.NodeType)
            {
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                    if (expression.Left.NodeType != ExpressionType.And && expression.Left.NodeType != ExpressionType.AndAlso)
                        opDel = () => SqlBuilder.Instance.And;
                    break;
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    if (expression.Left.NodeType != ExpressionType.Or && expression.Left.NodeType != ExpressionType.OrElse)
                        opDel = () => SqlBuilder.Instance.Or;
                    break;
                default:
                    throw new ArgumentException("Invalid expression.");
            }

            ILogicalOperator logicalOperator;
            if (opDel != null)
            {
                logicalOperator = opDel();
                logicalOperator.Add(ParseBoolOperator(expression.Left));
            }
            else
            {
                logicalOperator = (ILogicalOperator)ParseBoolOperator(expression.Left);
            }

            return logicalOperator.Add(ParseBoolOperator(expression.Right));
        }

        /// <summary>
        /// Compile a binary expression to an <see cref="IArithOperator"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The arithmetic operator.</returns>
        public static IArithOperator ParseArithmeticOperator(BinaryExpression expression)
        {
            Func<IArithOperator> opDel = null;

            switch (expression.NodeType)
            {
                case ExpressionType.Add:
                    if (expression.Left.NodeType != expression.NodeType)
                        opDel = () => SqlBuilder.Instance.Add;
                    break;
                case ExpressionType.Subtract:
                    if (expression.Left.NodeType != expression.NodeType)
                        opDel = () => SqlBuilder.Instance.Subtract;
                    break;
                case ExpressionType.Multiply:
                    if (expression.Left.NodeType != expression.NodeType)
                        opDel = () => SqlBuilder.Instance.Multiply;
                    break;
                case ExpressionType.Divide:
                    if (expression.Left.NodeType != expression.NodeType)
                        opDel = () => SqlBuilder.Instance.Divide;
                    break;
                case ExpressionType.Modulo:
                    if (expression.Left.NodeType != expression.NodeType)
                        opDel = () => SqlBuilder.Instance.Modulo;
                    break;
                default:
                    throw new ArgumentException("Invalid expression.");
            }

            IArithOperator arithOperator;
            if (opDel != null)
            {
                arithOperator = opDel();
                arithOperator.Add(ParseValue(expression.Left));
            }
            else
            {
                arithOperator = (IArithOperator)ParseValue(expression.Left);
            }

            return arithOperator.Add(ParseValue(expression.Right));
        }

        /// <summary>
        /// Compile a binary expression to an <see cref="IBitOperator"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The bitwise operator.</returns>
        public static IBitOperator ParseBitOperator(BinaryExpression expression)
        {
            Func<IBitOperator> opDel = null;

            switch (expression.NodeType)
            {
                case ExpressionType.And:
                    if (expression.Left.NodeType != expression.NodeType)
                        opDel = () => SqlBuilder.Instance.BitAnd;
                    break;
                case ExpressionType.Or:
                    if (expression.Left.NodeType != expression.NodeType)
                        opDel = () => SqlBuilder.Instance.BitOr;
                    break;
                case ExpressionType.ExclusiveOr:
                    if (expression.Left.NodeType != expression.NodeType)
                        opDel = () => SqlBuilder.Instance.BitXor;
                    break;
                default:
                    throw new ArgumentException("Invalid expression.");
            }

            IBitOperator bitOperator;
            if (opDel != null)
            {
                bitOperator = opDel();
                bitOperator.Add(ParseValue(expression.Left));
            }
            else
            {
                bitOperator = (IBitOperator)ParseValue(expression.Left);
            }

            return bitOperator.Add(ParseValue(expression.Right));
        }

        /// <summary>
        /// Compile a binary expression to an <see cref="IFunction"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The function operator.</returns>
        public static IFunction ParseFunctionOperator(BinaryExpression expression)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.Coalesce:
                    return SqlFn.Coalesce(ParseValue(expression.Left), ParseValue(expression.Right));
                default:
                    throw new ArgumentException("Invalid expression.");
            }
        }

        /// <summary>
        /// Compile an expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The result of the expression.</returns>
        public static object Compile(Expression expression)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.Convert:
                    return Compile((UnaryExpression)expression);
                case ExpressionType.Constant:
                    return ((ConstantExpression)expression).Value;
                case ExpressionType.MemberAccess:
                    return Compile((MemberExpression)expression);
                case ExpressionType.New:
                    return Compile((NewExpression)expression);
                case ExpressionType.NewArrayInit:
                    return Compile((NewArrayExpression)expression);
                case ExpressionType.Call:
                    return Compile((MethodCallExpression)expression);
            }

            if (expression is BinaryExpression binaryExpression)
                return Compile(binaryExpression);

            return Expression.Lambda(expression).Compile().DynamicInvoke();
        }

        /// <summary>
        /// Compile an expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The result of the expression.</returns>
        public static object Compile(UnaryExpression expression)
        {
            object value = Compile(expression.Operand);

            if (expression.Method != null)
                return expression.Method.Invoke(null, new object[] { value });

            if (expression.Type != typeof(object))
                return Convert.ChangeType(value, expression.Type);

            return value;
        }

        /// <summary>
        /// Compile an expression.
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
        /// Compile an expression.
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
        /// Compile an expression.
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
        /// Compile an expression.
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
            }

            return Expression.Lambda(expression).Compile().DynamicInvoke();
        }

        /// <summary>
        /// Compile an expression.
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
        /// Get the full method name.
        /// </summary>
        /// <param name="type">The type of the class of the method.</param>
        /// <param name="methodName">The method name.</param>
        /// <returns>The full method name.</returns>
        private static string GetMethodFullName(Type type, string methodName)
        {
            return type.FullName + "." + methodName;
        }

        /// <summary>
        /// Get the full method name.
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
        /// Gets the property path of an expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <typeparam name="T">The type.</typeparam>
        /// <returns>The property path.</returns>
        public static string GetPropertyPath<T>(Expression<Func<T, object>> expression)
        {
            return GetPropertyPath(expression.Body);
        }

        /// <summary>
        /// Gets the property path of an expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <typeparam name="T1">The type.</typeparam>
        /// <typeparam name="T2">The type of the property.</typeparam>
        /// <returns>The property path.</returns>
        public static string GetPropertyPath<T1, T2>(Expression<Func<T1, T2>> expression)
        {
            return GetPropertyPath(expression.Body);
        }

        /// <summary>
        /// Gets the property path of an expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The property path.</returns>
        public static string GetPropertyPath(Expression expression)
        {
            return string.Join(".", GetProperties(expression).Select(x => x.Name));
        }

        /// <summary>
        /// Get all the nested members of an expression.
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
        /// Get all the nested members of an expression.
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
        /// <param name="isParams">If the method contains a params argument.</param>
        public static void AddFunction(Type type, string methodName, bool isParams = false)
        {
            AddFunction(type, methodName, methodName, isParams);
        }

        /// <summary>
        /// Registers a function to compile it into an <see cref="IQueryFragment"/>.
        /// </summary>
        /// <param name="type">The type of the class of the method.</param>
        /// <param name="methodName">The method name.</param>
        /// <param name="nameSql">The method name in SQL</param>
        /// <param name="isParams">If the method contains a params argument.</param>
        public static void AddFunction(Type type, string methodName, string nameSql, bool isParams = false)
        {
            if (!isParams)
            {
                Functions[GetMethodFullName(type, methodName)] =
                    x => ExpressionHelper.Function(x, nameSql.ToUpperInvariant());
            }
            else
            {
                Functions[GetMethodFullName(type, methodName)] =
                    x => ExpressionHelper.FunctionParams(x, nameSql.ToUpperInvariant());
            }
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
        /// Register a type as a table.
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