using System;
using System.Linq;
using Suilder.Builder;
using Suilder.Core;
using Xunit;

namespace Suilder.Test.Builder
{
    public class ToStringBuilderTest : BuilderBaseTest
    {
        [Fact]
        public void Write()
        {
            string result = ToStringBuilder.Build(b => b.Write("SELECT"));

            Assert.Equal("SELECT", result);
        }

        [Fact]
        public void WriteFragment()
        {
            IAlias person = sql.Alias("person");
            IFrom from = sql.From(person);
            IOperator op = sql.Eq(person["Id"], 1);
            IQuery query = sql.Query.From(from);

            string result;

            result = ToStringBuilder.Build(b => b.WriteFragment(from));
            Assert.Equal("FROM person", result);

            result = ToStringBuilder.Build(b => b.WriteFragment(op));
            Assert.Equal("person.Id = 1", result);

            result = ToStringBuilder.Build(b => b.WriteFragment(query));
            Assert.Equal("(FROM person)", result);
        }

        [Fact]
        public void WriteFragment_Parentheses_False()
        {
            IAlias person = sql.Alias("person");
            IFrom from = sql.From(person);
            IOperator op = sql.Eq(person["Id"], 1);
            IQuery query = sql.Query.From(from);

            string result;

            result = ToStringBuilder.Build(b => b.WriteFragment(from, false));
            Assert.Equal("FROM person", result);

            result = ToStringBuilder.Build(b => b.WriteFragment(op, false));
            Assert.Equal("person.Id = 1", result);

            result = ToStringBuilder.Build(b => b.WriteFragment(query, false));
            Assert.Equal("FROM person", result);
        }

        [Fact]
        public void WriteFragment_Parentheses_True()
        {
            IAlias person = sql.Alias("person");
            IFrom from = sql.From(person);
            IOperator op = sql.Eq(person["Id"], 1);
            IQuery query = sql.Query.From(from);

            string result;

            result = ToStringBuilder.Build(b => b.WriteFragment(from, true));
            Assert.Equal("(FROM person)", result);

            result = ToStringBuilder.Build(b => b.WriteFragment(op, true));
            Assert.Equal("(person.Id = 1)", result);

            result = ToStringBuilder.Build(b => b.WriteFragment(query, true));
            Assert.Equal("(FROM person)", result);
        }

        [Fact]
        public void WriteFragment_Parentheses_Never()
        {
            IAlias person = sql.Alias("person");
            IFrom from = sql.From(person);
            IOperator op = sql.Eq(person["Id"], 1);
            IQuery query = sql.Query.From(from);

            string result;

            result = ToStringBuilder.Build(b => b.WriteFragment(from, Parentheses.Never));
            Assert.Equal("FROM person", result);

            result = ToStringBuilder.Build(b => b.WriteFragment(op, Parentheses.Never));
            Assert.Equal("person.Id = 1", result);

            result = ToStringBuilder.Build(b => b.WriteFragment(query, Parentheses.Never));
            Assert.Equal("FROM person", result);
        }

        [Fact]
        public void WriteFragment_Parentheses_SubFragment()
        {
            IAlias person = sql.Alias("person");
            IFrom from = sql.From(person);
            IOperator op = sql.Eq(person["Id"], 1);
            IQuery query = sql.Query.From(from);

            string result;

            result = ToStringBuilder.Build(b => b.WriteFragment(from, Parentheses.SubFragment));
            Assert.Equal("FROM person", result);

            result = ToStringBuilder.Build(b => b.WriteFragment(op, Parentheses.SubFragment));
            Assert.Equal("(person.Id = 1)", result);

            result = ToStringBuilder.Build(b => b.WriteFragment(query, Parentheses.SubFragment));
            Assert.Equal("(FROM person)", result);
        }

        [Fact]
        public void WriteFragment_Parentheses_SubQuery()
        {
            IAlias person = sql.Alias("person");
            IFrom from = sql.From(person);
            IOperator op = sql.Eq(person["Id"], 1);
            IQuery query = sql.Query.From(from);

            string result;

            result = ToStringBuilder.Build(b => b.WriteFragment(from, Parentheses.SubQuery));
            Assert.Equal("FROM person", result);

            result = ToStringBuilder.Build(b => b.WriteFragment(op, Parentheses.SubQuery));
            Assert.Equal("person.Id = 1", result);

            result = ToStringBuilder.Build(b => b.WriteFragment(query, Parentheses.SubQuery));
            Assert.Equal("(FROM person)", result);
        }

        [Fact]
        public void WriteFragment_Parentheses_Always()
        {
            IAlias person = sql.Alias("person");
            IFrom from = sql.From(person);
            IOperator op = sql.Eq(person["Id"], 1);
            IQuery query = sql.Query.From(from);

            string result;

            result = ToStringBuilder.Build(b => b.WriteFragment(from, Parentheses.Always));
            Assert.Equal("(FROM person)", result);

            result = ToStringBuilder.Build(b => b.WriteFragment(op, Parentheses.Always));
            Assert.Equal("(person.Id = 1)", result);

            result = ToStringBuilder.Build(b => b.WriteFragment(query, Parentheses.Always));
            Assert.Equal("(FROM person)", result);
        }

        [Fact]
        public void Invalid_WriteFragment_Parentheses()
        {
            IFrom from = sql.From("person");

            Exception ex = Assert.Throws<ArgumentException>(() =>
                ToStringBuilder.Build(b => b.WriteFragment(from, (Parentheses)int.MaxValue)));
            Assert.Equal($"Invalid value. (Parameter 'parentheses')", ex.Message);
        }

        [Fact]
        public void WriteValue()
        {
            IAlias person = sql.Alias("person");
            IFrom from = sql.From(person);
            IOperator op = sql.Eq(person["Id"], 1);
            IQuery query = sql.Query.From(from);
            string value = "abcd";

            string result;

            result = ToStringBuilder.Build(b => b.WriteValue(from));
            Assert.Equal("FROM person", result);

            result = ToStringBuilder.Build(b => b.WriteValue(op));
            Assert.Equal("person.Id = 1", result);

            result = ToStringBuilder.Build(b => b.WriteValue(query));
            Assert.Equal("(FROM person)", result);

            result = ToStringBuilder.Build(b => b.WriteValue(value));
            Assert.Equal("\"abcd\"", result);
        }

        [Fact]
        public void WriteValue_Parentheses_False()
        {
            IAlias person = sql.Alias("person");
            IFrom from = sql.From(person);
            IOperator op = sql.Eq(person["Id"], 1);
            IQuery query = sql.Query.From(from);
            string value = "abcd";

            string result;

            result = ToStringBuilder.Build(b => b.WriteValue(from, false));
            Assert.Equal("FROM person", result);

            result = ToStringBuilder.Build(b => b.WriteValue(op, false));
            Assert.Equal("person.Id = 1", result);

            result = ToStringBuilder.Build(b => b.WriteValue(query, false));
            Assert.Equal("FROM person", result);

            result = ToStringBuilder.Build(b => b.WriteValue(value, false));
            Assert.Equal("\"abcd\"", result);
        }

        [Fact]
        public void WriteValue_Parentheses_True()
        {
            IAlias person = sql.Alias("person");
            IFrom from = sql.From(person);
            IOperator op = sql.Eq(person["Id"], 1);
            IQuery query = sql.Query.From(from);
            string value = "abcd";

            string result;

            result = ToStringBuilder.Build(b => b.WriteValue(from, true));
            Assert.Equal("(FROM person)", result);

            result = ToStringBuilder.Build(b => b.WriteValue(op, true));
            Assert.Equal("(person.Id = 1)", result);

            result = ToStringBuilder.Build(b => b.WriteValue(query, true));
            Assert.Equal("(FROM person)", result);

            result = ToStringBuilder.Build(b => b.WriteValue(value, true));
            Assert.Equal("(\"abcd\")", result);
        }

        [Fact]
        public void WriteValue_Parentheses_Never()
        {
            IAlias person = sql.Alias("person");
            IFrom from = sql.From(person);
            IOperator op = sql.Eq(person["Id"], 1);
            IQuery query = sql.Query.From(from);
            string value = "abcd";

            string result;

            result = ToStringBuilder.Build(b => b.WriteValue(from, Parentheses.Never));
            Assert.Equal("FROM person", result);

            result = ToStringBuilder.Build(b => b.WriteValue(op, Parentheses.Never));
            Assert.Equal("person.Id = 1", result);

            result = ToStringBuilder.Build(b => b.WriteValue(query, Parentheses.Never));
            Assert.Equal("FROM person", result);

            result = ToStringBuilder.Build(b => b.WriteValue(value, Parentheses.Never));
            Assert.Equal("\"abcd\"", result);
        }

        [Fact]
        public void WriteValue_Parentheses_SubFragment()
        {
            IAlias person = sql.Alias("person");
            IFrom from = sql.From(person);
            IOperator op = sql.Eq(person["Id"], 1);
            IQuery query = sql.Query.From(from);
            string value = "abcd";

            string result;

            result = ToStringBuilder.Build(b => b.WriteValue(from, Parentheses.SubFragment));
            Assert.Equal("FROM person", result);

            result = ToStringBuilder.Build(b => b.WriteValue(op, Parentheses.SubFragment));
            Assert.Equal("(person.Id = 1)", result);

            result = ToStringBuilder.Build(b => b.WriteValue(query, Parentheses.SubFragment));
            Assert.Equal("(FROM person)", result);

            result = ToStringBuilder.Build(b => b.WriteValue(value, Parentheses.SubFragment));
            Assert.Equal("\"abcd\"", result);
        }

        [Fact]
        public void WriteValue_Parentheses_SubQuery()
        {
            IAlias person = sql.Alias("person");
            IFrom from = sql.From(person);
            IOperator op = sql.Eq(person["Id"], 1);
            IQuery query = sql.Query.From(from);
            string value = "abcd";

            string result;

            result = ToStringBuilder.Build(b => b.WriteValue(from, Parentheses.SubQuery));
            Assert.Equal("FROM person", result);

            result = ToStringBuilder.Build(b => b.WriteValue(op, Parentheses.SubQuery));
            Assert.Equal("person.Id = 1", result);

            result = ToStringBuilder.Build(b => b.WriteValue(query, Parentheses.SubQuery));
            Assert.Equal("(FROM person)", result);

            result = ToStringBuilder.Build(b => b.WriteValue(value, Parentheses.SubQuery));
            Assert.Equal("\"abcd\"", result);
        }

        [Fact]
        public void WriteValue_Parentheses_Always()
        {
            IAlias person = sql.Alias("person");
            IFrom from = sql.From(person);
            IOperator op = sql.Eq(person["Id"], 1);
            IQuery query = sql.Query.From(from);
            string value = "abcd";

            string result;

            result = ToStringBuilder.Build(b => b.WriteValue(from, Parentheses.Always));
            Assert.Equal("(FROM person)", result);

            result = ToStringBuilder.Build(b => b.WriteValue(op, Parentheses.Always));
            Assert.Equal("(person.Id = 1)", result);

            result = ToStringBuilder.Build(b => b.WriteValue(query, Parentheses.Always));
            Assert.Equal("(FROM person)", result);

            result = ToStringBuilder.Build(b => b.WriteValue(value, Parentheses.Always));
            Assert.Equal("(\"abcd\")", result);
        }

        [Fact]
        public void Invalid_WriteValue_Parentheses()
        {
            IFrom from = sql.From("person");

            Exception ex = Assert.Throws<ArgumentException>(() =>
                ToStringBuilder.Build(b => b.WriteValue(from, (Parentheses)int.MaxValue)));
            Assert.Equal($"Invalid value. (Parameter 'parentheses')", ex.Message);
        }

        [Fact]
        public void WriteParameter()
        {
            string result = ToStringBuilder.Build(b => b.WriteParameter("abcd"));

            Assert.Equal("\"abcd\"", result);
        }

        [Fact]
        public void WriteParameter_Parentheses_False()
        {
            string result = ToStringBuilder.Build(b => b.WriteParameter("abcd", false));

            Assert.Equal("\"abcd\"", result);
        }

        [Fact]
        public void WriteParameter_Parentheses_True()
        {
            string result = ToStringBuilder.Build(b => b.WriteParameter("abcd", true));

            Assert.Equal("(\"abcd\")", result);
        }

        [Fact]
        public void WriteParameter_Parentheses_Never()
        {
            string result = ToStringBuilder.Build(b => b.WriteParameter("abcd", Parentheses.Never));

            Assert.Equal("\"abcd\"", result);
        }

        [Fact]
        public void WriteParameter_Parentheses_SubFragment()
        {
            string result = ToStringBuilder.Build(b => b.WriteParameter("abcd", Parentheses.SubFragment));

            Assert.Equal("\"abcd\"", result);
        }

        [Fact]
        public void WriteParameter_Parentheses_SubQuery()
        {
            string result = ToStringBuilder.Build(b => b.WriteParameter("abcd", Parentheses.SubQuery));

            Assert.Equal("\"abcd\"", result);
        }

        [Fact]
        public void WriteParameter_Parentheses__Always()
        {
            string result = ToStringBuilder.Build(b => b.WriteParameter("abcd", Parentheses.Always));

            Assert.Equal("(\"abcd\")", result);
        }

        [Fact]
        public void Invalid_WriteParameter_Parentheses()
        {
            Exception ex = Assert.Throws<ArgumentException>(() =>
                ToStringBuilder.Build(b => b.WriteParameter("abcd", (Parentheses)int.MaxValue)));
            Assert.Equal($"Invalid value. (Parameter 'parentheses')", ex.Message);
        }

        [Fact]
        public void WriteParameter_String()
        {
            string result = ToStringBuilder.Build(b => b.WriteParameter("abcd"));

            Assert.Equal("\"abcd\"", result);
        }

        [Fact]
        public void WriteParameter_Boolean()
        {
            string resultTrue = ToStringBuilder.Build(b => b.WriteParameter(true));
            string resultFalse = ToStringBuilder.Build(b => b.WriteParameter(false));

            Assert.Equal("true", resultTrue);
            Assert.Equal("false", resultFalse);
        }

        [Fact]
        public void WriteParameter_Integer()
        {
            string result = ToStringBuilder.Build(b => b.WriteParameter(1));

            Assert.Equal("1", result);
        }

        [Fact]
        public void WriteParameter_Null()
        {
            string result = ToStringBuilder.Build(b => b.WriteParameter(null));

            Assert.Equal("null", result);
        }

        [Fact]
        public void WriteParameter_Enumerable()
        {
            string result = ToStringBuilder.Build(b => b.WriteParameter(new object[] { "abcd", true, false, 1, null }));

            Assert.Equal("[\"abcd\", true, false, 1, null]", result);
        }

        [Fact]
        public void WriteParameter_Enumerable_Max_Length()
        {
            string result = ToStringBuilder.Build(b => b.WriteParameter(Enumerable.Range(1, 20)));

            Assert.Equal("[1, 2, 3, 4, 5, ...]", result);
        }

        [Fact]
        public void RemoveLast()
        {
            string result = ToStringBuilder.Build(b => b.Write("SELECT").RemoveLast(3));

            Assert.Equal("SEL", result);
        }

        [Fact]
        public void IfNotNull()
        {
            IFrom from = sql.From("person");
            string result = ToStringBuilder.Build(b => b
                .IfNotNull(from, (x) => b.WriteFragment(x)));

            Assert.Equal("FROM person", result);
        }

        [Fact]
        public void IfNotNull_Null_Value()
        {
            IFrom from = null;
            string result = ToStringBuilder.Build(b => b
                .IfNotNull(from, (x) => b.WriteFragment(x)));

            Assert.Equal("", result);
        }

        [Fact]
        public void IfNotNull_Delegate_Without_Value()
        {
            IFrom from = sql.From("person");
            string result = ToStringBuilder.Build(b => b
                .IfNotNull(from, () => b.WriteFragment(from)));

            Assert.Equal("FROM person", result);
        }

        [Fact]
        public void IfNotNull_Delegate_Without_Value_Null_Value()
        {
            IFrom from = null;
            string result = ToStringBuilder.Build(b => b
                .IfNotNull(from, () => b.WriteFragment(from)));

            Assert.Equal("", result);
        }

        [Fact]
        public void If_True()
        {
            IFrom from = sql.From("person");
            string result = ToStringBuilder.Build(b => b
                .If(from != null, () => b.WriteFragment(from)));

            Assert.Equal("FROM person", result);
        }

        [Fact]
        public void If_False()
        {
            IFrom from = null;
            string result = ToStringBuilder.Build(b => b
                .If(from != null, () => b.WriteFragment(from)));

            Assert.Equal("", result);
        }

        [Fact]
        public void IfNot_True()
        {
            IFrom from = sql.From("person");
            string result = ToStringBuilder.Build(b => b
                .IfNot(from == null, () => b.WriteFragment(from)));

            Assert.Equal("FROM person", result);
        }

        [Fact]
        public void IfNot_False()
        {
            IFrom from = null;
            string result = ToStringBuilder.Build(b => b
                .IfNot(from == null, () => b.WriteFragment(from)));

            Assert.Equal("", result);
        }

        [Fact]
        public void ForEach()
        {
            string[] values = new string[] { "a", "b", "c" };
            string result = ToStringBuilder.Build(b => b
                .ForEach(values, (x) => b.Write(x)));

            Assert.Equal("abc", result);
        }

        [Fact]
        public void ForEach_Delegate_With_Index()
        {
            string[] values = new string[] { "a", "b", "c" };
            string result = ToStringBuilder.Build(b => b
                .ForEach(values, (x, i) => b.Write(x).Write(i.ToString())));

            Assert.Equal("a0b1c2", result);
        }

        [Fact]
        public void Join()
        {
            string[] values = new string[] { "a", "b", "c" };
            string result = ToStringBuilder.Build(b => b
                .Join(", ", values, (x) => b.Write(x)));

            Assert.Equal("a, b, c", result);
        }

        [Fact]
        public void Join_Delegate_With_Index()
        {
            string[] values = new string[] { "a", "b", "c" };
            string result = ToStringBuilder.Build(b => b
                .Join(", ", values, (x, i) => b.Write(x).Write(i.ToString())));

            Assert.Equal("a0, b1, c2", result);
        }
    }
}