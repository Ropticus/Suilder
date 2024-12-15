using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Suilder.Builder
{
    public static partial class ExpressionProcessor
    {
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
                return CompileGetValue(fieldInfo, value);
            }
            else
            {
                PropertyInfo propertyInfo = (PropertyInfo)expression.Member;
                return CompileGetValue(propertyInfo, value);
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
                return CompileCreateInstance(expression.Type);

            object[] args = null;
            if (expression.Arguments.Count > 0)
            {
                args = new object[expression.Arguments.Count];
                for (int i = 0; i < expression.Arguments.Count; i++)
                {
                    args[i] = Compile(expression.Arguments[i]);
                }
            }

            return CompileInvoke(expression.Constructor, args);
        }

        /// <summary>
        /// Compiles a <see cref="NewArrayExpression"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The result of the expression.</returns>
        public static object Compile(NewArrayExpression expression)
        {
            Array value = CompileArrayCreateInstance(expression.Type.GetElementType(), expression.Expressions.Count);

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

                CompileInvoke(item.AddMethod, value, args);
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

            return CompileInvoke(expression.Method, value, args);
        }

        /// <summary>
        /// Compiles a <see cref="UnaryExpression"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The result of the expression.</returns>
        public static object Compile(UnaryExpression expression)
        {
            if (expression.Method != null)
                return CompileInvoke(expression.Method, null, new object[] { Compile(expression.Operand) });

            switch (expression.NodeType)
            {
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                    if (expression.Type == typeof(object) || expression.Type.IsAssignableFrom(expression.Operand.Type))
                    {
                        return Compile(expression.Operand);
                    }
                    else if (expression.Type.IsPrimitive)
                    {
                        if (expression.Operand.Type.IsPrimitive)
                        {
                            return CompileConvert(Compile(expression.Operand), expression.Type,
                                expression.NodeType == ExpressionType.ConvertChecked);
                        }
                        else
                        {
                            Type operandType = Nullable.GetUnderlyingType(expression.Operand.Type);
                            if (operandType != null && operandType.IsPrimitive)
                            {
                                return CompileConvert(Compile(expression.Operand), expression.Type,
                                    expression.NodeType == ExpressionType.ConvertChecked);
                            }
                        }
                    }
                    break;
                case ExpressionType.ArrayLength:
                    Array array = (Array)Compile(expression.Operand);
                    return array.Length;
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
            {
                return CompileInvoke(expression.Method, null,
                    new object[] { Compile(expression.Left), Compile(expression.Right) });
            }

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
    }
}