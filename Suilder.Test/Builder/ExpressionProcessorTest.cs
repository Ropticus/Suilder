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