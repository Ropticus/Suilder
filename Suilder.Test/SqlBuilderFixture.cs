using System;
using Suilder.Builder;
using Suilder.Functions;
using Suilder.Test;
using Xunit;

[assembly: AssemblyFixture(typeof(SqlBuilderFixture))]
namespace Suilder.Test
{
    public sealed class SqlBuilderFixture : IDisposable
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
}