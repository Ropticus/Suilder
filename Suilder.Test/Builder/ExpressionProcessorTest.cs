using System;
using System.Linq;
using System.Linq.Expressions;
using Suilder.Builder;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder
{
    public class ExpressionProcessorTest : BuilderBaseTest
    {
        [Fact]
        public void Compile_Constant()
        {
            Expression<Func<object>> expression = () => 1;
            Assert.Equal(1, ExpressionProcessor.Compile(expression.Body));
        }

        [Fact]
        public void Compile_Member()
        {
            int value = 1;
            Expression<Func<object>> expression = () => value;
            Assert.Equal(value, ExpressionProcessor.Compile(expression.Body));
        }

        [Fact]
        public void Compile_New()
        {
            Expression<Func<object>> expression = () => new DateTime();
            Assert.Equal(new DateTime(), ExpressionProcessor.Compile(expression.Body));
        }

        [Fact]
        public void Compile_New_Array()
        {
            Expression<Func<object>> expression = () => new byte[] { 1, 2, 3 };
            Assert.Equal(new byte[] { 1, 2, 3 }, ExpressionProcessor.Compile(expression.Body));
        }

        [Fact]
        public void Compile_Dynamic_Invoke()
        {
            string value1 = null, value2 = "abcd";
            Expression<Func<object>> expression = () => value1 ?? value2;
            Assert.Equal("abcd", ExpressionProcessor.Compile(expression.Body));
        }

        [Fact]
        public void Compile_Binary()
        {
            string value1 = "abcd", value2 = "efgh";
            Expression<Func<object>> expression = () => value1 + value2;
            Assert.Equal(value1 + value2, ExpressionProcessor.Compile((BinaryExpression)expression.Body));
        }

        [Fact]
        public void Invalid_ParseValue()
        {
            Person person = null;
            Expression<Func<object>> expression = () => person.Name ?? "SomeName";

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionProcessor.ParseValue(expression.Body));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        [Fact]
        public void Invalid_ParseBoolOperator_Type()
        {
            Person person = null;
            Expression<Func<object>> expression = () => person.Salary + 100;

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionProcessor.ParseBoolOperator(expression.Body));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        [Fact]
        public void Invalid_ParseBoolOperator()
        {
            Person person = null;
            Expression<Func<object>> expression = () => person.Salary;

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionProcessor.ParseBoolOperator(expression.Body));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        [Fact]
        public void Invalid_ParseBoolOperator_BinaryExpression()
        {
            Person person = null;
            Expression<Func<object>> expression = () => person.Name ?? "SomeName";

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionProcessor.ParseBoolOperator(
                (BinaryExpression)expression.Body));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        [Fact]
        public void Invalid_ParseLogicalOperator_BinaryExpression()
        {
            Person person = null;
            Expression<Func<object>> expression = () => person.Name ?? "SomeName";

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionProcessor.ParseLogicalOperator(
                (BinaryExpression)expression.Body));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        [Fact]
        public void Invalid_ParseArithmeticOperator_BinaryExpression()
        {
            Person person = null;
            Expression<Func<object>> expression = () => person.Name ?? "SomeName";

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionProcessor.ParseArithmeticOperator(
                (BinaryExpression)expression.Body));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        [Fact]
        public void Invalid_ParseBitOperator_BinaryExpression()
        {
            Person person = null;
            Expression<Func<object>> expression = () => person.Name ?? "SomeName";

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionProcessor.ParseBitOperator(
                (BinaryExpression)expression.Body));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        [Fact]
        public void Invalid_Property()
        {
            Person person = null;
            Expression<Func<object>> expression = () => person.Name.ToString();

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionProcessor.GetProperties(expression.Body));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        [Fact]
        public void Get_Properties()
        {
            Person person = null;
            Expression<Func<object>> expression = () => person.Id;

            var properties = ExpressionProcessor.GetProperties(expression.Body);
            Assert.Equal("person.Id", string.Join('.', properties.Select(x => x.Name)));
        }

        [Fact]
        public void Add_Table()
        {
            ExpressionProcessor.AddTable(typeof(CustomTable));

            Assert.True(ExpressionProcessor.ContainsTable(typeof(CustomTable)));
        }

        [Fact]
        public void Remove_Table()
        {
            ExpressionProcessor.AddTable(typeof(CustomTable));
            ExpressionProcessor.RemoveTable(typeof(CustomTable));

            Assert.False(ExpressionProcessor.ContainsTable(typeof(CustomTable)));
        }

        private class CustomTable
        {
        }
    }
}