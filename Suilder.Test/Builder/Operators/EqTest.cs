using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Extensions;
using Suilder.Functions;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Operators
{
    public class EqTest : BaseTest
    {
        [Fact]
        public void Builder()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Eq(person["Id"], 1);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = 1 }, result.Parameters);
        }

        [Fact]
        public void Builder_With_Null()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Eq(person["Name"], null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" IS NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { }, result.Parameters);
        }

        [Fact]
        public void Builder_Expression()
        {
            Person person = null;
            IOperator op = sql.Eq(() => person.Id, 1);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = 1 }, result.Parameters);
        }

        [Fact]
        public void Builder_Expression_With_Null()
        {
            Person person = null;
            IOperator op = sql.Eq(() => person.Name, null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" IS NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { }, result.Parameters);
        }

        [Fact]
        public void Builder_Two_Expressions()
        {
            Person person = null;
            Department dept = null;
            IOperator op = sql.Eq(() => person.Department.Id, () => dept.Id);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentId\" = \"dept\".\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Builder_Two_Expressions_With_Null()
        {
            Person person = null;
            IOperator op = sql.Eq(() => person.Name, () => null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" IS NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { }, result.Parameters);
        }


        [Fact]
        public void Extension()
        {
            IAlias person = sql.Alias("person");
            IOperator op = person["Id"].Eq(1);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = 1 }, result.Parameters);
        }

        [Fact]
        public void Extension_With_Null()
        {
            IAlias person = sql.Alias("person");
            IOperator op = person["Name"].Eq(null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" IS NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { }, result.Parameters);
        }

        [Fact]
        public void Extension_Expression()
        {
            IAlias person = sql.Alias("person");
            Department dept = null;
            IOperator op = person["DepartmentId"].Eq(() => dept.Id);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentId\" = \"dept\".\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Extension_Expression_With_Null()
        {
            IAlias person = sql.Alias("person");
            IOperator op = person["Name"].Eq(() => null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" IS NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { }, result.Parameters);
        }

        [Fact]
        public void Expression()
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Id == 1);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = 1 }, result.Parameters);
        }

        [Fact]
        public void Expression_Bool_Property()
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Active);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Active\" = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = true }, result.Parameters);
        }

        [Fact]
        public void Expression_Bool_Property_Negation()
        {
            Person person = null;
            IOperator op = sql.Op(() => !person.Active);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Active\" = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = false }, result.Parameters);
        }

        [Fact]
        public void Expression_Bool()
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Active == true);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Active\" = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = true }, result.Parameters);
        }

        [Fact]
        public void Expression_Method()
        {
            Person person = null;
            IOperator op = sql.Op(() => SqlExp.Eq(person.Id, 1));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = 1 }, result.Parameters);
        }

        [Fact]
        public void Expression_Method_Null()
        {
            Person person = null;
            IOperator op = sql.Op(() => SqlExp.Eq(person.Name, null));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" IS NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { }, result.Parameters);
        }

        [Fact]
        public void To_String()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Eq(person["Id"], 1);

            Assert.Equal("person.Id = 1", op.ToString());
        }
    }
}