using Suilder.Builder;
using Suilder.Core;
using Xunit;

namespace Suilder.Test.Builder.FromJoin
{
    public class FromDummyTest : BuilderBaseTest
    {
        [Fact]
        public void From_Dummy_Empty()
        {
            IRawSql from = sql.FromDummy;

            QueryResult result = engine.Compile(from);

            Assert.Equal("", result.Sql);
        }

        [Fact]
        public void From_Dummy_Value()
        {
            engine.Options.FromDummyName = "DUAL";

            IRawSql from = sql.FromDummy;

            QueryResult result = engine.Compile(from);

            Assert.Equal("FROM DUAL", result.Sql);
        }

        [Fact]
        public void From_Dummy_To_String()
        {
            IRawSql from = sql.FromDummy;

            Assert.Equal("FROM dummy_table", from.ToString());
        }
    }
}