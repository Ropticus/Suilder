using System;
using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Exceptions;
using Suilder.Extensions;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Query
{
    public class DeleteTest : BuilderBaseTest
    {
        [Fact]
        public void Delete()
        {
            IQuery query = sql.Query.Delete();

            QueryResult result = engine.Compile(query);

            Assert.Equal("DELETE", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Delete_Without_Alias()
        {
            IAlias person = sql.Alias("person");
            IQuery query = sql.Query.Delete().From(person);

            QueryResult result = engine.Compile(query);

            Assert.Equal("DELETE FROM \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Delete_With_Alias()
        {
            engine.Options.DeleteWithAlias = true;

            IAlias person = sql.Alias("person");
            IQuery query = sql.Query.Delete().From(person);

            QueryResult result = engine.Compile(query);

            Assert.Equal("DELETE \"person\" FROM \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Delete_With_Join()
        {
            engine.Options.DeleteWithAlias = true;

            IAlias person = sql.Alias("person");
            IAlias dept = sql.Alias("dept");
            IQuery query = sql.Query.Delete()
                .From(person)
                .Join(dept)
                    .On(dept["Id"].Eq(person["DepartmentId"]));

            QueryResult result = engine.Compile(query);

            Assert.Equal("DELETE \"person\" FROM \"person\" INNER JOIN \"dept\" ON \"dept\".\"Id\" = "
                + "\"person\".\"DepartmentId\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Delete_Table_From()
        {
            IAlias person = sql.Alias("person");
            IAlias dept = sql.Alias("dept");
            IQuery query = sql.Query.Delete(x => x.Add(person))
                .From(person)
                .Join(dept)
                    .On(dept["Id"].Eq(person["DepartmentId"]));

            QueryResult result = engine.Compile(query);

            Assert.Equal("DELETE \"person\" FROM \"person\" INNER JOIN \"dept\" ON \"dept\".\"Id\" = "
                + "\"person\".\"DepartmentId\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Delete_Table_Join()
        {
            IAlias person = sql.Alias("person");
            IAlias dept = sql.Alias("dept");
            IQuery query = sql.Query.Delete(x => x.Add(dept))
                .From(person)
                .Join(dept)
                    .On(dept["Id"].Eq(person["DepartmentId"]));

            QueryResult result = engine.Compile(query);

            Assert.Equal("DELETE \"dept\" FROM \"person\" INNER JOIN \"dept\" ON \"dept\".\"Id\" = "
                + "\"person\".\"DepartmentId\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Delete_Multiple()
        {
            IAlias person = sql.Alias("person");
            IAlias dept = sql.Alias("dept");
            IQuery query = sql.Query.Delete(x => x.Add(person, dept))
                .From(person)
                .Join(dept)
                    .On(dept["Id"].Eq(person["DepartmentId"]));

            QueryResult result = engine.Compile(query);

            Assert.Equal("DELETE \"person\", \"dept\" FROM \"person\" INNER JOIN \"dept\" ON \"dept\".\"Id\" = "
                + "\"person\".\"DepartmentId\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Delete_Expression_Without_Alias()
        {
            Person person = null;
            IQuery query = sql.Query.Delete().From(() => person);

            QueryResult result = engine.Compile(query);

            Assert.Equal("DELETE FROM \"Person\" AS \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Delete_Expression_With_Alias()
        {
            engine.Options.DeleteWithAlias = true;

            Person person = null;
            IQuery query = sql.Query.Delete().From(() => person);

            QueryResult result = engine.Compile(query);

            Assert.Equal("DELETE \"person\" FROM \"Person\" AS \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Delete_Expression_With_Join()
        {
            engine.Options.DeleteWithAlias = true;

            Person person = null;
            Department dept = null;
            IQuery query = sql.Query.Delete()
                .From(() => person)
                .Join(() => dept)
                    .On(() => dept.Id == person.Department.Id);

            QueryResult result = engine.Compile(query);

            Assert.Equal("DELETE \"person\" FROM \"Person\" AS \"person\" INNER JOIN \"Dept\" AS \"dept\" "
                + "ON \"dept\".\"Id\" = \"person\".\"DepartmentId\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Delete_Expression_Table_From()
        {
            Person person = null;
            Department dept = null;
            IQuery query = sql.Query.Delete(x => x.Add(() => person))
                .From(() => person)
                .Join(() => dept)
                    .On(() => dept.Id == person.Department.Id);

            QueryResult result = engine.Compile(query);

            Assert.Equal("DELETE \"person\" FROM \"Person\" AS \"person\" INNER JOIN \"Dept\" AS \"dept\" "
                + "ON \"dept\".\"Id\" = \"person\".\"DepartmentId\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Delete_Expression_Table_Join()
        {
            Person person = null;
            Department dept = null;
            IQuery query = sql.Query.Delete(x => x.Add(() => dept))
                .From(() => person)
                .Join(() => dept)
                    .On(() => dept.Id == person.Department.Id);

            QueryResult result = engine.Compile(query);

            Assert.Equal("DELETE \"dept\" FROM \"Person\" AS \"person\" INNER JOIN \"Dept\" AS \"dept\" "
                + "ON \"dept\".\"Id\" = \"person\".\"DepartmentId\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Delete_Expression_Multiple()
        {
            Person person = null;
            Department dept = null;
            IQuery query = sql.Query.Delete(x => x.Add(() => person, () => dept))
                .From(() => person)
                .Join(() => dept)
                    .On(() => dept.Id == person.Department.Id);

            QueryResult result = engine.Compile(query);

            Assert.Equal("DELETE \"person\", \"dept\" FROM \"Person\" AS \"person\" INNER JOIN \"Dept\" "
                + "AS \"dept\" ON \"dept\".\"Id\" = \"person\".\"DepartmentId\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Delete_Func()
        {
            IQuery query = sql.Query.Delete(x => x.Top(10));

            QueryResult result = engine.Compile(query);

            Assert.Equal("DELETE TOP(@p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 10
            }, result.Parameters);
        }

        [Fact]
        public void Delete_Value()
        {
            IQuery query = sql.Query.Delete(sql.Delete().Top(10));

            QueryResult result = engine.Compile(query);

            Assert.Equal("DELETE TOP(@p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 10
            }, result.Parameters);
        }

        [Fact]
        public void Delete_Raw()
        {
            IQuery query = sql.Query.Delete(sql.Raw("DELETE TOP({0})", 10));

            QueryResult result = engine.Compile(query);

            Assert.Equal("DELETE TOP(@p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 10
            }, result.Parameters);
        }

        [Fact]
        public void Delete_With_Alias_Invalid_From()
        {
            engine.Options.DeleteWithAlias = true;

            IAlias person = sql.Alias("person");
            IQuery query = sql.Query.Delete().From(sql.Raw("FROM {0}", person));

            Exception ex = Assert.Throws<CompileException>(() => engine.Compile(query));
            Assert.Equal($"The \"from\" value must be a \"{typeof(IFrom)}\" instance or specify the alias to delete.",
                 ex.Message);
        }

        [Fact]
        public void Delete_With_Alias_Invalid_Alias_Name()
        {
            engine.Options.DeleteWithAlias = true;

            IAlias alias = sql.Alias((string)null);
            IQuery query = sql.Query.Delete().From(sql.From(alias));

            Exception ex = Assert.Throws<CompileException>(() => engine.Compile(query));
            Assert.Equal("The \"from\" must have an alias or a table name.", ex.Message);
        }
    }
}