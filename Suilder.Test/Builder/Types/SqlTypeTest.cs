using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Xunit;

namespace Suilder.Test.Builder.Types
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
        public void Type_Number_Format()
        {
            ISqlType type1 = sql.Type("VARCHAR", 1000);
            ISqlType type2 = sql.Type("DECIMAL", 2000, 1000);

            QueryResult result;

            result = engine.Compile(type1);

            Assert.Equal("VARCHAR(1000)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);

            result = engine.Compile(type2);

            Assert.Equal("DECIMAL(2000, 1000)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void To_String()
        {
            ISqlType type = sql.Type("VARCHAR");

            Assert.Equal("VARCHAR", type.ToString());
        }

        [Fact]
        public void To_String_Length()
        {
            ISqlType type = sql.Type("VARCHAR", 50);

            Assert.Equal("VARCHAR(50)", type.ToString());
        }

        [Fact]
        public void To_String_Precision()
        {
            ISqlType type = sql.Type("DECIMAL", 10, 2);

            Assert.Equal("DECIMAL(10, 2)", type.ToString());
        }
    }
}