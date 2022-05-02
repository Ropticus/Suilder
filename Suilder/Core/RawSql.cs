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
            if (sql == null)
                throw new ArgumentNullException(nameof(sql));

            Sql = new string[] { sql };
            Values = Array.Empty<object>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RawSql"/> class.
        /// <para>The values can be any object even other <see cref="IQueryFragment"/>.</para>
        /// <para>For escaped table and column names use an <see cref="IAlias"/> or an <see cref="IColumn"/> value.</para>
        /// </summary>
        /// <param name="sql">A composite format string, each item takes the following form: {index}.</param>
        /// <param name="values">An object array that contains zero or more objects to add to the raw SQL.</param>
        /// <returns>The raw fragment.</returns>
        /// <exception cref="FormatException">The format of <paramref name="sql"/> is invalid or the index of a format item
        /// is less than zero, or greater than or equal to the length of the <paramref name="values"/>  array.</exception>
        public RawSql(string sql, params object[] values)
        {
            if (sql == null)
                throw new ArgumentNullException(nameof(sql));

            if (values == null)
                throw new ArgumentNullException(nameof(values));

            List<string> sqlList = new List<string>();
            List<object> valuesList = new List<object>();

            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < sql.Length; i++)
            {
                char ch = sql[i];

                if (ch == '{')
                {
                    i++;

                    if (i == sql.Length)
                        throw new FormatException("Input string was not in a correct format.");

                    ch = sql[i];

                    // Escape value
                    if (ch == '{')
                    {
                        builder.Append(ch);
                    }
                    else
                    {
                        sqlList.Add(builder.ToString());
                        builder.Clear();

                        for (; i < sql.Length; i++)
                        {
                            ch = sql[i];

                            if (ch == '}')
                                break;

                            builder.Append(ch);
                        }

                        if (i == sql.Length)
                            throw new FormatException("Input string was not in a correct format.");

                        if (!int.TryParse(builder.ToString(), out int index))
                            throw new FormatException("Input string was not in a correct format.");

                        if (index < 0 || index >= values.Length)
                        {
                            throw new FormatException("Index (zero based) must be greater than or equal to zero and less "
                                + "than the size of the argument list");
                        }

                        valuesList.Add(values[index]);
                        builder.Clear();
                    }
                }
                else if (ch == '}')
                {
                    i++;

                    if (i == sql.Length || sql[i] != '}')
                        throw new FormatException("Input string was not in a correct format.");

                    // Escape value
                    builder.Append(ch);
                }
                else
                {
                    builder.Append(sql[i]);
                }
            }

            if (builder.Length > 0)
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