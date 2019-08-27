using System;
using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Exceptions;
using Suilder.Functions;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Functions
{
    public class FunctionHelperTest : BuilderBaseTest
    {
        [Fact]
        public void Not_Supported()
        {
            engine.AddFunction("LASTINSERTID", FunctionHelper.NotSupported);

            IFunction func = (IFunction)sql.Val(() => SqlExp.Function("LASTINSERTID"));

            Exception ex = Assert.Throws<ClauseNotSupportedException>(() => engine.Compile(func));
            Assert.Equal("Function \"LASTINSERTID\" is not supported in this engine.", ex.Message);
        }

        [Fact]
        public void NameOnly()
        {
            engine.AddFunction("SYSDATE", FunctionHelper.NameOnly);

            IFunction func = (IFunction)sql.Val(() => SqlExp.Function("SYSDATE"));

            QueryResult result = engine.Compile(func);

            Assert.Equal("SYSDATE", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void NameOnly_Invalid()
        {
            engine.AddFunction("SYSDATE", FunctionHelper.NameOnly);

            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Function("SYSDATE", person.Name));

            Exception ex = Assert.Throws<CompileException>(() => engine.Compile(func));
            Assert.Equal("Invalid function \"SYSDATE\", wrong number of parameters.", ex.Message);
        }


        [Fact]
        public void Cast_Invalid()
        {
            engine.AddFunction(FunctionName.Cast, FunctionHelper.Cast);

            IFunction func = (IFunction)sql.Val(() => SqlExp.Function("CAST"));

            Exception ex = Assert.Throws<CompileException>(() => engine.Compile(func));
            Assert.Equal("Invalid function \"CAST\", wrong number of parameters.", ex.Message);
        }

        [Fact]
        public void ConcatOr_Invalid()
        {
            engine.AddFunction(FunctionName.Concat, FunctionHelper.ConcatOr);

            IFunction func = (IFunction)sql.Val(() => SqlExp.Concat());

            Exception ex = Assert.Throws<CompileException>(() => engine.Compile(func));
            Assert.Equal("Invalid function \"CONCAT\", wrong number of parameters.", ex.Message);
        }

        [Fact]
        public void TrimLeading_Invalid()
        {
            engine.AddFunction(FunctionName.LTrim, FunctionHelper.TrimLeading);

            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Function("LTRIM", person.Name, ",", ","));

            Exception ex = Assert.Throws<CompileException>(() => engine.Compile(func));
            Assert.Equal("Invalid function \"LTRIM\", wrong number of parameters.", ex.Message);
        }

        [Fact]
        public void TrimTrailing_Invalid()
        {
            engine.AddFunction(FunctionName.RTrim, FunctionHelper.TrimTrailing);

            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Function("RTRIM", person.Name, ",", ","));

            Exception ex = Assert.Throws<CompileException>(() => engine.Compile(func));
            Assert.Equal("Invalid function \"RTRIM\", wrong number of parameters.", ex.Message);
        }

        [Fact]
        public void TrimBoth_Invalid()
        {
            engine.AddFunction(FunctionName.Trim, FunctionHelper.TrimBoth);

            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Function("TRIM", person.Name, ",", ","));

            Exception ex = Assert.Throws<CompileException>(() => engine.Compile(func));
            Assert.Equal("Invalid function \"TRIM\", wrong number of parameters.", ex.Message);
        }
    }
}