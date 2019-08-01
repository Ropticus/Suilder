using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Extensions;
using Suilder.Functions;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Operators
{
    public class LikeTest : BaseTest
    {
        [Fact]
        public void Builder()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Like(person["Name"], "%SomeName%");

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = "%SomeName%" }, result.Parameters);
        }

        [Fact]
        public void Builder_With_Null()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Like(person["Name"], null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" LIKE NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { }, result.Parameters);
        }

        [Fact]
        public void Builder_Expression()
        {
            Person person = null;
            IOperator op = sql.Like(() => person.Name, "%SomeName%");

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = "%SomeName%" }, result.Parameters);
        }

        [Fact]
        public void Builder_Expression_With_Null()
        {
            Person person = null;
            IOperator op = sql.Like(() => person.Name, null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" LIKE NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { }, result.Parameters);
        }

        [Fact]
        public void Builder_Two_Expressions()
        {
            Person person = null;
            Department dept = null;
            IOperator op = sql.Like(() => person.Name, () => dept.Name);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" LIKE \"dept\".\"Name\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Builder_Two_Expressions_With_Null()
        {
            Person person = null;
            IOperator op = sql.Like(() => person.Name, () => null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" LIKE NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { }, result.Parameters);
        }


        [Fact]
        public void Extension()
        {
            IAlias person = sql.Alias("person");
            IOperator op = person["Name"].Like("%SomeName%");

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = "%SomeName%" }, result.Parameters);
        }

        [Fact]
        public void Extension_With_Null()
        {
            IAlias person = sql.Alias("person");
            IOperator op = person["Name"].Like(null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" LIKE NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { }, result.Parameters);
        }

        [Fact]
        public void Extension_Expression()
        {
            IAlias person = sql.Alias("person");
            Department dept = null;
            IOperator op = person["Name"].Like(() => dept.Name);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" LIKE \"dept\".\"Name\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Extension_Expression_With_Null()
        {
            IAlias person = sql.Alias("person");
            IOperator op = person["Name"].Like(() => null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" LIKE NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { }, result.Parameters);
        }

        [Fact]
        public void Expression()
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Name.Like("%SomeName%"));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = "%SomeName%" }, result.Parameters);
        }

        [Fact]
        public void Expression_Method()
        {
            Person person = null;
            IOperator op = sql.Op(() => SqlExp.Like(person.Name, "%SomeName%"));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = "%SomeName%" }, result.Parameters);
        }

        [Fact]
        public void Expression_Method_Null()
        {
            Person person = null;
            IOperator op = sql.Op(() => SqlExp.Like(person.Name, null));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" LIKE NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { }, result.Parameters);
        }

        [Fact]
        public void ToLikeAny()
        {
            string value = sql.ToLikeAny("SomeName");
            Assert.Equal("%SomeName%", value);
        }

        [Fact]
        public void ToLikeAnyExtension()
        {
            string value = "SomeName".ToLikeAny();
            Assert.Equal("%SomeName%", value);
        }

        [Fact]
        public void ToLikeStart()
        {
            string value = sql.ToLikeStart("SomeName");
            Assert.Equal("SomeName%", value);
        }

        [Fact]
        public void ToLikeStartExtension()
        {
            string value = "SomeName".ToLikeStart();
            Assert.Equal("SomeName%", value);
        }

        [Fact]
        public void ToLikeEnd()
        {
            string value = sql.ToLikeEnd("SomeName");
            Assert.Equal("%SomeName", value);
        }

        [Fact]
        public void ToLikeEndExtension()
        {
            string value = "SomeName".ToLikeEnd();
            Assert.Equal("%SomeName", value);
        }

        [Fact]
        public void To_String()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Like(person["Name"], "%SomeName%");

            Assert.Equal("person.Name LIKE \"%SomeName%\"", op.ToString());
        }
    }
}