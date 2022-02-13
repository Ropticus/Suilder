using System;
using System.Diagnostics;
using Suilder.Builder;
using Suilder.Engines;

namespace Suilder.Core
{
    /// <summary>
    /// Implementation of <see cref="IColumn"/> for an entity type.
    /// </summary>
    public class EntityColumn : Column, IColumn
    {
        /// <summary>
        /// The table type.
        /// </summary>
        /// <value>The table type.</value>
        protected Type Type { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityColumn"/> class.
        /// </summary>
        /// <param name="type">The table type.</param>
        /// <param name="tableName">The table name or his alias.</param>
        /// <param name="columnName">The column name.</param>
        public EntityColumn(Type type, string tableName, string columnName) : base(tableName, columnName)
        {
            Type = type;
        }

        /// <summary>
        /// Creates a column without the table name or alias.
        /// </summary>
        /// <value>The column without the table name or alias.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public override IColumn Name => TableName != null ? new EntityColumn(Type, null, ColumnName) : this;

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
                foreach (string columnName in engine.GetInfo(Type).ColumnNames)
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
                queryBuilder.WriteName(engine.GetInfo(Type).GetColumnName(ColumnName));
            }
        }
    }
}