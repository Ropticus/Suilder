using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Xunit;

namespace Suilder.Test.Builder
{
    public class SqlTypeTest : BuilderBaseTest
    {
        [Fact]
        public void Type()
        {
            ISqlType type = sql.Type("VARCHAR");

            QueryResult result = engine.Compile(type);

            Assert.Equal("VARCHAR", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Type_Length()
        {
            ISqlType type = sql.Type("VARCHAR", 50);

            QueryResult result = engine.Compile(type);

            Assert.Equal("VARCHAR(50)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Type_Precision()
        {
            ISqlType type = sql.Type("DECIMAL", 10, 2);

            QueryResult result = engine.Compile(type);

            Assert.Equal("DECIMAL(10, 2)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void To_String()
        {
            ISqlType type = sql.Type("DECIMAL", 10, 2);

            Assert.Equal("DECIMAL(10, 2)", type.ToString());
        }
    }
}