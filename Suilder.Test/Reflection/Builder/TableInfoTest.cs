using System;
using System.Collections.Generic;
using Suilder.Engines;
using Suilder.Exceptions;
using Suilder.Reflection.Builder;
using Suilder.Reflection.Builder.Processors;
using Suilder.Test.Reflection.Builder.NoInherit.Tables;
using Xunit;

namespace Suilder.Test.Reflection.Builder
{
    public class TableInfoTest
    {
        [Fact]
        public void Get_Column_Name()
        {
            ITableBuilder tableBuilder = new TableBuilder();

            tableBuilder.Add<Person>()
                .ColumnName(x => x.Id, "Id2")
                .ColumnName(x => x.Address.Street, "Street2")
                .ColumnName(x => x.Department.Id, "DepartmentId2");

            tableBuilder.Add<Department>()
                .ColumnName(x => x.Id, "Id3")
                .ColumnName(x => x.Boss.Id, "BossId3");

            IEngine engine = new Engine(tableBuilder);

            Assert.Equal("Id2", engine.GetInfo(typeof(Person)).GetColumnName("Id"));
            Assert.Equal("Street2", engine.GetInfo(typeof(Person)).GetColumnName("Address.Street"));
            Assert.Equal("DepartmentId2", engine.GetInfo(typeof(Person)).GetColumnName("Department.Id"));

            Assert.Equal("Id3", engine.GetInfo(typeof(Department)).GetColumnName("Id"));
            Assert.Equal("BossId3", engine.GetInfo(typeof(Department)).GetColumnName("Boss.Id"));
        }

        [Fact]
        public void Get_Column_Name_Property_Not_Registered()
        {
            ITableBuilder tableBuilder = new TableBuilder();

            tableBuilder.Add<Person>();

            IEngine engine = new Engine(tableBuilder);

            Exception ex = Assert.Throws<InvalidConfigurationException>(() =>
                engine.GetInfo(typeof(Person)).GetColumnName("FullName"));
            Assert.Equal($"The property \"FullName\" for type \"{typeof(Person)}\" is not registered.", ex.Message);
        }

        [Fact]
        public void Get_Column_Name_Expression()
        {
            ITableBuilder tableBuilder = new TableBuilder();

            tableBuilder.Add<Person>()
                .ColumnName(x => x.Id, "Id2")
                .ColumnName(x => x.Address.Street, "Street2")
                .ColumnName(x => x.Department.Id, "DepartmentId2");

            tableBuilder.Add<Department>()
                .ColumnName(x => x.Id, "Id3")
                .ColumnName(x => x.Boss.Id, "BossId3");

            IEngine engine = new Engine(tableBuilder);

            Assert.Equal("Id2", engine.GetInfo<Person>().GetColumnName(x => x.Id));
            Assert.Equal("Street2", engine.GetInfo<Person>().GetColumnName(x => x.Address.Street));
            Assert.Equal("DepartmentId2", engine.GetInfo<Person>().GetColumnName(x => x.Department.Id));

            Assert.Equal("Id3", engine.GetInfo<Department>().GetColumnName(x => x.Id));
            Assert.Equal("BossId3", engine.GetInfo<Department>().GetColumnName(x => x.Boss.Id));
        }

        [Fact]
        public void Get_Column_Name_Expression_Property_Not_Registered()
        {
            ITableBuilder tableBuilder = new TableBuilder();

            tableBuilder.Add<Person>();

            IEngine engine = new Engine(tableBuilder);

            Exception ex = Assert.Throws<InvalidConfigurationException>(() =>
                engine.GetInfo<Person>().GetColumnName(x => x.FullName));
            Assert.Equal($"The property \"FullName\" for type \"{typeof(Person)}\" is not registered.", ex.Message);
        }

        [Fact]
        public void Get_Table_Metadata()
        {
            ITableBuilder tableBuilder = new TableBuilder()
                .AddProcessor(new DefaultMetadataProcessor());

            tableBuilder.Add<Person>()
                .AddTableMetadata("Key1", "Val1_Person")
                .AddTableMetadata("Key2", "Val2_Person");

            tableBuilder.Add<Department>()
                .AddTableMetadata("Key1", "Val1_Department")
                .AddTableMetadata("Key2", "Val2_Department");

            IEngine engine = new Engine(tableBuilder);

            Assert.Equal("Val1_Person", engine.GetInfo<Person>().GetTableMetadata("Key1"));
            Assert.Equal("Val2_Person", engine.GetInfo<Person>().GetTableMetadata("Key2"));

            Assert.Equal("Val1_Department", engine.GetInfo<Department>().GetTableMetadata("Key1"));
            Assert.Equal("Val2_Department", engine.GetInfo<Department>().GetTableMetadata("Key2"));

            Assert.Null(engine.GetInfo<Person>().GetTableMetadata("Key3"));
            Assert.Null(engine.GetInfo<Department>().GetTableMetadata("Key3"));
        }

        [Fact]
        public void Get_Table_Metadata_Cast()
        {
            ITableBuilder tableBuilder = new TableBuilder()
                .AddProcessor(new DefaultMetadataProcessor());

            tableBuilder.Add<Person>()
                .AddTableMetadata("Key_Int", 1)
                .AddTableMetadata("Key_Bool", true)
                .AddTableMetadata("Key_String", "Id_Person");

            IEngine engine = new Engine(tableBuilder);

            Assert.Equal(1, engine.GetInfo<Person>().GetTableMetadata<int>("Key_Int"));
            Assert.True(engine.GetInfo<Person>().GetTableMetadata<bool>("Key_Bool"));
            Assert.Equal("Id_Person", engine.GetInfo<Person>().GetTableMetadata<string>("Key_String"));

            Assert.Equal(0, engine.GetInfo<Person>().GetTableMetadata<int>("Key_Int2"));
            Assert.Null(engine.GetInfo<Person>().GetTableMetadata<int?>("Key_Int2"));

            Assert.False(engine.GetInfo<Person>().GetTableMetadata<bool>("Key_Bool2"));
            Assert.Null(engine.GetInfo<Person>().GetTableMetadata<bool?>("Key_Bool2"));

            Assert.Null(engine.GetInfo<Person>().GetTableMetadata<string>("Key_String2"));
        }

        [Fact]
        public void Get_Table_Metadata_Default_Value()
        {
            ITableBuilder tableBuilder = new TableBuilder()
                .AddProcessor(new DefaultMetadataProcessor());

            tableBuilder.Add<Person>()
                .AddTableMetadata("Key_Int", 1)
                .AddTableMetadata("Key_Bool", true)
                .AddTableMetadata("Key_String", "Id_Person");

            IEngine engine = new Engine(tableBuilder);

            Assert.Equal(1, engine.GetInfo<Person>().GetTableMetadata("Key_Int", 0));
            Assert.True(engine.GetInfo<Person>().GetTableMetadata("Key_Bool", false));
            Assert.Equal("Id_Person", engine.GetInfo<Person>().GetTableMetadata<string>("Key_String", null));

            Assert.Equal(1, engine.GetInfo<Person>().GetTableMetadata("Key_Int2", 1));
            Assert.Equal(1, engine.GetInfo<Person>().GetTableMetadata<int?>("Key_Int2", 1));

            Assert.True(engine.GetInfo<Person>().GetTableMetadata("Key_Bool2", true));
            Assert.True(engine.GetInfo<Person>().GetTableMetadata<bool?>("Key_Bool2", true));

            Assert.Equal("Default", engine.GetInfo<Person>().GetTableMetadata("Key_String2", "Default"));
        }

        [Fact]
        public void Get_All_Member_Metadata()
        {
            ITableBuilder tableBuilder = new TableBuilder()
                .AddProcessor(new DefaultMetadataProcessor());

            tableBuilder.Add<Person>()
                .AddMetadata(x => x.Id, "Id1", "Id1_Person")
                .AddMetadata(x => x.Id, "Id2", "Id2_Person")
                .AddMetadata(x => x.Guid, "Guid1", "Guid1_Person")
                .AddMetadata(x => x.Guid, "Guid2", "Guid2_Person");

            tableBuilder.Add<Department>()
                .AddMetadata(x => x.Id, "Id1", "Id1_Department")
                .AddMetadata(x => x.Id, "Id2", "Id2_Department")
                .AddMetadata(x => x.Guid, "Guid1", "Guid1_Department")
                .AddMetadata(x => x.Guid, "Guid2", "Guid2_Department");

            IEngine engine = new Engine(tableBuilder);

            Assert.Equal(new Dictionary<string, object>
            {
                ["Id1"] = "Id1_Person",
                ["Id2"] = "Id2_Person"
            }, engine.GetInfo(typeof(Person)).GetMetadata("Id"));

            Assert.Equal(new Dictionary<string, object>
            {
                ["Guid1"] = "Guid1_Person",
                ["Guid2"] = "Guid2_Person"
            }, engine.GetInfo(typeof(Person)).GetMetadata("Guid"));

            Assert.Equal(new Dictionary<string, object>
            {
                ["Id1"] = "Id1_Department",
                ["Id2"] = "Id2_Department"
            }, engine.GetInfo(typeof(Department)).GetMetadata("Id"));

            Assert.Equal(new Dictionary<string, object>
            {
                ["Guid1"] = "Guid1_Department",
                ["Guid2"] = "Guid2_Department"
            }, engine.GetInfo(typeof(Department)).GetMetadata("Guid"));

            Assert.Equal(new Dictionary<string, object>(), engine.GetInfo(typeof(Person)).GetMetadata("Name"));
            Assert.Equal(new Dictionary<string, object>(), engine.GetInfo(typeof(Department)).GetMetadata("Name"));
        }

        [Fact]
        public void Get_All_Member_Metadata_Expression()
        {
            ITableBuilder tableBuilder = new TableBuilder()
                .AddProcessor(new DefaultMetadataProcessor());

            tableBuilder.Add<Person>()
                .AddMetadata(x => x.Id, "Id1", "Id1_Person")
                .AddMetadata(x => x.Id, "Id2", "Id2_Person")
                .AddMetadata(x => x.Guid, "Guid1", "Guid1_Person")
                .AddMetadata(x => x.Guid, "Guid2", "Guid2_Person");

            tableBuilder.Add<Department>()
                .AddMetadata(x => x.Id, "Id1", "Id1_Department")
                .AddMetadata(x => x.Id, "Id2", "Id2_Department")
                .AddMetadata(x => x.Guid, "Guid1", "Guid1_Department")
                .AddMetadata(x => x.Guid, "Guid2", "Guid2_Department");

            IEngine engine = new Engine(tableBuilder);

            Assert.Equal(new Dictionary<string, object>
            {
                ["Id1"] = "Id1_Person",
                ["Id2"] = "Id2_Person"
            }, engine.GetInfo<Person>().GetMetadata(x => x.Id));

            Assert.Equal(new Dictionary<string, object>
            {
                ["Guid1"] = "Guid1_Person",
                ["Guid2"] = "Guid2_Person"
            }, engine.GetInfo<Person>().GetMetadata(x => x.Guid));

            Assert.Equal(new Dictionary<string, object>
            {
                ["Id1"] = "Id1_Department",
                ["Id2"] = "Id2_Department"
            }, engine.GetInfo<Department>().GetMetadata(x => x.Id));

            Assert.Equal(new Dictionary<string, object>
            {
                ["Guid1"] = "Guid1_Department",
                ["Guid2"] = "Guid2_Department"
            }, engine.GetInfo<Department>().GetMetadata(x => x.Guid));

            Assert.Equal(new Dictionary<string, object>(), engine.GetInfo<Person>().GetMetadata(x => x.Name));
            Assert.Equal(new Dictionary<string, object>(), engine.GetInfo<Department>().GetMetadata(x => x.Name));
        }

        [Fact]
        public void Get_Member_Metadata()
        {
            ITableBuilder tableBuilder = new TableBuilder()
                .AddProcessor(new DefaultMetadataProcessor());

            tableBuilder.Add<Person>()
                .AddMetadata(x => x.Id, "Id1", "Id1_Person")
                .AddMetadata(x => x.Id, "Id2", "Id2_Person")
                .AddMetadata(x => x.Guid, "Guid1", "Guid1_Person")
                .AddMetadata(x => x.Guid, "Guid2", "Guid2_Person");

            tableBuilder.Add<Department>()
                .AddMetadata(x => x.Id, "Id1", "Id1_Department")
                .AddMetadata(x => x.Id, "Id2", "Id2_Department")
                .AddMetadata(x => x.Guid, "Guid1", "Guid1_Department")
                .AddMetadata(x => x.Guid, "Guid2", "Guid2_Department");

            IEngine engine = new Engine(tableBuilder);

            Assert.Equal("Id1_Person", engine.GetInfo(typeof(Person)).GetMetadata("Id", "Id1"));
            Assert.Equal("Id2_Person", engine.GetInfo(typeof(Person)).GetMetadata("Id", "Id2"));
            Assert.Equal("Guid1_Person", engine.GetInfo(typeof(Person)).GetMetadata("Guid", "Guid1"));
            Assert.Equal("Guid2_Person", engine.GetInfo(typeof(Person)).GetMetadata("Guid", "Guid2"));

            Assert.Equal("Id1_Department", engine.GetInfo(typeof(Department)).GetMetadata("Id", "Id1"));
            Assert.Equal("Id2_Department", engine.GetInfo(typeof(Department)).GetMetadata("Id", "Id2"));
            Assert.Equal("Guid1_Department", engine.GetInfo(typeof(Department)).GetMetadata("Guid", "Guid1"));
            Assert.Equal("Guid2_Department", engine.GetInfo(typeof(Department)).GetMetadata("Guid", "Guid2"));

            Assert.Null(engine.GetInfo(typeof(Person)).GetMetadata("Id", "Id3"));
            Assert.Null(engine.GetInfo(typeof(Department)).GetMetadata("Id", "Id3"));

            Assert.Null(engine.GetInfo(typeof(Person)).GetMetadata("Name", "Name1"));
            Assert.Null(engine.GetInfo(typeof(Department)).GetMetadata("Name", "Name1"));
        }

        [Fact]
        public void Get_Member_Metadata_Expression()
        {
            ITableBuilder tableBuilder = new TableBuilder()
                .AddProcessor(new DefaultMetadataProcessor());

            tableBuilder.Add<Person>()
                .AddMetadata(x => x.Id, "Id1", "Id1_Person")
                .AddMetadata(x => x.Id, "Id2", "Id2_Person")
                .AddMetadata(x => x.Guid, "Guid1", "Guid1_Person")
                .AddMetadata(x => x.Guid, "Guid2", "Guid2_Person");

            tableBuilder.Add<Department>()
                .AddMetadata(x => x.Id, "Id1", "Id1_Department")
                .AddMetadata(x => x.Id, "Id2", "Id2_Department")
                .AddMetadata(x => x.Guid, "Guid1", "Guid1_Department")
                .AddMetadata(x => x.Guid, "Guid2", "Guid2_Department");

            IEngine engine = new Engine(tableBuilder);

            Assert.Equal("Id1_Person", engine.GetInfo<Person>().GetMetadata(x => x.Id, "Id1"));
            Assert.Equal("Id2_Person", engine.GetInfo<Person>().GetMetadata(x => x.Id, "Id2"));
            Assert.Equal("Guid1_Person", engine.GetInfo<Person>().GetMetadata(x => x.Guid, "Guid1"));
            Assert.Equal("Guid2_Person", engine.GetInfo<Person>().GetMetadata(x => x.Guid, "Guid2"));

            Assert.Equal("Id1_Department", engine.GetInfo<Department>().GetMetadata(x => x.Id, "Id1"));
            Assert.Equal("Id2_Department", engine.GetInfo<Department>().GetMetadata(x => x.Id, "Id2"));
            Assert.Equal("Guid1_Department", engine.GetInfo<Department>().GetMetadata(x => x.Guid, "Guid1"));
            Assert.Equal("Guid2_Department", engine.GetInfo<Department>().GetMetadata(x => x.Guid, "Guid2"));

            Assert.Null(engine.GetInfo<Person>().GetMetadata(x => x.Id, "Id3"));
            Assert.Null(engine.GetInfo<Department>().GetMetadata(x => x.Id, "Id3"));

            Assert.Null(engine.GetInfo<Person>().GetMetadata(x => x.Name, "Name1"));
            Assert.Null(engine.GetInfo<Department>().GetMetadata(x => x.Name, "Name1"));
        }

        [Fact]
        public void Get_Member_Metadata_Cast()
        {
            ITableBuilder tableBuilder = new TableBuilder()
                .AddProcessor(new DefaultMetadataProcessor());

            tableBuilder.Add<Person>()
                .AddMetadata(x => x.Id, "Id_Int", 1)
                .AddMetadata(x => x.Id, "Id_Bool", true)
                .AddMetadata(x => x.Id, "Id_String", "Id_Person");

            IEngine engine = new Engine(tableBuilder);

            Assert.Equal(1, engine.GetInfo(typeof(Person)).GetMetadata<int>("Id", "Id_Int"));
            Assert.True(engine.GetInfo(typeof(Person)).GetMetadata<bool>("Id", "Id_Bool"));
            Assert.Equal("Id_Person", engine.GetInfo(typeof(Person)).GetMetadata<string>("Id", "Id_String"));

            Assert.Equal(0, engine.GetInfo(typeof(Person)).GetMetadata<int>("Id", "Id_Int2"));
            Assert.Null(engine.GetInfo(typeof(Person)).GetMetadata<int?>("Id", "Id_Int2"));

            Assert.False(engine.GetInfo(typeof(Person)).GetMetadata<bool>("Id", "Id_Bool2"));
            Assert.Null(engine.GetInfo(typeof(Person)).GetMetadata<bool?>("Id", "Id_Bool2"));

            Assert.Null(engine.GetInfo(typeof(Person)).GetMetadata<string>("Id", "Id_String2"));
        }

        [Fact]
        public void Get_Member_Metadata_Expression_Cast()
        {
            ITableBuilder tableBuilder = new TableBuilder()
                .AddProcessor(new DefaultMetadataProcessor());

            tableBuilder.Add<Person>()
                .AddMetadata(x => x.Id, "Id_Int", 1)
                .AddMetadata(x => x.Id, "Id_Bool", true)
                .AddMetadata(x => x.Id, "Id_String", "Id_Person");

            IEngine engine = new Engine(tableBuilder);

            Assert.Equal(1, engine.GetInfo<Person>().GetMetadata<int>(x => x.Id, "Id_Int"));
            Assert.True(engine.GetInfo<Person>().GetMetadata<bool>(x => x.Id, "Id_Bool"));
            Assert.Equal("Id_Person", engine.GetInfo<Person>().GetMetadata<string>(x => x.Id, "Id_String"));

            Assert.Equal(0, engine.GetInfo<Person>().GetMetadata<int>(x => x.Id, "Id_Int2"));
            Assert.Null(engine.GetInfo<Person>().GetMetadata<int?>(x => x.Id, "Id_Int2"));

            Assert.False(engine.GetInfo<Person>().GetMetadata<bool>(x => x.Id, "Id_Bool2"));
            Assert.Null(engine.GetInfo<Person>().GetMetadata<bool?>(x => x.Id, "Id_Bool2"));

            Assert.Null(engine.GetInfo<Person>().GetMetadata<string>(x => x.Id, "Id_String2"));
        }

        [Fact]
        public void Get_Member_Metadata_Default_Value()
        {
            ITableBuilder tableBuilder = new TableBuilder()
                .AddProcessor(new DefaultMetadataProcessor());

            tableBuilder.Add<Person>()
                .AddMetadata(x => x.Id, "Id_Int", 1)
                .AddMetadata(x => x.Id, "Id_Bool", true)
                .AddMetadata(x => x.Id, "Id_String", "Id_Person");

            IEngine engine = new Engine(tableBuilder);

            Assert.Equal(1, engine.GetInfo(typeof(Person)).GetMetadata("Id", "Id_Int", 0));
            Assert.True(engine.GetInfo(typeof(Person)).GetMetadata("Id", "Id_Bool", false));
            Assert.Equal("Id_Person", engine.GetInfo(typeof(Person)).GetMetadata<string>("Id", "Id_String", null));

            Assert.Equal(1, engine.GetInfo(typeof(Person)).GetMetadata("Id", "Id_Int2", 1));
            Assert.Equal(1, engine.GetInfo(typeof(Person)).GetMetadata<int?>("Id", "Id_Int2", 1));

            Assert.True(engine.GetInfo(typeof(Person)).GetMetadata("Id", "Id_Bool2", true));
            Assert.True(engine.GetInfo(typeof(Person)).GetMetadata<bool?>("Id", "Id_Bool2", true));

            Assert.Equal("Default", engine.GetInfo(typeof(Person)).GetMetadata("Id", "Id_String2", "Default"));
        }

        [Fact]
        public void Get_Member_Metadata_Expression_Default_Value()
        {
            ITableBuilder tableBuilder = new TableBuilder()
                .AddProcessor(new DefaultMetadataProcessor());

            tableBuilder.Add<Person>()
                .AddMetadata(x => x.Id, "Id_Int", 1)
                .AddMetadata(x => x.Id, "Id_Bool", true)
                .AddMetadata(x => x.Id, "Id_String", "Id_Person");

            IEngine engine = new Engine(tableBuilder);

            Assert.Equal(1, engine.GetInfo<Person>().GetMetadata(x => x.Id, "Id_Int", 0));
            Assert.True(engine.GetInfo<Person>().GetMetadata(x => x.Id, "Id_Bool", false));
            Assert.Equal("Id_Person", engine.GetInfo<Person>().GetMetadata<string>(x => x.Id, "Id_String", null));

            Assert.Equal(1, engine.GetInfo<Person>().GetMetadata(x => x.Id, "Id_Int2", 1));
            Assert.Equal(1, engine.GetInfo<Person>().GetMetadata<int?>(x => x.Id, "Id_Int2", 1));

            Assert.True(engine.GetInfo<Person>().GetMetadata(x => x.Id, "Id_Bool2", true));
            Assert.True(engine.GetInfo<Person>().GetMetadata<bool?>(x => x.Id, "Id_Bool2", true));

            Assert.Equal("Default", engine.GetInfo<Person>().GetMetadata(x => x.Id, "Id_String2", "Default"));
        }
    }
}