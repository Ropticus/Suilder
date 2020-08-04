using Suilder.Builder;
using Suilder.Core;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.FromJoin
{
    public class FromSchemaTest : BuilderBaseTest
    {
        [Fact]
        public void From_String()
        {
            IFrom from = sql.From("dbo.person");

            QueryResult result = engine.Compile(from);

            Assert.Equal("FROM \"dbo\".\"person\"", result.Sql);
        }

        [Fact]
        public void From_String_With_Alias_Name()
        {
            IFrom from = sql.From("dbo.person", "per");

            QueryResult result = engine.Compile(from);

            Assert.Equal("FROM \"dbo\".\"person\" AS \"per\"", result.Sql);
        }

        [Fact]
        public void From_Alias()
        {
            IAlias person = sql.Alias("dbo.person");
            IFrom from = sql.From(person);

            QueryResult result = engine.Compile(from);

            Assert.Equal("FROM \"dbo\".\"person\"", result.Sql);
        }

        [Fact]
        public void From_Alias_With_Alias_Name()
        {
            IAlias person = sql.Alias("dbo.person", "per");
            IFrom from = sql.From(person);

            QueryResult result = engine.Compile(from);

            Assert.Equal("FROM \"dbo\".\"person\" AS \"per\"", result.Sql);
        }

        [Fact]
        public void From_Typed_Alias()
        {
            IAlias<Person2> person = sql.Alias<Person2>();
            IFrom from = sql.From(person);

            QueryResult result = engine.Compile(from);

            Assert.Equal("FROM \"dbo\".\"Person\" AS \"person2\"", result.Sql);
        }

        [Fact]
        public void From_Typed_Alias_With_Alias_Name()
        {
            IAlias<Person2> person = sql.Alias<Person2>("per");
            IFrom from = sql.From(person);

            QueryResult result = engine.Compile(from);

            Assert.Equal("FROM \"dbo\".\"Person\" AS \"per\"", result.Sql);
        }

        [Fact]
        public void From_Expression()
        {
            Person2 person = null;
            IFrom from = sql.From(() => person);

            QueryResult result = engine.Compile(from);

            Assert.Equal("FROM \"dbo\".\"Person\" AS \"person\"", result.Sql);
        }

        [Fact]
        public void AliasOrTableName_Property_Table_Name()
        {
            IAlias person = sql.Alias("dbo.person");
            IFrom from = sql.From(person);

            Assert.Equal("dbo.person", from.AliasOrTableName);
        }

        [Fact]
        public void AliasOrTableName_Property_Alias_Name()
        {
            IAlias person = sql.Alias("dbo.person", "per");
            IFrom from = sql.From(person);

            Assert.Equal("per", from.AliasOrTableName);
        }

        [Fact]
        public void To_String()
        {
            IAlias person = sql.Alias("dbo.person");
            IFrom from = sql.From(person);

            Assert.Equal("FROM dbo.person", from.ToString());
        }

        [Fact]
        public void To_String_With_Alias_Name()
        {
            IAlias person = sql.Alias("dbo.person", "per");
            IFrom from = sql.From(person);

            Assert.Equal("FROM dbo.person AS per", from.ToString());
        }

        [Fact]
        public void To_String_Typed_Alias()
        {
            IAlias<Person2> person = sql.Alias<Person2>();
            IFrom from = sql.From(person);

            Assert.Equal("FROM Person2 AS person2", from.ToString());
        }

        [Fact]
        public void To_String_Typed_Alias_With_Alias_Name()
        {
            IAlias<Person2> person = sql.Alias<Person2>("per");
            IFrom from = sql.From(person);

            Assert.Equal("FROM Person2 AS per", from.ToString());
        }
    }
}