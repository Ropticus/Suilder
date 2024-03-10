using System.Collections.Generic;
using Suilder.Reflection;
using Suilder.Reflection.Builder;
using Xunit;

namespace Suilder.Test.Reflection.TablePerType.Attributes
{
    public class PrimaryKeySingleTest : BaseTest
    {
        protected override void InitConfig()
        {
            tableBuilder.Add<Person>();

            tableBuilder.Add<Employee>();

            tableBuilder.Add<Department>();
        }

        [Fact]
        public void Primary_Keys()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo employeeInfo = tableBuilder.GetConfig<Employee>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { "Guid" }, personInfo.PrimaryKeys);
            Assert.Equal(new string[] { "Guid" }, employeeInfo.PrimaryKeys);
            Assert.Equal(new string[] { "Guid" }, deptInfo.PrimaryKeys);
        }

        [Fact]
        public void Foreign_Keys()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo employeeInfo = tableBuilder.GetConfig<Employee>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { }, personInfo.ForeignKeys);
            Assert.Equal(new string[] { "Department.Guid" }, employeeInfo.ForeignKeys);
            Assert.Equal(new string[] { "Boss.Guid" }, deptInfo.ForeignKeys);
        }

        [Fact]
        public void Columns()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo employeeInfo = tableBuilder.GetConfig<Employee>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { "Guid", "Id", "Name", "Surname", "Address.Street", "Address.City" },
                personInfo.Columns);
            Assert.Equal(new string[] { "Guid", "Salary", "DepartmentId", "Department.Guid", "Image" },
                employeeInfo.Columns);
            Assert.Equal(new string[] { "Guid", "Id", "Name", "Boss.Guid", "Tags" }, deptInfo.Columns);
        }

        [Fact]
        public void Column_Names_Dic()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo employeeInfo = tableBuilder.GetConfig<Employee>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new Dictionary<string, string>
            {
                ["Guid"] = "Guid",
                ["Id"] = "Id",
                ["Name"] = "Name",
                ["Surname"] = "Surname",
                ["Address.Street"] = "AddressStreet",
                ["Address.City"] = "AddressCity"
            }, personInfo.ColumnNamesDic);

            Assert.Equal(new Dictionary<string, string>
            {
                ["Guid"] = "Guid",
                ["Salary"] = "Salary",
                ["DepartmentId"] = "DepartmentId",
                ["Department.Guid"] = "DepartmentGuid",
                ["Image"] = "Image"
            }, employeeInfo.ColumnNamesDic);

            Assert.Equal(new Dictionary<string, string>
            {
                ["Guid"] = "Guid",
                ["Id"] = "Id",
                ["Name"] = "Name",
                ["Boss.Guid"] = "BossGuid",
                ["Tags"] = "Tags"
            }, deptInfo.ColumnNamesDic);
        }

        [Fact]
        public void Column_Names()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo employeeInfo = tableBuilder.GetConfig<Employee>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { "Guid", "Id", "Name", "Surname", "AddressStreet", "AddressCity" },
                personInfo.ColumnNames);
            Assert.Equal(new string[] { "Guid", "Salary", "DepartmentId", "DepartmentGuid", "Image" },
                employeeInfo.ColumnNames);
            Assert.Equal(new string[] { "Guid", "Id", "Name", "BossGuid", "Tags" }, deptInfo.ColumnNames);
        }

        [Nested]
        public class Address
        {
            public string Street { get; set; }

            public string City { get; set; }
        }

        public abstract class BaseConfig
        {
            public virtual int Id { get; set; }

            [PrimaryKey]
            public virtual string Guid { get; set; }

            public virtual string Name { get; set; }
        }

        public class Department : BaseConfig
        {
            public virtual Employee Boss { get; set; }

            public virtual List<Employee> Employees { get; set; }

            public virtual List<string> Tags { get; set; }
        }

        public class Employee : Person
        {
            public virtual decimal Salary { get; set; }

            public virtual int DepartmentId { get; set; }

            public virtual Department Department { get; set; }

            public virtual byte[] Image { get; set; }
        }

        public class Person : BaseConfig
        {
            public virtual string Surname { get; set; }

            public virtual string FullName => $"{Name} {Surname}".TrimEnd();

            public virtual Address Address { get; set; }
        }
    }
}