using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Suilder.Builder;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Expressions
{
    public class ExpressionProcessorTest : BuilderBaseTest
    {
        [Fact]
        public void Compile_Constant()
        {
            Expression<Func<object>> expression = () => 1;
            Assert.Equal(1, ExpressionProcessor.Compile(expression));
        }

        [Fact]
        public void Compile_Member()
        {
            int value = 1;
            Expression<Func<object>> expression = () => value;
            Assert.Equal(value, ExpressionProcessor.Compile(expression));
        }

        [Fact]
        public void Compile_New()
        {
            Expression<Func<object>> expression = () => new DateTime();
            Assert.Equal(new DateTime(), ExpressionProcessor.Compile(expression));
        }

        [Fact]
        public void Compile_New_Array()
        {
            Expression<Func<object>> expression = () => new byte[] { 1, 2, 3 };
            Assert.Equal(new byte[] { 1, 2, 3 }, ExpressionProcessor.Compile(expression));
        }

        [Fact]
        public void Compile_New_List()
        {
            Expression<Func<object>> expression = () => new List<int> { 1, 2, 3 };
            Assert.Equal(new List<int> { 1, 2, 3 }, ExpressionProcessor.Compile(expression));
        }

        [Fact]
        public void Compile_Dynamic_Invoke()
        {
            object value = 1;
            Expression<Func<bool>> expression = () => value is int;

            Assert.Equal(value is int, ExpressionProcessor.Compile(expression));
        }

        [Fact]
        public void Compile_UnaryExpression_Dynamic_Invoke()
        {
            Person person = new Person() { Id = 1 };
            Expression<Func<int>> expression = () => ~person.Id;

            Assert.Equal(~person.Id, ExpressionProcessor.Compile((UnaryExpression)expression.Body));
        }

        [Fact]
        public void Compile_BinaryExpression_Dynamic_Invoke()
        {
            Person person = new Person() { Id = 1 };
            Expression<Func<int>> expression = () => person.Id << 2;

            Assert.Equal(person.Id << 2, ExpressionProcessor.Compile((BinaryExpression)expression.Body));
        }

        [Fact]
        public void Invalid_ParseValue()
        {
            Person person = null;
            Expression<Func<object>> expression = () => person.Id == 1;

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionProcessor.ParseValue(expression));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        [Fact]
        public void Invalid_ParseBoolOperator()
        {
            Person person = null;
            Expression<Func<object>> expression = () => person.Salary;

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionProcessor.ParseBoolOperator(expression));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        [Fact]
        public void Invalid_ParseBoolOperator_BinaryExpression()
        {
            Person person = null;
            Expression<Func<object>> expression = () => person.Name ?? "abcd";

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionProcessor.ParseBoolOperator(
                (BinaryExpression)expression.Body));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        [Fact]
        public void Invalid_ParseLogicalOperator_BinaryExpression()
        {
            Person person = null;
            Expression<Func<object>> expression = () => person.Name ?? "abcd";

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionProcessor.ParseLogicalOperator(
                (BinaryExpression)expression.Body));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        [Fact]
        public void Invalid_ParseArithmeticOperator_BinaryExpression()
        {
            Person person = null;
            Expression<Func<object>> expression = () => person.Name ?? "abcd";

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionProcessor.ParseArithmeticOperator(
                (BinaryExpression)expression.Body));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        [Fact]
        public void Invalid_ParseBitOperator_BinaryExpression()
        {
            Person person = null;
            Expression<Func<object>> expression = () => person.Name ?? "abcd";

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionProcessor.ParseBitOperator(
                (BinaryExpression)expression.Body));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        [Fact]
        public void Invalid_ParseFunction_BinaryExpression()
        {
            Person person = null;
            Expression<Func<object>> expression = () => person.Name + "abcd";

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionProcessor.ParseFunctionOperator(
                (BinaryExpression)expression.Body));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        [Fact]
        public void Invalid_Property()
        {
            Person person = null;
            Expression<Func<object>> expression = () => person.Name.ToString();

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionProcessor.GetProperties(expression));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        [Fact]
        public void Get_Properties()
        {
            Person person = null;
            Expression<Func<object>> expression = () => person.Id;

            var properties = ExpressionProcessor.GetProperties(expression);
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