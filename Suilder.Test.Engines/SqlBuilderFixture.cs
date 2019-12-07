using System;
using Suilder.Builder;
using Suilder.Functions;
using Xunit;

namespace Suilder.Test.Engines
{
    public class SqlBuilderFixture
    {
        public SqlBuilderFixture()
        {
            SqlBuilder.Register(new SqlBuilder());
            SqlExp.Initialize();
        }
    }

    [CollectionDefinition("SqlBuilder")]
    public class DatabaseCollection : ICollectionFixture<SqlBuilderFixture>
    {
    }
}