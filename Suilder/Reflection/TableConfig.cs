using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using static Suilder.Reflection.ConfigData;

namespace Suilder.Reflection
{
    /// <summary>
    /// Class to set the configuration of a type.
    /// </summary>
    public class TableConfig
    {
        /// <summary>
        /// The configuration data.
        /// </summary>
        /// <returns>The configuration data.</returns>
        protected ConfigData Data { get; set; } = new ConfigData();

        /// <summary>
        /// Gets the config of the type.
        /// </summary>
        /// <returns>The config of the type.</returns>
        protected internal ConfigData GetConfig()
        {
            return Data;
        }
    }

    /// <summary>
    /// Class to set the configuration of a type.
    /// </summary>
    /// <typeparam name="T">The type to configure.</typeparam>
    public class TableConfig<T> : TableConfig
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TableConfig"/> class.
        /// </summary>
        public TableConfig()
        {
            Data.Type = typeof(T);

            Type parentType = Data.Type;
            while (parentType.BaseType != null)
            {
                parentType = parentType.BaseType;
                Data.InheritLevel++;
            }

            Data.Properties = Data.Type.GetProperties(BindingFlags.Public | BindingFlags.Instance
                | BindingFlags.DeclaredOnly).Where(x => x.CanRead && x.CanWrite).ToArray();
        }

        /// <summary>
        /// If the type is a table.
        /// <para>If false the type is only used for inherit the configuration.</para>
        /// <para>By default is false for abstract classes.</para>
        /// </summary>
        /// <param name="isTable">If the type is a table.</param>
        /// <returns>The configuration.</returns>
        public TableConfig<T> IsTable(bool isTable)
        {
            Data.IsTable = isTable;
            return this;
        }

        /// <summary>
        /// If the type must inherit the table name and the columns of the base type.
        /// </summary>
        /// <param name="inherit">If the type must inherit the table name and the columns of the base type.</param>
        /// <returns>The configuration.</returns>
        public TableConfig<T> InheritTable(bool inherit)
        {
            Data.InheritTable = inherit;
            return this;
        }

        /// <summary>
        /// If the type must inherit the columns of the base type.
        /// <para>By default is true for types whose base type is abstract.</para>
        /// </summary>
        /// <param name="inherit">If the type must inherit the columns of the base type.</param>
        /// <returns>The configuration.</returns>
        public TableConfig<T> InheritColumns(bool inherit)
        {
            Data.InheritColumns = inherit;
            return this;
        }

        /// <summary>
        /// Sets the name of the table.
        /// </summary>
        /// <param name="tableName">The table name.</param>
        /// <returns>The configuration.</returns>
        public TableConfig<T> WithName(string tableName)
        {
            Data.TableName = tableName;
            return this;
        }

        /// <summary>
        /// Sets the primary key of the table.
        /// <para>Call multiple times for composite keys.</para>
        /// </summary>
        /// <param name="expression">The property.</param>
        /// <returns>The configuration.</returns>
        public TableConfig<T> PrimaryKey(Expression<Func<T, object>> expression)
        {
            Data.PrimaryKeys.Add(GetProperty(expression.Body));
            return this;
        }

        /// <summary>
        /// Ignores a property.
        /// </summary>
        /// <param name="expression">The property.</param>
        /// <returns>The configuration.</returns>
        public TableConfig<T> Ignore(Expression<Func<T, object>> expression)
        {
            Data.Ignore.Add(GetProperty(expression.Body));
            return this;
        }

        /// <summary>
        /// Sets the column name for a property.
        /// </summary>
        /// <param name="expression">The property.</param>
        /// <param name="columnName">The column name.</param>
        /// <returns>The configuration.</returns>
        public TableConfig<T> ColumnName(Expression<Func<T, object>> expression, string columnName)
        {
            Data.ColumnsNames.Add(GetProperty(expression.Body), columnName);
            return this;
        }

        /// <summary>
        /// Sets a foreign key.
        /// <para>Call multiple times for composite keys.</para>
        /// </summary>
        /// <param name="expression">The property.</param>
        /// <returns>The configuration.</returns>
        public TableConfig<T> ForeignKey(Expression<Func<T, object>> expression)
        {
            return ForeignKey(expression, null);
        }

        /// <summary>
        /// Sets the column name of a foreign key.
        /// <para>Call multiple times for composite keys.</para>
        /// </summary>
        /// <param name="expression">The property.</param>
        /// <param name="columnName">The column name.</param>
        /// <returns>The configuration.</returns>
        public TableConfig<T> ForeignKey(Expression<Func<T, object>> expression, string columnName)
        {
            IList<MemberInfo> properties = GetProperties(expression.Body);

            List<ColumnData> keys = null;
            if (!Data.ForeignKeys.ContainsKey(properties[0].Name))
            {
                keys = new List<ColumnData>();
                Data.ForeignKeys.Add(properties[0].Name, keys);
            }
            else
            {
                keys = Data.ForeignKeys[properties[0].Name];
            }

            keys.Add(new ColumnData()
            {
                PropertyName = string.Join(".", properties.Select(x => x.Name)),
                ColumnName = !string.IsNullOrEmpty(columnName) ? columnName : string.Join("", properties.Select(x => x.Name))
            });

            return this;
        }

        /// <summary>
        /// Gets the full path of a property.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The full path of the property.</returns>
        protected static string GetProperty(Expression expression)
        {
            return string.Join(".", GetProperties(expression).Select(x => x.Name));
        }

        /// <summary>
        /// Get all the nested members of an expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>A list with the <see cref="MemberInfo"/> of all members.</returns>
        protected static IList<MemberInfo> GetProperties(Expression expression)
        {
            if (expression.NodeType == ExpressionType.Convert)
            {
                return GetProperties(((UnaryExpression)expression).Operand);
            }

            MemberExpression memberExp = expression as MemberExpression;
            if (memberExp == null)
            {
                throw new ArgumentException("Invalid expression.");
            }
            else
            {
                return GetMemberInfoList(memberExp);
            }
        }

        /// <summary>
        /// Get all the nested members of an expression.
        /// </summary>
        /// <param name="memberExp">The expression.</param>
        /// <returns>A list with the <see cref="MemberInfo"/> of all members.</returns>
        protected static IList<MemberInfo> GetMemberInfoList(MemberExpression memberExp)
        {
            List<MemberInfo> list = new List<MemberInfo>();
            while (memberExp != null)
            {
                list.Add(memberExp.Member);
                memberExp = memberExp.Expression as MemberExpression;
            }
            list.Reverse();
            return list;
        }
    }
}