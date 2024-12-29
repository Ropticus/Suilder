using Suilder.Builder;
using Suilder.Functions;
using Suilder.Test.Engines;
using Xunit;

[assembly: AssemblyFixture(typeof(SqlBuilderFixture))]
namespace Suilder.Test.Engines
{
    public sealed class SqlBuilderFixture
    {
        public SqlBuilderFixture()
        {
            SqlBuilder.Register(new SqlBuilder());
            SqlExp.Initialize();
        }
    }
}