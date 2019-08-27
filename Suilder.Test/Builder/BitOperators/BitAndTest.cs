using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Exceptions;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.BitOperators
{
    public class BitAndTest : BuilderBaseTest
    {
        [Fact]
        public void Add()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.BitAnd
                .Add(person["Id"])
                .Add(1)
                .Add(2);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" & @p0 & @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = 2
            }, result.Parameters);
        }

        [Fact]
        public void Add_Params()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.BitAnd.Add(person["Id"], 1, 2);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" & @p0 & @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = 2
            }, result.Parameters);
        }

        [Fact]
        public void Add_Enumerable()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.BitAnd.Add(new List<object> { person["Id"], 1, 2 });

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" & @p0 & @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = 2
            }, result.Parameters);
        }

        [Fact]
        public void Add_Expression()
        {
            Person person = null;
            IOperator op = sql.BitAnd
                .Add(() => person.Id)
                .Add(() => 1)
                .Add(() => 2);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" & @p0 & @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = 2
            }, result.Parameters);
        }

        [Fact]
        public void Add_Expression_Params()
        {
            Person person = null;
            IOperator op = sql.BitAnd.Add(() => person.Id, () => 1, () => 2);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" & @p0 & @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = 2
            }, result.Parameters);
        }

        [Fact]
        public void Add_Expression_Enumerable()
        {
            Person person = null;
            IOperator op = sql.BitAnd.Add(new List<Expression<Func<object>>> { () => person.Id, () => 1, () => 2 });

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" & @p0 & @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = 2
            }, result.Parameters);
        }

        [Fact]
        public void Expression()
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => person.Id & 1 & 2);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" & @p0 & @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = 2
            }, result.Parameters);
        }

        [Fact]
        public void Empty_List()
        {
            IOperator op = sql.BitAnd;

            Exception ex = Assert.Throws<CompileException>(() => engine.Compile(op));
            Assert.Equal("List is empty.", ex.Message);
        }

        [Fact]
        public void To_String()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.BitAnd
                .Add(person["Id"])
                .Add(1)
                .Add(2);

            Assert.Equal("person.Id & 1 & 2", op.ToString());
        }
    }
}