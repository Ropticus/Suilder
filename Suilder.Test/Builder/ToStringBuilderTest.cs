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
            IFrom from = sql.From("person");
            string result = ToStringBuilder.Build(b => b.WriteFragment(from));

            Assert.Equal("FROM person", result);
        }

        [Fact]
        public void WriteFragment_SubQuery()
        {
            IQuery query = sql.Query.From("person");
            string result = ToStringBuilder.Build(b => b.WriteFragment(query));

            Assert.Equal("(FROM person)", result);
        }

        [Fact]
        public void WriteFragment_Parentheses()
        {
            IFrom from = sql.From("person");
            string result = ToStringBuilder.Build(b => b.WriteFragment(from, true));

            Assert.Equal("(FROM person)", result);
        }

        [Fact]
        public void WriteValue_Fragment()
        {
            IFrom from = sql.From("person");
            string result = ToStringBuilder.Build(b => b.WriteValue(from));

            Assert.Equal("FROM person", result);
        }

        [Fact]
        public void WriteValue_Parameter()
        {
            string result = ToStringBuilder.Build(b => b.WriteValue("value"));

            Assert.Equal("\"value\"", result);
        }

        [Fact]
        public void WriteParameter_String()
        {
            string result = ToStringBuilder.Build(b => b.WriteParameter("value"));

            Assert.Equal("\"value\"", result);
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

            Assert.Equal("NULL", result);
        }

        [Fact]
        public void WriteParameter_Enumerable()
        {
            string result = ToStringBuilder.Build(b => b.WriteParameter(new object[] { "value", true, false, 1, null }));

            Assert.Equal("[\"value\", true, false, 1, NULL]", result);
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