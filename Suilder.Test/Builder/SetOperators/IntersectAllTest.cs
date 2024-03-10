using System;
using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Exceptions;
using Suilder.Operators;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.SetOperators
{
    public class IntersectAllTest : BuilderBaseTest
    {
        [Fact]
        public void Builder()
        {
            Person person = null;
            Department dept = null;
            IOperator op = sql.IntersectAll(
                sql.Query.Select(() => person.Name).From(() => person),
                sql.Query.Select(() => dept.Name).From(() => dept));

            QueryResult result = engine.Compile(op);

            Assert.Equal("(SELECT \"person\".\"Name\" FROM \"Person\" AS \"person\") "
                + "INTERSECT ALL (SELECT \"dept\".\"Name\" FROM \"Dept\" AS \"dept\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Builder_Left_Null()
        {
            Person person = null;

            Exception ex = Assert.Throws<ArgumentNullException>(() =>
            {
                sql.IntersectAll(null, sql.Query.Select(() => person.Name).From(() => person));
            });
            Assert.Equal("Value cannot be null. (Parameter 'left')", ex.Message);
        }

        [Fact]
        public void Builder_Right_Null()
        {
            Person person = null;

            Exception ex = Assert.Throws<ArgumentNullException>(() =>
            {
                sql.IntersectAll(sql.Query.Select(() => person.Name).From(() => person), null);
            });
            Assert.Equal("Value cannot be null. (Parameter 'right')", ex.Message);
        }

        [Fact]
        public void Translation()
        {
            engine.AddOperator(OperatorName.IntersectAll, "TRANSLATED");

            Person person = null;
            Department dept = null;
            IOperator op = sql.IntersectAll(
                sql.Query.Select(() => person.Name).From(() => person),
                sql.Query.Select(() => dept.Name).From(() => dept));

            QueryResult result = engine.Compile(op);

            Assert.Equal("(SELECT \"person\".\"Name\" FROM \"Person\" AS \"person\") "
                + "TRANSLATED (SELECT \"dept\".\"Name\" FROM \"Dept\" AS \"dept\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Translation_Function_Invalid()
        {
            engine.AddOperator(OperatorName.IntersectAll, "TRANSLATED", true);

            Person person = null;
            Department dept = null;
            IOperator op = sql.IntersectAll(
                sql.Query.Select(() => person.Name).From(() => person),
                sql.Query.Select(() => dept.Name).From(() => dept));

            Exception ex = Assert.Throws<InvalidConfigurationException>(() => engine.Compile(op));
            Assert.Equal("The set operator cannot be a function.", ex.Message);
        }

        [Fact]
        public void Wrap_Query_False()
        {
            engine.Options.SetOperatorWrapQuery = false;

            Person person = null;
            Department dept = null;
            IOperator op = sql.IntersectAll(
                sql.Query.Select(() => person.Name).From(() => person),
                sql.Query.Select(() => dept.Name).From(() => dept));

            QueryResult result = engine.Compile(op);

            Assert.Equal("SELECT \"person\".\"Name\" FROM \"Person\" AS \"person\" "
                + "INTERSECT ALL SELECT \"dept\".\"Name\" FROM \"Dept\" AS \"dept\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void SubOperator()
        {
            Person person = null;
            Department dept = null;
            IOperator op = sql.IntersectAll(
                sql.Except(
                    sql.Query.Select(() => person.Name).From(() => person),
                    sql.Query.Select(() => dept.Name).From(() => dept)),
                sql.Intersect(
                    sql.Query.Select(() => person.Surname).From(() => person),
                    sql.Query.Select(() => dept.Name).From(() => dept)));

            QueryResult result = engine.Compile(op);

            Assert.Equal("((SELECT \"person\".\"Name\" FROM \"Person\" AS \"person\") "
                + "EXCEPT (SELECT \"dept\".\"Name\" FROM \"Dept\" AS \"dept\")) "
                + "INTERSECT ALL ((SELECT \"person\".\"Surname\" FROM \"Person\" AS \"person\") "
                + "INTERSECT (SELECT \"dept\".\"Name\" FROM \"Dept\" AS \"dept\"))", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void SubOperator_Wrap_Query_False()
        {
            engine.Options.SetOperatorWrapQuery = false;

            Person person = null;
            Department dept = null;
            IOperator op = sql.IntersectAll(
                sql.Except(
                    sql.Query.Select(() => person.Name).From(() => person),
                    sql.Query.Select(() => dept.Name).From(() => dept)),
                sql.Intersect(
                    sql.Query.Select(() => person.Surname).From(() => person),
                    sql.Query.Select(() => dept.Name).From(() => dept)));

            QueryResult result = engine.Compile(op);

            Assert.Equal("(SELECT \"person\".\"Name\" FROM \"Person\" AS \"person\" "
                + "EXCEPT SELECT \"dept\".\"Name\" FROM \"Dept\" AS \"dept\") "
                + "INTERSECT ALL (SELECT \"person\".\"Surname\" FROM \"Person\" AS \"person\" "
                + "INTERSECT SELECT \"dept\".\"Name\" FROM \"Dept\" AS \"dept\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void SubOperator_With_SubQuery()
        {
            engine.Options.SetOperatorWithSubQuery = true;

            Person person = null;
            Department dept = null;
            IOperator op = sql.IntersectAll(
                sql.Except(
                    sql.Query.Select(() => person.Name).From(() => person),
                    sql.Query.Select(() => dept.Name).From(() => dept)),
                sql.Intersect(
                    sql.Query.Select(() => person.Surname).From(() => person),
                    sql.Query.Select(() => dept.Name).From(() => dept)));

            QueryResult result = engine.Compile(op);

            Assert.Equal("SELECT * FROM ((SELECT \"person\".\"Name\" FROM \"Person\" AS \"person\") "
                + "EXCEPT (SELECT \"dept\".\"Name\" FROM \"Dept\" AS \"dept\")) "
                + "INTERSECT ALL SELECT * FROM ((SELECT \"person\".\"Surname\" FROM \"Person\" AS \"person\") "
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
            IOperator op = sql.IntersectAll(
                sql.Except(
                    sql.Query.Select(() => person.Name).From(() => person),
                    sql.Query.Select(() => dept.Name).From(() => dept)),
                sql.Intersect(
                    sql.Query.Select(() => person.Surname).From(() => person),
                    sql.Query.Select(() => dept.Name).From(() => dept)));

            QueryResult result = engine.Compile(op);

            Assert.Equal("SELECT * FROM (SELECT \"person\".\"Name\" FROM \"Person\" AS \"person\" "
                + "EXCEPT SELECT \"dept\".\"Name\" FROM \"Dept\" AS \"dept\") "
                + "INTERSECT ALL SELECT * FROM (SELECT \"person\".\"Surname\" FROM \"Person\" AS \"person\" "
                + "INTERSECT SELECT \"dept\".\"Name\" FROM \"Dept\" AS \"dept\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void To_String()
        {
            Person person = null;
            Department dept = null;
            IOperator op = sql.IntersectAll(
                sql.Query.Select(() => person.Name).From(() => person),
                sql.Query.Select(() => dept.Name).From(() => dept));

            Assert.Equal("(SELECT person.Name FROM Person AS person) "
                + "INTERSECT ALL (SELECT dept.Name FROM Department AS dept)", op.ToString());
        }

        [Fact]
        public void To_String_SubOperator()
        {
            Person person = null;
            Department dept = null;
            IOperator op = sql.IntersectAll(
                sql.Except(
                    sql.Query.Select(() => person.Name).From(() => person),
                    sql.Query.Select(() => dept.Name).From(() => dept)),
                sql.Intersect(
                    sql.Query.Select(() => person.Surname).From(() => person),
                    sql.Query.Select(() => dept.Name).From(() => dept)));

            Assert.Equal("((SELECT person.Name FROM Person AS person) "
                + "EXCEPT (SELECT dept.Name FROM Department AS dept)) "
                + "INTERSECT ALL ((SELECT person.Surname FROM Person AS person) "
                + "INTERSECT (SELECT dept.Name FROM Department AS dept))", op.ToString());
        }
    }
}