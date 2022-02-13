using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Exceptions;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Alias.ClassAlias
{
    public class ColumnTest : BuilderBaseTest
    {
        [Fact]
        public void Expression_All()
        {
            Person person = null;
            IColumn column = sql.Col(() => person);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\", \"person\".\"Active\", \"person\".\"Name\", \"person\".\"SurName\", "
                + "\"person\".\"AddressStreet\", \"person\".\"AddressNumber\", \"person\".\"AddressCity\", "
                + "\"person\".\"Salary\", \"person\".\"DateCreated\", \"person\".\"DepartmentId\", \"person\".\"Image\", "
                + "\"person\".\"Flags\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Column()
        {
            Person person = null;
            IColumn column = sql.Col(() => person.Id);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Column_Bool_Property()
        {
            Person person = null;
            IColumn column = sql.Col(() => person.Active);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Active\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Column_ForeignKey()
        {
            Person person = null;
            IColumn column = sql.Col(() => person.Department.Id);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"DepartmentId\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Column_Nested()
        {
            Person person = null;
            IColumn column = sql.Col(() => person.Address.Street);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"AddressStreet\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Column_Nested_Deep()
        {
            Person2 person = null;
            IColumn column = sql.Col(() => person.Address.City.Country.Name);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"AddressCityCountryName\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Column_With_Translation()
        {
            Person person = null;
            IColumn column = sql.Col(() => person.Created);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"DateCreated\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Lambda_Overload()
        {
            Person person = null;
            Expression<Func<object>> expression = () => person.Id;
            IColumn column = sql.Col((LambdaExpression)expression);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Body_Overload()
        {
            Person person = null;
            Expression<Func<object>> expression = () => person.Id;
            IColumn column = sql.Col(expression.Body);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Val_Method_All()
        {
            Person person = null;
            IColumn column = (IColumn)sql.Val(() => person);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\", \"person\".\"Active\", \"person\".\"Name\", \"person\".\"SurName\", "
                + "\"person\".\"AddressStreet\", \"person\".\"AddressNumber\", \"person\".\"AddressCity\", "
                + "\"person\".\"Salary\", \"person\".\"DateCreated\", \"person\".\"DepartmentId\", \"person\".\"Image\", "
                + "\"person\".\"Flags\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Val_Method_Column()
        {
            Person person = null;
            IColumn column = (IColumn)sql.Val(() => person.Id);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Val_Method_Column_Bool_Property()
        {
            Person person = null;
            IColumn column = (IColumn)sql.Val(() => person.Active);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Active\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Val_Method_Column_ForeignKey()
        {
            Person person = null;
            IColumn column = (IColumn)sql.Val(() => person.Department.Id);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"DepartmentId\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Val_Method_Column_Nested()
        {
            Person person = null;
            IColumn column = (IColumn)sql.Val(() => person.Address.Street);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"AddressStreet\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Val_Method_Column_Nested_Deep()
        {
            Person2 person = null;
            IColumn column = (IColumn)sql.Val(() => person.Address.City.Country.Name);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"AddressCityCountryName\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Val_Method_Column_With_Translation()
        {
            Person person = null;
            IColumn column = (IColumn)sql.Val(() => person.Created);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"DateCreated\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Val_Method_Lambda_Overload()
        {
            Person person = null;
            Expression<Func<object>> expression = () => person.Id;
            IColumn column = (IColumn)sql.Val((LambdaExpression)expression);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Val_Method_Body_Overload()
        {
            Person person = null;
            Expression<Func<object>> expression = () => person.Id;
            IColumn column = (IColumn)sql.Val(expression.Body);

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"person\".\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Invalid_Ignored_Property()
        {
            Person person = null;
            IColumn column = sql.Col(() => person.Ignore);

            Exception ex = Assert.Throws<InvalidConfigurationException>(() => engine.Compile(column));
            Assert.Equal($"The property \"Ignore\" for type \"{typeof(Person).FullName}\" is not registered.", ex.Message);
        }

        [Fact]
        public void Invalid_Not_Registered_Property()
        {
            Person person = null;
            IColumn column = sql.Col(() => person.FullName);

            Exception ex = Assert.Throws<InvalidConfigurationException>(() => engine.Compile(column));
            Assert.Equal($"The property \"FullName\" for type \"{typeof(Person).FullName}\" is not registered.", ex.Message);
        }

        [Fact]
        public void Invalid_Nested_Property()
        {
            Person person = null;
            IColumn column = sql.Col(() => person.Department.Name);

            Exception ex = Assert.Throws<InvalidConfigurationException>(() => engine.Compile(column));
            Assert.Equal($"The property \"Department.Name\" for type \"{typeof(Person).FullName}\" is not registered.",
                ex.Message);
        }

        [Fact]
        public void Invalid_Property()
        {
            Person person = null;

            Exception ex = Assert.Throws<ArgumentException>(() => sql.Col(() => person.ToString()));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        [Fact]
        public void Invalid_Property_Object_Overload()
        {
            Person person = null;
            Expression<Func<object>> expression = () => person.ToString();

            Exception ex = Assert.Throws<ArgumentException>(() => sql.Col(() => person.ToString()));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        [Fact]
        public void Name_All()
        {
            Person person = null;
            IColumn column = sql.Col(() => person).Name;

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"Id\", \"Active\", \"Name\", \"SurName\", \"AddressStreet\", \"AddressNumber\", "
                + "\"AddressCity\", \"Salary\", \"DateCreated\", \"DepartmentId\", \"Image\", \"Flags\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Name_Column()
        {
            Person person = null;
            IColumn column = sql.Col(() => person.Id).Name;

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Name_Column_ForeignKey()
        {
            Person person = null;
            IColumn column = sql.Col(() => person.Department.Id).Name;

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"DepartmentId\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Name_Column_Nested()
        {
            Person person = null;
            IColumn column = sql.Col(() => person.Address.Street).Name;

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"AddressStreet\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Name_Column_Nested_Deep()
        {
            Person2 person = null;
            IColumn column = sql.Col(() => person.Address.City.Country.Name).Name;

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"AddressCityCountryName\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Name_Column_With_Translation()
        {
            Person person = null;
            IColumn column = sql.Col(() => person.Created).Name;

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"DateCreated\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Name_Name_All()
        {
            Person person = null;
            IColumn column = sql.Col(() => person).Name.Name;

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"Id\", \"Active\", \"Name\", \"SurName\", \"AddressStreet\", \"AddressNumber\", "
                + "\"AddressCity\", \"Salary\", \"DateCreated\", \"DepartmentId\", \"Image\", \"Flags\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Name_Name_Column()
        {
            Person person = null;
            IColumn column = sql.Col(() => person.Id).Name.Name;

            QueryResult result = engine.Compile(column);

            Assert.Equal("\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void To_String_All()
        {
            Person person = null;
            IColumn column = sql.Col(() => person);

            Assert.Equal("person.*", column.ToString());
        }

        [Fact]
        public void To_String_Column()
        {
            Person person = null;
            IColumn column = sql.Col(() => person.Id);

            Assert.Equal("person.Id", column.ToString());
        }

        [Fact]
        public void To_String_Column_ForeignKey()
        {
            Person person = null;
            IColumn column = sql.Col(() => person.Department.Id);

            Assert.Equal("person.Department.Id", column.ToString());
        }

        [Fact]
        public void To_String_Column_Nested()
        {
            Person person = null;
            IColumn column = sql.Col(() => person.Address.Street);

            Assert.Equal("person.Address.Street", column.ToString());
        }

        [Fact]
        public void To_String_Column_Nested_Deep()
        {
            Person2 person = null;
            IColumn column = sql.Col(() => person.Address.City.Country.Name);

            Assert.Equal("person.Address.City.Country.Name", column.ToString());
        }

        [Fact]
        public void To_String_Column_With_Translation()
        {
            Person person = null;
            IColumn column = sql.Col(() => person.Created);

            Assert.Equal("person.Created", column.ToString());
        }

        [Fact]
        public void To_String_Name_All()
        {
            Person person = null;
            IColumn column = sql.Col(() => person).Name;

            Assert.Equal("*", column.ToString());
        }

        [Fact]
        public void To_String_Name_Column()
        {
            Person person = null;
            IColumn column = sql.Col(() => person.Id).Name;

            Assert.Equal("Id", column.ToString());
        }

        [Fact]
        public void To_String_Name_Column_ForeignKey()
        {
            Person person = null;
            IColumn column = sql.Col(() => person.Department.Id).Name;

            Assert.Equal("Department.Id", column.ToString());
        }

        [Fact]
        public void To_String_Name_Column_Nested()
        {
            Person person = null;
            IColumn column = sql.Col(() => person.Address.Street).Name;

            Assert.Equal("Address.Street", column.ToString());
        }

        [Fact]
        public void To_String_Name_Column_Nested_Deep()
        {
            Person2 person = null;
            IColumn column = sql.Col(() => person.Address.City.Country.Name).Name;

            Assert.Equal("Address.City.Country.Name", column.ToString());
        }

        [Fact]
        public void To_String_Name_Column_With_Translation()
        {
            Person person = null;
            IColumn column = sql.Col(() => person.Created).Name;

            Assert.Equal("Created", column.ToString());
        }
    }
}