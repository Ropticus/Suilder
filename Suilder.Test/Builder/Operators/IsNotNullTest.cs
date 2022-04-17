using System;
using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Extensions;
using Suilder.Functions;
using Suilder.Operators;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Operators
{
    public class IsNotNullTest : BuilderBaseTest
    {
        [Fact]
        public void Builder_Object()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.IsNotNull(person["Name"]);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" IS NOT NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Builder_Object_Right_Null()
        {
            IOperator op = sql.IsNotNull(null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("@p0 IS NOT NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = null
            }, result.Parameters);
        }

        [Fact]
        public void Builder_Expression()
        {
            Person person = null;
            IOperator op = sql.IsNotNull(() => person.Name);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" IS NOT NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Builder_Expression_ForeignKey()
        {
            Person person = null;
            IOperator op = sql.IsNotNull(() => person.Department.Id);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentId\" IS NOT NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Builder_Expression_Nested()
        {
            Person person = null;
            IOperator op = sql.IsNotNull(() => person.Address.Street);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"AddressStreet\" IS NOT NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Builder_Expression_Nested_Deep()
        {
            Person2 person = null;
            IOperator op = sql.IsNotNull(() => person.Address.City.Country.Name);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"AddressCityCountryName\" IS NOT NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Extension_Object()
        {
            IAlias person = sql.Alias("person");
            IOperator op = person["Name"].IsNotNull();

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" IS NOT NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression()
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Name != null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" IS NOT NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_ForeignKey()
        {
            Person2 person = null;
            IOperator op = sql.Op(() => person.Department.Guid != null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentGuid\" IS NOT NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Nested()
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Address.Street != null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"AddressStreet\" IS NOT NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Nested_Deep()
        {
            Person2 person = null;
            IOperator op = sql.Op(() => person.Address.City.Country.Name != null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"AddressCityCountryName\" IS NOT NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Method()
        {
            Person person = null;
            IOperator op = sql.Op(() => SqlExp.IsNotNull(person.Name));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" IS NOT NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Method_ForeignKey()
        {
            Person person = null;
            IOperator op = sql.Op(() => SqlExp.IsNotNull(person.Department.Id));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentId\" IS NOT NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Method_Nested()
        {
            Person person = null;
            IOperator op = sql.Op(() => SqlExp.IsNotNull(person.Address.Street));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"AddressStreet\" IS NOT NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Method_Nested_Deep()
        {
            Person2 person = null;
            IOperator op = sql.Op(() => SqlExp.IsNotNull(person.Address.City.Country.Name));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"AddressCityCountryName\" IS NOT NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Method_Invalid_Call()
        {
            Person person = new Person();

            Exception ex = Assert.Throws<NotSupportedException>(() => SqlExp.IsNotNull(person.Name));
            Assert.Equal("Only for expressions.", ex.Message);
        }

        [Fact]
        public void Expression_Val_Method()
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => person.Name != null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" IS NOT NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Translation()
        {
            engine.AddOperator(OperatorName.IsNotNull, "TRANSLATED");

            IAlias person = sql.Alias("person");
            IOperator op = sql.IsNotNull(person["Name"]);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" TRANSLATED", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Translation_Function()
        {
            engine.AddOperator(OperatorName.IsNotNull, "TRANSLATED", true);

            IAlias person = sql.Alias("person");
            IOperator op = sql.IsNotNull(person["Name"]);

            QueryResult result = engine.Compile(op);

            Assert.Equal("TRANSLATED(\"person\".\"Name\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void SubOperator()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.IsNotNull(sql.Gt(person["Id"], 10));

            QueryResult result = engine.Compile(op);

            Assert.Equal("(\"person\".\"Id\" > @p0) IS NOT NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 10
            }, result.Parameters);
        }

        [Fact]
        public void SubOperator_List()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.IsNotNull(sql.Add.Add(person["Salary"], 1000m));

            QueryResult result = engine.Compile(op);

            Assert.Equal("(\"person\".\"Salary\" + @p0) IS NOT NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1000m
            }, result.Parameters);
        }

        [Fact]
        public void SubQuery()
        {
            IOperator op = sql.IsNotNull(sql.RawQuery("Subquery"));

            QueryResult result = engine.Compile(op);

            Assert.Equal("(Subquery) IS NOT NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void To_String()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.IsNotNull(person["Name"]);

            Assert.Equal("person.Name IS NOT NULL", op.ToString());
        }

        [Fact]
        public void To_String_SubOperator()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.IsNotNull(sql.Gt(person["Id"], 10));

            Assert.Equal("(person.Id > 10) IS NOT NULL", op.ToString());
        }

        [Fact]
        public void To_String_SubOperator_List()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.IsNotNull(sql.Add.Add(person["Salary"], 1000m));

            Assert.Equal("(person.Salary + 1000) IS NOT NULL", op.ToString());
        }

        [Fact]
        public void To_String_SubQuery()
        {
            IOperator op = sql.IsNotNull(sql.RawQuery("Subquery"));

            Assert.Equal("(Subquery) IS NOT NULL", op.ToString());
        }
    }
}