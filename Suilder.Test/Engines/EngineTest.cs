using System;
using System.Collections.Generic;
using Suilder.Engines;
using Suilder.Exceptions;
using Suilder.Functions;
using Suilder.Operators;
using Suilder.Reflection.Builder;
using Suilder.Test.Reflection;
using Suilder.Test.Reflection.Builder.TablePerType.Tables;
using Xunit;

namespace Suilder.Test.Engines
{
    public class EngineTest
    {
        protected IEngine engine = new Engine();

        [Fact]
        public void Engine_Name()
        {
            Assert.Null(engine.Options.Name);
        }

        [Fact]
        public void Escape_Characters()
        {
            Assert.Equal('\"', engine.Options.EscapeStart);
            Assert.Equal('\"', engine.Options.EscapeEnd);
        }

        [Fact]
        public void Parameters()
        {
            Assert.Equal("@p", engine.Options.ParameterPrefix);
            Assert.True(engine.Options.ParameterIndex);
        }

        [Fact]
        public void Escape_Name()
        {
            Assert.Equal("\"Id\"", engine.EscapeName("Id"));
            Assert.Equal("\"person\".\"Id\"", engine.EscapeName("person.Id"));
            Assert.Equal("\"dbo\".\"person\".\"Id\"", engine.EscapeName("dbo.person.Id"));
        }

        [Fact]
        public void Escape_Name_UpperCase()
        {
            engine.Options.UpperCaseNames = true;

            Assert.Equal("\"ID\"", engine.EscapeName("Id"));
            Assert.Equal("\"PERSON\".\"ID\"", engine.EscapeName("person.Id"));
            Assert.Equal("\"DBO\".\"PERSON\".\"ID\"", engine.EscapeName("dbo.person.Id"));
        }

        [Fact]
        public void Escape_Name_LowerCase()
        {
            engine.Options.LowerCaseNames = true;

            Assert.Equal("\"id\"", engine.EscapeName("Id"));
            Assert.Equal("\"person\".\"id\"", engine.EscapeName("person.Id"));
            Assert.Equal("\"dbo\".\"person\".\"id\"", engine.EscapeName("dbo.person.Id"));
        }

        [Fact]
        public void Escape_Name_With_Quotes()
        {
            Assert.Equal("\"Id\"", engine.EscapeName("\"Id\""));
            Assert.Equal("\"person\".\"Id\"", engine.EscapeName("\"person\".\"Id\""));
            Assert.Equal("\"dbo\".\"person\".\"Id\"", engine.EscapeName("\"dbo\".\"person\".\"Id\""));
            Assert.Equal("\";DELETE FROM person\"", engine.EscapeName("\";DELETE FROM person"));
        }

        public static IEnumerable<object[]> DataRegister
        {
            get
            {
                return new List<object[]>
                {
                    new object[] { "Name", "Name" },
                    new object[] { "Name", "NAME" },
                    new object[] { "Name", "name" },
                    new object[] { "UPPER_NAME", "Upper_Name" },
                    new object[] { "UPPER_NAME", "UPPER_NAME" },
                    new object[] { "UPPER_NAME", "upper_name" },
                    new object[] { "lower_name", "Lower_Name" },
                    new object[] { "lower_name", "LOWER_NAME" },
                    new object[] { "lower_name", "lower_name" }
                };
            }
        }

        public static IEnumerable<object[]> DataRegisterTranslation
        {
            get
            {
                return new List<object[]>
                {
                    new object[] { "Name", "Name", "Name_Sql" },
                    new object[] { "Name", "NAME", "UPPER_NAME_SQL" },
                    new object[] { "Name", "name", "lower_name_sql" },
                    new object[] { "UPPER_NAME", "Upper_Name", "Name_Sql" },
                    new object[] { "UPPER_NAME", "UPPER_NAME", "UPPER_NAME_SQL" },
                    new object[] { "UPPER_NAME", "upper_name","lower_name_sql" },
                    new object[] { "lower_name", "Lower_Name", "Name_Sql" },
                    new object[] { "lower_name", "LOWER_NAME", "UPPER_NAME_SQL" },
                    new object[] { "lower_name", "lower_name", "lower_name_sql" }
                };
            }
        }

        [Theory]
        [MemberData(nameof(DataRegisterTranslation))]
        public void Add_Operator(string op, string opKey, string opSql)
        {
            engine.AddOperator(op, opSql);
            Assert.True(engine.ContainsOperator(opKey));
        }

        [Theory]
        [MemberData(nameof(DataRegisterTranslation))]
        public void Add_Operator_Function(string op, string opKey, string opSql)
        {
            engine.AddOperator(op, opSql, true);
            Assert.True(engine.ContainsOperator(opKey));
        }

        [Theory]
        [MemberData(nameof(DataRegisterTranslation))]
        public void Remove_Operator(string op, string opKey, string opSql)
        {
            engine.AddOperator(op, opSql);
            engine.RemoveOperator(opKey);

            Assert.False(engine.ContainsOperator(op));
        }

        [Fact]
        public void Clear_Operators()
        {
            engine.AddOperator("Name", "Name_Sql");
            engine.AddOperator("UPPER_NAME", "UPPER_NAME_SQL");
            engine.AddOperator("lower_name", "lower_name_sql");
            engine.ClearOperators();

            Assert.False(engine.ContainsOperator("Name"));
            Assert.False(engine.ContainsOperator("UPPER_NAME"));
            Assert.False(engine.ContainsOperator("lower_name"));
        }

        [Theory]
        [MemberData(nameof(DataRegisterTranslation))]
        public void Get_Operator(string op, string opKey, string opSql)
        {
            engine.AddOperator(op, opSql);

            IOperatorInfo opInfo = engine.GetOperator(opKey);
            Assert.Equal(opSql, opInfo.Op);
        }

        [Theory]
        [MemberData(nameof(DataRegisterTranslation))]
        public void Get_Operator_Function(string op, string opKey, string opSql)
        {
            engine.AddOperator(op, opSql, true);

            IOperatorInfo opInfo = engine.GetOperator(opKey);
            Assert.Equal(opSql, opInfo.Op);
            Assert.True(opInfo.Function);
        }

        [Theory]
        [MemberData(nameof(DataRegister))]
        public void Add_Function(string name, string nameKey)
        {
            engine.AddFunction(name);
            Assert.True(engine.ContainsFunction(nameKey));
        }

        [Theory]
        [MemberData(nameof(DataRegisterTranslation))]
        public void Add_Function_With_Translation(string name, string nameKey, string nameSql)
        {
            engine.AddFunction(name, nameSql);
            Assert.True(engine.ContainsFunction(nameKey));
        }

        [Theory]
        [MemberData(nameof(DataRegister))]
        public void Remove_Function(string name, string nameKey)
        {
            engine.AddFunction(name);
            engine.RemoveFunction(nameKey);

            Assert.False(engine.ContainsFunction(name));
        }

        [Fact]
        public void Clear_Functions()
        {
            engine.AddFunction("Name");
            engine.AddFunction("UPPER_NAME");
            engine.AddFunction("lower_name");
            engine.ClearFunctions();

            Assert.False(engine.ContainsFunction("Name"));
            Assert.False(engine.ContainsFunction("UPPER_NAME"));
            Assert.False(engine.ContainsFunction("lower_name"));
        }

        [Theory]
        [MemberData(nameof(DataRegister))]
        public void Get_Function(string name, string nameKey)
        {
            engine.AddFunction(name);

            IFunctionInfo funcInfo = engine.GetFunction(nameKey);
            Assert.Equal(name, funcInfo.Name);
        }

        [Theory]
        [MemberData(nameof(DataRegisterTranslation))]
        public void Get_Function_With_Translation(string name, string nameKey, string nameSql)
        {
            engine.AddFunction(name, nameSql);

            IFunctionInfo funcInfo = engine.GetFunction(nameKey);
            Assert.Equal(nameSql, funcInfo.Name);
        }

        [Fact]
        public void Get_Registered_Types()
        {
            ITableBuilder tableBuilder = new TableBuilder();

            tableBuilder.Add<Person>();

            tableBuilder.Add<Employee>();

            tableBuilder.Add<Department>();

            IEngine engine = new Engine(tableBuilder);

            Assert.Equal(new Type[] { typeof(Person), typeof(Employee), typeof(Department) }, engine.GetRegisteredTypes());
        }

        [Fact]
        public void Get_Info()
        {
            ITableBuilder tableBuilder = new TableBuilder();

            tableBuilder.Add<Person>();

            tableBuilder.Add<Employee>();

            tableBuilder.Add<Department>();

            IEngine engine = new Engine(tableBuilder);

            Assert.Equal(tableBuilder.GetConfig<Person>(), engine.GetInfo(typeof(Person)));
            Assert.Equal(tableBuilder.GetConfig<Employee>(), engine.GetInfo(typeof(Employee)));
            Assert.Equal(tableBuilder.GetConfig<Department>(), engine.GetInfo(typeof(Department)));
        }

        [Fact]
        public void Get_Info_Not_Registered()
        {
            ITableBuilder tableBuilder = new TableBuilder();

            IEngine engine = new Engine(tableBuilder);

            Exception ex = Assert.Throws<InvalidConfigurationException>(() => engine.GetInfo(typeof(Person)));
            Assert.Equal($"The type \"{typeof(Person)}\" is not registered.", ex.Message);
        }

        [Fact]
        public void Get_Info_Generic()
        {
            ITableBuilder tableBuilder = new TableBuilder();

            tableBuilder.Add<Person>();

            tableBuilder.Add<Employee>();

            tableBuilder.Add<Department>();

            IEngine engine = new Engine(tableBuilder);

            Assert.Equal(tableBuilder.GetConfig<Person>(), engine.GetInfo<Person>());
            Assert.Equal(tableBuilder.GetConfig<Employee>(), engine.GetInfo<Employee>());
            Assert.Equal(tableBuilder.GetConfig<Department>(), engine.GetInfo<Department>());
        }

        [Fact]
        public void Get_Info_Generic_Not_Registered()
        {
            ITableBuilder tableBuilder = new TableBuilder();

            IEngine engine = new Engine(tableBuilder);

            Exception ex = Assert.Throws<InvalidConfigurationException>(() => engine.GetInfo<Person>());
            Assert.Equal($"The type \"{typeof(Person)}\" is not registered.", ex.Message);
        }

        [Fact]
        public void Get_Info_Or_Default()
        {
            ITableBuilder tableBuilder = new TableBuilder();

            tableBuilder.Add<Person>();

            tableBuilder.Add<Employee>();

            tableBuilder.Add<Department>();

            IEngine engine = new Engine(tableBuilder);

            Assert.Equal(tableBuilder.GetConfig<Person>(), engine.GetInfoOrDefault(typeof(Person)));
            Assert.Equal(tableBuilder.GetConfig<Employee>(), engine.GetInfoOrDefault(typeof(Employee)));
            Assert.Equal(tableBuilder.GetConfig<Department>(), engine.GetInfoOrDefault(typeof(Department)));
        }

        [Fact]
        public void Get_Info_Or_Default_Not_Registered()
        {
            ITableBuilder tableBuilder = new TableBuilder();

            IEngine engine = new Engine(tableBuilder);

            Assert.Null(engine.GetInfoOrDefault(typeof(Person)));
            Assert.Null(engine.GetInfoOrDefault(typeof(Employee)));
            Assert.Null(engine.GetInfoOrDefault(typeof(Department)));
        }

        [Fact]
        public void Get_Info_Or_Default_Generic()
        {
            ITableBuilder tableBuilder = new TableBuilder();

            tableBuilder.Add<Person>();

            tableBuilder.Add<Employee>();

            tableBuilder.Add<Department>();

            IEngine engine = new Engine(tableBuilder);

            Assert.Equal(tableBuilder.GetConfig<Person>(), engine.GetInfoOrDefault<Person>());
            Assert.Equal(tableBuilder.GetConfig<Employee>(), engine.GetInfoOrDefault<Employee>());
            Assert.Equal(tableBuilder.GetConfig<Department>(), engine.GetInfoOrDefault<Department>());
        }

        [Fact]
        public void Get_Info_Or_Default_Generic_Not_Registered()
        {
            ITableBuilder tableBuilder = new TableBuilder();

            IEngine engine = new Engine(tableBuilder);

            Assert.Null(engine.GetInfoOrDefault<Person>());
            Assert.Null(engine.GetInfoOrDefault<Employee>());
            Assert.Null(engine.GetInfoOrDefault<Department>());
        }

        [Fact]
        public void Is_Table()
        {
            ITableBuilder tableBuilder = new TableBuilder();

            tableBuilder.Add<Person>();

            tableBuilder.Add<Employee>();

            tableBuilder.Add<Department>();

            IEngine engine = new Engine(tableBuilder);

            Assert.False(engine.IsTable(typeof(BaseConfig)));
            Assert.True(engine.IsTable(typeof(Person)));
            Assert.True(engine.IsTable(typeof(Employee)));
            Assert.True(engine.IsTable(typeof(Department)));
        }

        [Fact]
        public void Is_Table_Generic()
        {
            ITableBuilder tableBuilder = new TableBuilder();

            tableBuilder.Add<Person>();

            tableBuilder.Add<Employee>();

            tableBuilder.Add<Department>();

            IEngine engine = new Engine(tableBuilder);

            Assert.False(engine.IsTable<BaseConfig>());
            Assert.True(engine.IsTable<Person>());
            Assert.True(engine.IsTable<Employee>());
            Assert.True(engine.IsTable<Department>());
        }
    }
}