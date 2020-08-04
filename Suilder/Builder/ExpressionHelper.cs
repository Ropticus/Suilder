using System;
using System.Linq.Expressions;
using Suilder.Core;

namespace Suilder.Builder
{
    /// <summary>
    /// Helper with predefined delegates to register in <see cref="ExpressionProcessor"/>.
    /// </summary>
    public static class ExpressionHelper
    {
        /// <summary>
        /// Delegate for functions.
        /// </summary>
        /// <value>The delegate.</value>
        public readonly static Func<MethodCallExpression, string, IFunction> Function = (expression, name) =>
        {
            IFunction func = SqlBuilder.Instance.Function(name);
            if (expression.Object != null)
                func.Add(SqlBuilder.Instance.Val(expression.Object));

            foreach (var arg in expression.Arguments)
            {
                func.Add(SqlBuilder.Instance.Val(arg));
            }
            return func;
        };

        /// <summary>
        /// Delegate for functions with params arg.
        /// </summary>
        /// <value>The delegate.</value>
        public readonly static Func<MethodCallExpression, string, IFunction> FunctionParams = (expression, name) =>
        {
            IFunction func = SqlBuilder.Instance.Function(name);
            if (expression.Object != null)
                func.Add(SqlBuilder.Instance.Val(expression.Object));

            for (int i = 0; i < expression.Arguments.Count - 1; i++)
            {
                func.Add(SqlBuilder.Instance.Val(expression.Arguments[i]));
            }
            if (expression.Arguments.Count > 0)
            {
                // Last parameter is params argument
                if (expression.Arguments[expression.Arguments.Count - 1] is NewArrayExpression expParams)
                {
                    foreach (var arg in expParams.Expressions)
                    {
                        func.Add(SqlBuilder.Instance.Val(arg));
                    }
                }
            }

            return func;
        };

        /// <summary>
        /// Delegate for functions where the first parameter is the name of the function.
        /// </summary>
        /// <value>The delegate.</value>
        public readonly static Func<MethodCallExpression, IFunction> FunctionWithName = expression =>
        {
            if (expression.Arguments.Count == 0)
                throw new ArgumentException("Invalid expression, wrong number of parameters.");

            ConstantExpression expName = expression.Arguments[0] as ConstantExpression;
            if (expName == null)
                throw new ArgumentException("Invalid expression, the name must be a constant.");

            IFunction func = SqlBuilder.Instance.Function(((string)expName.Value).ToUpperInvariant());

            if (expression.Arguments.Count > 1)
            {
                if (expression.Arguments[1] is NewArrayExpression expParams)
                {
                    foreach (var arg in expParams.Expressions)
                    {
                        func.Add(SqlBuilder.Instance.Val(arg));
                    }
                }
            }

            return func;
        };

        /// <summary>
        /// Delegate for operators with two params.
        /// </summary>
        /// <value>The delegate.</value>
        public readonly static Func<MethodCallExpression, Func<object, object, IOperator>, IOperator> Operator
            = (expression, op) =>
        {
            if (expression.Object == null)
            {
                if (expression.Arguments.Count != 2)
                    throw new ArgumentException("Invalid expression, wrong number of parameters.");

                return op(SqlBuilder.Instance.Val(expression.Arguments[0]),
                    SqlBuilder.Instance.Val(expression.Arguments[1]));
            }
            else
            {
                if (expression.Arguments.Count != 1)
                    throw new ArgumentException("Invalid expression, wrong number of parameters.");

                return op(SqlBuilder.Instance.Val(expression.Object),
                    SqlBuilder.Instance.Val(expression.Arguments[0]));
            }
        };

        /// <summary>
        /// Delegate for operators with one param.
        /// </summary>
        /// <value>The delegate.</value>
        public readonly static Func<MethodCallExpression, Func<object, IOperator>, IOperator> SingleOperator
            = (expression, op) =>
        {
            if (expression.Object == null)
            {
                if (expression.Arguments.Count != 1)
                    throw new ArgumentException("Invalid expression, wrong number of parameters.");

                return op(SqlBuilder.Instance.Val(expression.Arguments[0]));
            }
            else
            {
                if (expression.Arguments.Count != 0)
                    throw new ArgumentException("Invalid expression, wrong number of parameters.");

                return op(SqlBuilder.Instance.Val(expression.Object));
            }
        };

        /// <summary>
        /// Delegate for "not" operator.
        /// </summary>
        /// <value>The delegate.</value>
        public readonly static Func<MethodCallExpression, IOperator> Not = expression =>
        {
            if (expression.Object == null)
            {
                if (expression.Arguments.Count != 1)
                    throw new ArgumentException("Invalid expression, wrong number of parameters.");

                return SqlBuilder.Instance.Not(SqlBuilder.Instance.Op(expression.Arguments[0]));
            }
            else
            {
                if (expression.Arguments.Count != 0)
                    throw new ArgumentException("Invalid expression, wrong number of parameters.");

                return SqlBuilder.Instance.Not(SqlBuilder.Instance.Val(expression.Object));
            }
        };

        /// <summary>
        /// Delegate for "like" operator with a pattern that match the start of the value.
        /// </summary>
        /// <value>The delegate.</value>
        public readonly static Func<MethodCallExpression, IOperator> LikeStart = expression =>
        {
            if (expression.Object == null)
            {
                if (expression.Arguments.Count != 2)
                    throw new ArgumentException("Invalid expression, wrong number of parameters.");

                return SqlBuilder.Instance.Like(SqlBuilder.Instance.Val(expression.Arguments[0]),
                    SqlBuilder.Instance.ToLikeStart((string)SqlBuilder.Instance.Val(expression.Arguments[1])));
            }
            else
            {
                if (expression.Arguments.Count != 1)
                    throw new ArgumentException("Invalid expression, wrong number of parameters.");

                return SqlBuilder.Instance.Like(SqlBuilder.Instance.Val(expression.Object),
                    SqlBuilder.Instance.ToLikeStart((string)SqlBuilder.Instance.Val(expression.Arguments[0])));
            }
        };

        /// <summary>
        /// Delegate for "like" operator with a pattern that match the end of the value.
        /// </summary>
        /// <value>The delegate.</value>
        public readonly static Func<MethodCallExpression, IOperator> LikeEnd = expression =>
        {
            if (expression.Object == null)
            {
                if (expression.Arguments.Count != 2)
                    throw new ArgumentException("Invalid expression, wrong number of parameters.");

                return SqlBuilder.Instance.Like(SqlBuilder.Instance.Val(expression.Arguments[0]),
                    SqlBuilder.Instance.ToLikeEnd((string)SqlBuilder.Instance.Val(expression.Arguments[1])));
            }
            else
            {
                if (expression.Arguments.Count != 1)
                    throw new ArgumentException("Invalid expression, wrong number of parameters.");

                return SqlBuilder.Instance.Like(SqlBuilder.Instance.Val(expression.Object),
                    SqlBuilder.Instance.ToLikeEnd((string)SqlBuilder.Instance.Val(expression.Arguments[0])));
            }
        };

        /// <summary>
        /// Delegate for "like" operator with a pattern that match anywhere of the value.
        /// </summary>
        /// <value>The delegate.</value>
        public readonly static Func<MethodCallExpression, IOperator> LikeAny = expression =>
        {
            if (expression.Object == null)
            {
                if (expression.Arguments.Count != 2)
                    throw new ArgumentException("Invalid expression, wrong number of parameters.");

                return SqlBuilder.Instance.Like(SqlBuilder.Instance.Val(expression.Arguments[0]),
                    SqlBuilder.Instance.ToLikeAny((string)SqlBuilder.Instance.Val(expression.Arguments[1])));
            }
            else
            {
                if (expression.Arguments.Count != 1)
                    throw new ArgumentException("Invalid expression, wrong number of parameters.");

                return SqlBuilder.Instance.Like(SqlBuilder.Instance.Val(expression.Object),
                    SqlBuilder.Instance.ToLikeAny((string)SqlBuilder.Instance.Val(expression.Arguments[0])));
            }
        };

        /// <summary>
        /// Delegate to compile the expression within the method.
        /// </summary>
        /// <value>The delegate.</value>
        public readonly static Func<MethodCallExpression, object> Val = expression =>
        {
            if (expression.Object == null)
            {
                if (expression.Arguments.Count != 1)
                    throw new ArgumentException("Invalid expression, wrong number of parameters.");

                return ExpressionProcessor.Compile(expression.Arguments[0]);
            }
            else
            {
                if (expression.Arguments.Count != 0)
                    throw new ArgumentException("Invalid expression, wrong number of parameters.");

                return ExpressionProcessor.Compile(expression.Object);
            }
        };
    }
}