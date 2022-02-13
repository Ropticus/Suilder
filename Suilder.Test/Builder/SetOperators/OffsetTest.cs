using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.SetOperators
{
    public class OffsetTest : BuilderBaseTest
    {
        [Fact]
        public void Offset()
        {
            Person person = null;
            Department dept = null;
            IOperator op = sql.Union(
                sql.Query.Select(() => person.Name).From(() => person),
                sql.Query.Select(() => dept.Name).From(() => dept))
                .Offset(10);

            QueryResult result = engine.Compile(op);

            Assert.Equal("(SELECT \"person\".\"Name\" FROM \"Person\" AS \"person\") "
               + "UNION (SELECT \"dept\".\"Name\" FROM \"Dept\" AS \"dept\") "
               + "OFFSET @p0 ROWS", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 10
            }, result.Parameters);
        }

        [Fact]
        public void Offset_Fetch_Overload()
        {
            Person person = null;
            Department dept = null;
            IOperator op = sql.Union(
                sql.Query.Select(() => person.Name).From(() => person),
                sql.Query.Select(() => dept.Name).From(() => dept))
                .Offset(10, 20);

            QueryResult result = engine.Compile(op);

            Assert.Equal("(SELECT \"person\".\"Name\" FROM \"Person\" AS \"person\") "
               + "UNION (SELECT \"dept\".\"Name\" FROM \"Dept\" AS \"dept\") "
               + "OFFSET @p0 ROWS FETCH NEXT @p1 ROWS ONLY", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 10,
                ["@p1"] = 20
            }, result.Parameters);
        }

        [Fact]
        public void Fetch()
        {
            Person person = null;
            Department dept = null;
            IOperator op = sql.Union(
                sql.Query.Select(() => person.Name).From(() => person),
                sql.Query.Select(() => dept.Name).From(() => dept))
                .Fetch(20);

            QueryResult result = engine.Compile(op);

            Assert.Equal("(SELECT \"person\".\"Name\" FROM \"Person\" AS \"person\") "
               + "UNION (SELECT \"dept\".\"Name\" FROM \"Dept\" AS \"dept\") "
               + "OFFSET @p0 ROWS FETCH NEXT @p1 ROWS ONLY", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 0,
                ["@p1"] = 20
            }, result.Parameters);
        }

        [Fact]
        public void Offset_Fetch()
        {
            Person person = null;
            Department dept = null;
            IOperator op = sql.Union(
                sql.Query.Select(() => person.Name).From(() => person),
                sql.Query.Select(() => dept.Name).From(() => dept))
                .Offset(10).Fetch(20);

            QueryResult result = engine.Compile(op);

            Assert.Equal("(SELECT \"person\".\"Name\" FROM \"Person\" AS \"person\") "
               + "UNION (SELECT \"dept\".\"Name\" FROM \"Dept\" AS \"dept\") "
               + "OFFSET @p0 ROWS FETCH NEXT @p1 ROWS ONLY", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 10,
                ["@p1"] = 20
            }, result.Parameters);
        }

        [Fact]
        public void Fetch_Offset()
        {
            Person person = null;
            Department dept = null;
            IOperator op = sql.Union(
                sql.Query.Select(() => person.Name).From(() => person),
                sql.Query.Select(() => dept.Name).From(() => dept))
                .Fetch(20).Offset(10);

            QueryResult result = engine.Compile(op);

            Assert.Equal("(SELECT \"person\".\"Name\" FROM \"Person\" AS \"person\") "
               + "UNION (SELECT \"dept\".\"Name\" FROM \"Dept\" AS \"dept\") "
               + "OFFSET @p0 ROWS FETCH NEXT @p1 ROWS ONLY", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 10,
                ["@p1"] = 20
            }, result.Parameters);
        }

        [Fact]
        public void Offset_Value()
        {
            Person person = null;
            Department dept = null;
            IOperator op = sql.Union(
                sql.Query.Select(() => person.Name).From(() => person),
                sql.Query.Select(() => dept.Name).From(() => dept))
                .Offset(sql.Offset(10, 20));

            QueryResult result = engine.Compile(op);

            Assert.Equal("(SELECT \"person\".\"Name\" FROM \"Person\" AS \"person\") "
               + "UNION (SELECT \"dept\".\"Name\" FROM \"Dept\" AS \"dept\") "
               + "OFFSET @p0 ROWS FETCH NEXT @p1 ROWS ONLY", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 10,
                ["@p1"] = 20
            }, result.Parameters);
        }

        [Fact]
        public void Offset_Raw()
        {
            Person person = null;
            Department dept = null;
            IOperator op = sql.Union(
                sql.Query.Select(() => person.Name).From(() => person),
                sql.Query.Select(() => dept.Name).From(() => dept))
                .Offset(sql.Raw("OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY", 10, 20));

            QueryResult result = engine.Compile(op);

            Assert.Equal("(SELECT \"person\".\"Name\" FROM \"Person\" AS \"person\") "
                + "UNION (SELECT \"dept\".\"Name\" FROM \"Dept\" AS \"dept\") "
                + "OFFSET @p0 ROWS FETCH NEXT @p1 ROWS ONLY", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 10,
                ["@p1"] = 20
            }, result.Parameters);
        }
    }
}