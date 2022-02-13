using System.Diagnostics;
using Suilder.Builder;
using Suilder.Engines;

namespace Suilder.Core
{
    /// <summary>
    /// Implementation of <see cref="IAlias"/>.
    /// </summary>
    public class Alias : IAlias
    {
        /// <summary>
        /// The table name.
        /// </summary>
        /// <value>The table name.</value>
        protected string TableName { get; set; }

        /// <summary>
        /// The alias name.
        /// </summary>
        /// <value>The alias name.</value>
        public string AliasName { get; protected set; }

        /// <summary>
        /// The alias name or the table name.
        /// </summary>
        /// <value>The alias name or the table name.</value>
        public string AliasOrTableName => AliasName ?? TableName;

        /// <summary>
        /// Initializes a new instance of the <see cref="Alias"/> class.
        /// </summary>
        /// <param name="tableName">The table name.</param>
        public Alias(string tableName)
        {
            TableName = tableName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Alias"/> class.
        /// </summary>
        /// <param name="tableName">The table name.</param>
        /// <param name="aliasName">The alias name.</param>
        public Alias(string tableName, string aliasName)
        {
            TableName = tableName;
            AliasName = aliasName;
        }

        /// <summary>
        /// Creates a column with the alias.
        /// </summary>
        /// <param name="columnName">The column name.</param>
        /// <returns>The column.</returns>
        public virtual IColumn Col(string columnName)
        {
            return SqlBuilder.Instance.Col(AliasName ?? TableName, columnName);
        }

        /// <summary>
        /// Creates a column with the alias.
        /// </summary>
        /// <param name="columnName">The column name.</param>
        /// <returns>The column.</returns>
        public virtual IColumn this[string columnName] => Col(columnName);

        /// <summary>
        /// Creates a select all column with the alias.
        /// </summary>
        /// <value>The column.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public virtual IColumn All => Col("*");

        /// <summary>
        /// Compiles the fragment.
        /// </summary>
        /// <param name="queryBuilder">The query builder.</param>
        /// <param name="engine">The engine.</param>
        public virtual void Compile(QueryBuilder queryBuilder, IEngine engine)
        {
            queryBuilder.WriteName(TableName);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return ToStringBuilder.Build(b => b.Write(TableName));
        }
    }
}