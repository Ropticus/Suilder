using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Suilder.Builder;
using Suilder.Functions;
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
        public void Compile_UnaryExpression_Convert_Dynamic_Invoke()
        {
            object value = 1;
            Expression<Func<int>> expression = () => (int)value;

            Assert.Equal(value, ExpressionProcessor.Compile(expression));
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
            Expression<Func<object>> expression = () => 1;

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionProcessor.ParseValue((Expression)expression));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        [Fact]
        public void Invalid_ParseValue_UnaryExpression()
        {
            object value = 1;
            Expression<Func<object>> expression = () => value is int;

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionProcessor.ParseValue(expression.Body));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        [Fact]
        public void Invalid_ParseBoolOperator()
        {
            Expression<Func<object>> expression = () => new DateTime();

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionProcessor.ParseBoolOperator(expression));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        [Fact]
        public void Invalid_ParseBoolOperator_MemberExpression()
        {
            Person person = null;
            Expression<Func<object>> expression = () => person.Salary;

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionProcessor.ParseBoolOperator(expression));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        [Fact]
        public void Invalid_ParseBoolOperator_MethodCallExpression()
        {
            Expression<Func<object>> expression = () => SqlExp.Count();

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionProcessor.ParseBoolOperator(expression));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        [Fact]
        public void Invalid_ParseBoolOperator_MethodCallExpression_Value()
        {
            string value = null;
            Expression<Func<object>> expression = () => string.IsNullOrEmpty(value);

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionProcessor.ParseBoolOperator(expression));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        [Fact]
        public void Invalid_ParseBoolOperator_UnaryExpression_Not()
        {
            Person person = null;
            Expression<Func<object>> expression = () => ~person.Flags;

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionProcessor.ParseBoolOperator(expression));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        [Fact]
        public void Invalid_ParseBoolOperator_UnaryExpression_Not_Value()
        {
            string value = null;
            Expression<Func<object>> expression = () => !string.IsNullOrEmpty(value);

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
        public void Invalid_ParseBoolOperator_BinaryExpression_And()
        {
            Person person = null;
            Expression<Func<object>> expression = () => person.Flags & 1;

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionProcessor.ParseBoolOperator(expression));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        [Fact]
        public void Invalid_ParseBoolOperator_BinaryExpression_Or()
        {
            Person person = null;
            Expression<Func<object>> expression = () => person.Flags | 1;

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionProcessor.ParseBoolOperator(expression));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        [Fact]
        public void Invalid_ParseLogicalOperator()
        {
            Person person = null;
            Expression<Func<object>> expression = () => person.Name ?? "abcd";

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionProcessor.ParseLogicalOperator(
                (BinaryExpression)expression.Body));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        [Fact]
        public void Invalid_ParseArithmeticOperator()
        {
            Person person = null;
            Expression<Func<object>> expression = () => person.Name ?? "abcd";

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionProcessor.ParseArithmeticOperator(
                (BinaryExpression)expression.Body));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        [Fact]
        public void Invalid_ParseBitOperator()
        {
            Person person = null;
            Expression<Func<object>> expression = () => person.Name ?? "abcd";

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionProcessor.ParseBitOperator(
                (BinaryExpression)expression.Body));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        [Fact]
        public void Invalid_ParseFunction()
        {
            Person person = null;
            Expression<Func<object>> expression = () => person.Name ?? "abcd";

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionProcessor.ParseFunction(
                (BinaryExpression)expression.Body));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        [Fact]
        public void Invalid_Compile_Field()
        {
            CompileTest value = null;
            Expression<Func<object>> expression = () => value.field;

            Exception ex = Assert.Throws<TargetInvocationException>(() => ExpressionProcessor.Compile(expression));
            Assert.Equal("Exception has been thrown by the target of an invocation.", ex.Message);
            Assert.IsType<NullReferenceException>(ex.InnerException);
        }

        [Fact]
        public void Invalid_Compile_Property()
        {
            CompileTest value = null;
            Expression<Func<object>> expression = () => value.Property;

            Exception ex = Assert.Throws<TargetInvocationException>(() => ExpressionProcessor.Compile(expression));
            Assert.Equal("Exception has been thrown by the target of an invocation.", ex.Message);
            Assert.IsType<NullReferenceException>(ex.InnerException);
        }

        [Fact]
        public void Invalid_Compile_Property_Exception()
        {
            CompileTest value = new CompileTest();
            Expression<Func<object>> expression = () => value.PropertyException;

            Exception ex = Assert.Throws<TargetInvocationException>(() => ExpressionProcessor.Compile(expression));
            Assert.Equal("Exception has been thrown by the target of an invocation.", ex.Message);
            Assert.IsType<NotImplementedException>(ex.InnerException);
        }

        [Fact]
        public void Invalid_Compile_Constructor()
        {
            Expression expression = Expression.New(typeof(AbstractTest));

            Exception ex = Assert.Throws<TargetInvocationException>(() => ExpressionProcessor.Compile(expression));
            Assert.Equal("Exception has been thrown by the target of an invocation.", ex.Message);
            Assert.IsType<MemberAccessException>(ex.InnerException);
        }

        [Fact]
        public void Invalid_Compile_Constructor_Exception()
        {
            Expression<Func<object>> expression = () => new CompileTest(true);

            Exception ex = Assert.Throws<TargetInvocationException>(() => ExpressionProcessor.Compile(expression));
            Assert.Equal("Exception has been thrown by the target of an invocation.", ex.Message);
            Assert.IsType<NotImplementedException>(ex.InnerException);
        }

        [Fact]
        public void Invalid_Compile_Method()
        {
            CompileTest value = null;
            Expression<Func<object>> expression = () => value.ToString();

            Exception ex = Assert.Throws<TargetInvocationException>(() => ExpressionProcessor.Compile(expression));
            Assert.Equal("Exception has been thrown by the target of an invocation.", ex.Message);
            Assert.IsType<NullReferenceException>(ex.InnerException);
        }

        [Fact]
        public void Invalid_Compile_Method_Exception()
        {
            CompileTest value = new CompileTest();
            Expression<Func<object>> expression = () => value.MethodException();

            Exception ex = Assert.Throws<TargetInvocationException>(() => ExpressionProcessor.Compile(expression));
            Assert.Equal("Exception has been thrown by the target of an invocation.", ex.Message);
            Assert.IsType<NotImplementedException>(ex.InnerException);
        }

        [Fact]
        public void Invalid_Compile_Convert()
        {
            int? value = null;
            Expression<Func<object>> expression = () => (int)value;

            Exception ex = Assert.Throws<TargetInvocationException>(() => ExpressionProcessor.Compile(expression));
            Assert.Equal("Exception has been thrown by the target of an invocation.", ex.Message);
            Assert.IsType<InvalidOperationException>(ex.InnerException);
        }

        [Fact]
        public void GetProperties()
        {
            Expression<Func<Person, object>> expression = x => x.Id;

            var properties = ExpressionProcessor.GetProperties(expression);
            Assert.Equal("Id", string.Join('.', properties.Select(x => x.Name)));
        }

        [Fact]
        public void GetProperties_Checked()
        {
            Expression<Func<Person, object>> expression = x => checked((long)x.Id);

            var properties = ExpressionProcessor.GetProperties(expression);
            Assert.Equal("Id", string.Join('.', properties.Select(x => x.Name)));
        }

        [Fact]
        public void GetProperties_Nested()
        {
            Expression<Func<Person, object>> expression = x => x.Address.Street;

            var properties = ExpressionProcessor.GetProperties(expression);
            Assert.Equal("Address.Street", string.Join('.', properties.Select(x => x.Name)));
        }

        [Fact]
        public void GetProperties_Nested_Deep()
        {
            Expression<Func<Person2, object>> expression = x => x.Address.City.Country.Name;

            var properties = ExpressionProcessor.GetProperties(expression);
            Assert.Equal("Address.City.Country.Name", string.Join('.', properties.Select(x => x.Name)));
        }

        [Fact]
        public void Invalid_GetProperties_Operator()
        {
            Expression<Func<Person, object>> expression = x => -x.Id;

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionProcessor.GetProperties(expression));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        [Fact]
        public void Invalid_GetProperties_Method()
        {
            Expression<Func<Person, object>> expression = x => x.ToString();

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionProcessor.GetProperties(expression));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        [Fact]
        public void Invalid_GetProperties_Property_Method()
        {
            Expression<Func<Person, object>> expression = x => x.Id.ToString();

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionProcessor.GetProperties(expression));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        [Fact]
        public void Invalid_GetProperties_Method_Property()
        {
            Expression<Func<Person, object>> expression = x => x.ToString().Length;

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionProcessor.GetProperties(expression));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        [Fact]
        public void Invalid_GetProperties_Instance()
        {
            Person person = null;
            Expression<Func<object>> expression = () => person.Id;

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionProcessor.GetProperties(expression));
            Assert.Equal("Invalid expression.", ex.Message);
        }

        public static Person personStatic;

        [Fact]
        public void Invalid_GetProperties_Static()
        {
            Expression<Func<object>> expression = () => personStatic.Id;

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionProcessor.GetProperties(expression));
            Assert.Equal("Invalid expression.", ex.Message);
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

        private class CompileTest
        {
            public CompileTest()
            {
            }

            public CompileTest(bool exception)
            {
                throw new NotImplementedException();
            }

            public string field = null;

            public string Property { get; set; }

            public string PropertyException => throw new NotImplementedException();

            public string MethodException() => throw new NotImplementedException();
        }

        private abstract class AbstractTest
        {
        }

        private class CustomTable
        {
        }
    }
}