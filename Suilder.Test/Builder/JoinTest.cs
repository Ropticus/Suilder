using Suilder.Builder;
using Suilder.Core;
using Suilder.Extensions;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder
{
    public class JoinTest : BaseTest
    {
        [Fact]
        public void Inner_Join()
        {
            IJoin join = sql.Inner.Join("person");

            QueryResult result = engine.Compile(join);

            Assert.Equal("INNER JOIN \"person\"", result.Sql);
        }

        [Fact]
        public void Left_Join()
        {
            IJoin join = sql.Left.Join("person");

            QueryResult result = engine.Compile(join);

            Assert.Equal("LEFT JOIN \"person\"", result.Sql);
        }

        [Fact]
        public void Right_Join()
        {
            IJoin join = sql.Right.Join("person");

            QueryResult result = engine.Compile(join);

            Assert.Equal("RIGHT JOIN \"person\"", result.Sql);
        }

        [Fact]
        public void Full_Join()
        {
            IJoin join = sql.Full.Join("person");

            QueryResult result = engine.Compile(join);

            Assert.Equal("FULL JOIN \"person\"", result.Sql);
        }

        [Fact]
        public void Cross_Join()
        {
            IJoin join = sql.Cross.Join("person");

            QueryResult result = engine.Compile(join);

            Assert.Equal("CROSS JOIN \"person\"", result.Sql);
        }

        [Fact]
        public void Join_String()
        {
            IJoin join = sql.Join("person");

            QueryResult result = engine.Compile(join);

            Assert.Equal("INNER JOIN \"person\"", result.Sql);
        }

        [Fact]
        public void Join_String_With_Type()
        {
            IJoin join = sql.Join("person", JoinType.Left);

            QueryResult result = engine.Compile(join);

            Assert.Equal("LEFT JOIN \"person\"", result.Sql);
        }

        [Fact]
        public void Join_String_With_Alias()
        {
            IJoin join = sql.Join("person", "per");

            QueryResult result = engine.Compile(join);

            Assert.Equal("INNER JOIN \"person\" AS \"per\"", result.Sql);
        }

        [Fact]
        public void Join_String_With_Alias_With_Type()
        {
            IJoin join = sql.Join("person", "per", JoinType.Left);

            QueryResult result = engine.Compile(join);

            Assert.Equal("LEFT JOIN \"person\" AS \"per\"", result.Sql);
        }

        [Fact]
        public void Join_Type_String()
        {
            IJoin join = sql.Left.Join("person");

            QueryResult result = engine.Compile(join);

            Assert.Equal("LEFT JOIN \"person\"", result.Sql);
        }

        [Fact]
        public void Join_Type_String_With_Alias()
        {
            IJoin join = sql.Left.Join("person", "per");

            QueryResult result = engine.Compile(join);

            Assert.Equal("LEFT JOIN \"person\" AS \"per\"", result.Sql);
        }

        [Fact]
        public void Join_StringAlias()
        {
            IAlias person = sql.Alias("person");
            IJoin join = sql.Join(person);

            QueryResult result = engine.Compile(join);

            Assert.Equal("INNER JOIN \"person\"", result.Sql);
        }

        [Fact]
        public void Join_StringAlias_With_Type()
        {
            IAlias person = sql.Alias("person");
            IJoin join = sql.Join(person, JoinType.Left);

            QueryResult result = engine.Compile(join);

            Assert.Equal("LEFT JOIN \"person\"", result.Sql);
        }

        [Fact]
        public void Join_StringAlias_With_Alias()
        {
            IAlias person = sql.Alias("person", "per");
            IJoin join = sql.Join(person);

            QueryResult result = engine.Compile(join);

            Assert.Equal("INNER JOIN \"person\" AS \"per\"", result.Sql);
        }

        [Fact]
        public void Join_StringAlias_With_Alias_With_Type()
        {
            IAlias person = sql.Alias("person", "per");
            IJoin join = sql.Join(person, JoinType.Left);

            QueryResult result = engine.Compile(join);

            Assert.Equal("LEFT JOIN \"person\" AS \"per\"", result.Sql);
        }

        [Fact]
        public void Join_Type_StringAlias()
        {
            IAlias person = sql.Alias("person");
            IJoin join = sql.Left.Join(person);

            QueryResult result = engine.Compile(join);

            Assert.Equal("LEFT JOIN \"person\"", result.Sql);
        }

        [Fact]
        public void Join_Type_StringAlias_With_Alias()
        {
            IAlias person = sql.Alias("person", "per");
            IJoin join = sql.Left.Join(person);

            QueryResult result = engine.Compile(join);

            Assert.Equal("LEFT JOIN \"person\" AS \"per\"", result.Sql);
        }

        [Fact]
        public void Join_TypedAlias()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IJoin join = sql.Join(person);

            QueryResult result = engine.Compile(join);

            Assert.Equal("INNER JOIN \"Person\" AS \"person\"", result.Sql);
        }

        [Fact]
        public void Join_TypedAlias_With_Type()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IJoin join = sql.Join(person, JoinType.Left);

            QueryResult result = engine.Compile(join);

            Assert.Equal("LEFT JOIN \"Person\" AS \"person\"", result.Sql);
        }

        [Fact]
        public void Join_TypedAlias_With_Alias()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IJoin join = sql.Join(person);

            QueryResult result = engine.Compile(join);

            Assert.Equal("INNER JOIN \"Person\" AS \"per\"", result.Sql);
        }

        [Fact]
        public void Join_TypedAlias_With_Alias_With_Type()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IJoin join = sql.Join(person, JoinType.Left);

            QueryResult result = engine.Compile(join);

            Assert.Equal("LEFT JOIN \"Person\" AS \"per\"", result.Sql);
        }

        [Fact]
        public void Join_Type_TypedAlias()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IJoin join = sql.Left.Join(person);

            QueryResult result = engine.Compile(join);

            Assert.Equal("LEFT JOIN \"Person\" AS \"person\"", result.Sql);
        }

        [Fact]
        public void Join_Type_TypedAlias_With_Alias()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IJoin join = sql.Left.Join(person);

            QueryResult result = engine.Compile(join);

            Assert.Equal("LEFT JOIN \"Person\" AS \"per\"", result.Sql);
        }

        [Fact]
        public void Join_Expression()
        {
            Person person = null;
            IJoin join = sql.Join(() => person);

            QueryResult result = engine.Compile(join);

            Assert.Equal("INNER JOIN \"Person\" AS \"person\"", result.Sql);
        }

        [Fact]
        public void Join_Expression_With_Type()
        {
            Person person = null;
            IJoin join = sql.Join(() => person, JoinType.Left);

            QueryResult result = engine.Compile(join);

            Assert.Equal("LEFT JOIN \"Person\" AS \"person\"", result.Sql);
        }

        [Fact]
        public void Join_Type_Expression()
        {
            Person person = null;
            IJoin join = sql.Left.Join(() => person);

            QueryResult result = engine.Compile(join);

            Assert.Equal("LEFT JOIN \"Person\" AS \"person\"", result.Sql);
        }

        [Fact]
        public void Join_Subquery_String()
        {
            IJoin join = sql.Join(sql.RawQuery("Subquery"), "sub");

            QueryResult result = engine.Compile(join);

            Assert.Equal("INNER JOIN (Subquery) AS \"sub\"", result.Sql);
        }

        [Fact]
        public void Join_Subquery_String_With_Type()
        {
            IJoin join = sql.Join(sql.RawQuery("Subquery"), "sub", JoinType.Left);

            QueryResult result = engine.Compile(join);

            Assert.Equal("LEFT JOIN (Subquery) AS \"sub\"", result.Sql);
        }

        [Fact]
        public void Join_Type_Subquery_String()
        {
            IJoin join = sql.Left.Join(sql.RawQuery("Subquery"), "sub");

            QueryResult result = engine.Compile(join);

            Assert.Equal("LEFT JOIN (Subquery) AS \"sub\"", result.Sql);
        }

        [Fact]
        public void Join_Subquery_StringAlias()
        {
            IAlias person = sql.Alias("person");
            IJoin join = sql.Join(sql.RawQuery("Subquery"), person);

            QueryResult result = engine.Compile(join);

            Assert.Equal("INNER JOIN (Subquery) AS \"person\"", result.Sql);
        }

        [Fact]
        public void Join_Subquery_StringAlias_With_Type()
        {
            IAlias person = sql.Alias("person");
            IJoin join = sql.Join(sql.RawQuery("Subquery"), person, JoinType.Left);

            QueryResult result = engine.Compile(join);

            Assert.Equal("LEFT JOIN (Subquery) AS \"person\"", result.Sql);
        }

        [Fact]
        public void Join_Subquery_StringAlias_With_Alias()
        {
            IAlias person = sql.Alias("person", "per");
            IJoin join = sql.Join(sql.RawQuery("Subquery"), person);

            QueryResult result = engine.Compile(join);

            Assert.Equal("INNER JOIN (Subquery) AS \"per\"", result.Sql);
        }

        [Fact]
        public void Join_Subquery_StringAlias_With_Alias_With_Type()
        {
            IAlias person = sql.Alias("person", "per");
            IJoin join = sql.Join(sql.RawQuery("Subquery"), person, JoinType.Left);

            QueryResult result = engine.Compile(join);

            Assert.Equal("LEFT JOIN (Subquery) AS \"per\"", result.Sql);
        }

        [Fact]
        public void Join_Type_Subquery_StringAlias()
        {
            IAlias person = sql.Alias("person");
            IJoin join = sql.Left.Join(sql.RawQuery("Subquery"), person);

            QueryResult result = engine.Compile(join);

            Assert.Equal("LEFT JOIN (Subquery) AS \"person\"", result.Sql);
        }

        [Fact]
        public void Join_Type_Subquery_StringAlias_With_Alias()
        {
            IAlias person = sql.Alias("person", "per");
            IJoin join = sql.Left.Join(sql.RawQuery("Subquery"), person);

            QueryResult result = engine.Compile(join);

            Assert.Equal("LEFT JOIN (Subquery) AS \"per\"", result.Sql);
        }

        [Fact]
        public void Join_Subquery_Expression()
        {
            Person person = null;
            IJoin join = sql.Join(sql.RawQuery("Subquery"), () => person);

            QueryResult result = engine.Compile(join);

            Assert.Equal("INNER JOIN (Subquery) AS \"person\"", result.Sql);
        }

        [Fact]
        public void Join_Subquery_Expression_With_Type()
        {
            IAlias person = sql.Alias("per");
            IJoin join = sql.Join(sql.RawQuery("Subquery"), () => person, JoinType.Left);

            QueryResult result = engine.Compile(join);

            Assert.Equal("LEFT JOIN (Subquery) AS \"person\"", result.Sql);
        }

        [Fact]
        public void Join_Cte()
        {
            IAlias person = sql.Alias("person");
            IAlias personCte = sql.Alias("personCte");
            ICte cte = sql.Cte("cte").As(sql.Query.Select(person.All).From(person));
            IJoin join = sql.Join(cte, personCte);

            QueryResult result = engine.Compile(join);

            Assert.Equal("INNER JOIN \"cte\" AS \"personCte\"", result.Sql);
        }

        [Fact]
        public void Join_Cte_Expression()
        {
            Person person = null;
            Person personCte = null;
            ICte cte = sql.Cte("cte").As(sql.Query.Select(() => person).From(() => person));
            IJoin join = sql.Join(cte, () => personCte);

            QueryResult result = engine.Compile(join);

            Assert.Equal("INNER JOIN \"cte\" AS \"personCte\"", result.Sql);
        }

        [Fact]
        public void Join_On()
        {
            IAlias person = sql.Alias("person");
            IAlias dept = sql.Alias("dept");
            IJoin join = sql.Join(dept).On(dept["Id"].Eq(person["DepartmentId"]));

            QueryResult result = engine.Compile(join);

            Assert.Equal("INNER JOIN \"dept\" ON \"dept\".\"Id\" = \"person\".\"DepartmentId\"", result.Sql);
        }

        [Fact]
        public void Join_On_Expression()
        {
            Person person = null;
            Department dept = null;
            IJoin join = sql.Join(() => dept).On(() => dept.Id == person.Department.Id);

            QueryResult result = engine.Compile(join);

            Assert.Equal("INNER JOIN \"Dept\" AS \"dept\" ON \"dept\".\"Id\" = \"person\".\"DepartmentId\"", result.Sql);
        }

        [Fact]
        public void Options()
        {
            Person person = null;
            Department dept = null;
            IJoin join = sql.Join(() => dept)
                .On(() => dept.Id == person.Department.Id)
                .Options(sql.Raw("USE INDEX (myIndex)"));

            QueryResult result = engine.Compile(join);

            Assert.Equal("INNER JOIN \"Dept\" AS \"dept\" USE INDEX (myIndex) "
                + "ON \"dept\".\"Id\" = \"person\".\"DepartmentId\"", result.Sql);
        }

        [Fact]
        public void To_String()
        {
            IAlias person = sql.Alias("person", "per");
            IJoin join = sql.Left.Join(person);

            Assert.Equal("LEFT JOIN person AS per", join.ToString());
        }
    }
}