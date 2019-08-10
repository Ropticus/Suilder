using System;
using System.Collections.Generic;
using System.Text;
using Suilder.Builder;
using Suilder.Engines;

namespace Suilder.Core
{
    /// <summary>
    /// Implementation of <see cref="IRawSql"/>.
    /// </summary>
    public class RawSql : IRawSql
    {
        /// <summary>
        /// The SQL values.
        /// </summary>
        /// <value>The SQL values.</value>
        protected string[] Sql { get; set; }

        /// <summary>
        /// The values.
        /// </summary>
        /// <value>The values.</value>
        protected object[] Values { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RawSql"/> class.
        /// </summary>
        /// <param name="sql">The raw SQL.</param>
        public RawSql(string sql)
        {
            Sql = new string[] { sql };
            Values = Array.Empty<object>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RawSql"/> class.
        /// <para>The values can be any object even other <see cref="IQueryFragment"/>.</para>
        /// <para>For escaped table and column names use a <see cref="IAlias"/> or a <see cref="IColumn"/> value.</para>
        /// </summary>
        /// <param name="sql">A composite string, each item takes the following form: {index}.</param>
        /// <param name="values">An object array that contains zero or more objects to add to the raw SQL.</param>
        /// <returns>The raw fragment.</returns>
        /// <exception cref="FormatException">The format of <paramref name="sql"/> is invalid or the index of a format item
        /// is less than zero, or greater than or equal to the length of the <paramref name="values"/>  array.</exception>
        public RawSql(string sql, params object[] values)
        {
            List<string> sqlList = new List<string>();
            List<object> valuesList = new List<object>();

            StringBuilder builder = new StringBuilder();
            bool startValue = false;

            for (int i = 0; i < sql.Length; i++)
            {
                if (sql[i] == '{')
                {
                    if (startValue || i + 1 == sql.Length)
                        throw new FormatException("Input string was not in a correct format.");

                    // Escape value
                    if (sql[i + 1] == '{')
                    {
                        builder.Append(sql[++i]);
                    }
                    else
                    {
                        startValue = true;
                        sqlList.Add(builder.ToString());
                        builder.Clear();
                    }
                }
                else if (sql[i] == '}')
                {
                    if (startValue)
                    {
                        string valueIndex = builder.ToString();
                        builder.Clear();

                        int index;
                        if (!int.TryParse(valueIndex, out index))
                            throw new FormatException("Input string was not in a correct format.");

                        if (index < 0 || index >= values.Length)
                        {
                            throw new FormatException("Index (zero based) must be greater than or equal to zero and less "
                                + "than the size of the argument list");
                        }

                        startValue = false;
                        valuesList.Add(values[index]);
                    }
                    else
                    {
                        // Escape value
                        if (i + 1 == sql.Length || sql[i + 1] != '}')
                            throw new FormatException("Input string was not in a correct format.");

                        builder.Append(sql[++i]);
                    }
                }
                else
                {
                    builder.Append(sql[i]);
                }
            }

            if (startValue)
                throw new FormatException("Input string was not in a correct format.");
            else if (builder.Length > 0)
                sqlList.Add(builder.ToString());

            Sql = sqlList.ToArray();
            Values = valuesList.ToArray();
        }

        /// <summary>
        /// Compiles the fragment.
        /// </summary>
        /// <param name="queryBuilder">The query builder.</param>
        /// <param name="engine">The engine.</param>
        public virtual void Compile(QueryBuilder queryBuilder, IEngine engine)
        {
            for (int i = 0; i < Sql.Length; i++)
            {
                queryBuilder.Write(Sql[i]);
                if (Values.Length > i)
                    queryBuilder.WriteValue(Values[i]);
            }
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return ToStringBuilder.Build(b => b.ForEach(Sql, (x, i) => b
                .Write(x).If(Values.Length > i, () => b.WriteValue(Values[i]))));
        }
    }
}