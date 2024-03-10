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
    public class NotBetweenTest : BuilderBaseTest
    {
        [Theory]
        [MemberData(nameof(DataObject2))]
        public void Builder_Object(object min, object max)
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.NotBetween(person["Id"], min, max);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" NOT BETWEEN @p0 AND @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = min,
                ["@p1"] = max
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataArray2))]
        public void Builder_Object_Array<T>(T[] min, T[] max)
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.NotBetween(person["Image"], min, max);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Image\" NOT BETWEEN @p0 AND @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = min,
                ["@p1"] = max
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataList2))]
        public void Builder_Object_List<T>(List<T> min, List<T> max)
        {
            IAlias dept = sql.Alias("dept");
            IOperator op = sql.NotBetween(dept["Tags"], min, max);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"dept\".\"Tags\" NOT BETWEEN @p0 AND @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = min,
                ["@p1"] = max
            }, result.Parameters);
        }

        [Fact]
        public void Builder_Object_Column()
        {
            IAlias person = sql.Alias("person");
            IAlias dept = sql.Alias("dept");
            IOperator op = sql.NotBetween(person["DepartmentId"], dept["Id"], dept["BossId"]);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentId\" NOT BETWEEN \"dept\".\"Id\" AND \"dept\".\"BossId\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Builder_Object_Right_Null()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.NotBetween(person["Name"], null, null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" NOT BETWEEN @p0 AND @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = null,
                ["@p1"] = null
            }, result.Parameters);
        }

        [Fact]
        public void Builder_Object_Left_Null()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.NotBetween((object)null, person["Name"], person["Surname"]);

            QueryResult result = engine.Compile(op);

            Assert.Equal("@p0 NOT BETWEEN \"person\".\"Name\" AND \"person\".\"Surname\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = null
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataObject2))]
        public void Builder_Expression(object min, object max)
        {
            Person person = null;
            IOperator op = sql.NotBetween(() => person.Id, min, max);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" NOT BETWEEN @p0 AND @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = min,
                ["@p1"] = max
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataObject2))]
        public void Builder_Expression_ForeignKey(object min, object max)
        {
            Person person = null;
            IOperator op = sql.NotBetween(() => person.Department.Id, min, max);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentId\" NOT BETWEEN @p0 AND @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = min,
                ["@p1"] = max
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataObject2))]
        public void Builder_Expression_Nested(object min, object max)
        {
            Person person = null;
            IOperator op = sql.NotBetween(() => person.Address.Street, min, max);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"AddressStreet\" NOT BETWEEN @p0 AND @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = min,
                ["@p1"] = max
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataObject2))]
        public void Builder_Expression_Nested_Deep(object min, object max)
        {
            Person2 person = null;
            IOperator op = sql.NotBetween(() => person.Address.City.Country.Name, min, max);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"AddressCityCountryName\" NOT BETWEEN @p0 AND @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = min,
                ["@p1"] = max
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataArray2))]
        public void Builder_Expression_Array<T>(T[] min, T[] max)
        {
            Person person = null;
            IOperator op = sql.NotBetween(() => person.Image, min, max);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Image\" NOT BETWEEN @p0 AND @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = min,
                ["@p1"] = max
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataList2))]
        public void Builder_Expression_List<T>(List<T> min, List<T> max)
        {
            Department dept = null;
            IOperator op = sql.NotBetween(() => dept.Tags, min, max);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"dept\".\"Tags\" NOT BETWEEN @p0 AND @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = min,
                ["@p1"] = max
            }, result.Parameters);
        }

        [Fact]
        public void Builder_Expression_Column()
        {
            Person person = null;
            IAlias dept = sql.Alias("dept");
            IOperator op = sql.NotBetween(() => person.Department.Id, dept["Id"], dept["BossId"]);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentId\" NOT BETWEEN \"dept\".\"Id\" AND \"dept\".\"BossId\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Builder_Expression_Right_Null()
        {
            Person person = null;
            IOperator op = sql.NotBetween(() => person.Name, null, null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" NOT BETWEEN @p0 AND @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = null,
                ["@p1"] = null
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataObject2))]
        public void Builder_Two_Expressions(object min, object max)
        {
            Person person = null;
            IOperator op = sql.NotBetween(() => person.Id, () => min, () => max);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" NOT BETWEEN @p0 AND @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = min,
                ["@p1"] = max
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataObject2))]
        public void Builder_Two_Expressions_ForeignKey(object min, object max)
        {
            Person person = null;
            IOperator op = sql.NotBetween(() => person.Department.Id, () => min, () => max);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentId\" NOT BETWEEN @p0 AND @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = min,
                ["@p1"] = max
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataObject2))]
        public void Builder_Two_Expressions_Nested(object min, object max)
        {
            Person person = null;
            IOperator op = sql.NotBetween(() => person.Address.Street, () => min, () => max);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"AddressStreet\" NOT BETWEEN @p0 AND @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = min,
                ["@p1"] = max
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataObject2))]
        public void Builder_Two_Expressions_Nested_Deep(object min, object max)
        {
            Person2 person = null;
            IOperator op = sql.NotBetween(() => person.Address.City.Country.Name, () => min, () => max);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"AddressCityCountryName\" NOT BETWEEN @p0 AND @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = min,
                ["@p1"] = max
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataArray2))]
        public void Builder_Two_Expressions_Array<T>(T[] min, T[] max)
        {
            Person person = null;
            IOperator op = sql.NotBetween(() => person.Image, () => min, () => max);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Image\" NOT BETWEEN @p0 AND @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = min,
                ["@p1"] = max
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataList2))]
        public void Builder_Two_Expressions_List<T>(List<T> min, List<T> max)
        {
            Department dept = null;
            IOperator op = sql.NotBetween(() => dept.Tags, () => min, () => max);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"dept\".\"Tags\" NOT BETWEEN @p0 AND @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = min,
                ["@p1"] = max
            }, result.Parameters);
        }

        [Fact]
        public void Builder_Two_Expressions_Column()
        {
            Person person = null;
            Department dept = null;
            IOperator op = sql.NotBetween(() => person.Department.Id, () => dept.Id, () => dept.Boss.Id);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentId\" NOT BETWEEN \"dept\".\"Id\" AND \"dept\".\"BossId\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Builder_Two_Expressions_Right_Value_Null()
        {
            Person person = null;
            IOperator op = sql.NotBetween(() => person.Name, () => null, () => null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" NOT BETWEEN @p0 AND @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = null,
                ["@p1"] = null
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataObject2))]
        public void Extension_Object(object min, object max)
        {
            IAlias person = sql.Alias("person");
            IOperator op = person["Id"].NotBetween(min, max);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" NOT BETWEEN @p0 AND @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = min,
                ["@p1"] = max
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataArray2))]
        public void Extension_Object_Array<T>(T[] min, T[] max)
        {
            IAlias person = sql.Alias("person");
            IOperator op = person["Image"].NotBetween(min, max);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Image\" NOT BETWEEN @p0 AND @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = min,
                ["@p1"] = max
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataList2))]
        public void Extension_Object_List<T>(List<T> min, List<T> max)
        {
            IAlias dept = sql.Alias("dept");
            IOperator op = dept["Tags"].NotBetween(min, max);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"dept\".\"Tags\" NOT BETWEEN @p0 AND @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = min,
                ["@p1"] = max
            }, result.Parameters);
        }

        [Fact]
        public void Extension_Object_Null()
        {
            IAlias person = sql.Alias("person");
            IOperator op = person["Name"].NotBetween(null, null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" NOT BETWEEN @p0 AND @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = null,
                ["@p1"] = null
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataObject2))]
        public void Extension_Expression(object min, object max)
        {
            IAlias person = sql.Alias("person");
            IOperator op = person["Id"].NotBetween(() => min, () => max);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" NOT BETWEEN @p0 AND @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = min,
                ["@p1"] = max
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataArray2))]
        public void Extension_Expression_Array<T>(T[] min, T[] max)
        {
            IAlias person = sql.Alias("person");
            IOperator op = person["Image"].NotBetween(() => min, () => max);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Image\" NOT BETWEEN @p0 AND @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = min,
                ["@p1"] = max
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataList2))]
        public void Extension_Expression_List<T>(List<T> min, List<T> max)
        {
            IAlias dept = sql.Alias("dept");
            IOperator op = dept["Tags"].NotBetween(() => min, () => max);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"dept\".\"Tags\" NOT BETWEEN @p0 AND @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = min,
                ["@p1"] = max
            }, result.Parameters);
        }

        [Fact]
        public void Extension_Expression_Column()
        {
            IAlias person = sql.Alias("person");
            Department dept = null;
            IOperator op = person["DepartmentId"].NotBetween(() => dept.Id, () => dept.Boss.Id);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentId\" NOT BETWEEN \"dept\".\"Id\" AND \"dept\".\"BossId\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Extension_Expression_Null()
        {
            IAlias person = sql.Alias("person");
            IOperator op = person["Name"].NotBetween(() => null, () => null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" NOT BETWEEN @p0 AND @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = null,
                ["@p1"] = null
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataObject2))]
        public void Expression_Method(object min, object max)
        {
            Person person = null;
            IOperator op = sql.Op(() => SqlExp.NotBetween(person.Id, min, max));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" NOT BETWEEN @p0 AND @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = min,
                ["@p1"] = max
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Method_Inline_Value()
        {
            Person person = null;
            IOperator op = sql.Op(() => SqlExp.NotBetween(person.Id, 10, 20));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" NOT BETWEEN @p0 AND @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 10,
                ["@p1"] = 20
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataObject2))]
        public void Expression_Method_ForeignKey(object min, object max)
        {
            Person person = null;
            IOperator op = sql.Op(() => SqlExp.NotBetween(person.Department.Id, min, max));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentId\" NOT BETWEEN @p0 AND @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = min,
                ["@p1"] = max
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataObject2))]
        public void Expression_Method_Nested(object min, object max)
        {
            Person person = null;
            IOperator op = sql.Op(() => SqlExp.NotBetween(person.Address.Street, min, max));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"AddressStreet\" NOT BETWEEN @p0 AND @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = min,
                ["@p1"] = max
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataObject2))]
        public void Expression_Method_Nested_Deep(object min, object max)
        {
            Person2 person = null;
            IOperator op = sql.Op(() => SqlExp.NotBetween(person.Address.City.Country.Name, min, max));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"AddressCityCountryName\" NOT BETWEEN @p0 AND @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = min,
                ["@p1"] = max
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataArray2))]
        public void Expression_Method_Array<T>(T[] min, T[] max)
        {
            Person person = null;
            IOperator op = sql.Op(() => SqlExp.NotBetween(person.Image, min, max));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Image\" NOT BETWEEN @p0 AND @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = min,
                ["@p1"] = max
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Method_Array_Inline_Value()
        {
            Person person = null;
            IOperator op = sql.Op(() => SqlExp.NotBetween(person.Image, new byte[] { 1, 2, 3 }, new byte[] { 4, 5, 6 }));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Image\" NOT BETWEEN @p0 AND @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = new byte[] { 1, 2, 3 },
                ["@p1"] = new byte[] { 4, 5, 6 }
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataList2))]
        public void Expression_Method_List<T>(List<T> min, List<T> max)
        {
            Department dept = null;
            IOperator op = sql.Op(() => SqlExp.NotBetween(dept.Tags, min, max));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"dept\".\"Tags\" NOT BETWEEN @p0 AND @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = min,
                ["@p1"] = max
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Method_List_Inline_Value()
        {
            Department dept = null;
            IOperator op = sql.Op(() => SqlExp.NotBetween(dept.Tags, new List<string> { "abcd", "efgh", "ijkl" },
                new List<string> { "mnop", "qrst", "uvwx" }));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"dept\".\"Tags\" NOT BETWEEN @p0 AND @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = new List<string> { "abcd", "efgh", "ijkl" },
                ["@p1"] = new List<string> { "mnop", "qrst", "uvwx" }
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Method_Column()
        {
            Person person = null;
            Department dept = null;
            IOperator op = sql.Op(() => SqlExp.NotBetween(person.Department.Id, dept.Id, dept.Boss.Id));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentId\" NOT BETWEEN \"dept\".\"Id\" AND \"dept\".\"BossId\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Method_Null()
        {
            Person person = null;
            IOperator op = sql.Op(() => SqlExp.NotBetween(person.Name, null, null));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" NOT BETWEEN @p0 AND @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = null,
                ["@p1"] = null
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Method_SubQuery()
        {
            Person person = null;
            IRawQuery query1 = sql.RawQuery("Subquery1");
            IRawQuery query2 = sql.RawQuery("Subquery2");
            IOperator op = sql.Op(() => SqlExp.NotBetween(person.Id, query1, query2));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" NOT BETWEEN (Subquery1) AND (Subquery2)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Method_Invalid_Call()
        {
            Person person = new Person();

            Exception ex = Assert.Throws<NotSupportedException>(() => SqlExp.NotBetween(person.Id, 10, 20));
            Assert.Equal("Only for expressions.", ex.Message);
        }

        [Fact]
        public void Translation()
        {
            engine.AddOperator(OperatorName.NotBetween, "TRANSLATED1");
            engine.AddOperator(OperatorName.And, "TRANSLATED2");

            IAlias person = sql.Alias("person");
            IOperator op = sql.NotBetween(person["Id"], 10, 20);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" TRANSLATED1 @p0 TRANSLATED2 @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 10,
                ["@p1"] = 20
            }, result.Parameters);
        }

        [Fact]
        public void Translation_Function()
        {
            engine.AddOperator(OperatorName.NotBetween, "TRANSLATED1", true);
            engine.AddOperator(OperatorName.And, "TRANSLATED2", true);

            IAlias person = sql.Alias("person");
            IOperator op = sql.NotBetween(person["Id"], 10, 20);

            QueryResult result = engine.Compile(op);

            Assert.Equal("TRANSLATED1(\"person\".\"Id\", @p0, @p1)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 10,
                ["@p1"] = 20
            }, result.Parameters);
        }

        [Fact]
        public void SubOperator()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.NotBetween(sql.Eq(person["Id"], 15), sql.Gt(person["Id"], 10), sql.Lt(person["Id"], 20));

            QueryResult result = engine.Compile(op);

            Assert.Equal("(\"person\".\"Id\" = @p0) NOT BETWEEN (\"person\".\"Id\" > @p1) AND (\"person\".\"Id\" < @p2)",
                result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 15,
                ["@p1"] = 10,
                ["@p2"] = 20
            }, result.Parameters);
        }

        [Fact]
        public void SubOperator_List()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.NotBetween(sql.Add.Add(person["Salary"], 1000m), sql.Multiply.Add(person["Salary"], 2m),
                sql.Multiply.Add(person["Salary"], 3m));

            QueryResult result = engine.Compile(op);

            Assert.Equal("(\"person\".\"Salary\" + @p0) NOT BETWEEN (\"person\".\"Salary\" * @p1) "
                + "AND (\"person\".\"Salary\" * @p2)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1000m,
                ["@p1"] = 2m,
                ["@p2"] = 3m
            }, result.Parameters);
        }

        [Fact]
        public void SubQuery()
        {
            IAlias person = sql.Alias("person");
            IOperator op1 = sql.NotBetween(person["Id"], sql.RawQuery("Subquery1"), sql.RawQuery("Subquery2"));
            IOperator op2 = sql.NotBetween(sql.RawQuery("Subquery"), person["Id"], person["DepartmentId"]);
            IOperator op3 = sql.NotBetween(sql.RawQuery("Subquery1"), sql.RawQuery("Subquery2"), sql.RawQuery("Subquery3"));

            QueryResult result;

            result = engine.Compile(op1);

            Assert.Equal("\"person\".\"Id\" NOT BETWEEN (Subquery1) AND (Subquery2)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);

            result = engine.Compile(op2);

            Assert.Equal("(Subquery) NOT BETWEEN \"person\".\"Id\" AND \"person\".\"DepartmentId\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);

            result = engine.Compile(op3);

            Assert.Equal("(Subquery1) NOT BETWEEN (Subquery2) AND (Subquery3)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void To_String()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.NotBetween(person["Id"], 10, 20);

            Assert.Equal("person.Id NOT BETWEEN 10 AND 20", op.ToString());
        }

        [Fact]
        public void To_String_Array()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.NotBetween(person["Image"], new byte[] { 1, 2, 3 }, new byte[] { 4, 5, 6 });

            Assert.Equal("person.Image NOT BETWEEN [1, 2, 3] AND [4, 5, 6]", op.ToString());
        }

        [Fact]
        public void To_String_List()
        {
            IAlias dept = sql.Alias("dept");
            IOperator op = sql.NotBetween(dept["Tags"], new List<string> { "abcd", "efgh", "ijkl" },
                new string[] { "mnop", "qrst", "uvwx" });

            Assert.Equal("dept.Tags NOT BETWEEN [\"abcd\", \"efgh\", \"ijkl\"] AND [\"mnop\", \"qrst\", \"uvwx\"]",
                op.ToString());
        }

        [Fact]
        public void To_String_SubOperator()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.NotBetween(sql.Eq(person["Id"], 15), sql.Gt(person["Id"], 10), sql.Lt(person["Id"], 20));

            Assert.Equal("(person.Id = 15) NOT BETWEEN (person.Id > 10) AND (person.Id < 20)", op.ToString());
        }

        [Fact]
        public void To_String_SubOperator_List()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.NotBetween(sql.Add.Add(person["Salary"], 1000m), sql.Multiply.Add(person["Salary"], 2m),
                sql.Multiply.Add(person["Salary"], 3m));

            Assert.Equal("(person.Salary + 1000) NOT BETWEEN (person.Salary * 2) AND (person.Salary * 3)", op.ToString());
        }

        [Fact]
        public void To_String_SubQuery()
        {
            IAlias person = sql.Alias("person");
            IOperator op1 = sql.NotBetween(person["Id"], sql.RawQuery("Subquery1"), sql.RawQuery("Subquery2"));
            IOperator op2 = sql.NotBetween(sql.RawQuery("Subquery"), person["Id"], person["DepartmentId"]);
            IOperator op3 = sql.NotBetween(sql.RawQuery("Subquery1"), sql.RawQuery("Subquery2"), sql.RawQuery("Subquery3"));

            Assert.Equal("person.Id NOT BETWEEN (Subquery1) AND (Subquery2)", op1.ToString());
            Assert.Equal("(Subquery) NOT BETWEEN person.Id AND person.DepartmentId", op2.ToString());
            Assert.Equal("(Subquery1) NOT BETWEEN (Subquery2) AND (Subquery3)", op3.ToString());
        }
    }
}