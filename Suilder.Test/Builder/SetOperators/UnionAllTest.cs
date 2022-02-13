using System;
using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.SetOperators
{
    public class UnionAllTest : BuilderBaseTest
    {
        [Fact]
        public void Builder()
        {
            Person person = null;
            Department dept = null;
            IOperator op = sql.UnionAll(
                sql.Query.Select(() => person.Name).From(() => person),
                sql.Query.Select(() => dept.Name).From(() => dept));

            QueryResult result = engine.Compile(op);

            Assert.Equal("(SELECT \"person\".\"Name\" FROM \"Person\" AS \"person\") "
                + "UNION ALL (SELECT \"dept\".\"Name\" FROM \"Dept\" AS \"dept\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Builder_Left_Null()
        {
            Person person = null;

            Exception ex = Assert.Throws<ArgumentNullException>(() =>
            {
                sql.UnionAll(null, sql.Query.Select(() => person.Name).From(() => person));
            });
            Assert.Equal($"Left value is null. (Parameter 'left')", ex.Message);
        }

        [Fact]
        public void Builder_Right_Null()
        {
            Person person = null;

            Exception ex = Assert.Throws<ArgumentNullException>(() =>
            {
                sql.UnionAll(sql.Query.Select(() => person.Name).From(() => person), null);
            });
            Assert.Equal($"Right value is null. (Parameter 'right')", ex.Message);
        }

        [Fact]
        public void Wrap_Query_False()
        {
            engine.Options.SetOperatorWrapQuery = false;

            Person person = null;
            Department dept = null;
            IOperator op = sql.UnionAll(
                sql.Query.Select(() => person.Name).From(() => person),
                sql.Query.Select(() => dept.Name).From(() => dept));

            QueryResult result = engine.Compile(op);

            Assert.Equal("SELECT \"person\".\"Name\" FROM \"Person\" AS \"person\" "
                + "UNION ALL SELECT \"dept\".\"Name\" FROM \"Dept\" AS \"dept\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void SubOperator()
        {
            Person person = null;
            Department dept = null;
            IOperator op = sql.UnionAll(
                sql.Except(
                    sql.Query.Select(() => person.Name).From(() => person),
                    sql.Query.Select(() => dept.Name).From(() => dept)),
                sql.Intersect(
                    sql.Query.Select(() => person.SurName).From(() => person),
                    sql.Query.Select(() => dept.Name).From(() => dept)));

            QueryResult result = engine.Compile(op);

            Assert.Equal("((SELECT \"person\".\"Name\" FROM \"Person\" AS \"person\") "
                + "EXCEPT (SELECT \"dept\".\"Name\" FROM \"Dept\" AS \"dept\")) "
                + "UNION ALL ((SELECT \"person\".\"SurName\" FROM \"Person\" AS \"person\") "
                + "INTERSECT (SELECT \"dept\".\"Name\" FROM \"Dept\" AS \"dept\"))", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void SubOperator_Wrap_Query_False()
        {
            engine.Options.SetOperatorWrapQuery = false;

            Person person = null;
            Department dept = null;
            IOperator op = sql.UnionAll(
                sql.Except(
                    sql.Query.Select(() => person.Name).From(() => person),
                    sql.Query.Select(() => dept.Name).From(() => dept)),
                sql.Intersect(
                    sql.Query.Select(() => person.SurName).From(() => person),
                    sql.Query.Select(() => dept.Name).From(() => dept)));

            QueryResult result = engine.Compile(op);

            Assert.Equal("(SELECT \"person\".\"Name\" FROM \"Person\" AS \"person\" "
                + "EXCEPT SELECT \"dept\".\"Name\" FROM \"Dept\" AS \"dept\") "
                + "UNION ALL (SELECT \"person\".\"SurName\" FROM \"Person\" AS \"person\" "
                + "INTERSECT SELECT \"dept\".\"Name\" FROM \"Dept\" AS \"dept\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void SubOperator_With_SubQuery()
        {
            engine.Options.SetOperatorWithSubQuery = true;

            Person person = null;
            Department dept = null;
            IOperator op = sql.UnionAll(
                sql.Except(
                    sql.Query.Select(() => person.Name).From(() => person),
                    sql.Query.Select(() => dept.Name).From(() => dept)),
                sql.Intersect(
                    sql.Query.Select(() => person.SurName).From(() => person),
                    sql.Query.Select(() => dept.Name).From(() => dept)));

            QueryResult result = engine.Compile(op);

            Assert.Equal("SELECT * FROM ((SELECT \"person\".\"Name\" FROM \"Person\" AS \"person\") "
                + "EXCEPT (SELECT \"dept\".\"Name\" FROM \"Dept\" AS \"dept\")) "
                + "UNION ALL SELECT * FROM ((SELECT \"person\".\"SurName\" FROM \"Person\" AS \"person\") "
                + "INTERSECT (SELECT \"dept\".\"Name\" FROM \"Dept\" AS \"dept\"))", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void SubOperator_With_SubQuery_Wrap_Query_False()
        {
            engine.Options.SetOperatorWrapQuery = false;
            engine.Options.SetOperatorWithSubQuery = true;

            Person person = null;
            Department dept = null;
            IOperator op = sql.UnionAll(
                sql.Except(
                    sql.Query.Select(() => person.Name).From(() => person),
                    sql.Query.Select(() => dept.Name).From(() => dept)),
                sql.Intersect(
                    sql.Query.Select(() => person.SurName).From(() => person),
                    sql.Query.Select(() => dept.Name).From(() => dept)));

            QueryResult result = engine.Compile(op);

            Assert.Equal("SELECT * FROM (SELECT \"person\".\"Name\" FROM \"Person\" AS \"person\" "
                + "EXCEPT SELECT \"dept\".\"Name\" FROM \"Dept\" AS \"dept\") "
                + "UNION ALL SELECT * FROM (SELECT \"person\".\"SurName\" FROM \"Person\" AS \"person\" "
                + "INTERSECT SELECT \"dept\".\"Name\" FROM \"Dept\" AS \"dept\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void To_String()
        {
            Person person = null;
            Department dept = null;
            IOperator op = sql.UnionAll(
                sql.Query.Select(() => person.Name).From(() => person),
                sql.Query.Select(() => dept.Name).From(() => dept));

            Assert.Equal("(SELECT person.Name FROM Person AS person) "
                + "UNION ALL (SELECT dept.Name FROM Department AS dept)", op.ToString());
        }

        [Fact]
        public void To_String_SubOperator()
        {
            Person person = null;
            Department dept = null;
            IOperator op = sql.UnionAll(
                sql.Except(
                    sql.Query.Select(() => person.Name).From(() => person),
                    sql.Query.Select(() => dept.Name).From(() => dept)),
                sql.Intersect(
                    sql.Query.Select(() => person.SurName).From(() => person),
                    sql.Query.Select(() => dept.Name).From(() => dept)));

            Assert.Equal("((SELECT person.Name FROM Person AS person) "
                + "EXCEPT (SELECT dept.Name FROM Department AS dept)) "
                + "UNION ALL ((SELECT person.SurName FROM Person AS person) "
                + "INTERSECT (SELECT dept.Name FROM Department AS dept))", op.ToString());
        }
    }
}