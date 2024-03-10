using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Exceptions;
using Suilder.Extensions;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Order
{
    public class OrderByTest : BuilderBaseTest
    {
        [Fact]
        public void Add()
        {
            IAlias person = sql.Alias("person");
            IOrderBy orderBy = sql.OrderBy()
                .Add(person["Name"])
                .Add(person["Surname"]);

            QueryResult result = engine.Compile(orderBy);

            Assert.Equal("ORDER BY \"person\".\"Name\", \"person\".\"Surname\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Add_With_Order()
        {
            IAlias person = sql.Alias("person");
            IOrderBy orderBy = sql.OrderBy()
                .Add(person["Name"]).Asc
                .Add(person["Surname"]).Desc;

            QueryResult result = engine.Compile(orderBy);

            Assert.Equal("ORDER BY \"person\".\"Name\" ASC, \"person\".\"Surname\" DESC", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Add_Params()
        {
            IAlias person = sql.Alias("person");
            IOrderBy orderBy = sql.OrderBy().Add(person["Name"], person["Surname"]);

            QueryResult result = engine.Compile(orderBy);

            Assert.Equal("ORDER BY \"person\".\"Name\", \"person\".\"Surname\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Add_Params_With_Order()
        {
            IAlias person = sql.Alias("person");
            IOrderBy orderBy = sql.OrderBy().Add(person["Name"], person["Surname"]).Desc;

            QueryResult result = engine.Compile(orderBy);

            Assert.Equal("ORDER BY \"person\".\"Name\" DESC, \"person\".\"Surname\" DESC", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Add_Enumerable()
        {
            IAlias person = sql.Alias("person");
            IOrderBy orderBy = sql.OrderBy().Add(new List<object> { person["Name"], person["Surname"] });

            QueryResult result = engine.Compile(orderBy);

            Assert.Equal("ORDER BY \"person\".\"Name\", \"person\".\"Surname\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Add_Enumerable_With_Order()
        {
            IAlias person = sql.Alias("person");
            IOrderBy orderBy = sql.OrderBy().Add(new List<object> { person["Name"], person["Surname"] }).Desc;

            QueryResult result = engine.Compile(orderBy);

            Assert.Equal("ORDER BY \"person\".\"Name\" DESC, \"person\".\"Surname\" DESC", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Add_Expression()
        {
            Person person = null;
            IOrderBy orderBy = sql.OrderBy()
                .Add(() => person.Name)
                .Add(() => person.Surname);

            QueryResult result = engine.Compile(orderBy);

            Assert.Equal("ORDER BY \"person\".\"Name\", \"person\".\"Surname\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Add_Expression_With_Order()
        {
            Person person = null;
            IOrderBy orderBy = sql.OrderBy()
                .Add(() => person.Name).Asc
                .Add(() => person.Surname).Desc;

            QueryResult result = engine.Compile(orderBy);

            Assert.Equal("ORDER BY \"person\".\"Name\" ASC, \"person\".\"Surname\" DESC", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Add_Expression_Params()
        {
            Person person = null;
            IOrderBy orderBy = sql.OrderBy().Add(() => person.Name, () => person.Surname);

            QueryResult result = engine.Compile(orderBy);

            Assert.Equal("ORDER BY \"person\".\"Name\", \"person\".\"Surname\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Add_Expression_Params_With_Order()
        {
            Person person = null;
            IOrderBy orderBy = sql.OrderBy().Add(() => person.Name, () => person.Surname).Desc;

            QueryResult result = engine.Compile(orderBy);

            Assert.Equal("ORDER BY \"person\".\"Name\" DESC, \"person\".\"Surname\" DESC", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Add_Expression_Enumerable()
        {
            Person person = null;
            IOrderBy orderBy = sql.OrderBy().Add(new List<Expression<Func<object>>> { () => person.Name,
                () => person.Surname });

            QueryResult result = engine.Compile(orderBy);

            Assert.Equal("ORDER BY \"person\".\"Name\", \"person\".\"Surname\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }


        [Fact]
        public void Add_Expression_Enumerable_With_Order()
        {
            Person person = null;
            IOrderBy orderBy = sql.OrderBy().Add(new List<Expression<Func<object>>> { () => person.Name,
                () => person.Surname }).Desc;

            QueryResult result = engine.Compile(orderBy);

            Assert.Equal("ORDER BY \"person\".\"Name\" DESC, \"person\".\"Surname\" DESC", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Add_One_Value()
        {
            IAlias person = sql.Alias("person");
            IOrderBy orderBy = sql.OrderBy().Add(person["Name"]);

            QueryResult result = engine.Compile(orderBy);

            Assert.Equal("ORDER BY \"person\".\"Name\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Add_One_Value_With_Order()
        {
            IAlias person = sql.Alias("person");
            IOrderBy orderBy = sql.OrderBy().Add(person["Name"]).Asc;

            QueryResult result = engine.Compile(orderBy);

            Assert.Equal("ORDER BY \"person\".\"Name\" ASC", result.Sql);
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
        public void Order_Not_Column()
        {
            IAlias person = sql.Alias("person");
            IOrderBy orderBy = sql.OrderBy()
                .Add(sql.Case()
                    .When(person["Name"].IsNotNull(), person["Name"])
                    .Else(person["Surname"])).Desc;

            QueryResult result = engine.Compile(orderBy);

            Assert.Equal("ORDER BY CASE WHEN \"person\".\"Name\" IS NOT NULL THEN \"person\".\"Name\" "
                + "ELSE \"person\".\"Surname\" END DESC", result.Sql);
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
        public void Count_List()
        {
            IAlias person = sql.Alias("person");
            IOrderBy orderBy = sql.OrderBy();
            object[] values = new object[] { person["Name"], person["Surname"], person["Id"] };

            int i = 0;
            Assert.Equal(i, orderBy.Count);

            foreach (object value in values)
            {
                orderBy.Add(value);
                Assert.Equal(++i, orderBy.Count);
            }
        }

        [Fact]
        public void Empty_List()
        {
            IOrderBy orderBy = sql.OrderBy();

            Exception ex = Assert.Throws<CompileException>(() => engine.Compile(orderBy));
            Assert.Equal("List is empty.", ex.Message);
        }

        [Fact]
        public void To_String()
        {
            IAlias person = sql.Alias("person");
            IOrderBy orderBy = sql.OrderBy()
                .Add(person["Name"])
                .Add(person["Surname"]);

            Assert.Equal("ORDER BY person.Name, person.Surname", orderBy.ToString());
        }

        [Fact]
        public void To_String_With_Order()
        {
            IAlias person = sql.Alias("person");
            IOrderBy orderBy = sql.OrderBy()
                .Add(person["Name"]).Asc
                .Add(person["Surname"]).Desc;

            Assert.Equal("ORDER BY person.Name ASC, person.Surname DESC", orderBy.ToString());
        }

        [Fact]
        public void To_String_One_Value()
        {
            IAlias person = sql.Alias("person");
            IOrderBy orderBy = sql.OrderBy().Add(person["Name"]);

            Assert.Equal("ORDER BY person.Name", orderBy.ToString());
        }

        [Fact]
        public void To_String_One_Value_With_Order()
        {
            IAlias person = sql.Alias("person");
            IOrderBy orderBy = sql.OrderBy().Add(person["Name"]).Asc;

            Assert.Equal("ORDER BY person.Name ASC", orderBy.ToString());
        }

        [Fact]
        public void To_String_Empty()
        {
            IOrderBy orderBy = sql.OrderBy();

            Assert.Equal("ORDER BY", orderBy.ToString());
        }
    }
}