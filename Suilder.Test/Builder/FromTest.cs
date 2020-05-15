using System;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder
{
    public class FromTest : BuilderBaseTest
    {
        [Fact]
        public void From_String()
        {
            IFrom from = sql.From("person");

            QueryResult result = engine.Compile(from);

            Assert.Equal("FROM \"person\"", result.Sql);
        }

        [Fact]
        public void From_String_With_Alias_Name()
        {
            IFrom from = sql.From("person", "per");

            QueryResult result = engine.Compile(from);

            Assert.Equal("FROM \"person\" AS \"per\"", result.Sql);
        }

        [Fact]
        public void From_Alias()
        {
            IAlias person = sql.Alias("person");
            IFrom from = sql.From(person);

            QueryResult result = engine.Compile(from);

            Assert.Equal("FROM \"person\"", result.Sql);
        }

        [Fact]
        public void From_Alias_With_Alias_Name()
        {
            IAlias person = sql.Alias("person", "per");
            IFrom from = sql.From(person);

            QueryResult result = engine.Compile(from);

            Assert.Equal("FROM \"person\" AS \"per\"", result.Sql);
        }

        [Fact]
        public void From_Typed_Alias()
        {
            IAlias<Person> person = sql.Alias<Person>();
            IFrom from = sql.From(person);

            QueryResult result = engine.Compile(from);

            Assert.Equal("FROM \"Person\" AS \"person\"", result.Sql);
        }

        [Fact]
        public void From_Typed_Alias_With_Alias_Name()
        {
            IAlias<Person> person = sql.Alias<Person>("per");
            IFrom from = sql.From(person);

            QueryResult result = engine.Compile(from);

            Assert.Equal("FROM \"Person\" AS \"per\"", result.Sql);
        }

        [Fact]
        public void From_Expression()
        {
            Person person = null;
            IFrom from = sql.From(() => person);

            QueryResult result = engine.Compile(from);

            Assert.Equal("FROM \"Person\" AS \"person\"", result.Sql);
        }

        [Fact]
        public void From_Subquery_String()
        {
            IFrom from = sql.From(sql.RawQuery("Subquery"), "sub");

            QueryResult result = engine.Compile(from);

            Assert.Equal("FROM (Subquery) AS \"sub\"", result.Sql);
        }

        [Fact]
        public void From_Subquery_Alias()
        {
            IAlias person = sql.Alias("person");
            IFrom from = sql.From(sql.RawQuery("Subquery"), person);

            QueryResult result = engine.Compile(from);

            Assert.Equal("FROM (Subquery) AS \"person\"", result.Sql);
        }

        [Fact]
        public void From_Subquery_Alias_With_Alias_Name()
        {
            IAlias person = sql.Alias("person", "per");
            IFrom from = sql.From(sql.RawQuery("Subquery"), person);

            QueryResult result = engine.Compile(from);

            Assert.Equal("FROM (Subquery) AS \"per\"", result.Sql);
        }

        [Fact]
        public void From_Subquery_Expression()
        {
            Person person = null;
            IFrom from = sql.From(sql.RawQuery("Subquery"), () => person);

            QueryResult result = engine.Compile(from);

            Assert.Equal("FROM (Subquery) AS \"person\"", result.Sql);
        }

        [Fact]
        public void From_Subquery_String_Invalid_Alias_Name()
        {
            string alias = null;

            Exception ex = Assert.Throws<ArgumentNullException>(() => sql.From(sql.RawQuery("Subquery"), alias));
            Assert.Equal($"Alias name is null. (Parameter 'aliasName')", ex.Message);
        }

        [Fact]
        public void From_Subquery_Alias_Invalid_Alias_Name()
        {
            string alias = null;
            IAlias person = sql.Alias(alias);

            Exception ex = Assert.Throws<ArgumentException>(() => sql.From(sql.RawQuery("Subquery"), person));
            Assert.Equal($"Alias name is null. (Parameter 'alias')", ex.Message);
        }

        [Fact]
        public void From_Cte_String()
        {
            IAlias person = sql.Alias("person");
            ICte cte = sql.Cte("cte").As(sql.Query.Select(person.All).From(person));
            IFrom from = sql.From(cte, "personCte");

            QueryResult result = engine.Compile(from);

            Assert.Equal("FROM \"cte\" AS \"personCte\"", result.Sql);
        }

        [Fact]
        public void From_Cte_Alias()
        {
            IAlias person = sql.Alias("person");
            IAlias personCte = sql.Alias("personCte");
            ICte cte = sql.Cte("cte").As(sql.Query.Select(person.All).From(person));
            IFrom from = sql.From(cte, personCte);

            QueryResult result = engine.Compile(from);

            Assert.Equal("FROM \"cte\" AS \"personCte\"", result.Sql);
        }

        [Fact]
        public void From_Cte_Expression()
        {
            Person person = null;
            Person personCte = null;
            ICte cte = sql.Cte("cte").As(sql.Query.Select(() => person).From(() => person));
            IFrom from = sql.From(cte, () => personCte);

            QueryResult result = engine.Compile(from);

            Assert.Equal("FROM \"cte\" AS \"personCte\"", result.Sql);
        }

        [Fact]
        public void Options()
        {
            Person person = null;
            IFrom from = sql.From(() => person).Options(sql.Raw("WITH (NO LOCK)"));

            QueryResult result = engine.Compile(from);

            Assert.Equal("FROM \"Person\" AS \"person\" WITH (NO LOCK)", result.Sql);
        }

        [Fact]
        public void From_Without_As()
        {
            engine.Options.TableAs = false;

            IAlias person = sql.Alias("person", "per");
            IFrom from = sql.From(person);

            QueryResult result = engine.Compile(from);

            Assert.Equal("FROM \"person\" \"per\"", result.Sql);
        }

        [Fact]
        public void AliasOrTableName_Property_Alias_Table_Name()
        {
            IAlias person = sql.Alias("person");
            IFrom from = sql.From(person);

            Assert.Equal("person", from.AliasOrTableName);
        }

        [Fact]
        public void AliasOrTableName_Property_Alias_Alias_Name()
        {
            IAlias person = sql.Alias("person", "per");
            IFrom from = sql.From(person);

            Assert.Equal("per", from.AliasOrTableName);
        }

        [Fact]
        public void AliasOrTableName_Property_Null()
        {
            IAlias person = sql.Alias("person");
            IFrom from = new FromAliasNameNull(person);

            Assert.Null(from.AliasOrTableName);
        }

        private class FromAliasNameNull : From
        {
            public FromAliasNameNull(IAlias alias) : base(alias)
            {
                Source = null;
                AliasName = null;
            }
        }

        [Fact]
        public void From_Dummy_Empty()
        {
            IRawSql from = sql.FromDummy;

            QueryResult result = engine.Compile(from);

            Assert.Equal("", result.Sql);
        }

        [Fact]
        public void From_Dummy_Value()
        {
            engine.Options.FromDummyName = "DUAL";

            IRawSql from = sql.FromDummy;

            QueryResult result = engine.Compile(from);

            Assert.Equal("FROM DUAL", result.Sql);
        }

        [Fact]
        public void From_Dummy_To_String()
        {
            IRawSql from = sql.FromDummy;

            Assert.Equal("FROM dummy_table", from.ToString());
        }

        [Fact]
        public void To_String()
        {
            IAlias person = sql.Alias("person");
            IFrom from = sql.From(person);

            Assert.Equal("FROM person", from.ToString());
        }

        [Fact]
        public void To_String_With_Alias_Name()
        {
            IAlias person = sql.Alias("person", "per");
            IFrom from = sql.From(person);

            Assert.Equal("FROM person AS per", from.ToString());
        }

        [Fact]
        public void To_String_Options()
        {
            IAlias person = sql.Alias("person");
            IFrom from = sql.From(person).Options(sql.Raw("WITH (NO LOCK)"));

            Assert.Equal("FROM person WITH (NO LOCK)", from.ToString());
        }

        [Fact]
        public void To_String_Subquery()
        {
            IAlias person = sql.Alias("person");
            IFrom from = sql.From(sql.RawQuery("Subquery"), person);

            Assert.Equal("FROM (Subquery) AS person", from.ToString());
        }
    }
}