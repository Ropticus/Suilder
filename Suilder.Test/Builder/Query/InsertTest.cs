using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Query
{
    public class InsertTest : BuilderBaseTest
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
        public void Values_One_Column()
        {
            Person person = null;
            IQuery query = sql.Query.Insert(x => x.Into(() => person)
                .Add(() => person.Name))
                .Values("Name1");

            QueryResult result = engine.Compile(query);

            Assert.Equal("INSERT INTO \"Person\" (\"Name\") VALUES (@p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "Name1"
            }, result.Parameters);
        }

        [Fact]
        public void Values_One_Column_Enumerable()
        {
            Person person = null;
            IQuery query = sql.Query.Insert(x => x.Into(() => person)
                .Add(() => person.Image))
                .Values(new byte[] { 1, 2, 3 });

            QueryResult result = engine.Compile(query);

            Assert.Equal("INSERT INTO \"Person\" (\"Image\") VALUES (@p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = new byte[] { 1, 2, 3 }
            }, result.Parameters);
        }

        [Fact]
        public void Values_Params()
        {
            Person person = null;
            IQuery query = sql.Query.Insert(x => x.Into(() => person)
                .Add(() => person.Name, () => person.Surname, () => person.Image))
                .Values("Name1", "Surname1", new byte[] { 1, 2, 3 });

            QueryResult result = engine.Compile(query);

            Assert.Equal("INSERT INTO \"Person\" (\"Name\", \"Surname\", \"Image\") VALUES (@p0, @p1, @p2)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "Name1",
                ["@p1"] = "Surname1",
                ["@p2"] = new byte[] { 1, 2, 3 }
            }, result.Parameters);
        }

        [Fact]
        public void Values_Multiple()
        {
            Person person = null;
            IQuery query = sql.Query.Insert(x => x.Into(() => person)
                .Add(() => person.Name, () => person.Surname))
                .Values("Name1", "Surname1")
                .Values("Name2", "Surname2")
                .Values("Name3", "Surname3");

            QueryResult result = engine.Compile(query);

            Assert.Equal("INSERT INTO \"Person\" (\"Name\", \"Surname\") VALUES (@p0, @p1), (@p2, @p3), (@p4, @p5)",
                result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "Name1",
                ["@p1"] = "Surname1",
                ["@p2"] = "Name2",
                ["@p3"] = "Surname2",
                ["@p4"] = "Name3",
                ["@p5"] = "Surname3"
            }, result.Parameters);
        }

        [Fact]
        public void Values_List()
        {
            Person person = null;
            IQuery query = sql.Query.Insert(x => x.Into(() => person)
                .Add(() => person.Name, () => person.Surname))
                .Values(sql.ValList.Add("Name1", "Surname1"));

            QueryResult result = engine.Compile(query);

            Assert.Equal("INSERT INTO \"Person\" (\"Name\", \"Surname\") VALUES (@p0, @p1)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "Name1",
                ["@p1"] = "Surname1"
            }, result.Parameters);
        }

        [Fact]
        public void Values_Union()
        {
            engine.Options.InsertWithUnion = true;

            Person person = null;
            IQuery query = sql.Query.Insert(x => x.Into(() => person)
                .Add(() => person.Name, () => person.Surname))
                .Values("Name1", "Surname1");

            QueryResult result = engine.Compile(query);

            Assert.Equal("INSERT INTO \"Person\" (\"Name\", \"Surname\") VALUES (@p0, @p1)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "Name1",
                ["@p1"] = "Surname1"
            }, result.Parameters);
        }

        [Fact]
        public void Values_Multiple_Union()
        {
            engine.Options.InsertWithUnion = true;

            Person person = null;
            IQuery query = sql.Query.Insert(x => x.Into(() => person)
                .Add(() => person.Name, () => person.Surname))
                .Values("Name1", "Surname1")
                .Values("Name2", "Surname2")
                .Values("Name3", "Surname3");

            QueryResult result = engine.Compile(query);

            Assert.Equal("INSERT INTO \"Person\" (\"Name\", \"Surname\") SELECT @p0, @p1 UNION ALL "
                + "SELECT @p2, @p3 UNION ALL SELECT @p4, @p5", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "Name1",
                ["@p1"] = "Surname1",
                ["@p2"] = "Name2",
                ["@p3"] = "Surname2",
                ["@p4"] = "Name3",
                ["@p5"] = "Surname3"
            }, result.Parameters);
        }

        [Fact]
        public void Values_Multiple_Union_Dummy()
        {
            engine.Options.InsertWithUnion = true;
            engine.Options.FromDummyName = "DUAL";

            Person person = null;
            IQuery query = sql.Query.Insert(x => x.Into(() => person)
                .Add(() => person.Name, () => person.Surname))
                .Values("Name1", "Surname1")
                .Values("Name2", "Surname2")
                .Values("Name3", "Surname3");

            QueryResult result = engine.Compile(query);

            Assert.Equal("INSERT INTO \"Person\" (\"Name\", \"Surname\") SELECT @p0, @p1 FROM DUAL UNION ALL "
                + "SELECT @p2, @p3 FROM DUAL UNION ALL SELECT @p4, @p5 FROM DUAL", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "Name1",
                ["@p1"] = "Surname1",
                ["@p2"] = "Name2",
                ["@p3"] = "Surname2",
                ["@p4"] = "Name3",
                ["@p5"] = "Surname3"
            }, result.Parameters);
        }

        [Fact]
        public void Insert_Select()
        {
            Person person = null;
            IQuery query = sql.Query.Insert(x => x.Into(() => person)
                .Add(() => person.Name, () => person.Surname))
                .Select(() => person.Name, () => person.Surname)
                .From(() => person)
                .Where(() => !person.Active);

            QueryResult result = engine.Compile(query);

            Assert.Equal("INSERT INTO \"Person\" (\"Name\", \"Surname\") SELECT \"person\".\"Name\", "
                + "\"person\".\"Surname\" FROM \"Person\" AS \"person\" WHERE \"person\".\"Active\" = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = false
            }, result.Parameters);
        }
    }
}