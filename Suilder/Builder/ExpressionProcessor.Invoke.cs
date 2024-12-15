using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Suilder.Builder
{
    public static partial class ExpressionProcessor
    {
        /// <summary>
        /// Compiles an <see cref="Expression"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The result of the expression.</returns>
        /// <exception cref="TargetInvocationException">The invoked expression throws an exception.</exception>
        private static object CompileDynamicInvoke(Expression expression)
        {
            return Expression.Lambda(expression).Compile(true).DynamicInvoke();
        }

        /// <summary>
        /// Gets the value of a <see cref="FieldInfo"/>.
        /// </summary>
        /// <param name="fieldInfo">The field info.</param>
        /// <param name="obj">The object whose field value will be returned.</param>
        /// <returns>An object containing the value of the field.</returns>
        /// <exception cref="TargetInvocationException">The field is not static and <paramref name="obj"/> is null.
        /// </exception>
        private static object CompileGetValue(FieldInfo fieldInfo, object obj)
        {
            try
            {
                if (!fieldInfo.IsStatic)
                    obj.GetType();

                return fieldInfo.GetValue(obj);
            }
            catch (Exception ex)
            {
                throw new TargetInvocationException(ex);
            }
        }

        /// <summary>
        /// Gets the value of a <see cref="PropertyInfo"/>.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        /// <param name="obj">The object whose property value will be returned.</param>
        /// <returns>An object containing the value of the property.</returns>
        /// <exception cref="TargetInvocationException">The property is not static and <paramref name="obj"/> is null,
        /// or the invoked property throws an exception.</exception>
        private static object CompileGetValue(PropertyInfo propertyInfo, object obj)
        {
            try
            {
                MethodInfo methodInfo = propertyInfo.GetGetMethod(true);

                if (!methodInfo.IsStatic)
                    obj.GetType();

                return methodInfo.Invoke(obj, null);
            }
            catch (Exception ex) when (!(ex is TargetInvocationException))
            {
                throw new TargetInvocationException(ex);
            }
        }

        /// <summary>
        /// Invokes a <see cref="ConstructorInfo"/>.
        /// </summary>
        /// <param name="constructorInfo">The constructor info.</param>
        /// <param name="parameters">An argument list for the invoked constructor.</param>
        /// <returns>An instance of the class associated with the constructor.</returns>
        /// <exception cref="TargetInvocationException">The invoked constructor throws an exception.</exception>
        private static object CompileInvoke(ConstructorInfo constructorInfo, object[] parameters)
        {
            try
            {
                return constructorInfo.Invoke(parameters);
            }
            catch (Exception ex) when (!(ex is TargetInvocationException))
            {
                throw new TargetInvocationException(ex);
            }
        }

        /// <summary>
        /// Invokes a <see cref="MethodInfo"/>.
        /// </summary>
        /// <param name="methodInfo">The method info.</param>
        /// <param name="obj">The object on which to invoke the method.</param>
        /// <param name="parameters">An argument list for the invoked method.</param>
        /// <returns>An object containing the return value of the invoked method.</returns>
        /// <exception cref="TargetInvocationException">The method is not static and <paramref name="obj"/> is null,
        /// or the invoked method throws an exception.</exception>
        private static object CompileInvoke(MethodInfo methodInfo, object obj, object[] parameters)
        {
            try
            {
                if (!methodInfo.IsStatic)
                    obj.GetType();

                return methodInfo.Invoke(obj, parameters);
            }
            catch (Exception ex) when (!(ex is TargetInvocationException))
            {
                throw new TargetInvocationException(ex);
            }
        }

        /// <summary>
        /// Converts a value to the specified type.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="conversionType">The type of object to return.</param>
        /// <param name="isChecked">If the conversion is checked.</param>
        /// <returns>An object containing the converted value.</returns>
        /// <exception cref="TargetInvocationException">The conversion throws an exception.</exception>
        private static object CompileConvert(object value, Type conversionType, bool isChecked)
        {
            try
            {
                if (value == null)
                    return (int)default(int?);

                switch (value)
                {
                    case char castValue:
                        return CompileConvert((ulong)castValue, conversionType, isChecked);
                    case sbyte castValue:
                        return CompileConvert(castValue, conversionType, isChecked);
                    case byte castValue:
                        return CompileConvert((ulong)castValue, conversionType, isChecked);
                    case short castValue:
                        return CompileConvert(castValue, conversionType, isChecked);
                    case ushort castValue:
                        return CompileConvert((ulong)castValue, conversionType, isChecked);
                    case int castValue:
                        return CompileConvert(castValue, conversionType, isChecked);
                    case uint castValue:
                        return CompileConvert((ulong)castValue, conversionType, isChecked);
                    case long castValue:
                        return CompileConvert(castValue, conversionType, isChecked);
                    case ulong castValue:
                        return CompileConvert(castValue, conversionType, isChecked);
                    case float castValue:
                        return CompileConvert(castValue, conversionType, isChecked);
                    default:
                        return CompileConvert((double)value, conversionType, isChecked);
                }
            }
            catch (Exception ex)
            {
                throw new TargetInvocationException(ex);
            }
        }

        /// <summary>
        /// Converts a value to the specified type.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="conversionType">The type of object to return.</param>
        /// <param name="isChecked">If the conversion is checked.</param>
        /// <returns>An object containing the converted value.</returns>
        private static object CompileConvert(long value, Type conversionType, bool isChecked)
        {
            if (conversionType == typeof(char))
                return isChecked ? checked((char)value) : (char)value;
            else if (conversionType == typeof(sbyte))
                return isChecked ? checked((sbyte)value) : (sbyte)value;
            else if (conversionType == typeof(byte))
                return isChecked ? checked((byte)value) : (byte)value;
            else if (conversionType == typeof(short))
                return isChecked ? checked((short)value) : (short)value;
            else if (conversionType == typeof(ushort))
                return isChecked ? checked((ushort)value) : (ushort)value;
            else if (conversionType == typeof(int))
                return isChecked ? checked((int)value) : (int)value;
            else if (conversionType == typeof(uint))
                return isChecked ? checked((uint)value) : (uint)value;
            else if (conversionType == typeof(long))
                return value;
            else if (conversionType == typeof(ulong))
                return isChecked ? checked((ulong)value) : (ulong)value;
            else if (conversionType == typeof(float))
                return (float)value;
            else
                return (double)value;
        }

        /// <summary>
        /// Converts a value to the specified type.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="conversionType">The type of object to return.</param>
        /// <param name="isChecked">If the conversion is checked.</param>
        /// <returns>An object containing the converted value.</returns>
        private static object CompileConvert(ulong value, Type conversionType, bool isChecked)
        {
            if (conversionType == typeof(char))
                return isChecked ? checked((char)value) : (char)value;
            else if (conversionType == typeof(sbyte))
                return isChecked ? checked((sbyte)value) : (sbyte)value;
            else if (conversionType == typeof(byte))
                return isChecked ? checked((byte)value) : (byte)value;
            else if (conversionType == typeof(short))
                return isChecked ? checked((short)value) : (short)value;
            else if (conversionType == typeof(ushort))
                return isChecked ? checked((ushort)value) : (ushort)value;
            else if (conversionType == typeof(int))
                return isChecked ? checked((int)value) : (int)value;
            else if (conversionType == typeof(uint))
                return isChecked ? checked((uint)value) : (uint)value;
            else if (conversionType == typeof(long))
                return isChecked ? checked((long)value) : (long)value;
            else if (conversionType == typeof(ulong))
                return value;
            else if (conversionType == typeof(float))
                return (float)value;
            else
                return (double)value;
        }

        /// <summary>
        /// Converts a value to the specified type.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="conversionType">The type of object to return.</param>
        /// <param name="isChecked">If the conversion is checked.</param>
        /// <returns>An object containing the converted value.</returns>
        private static object CompileConvert(double value, Type conversionType, bool isChecked)
        {
            if (conversionType == typeof(char))
                return isChecked ? checked((char)value) : (char)value;
            else if (conversionType == typeof(sbyte))
                return isChecked ? checked((sbyte)value) : (sbyte)value;
            else if (conversionType == typeof(byte))
                return isChecked ? checked((byte)value) : (byte)value;
            else if (conversionType == typeof(short))
                return isChecked ? checked((short)value) : (short)value;
            else if (conversionType == typeof(ushort))
                return isChecked ? checked((ushort)value) : (ushort)value;
            else if (conversionType == typeof(int))
                return isChecked ? checked((int)value) : (int)value;
            else if (conversionType == typeof(uint))
                return isChecked ? checked((uint)value) : (uint)value;
            else if (conversionType == typeof(long))
                return isChecked ? checked((long)value) : (long)value;
            else if (conversionType == typeof(ulong))
                return isChecked ? checked((ulong)value) : (ulong)value;
            else if (conversionType == typeof(float))
                return (float)value;
            else
                return value;
        }

        /// <summary>
        /// Creates an instance of the specified type.
        /// </summary>
        /// <param name="type">The type of object to create.</param>
        /// <returns>An instance of the specified type.</returns>
        private static object CompileCreateInstance(Type type)
        {
            return Activator.CreateInstance(type);
        }

        /// <summary>
        /// Creates an array of the specified type and length.
        /// </summary>
        /// <param name="elementType">The type of the array to create.</param>
        /// <param name="length">The size of the array to create.</param>
        /// <returns>An array of the specified type and length.</returns>
        private static Array CompileArrayCreateInstance(Type elementType, int length)
        {
            return Array.CreateInstance(elementType, length);
        }
    }
}