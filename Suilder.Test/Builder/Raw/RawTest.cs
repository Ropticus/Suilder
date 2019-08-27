using System;
using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Xunit;

namespace Suilder.Test.Builder.Raw
{
    public class RawTest : BuilderBaseTest
    {
        [Fact]
        public void Raw_Text()
        {
            IRawSql raw = sql.Raw("SELECT * FROM person");

            QueryResult result = engine.Compile(raw);

            Assert.Equal("SELECT * FROM person", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Raw_Format()
        {
            IAlias person = sql.Alias("person");
            IRawSql raw = sql.Raw("SELECT {0}, {1} FROM {2}", person["Name"], "Some text", person);

            QueryResult result = engine.Compile(raw);

            Assert.Equal("SELECT \"person\".\"Name\", @p0 FROM \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "Some text"
            }, result.Parameters);
        }

        [Fact]
        public void Raw_Format_Start_End()
        {
            IAlias person = sql.Alias("person");
            IRawSql raw = sql.Raw("{0} FROM {1}", person["Name"], person);

            QueryResult result = engine.Compile(raw);

            Assert.Equal("\"person\".\"Name\" FROM \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Raw_Format_Only_Index()
        {
            IAlias person = sql.Alias("person");
            IRawSql raw = sql.Raw("{0}", person["Name"], person);

            QueryResult result = engine.Compile(raw);

            Assert.Equal("\"person\".\"Name\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Raw_Format_Repeat()
        {
            IAlias person = sql.Alias("person");
            IRawSql raw = sql.Raw("SELECT {0}, {0} FROM {1}", person["Name"], person);

            QueryResult result = engine.Compile(raw);

            Assert.Equal("SELECT \"person\".\"Name\", \"person\".\"Name\" FROM \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Raw_Format_Escape_Index()
        {
            IAlias person = sql.Alias("person");
            IRawSql raw = sql.Raw("SELECT '{{0}}', {0} FROM {1}", person["Name"], person);

            QueryResult result = engine.Compile(raw);

            Assert.Equal("SELECT '{0}', \"person\".\"Name\" FROM \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Raw_Format_Escape_Text()
        {
            IAlias person = sql.Alias("person");
            IRawSql raw = sql.Raw("SELECT '{{Some text}}', {0} FROM {1}", person["Name"], person);

            QueryResult result = engine.Compile(raw);

            Assert.Equal("SELECT '{Some text}', \"person\".\"Name\" FROM \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Raw_Format_Escape_Multiple()
        {
            IAlias person = sql.Alias("person");
            IRawSql raw = sql.Raw("SELECT '{{{{Some text}}}}', {0} FROM {1}", person["Name"], person);

            QueryResult result = engine.Compile(raw);

            Assert.Equal("SELECT '{{Some text}}', \"person\".\"Name\" FROM \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Raw_Format_Escape_Start_End()
        {
            IAlias person = sql.Alias("person");
            IRawSql raw = sql.Raw("{{Some text}}", person["Name"], person);

            QueryResult result = engine.Compile(raw);

            Assert.Equal("{Some text}", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Invalid_Index_Out_Range()
        {
            IAlias person = sql.Alias("person");

            Exception ex = Assert.Throws<FormatException>(() => sql.Raw("SELECT {2} FROM", person["Name"]));
            Assert.Equal("Index (zero based) must be greater than or equal to zero and less than the size of the "
                + "argument list", ex.Message);
        }

        [Fact]
        public void Invalid_Index_Negative()
        {
            IAlias person = sql.Alias("person");

            Exception ex = Assert.Throws<FormatException>(() => sql.Raw("SELECT {-1} FROM", person["Name"]));
            Assert.Equal("Index (zero based) must be greater than or equal to zero and less than the size of the "
                + "argument list", ex.Message);
        }

        [Fact]
        public void Invalid_Index_Empty()
        {
            IAlias person = sql.Alias("person");

            Exception ex = Assert.Throws<FormatException>(() => sql.Raw("SELECT {} FROM", person["Name"]));
            Assert.Equal("Input string was not in a correct format.", ex.Message);
        }

        [Fact]
        public void Invalid_Index()
        {
            IAlias person = sql.Alias("person");

            Exception ex = Assert.Throws<FormatException>(() => sql.Raw("SELECT {name} FROM", person["Name"]));
            Assert.Equal("Input string was not in a correct format.", ex.Message);
        }

        [Fact]
        public void Invalid_Index_Open()
        {
            IAlias person = sql.Alias("person");

            Exception ex = Assert.Throws<FormatException>(() => sql.Raw("SELECT { FROM", person["Name"]));
            Assert.Equal("Input string was not in a correct format.", ex.Message);
        }

        [Fact]
        public void Invalid_Index_Open_Start()
        {
            IAlias person = sql.Alias("person");

            Exception ex = Assert.Throws<FormatException>(() => sql.Raw("{SELECT * FROM", person["Name"]));
            Assert.Equal("Input string was not in a correct format.", ex.Message);
        }

        [Fact]
        public void Invalid_Index_Open_End()
        {
            IAlias person = sql.Alias("person");

            Exception ex = Assert.Throws<FormatException>(() => sql.Raw("SELECT * FROM{", person["Name"]));
            Assert.Equal("Input string was not in a correct format.", ex.Message);
        }

        [Fact]
        public void Invalid_Index_Open_Length_One()
        {
            IAlias person = sql.Alias("person");

            Exception ex = Assert.Throws<FormatException>(() => sql.Raw("{", person["Name"]));
            Assert.Equal("Input string was not in a correct format.", ex.Message);
        }

        [Fact]
        public void Invalid_Index_Open_Before_Close()
        {
            IAlias person = sql.Alias("person");

            Exception ex = Assert.Throws<FormatException>(() => sql.Raw("SELECT { { FROM ", person["Name"]));
            Assert.Equal("Input string was not in a correct format.", ex.Message);
        }

        [Fact]
        public void Invalid_Index_Open_Escaped()
        {
            IAlias person = sql.Alias("person");

            Exception ex = Assert.Throws<FormatException>(() => sql.Raw("SELECT {{{ FROM", person["Name"]));
            Assert.Equal("Input string was not in a correct format.", ex.Message);
        }

        [Fact]
        public void Invalid_Index_Open_Escaped_Start()
        {
            IAlias person = sql.Alias("person");

            Exception ex = Assert.Throws<FormatException>(() => sql.Raw("{{{SELECT * FROM", person["Name"]));
            Assert.Equal("Input string was not in a correct format.", ex.Message);
        }

        [Fact]
        public void Invalid_Index_Open_Escaped_End()
        {
            IAlias person = sql.Alias("person");

            Exception ex = Assert.Throws<FormatException>(() => sql.Raw("SELECT * FROM{{{", person["Name"]));
            Assert.Equal("Input string was not in a correct format.", ex.Message);
        }

        [Fact]
        public void Invalid_Index_Close()
        {
            IAlias person = sql.Alias("person");

            Exception ex = Assert.Throws<FormatException>(() => sql.Raw("SELECT } FROM", person["Name"]));
            Assert.Equal("Input string was not in a correct format.", ex.Message);
        }

        [Fact]
        public void Invalid_Index_Close_Start()
        {
            IAlias person = sql.Alias("person");

            Exception ex = Assert.Throws<FormatException>(() => sql.Raw("}SELECT * FROM", person["Name"]));
            Assert.Equal("Input string was not in a correct format.", ex.Message);
        }

        [Fact]
        public void Invalid_Index_Close_End()
        {
            IAlias person = sql.Alias("person");

            Exception ex = Assert.Throws<FormatException>(() => sql.Raw("SELECT * FROM}", person["Name"]));
            Assert.Equal("Input string was not in a correct format.", ex.Message);
        }

        [Fact]
        public void Invalid_Index_Close_Length_One()
        {
            IAlias person = sql.Alias("person");

            Exception ex = Assert.Throws<FormatException>(() => sql.Raw("}", person["Name"]));
            Assert.Equal("Input string was not in a correct format.", ex.Message);
        }

        [Fact]
        public void Invalid_Index_Close_Escaped()
        {
            IAlias person = sql.Alias("person");

            Exception ex = Assert.Throws<FormatException>(() => sql.Raw("SELECT }}} FROM", person["Name"]));
            Assert.Equal("Input string was not in a correct format.", ex.Message);
        }

        [Fact]
        public void Invalid_Index_Close_Escaped_Start()
        {
            IAlias person = sql.Alias("person");

            Exception ex = Assert.Throws<FormatException>(() => sql.Raw("}}}SELECT * FROM", person["Name"]));
            Assert.Equal("Input string was not in a correct format.", ex.Message);
        }

        [Fact]
        public void Invalid_Index_Close_Escaped_End()
        {
            IAlias person = sql.Alias("person");

            Exception ex = Assert.Throws<FormatException>(() => sql.Raw("SELECT * FROM}}}", person["Name"]));
            Assert.Equal("Input string was not in a correct format.", ex.Message);
        }

        [Fact]
        public void To_String()
        {
            IAlias person = sql.Alias("person");
            IRawSql raw = sql.Raw("SELECT {0} FROM {2}", person["Name"], "Some text", person);

            Assert.Equal("SELECT person.Name FROM person", raw.ToString());
        }
    }
}