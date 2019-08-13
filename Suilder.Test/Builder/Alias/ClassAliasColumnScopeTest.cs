using System;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Exceptions;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Alias
{
    public class ClassAliasColumnScopeTest : BaseTest
    {
        private static Person person = null;

        public static Department Dept { get; set; }

        [Fact]
        public void Field()
        {
            IColumn column = sql.Col(() => person.Id);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\"", result.Sql);
        }

        [Fact]
        public void Property()
        {
            IColumn column = sql.Col(() => Dept.Id);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"Dept\".\"Id\"", result.Sql);
        }
    }
}