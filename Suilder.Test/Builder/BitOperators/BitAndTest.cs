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
        [Theory]
        [MemberData(nameof(DataInt))]
        public void Add(int value)
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.BitAnd
                .Add(person["Id"])
                .Add(1)
                .Add(value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" & @p0 & @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataInt))]
        public void Add_Params(int value)
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.BitAnd.Add(person["Id"], 1, value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" & @p0 & @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataInt))]
        public void Add_Enumerable(int value)
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.BitAnd.Add(new List<object> { person["Id"], 1, value });

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" & @p0 & @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataInt))]
        public void Add_Expression(int value)
        {
            Person person = null;
            IOperator op = sql.BitAnd
                .Add(() => person.Id)
                .Add(() => 1)
                .Add(() => value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" & @p0 & @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataInt))]
        public void Add_Expression_Params(int value)
        {
            Person person = null;
            IOperator op = sql.BitAnd.Add(() => person.Id, () => 1, () => value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" & @p0 & @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataInt))]
        public void Add_Expression_Enumerable(int value)
        {
            Person person = null;
            IOperator op = sql.BitAnd.Add(new List<Expression<Func<object>>> { () => person.Id, () => 1, () => value });

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" & @p0 & @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataInt))]
        public void Expression(int value)
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => person.Id & 1 & value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" & @p0 & @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = value
            }, result.Parameters);
        }

        [Theory]
        [InlineData(2, 3, 4)]
        public void Expression_Large(int value1, int value2, int value3)
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => person.Id & 1 & value1 & value2 & value3 & 5);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" & @p0 & @p1 & @p2 & @p3 & @p4", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = value1,
                ["@p2"] = value2,
                ["@p3"] = value3,
                ["@p4"] = 5
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