using System;
using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Alias.ClassAlias
{
    public class ColumnScopeTest : BuilderBaseTest
    {
        [Fact]
        public void Expression_Field_All()
        {
            IColumn column = sql.Col(() => personField);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"personField\".\"Id\", \"personField\".\"Active\", \"personField\".\"Name\", "
                + "\"personField\".\"Surname\", \"personField\".\"AddressStreet\", \"personField\".\"AddressNumber\", "
                + "\"personField\".\"AddressCity\", \"personField\".\"Salary\", \"personField\".\"DateCreated\", "
                + "\"personField\".\"DepartmentId\", \"personField\".\"Image\", \"personField\".\"Flags\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Field_Column()
        {
            IColumn column = sql.Col(() => personField.Id);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"personField\".\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Property_All()
        {
            IColumn column = sql.Col(() => PersonProperty);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"PersonProperty\".\"Id\", \"PersonProperty\".\"Active\", \"PersonProperty\".\"Name\", "
                + "\"PersonProperty\".\"Surname\", \"PersonProperty\".\"AddressStreet\", "
                + "\"PersonProperty\".\"AddressNumber\", \"PersonProperty\".\"AddressCity\", "
                + "\"PersonProperty\".\"Salary\", \"PersonProperty\".\"DateCreated\", \"PersonProperty\".\"DepartmentId\", "
                + "\"PersonProperty\".\"Image\", \"PersonProperty\".\"Flags\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Property_Column()
        {
            IColumn column = sql.Col(() => PersonProperty.Id);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"PersonProperty\".\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Field_Static_All()
        {
            IColumn column = sql.Col(() => personFieldStatic);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"personFieldStatic\".\"Id\", \"personFieldStatic\".\"Active\", "
                + "\"personFieldStatic\".\"Name\", \"personFieldStatic\".\"Surname\", "
                + "\"personFieldStatic\".\"AddressStreet\", \"personFieldStatic\".\"AddressNumber\", "
                + "\"personFieldStatic\".\"AddressCity\", \"personFieldStatic\".\"Salary\", "
                + "\"personFieldStatic\".\"DateCreated\", \"personFieldStatic\".\"DepartmentId\", "
                + "\"personFieldStatic\".\"Image\", \"personFieldStatic\".\"Flags\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Field_Static_Column()
        {
            IColumn column = sql.Col(() => personFieldStatic.Id);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"personFieldStatic\".\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Property_Static_All()
        {
            IColumn column = sql.Col(() => PersonPropertyStatic);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"PersonPropertyStatic\".\"Id\", \"PersonPropertyStatic\".\"Active\", "
                + "\"PersonPropertyStatic\".\"Name\", \"PersonPropertyStatic\".\"Surname\", "
                + "\"PersonPropertyStatic\".\"AddressStreet\", \"PersonPropertyStatic\".\"AddressNumber\", "
                + "\"PersonPropertyStatic\".\"AddressCity\", \"PersonPropertyStatic\".\"Salary\", "
                + "\"PersonPropertyStatic\".\"DateCreated\", \"PersonPropertyStatic\".\"DepartmentId\", "
                + "\"PersonPropertyStatic\".\"Image\", \"PersonPropertyStatic\".\"Flags\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Property_Static_Column()
        {
            IColumn column = sql.Col(() => PersonPropertyStatic.Id);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"PersonPropertyStatic\".\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Nested_Field_Static_All()
        {
            IColumn column = sql.Col(() => Tables.person);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\", \"person\".\"Active\", \"person\".\"Name\", \"person\".\"Surname\", "
                + "\"person\".\"AddressStreet\", \"person\".\"AddressNumber\", \"person\".\"AddressCity\", "
                + "\"person\".\"Salary\", \"person\".\"DateCreated\", \"person\".\"DepartmentId\", \"person\".\"Image\", "
                + "\"person\".\"Flags\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Nested_Field_Static_Column()
        {
            IColumn column = sql.Col(() => Tables.person.Id);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Nested_Property_Static_All()
        {
            IColumn column = sql.Col(() => Tables.Person);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"Person\".\"Id\", \"Person\".\"Active\", \"Person\".\"Name\", \"Person\".\"Surname\", "
                + "\"Person\".\"AddressStreet\", \"Person\".\"AddressNumber\", \"Person\".\"AddressCity\", "
                + "\"Person\".\"Salary\", \"Person\".\"DateCreated\", \"Person\".\"DepartmentId\", \"Person\".\"Image\", "
                + "\"Person\".\"Flags\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Nested_Property_Static_Column()
        {
            IColumn column = sql.Col(() => Tables.Person.Id);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"Person\".\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Val_Method_Field_All()
        {
            IColumn column = (IColumn)sql.Val(() => personField);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"personField\".\"Id\", \"personField\".\"Active\", \"personField\".\"Name\", "
                + "\"personField\".\"Surname\", \"personField\".\"AddressStreet\", \"personField\".\"AddressNumber\", "
                + "\"personField\".\"AddressCity\", \"personField\".\"Salary\", \"personField\".\"DateCreated\", "
                + "\"personField\".\"DepartmentId\", \"personField\".\"Image\", \"personField\".\"Flags\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Val_Method_Field_Column()
        {
            IColumn column = (IColumn)sql.Val(() => personField.Id);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"personField\".\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Val_Method_Property_All()
        {
            IColumn column = (IColumn)sql.Val(() => PersonProperty);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"PersonProperty\".\"Id\", \"PersonProperty\".\"Active\", \"PersonProperty\".\"Name\", "
                + "\"PersonProperty\".\"Surname\", \"PersonProperty\".\"AddressStreet\", "
                + "\"PersonProperty\".\"AddressNumber\", \"PersonProperty\".\"AddressCity\", "
                + "\"PersonProperty\".\"Salary\", \"PersonProperty\".\"DateCreated\", \"PersonProperty\".\"DepartmentId\", "
                + "\"PersonProperty\".\"Image\", \"PersonProperty\".\"Flags\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Val_Method_Property_Column()
        {
            IColumn column = (IColumn)sql.Val(() => PersonProperty.Id);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"PersonProperty\".\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Val_Method_Field_Static_All()
        {
            IColumn column = (IColumn)sql.Val(() => personFieldStatic);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"personFieldStatic\".\"Id\", \"personFieldStatic\".\"Active\", "
                + "\"personFieldStatic\".\"Name\", \"personFieldStatic\".\"Surname\", "
                + "\"personFieldStatic\".\"AddressStreet\", \"personFieldStatic\".\"AddressNumber\", "
                + "\"personFieldStatic\".\"AddressCity\", \"personFieldStatic\".\"Salary\", "
                + "\"personFieldStatic\".\"DateCreated\", \"personFieldStatic\".\"DepartmentId\", "
                + "\"personFieldStatic\".\"Image\", \"personFieldStatic\".\"Flags\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Val_Method_Field_Static_Column()
        {
            IColumn column = (IColumn)sql.Val(() => personFieldStatic.Id);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"personFieldStatic\".\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Val_Method_Property_Static_All()
        {
            IColumn column = (IColumn)sql.Val(() => PersonPropertyStatic);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"PersonPropertyStatic\".\"Id\", \"PersonPropertyStatic\".\"Active\", "
                + "\"PersonPropertyStatic\".\"Name\", \"PersonPropertyStatic\".\"Surname\", "
                + "\"PersonPropertyStatic\".\"AddressStreet\", \"PersonPropertyStatic\".\"AddressNumber\", "
                + "\"PersonPropertyStatic\".\"AddressCity\", \"PersonPropertyStatic\".\"Salary\", "
                + "\"PersonPropertyStatic\".\"DateCreated\", \"PersonPropertyStatic\".\"DepartmentId\", "
                + "\"PersonPropertyStatic\".\"Image\", \"PersonPropertyStatic\".\"Flags\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Val_Method_Property_Static_Column()
        {
            IColumn column = (IColumn)sql.Val(() => PersonPropertyStatic.Id);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"PersonPropertyStatic\".\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Val_Method_Nested_Field_Static_All()
        {
            IColumn column = (IColumn)sql.Val(() => Tables.person);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\", \"person\".\"Active\", \"person\".\"Name\", \"person\".\"Surname\", "
                + "\"person\".\"AddressStreet\", \"person\".\"AddressNumber\", \"person\".\"AddressCity\", "
                + "\"person\".\"Salary\", \"person\".\"DateCreated\", \"person\".\"DepartmentId\", \"person\".\"Image\", "
                + "\"person\".\"Flags\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Val_Method_Nested_Field_Static_Column()
        {
            IColumn column = (IColumn)sql.Val(() => Tables.person.Id);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Val_Method_Nested_Property_Static_All()
        {
            IColumn column = (IColumn)sql.Val(() => Tables.Person);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"Person\".\"Id\", \"Person\".\"Active\", \"Person\".\"Name\", \"Person\".\"Surname\", "
                + "\"Person\".\"AddressStreet\", \"Person\".\"AddressNumber\", \"Person\".\"AddressCity\", "
                + "\"Person\".\"Salary\", \"Person\".\"DateCreated\", \"Person\".\"DepartmentId\", \"Person\".\"Image\", "
                + "\"Person\".\"Flags\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Val_Method_Nested_Property_Static_Column()
        {
            IColumn column = (IColumn)sql.Val(() => Tables.Person.Id);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"Person\".\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Invalid_Expression_Method_All()
        {
            Exception ex = Assert.Throws<ArgumentException>(() => sql.Col(() => GetPerson()));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        [Fact]
        public void Invalid_Expression_Method_Column()
        {
            Exception ex = Assert.Throws<ArgumentException>(() => sql.Col(() => GetPerson().Id));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        [Fact]
        public void Invalid_Expression_Nested_Method_All()
        {
            TablesError tables = null;

            Exception ex = Assert.Throws<ArgumentException>(() => sql.Col(() => tables.GetPerson()));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        [Fact]
        public void Invalid_Expression_Nested_Method_Column()
        {
            TablesError tables = null;

            Exception ex = Assert.Throws<ArgumentException>(() => sql.Col(() => tables.GetPerson().Id));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        [Fact]
        public void Invalid_Expression_Method_Static_All()
        {
            Exception ex = Assert.Throws<ArgumentException>(() => sql.Col(() => GetPersonStatic()));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        [Fact]
        public void Invalid_Expression_Method_Static_Column()
        {
            Exception ex = Assert.Throws<ArgumentException>(() => sql.Col(() => GetPersonStatic().Id));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        [Fact]
        public void Invalid_Expression_Nested_Method_Static_All()
        {
            Exception ex = Assert.Throws<ArgumentException>(() => sql.Col(() => Tables.GetPerson()));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        [Fact]
        public void Invalid_Expression_Nested_Method_Static_Column()
        {
            Exception ex = Assert.Throws<ArgumentException>(() => sql.Col(() => Tables.GetPerson().Id));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        public Person personField;

        public Person PersonProperty { get; set; }

        public Person GetPerson() => personField;

        public static Person personFieldStatic;

        public static Person PersonPropertyStatic { get; set; }

        public static Person GetPersonStatic() => null;

        protected static class Tables
        {
            public static Person person;

            public static Person Person { get; set; }

            public static Person GetPerson() => null;
        }

        protected class TablesError
        {
            public Person GetPerson() => null;
        }
    }
}