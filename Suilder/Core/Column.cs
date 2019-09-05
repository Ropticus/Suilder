using Suilder.Builder;
using Suilder.Engines;

namespace Suilder.Core
{
    /// <summary>
    /// Implementation of <see cref="IColumn"/>.
    /// </summary>
    public class Column : IColumn
    {
        /// <summary>
        /// The table name or his alias.
        /// </summary>
        /// <value>The table name or his alias.</value>
        public string TableName { get; protected set; }

        /// <summary>
        /// The column name.
        /// </summary>
        /// <value>The column name.</value>
        protected string ColumnName { get; set; }

        /// <summary>
        /// If the column is a select all.
        /// </summary>
        /// <value>True if the column is a select all.</value>
        public bool SelectAll => ColumnName == "*";

        /// <summary>
        /// Initializes a new instance of the <see cref="Column"/> class.
        /// </summary>
        protected Column()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Column"/> class.
        /// </summary>
        /// <param name="tableName">The table name or his alias.</param>
        /// <param name="columnName">The column name.</param>
        public Column(string tableName, string columnName)
        {
            TableName = tableName;
            ColumnName = columnName;
        }

        /// <summary>
        /// Compiles the fragment.
        /// </summary>
        /// <param name="queryBuilder">The query builder.</param>
        /// <param name="engine">The engine.</param>
        public virtual void Compile(QueryBuilder queryBuilder, IEngine engine)
        {
            Compile(queryBuilder, engine, true);
        }

        /// <summary>
        /// Compiles the fragment.
        /// </summary>
        /// <param name="queryBuilder">The query builder.</param>
        /// <param name="engine">The engine.</param>
        /// <param name="withTableName">If compile with the table name.</param>
        public virtual void Compile(QueryBuilder queryBuilder, IEngine engine, bool withTableName)
        {
            if (withTableName && TableName != null)
            {
                queryBuilder.WriteName(TableName).Write(".");
            }
            if (SelectAll)
                queryBuilder.Write("*");
            else
                queryBuilder.WriteName(ColumnName);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return ToStringBuilder.Build(b => b.IfNotNull(TableName, x => b.Write(x).Write(".")).Write(ColumnName));
        }
    }

    /// <summary>
    /// Implementation of <see cref="IColumn"/>.
    /// </summary>
    public class Column<T> : Column, IColumn
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Column{T}"/> class.
        /// </summary>
        protected Column()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Column{T}"/> class.
        /// </summary>
        /// <param name="tableName">The table name or his alias.</param>
        /// <param name="columnName">The column name.</param>
        public Column(string tableName, string columnName) : base(tableName, columnName)
        {
        }

        /// <summary>
        /// Compiles the fragment.
        /// </summary>
        /// <param name="queryBuilder">The query builder.</param>
        /// <param name="engine">The engine.</param>
        /// <param name="withTableName">If compile with the table name.</param>
        public override void Compile(QueryBuilder queryBuilder, IEngine engine, bool withTableName)
        {
            if (SelectAll)
            {
                string sep = ", ";
                foreach (string columnName in engine.GetColumnNames(typeof(T)))
                {
                    if (withTableName && TableName != null)
                    {
                        queryBuilder.WriteName(TableName).Write(".");
                    }
                    queryBuilder.WriteName(columnName).Write(sep);
                }
                queryBuilder.RemoveLast(sep.Length);
            }
            else
            {
                if (withTableName && TableName != null)
                {
                    queryBuilder.WriteName(TableName).Write(".");
                }
                queryBuilder.WriteName(engine.GetColumnName(typeof(T), ColumnName));
            }
        }
    }
}