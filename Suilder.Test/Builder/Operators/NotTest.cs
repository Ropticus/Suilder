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
    public class NotTest : BuilderBaseTest
    {
        [Theory]
        [MemberData(nameof(DataObject))]
        public void Builder_Object(object value)
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Not(person["Id"].Eq(value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("NOT (\"person\".\"Id\" = @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataArray))]
        public void Builder_Object_Array<T>(T[] value)
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Not(person["Image"].Eq(value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("NOT (\"person\".\"Image\" = @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataList))]
        public void Builder_Object_List<T>(List<T> value)
        {
            IAlias dept = sql.Alias("dept");
            IOperator op = sql.Not(dept["Tags"].Eq(value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("NOT (\"dept\".\"Tags\" = @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Builder_Object_Column()
        {
            IAlias person = sql.Alias("person");
            IAlias dept = sql.Alias("dept");
            IOperator op = sql.Not(person["DepartmentId"].Eq(dept["Id"]));

            QueryResult result = engine.Compile(op);

            Assert.Equal("NOT (\"person\".\"DepartmentId\" = \"dept\".\"Id\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Builder_Object_Null()
        {
            IOperator op = sql.Not(null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("NOT @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = null
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataInt))]
        public void Builder_Expression(int value)
        {
            Person person = null;
            IOperator op = sql.Not(() => person.Id == value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("NOT (\"person\".\"Id\" = @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Builder_Expression_Inline_Value()
        {
            Person person = null;
            IOperator op = sql.Not(() => person.Id == 1);

            QueryResult result = engine.Compile(op);

            Assert.Equal("NOT (\"person\".\"Id\" = @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataInt))]
        public void Builder_Expression_ForeignKey(int value)
        {
            Person person = null;
            IOperator op = sql.Not(() => person.Department.Id == value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("NOT (\"person\".\"DepartmentId\" = @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataString))]
        public void Builder_Expression_Nested(string value)
        {
            Person person = null;
            IOperator op = sql.Not(() => person.Address.Street == value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("NOT (\"person\".\"AddressStreet\" = @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataString))]
        public void Builder_Expression_Nested_Deep(string value)
        {
            Person2 person = null;
            IOperator op = sql.Not(() => person.Address.City.Country.Name == value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("NOT (\"person\".\"AddressCityCountryName\" = @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataByteArray))]
        public void Builder_Expression_Array(byte[] value)
        {
            Person person = null;
            IOperator op = sql.Not(() => person.Image == value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("NOT (\"person\".\"Image\" = @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataStringList))]
        public void Builder_Expression_List(List<string> value)
        {
            Department dept = null;
            IOperator op = sql.Not(() => dept.Tags == value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("NOT (\"dept\".\"Tags\" = @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Builder_Expression_Column()
        {
            Person person = null;
            Department dept = null;
            IOperator op = sql.Not(() => person.Department.Id == dept.Id);

            QueryResult result = engine.Compile(op);

            Assert.Equal("NOT (\"person\".\"DepartmentId\" = \"dept\".\"Id\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataObject))]
        public void Extension_Object(object value)
        {
            IAlias person = sql.Alias("person");
            IOperator op = person["Id"].Eq(value).Not();

            QueryResult result = engine.Compile(op);

            Assert.Equal("NOT (\"person\".\"Id\" = @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataInt))]
        public void Expression(int value)
        {
            Person person = null;
            IOperator op = sql.Op(() => !(person.Id == value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("NOT (\"person\".\"Id\" = @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Inline_Value()
        {
            Person person = null;
            IOperator op = sql.Op(() => !(person.Id == 1));

            QueryResult result = engine.Compile(op);

            Assert.Equal("NOT (\"person\".\"Id\" = @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataInt))]
        public void Expression_ForeignKey(int value)
        {
            Person person = null;
            IOperator op = sql.Op(() => !(person.Department.Id == value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("NOT (\"person\".\"DepartmentId\" = @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataString))]
        public void Expression_Nested(string value)
        {
            Person person = null;
            IOperator op = sql.Op(() => !(person.Address.Street == value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("NOT (\"person\".\"AddressStreet\" = @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataString))]
        public void Expression_Nested_Deep(string value)
        {
            Person2 person = null;
            IOperator op = sql.Op(() => !(person.Address.City.Country.Name == value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("NOT (\"person\".\"AddressCityCountryName\" = @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataByteArray))]
        public void Expression_Array(byte[] value)
        {
            Person person = null;
            IOperator op = sql.Op(() => !(person.Image == value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("NOT (\"person\".\"Image\" = @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Array_Inline()
        {
            Person person = null;
            IOperator op = sql.Op(() => !(person.Image == new byte[] { 1, 2, 3 }));

            QueryResult result = engine.Compile(op);

            Assert.Equal("NOT (\"person\".\"Image\" = @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = new byte[] { 1, 2, 3 }
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataStringList))]
        public void Expression_List(List<string> value)
        {
            Department dept = null;
            IOperator op = sql.Op(() => !(dept.Tags == value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("NOT (\"dept\".\"Tags\" = @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Expression_List_Inline()
        {
            Department dept = null;
            IOperator op = sql.Op(() => !(dept.Tags == new List<string> { "abcd", "efgh", "ijkl" }));

            QueryResult result = engine.Compile(op);

            Assert.Equal("NOT (\"dept\".\"Tags\" = @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = new List<string> { "abcd", "efgh", "ijkl" }
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Column()
        {
            Person person = null;
            Department dept = null;
            IOperator op = sql.Op(() => !(person.Department.Id == dept.Id));

            QueryResult result = engine.Compile(op);

            Assert.Equal("NOT (\"person\".\"DepartmentId\" = \"dept\".\"Id\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Function()
        {
            Person person = null;
            IOperator op = sql.Op(() => !(person.Id == SqlExp.Max(person.Id)));

            QueryResult result = engine.Compile(op);

            Assert.Equal("NOT (\"person\".\"Id\" = MAX(\"person\".\"Id\"))", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataObject))]
        public void Expression_Operator_Method(object value)
        {
            Person person = null;
            IOperator op = sql.Op(() => !SqlExp.Eq(person.Id, value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("NOT (\"person\".\"Id\" = @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataInt))]
        public void Expression_Method(int value)
        {
            Person person = null;
            IOperator op = sql.Op(() => SqlExp.Not(person.Id == value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("NOT (\"person\".\"Id\" = @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Method_Inline_Value()
        {
            Person person = null;
            IOperator op = sql.Op(() => SqlExp.Not(person.Id == 1));

            QueryResult result = engine.Compile(op);

            Assert.Equal("NOT (\"person\".\"Id\" = @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataInt))]
        public void Expression_Method_ForeignKey(int value)
        {
            Person person = null;
            IOperator op = sql.Op(() => SqlExp.Not(person.Department.Id == value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("NOT (\"person\".\"DepartmentId\" = @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataString))]
        public void Expression_Method_Nested(string value)
        {
            Person person = null;
            IOperator op = sql.Op(() => SqlExp.Not(person.Address.Street == value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("NOT (\"person\".\"AddressStreet\" = @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataString))]
        public void Expression_Method_Nested_Deep(string value)
        {
            Person2 person = null;
            IOperator op = sql.Op(() => SqlExp.Not(person.Address.City.Country.Name == value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("NOT (\"person\".\"AddressCityCountryName\" = @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Method_Bool_Property()
        {
            Person person = null;
            IOperator op = sql.Op(() => SqlExp.Not(person.Active));

            QueryResult result = engine.Compile(op);

            Assert.Equal("NOT (\"person\".\"Active\" = @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = true
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Method_Column()
        {
            Person person = null;
            Department dept = null;
            IOperator op = sql.Op(() => SqlExp.Not(person.Department.Id == dept.Id));

            QueryResult result = engine.Compile(op);

            Assert.Equal("NOT (\"person\".\"DepartmentId\" = \"dept\".\"Id\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Method_Null()
        {
            IOperator op = sql.Op(() => SqlExp.Not(null));

            QueryResult result = engine.Compile(op);

            Assert.Equal("NOT @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = null
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Method_SubQuery()
        {
            IRawQuery query = sql.RawQuery("Subquery");
            IOperator op = sql.Op(() => SqlExp.Not(query));

            QueryResult result = engine.Compile(op);

            Assert.Equal("NOT ((Subquery) = @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = true
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Method_Invalid_Call()
        {
            Person person = new Person();

            Exception ex = Assert.Throws<NotSupportedException>(() => SqlExp.Not(person.Id == 1));
            Assert.Equal("Only for expressions.", ex.Message);
        }

        [Theory]
        [MemberData(nameof(DataInt))]
        public void Expression_Val_Method(int value)
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => !(person.Id == value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("NOT (\"person\".\"Id\" = @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void SubOperator_List()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Not(sql.And.Add(person["Id"].Gt(10), person["Id"].Lt(20)));

            QueryResult result = engine.Compile(op);

            Assert.Equal("NOT (\"person\".\"Id\" > @p0 AND \"person\".\"Id\" < @p1)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 10,
                ["@p1"] = 20
            }, result.Parameters);
        }

        [Fact]
        public void SubQuery()
        {
            IOperator op = sql.Not(sql.RawQuery("Subquery"));

            QueryResult result = engine.Compile(op);

            Assert.Equal("NOT (Subquery)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void To_String()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Not(person["Id"].Eq(1));

            Assert.Equal("NOT (person.Id = 1)", op.ToString());
        }

        [Fact]
        public void To_String_SubOperator_List()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Not(sql.And.Add(person["Id"].Gt(10), person["Id"].Lt(20)));

            Assert.Equal("NOT (person.Id > 10 AND person.Id < 20)", op.ToString());
        }

        [Fact]
        public void To_String_SubQuery()
        {
            IOperator op = sql.Not(sql.RawQuery("Subquery"));

            Assert.Equal("NOT (Subquery)", op.ToString());
        }

        private static class CustomExp
        {
            public static bool IsNumeric(object value)
            {
                throw new NotSupportedException("Only for expressions.");
            }

            public static bool Invalid(object value)
            {
                return true;
            }
        }
    }
}