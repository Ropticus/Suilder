using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Suilder.Builder
{
    public static partial class ExpressionProcessor
    {
        /// <summary>
        /// Gets the specified property.
        /// <para>The property can be a nested property.</para>
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>The property info, if found; otherwise, <see langword="null"/>.</returns>
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
        /// <exception cref="ArgumentException">The expression is invalid.</exception>
        public static string GetPropertyPath(LambdaExpression expression)
        {
            return GetPropertyPath(expression.Body);
        }

        /// <summary>
        /// Gets the property path of an <see cref="Expression"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The property path.</returns>
        /// <exception cref="ArgumentException">The expression is invalid.</exception>
        public static string GetPropertyPath(Expression expression)
        {
            return string.Join(".", GetProperties(expression).Select(x => x.Name));
        }

        /// <summary>
        /// Gets all the nested members of a <see cref="LambdaExpression"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>A list with the <see cref="MemberInfo"/> of all members.</returns>
        /// <exception cref="ArgumentException">The expression is invalid.</exception>
        public static IList<MemberInfo> GetProperties(LambdaExpression expression)
        {
            return GetProperties(expression.Body);
        }

        /// <summary>
        /// Gets all the nested members of an <see cref="Expression"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>A list with the <see cref="MemberInfo"/> of all members.</returns>
        /// <exception cref="ArgumentException">The expression is invalid.</exception>
        public static IList<MemberInfo> GetProperties(Expression expression)
        {
            switch (expression)
            {
                case UnaryExpression unaryExpression:
                    if (unaryExpression.NodeType == ExpressionType.Convert
                        || unaryExpression.NodeType == ExpressionType.ConvertChecked)
                    {
                        return GetProperties(unaryExpression.Operand);
                    }
                    break;
                case MemberExpression memberExpression:
                    return GetMemberInfoList(memberExpression);
            }

            throw new ArgumentException("Invalid expression.");
        }

        /// <summary>
        /// Gets all the nested members of a <see cref="MemberExpression"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>A list with the <see cref="MemberInfo"/> of all members.</returns>
        /// <exception cref="ArgumentException">The expression is invalid.</exception>
        public static IList<MemberInfo> GetMemberInfoList(MemberExpression expression)
        {
            List<MemberInfo> list = new List<MemberInfo> { expression.Member };

            while (true)
            {
                switch (expression.Expression)
                {
                    case MemberExpression memberExpression:
                        list.Add(memberExpression.Member);
                        expression = memberExpression;
                        break;
                    case ParameterExpression _:
                        list.Reverse();
                        return list;
                    default:
                        throw new ArgumentException("Invalid expression.");
                }
            }
        }
    }
}