using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.QueryOperators
{
    public class UnionTest : BaseTest
    {
        [Fact]
        public void Builder()
        {
            Person person = null;
            Department dept = null;
            IOperator op = sql.Union(
                sql.Query.Select(() => person.Name).From(() => person),
                sql.Query.Select(() => dept.Name).From(() => dept));

            QueryResult result = engine.Compile(op);

            Assert.Equal("SELECT \"person\".\"Name\" FROM \"Person\" AS \"person\" "
                + "UNION SELECT \"dept\".\"Name\" FROM \"Dept\" AS \"dept\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void To_String()
        {
            Person person = null;
            Department dept = null;
            IOperator op = sql.Union(
                sql.Query.Select(() => person.Name).From(() => person),
                sql.Query.Select(() => dept.Name).From(() => dept));

            Assert.Equal("SELECT person.Name FROM Person AS person "
                + "UNION SELECT dept.Name FROM Department AS dept", op.ToString());
        }
    }
}