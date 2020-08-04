using Suilder.Builder;
using Suilder.Core;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.FromJoin
{
    public class JoinSchemaTest : BuilderBaseTest
    {
        [Fact]
        public void Join_String()
        {
            IJoin join = sql.Join("dbo.person");

            QueryResult result = engine.Compile(join);

            Assert.Equal("INNER JOIN \"dbo\".\"person\"", result.Sql);
        }

        [Fact]
        public void Join_String_With_Alias_Name()
        {
            IJoin join = sql.Join("dbo.person", "per");

            QueryResult result = engine.Compile(join);

            Assert.Equal("INNER JOIN \"dbo\".\"person\" AS \"per\"", result.Sql);
        }

        [Fact]
        public void Join_Alias()
        {
            IAlias person = sql.Alias("dbo.person");
            IJoin join = sql.Join(person);

            QueryResult result = engine.Compile(join);

            Assert.Equal("INNER JOIN \"dbo\".\"person\"", result.Sql);
        }

        [Fact]
        public void Join_Typed_Alias()
        {
            IAlias<Person2> person = sql.Alias<Person2>();
            IJoin join = sql.Join(person);

            QueryResult result = engine.Compile(join);

            Assert.Equal("INNER JOIN \"dbo\".\"Person\" AS \"person2\"", result.Sql);
        }

        [Fact]
        public void Join_Typed_Alias_With_Alias_Name()
        {
            IAlias<Person2> person = sql.Alias<Person2>("per");
            IJoin join = sql.Join(person);

            QueryResult result = engine.Compile(join);

            Assert.Equal("INNER JOIN \"dbo\".\"Person\" AS \"per\"", result.Sql);
        }

        [Fact]
        public void Join_Expression()
        {
            Person2 person = null;
            IJoin join = sql.Join(() => person);

            QueryResult result = engine.Compile(join);

            Assert.Equal("INNER JOIN \"dbo\".\"Person\" AS \"person\"", result.Sql);
        }

        [Fact]
        public void AliasOrTableName_Property_Table_Name()
        {
            IAlias person = sql.Alias("dbo.person");
            IJoin join = sql.Join(person);

            Assert.Equal("dbo.person", join.AliasOrTableName);
        }

        [Fact]
        public void AliasOrTableName_Property_Alias_Name()
        {
            IAlias person = sql.Alias("dbo.person", "per");
            IJoin join = sql.Join(person);

            Assert.Equal("per", join.AliasOrTableName);
        }

        [Fact]
        public void To_String()
        {
            IAlias person = sql.Alias("dbo.person");
            IJoin join = sql.Left.Join(person);

            Assert.Equal("LEFT JOIN dbo.person", join.ToString());
        }

        [Fact]
        public void To_String_With_Alias_Name()
        {
            IAlias person = sql.Alias("dbo.person", "per");
            IJoin join = sql.Left.Join(person);

            Assert.Equal("LEFT JOIN dbo.person AS per", join.ToString());
        }

        [Fact]
        public void To_String_Typed_Alias()
        {
            IAlias<Person2> person = sql.Alias<Person2>();
            IJoin join = sql.Left.Join(person);

            Assert.Equal("LEFT JOIN Person2 AS person2", join.ToString());
        }

        [Fact]
        public void To_String_Typed_Alias_With_Alias_Name()
        {
            IAlias<Person2> person = sql.Alias<Person2>("per");
            IJoin join = sql.Left.Join(person);

            Assert.Equal("LEFT JOIN Person2 AS per", join.ToString());
        }
    }
}