using System;
using Suilder.Engines;
using Suilder.Exceptions;
using Suilder.Functions;
using Suilder.Reflection.Builder;
using Suilder.Test.Reflection;
using Suilder.Test.Reflection.Builder.TablePerType.Tables;
using Xunit;

namespace Suilder.Test.Engines
{
    public class EngineTest
    {
        [Fact]
        public void Engine_Name()
        {
            IEngine engine = new Engine();

            Assert.Null(engine.Options.Name);
        }

        [Fact]
        public void Escape_Characters()
        {
            IEngine engine = new Engine();

            Assert.Equal('\"', engine.Options.EscapeStart);
            Assert.Equal('\"', engine.Options.EscapeEnd);
        }

        [Fact]
        public void Escape_Name()
        {
            IEngine engine = new Engine();

            Assert.Equal("\"Id\"", engine.EscapeName("Id"));
            Assert.Equal("\"person\".\"Id\"", engine.EscapeName("person.Id"));
            Assert.Equal("\"dbo\".\"person\".\"Id\"", engine.EscapeName("dbo.person.Id"));
        }

        [Fact]
        public void Escape_Name_UpperCase()
        {
            IEngine engine = new Engine();
            engine.Options.UpperCaseNames = true;

            Assert.Equal("\"ID\"", engine.EscapeName("Id"));
            Assert.Equal("\"PERSON\".\"ID\"", engine.EscapeName("person.Id"));
            Assert.Equal("\"DBO\".\"PERSON\".\"ID\"", engine.EscapeName("dbo.person.Id"));
        }

        [Fact]
        public void Escape_Name_LowerCase()
        {
            IEngine engine = new Engine();
            engine.Options.LowerCaseNames = true;

            Assert.Equal("\"id\"", engine.EscapeName("Id"));
            Assert.Equal("\"person\".\"id\"", engine.EscapeName("person.Id"));
            Assert.Equal("\"dbo\".\"person\".\"id\"", engine.EscapeName("dbo.person.Id"));
        }

        [Fact]
        public void Escape_Name_With_Quotes()
        {
            IEngine engine = new Engine();

            Assert.Equal("\"Id\"", engine.EscapeName("\"Id\""));
            Assert.Equal("\"person\".\"Id\"", engine.EscapeName("\"person\".\"Id\""));
            Assert.Equal("\"dbo\".\"person\".\"Id\"", engine.EscapeName("\"dbo\".\"person\".\"Id\""));
            Assert.Equal("\";DELETE FROM person\"", engine.EscapeName("\";DELETE FROM person"));
        }

        [Fact]
        public void Add_Function()
        {
            IEngine engine = new Engine();
            engine.AddFunction("CONCAT");

            Assert.True(engine.ContainsFunction("CONCAT"));
        }

        [Fact]
        public void Remove_Function()
        {
            IEngine engine = new Engine();
            engine.AddFunction("CONCAT");
            engine.RemoveFunction("CONCAT");

            Assert.False(engine.ContainsFunction("CONCAT"));
        }

        [Fact]
        public void Clear_Functions()
        {
            IEngine engine = new Engine();
            engine.AddFunction("CONCAT");
            engine.AddFunction("SUBSTRING");
            engine.ClearFunctions();

            Assert.False(engine.ContainsFunction("CONCAT"));
            Assert.False(engine.ContainsFunction("SUBSTRING"));
        }

        [Fact]
        public void Get_Function()
        {
            IEngine engine = new Engine();
            engine.AddFunction("CONCAT");

            IFunctionData funcData = engine.GetFunction("CONCAT");

            Assert.Equal("CONCAT", funcData.Name);
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