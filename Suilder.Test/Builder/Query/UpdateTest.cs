using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Extensions;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Query
{
    public class UpdateTest : BaseTest
    {
        [Fact]
        public void Update()
        {
            IQuery query = sql.Query.Update();

            QueryResult result = engine.Compile(query);

            Assert.Equal("UPDATE", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Update_Set_Without_TableName()
        {
            IAlias person = sql.Alias("person");
            IQuery query = sql.Query.Update()
                .Set(person["Name"], "SomeName")
                .From(person);

            QueryResult result = engine.Compile(query);

            Assert.Equal("UPDATE \"person\" SET \"Name\" = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = "SomeName" }, result.Parameters);
        }

        [Fact]
        public void Update_Set_With_TableName()
        {
            engine.Options.UpdateSetWithTableName = true;

            IAlias person = sql.Alias("person");
            IQuery query = sql.Query.Update()
                .Set(person["Name"], "SomeName")
                .From(person);

            QueryResult result = engine.Compile(query);

            Assert.Equal("UPDATE \"person\" SET \"person\".\"Name\" = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = "SomeName" }, result.Parameters);
        }

        [Fact]
        public void Update_With_From()
        {
            engine.Options.UpdateWithFrom = true;

            IAlias person = sql.Alias("person");
            IQuery query = sql.Query.Update()
                .Set(person["Name"], "SomeName")
                .From(person);

            QueryResult result = engine.Compile(query);

            Assert.Equal("UPDATE \"person\" SET \"Name\" = @p0 FROM \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = "SomeName" }, result.Parameters);
        }

        [Fact]
        public void Update_With_Join()
        {
            engine.Options.UpdateSetWithTableName = true;

            IAlias person = sql.Alias("person");
            IAlias dept = sql.Alias("dept");
            IQuery query = sql.Query.Update()
                .Set(person["Name"], "SomeName")
                .From(person)
                .Join(dept)
                    .On(dept["Id"].Eq(person["DepartmentId"]));

            QueryResult result = engine.Compile(query);

            Assert.Equal("UPDATE \"person\" INNER JOIN \"dept\" ON \"dept\".\"Id\" = \"person\".\"DepartmentId\" "
                + "SET \"person\".\"Name\" = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = "SomeName" }, result.Parameters);
        }

        [Fact]
        public void Update_With_From_Join()
        {
            engine.Options.UpdateWithFrom = true;

            IAlias person = sql.Alias("person");
            IAlias dept = sql.Alias("dept");
            IQuery query = sql.Query.Update()
                .Set(person["Name"], "SomeName")
                .From(person)
                .Join(dept)
                    .On(dept["Id"].Eq(person["DepartmentId"]));

            QueryResult result = engine.Compile(query);

            Assert.Equal("UPDATE \"person\" SET \"Name\" = @p0 FROM \"person\" INNER JOIN \"dept\" ON "
                + "\"dept\".\"Id\" = \"person\".\"DepartmentId\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = "SomeName" }, result.Parameters);
        }

        [Fact]
        public void Update_Set_Table_Join()
        {
            engine.Options.UpdateSetWithTableName = true;

            IAlias person = sql.Alias("person");
            IAlias dept = sql.Alias("dept");
            IQuery query = sql.Query.Update()
                .Set(dept["Name"], "SomeName")
                .From(person)
                .Join(dept)
                    .On(dept["Id"].Eq(person["DepartmentId"]));

            QueryResult result = engine.Compile(query);

            Assert.Equal("UPDATE \"person\" INNER JOIN \"dept\" ON \"dept\".\"Id\" = \"person\".\"DepartmentId\" "
                + "SET \"dept\".\"Name\" = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = "SomeName" }, result.Parameters);
        }

        [Fact]
        public void Update_With_From_Set_Table_Join()
        {
            engine.Options.UpdateWithFrom = true;

            IAlias person = sql.Alias("person");
            IAlias dept = sql.Alias("dept");
            IQuery query = sql.Query.Update()
                .Set(dept["Name"], "SomeName")
                .From(person)
                .Join(dept)
                    .On(dept["Id"].Eq(person["DepartmentId"]));

            QueryResult result = engine.Compile(query);

            Assert.Equal("UPDATE \"dept\" SET \"Name\" = @p0 FROM \"person\" INNER JOIN \"dept\" ON "
                + "\"dept\".\"Id\" = \"person\".\"DepartmentId\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = "SomeName" }, result.Parameters);
        }

        [Fact]
        public void Update_Multiple()
        {
            engine.Options.UpdateSetWithTableName = true;

            IAlias person = sql.Alias("person");
            IAlias dept = sql.Alias("dept");
            IQuery query = sql.Query.Update()
                .Set(person["Name"], "SomeName")
                .Set(dept["Name"], "SomeName2")
                .From(person)
                .Join(dept)
                    .On(dept["Id"].Eq(person["DepartmentId"]));

            QueryResult result = engine.Compile(query);

            Assert.Equal("UPDATE \"person\" INNER JOIN \"dept\" ON \"dept\".\"Id\" = \"person\".\"DepartmentId\" "
                + "SET \"person\".\"Name\" = @p0, \"dept\".\"Name\" = @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = "SomeName", ["@p1"] = "SomeName2" }, result.Parameters);
        }

        [Fact]
        public void Update_Expression_Set_Without_TableName()
        {
            Person person = null;
            IQuery query = sql.Query.Update()
                .Set(() => person.Name, "SomeName")
                .From(() => person);

            QueryResult result = engine.Compile(query);

            Assert.Equal("UPDATE \"Person\" AS \"person\" SET \"Name\" = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = "SomeName" }, result.Parameters);
        }

        [Fact]
        public void Update_Expression_Set_With_TableName()
        {
            engine.Options.UpdateSetWithTableName = true;

            Person person = null;
            IQuery query = sql.Query.Update()
                .Set(() => person.Name, "SomeName")
                .From(() => person);

            QueryResult result = engine.Compile(query);

            Assert.Equal("UPDATE \"Person\" AS \"person\" SET \"person\".\"Name\" = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = "SomeName" }, result.Parameters);
        }

        [Fact]
        public void Update_Expression_With_From()
        {
            engine.Options.UpdateWithFrom = true;

            Person person = null;
            IQuery query = sql.Query.Update()
                .Set(() => person.Name, "SomeName")
                .From(() => person);

            QueryResult result = engine.Compile(query);

            Assert.Equal("UPDATE \"person\" SET \"Name\" = @p0 FROM \"Person\" AS \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = "SomeName" }, result.Parameters);
        }

        [Fact]
        public void Update_Expression_With_Join()
        {
            engine.Options.UpdateSetWithTableName = true;

            Person person = null;
            Department dept = null;
            IQuery query = sql.Query.Update()
                .Set(() => person.Name, "SomeName")
                .From(() => person)
                .Join(() => dept)
                    .On(() => dept.Id == person.Department.Id);

            QueryResult result = engine.Compile(query);

            Assert.Equal("UPDATE \"Person\" AS \"person\" INNER JOIN \"Dept\" AS \"dept\" "
                + "ON \"dept\".\"Id\" = \"person\".\"DepartmentId\" SET \"person\".\"Name\" = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = "SomeName" }, result.Parameters);
        }

        [Fact]
        public void Update_Expression_With_From_Join()
        {
            engine.Options.UpdateWithFrom = true;

            Person person = null;
            Department dept = null;
            IQuery query = sql.Query.Update()
                .Set(() => person.Name, "SomeName")
                .From(() => person)
                .Join(() => dept)
                    .On(() => dept.Id == person.Department.Id);

            QueryResult result = engine.Compile(query);

            Assert.Equal("UPDATE \"person\" SET \"Name\" = @p0 FROM \"Person\" AS \"person\" INNER JOIN "
                + "\"Dept\" AS \"dept\" ON \"dept\".\"Id\" = \"person\".\"DepartmentId\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = "SomeName" }, result.Parameters);
        }

        [Fact]
        public void Update_Expression_Set_Table_Join()
        {
            engine.Options.UpdateSetWithTableName = true;

            Person person = null;
            Department dept = null;
            IQuery query = sql.Query.Update()
                .Set(() => dept.Name, "SomeName")
                .From(() => person)
                .Join(() => dept)
                    .On(() => dept.Id == person.Department.Id);

            QueryResult result = engine.Compile(query);

            Assert.Equal("UPDATE \"Person\" AS \"person\" INNER JOIN \"Dept\" AS \"dept\" "
                + "ON \"dept\".\"Id\" = \"person\".\"DepartmentId\" SET \"dept\".\"Name\" = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = "SomeName" }, result.Parameters);
        }

        [Fact]
        public void Update_Expression_With_From_Set_Table_Join()
        {
            engine.Options.UpdateWithFrom = true;

            Person person = null;
            Department dept = null;
            IQuery query = sql.Query.Update()
                .Set(() => dept.Name, "SomeName")
                .From(() => person)
                .Join(() => dept)
                    .On(() => dept.Id == person.Department.Id);

            QueryResult result = engine.Compile(query);

            Assert.Equal("UPDATE \"dept\" SET \"Name\" = @p0 FROM \"Person\" AS \"person\" INNER JOIN "
                + "\"Dept\" AS \"dept\" ON \"dept\".\"Id\" = \"person\".\"DepartmentId\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = "SomeName" }, result.Parameters);
        }

        [Fact]
        public void Update_Expression_Multiple()
        {
            engine.Options.UpdateSetWithTableName = true;

            Person person = null;
            Department dept = null;
            IQuery query = sql.Query.Update()
                .Set(() => person.Name, "SomeName")
                .Set(() => dept.Name, "SomeName2")
                .From(() => person)
                .Join(() => dept)
                    .On(() => dept.Id == person.Department.Id);

            QueryResult result = engine.Compile(query);

            Assert.Equal("UPDATE \"Person\" AS \"person\" INNER JOIN \"Dept\" AS \"dept\" "
                + "ON \"dept\".\"Id\" = \"person\".\"DepartmentId\" SET \"person\".\"Name\" = @p0, "
                + "\"dept\".\"Name\" = @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = "SomeName", ["@p1"] = "SomeName2" }, result.Parameters);
        }

        [Fact]
        public void Update_Func()
        {
            IQuery query = sql.Query.Update(x => x.Top(10));

            QueryResult result = engine.Compile(query);

            Assert.Equal("UPDATE TOP(@p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = 10 }, result.Parameters);
        }

        [Fact]
        public void Update_Value()
        {
            IQuery query = sql.Query.Update(sql.Update().Top(10));

            QueryResult result = engine.Compile(query);

            Assert.Equal("UPDATE TOP(@p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = 10 }, result.Parameters);
        }

        [Fact]
        public void Update_Raw()
        {
            IQuery query = sql.Query.Update(sql.Raw("UPDATE TOP({0})", 10));

            QueryResult result = engine.Compile(query);

            Assert.Equal("UPDATE TOP(@p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = 10 }, result.Parameters);
        }
    }
}