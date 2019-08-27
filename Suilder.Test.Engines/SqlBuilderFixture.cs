using System;
using Suilder.Builder;
using Suilder.Functions;
using Xunit;

namespace Suilder.Test.Engines
{
    public class SqlBuilderFixture : IDisposable
    {
        public SqlBuilderFixture()
        {
            ISqlBuilder sql = SqlBuilder.Register(new SqlBuilder());
            SqlBuilder.Register(sql, true);
            SqlExp.Initialize();
        }

        public void Dispose()
        {
            ExpressionProcessor.ClearFunctions();
            ExpressionProcessor.ClearTables();
        }
    }

    [CollectionDefinition("SqlBuilder")]
    public class DatabaseCollection : ICollectionFixture<SqlBuilderFixture>
    {
    }
}