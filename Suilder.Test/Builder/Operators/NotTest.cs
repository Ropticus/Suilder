using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Extensions;
using Suilder.Functions;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Operators
{
    public class NotTest : BaseTest
    {
        [Fact]
        public void Builder()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Not(person["Id"].Eq(1));

            QueryResult result = engine.Compile(op);

            Assert.Equal("NOT \"person\".\"Id\" = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = 1 }, result.Parameters);
        }

        [Fact]
        public void Builder_Expression()
        {
            Person person = null;
            IOperator op = sql.Not(() => person.Id == 1);

            QueryResult result = engine.Compile(op);

            Assert.Equal("NOT \"person\".\"Id\" = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = 1 }, result.Parameters);
        }

        [Fact]
        public void Extension()
        {
            IAlias person = sql.Alias("person");
            IOperator op = person["Id"].Eq(1).Not();

            QueryResult result = engine.Compile(op);

            Assert.Equal("NOT \"person\".\"Id\" = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = 1 }, result.Parameters);
        }

        [Fact]
        public void Expression()
        {
            Person person = null;
            IOperator op = sql.Op(() => !(person.Id == 1));

            QueryResult result = engine.Compile(op);

            Assert.Equal("NOT \"person\".\"Id\" = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = 1 }, result.Parameters);
        }

        [Fact]
        public void Expression_Method()
        {
            Person person = null;
            IOperator op = sql.Op(() => SqlExp.Not(person.Id == 1));

            QueryResult result = engine.Compile(op);

            Assert.Equal("NOT \"person\".\"Id\" = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = 1 }, result.Parameters);
        }

        [Fact]
        public void To_String()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Not(person["Id"].Eq(1));

            Assert.Equal("NOT person.Id = 1", op.ToString());
        }
    }
}