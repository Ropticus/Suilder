using System;
using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Exceptions;
using Suilder.Extensions;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.FromJoin
{
    public class JoinTest : BuilderBaseTest
    {
        [Fact]
        public void Inner_Join()
        {
            IJoin join = sql.Inner.Join("person");

            QueryResult result = engine.Compile(join);

            Assert.Equal("INNER JOIN \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Left_Join()
        {
            IJoin join = sql.Left.Join("person");

            QueryResult result = engine.Compile(join);

            Assert.Equal("LEFT JOIN \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Right_Join()
        {
            IJoin join = sql.Right.Join("person");

            QueryResult result = engine.Compile(join);

            Assert.Equal("RIGHT JOIN \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Full_Join()
        {
            IJoin join = sql.Full.Join("person");

            QueryResult result = engine.Compile(join);

            Assert.Equal("FULL JOIN \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Cross_Join()
        {
            IJoin join = sql.Cross.Join("person");

            QueryResult result = engine.Compile(join);

            Assert.Equal("CROSS JOIN \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Join_String()
        {
            IJoin join = sql.Join("person");

            QueryResult result = engine.Compile(join);

            Assert.Equal("INNER JOIN \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Join_String_With_JoinType()
        {
            IJoin join = sql.Join("person", JoinType.Left);

            QueryResult result = engine.Compile(join);

            Assert.Equal("LEFT JOIN \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Join_String_With_Alias_Name()
        {
            IJoin join = sql.Join("person", "per");

            QueryResult result = engine.Compile(join);

            Assert.Equal("INNER JOIN \"person\" AS \"per\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Join_String_With_Alias_Name_With_JoinType()
        {
            IJoin join = sql.Join("person", "per", JoinType.Left);

            QueryResult result = engine.Compile(join);

            Assert.Equal("LEFT JOIN \"person\" AS \"per\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void JoinType_String()
        {
            IJoin join = sql.Left.Join("person");

            QueryResult result = engine.Compile(join);

            Assert.Equal("LEFT JOIN \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void JoinType_String_With_Alias_Name()
        {
            IJoin join = sql.Left.Join("person", "per");

            QueryResult result = engine.Compile(join);

            Assert.Equal("LEFT JOIN \"person\" AS \"per\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Join_Alias()
        {
            IAlias person = sql.Alias("person");
            IJoin join = sql.Join(person);

            QueryResult result = engine.Compile(join);

            Assert.Equal("INNER JOIN \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Join_Alias_With_JoinType()
        {
            IAlias person = sql.Alias("person");
            IJoin join = sql.Join(person, JoinType.Left);

            QueryResult result = engine.Compile(join);

            Assert.Equal("LEFT JOIN \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Join_Alias_With_Alias_Name()
        {
            IAlias person = sql.Alias("person", "per");
            IJoin join = sql.Join(person);

            QueryResult result = engine.Compile(join);

            Assert.Equal("INNER JOIN \"person\" AS \"per\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Join_Alias_With_Alias_Name_With_JoinType()
        {
            IAlias person = sql.Alias("person", "per");
            IJoin join = sql.Join(person, JoinType.Left);

            QueryResult result = engine.Compile(join);

            Assert.Equal("LEFT JOIN \"person\" AS \"per\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void JoinType_Alias()
        {
            IAlias person = sql.Alias("person");
            IJoin join = sql.Left.Join(person);

            QueryResult result = engine.Compile(join);

            Assert.Equal("LEFT JOIN \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void JoinType_Alias_With_Alias_Name()
        {
            IAlias person = sql.Alias("person", "per");
            IJoin join = sql.Left.Join(person);

            QueryResult result = engine.Compile(join);

            Assert.Equal("LEFT JOIN \"person\" AS \"per\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Join_Typed_Alias()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IJoin join = sql.Join(person);

            QueryResult result = engine.Compile(join);

            Assert.Equal("INNER JOIN \"Person\" AS \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Join_Typed_Alias_With_JoinType()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IJoin join = sql.Join(person, JoinType.Left);

            QueryResult result = engine.Compile(join);

            Assert.Equal("LEFT JOIN \"Person\" AS \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Join_Typed_Alias_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IJoin join = sql.Join(person);

            QueryResult result = engine.Compile(join);

            Assert.Equal("INNER JOIN \"Person\" AS \"per\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Join_Typed_Alias_With_Alias_Name_With_JoinType()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IJoin join = sql.Join(person, JoinType.Left);

            QueryResult result = engine.Compile(join);

            Assert.Equal("LEFT JOIN \"Person\" AS \"per\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void JoinType_Typed_Alias()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IJoin join = sql.Left.Join(person);

            QueryResult result = engine.Compile(join);

            Assert.Equal("LEFT JOIN \"Person\" AS \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void JoinType_Typed_Alias_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IJoin join = sql.Left.Join(person);

            QueryResult result = engine.Compile(join);

            Assert.Equal("LEFT JOIN \"Person\" AS \"per\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Join_Expression()
        {
            Person person = null;
            IJoin join = sql.Join(() => person);

            QueryResult result = engine.Compile(join);

            Assert.Equal("INNER JOIN \"Person\" AS \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Join_Expression_With_JoinType()
        {
            Person person = null;
            IJoin join = sql.Join(() => person, JoinType.Left);

            QueryResult result = engine.Compile(join);

            Assert.Equal("LEFT JOIN \"Person\" AS \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void JoinType_Expression()
        {
            Person person = null;
            IJoin join = sql.Left.Join(() => person);

            QueryResult result = engine.Compile(join);

            Assert.Equal("LEFT JOIN \"Person\" AS \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Join_SubQuery_String()
        {
            IJoin join = sql.Join(sql.RawQuery("Subquery"), "sub");

            QueryResult result = engine.Compile(join);

            Assert.Equal("INNER JOIN (Subquery) AS \"sub\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Join_SubQuery_String_With_JoinType()
        {
            IJoin join = sql.Join(sql.RawQuery("Subquery"), "sub", JoinType.Left);

            QueryResult result = engine.Compile(join);

            Assert.Equal("LEFT JOIN (Subquery) AS \"sub\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void JoinType_SubQuery_String()
        {
            IJoin join = sql.Left.Join(sql.RawQuery("Subquery"), "sub");

            QueryResult result = engine.Compile(join);

            Assert.Equal("LEFT JOIN (Subquery) AS \"sub\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Join_SubQuery_Alias()
        {
            IAlias person = sql.Alias("person");
            IJoin join = sql.Join(sql.RawQuery("Subquery"), person);

            QueryResult result = engine.Compile(join);

            Assert.Equal("INNER JOIN (Subquery) AS \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Join_SubQuery_Alias_With_JoinType()
        {
            IAlias person = sql.Alias("person");
            IJoin join = sql.Join(sql.RawQuery("Subquery"), person, JoinType.Left);

            QueryResult result = engine.Compile(join);

            Assert.Equal("LEFT JOIN (Subquery) AS \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Join_SubQuery_Alias_With_Alias_Name()
        {
            IAlias person = sql.Alias("person", "per");
            IJoin join = sql.Join(sql.RawQuery("Subquery"), person);

            QueryResult result = engine.Compile(join);

            Assert.Equal("INNER JOIN (Subquery) AS \"per\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Join_SubQuery_Alias_With_Alias_Name_With_JoinType()
        {
            IAlias person = sql.Alias("person", "per");
            IJoin join = sql.Join(sql.RawQuery("Subquery"), person, JoinType.Left);

            QueryResult result = engine.Compile(join);

            Assert.Equal("LEFT JOIN (Subquery) AS \"per\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void JoinType_SubQuery_Alias()
        {
            IAlias person = sql.Alias("person");
            IJoin join = sql.Left.Join(sql.RawQuery("Subquery"), person);

            QueryResult result = engine.Compile(join);

            Assert.Equal("LEFT JOIN (Subquery) AS \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void JoinType_SubQuery_Alias_With_Alias_Name()
        {
            IAlias person = sql.Alias("person", "per");
            IJoin join = sql.Left.Join(sql.RawQuery("Subquery"), person);

            QueryResult result = engine.Compile(join);

            Assert.Equal("LEFT JOIN (Subquery) AS \"per\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Join_SubQuery_Expression()
        {
            Person person = null;
            IJoin join = sql.Join(sql.RawQuery("Subquery"), () => person);

            QueryResult result = engine.Compile(join);

            Assert.Equal("INNER JOIN (Subquery) AS \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Join_SubQuery_Expression_With_JoinType()
        {
            Person person = null;
            IJoin join = sql.Join(sql.RawQuery("Subquery"), () => person, JoinType.Left);

            QueryResult result = engine.Compile(join);

            Assert.Equal("LEFT JOIN (Subquery) AS \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void JoinType_SubQuery_Expression()
        {
            Person person = null;
            IJoin join = sql.Left.Join(sql.RawQuery("Subquery"), () => person);

            QueryResult result = engine.Compile(join);

            Assert.Equal("LEFT JOIN (Subquery) AS \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Join_SubQuery_String_Invalid_Alias_Name()
        {
            string alias = null;
            Exception ex = Assert.Throws<ArgumentNullException>(() => sql.Join(sql.RawQuery("Subquery"), alias));
            Assert.Equal("Alias name cannot be null. (Parameter 'aliasName')", ex.Message);
        }

        [Fact]
        public void Join_SubQuery_Alias_Invalid_Alias_Name()
        {
            string alias = null;
            IAlias person = sql.Alias(alias);

            Exception ex = Assert.Throws<ArgumentException>(() => sql.Join(sql.RawQuery("Subquery"), person));
            Assert.Equal("Alias name cannot be null. (Parameter 'alias')", ex.Message);
        }

        [Fact]
        public void Join_Cte_String()
        {
            IAlias person = sql.Alias("person");
            ICte cte = sql.Cte("cte").As(sql.Query.Select(person.All).From(person));
            IJoin join = sql.Join(cte, "personCte");

            QueryResult result = engine.Compile(join);

            Assert.Equal("INNER JOIN \"cte\" AS \"personCte\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Join_Cte_Alias()
        {
            IAlias person = sql.Alias("person");
            IAlias personCte = sql.Alias("personCte");
            ICte cte = sql.Cte("cte").As(sql.Query.Select(person.All).From(person));
            IJoin join = sql.Join(cte, personCte);

            QueryResult result = engine.Compile(join);

            Assert.Equal("INNER JOIN \"cte\" AS \"personCte\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
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
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Join_On()
        {
            IAlias person = sql.Alias("person");
            IAlias dept = sql.Alias("dept");
            IJoin join = sql.Join(dept).On(dept["Id"].Eq(person["DepartmentId"]));

            QueryResult result = engine.Compile(join);

            Assert.Equal("INNER JOIN \"dept\" ON \"dept\".\"Id\" = \"person\".\"DepartmentId\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Join_On_Expression()
        {
            Person person = null;
            Department dept = null;
            IJoin join = sql.Join(() => dept).On(() => dept.Id == person.Department.Id);

            QueryResult result = engine.Compile(join);

            Assert.Equal("INNER JOIN \"Dept\" AS \"dept\" ON \"dept\".\"Id\" = \"person\".\"DepartmentId\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Join_Without_As()
        {
            engine.Options.TableAs = false;

            IAlias person = sql.Alias("person", "per");
            IJoin join = sql.Join(person, JoinType.Left);

            QueryResult result = engine.Compile(join);

            Assert.Equal("LEFT JOIN \"person\" \"per\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void AliasOrTableName_Property_Table_Name()
        {
            IAlias person = sql.Alias("person");
            IJoin join = sql.Join(person);

            Assert.Equal("person", join.AliasOrTableName);
        }

        [Fact]
        public void AliasOrTableName_Property_Alias_Name()
        {
            IAlias person = sql.Alias("person", "per");
            IJoin join = sql.Join(person);

            Assert.Equal("per", join.AliasOrTableName);
        }

        [Fact]
        public void AliasOrTableName_Property_Null()
        {
            IJoin from = (IJoin)sql.Left;

            Assert.Null(from.AliasOrTableName);
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
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Right_Join_Not_Supported()
        {
            engine.Options.RightJoinSupported = false;

            IAlias person = sql.Alias("person");
            IJoin join = sql.Right.Join(person);

            Exception ex = Assert.Throws<ClauseNotSupportedException>(() => engine.Compile(join));
            Assert.Equal("Right join is not supported in this engine.", ex.Message);
        }

        [Fact]
        public void Full_Join_Not_Supported()
        {
            engine.Options.FullJoinSupported = false;

            IAlias person = sql.Alias("person");
            IJoin join = sql.Full.Join(person);

            Exception ex = Assert.Throws<ClauseNotSupportedException>(() => engine.Compile(join));
            Assert.Equal("Full join is not supported in this engine.", ex.Message);
        }

        [Fact]
        public void Invalid_JoinType()
        {
            IJoin join = sql.Join("person", (JoinType)int.MaxValue);

            Exception ex = Assert.Throws<CompileException>(() => engine.Compile(join));
            Assert.Equal($"Invalid join type \"{int.MaxValue}\".", ex.Message);
        }

        [Fact]
        public void To_String()
        {
            IAlias person = sql.Alias("person");
            IJoin join = sql.Left.Join(person);

            Assert.Equal("LEFT JOIN person", join.ToString());
        }

        [Fact]
        public void To_String_With_Alias_Name()
        {
            IAlias person = sql.Alias("person", "per");
            IJoin join = sql.Left.Join(person);

            Assert.Equal("LEFT JOIN person AS per", join.ToString());
        }

        [Fact]
        public void To_String_Typed_Alias()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IJoin join = sql.Left.Join(person);

            Assert.Equal("LEFT JOIN Person AS person", join.ToString());
        }

        [Fact]
        public void To_String_Typed_Alias_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IJoin join = sql.Left.Join(person);

            Assert.Equal("LEFT JOIN Person AS per", join.ToString());
        }

        [Fact]
        public void To_String_Options()
        {
            IAlias person = sql.Alias("person");
            IJoin join = sql.Left.Join(person)
                .Options(sql.Raw("USE INDEX (myIndex)"));

            Assert.Equal("LEFT JOIN person USE INDEX (myIndex)", join.ToString());
        }

        [Fact]
        public void To_String_SubQuery()
        {
            IAlias person = sql.Alias("person");
            IJoin join = sql.Left.Join(sql.RawQuery("Subquery"), person);

            Assert.Equal("LEFT JOIN (Subquery) AS person", join.ToString());
        }
    }
}