using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder
{
    public class OrderByTest : BaseTest
    {
        [Fact]
        public void Add()
        {
            IAlias person = sql.Alias("person");
            IOrderBy orderBy = sql.OrderBy()
                .Add(person["Name"])
                .Add(person["SurName"]);

            QueryResult result = engine.Compile(orderBy);

            Assert.Equal("ORDER BY \"person\".\"Name\", \"person\".\"SurName\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Add_With_Order()
        {
            IAlias person = sql.Alias("person");
            IOrderBy orderBy = sql.OrderBy()
                .Add(person["Name"]).Asc
                .Add(person["SurName"]).Desc;

            QueryResult result = engine.Compile(orderBy);

            Assert.Equal("ORDER BY \"person\".\"Name\" ASC, \"person\".\"SurName\" DESC", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Add_Params()
        {
            IAlias person = sql.Alias("person");
            IOrderBy orderBy = sql.OrderBy().Add(person["Name"], person["SurName"]);

            QueryResult result = engine.Compile(orderBy);

            Assert.Equal("ORDER BY \"person\".\"Name\", \"person\".\"SurName\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Add_Params_With_Order()
        {
            IAlias person = sql.Alias("person");
            IOrderBy orderBy = sql.OrderBy().Add(person["Name"], person["SurName"]).Desc;

            QueryResult result = engine.Compile(orderBy);

            Assert.Equal("ORDER BY \"person\".\"Name\" DESC, \"person\".\"SurName\" DESC", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Add_Enumerable()
        {
            IAlias person = sql.Alias("person");
            IOrderBy orderBy = sql.OrderBy().Add(new List<object>() { person["Name"], person["SurName"] });

            QueryResult result = engine.Compile(orderBy);

            Assert.Equal("ORDER BY \"person\".\"Name\", \"person\".\"SurName\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Add_Enumerable_With_Order()
        {
            IAlias person = sql.Alias("person");
            IOrderBy orderBy = sql.OrderBy().Add(new List<object>() { person["Name"], person["SurName"] }).Desc;

            QueryResult result = engine.Compile(orderBy);

            Assert.Equal("ORDER BY \"person\".\"Name\" DESC, \"person\".\"SurName\" DESC", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Add_Expression()
        {
            Person person = null;
            IOrderBy orderBy = sql.OrderBy()
                .Add(() => person.Name)
                .Add(() => person.SurName);

            QueryResult result = engine.Compile(orderBy);

            Assert.Equal("ORDER BY \"person\".\"Name\", \"person\".\"SurName\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Add_Expression_With_Order()
        {
            Person person = null;
            IOrderBy orderBy = sql.OrderBy()
                .Add(() => person.Name).Asc
                .Add(() => person.SurName).Desc;

            QueryResult result = engine.Compile(orderBy);

            Assert.Equal("ORDER BY \"person\".\"Name\" ASC, \"person\".\"SurName\" DESC", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Add_Expression_Params()
        {
            Person person = null;
            IOrderBy orderBy = sql.OrderBy().Add(() => person.Name, () => person.SurName);

            QueryResult result = engine.Compile(orderBy);

            Assert.Equal("ORDER BY \"person\".\"Name\", \"person\".\"SurName\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Add_Expression_Params_With_Order()
        {
            Person person = null;
            IOrderBy orderBy = sql.OrderBy().Add(() => person.Name, () => person.SurName).Desc;

            QueryResult result = engine.Compile(orderBy);

            Assert.Equal("ORDER BY \"person\".\"Name\" DESC, \"person\".\"SurName\" DESC", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Add_Expression_Enumerable()
        {
            Person person = null;
            IOrderBy orderBy = sql.OrderBy().Add(new List<Expression<Func<object>>>() { () => person.Name,
                () => person.SurName });

            QueryResult result = engine.Compile(orderBy);

            Assert.Equal("ORDER BY \"person\".\"Name\", \"person\".\"SurName\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }


        [Fact]
        public void Add_Expression_Enumerable_With_Order()
        {
            Person person = null;
            IOrderBy orderBy = sql.OrderBy().Add(new List<Expression<Func<object>>>() { () => person.Name,
                () => person.SurName }).Desc;

            QueryResult result = engine.Compile(orderBy);

            Assert.Equal("ORDER BY \"person\".\"Name\" DESC, \"person\".\"SurName\" DESC", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Ascending()
        {
            IAlias person = sql.Alias("person");
            IOrderBy orderBy = sql.OrderBy().Add(person["Name"]).Asc;

            QueryResult result = engine.Compile(orderBy);

            Assert.Equal("ORDER BY \"person\".\"Name\" ASC", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Descending()
        {
            IAlias person = sql.Alias("person");
            IOrderBy orderBy = sql.OrderBy().Add(person["Name"]).Desc;

            QueryResult result = engine.Compile(orderBy);

            Assert.Equal("ORDER BY \"person\".\"Name\" DESC", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void SetOrderAsc()
        {
            IAlias person = sql.Alias("person");
            IOrderBy orderBy = sql.OrderBy().Add(person["Name"]).SetOrder(true);

            QueryResult result = engine.Compile(orderBy);

            Assert.Equal("ORDER BY \"person\".\"Name\" ASC", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void SetOrderDesc()
        {
            IAlias person = sql.Alias("person");
            IOrderBy orderBy = sql.OrderBy().Add(person["Name"]).SetOrder(false);

            QueryResult result = engine.Compile(orderBy);

            Assert.Equal("ORDER BY \"person\".\"Name\" DESC", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Invalid_Order_Empty()
        {
            Exception ex = Assert.Throws<InvalidOperationException>(() => sql.OrderBy().Asc);
            Assert.Equal("List is empty.", ex.Message);
        }

        [Fact]
        public void Invalid_Order_All()
        {
            IAlias person = sql.Alias("person");

            Exception ex = Assert.Throws<InvalidOperationException>(() => sql.OrderBy().Add(person.All).Asc);
            Assert.Equal("Cannot add order for select all column.", ex.Message);
        }

        [Fact]
        public void To_String()
        {
            IAlias person = sql.Alias("person");
            IOrderBy orderBy = sql.OrderBy()
                .Add(person["Name"]).Asc
                .Add(person["SurName"]).Desc;

            Assert.Equal("ORDER BY person.Name ASC, person.SurName DESC", orderBy.ToString());
        }
    }
}