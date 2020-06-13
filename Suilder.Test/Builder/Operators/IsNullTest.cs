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
    public class IsNullTest : BuilderBaseTest
    {
        [Fact]
        public void Builder_Object()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.IsNull(person["Name"]);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" IS NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Builder_Object_Right_Null()
        {
            IOperator op = sql.IsNull(null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("NULL IS NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Builder_Expression()
        {
            Person person = null;
            IOperator op = sql.IsNull(() => person.Name);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" IS NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Builder_Expression_ForeignKey()
        {
            Person person = null;
            IOperator op = sql.IsNull(() => person.Department.Id);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentId\" IS NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Builder_Expression_Nested()
        {
            Person person = null;
            IOperator op = sql.IsNull(() => person.Address.Street);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"AddressStreet\" IS NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Builder_Expression_Nested_Deep()
        {
            Person2 person = null;
            IOperator op = sql.IsNull(() => person.Address.City.Country.Name);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"AddressCityCountryName\" IS NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Extension_Object()
        {
            IAlias person = sql.Alias("person");
            IOperator op = person["Name"].IsNull();

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" IS NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression()
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Name == null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" IS NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_ForeignKey()
        {
            Person2 person = null;
            IOperator op = sql.Op(() => person.Department.Guid == null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentGuid\" IS NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Nested()
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Address.Street == null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"AddressStreet\" IS NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Nested_Deep()
        {
            Person2 person = null;
            IOperator op = sql.Op(() => person.Address.City.Country.Name == null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"AddressCityCountryName\" IS NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Method()
        {
            Person person = null;
            IOperator op = sql.Op(() => SqlExp.IsNull(person.Name));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" IS NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Method_ForeignKey()
        {
            Person person = null;
            IOperator op = sql.Op(() => SqlExp.IsNull(person.Department.Id));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentId\" IS NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Method_Nested()
        {
            Person person = null;
            IOperator op = sql.Op(() => SqlExp.IsNull(person.Address.Street));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"AddressStreet\" IS NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Method_Nested_Deep()
        {
            Person2 person = null;
            IOperator op = sql.Op(() => SqlExp.IsNull(person.Address.City.Country.Name));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"AddressCityCountryName\" IS NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Method_Invalid_Call()
        {
            Person person = new Person();

            Exception ex = Assert.Throws<InvalidOperationException>(() => SqlExp.IsNull(person.Name));
            Assert.Equal("Only for expressions.", ex.Message);
        }

        [Fact]
        public void To_String()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.IsNull(person["Name"]);

            Assert.Equal("person.Name IS NULL", op.ToString());
        }
    }
}