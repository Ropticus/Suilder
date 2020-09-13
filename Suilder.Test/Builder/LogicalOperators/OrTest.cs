using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Exceptions;
using Suilder.Extensions;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.LogicalOperators
{
    public class OrTest : BuilderBaseTest
    {
        [Fact]
        public void Add()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Or
                .Add(person["Id"].Eq(1))
                .Add(person["Active"].Eq(true))
                .Add(person["Name"].Like("%abcd%"));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" = @p0 OR \"person\".\"Active\" = @p1 OR \"person\".\"Name\" LIKE @p2",
                result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = true,
                ["@p2"] = "%abcd%"
            }, result.Parameters);
        }

        [Fact]
        public void Add_Params()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Or.Add(person["Id"].Eq(1), person["Active"].Eq(true), person["Name"].Like("%abcd%"));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" = @p0 OR \"person\".\"Active\" = @p1 OR \"person\".\"Name\" LIKE @p2",
                result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = true,
                ["@p2"] = "%abcd%"
            }, result.Parameters);
        }

        [Fact]
        public void Add_Enumerable()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Or.Add(new List<IQueryFragment> { person["Id"].Eq(1), person["Active"].Eq(true),
                person["Name"].Like("%abcd%") });

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" = @p0 OR \"person\".\"Active\" = @p1 OR \"person\".\"Name\" LIKE @p2",
                result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = true,
                ["@p2"] = "%abcd%"
            }, result.Parameters);
        }

        [Fact]
        public void Add_Expression()
        {
            Person person = null;
            IOperator op = sql.Or
                .Add(() => person.Id == 1)
                .Add(() => person.Active)
                .Add(() => person.Name.Like("%abcd%"));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" = @p0 OR \"person\".\"Active\" = @p1 OR \"person\".\"Name\" LIKE @p2",
                result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = true,
                ["@p2"] = "%abcd%"
            }, result.Parameters);
        }

        [Fact]
        public void Add_Expression_Params()
        {
            Person person = null;
            IOperator op = sql.Or.Add(() => person.Id == 1, () => person.Active, () => person.Name.Like("%abcd%"));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" = @p0 OR \"person\".\"Active\" = @p1 OR \"person\".\"Name\" LIKE @p2",
                result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = true,
                ["@p2"] = "%abcd%"
            }, result.Parameters);
        }

        [Fact]
        public void Add_Expression_Enumerable()
        {
            Person person = null;
            IOperator op = sql.Or.Add(new List<Expression<Func<bool>>> { () => person.Id == 1, () => person.Active,
                () => person.Name.Like("%abcd%")});

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" = @p0 OR \"person\".\"Active\" = @p1 OR \"person\".\"Name\" LIKE @p2",
                result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = true,
                ["@p2"] = "%abcd%"
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Or()
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Id == 1 | person.Active | person.Name.Like("%abcd%"));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" = @p0 OR \"person\".\"Active\" = @p1 OR \"person\".\"Name\" LIKE @p2",
                result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = true,
                ["@p2"] = "%abcd%"
            }, result.Parameters);
        }

        [Fact]
        public void Expression_OrElse()
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Id == 1 || person.Active || person.Name.Like("%abcd%"));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" = @p0 OR \"person\".\"Active\" = @p1 OR \"person\".\"Name\" LIKE @p2",
                result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = true,
                ["@p2"] = "%abcd%"
            }, result.Parameters);
        }

        [Theory]
        [InlineData(1000, 10, "efgh")]
        public void Expression_Large(decimal value1, int value2, string value3)
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Id == 1 || person.Active || person.Name.Like("%abcd%")
                || person.Salary > value1 || person.Address.Number == value2 || person.Address.City == value3);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" = @p0 OR \"person\".\"Active\" = @p1 OR \"person\".\"Name\" LIKE @p2 "
                + "OR \"person\".\"Salary\" > @p3 OR \"person\".\"AddressNumber\" = @p4 "
                + "OR \"person\".\"AddressCity\" = @p5", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = true,
                ["@p2"] = "%abcd%",
                ["@p3"] = value1,
                ["@p4"] = value2,
                ["@p5"] = value3
            }, result.Parameters);
        }

        [Fact]
        public void Empty_List()
        {
            IOperator op = sql.Or;

            Exception ex = Assert.Throws<CompileException>(() => engine.Compile(op));
            Assert.Equal("List is empty.", ex.Message);
        }

        [Fact]
        public void To_String()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Or
                .Add(person["Id"].Eq(1))
                .Add(person["Active"].Eq(true))
                .Add(person["Name"].Like("%abcd%"));

            Assert.Equal("person.Id = 1 OR person.Active = true OR person.Name LIKE \"%abcd%\"", op.ToString());
        }
    }
}