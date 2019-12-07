using System;
using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Extensions;
using Suilder.Functions;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Operators
{
    public class NotEqTest : BuilderBaseTest
    {
        [Fact]
        public void Builder_Object()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.NotEq(person["Id"], 1);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" <> @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1
            }, result.Parameters);
        }

        [Fact]
        public void Builder_Object_Enumerable()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.NotEq(person["Image"], new byte[] { 1, 2, 3 });

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Image\" <> @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = new byte[] { 1, 2, 3 }
            }, result.Parameters);
        }

        [Fact]
        public void Builder_Object_Right_Null()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.NotEq(person["Name"], null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" IS NOT NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Builder_Left_Value_Null()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.NotEq((object)null, person["Name"]);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" IS NOT NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Builder_Expression()
        {
            Person person = null;
            IOperator op = sql.NotEq(() => person.Id, 1);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" <> @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1
            }, result.Parameters);
        }

        [Fact]
        public void Builder_Expression_Enumerable()
        {
            Person person = null;
            IOperator op = sql.NotEq(() => person.Image, new byte[] { 1, 2, 3 });

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Image\" <> @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = new byte[] { 1, 2, 3 }
            }, result.Parameters);
        }

        [Fact]
        public void Builder_Expression_Right_Null()
        {
            Person person = null;
            IOperator op = sql.NotEq(() => person.Name, null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" IS NOT NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Builder_Two_Expressions()
        {
            Person person = null;
            Department dept = null;
            IOperator op = sql.NotEq(() => person.Department.Id, () => dept.Id);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentId\" <> \"dept\".\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Builder_Two_Expressions_Right_Enumerable()
        {
            Person person = null;
            IOperator op = sql.NotEq(() => person.Image, () => new byte[] { 1, 2, 3 });

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Image\" <> @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = new byte[] { 1, 2, 3 }
            }, result.Parameters);
        }

        [Fact]
        public void Builder_Two_Expressions_Right_Null()
        {
            Person person = null;
            IOperator op = sql.NotEq(() => person.Name, () => null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" IS NOT NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Extension_Object()
        {
            IAlias person = sql.Alias("person");
            IOperator op = person["Id"].NotEq(1);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" <> @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1
            }, result.Parameters);
        }

        [Fact]
        public void Extension_Object_Enumerable()
        {
            IAlias person = sql.Alias("person");
            IOperator op = person["Image"].NotEq(new byte[] { 1, 2, 3 });

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Image\" <> @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = new byte[] { 1, 2, 3 }
            }, result.Parameters);
        }

        [Fact]
        public void Extension_Object_Null()
        {
            IAlias person = sql.Alias("person");
            IOperator op = person["Name"].NotEq(null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" IS NOT NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Extension_Expression()
        {
            IAlias person = sql.Alias("person");
            Department dept = null;
            IOperator op = person["DepartmentId"].NotEq(() => dept.Id);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentId\" <> \"dept\".\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Extension_Expression_Enumerable()
        {
            IAlias person = sql.Alias("person");
            IOperator op = person["Image"].NotEq(() => new byte[] { 1, 2, 3 });

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Image\" <> @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = new byte[] { 1, 2, 3 }
            }, result.Parameters);
        }

        [Fact]
        public void Extension_Expression_Null()
        {
            IAlias person = sql.Alias("person");
            IOperator op = person["Name"].NotEq(() => null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" IS NOT NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression()
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Id != 1);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" <> @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Bool()
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Active != true);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Active\" <> @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = true
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Enumerable()
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Image != new byte[] { 1, 2, 3 });

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Image\" <> @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = new byte[] { 1, 2, 3 }
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Method()
        {
            Person person = null;
            IOperator op = sql.Op(() => SqlExp.NotEq(person.Id, 1));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" <> @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Method_Enumerable()
        {
            Person person = null;
            IOperator op = sql.Op(() => SqlExp.NotEq(person.Image, new byte[] { 1, 2, 3 }));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Image\" <> @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = new byte[] { 1, 2, 3 }
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Method_Null()
        {
            Person person = null;
            IOperator op = sql.Op(() => SqlExp.NotEq(person.Name, null));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" IS NOT NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Method_Invalid_Call()
        {
            Person person = new Person();

            Exception ex = Assert.Throws<InvalidOperationException>(() => SqlExp.NotEq(person.Id, 1));
            Assert.Equal("Only for expressions.", ex.Message);
        }

        [Fact]
        public void Subquery()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.NotEq(person["Id"], sql.RawQuery("Subquery"));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" <> (Subquery)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void To_String()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.NotEq(person["Id"], 1);

            Assert.Equal("person.Id <> 1", op.ToString());
        }

        [Fact]
        public void To_String_Enumerable()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.NotEq(person["Id"], new byte[] { 1, 2, 3 });

            Assert.Equal("person.Id <> [1, 2, 3]", op.ToString());
        }
    }
}