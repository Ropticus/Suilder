using System;
using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Exceptions;
using Suilder.Extensions;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Case
{
    public class CaseTest : BuilderBaseTest
    {
        [Fact]
        public void When()
        {
            IAlias person = sql.Alias("person");
            ICase caseWhen = sql.Case()
                .When(person["Name"].IsNull(), "abcd");

            QueryResult result = engine.Compile(caseWhen);

            Assert.Equal("CASE WHEN \"person\".\"Name\" IS NULL THEN @p0 END", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "abcd"
            }, result.Parameters);
        }

        [Fact]
        public void When_Then_Column()
        {
            IAlias person = sql.Alias("person");
            ICase caseWhen = sql.Case()
                .When(person["Name"].IsNotNull(), person["Name"]);

            QueryResult result = engine.Compile(caseWhen);

            Assert.Equal("CASE WHEN \"person\".\"Name\" IS NOT NULL THEN \"person\".\"Name\" END", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void When_Then_Null()
        {
            IAlias person = sql.Alias("person");
            ICase caseWhen = sql.Case()
                .When(person["Name"].IsNotNull(), null);

            QueryResult result = engine.Compile(caseWhen);

            Assert.Equal("CASE WHEN \"person\".\"Name\" IS NOT NULL THEN NULL END", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void When_Multiple()
        {
            IAlias person = sql.Alias("person");
            ICase caseWhen = sql.Case()
                .When(person["Salary"].Gt(2000m), "Greather than 2000")
                .When(person["Salary"].Gt(1000m), "Greather than 1000")
                .Else("Under 1000");

            QueryResult result = engine.Compile(caseWhen);

            Assert.Equal("CASE WHEN \"person\".\"Salary\" > @p0 THEN @p1 "
                + "WHEN \"person\".\"Salary\" > @p2 THEN @p3 ELSE @p4 END", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 2000m,
                ["@p1"] = "Greather than 2000",
                ["@p2"] = 1000m,
                ["@p3"] = "Greather than 1000",
                ["@p4"] = "Under 1000"
            }, result.Parameters);
        }

        [Fact]
        public void Else()
        {
            IAlias person = sql.Alias("person");
            ICase caseWhen = sql.Case()
                .When(person["Name"].IsNotNull(), person["Name"])
                .Else(person["SurName"]);

            QueryResult result = engine.Compile(caseWhen);

            Assert.Equal("CASE WHEN \"person\".\"Name\" IS NOT NULL THEN \"person\".\"Name\" "
                + "ELSE \"person\".\"SurName\" END", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Else_Null()
        {
            IAlias person = sql.Alias("person");
            ICase caseWhen = sql.Case()
                .When(person["Name"].IsNotNull(), person["Name"])
                .Else(null);

            QueryResult result = engine.Compile(caseWhen);

            Assert.Equal("CASE WHEN \"person\".\"Name\" IS NOT NULL THEN \"person\".\"Name\" ELSE NULL END", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void When_Expression()
        {
            Person person = null;
            ICase caseWhen = sql.Case()
                .When(() => person.Name == null, "abcd");

            QueryResult result = engine.Compile(caseWhen);

            Assert.Equal("CASE WHEN \"person\".\"Name\" IS NULL THEN @p0 END", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "abcd"
            }, result.Parameters);
        }

        [Fact]
        public void When_Expression_Then_Null()
        {
            Person person = null;
            ICase caseWhen = sql.Case()
                .When(() => person.Name != null, null);

            QueryResult result = engine.Compile(caseWhen);

            Assert.Equal("CASE WHEN \"person\".\"Name\" IS NOT NULL THEN NULL END", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void When_Two_Expressions()
        {
            Person person = null;
            ICase caseWhen = sql.Case()
                .When(() => person.Name == null, () => "abcd");

            QueryResult result = engine.Compile(caseWhen);

            Assert.Equal("CASE WHEN \"person\".\"Name\" IS NULL THEN @p0 END", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "abcd"
            }, result.Parameters);
        }

        [Fact]
        public void When_Two_Expressions_Then_Column()
        {
            Person person = null;
            ICase caseWhen = sql.Case()
                .When(() => person.Name != null, () => person.Name);

            QueryResult result = engine.Compile(caseWhen);

            Assert.Equal("CASE WHEN \"person\".\"Name\" IS NOT NULL THEN \"person\".\"Name\" END", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void When_Expression_Bool_Property()
        {
            Person person = null;
            ICase caseWhen = sql.Case()
                .When(() => person.Active, "Active")
                .Else("Inactive");

            QueryResult result = engine.Compile(caseWhen);

            Assert.Equal("CASE WHEN \"person\".\"Active\" = @p0 THEN @p1 ELSE @p2 END", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = true,
                ["@p1"] = "Active",
                ["@p2"] = "Inactive"
            }, result.Parameters);
        }

        [Fact]
        public void When_Expression_Multiple()
        {
            Person person = null;
            ICase caseWhen = sql.Case()
                .When(() => person.Salary > 2000m, "Greather than 2000")
                .When(() => person.Salary > 1000m, "Greather than 1000")
                .Else("Under 1000");

            QueryResult result = engine.Compile(caseWhen);

            Assert.Equal("CASE WHEN \"person\".\"Salary\" > @p0 THEN @p1 "
                + "WHEN \"person\".\"Salary\" > @p2 THEN @p3 ELSE @p4 END", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 2000m,
                ["@p1"] = "Greather than 2000",
                ["@p2"] = 1000m,
                ["@p3"] = "Greather than 1000",
                ["@p4"] = "Under 1000"
            }, result.Parameters);
        }

        [Fact]
        public void Else_Expression()
        {
            Person person = null;
            ICase caseWhen = sql.Case()
                .When(() => person.Name != null, () => person.Name)
                .Else(() => person.SurName);

            QueryResult result = engine.Compile(caseWhen);

            Assert.Equal("CASE WHEN \"person\".\"Name\" IS NOT NULL THEN \"person\".\"Name\" "
                + "ELSE \"person\".\"SurName\" END", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Case_Value()
        {
            IAlias person = sql.Alias("person");
            ICase caseWhen = sql.Case(person["Active"])
                .When(true, "Active");

            QueryResult result = engine.Compile(caseWhen);

            Assert.Equal("CASE \"person\".\"Active\" WHEN @p0 THEN @p1 END", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = true,
                ["@p1"] = "Active"
            }, result.Parameters);
        }

        [Fact]
        public void Case_Value_Null()
        {
            IAlias person = sql.Alias("person");
            ICase caseWhen = sql.Case(null)
                .When(person["SurName"], "abcd");

            QueryResult result = engine.Compile(caseWhen);

            Assert.Equal("CASE NULL WHEN \"person\".\"SurName\" THEN @p0 END", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "abcd"
            }, result.Parameters);
        }

        [Fact]
        public void Case_Value_When_Column()
        {
            IAlias person = sql.Alias("person");
            ICase caseWhen = sql.Case(person["Name"])
                .When(person["SurName"], "abcd");

            QueryResult result = engine.Compile(caseWhen);

            Assert.Equal("CASE \"person\".\"Name\" WHEN \"person\".\"SurName\" THEN @p0 END", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "abcd"
            }, result.Parameters);
        }

        [Fact]
        public void Case_Value_When_Null()
        {
            IAlias person = sql.Alias("person");
            ICase caseWhen = sql.Case(person["Name"])
                .When(null, "abcd");

            QueryResult result = engine.Compile(caseWhen);

            Assert.Equal("CASE \"person\".\"Name\" WHEN NULL THEN @p0 END", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "abcd"
            }, result.Parameters);
        }

        [Fact]
        public void Case_Value_Then_Column()
        {
            IAlias person = sql.Alias("person");
            ICase caseWhen = sql.Case(person["Name"])
                .When("abcd", person["Name"]);

            QueryResult result = engine.Compile(caseWhen);

            Assert.Equal("CASE \"person\".\"Name\" WHEN @p0 THEN \"person\".\"Name\" END", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "abcd"
            }, result.Parameters);
        }

        [Fact]
        public void Case_Value_Then_Null()
        {
            IAlias person = sql.Alias("person");
            ICase caseWhen = sql.Case(person["Name"])
                .When(person["SurName"], null);

            QueryResult result = engine.Compile(caseWhen);

            Assert.Equal("CASE \"person\".\"Name\" WHEN \"person\".\"SurName\" THEN NULL END", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Case_Value_Two_Columns()
        {
            IAlias person = sql.Alias("person");
            ICase caseWhen = sql.Case(person["Name"])
                .When(person["SurName"], person["Name"]);

            QueryResult result = engine.Compile(caseWhen);

            Assert.Equal("CASE \"person\".\"Name\" WHEN \"person\".\"SurName\" THEN \"person\".\"Name\" END", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Case_Value_Two_Null()
        {
            IAlias person = sql.Alias("person");
            ICase caseWhen = sql.Case(person["Name"])
                .When(null, null);

            QueryResult result = engine.Compile(caseWhen);

            Assert.Equal("CASE \"person\".\"Name\" WHEN NULL THEN NULL END", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Case_Value_When_Multiple()
        {
            IAlias person = sql.Alias("person");
            ICase caseWhen = sql.Case(person["Name"])
                .When(person["SurName"], "abcd")
                .When("efgh", person["Name"])
                .Else("ijkl");

            QueryResult result = engine.Compile(caseWhen);

            Assert.Equal("CASE \"person\".\"Name\" WHEN \"person\".\"SurName\" THEN @p0 "
                + "WHEN @p1 THEN \"person\".\"Name\" ELSE @p2 END", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "abcd",
                ["@p1"] = "efgh",
                ["@p2"] = "ijkl"
            }, result.Parameters);
        }

        [Fact]
        public void Case_Value_Expression()
        {
            Person person = null;
            ICase caseWhen = sql.Case(() => person.Active)
                .When(true, "Active");

            QueryResult result = engine.Compile(caseWhen);

            Assert.Equal("CASE \"person\".\"Active\" WHEN @p0 THEN @p1 END", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = true,
                ["@p1"] = "Active"
            }, result.Parameters);
        }

        [Fact]
        public void Case_Value_When_Expression()
        {
            Person person = null;
            ICase caseWhen = sql.Case(() => person.Name)
                .When(() => person.SurName, "abcd");

            QueryResult result = engine.Compile(caseWhen);

            Assert.Equal("CASE \"person\".\"Name\" WHEN \"person\".\"SurName\" THEN @p0 END", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "abcd"
            }, result.Parameters);
        }

        [Fact]
        public void Case_Value_When_Expression_Then_Null()
        {
            Person person = null;
            ICase caseWhen = sql.Case(() => person.Name)
                .When(() => person.SurName, null);

            QueryResult result = engine.Compile(caseWhen);

            Assert.Equal("CASE \"person\".\"Name\" WHEN \"person\".\"SurName\" THEN NULL END", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Case_Value_Then_Expression()
        {
            Person person = null;
            ICase caseWhen = sql.Case(() => person.Name)
                .When("abcd", () => person.Name);

            QueryResult result = engine.Compile(caseWhen);

            Assert.Equal("CASE \"person\".\"Name\" WHEN @p0 THEN \"person\".\"Name\" END", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "abcd"
            }, result.Parameters);
        }

        [Fact]
        public void Case_Value_Then_Expression_When_Null()
        {
            Person person = null;
            ICase caseWhen = sql.Case(() => person.Name)
                .When(null, () => "abcd");

            QueryResult result = engine.Compile(caseWhen);

            Assert.Equal("CASE \"person\".\"Name\" WHEN NULL THEN @p0 END", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "abcd"
            }, result.Parameters);
        }

        [Fact]
        public void Case_Value_Two_Expressions()
        {
            Person person = null;
            ICase caseWhen = sql.Case(() => person.Name)
                .When(() => person.SurName, () => person.Name);

            QueryResult result = engine.Compile(caseWhen);

            Assert.Equal("CASE \"person\".\"Name\" WHEN \"person\".\"SurName\" THEN \"person\".\"Name\" END", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Case_Value_Expression_Bool_Property()
        {
            Person person = null;
            ICase caseWhen = sql.Case(true)
                .When(() => person.Active, "Active")
                .Else("Inactive");

            QueryResult result = engine.Compile(caseWhen);

            Assert.Equal("CASE @p0 WHEN \"person\".\"Active\" THEN @p1 ELSE @p2 END", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = true,
                ["@p1"] = "Active",
                ["@p2"] = "Inactive"
            }, result.Parameters);
        }

        [Fact]
        public void Case_Value_When_Expression_Multiple()
        {
            Person person = null;
            ICase caseWhen = sql.Case(() => person.Name)
                .When(() => person.SurName, "abcd")
                .When("efgh", () => person.Name)
                .Else("ijkl");

            QueryResult result = engine.Compile(caseWhen);

            Assert.Equal("CASE \"person\".\"Name\" WHEN \"person\".\"SurName\" THEN @p0 "
                + "WHEN @p1 THEN \"person\".\"Name\" ELSE @p2 END", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "abcd",
                ["@p1"] = "efgh",
                ["@p2"] = "ijkl"
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataString))]
        public void Expression_Left_Value(string value)
        {
            Person person = null;
            ICase caseWhen = (ICase)sql.Val(() => person.Name == null ? value : person.SurName);

            QueryResult result = engine.Compile(caseWhen);

            Assert.Equal("CASE WHEN \"person\".\"Name\" IS NULL THEN @p0 ELSE \"person\".\"SurName\" END", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataString))]
        public void Expression_Right_Value(string value)
        {
            Person person = null;
            ICase caseWhen = (ICase)sql.Val(() => person.Name != null ? person.SurName : value);

            QueryResult result = engine.Compile(caseWhen);

            Assert.Equal("CASE WHEN \"person\".\"Name\" IS NOT NULL THEN \"person\".\"SurName\" ELSE @p0 END", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Column()
        {
            Person person = null;
            ICase caseWhen = (ICase)sql.Val(() => person.Name.Like("%abcd%") ? person.Name : person.SurName);

            QueryResult result = engine.Compile(caseWhen);

            Assert.Equal("CASE WHEN \"person\".\"Name\" LIKE @p0 THEN \"person\".\"Name\" "
                + "ELSE \"person\".\"SurName\" END", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "%abcd%"
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Object()
        {
            Person person = null;
            ICase caseWhen = (ICase)sql.Val(() => person.Name != null ? (object)person.SurName : person.Id);

            QueryResult result = engine.Compile(caseWhen);

            Assert.Equal("CASE WHEN \"person\".\"Name\" IS NOT NULL THEN \"person\".\"SurName\" "
                + "ELSE \"person\".\"Id\" END", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Bool_Property()
        {
            Person person = null;
            ICase caseWhen = (ICase)sql.Val(() => person.Active ? "Active" : "Inactive");

            QueryResult result = engine.Compile(caseWhen);

            Assert.Equal("CASE WHEN \"person\".\"Active\" = @p0 THEN @p1 ELSE @p2 END", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = true,
                ["@p1"] = "Active",
                ["@p2"] = "Inactive"
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Multiple()
        {
            Person person = null;
            ICase caseWhen = (ICase)sql.Val(() => person.Salary > 2000m ? "Greather than 2000"
                : person.Salary > 1000m ? "Greather than 1000"
                : "Under 1000");

            QueryResult result = engine.Compile(caseWhen);

            Assert.Equal("CASE WHEN \"person\".\"Salary\" > @p0 THEN @p1 "
                + "WHEN \"person\".\"Salary\" > @p2 THEN @p3 ELSE @p4 END", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 2000m,
                ["@p1"] = "Greather than 2000",
                ["@p2"] = 1000m,
                ["@p3"] = "Greather than 1000",
                ["@p4"] = "Under 1000"
            }, result.Parameters);
        }

        [Theory]
        [InlineData(1000, 10, "efgh")]
        public void Expression_Large(decimal value1, int value2, string value3)
        {
            Person person = null;
            ICase caseWhen = (ICase)sql.Val(() => person.Salary > 2000m ? "Greather than 2000"
                : person.Name.Like("%abcd%") ? person.Name
                : person.Salary > value1 ? $"Greather than {value1}"
                : person.Id == value2 ? person.Address.Street
                : person.Active ? value3
                : "Under 1000");

            QueryResult result = engine.Compile(caseWhen);

            Assert.Equal("CASE WHEN \"person\".\"Salary\" > @p0 THEN @p1 "
                + "WHEN \"person\".\"Name\" LIKE @p2 THEN \"person\".\"Name\" "
                + "WHEN \"person\".\"Salary\" > @p3 THEN @p4 "
                + "WHEN \"person\".\"Id\" = @p5 THEN \"person\".\"AddressStreet\" "
                + "WHEN \"person\".\"Active\" = @p6 THEN @p7 ELSE @p8 END", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 2000m,
                ["@p1"] = "Greather than 2000",
                ["@p2"] = "%abcd%",
                ["@p3"] = value1,
                ["@p4"] = "Greather than 1000",
                ["@p5"] = value2,
                ["@p6"] = true,
                ["@p7"] = value3,
                ["@p8"] = "Under 1000"
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Nested()
        {
            Person person = null;
            ICase caseWhen = (ICase)sql.Val(() => person.Department.Id == 1
                ? (person.Salary > 2000m ? "Greather than 2000"
                    : person.Salary > 1000m ? "Greather than 1000"
                    : "Under 1000")
                : person.Salary > 4000m ? "Greather than 4000"
                : person.Salary > 3000m ? "Greather than 3000"
                : "Under 3000");

            QueryResult result = engine.Compile(caseWhen);

            Assert.Equal("CASE WHEN \"person\".\"DepartmentId\" = @p0 THEN "
                + "CASE WHEN \"person\".\"Salary\" > @p1 THEN @p2 "
                + "WHEN \"person\".\"Salary\" > @p3 THEN @p4 ELSE @p5 END "
                + "WHEN \"person\".\"Salary\" > @p6 THEN @p7 "
                + "WHEN \"person\".\"Salary\" > @p8 THEN @p9 ELSE @p10 END", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = 2000m,
                ["@p2"] = "Greather than 2000",
                ["@p3"] = 1000m,
                ["@p4"] = "Greather than 1000",
                ["@p5"] = "Under 1000",
                ["@p6"] = 4000m,
                ["@p7"] = "Greather than 4000",
                ["@p8"] = 3000m,
                ["@p9"] = "Greather than 3000",
                ["@p10"] = "Under 3000"
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Nested_Multiple()
        {
            Person person = null;
            ICase caseWhen = (ICase)sql.Val(() => person.Department.Id == 1
                ? (person.Salary > 2000m ? "Greather than 2000"
                    : person.Salary > 1000m ? "Greather than 1000"
                    : "Under 1000")
                : person.DepartmentId == 2
                    ? (person.Salary > 4000m ? "Greather than 4000"
                        : person.Salary > 3000m ? "Greather than 3000"
                        : "Under 3000")
                : "Other");

            QueryResult result = engine.Compile(caseWhen);

            Assert.Equal("CASE WHEN \"person\".\"DepartmentId\" = @p0 THEN "
                + "CASE WHEN \"person\".\"Salary\" > @p1 THEN @p2 "
                + "WHEN \"person\".\"Salary\" > @p3 THEN @p4 ELSE @p5 END "
                + "WHEN \"person\".\"DepartmentId\" = @p6 THEN "
                + "CASE WHEN \"person\".\"Salary\" > @p7 THEN @p8 "
                + "WHEN \"person\".\"Salary\" > @p9 THEN @p10 ELSE @p11 END "
                + "ELSE @p12 END", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = 2000m,
                ["@p2"] = "Greather than 2000",
                ["@p3"] = 1000m,
                ["@p4"] = "Greather than 1000",
                ["@p5"] = "Under 1000",
                ["@p6"] = 2,
                ["@p7"] = 4000m,
                ["@p8"] = "Greather than 4000",
                ["@p9"] = 3000m,
                ["@p10"] = "Greather than 3000",
                ["@p11"] = "Under 3000",
                ["@p12"] = "Other"
            }, result.Parameters);
        }

        [Fact]
        public void Empty_List()
        {
            ICase caseWhen = sql.Case();

            Exception ex = Assert.Throws<CompileException>(() => engine.Compile(caseWhen));
            Assert.Equal("Add at least one \"when\" clause.", ex.Message);
        }

        [Fact]
        public void To_String()
        {
            IAlias person = sql.Alias("person");
            ICase caseWhen = sql.Case()
                .When(person["Name"].IsNotNull(), person["Name"]);

            Assert.Equal("CASE WHEN person.Name IS NOT NULL THEN person.Name END", caseWhen.ToString());
        }

        [Fact]
        public void To_String_Else()
        {
            IAlias person = sql.Alias("person");
            ICase caseWhen = sql.Case()
                .When(person["Name"].IsNotNull(), person["Name"])
                .Else(person["SurName"]);

            Assert.Equal("CASE WHEN person.Name IS NOT NULL THEN person.Name ELSE person.SurName END", caseWhen.ToString());
        }

        [Fact]
        public void To_String_Case_Value()
        {
            IAlias person = sql.Alias("person");
            ICase caseWhen = sql.Case(person["Active"])
                .When(true, "Active")
                .Else("Inactive");

            Assert.Equal("CASE person.Active WHEN true THEN \"Active\" ELSE \"Inactive\" END", caseWhen.ToString());
        }
    }
}