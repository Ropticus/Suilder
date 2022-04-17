using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Exceptions;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Lists
{
    public class SubListTest : BuilderBaseTest
    {
        [Theory]
        [MemberData(nameof(DataObject))]
        public void Add(object value)
        {
            IAlias person = sql.Alias("person");
            ISubList list = sql.SubList
                .Add(person["Id"])
                .Add(1)
                .Add(value);

            QueryResult result = engine.Compile(list);

            Assert.Equal("\"person\".\"Id\", @p0, @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataObject))]
        public void Add_Params(object value)
        {
            IAlias person = sql.Alias("person");
            ISubList list = sql.SubList.Add(person["Id"], 1, value);

            QueryResult result = engine.Compile(list);

            Assert.Equal("\"person\".\"Id\", @p0, @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataObject))]
        public void Add_Enumerable(object value)
        {
            IAlias person = sql.Alias("person");
            ISubList list = sql.SubList.Add(new List<object> { person["Id"], 1, value });

            QueryResult result = engine.Compile(list);

            Assert.Equal("\"person\".\"Id\", @p0, @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataObject))]
        public void Add_Expression(object value)
        {
            Person person = null;
            ISubList list = sql.SubList
                .Add(() => person.Id)
                .Add(() => 1)
                .Add(() => value);

            QueryResult result = engine.Compile(list);

            Assert.Equal("\"person\".\"Id\", @p0, @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataObject))]
        public void Add_Expression_Params(object value)
        {
            Person person = null;
            ISubList list = sql.SubList.Add(() => person.Id, () => 1, () => value);

            QueryResult result = engine.Compile(list);

            Assert.Equal("\"person\".\"Id\", @p0, @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataObject))]
        public void Add_Expression_Enumerable(object value)
        {
            Person person = null;
            ISubList list = sql.SubList.Add(new List<Expression<Func<object>>> { () => person.Id, () => 1, () => value });

            QueryResult result = engine.Compile(list);

            Assert.Equal("\"person\".\"Id\", @p0, @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Add_One_Value()
        {
            IAlias person = sql.Alias("person");
            ISubList list = sql.SubList.Add(person["Id"]);

            QueryResult result = engine.Compile(list);

            Assert.Equal("\"person\".\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void As_SubQuery()
        {
            ISubList list = sql.SubList.Add(1, 2, 3);

            QueryResult result = engine.Compile(sql.Raw("{0}", list));

            Assert.Equal("(@p0, @p1, @p2)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = 2,
                ["@p2"] = 3
            }, result.Parameters);
        }

        [Fact]
        public void SubOperator()
        {
            IAlias person = sql.Alias("person");
            ISubList list = sql.SubList.Add(sql.Gt(person["Id"], 10), sql.Lt(person["Id"], 20));

            QueryResult result = engine.Compile(list);

            Assert.Equal("\"person\".\"Id\" > @p0, \"person\".\"Id\" < @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 10,
                ["@p1"] = 20
            }, result.Parameters);
        }

        [Fact]
        public void SubOperator_List()
        {
            IAlias person = sql.Alias("person");
            ISubList list = sql.SubList.Add(sql.Add.Add(person["Salary"], 1000m), sql.Multiply.Add(person["Salary"], 2m));

            QueryResult result = engine.Compile(list);

            Assert.Equal("\"person\".\"Salary\" + @p0, \"person\".\"Salary\" * @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1000m,
                ["@p1"] = 2m
            }, result.Parameters);
        }

        [Fact]
        public void SubQuery()
        {
            ISubList list = sql.SubList.Add(sql.RawQuery("Subquery1"), sql.RawQuery("Subquery2"));

            QueryResult result = engine.Compile(list);

            Assert.Equal("(Subquery1), (Subquery2)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Count_List()
        {
            IAlias person = sql.Alias("person");
            ISubList list = sql.SubList;
            object[] values = new object[] { person["Id"], 1, "abcd" };

            int i = 0;
            Assert.Equal(i, list.Count);

            foreach (object value in values)
            {
                list.Add(value);
                Assert.Equal(++i, list.Count);
            }
        }

        [Fact]
        public void Empty_List()
        {
            ISubList list = sql.SubList;

            Exception ex = Assert.Throws<CompileException>(() => engine.Compile(list));
            Assert.Equal("List is empty.", ex.Message);
        }

        [Fact]
        public void To_String()
        {
            IAlias person = sql.Alias("person");
            ISubList list = sql.SubList
                .Add(person["Id"])
                .Add(1)
                .Add("abcd");

            Assert.Equal("person.Id, 1, \"abcd\"", list.ToString());
        }

        [Fact]
        public void To_String_One_Value()
        {
            IAlias person = sql.Alias("person");
            ISubList list = sql.SubList.Add(person["Id"]);

            Assert.Equal("person.Id", list.ToString());
        }

        [Fact]
        public void To_String_Empty()
        {
            ISubList list = sql.SubList;

            Assert.Equal("", list.ToString());
        }
    }
}