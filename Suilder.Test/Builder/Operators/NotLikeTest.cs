using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Extensions;
using Suilder.Functions;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Operators
{
    public class NotLikeTest : BaseTest
    {
        [Fact]
        public void Builder()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.NotLike(person["Name"], "%SomeName%");

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" NOT LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = "%SomeName%" }, result.Parameters);
        }

        [Fact]
        public void Builder_With_Null()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.NotLike(person["Name"], null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" NOT LIKE NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { }, result.Parameters);
        }

        [Fact]
        public void Builder_Expression()
        {
            Person person = null;
            IOperator op = sql.NotLike(() => person.Name, "%SomeName%");

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" NOT LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = "%SomeName%" }, result.Parameters);
        }

        [Fact]
        public void Builder_Expression_With_Null()
        {
            Person person = null;
            IOperator op = sql.NotLike(() => person.Name, null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" NOT LIKE NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { }, result.Parameters);
        }

        [Fact]
        public void Builder_Two_Expressions()
        {
            Person person = null;
            Department dept = null;
            IOperator op = sql.NotLike(() => person.Name, () => dept.Name);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" NOT LIKE \"dept\".\"Name\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Builder_Two_Expressions_With_Null()
        {
            Person person = null;
            IOperator op = sql.NotLike(() => person.Name, () => null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" NOT LIKE NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { }, result.Parameters);
        }


        [Fact]
        public void Extension()
        {
            IAlias person = sql.Alias("person");
            IOperator op = person["Name"].NotLike("%SomeName%");

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" NOT LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = "%SomeName%" }, result.Parameters);
        }

        [Fact]
        public void Extension_With_Null()
        {
            IAlias person = sql.Alias("person");
            IOperator op = person["Name"].NotLike(null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" NOT LIKE NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { }, result.Parameters);
        }

        [Fact]
        public void Extension_Expression()
        {
            IAlias person = sql.Alias("person");
            Department dept = null;
            IOperator op = person["Name"].NotLike(() => dept.Name);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" NOT LIKE \"dept\".\"Name\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Extension_Expression_With_Null()
        {
            IAlias person = sql.Alias("person");
            IOperator op = person["Name"].NotLike(() => null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" NOT LIKE NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { }, result.Parameters);
        }

        [Fact]
        public void Expression()
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Name.NotLike("%SomeName%"));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" NOT LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = "%SomeName%" }, result.Parameters);
        }

        [Fact]
        public void Expression_Method()
        {
            Person person = null;
            IOperator op = sql.Op(() => SqlExp.NotLike(person.Name, "%SomeName%"));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" NOT LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = "%SomeName%" }, result.Parameters);
        }

        [Fact]
        public void Expression_Method_Null()
        {
            Person person = null;
            IOperator op = sql.Op(() => SqlExp.NotLike(person.Name, null));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" NOT LIKE NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { }, result.Parameters);
        }

        [Fact]
        public void To_String()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.NotLike(person["Name"], "%SomeName%");

            Assert.Equal("person.Name NOT LIKE \"%SomeName%\"", op.ToString());
        }
    }
}