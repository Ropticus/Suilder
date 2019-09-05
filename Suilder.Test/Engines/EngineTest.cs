using System.Collections.Generic;
using Suilder.Engines;
using Suilder.Functions;
using Suilder.Reflection;
using Xunit;

namespace Suilder.Test.Engines
{
    public class EngineTest
    {
        [Fact]
        public void Escape_Characters()
        {
            IEngine engine = new Engine();

            Assert.Equal('\"', engine.Options.EscapeStart);
            Assert.Equal('\"', engine.Options.EscapeEnd);
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
        public void Is_Table()
        {
            TableBuilder tableBuilder = new TableBuilder();

            tableBuilder.Add<Person>();

            tableBuilder.Add<Department>();

            IEngine engine = new Engine(tableBuilder);

            Assert.False(engine.IsTable(typeof(BaseConfig)));
            Assert.True(engine.IsTable(typeof(Person)));
            Assert.True(engine.IsTable(typeof(Department)));
        }

        [Fact]
        public void Get_Table_Name()
        {
            TableBuilder tableBuilder = new TableBuilder();

            tableBuilder.Add<Person>();

            tableBuilder.Add<Department>();

            IEngine engine = new Engine(tableBuilder);

            Assert.Equal("Person", engine.GetTableName(typeof(Person)));
            Assert.Equal("Department", engine.GetTableName(typeof(Department)));
        }

        [Fact]
        public void Get_Primary_Keys()
        {
            TableBuilder tableBuilder = new TableBuilder();

            tableBuilder.Add<Person>();

            tableBuilder.Add<Department>(config => config
                .PrimaryKey(x => x.Guid));

            IEngine engine = new Engine(tableBuilder);

            Assert.Equal(new string[] { "Id" }, engine.GetPrimaryKeys(typeof(Person)));
            Assert.Equal(new string[] { "Guid" }, engine.GetPrimaryKeys(typeof(Department)));
        }

        [Fact]
        public void Get_Columns()
        {
            TableBuilder tableBuilder = new TableBuilder();

            tableBuilder.Add<Person>();

            tableBuilder.Add<Department>();

            IEngine engine = new Engine(tableBuilder);

            Assert.Equal(new string[] { "Id", "Guid", "Name", "SurName", "DepartmentId", "Department.Id" },
                engine.GetColumns(typeof(Person)));
            Assert.Equal(new string[] { "Id", "Guid", "Name", "Boss.Id" }, engine.GetColumns(typeof(Department)));
        }

        [Fact]
        public void Get_Column_Names()
        {
            TableBuilder tableBuilder = new TableBuilder();

            tableBuilder.Add<Person>(config => config
                .ColumnName(x => x.Id, "Id2"));

            tableBuilder.Add<Department>(config => config
                .ColumnName(x => x.Id, "Id3"));

            IEngine engine = new Engine(tableBuilder);

            Assert.Equal(new string[] { "Id2", "Guid", "Name", "SurName", "DepartmentId" },
                engine.GetColumnNames(typeof(Person)));
            Assert.Equal(new string[] { "Id3", "Guid", "Name", "BossId" }, engine.GetColumnNames(typeof(Department)));
        }

        [Fact]
        public void Get_Column_Name()
        {
            TableBuilder tableBuilder = new TableBuilder();

            tableBuilder.Add<Person>(config => config
                .ColumnName(x => x.Id, "Id2"));

            tableBuilder.Add<Department>(config => config
                .ColumnName(x => x.Id, "Id3"));

            IEngine engine = new Engine(tableBuilder);

            Assert.Equal("Id2", engine.GetColumnName(typeof(Person), "Id"));
            Assert.Equal("Id3", engine.GetColumnName(typeof(Department), "Id"));
        }

        [Fact]
        public void Get_Column_Names_Dic()
        {
            TableBuilder tableBuilder = new TableBuilder();

            tableBuilder.Add<Person>(config => config
                .ColumnName(x => x.Id, "Id2"));

            tableBuilder.Add<Department>(config => config
                .ColumnName(x => x.Id, "Id3"));

            IEngine engine = new Engine(tableBuilder);

            Assert.Equal(new Dictionary<string, string>
            {
                ["Id"] = "Id2",
                ["Guid"] = "Guid",
                ["Name"] = "Name",
                ["SurName"] = "SurName",
                ["DepartmentId"] = "DepartmentId",
                ["Department.Id"] = "DepartmentId"
            }, engine.GetColumnNamesDic(typeof(Person)));

            Assert.Equal(new Dictionary<string, string>
            {
                ["Id"] = "Id3",
                ["Guid"] = "Guid",
                ["Name"] = "Name",
                ["Boss.Id"] = "BossId"
            }, engine.GetColumnNamesDic(typeof(Department)));
        }


        public abstract class BaseConfig
        {
            public int Id { get; set; }

            public string Guid { get; set; }

            public string Name { get; set; }
        }

        public class Department : BaseConfig
        {
            public Person Boss { get; set; }

            public List<Person> Employees { get; set; }
        }

        public class Person : BaseConfig
        {
            public string SurName { get; set; }

            public int DepartmentId { get; set; }

            public Department Department { get; set; }
        }
    }
}