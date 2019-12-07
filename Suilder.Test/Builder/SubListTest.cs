using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Exceptions;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder
{
    public class SubListTest : BuilderBaseTest
    {
        [Fact]
        public void Add()
        {
            IAlias person = sql.Alias("person");
            ISubList list = sql.SubList
                .Add(person["Id"])
                .Add(1)
                .Add("text");

            QueryResult result = engine.Compile(list);

            Assert.Equal("\"person\".\"Id\", @p0, @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = "text"
            }, result.Parameters);
        }

        [Fact]
        public void Add_Params()
        {
            IAlias person = sql.Alias("person");
            ISubList list = sql.SubList.Add(person["Id"], 1, "text");

            QueryResult result = engine.Compile(list);

            Assert.Equal("\"person\".\"Id\", @p0, @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = "text"
            }, result.Parameters);
        }

        [Fact]
        public void Add_Enumerable()
        {
            IAlias person = sql.Alias("person");
            ISubList list = sql.SubList.Add(new List<object> { person["Id"], 1, "text" });

            QueryResult result = engine.Compile(list);

            Assert.Equal("\"person\".\"Id\", @p0, @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = "text"
            }, result.Parameters);
        }

        [Fact]
        public void Add_Expression()
        {
            Person person = null;
            ISubList list = sql.SubList
                .Add(() => person.Id)
                .Add(() => 1)
                .Add(() => "text");

            QueryResult result = engine.Compile(list);

            Assert.Equal("\"person\".\"Id\", @p0, @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = "text"
            }, result.Parameters);
        }

        [Fact]
        public void Add_Expression_Params()
        {
            Person person = null;
            ISubList list = sql.SubList.Add(() => person.Id, () => 1, () => "text");

            QueryResult result = engine.Compile(list);

            Assert.Equal("\"person\".\"Id\", @p0, @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = "text"
            }, result.Parameters);
        }

        [Fact]
        public void Add_Expression_Enumerable()
        {
            Person person = null;
            ISubList list = sql.SubList.Add(new List<Expression<Func<object>>> { () => person.Id, () => 1, () => "text" });

            QueryResult result = engine.Compile(list);

            Assert.Equal("\"person\".\"Id\", @p0, @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = "text"
            }, result.Parameters);
        }

        [Fact]
        public void Empty_List()
        {
            ISubList list = sql.SubList;

            Exception ex = Assert.Throws<CompileException>(() => engine.Compile(list));
            Assert.Equal("List is empty.", ex.Message);
        }

        [Fact]
        public void SubQuery()
        {
            IAlias person = sql.Alias("person");
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
        public void To_String()
        {
            IAlias person = sql.Alias("person");
            ISubList list = sql.SubList
                .Add(person["Id"])
                .Add(1)
                .Add("text");

            Assert.Equal("person.Id, 1, \"text\"", list.ToString());
        }
    }
}