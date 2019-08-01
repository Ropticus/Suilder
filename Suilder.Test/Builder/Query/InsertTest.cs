using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Query
{
    public class InsertTest : BaseTest
    {
        [Fact]
        public void Insert_String()
        {
            IQuery query = sql.Query.Insert("person");

            QueryResult result = engine.Compile(query);

            Assert.Equal("INSERT INTO \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Insert_Alias()
        {
            IAlias person = sql.Alias("person");
            IQuery query = sql.Query.Insert(person);

            QueryResult result = engine.Compile(query);

            Assert.Equal("INSERT INTO \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Insert_Expression()
        {
            Person person = null;
            IQuery query = sql.Query.Insert(() => person);

            QueryResult result = engine.Compile(query);

            Assert.Equal("INSERT INTO \"Person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Insert_Func()
        {
            IAlias person = sql.Alias("person");
            IQuery query = sql.Query.Insert(x => x.Into(person));

            QueryResult result = engine.Compile(query);

            Assert.Equal("INSERT INTO \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Insert_Value()
        {
            IAlias person = sql.Alias("person");
            IQuery query = sql.Query.Insert(sql.Insert().Into(person));

            QueryResult result = engine.Compile(query);

            Assert.Equal("INSERT INTO \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Insert_Raw()
        {
            IAlias person = sql.Alias("person");
            IQuery query = sql.Query.Insert(sql.Raw("INSERT INTO {0}", person));

            QueryResult result = engine.Compile(query);

            Assert.Equal("INSERT INTO \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Insert_Values()
        {
            Person person = null;
            IQuery query = sql.Query.Insert(x => x.Into(() => person)
                .Add(() => person.Name, () => person.SurName))
                .Values("Name1", "SurName1");

            QueryResult result = engine.Compile(query);

            Assert.Equal("INSERT INTO \"Person\" (\"Name\", \"SurName\") VALUES (@p0, @p1)", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = "Name1", ["@p1"] = "SurName1" }, result.Parameters);
        }

        [Fact]
        public void Insert_Values_Multiple()
        {
            Person person = null;
            IQuery query = sql.Query.Insert(x => x.Into(() => person)
                .Add(() => person.Name, () => person.SurName))
                .Values("Name1", "SurName1")
                .Values("Name2", "SurName2")
                .Values("Name3", "SurName3");

            QueryResult result = engine.Compile(query);

            Assert.Equal("INSERT INTO \"Person\" (\"Name\", \"SurName\") VALUES (@p0, @p1), (@p2, @p3), (@p4, @p5)",
                result.Sql);
            Assert.Equal(new Dictionary<string, object>()
            {
                ["@p0"] = "Name1",
                ["@p1"] = "SurName1",
                ["@p2"] = "Name2",
                ["@p3"] = "SurName2",
                ["@p4"] = "Name3",
                ["@p5"] = "SurName3",
            }, result.Parameters);
        }

        [Fact]
        public void Insert_Values_List()
        {
            Person person = null;
            IQuery query = sql.Query.Insert(x => x.Into(() => person)
                .Add(() => person.Name, () => person.SurName))
                .Values(sql.ValList.Add("Name1", "SurName1"));

            QueryResult result = engine.Compile(query);

            Assert.Equal("INSERT INTO \"Person\" (\"Name\", \"SurName\") VALUES (@p0, @p1)", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = "Name1", ["@p1"] = "SurName1" }, result.Parameters);
        }

        [Fact]
        public void Insert_Select()
        {
            Person person = null;
            IQuery query = sql.Query.Insert(x => x.Into(() => person)
                .Add(() => person.Name, () => person.SurName))
                .Select(() => person.Name, () => person.SurName)
                .From(() => person)
                .Where(() => !person.Active);

            QueryResult result = engine.Compile(query);

            Assert.Equal("INSERT INTO \"Person\" (\"Name\", \"SurName\") SELECT \"person\".\"Name\", "
                + "\"person\".\"SurName\" FROM \"Person\" AS \"person\" WHERE \"person\".\"Active\" = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = false }, result.Parameters);
        }
    }
}