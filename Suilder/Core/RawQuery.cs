using System;
using Suilder.Builder;
using Suilder.Engines;

namespace Suilder.Core
{
    /// <summary>
    /// Implementation of <see cref="IRawQuery"/>.
    /// </summary>
    public class RawQuery : RawSql, IRawQuery
    {
        /// <summary>
        /// The value to write before the query.
        /// </summary>
        /// <value>The value to write before the query.</value>
        protected IQueryFragment BeforeValue { get; set; }

        /// <summary>
        /// The value to write after the query.
        /// </summary>
        /// <value>The value to write after the query.</value>
        protected IQueryFragment AfterValue { get; set; }

        /// <summary>
        /// The offset value.
        /// </summary>
        /// <value>The offset value.</value>
        protected IQueryFragment OffsetValue { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RawQuery"/> class.
        /// </summary>
        /// <param name="sql">The raw SQL.</param>
        public RawQuery(string sql) : base(sql)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RawQuery"/> class.
        /// <para>The values can be any object even other <see cref="IQueryFragment"/>.</para>
        /// <para>For escaped table and column names use an <see cref="IAlias"/> or an <see cref="IColumn"/> value.</para>
        /// </summary>
        /// <param name="sql">A composite string, each item takes the following form: {index}.</param>
        /// <param name="values">An object array that contains zero or more objects to add to the raw SQL.</param>
        /// <returns>The raw fragment.</returns>
        /// <exception cref="FormatException">The format of <paramref name="sql"/> is invalid or the index of a format item
        /// is less than zero, or greater than or equal to the length of the <paramref name="values"/>  array.</exception>
        public RawQuery(string sql, params object[] values) : base(sql, values)
        {
        }

        /// <summary>
        /// Sets a value to write before the query.
        /// </summary>
        /// <param name="value">The value to write before the query.</param>
        /// <returns>The raw query.</returns>
        public virtual IRawQuery Before(IQueryFragment value)
        {
            BeforeValue = value;
            return this;
        }

        /// <summary>
        /// Sets a value to write after the query.
        /// </summary>
        /// <param name="value">The value to write after the query.</param>
        /// <returns>The raw query.</returns>
        public virtual IRawQuery After(IQueryFragment value)
        {
            AfterValue = value;
            return this;
        }

        /// <summary>
        /// Sets the "offset" clause.
        /// </summary>
        /// <param name="offset">The "offset" clause.</param>
        /// <returns>The raw query.</returns>
        public virtual IRawQuery Offset(IOffset offset)
        {
            OffsetValue = offset;
            return this;
        }

        /// <summary>
        /// Sets a raw "offset" clause.
        /// <para>You must write the entire clause.</para>
        /// </summary>
        /// <param name="offset">The "offset" clause.</param>
        /// <returns>The raw query.</returns>
        public virtual IRawQuery Offset(IRawSql offset)
        {
            OffsetValue = offset;
            return this;
        }

        /// <summary>
        /// Creates or adds an "offset" clause.
        /// </summary>
        /// <param name="offset">The number of rows to skip.</param>
        /// <returns>The raw query.</returns>
        public virtual IRawQuery Offset(object offset)
        {
            if (OffsetValue is IOffset offsetValue)
                offsetValue.Offset(offset);
            else
                OffsetValue = SqlBuilder.Instance.Offset(offset);
            return this;
        }

        /// <summary>
        /// Creates an "offset fetch" clause.
        /// </summary>
        /// <param name="offset">The number of rows to skip.</param>
        /// <param name="fetch">The number of rows to return.</param>
        /// <returns>The raw query.</returns>
        public virtual IRawQuery Offset(object offset, object fetch)
        {
            OffsetValue = SqlBuilder.Instance.Offset(offset, fetch);
            return this;
        }

        /// <summary>
        /// Creates or adds "fetch" clause.
        /// </summary>
        /// <param name="fetch">The number of rows to return.</param>
        /// <returns>The raw query.</returns>
        public virtual IRawQuery Fetch(object fetch)
        {
            if (OffsetValue is IOffset offsetValue)
                offsetValue.Fetch(fetch);
            else
                OffsetValue = SqlBuilder.Instance.Fetch(fetch);
            return this;
        }

        /// <summary>
        /// Compiles the fragment.
        /// </summary>
        /// <param name="queryBuilder">The query builder.</param>
        /// <param name="engine">The engine.</param>
        public override void Compile(QueryBuilder queryBuilder, IEngine engine)
        {
            if (BeforeValue != null)
                queryBuilder.WriteFragment(BeforeValue).Write(" ");

            base.Compile(queryBuilder, engine);

            if (OffsetValue != null)
                queryBuilder.Write(" ").WriteFragment(OffsetValue);

            if (AfterValue != null)
                queryBuilder.Write(" ").WriteFragment(AfterValue);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return ToStringBuilder.Build(b => b
                .IfNotNull(BeforeValue, x => b.WriteFragment(x).Write(" "))
                .Write(base.ToString())
                .IfNotNull(OffsetValue, x => b.Write(" ").WriteFragment(x))
                .IfNotNull(AfterValue, x => b.Write(" ").WriteFragment(x)));
        }
    }
}