using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Exceptions;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.ArithOperators
{
    public class ModuloTest : BuilderBaseTest
    {
        [Theory]
        [MemberData(nameof(DataDecimal))]
        public void Add(decimal value)
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Modulo
                .Add(person["Salary"])
                .Add(100m)
                .Add(value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Salary\" % @p0 % @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 100m,
                ["@p1"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataDecimal))]
        public void Add_Params(decimal value)
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Modulo.Add(person["Salary"], 100m, value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Salary\" % @p0 % @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 100m,
                ["@p1"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataDecimal))]
        public void Add_Enumerable(decimal value)
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Modulo.Add(new List<object> { person["Salary"], 100m, value });

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Salary\" % @p0 % @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 100m,
                ["@p1"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataDecimal))]
        public void Add_Expression(decimal value)
        {
            Person person = null;
            IOperator op = sql.Modulo
                .Add(() => person.Salary)
                .Add(() => 100m)
                .Add(() => value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Salary\" % @p0 % @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 100m,
                ["@p1"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataDecimal))]
        public void Add_Expression_Params(decimal value)
        {
            Person person = null;
            IOperator op = sql.Modulo.Add(() => person.Salary, () => 100m, () => value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Salary\" % @p0 % @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 100m,
                ["@p1"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataDecimal))]
        public void Add_Expression_Enumerable(decimal value)
        {
            Person person = null;
            IOperator op = sql.Modulo.Add(new List<Expression<Func<object>>> { () => person.Salary, () => 100m,
                () => value });

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Salary\" % @p0 % @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 100m,
                ["@p1"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataDecimal))]
        public void Expression(decimal value)
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => person.Salary % 100 % value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Salary\" % @p0 % @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 100m,
                ["@p1"] = value
            }, result.Parameters);
        }

        [Theory]
        [InlineData(200, 300, 400)]
        public void Expression_Large(decimal value1, int value2, long value3)
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => person.Salary % 100 % value1 % value2 % value3 % 500);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Salary\" % @p0 % @p1 % @p2 % @p3 % @p4", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 100m,
                ["@p1"] = value1,
                ["@p2"] = value2,
                ["@p3"] = value3,
                ["@p4"] = 500m
            }, result.Parameters);
        }

        [Fact]
        public void Empty_List()
        {
            IOperator op = sql.Modulo;

            Exception ex = Assert.Throws<CompileException>(() => engine.Compile(op));
            Assert.Equal("List is empty.", ex.Message);
        }

        [Fact]
        public void To_String()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Modulo
                .Add(person["Salary"])
                .Add(100m)
                .Add(200m);

            Assert.Equal("person.Salary % 100 % 200", op.ToString());
        }
    }
}