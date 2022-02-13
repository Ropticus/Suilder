using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Functions;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.SetOperators
{
    public class OrderByTest : BuilderBaseTest
    {
        [Fact]
        public void OrderBy_Func()
        {
            Person person = null;
            Department dept = null;
            IOperator op = sql.Union(
                sql.Query.Select(() => person.Name).From(() => person),
                sql.Query.Select(() => dept.Name).From(() => dept))
                .OrderBy(x => x.Add(() => SqlExp.ColName(person.Name)));

            QueryResult result = engine.Compile(op);

            Assert.Equal("(SELECT \"person\".\"Name\" FROM \"Person\" AS \"person\") "
               + "UNION (SELECT \"dept\".\"Name\" FROM \"Dept\" AS \"dept\") "
               + "ORDER BY \"Name\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void OrderBy_Value()
        {
            Person person = null;
            Department dept = null;
            IOperator op = sql.Union(
                sql.Query.Select(() => person.Name).From(() => person),
                sql.Query.Select(() => dept.Name).From(() => dept))
                .OrderBy(x => x.Add(() => SqlExp.ColName(person.Name)));

            QueryResult result = engine.Compile(op);

            Assert.Equal("(SELECT \"person\".\"Name\" FROM \"Person\" AS \"person\") "
               + "UNION (SELECT \"dept\".\"Name\" FROM \"Dept\" AS \"dept\") "
               + "ORDER BY \"Name\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void OrderBy_Raw()
        {
            Person person = null;
            Department dept = null;
            IOperator op = sql.Union(
                sql.Query.Select(() => person.Name).From(() => person),
                sql.Query.Select(() => dept.Name).From(() => dept))
                .OrderBy(sql.Raw("ORDER BY {0}", sql.Val(() => SqlExp.ColName(person.Name))));

            QueryResult result = engine.Compile(op);

            Assert.Equal("(SELECT \"person\".\"Name\" FROM \"Person\" AS \"person\") "
               + "UNION (SELECT \"dept\".\"Name\" FROM \"Dept\" AS \"dept\") "
               + "ORDER BY \"Name\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }
    }
}