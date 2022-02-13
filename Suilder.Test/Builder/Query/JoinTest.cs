using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Extensions;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Query
{
    public class JoinTest : BuilderBaseTest
    {
        [Fact]
        public void Inner_Join()
        {
            IQuery query = sql.Query.Inner.Join("person");

            QueryResult result = engine.Compile(query);

            Assert.Equal("INNER JOIN \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Left_Join()
        {
            IQuery query = sql.Query.Left.Join("person");

            QueryResult result = engine.Compile(query);

            Assert.Equal("LEFT JOIN \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Right_Join()
        {
            IQuery query = sql.Query.Right.Join("person");

            QueryResult result = engine.Compile(query);

            Assert.Equal("RIGHT JOIN \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Full_Join()
        {
            IQuery query = sql.Query.Full.Join("person");

            QueryResult result = engine.Compile(query);

            Assert.Equal("FULL JOIN \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Cross_Join()
        {
            IQuery query = sql.Query.Cross.Join("person");

            QueryResult result = engine.Compile(query);

            Assert.Equal("CROSS JOIN \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Join_String()
        {
            IQuery query = sql.Query.Join("person");

            QueryResult result = engine.Compile(query);

            Assert.Equal("INNER JOIN \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Join_String_With_JoinType()
        {
            IQuery query = sql.Query.Join("person", JoinType.Left);

            QueryResult result = engine.Compile(query);

            Assert.Equal("LEFT JOIN \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Join_String_With_Alias_Name()
        {
            IQuery query = sql.Query.Join("person", "per");

            QueryResult result = engine.Compile(query);

            Assert.Equal("INNER JOIN \"person\" AS \"per\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Join_String_With_Alias_Name_With_JoinType()
        {
            IQuery query = sql.Query.Join("person", "per", JoinType.Left);

            QueryResult result = engine.Compile(query);

            Assert.Equal("LEFT JOIN \"person\" AS \"per\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void JoinType_String()
        {
            IQuery query = sql.Query.Left.Join("person");

            QueryResult result = engine.Compile(query);

            Assert.Equal("LEFT JOIN \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void JoinType_String_With_Alias_Name()
        {
            IQuery query = sql.Query.Left.Join("person", "per");

            QueryResult result = engine.Compile(query);

            Assert.Equal("LEFT JOIN \"person\" AS \"per\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Join_Alias()
        {
            IAlias person = sql.Alias("person");
            IQuery query = sql.Query.Join(person);

            QueryResult result = engine.Compile(query);

            Assert.Equal("INNER JOIN \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Join_Alias_With_JoinType()
        {
            IAlias person = sql.Alias("person");
            IQuery query = sql.Query.Join(person, JoinType.Left);

            QueryResult result = engine.Compile(query);

            Assert.Equal("LEFT JOIN \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Join_Alias_With_Alias_Name()
        {
            IAlias person = sql.Alias("person", "per");
            IQuery query = sql.Query.Join(person);

            QueryResult result = engine.Compile(query);

            Assert.Equal("INNER JOIN \"person\" AS \"per\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Join_Alias_With_Alias_Name_With_JoinType()
        {
            IAlias person = sql.Alias("person", "per");
            IQuery query = sql.Query.Join(person, JoinType.Left);

            QueryResult result = engine.Compile(query);

            Assert.Equal("LEFT JOIN \"person\" AS \"per\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void JoinType_Alias()
        {
            IAlias person = sql.Alias("person");
            IQuery query = sql.Query.Left.Join(person);

            QueryResult result = engine.Compile(query);

            Assert.Equal("LEFT JOIN \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void JoinType_Alias_With_Alias_Name()
        {
            IAlias person = sql.Alias("person", "per");
            IQuery query = sql.Query.Left.Join(person);

            QueryResult result = engine.Compile(query);

            Assert.Equal("LEFT JOIN \"person\" AS \"per\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Join_Typed_Alias()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IQuery query = sql.Query.Join(person);

            QueryResult result = engine.Compile(query);

            Assert.Equal("INNER JOIN \"Person\" AS \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Join_Typed_Alias_With_JoinType()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IQuery query = sql.Query.Join(person, JoinType.Left);

            QueryResult result = engine.Compile(query);

            Assert.Equal("LEFT JOIN \"Person\" AS \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Join_Typed_Alias_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IQuery query = sql.Query.Join(person);

            QueryResult result = engine.Compile(query);

            Assert.Equal("INNER JOIN \"Person\" AS \"per\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Join_Typed_Alias_With_Alias_Name_With_JoinType()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IQuery query = sql.Query.Join(person, JoinType.Left);

            QueryResult result = engine.Compile(query);

            Assert.Equal("LEFT JOIN \"Person\" AS \"per\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void JoinType_Typed_Alias()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IQuery query = sql.Query.Left.Join(person);

            QueryResult result = engine.Compile(query);

            Assert.Equal("LEFT JOIN \"Person\" AS \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void JoinType_Typed_Alias_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IQuery query = sql.Query.Left.Join(person);

            QueryResult result = engine.Compile(query);

            Assert.Equal("LEFT JOIN \"Person\" AS \"per\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Join_Expression()
        {
            Person person = null;
            IQuery query = sql.Query.Join(() => person);

            QueryResult result = engine.Compile(query);

            Assert.Equal("INNER JOIN \"Person\" AS \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Join_Expression_With_JoinType()
        {
            Person person = null;
            IQuery query = sql.Query.Join(() => person, JoinType.Left);

            QueryResult result = engine.Compile(query);

            Assert.Equal("LEFT JOIN \"Person\" AS \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void JoinType_Expression()
        {
            Person person = null;
            IQuery query = sql.Query.Left.Join(() => person);

            QueryResult result = engine.Compile(query);

            Assert.Equal("LEFT JOIN \"Person\" AS \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Join_SubQuery_String()
        {
            IQuery query = sql.Query.Join(sql.RawQuery("Subquery"), "sub");

            QueryResult result = engine.Compile(query);

            Assert.Equal("INNER JOIN (Subquery) AS \"sub\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Join_SubQuery_String_With_JoinType()
        {
            IQuery query = sql.Query.Join(sql.RawQuery("Subquery"), "sub", JoinType.Left);

            QueryResult result = engine.Compile(query);

            Assert.Equal("LEFT JOIN (Subquery) AS \"sub\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void JoinType_SubQuery_String()
        {
            IQuery query = sql.Query.Left.Join(sql.RawQuery("Subquery"), "sub");

            QueryResult result = engine.Compile(query);

            Assert.Equal("LEFT JOIN (Subquery) AS \"sub\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Join_SubQuery_Alias()
        {
            IAlias person = sql.Alias("person");
            IQuery query = sql.Query.Join(sql.RawQuery("Subquery"), person);

            QueryResult result = engine.Compile(query);

            Assert.Equal("INNER JOIN (Subquery) AS \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Join_SubQuery_Alias_With_JoinType()
        {
            IAlias person = sql.Alias("person");
            IQuery query = sql.Query.Join(sql.RawQuery("Subquery"), person, JoinType.Left);

            QueryResult result = engine.Compile(query);

            Assert.Equal("LEFT JOIN (Subquery) AS \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Join_SubQuery_Alias_With_Alias_Name()
        {
            IAlias person = sql.Alias("person", "per");
            IQuery query = sql.Query.Join(sql.RawQuery("Subquery"), person);

            QueryResult result = engine.Compile(query);

            Assert.Equal("INNER JOIN (Subquery) AS \"per\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Join_SubQuery_Alias_With_Alias_Name_With_JoinType()
        {
            IAlias person = sql.Alias("person", "per");
            IQuery query = sql.Query.Join(sql.RawQuery("Subquery"), person, JoinType.Left);

            QueryResult result = engine.Compile(query);

            Assert.Equal("LEFT JOIN (Subquery) AS \"per\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void JoinType_SubQuery_Alias()
        {
            IAlias person = sql.Alias("person");
            IQuery query = sql.Query.Left.Join(sql.RawQuery("Subquery"), person);

            QueryResult result = engine.Compile(query);

            Assert.Equal("LEFT JOIN (Subquery) AS \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void JoinType_SubQuery_Alias_With_Alias_Name()
        {
            IAlias person = sql.Alias("person", "per");
            IQuery query = sql.Query.Left.Join(sql.RawQuery("Subquery"), person);

            QueryResult result = engine.Compile(query);

            Assert.Equal("LEFT JOIN (Subquery) AS \"per\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Join_SubQuery_Expression()
        {
            Person person = null;
            IQuery query = sql.Query.Join(sql.RawQuery("Subquery"), () => person);

            QueryResult result = engine.Compile(query);

            Assert.Equal("INNER JOIN (Subquery) AS \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Join_SubQuery_Expression_With_JoinType()
        {
            Person person = null;
            IQuery query = sql.Query.Join(sql.RawQuery("Subquery"), () => person, JoinType.Left);

            QueryResult result = engine.Compile(query);

            Assert.Equal("LEFT JOIN (Subquery) AS \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void JoinType_SubQuery_Expression()
        {
            Person person = null;
            IQuery query = sql.Query.Left.Join(sql.RawQuery("Subquery"), () => person);

            QueryResult result = engine.Compile(query);

            Assert.Equal("LEFT JOIN (Subquery) AS \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Join_On()
        {
            IAlias person = sql.Alias("person");
            IAlias dept = sql.Alias("dept");
            IQuery query = sql.Query.Join(dept).On(dept["Id"].Eq(person["DepartmentId"]));

            QueryResult result = engine.Compile(query);

            Assert.Equal("INNER JOIN \"dept\" ON \"dept\".\"Id\" = \"person\".\"DepartmentId\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Join_On_Expression()
        {
            Person person = null;
            Department dept = null;
            IQuery query = sql.Query.Join(() => dept).On(() => dept.Id == person.Department.Id);

            QueryResult result = engine.Compile(query);

            Assert.Equal("INNER JOIN \"Dept\" AS \"dept\" ON \"dept\".\"Id\" = \"person\".\"DepartmentId\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Join_Value()
        {
            IAlias person = sql.Alias("person");
            IQuery query = sql.Query.Join(sql.Join(person));

            QueryResult result = engine.Compile(query);

            Assert.Equal("INNER JOIN \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Join_Raw()
        {
            IAlias person = sql.Alias("person");
            IQuery query = sql.Query.Join(sql.Raw("INNER JOIN {0}", person));

            QueryResult result = engine.Compile(query);

            Assert.Equal("INNER JOIN \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Options()
        {
            Person person = null;
            Department dept = null;
            IQuery query = sql.Query.Join(() => dept)
                .On(() => dept.Id == person.Department.Id)
                .Options(sql.Raw("USE INDEX (myIndex)"));

            QueryResult result = engine.Compile(query);

            Assert.Equal("INNER JOIN \"Dept\" AS \"dept\" USE INDEX (myIndex) "
                + "ON \"dept\".\"Id\" = \"person\".\"DepartmentId\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }
    }
}