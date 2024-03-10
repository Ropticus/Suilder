using System;
using Suilder.Functions;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Functions
{
    public class SqlExpInvalidCallTest : BuilderBaseTest
    {
        [Fact]
        public void Function()
        {
            Person person = new Person();

            Exception ex = Assert.Throws<NotSupportedException>(() =>
                SqlExp.Function("CONCAT", person.Name, person.Surname));
            Assert.Equal("Only for expressions.", ex.Message);
        }

        [Fact]
        public void Function_Name_Only()
        {
            Exception ex = Assert.Throws<NotSupportedException>(() => SqlExp.Function("NOW"));
            Assert.Equal("Only for expressions.", ex.Message);
        }

        [Fact]
        public void ColName()
        {
            Person person = new Person();

            Exception ex = Assert.Throws<NotSupportedException>(() => SqlExp.ColName(person.Id));
            Assert.Equal("Only for expressions.", ex.Message);
        }

        [Fact]
        public void As()
        {
            Person person = new Person();

            Exception ex = Assert.Throws<NotSupportedException>(() => SqlExp.As<string>(person.Id));
            Assert.Equal("Only for expressions.", ex.Message);
        }

        [Fact]
        public void Val()
        {
            Person person = new Person();

            Exception ex = Assert.Throws<NotSupportedException>(() => SqlExp.Val(person.Id));
            Assert.Equal("Only for expressions.", ex.Message);
        }

        [Fact]
        public void Abs()
        {
            Person person = new Person();

            Exception ex = Assert.Throws<NotSupportedException>(() => SqlExp.Abs(person.Salary));
            Assert.Equal("Only for expressions.", ex.Message);
        }

        [Fact]
        public void Avg()
        {
            Person person = new Person();

            Exception ex = Assert.Throws<NotSupportedException>(() => SqlExp.Avg(person.Salary));
            Assert.Equal("Only for expressions.", ex.Message);
        }

        [Fact]
        public void AvgDistinct()
        {
            Person person = new Person();

            Exception ex = Assert.Throws<NotSupportedException>(() => SqlExp.AvgDistinct(person.Salary));
            Assert.Equal("Only for expressions.", ex.Message);
        }

        [Fact]
        public void Cast()
        {
            Person person = new Person();

            Exception ex = Assert.Throws<NotSupportedException>(() => SqlExp.Cast(person.Salary, sql.Type("VARCHAR")));
            Assert.Equal("Only for expressions.", ex.Message);
        }

        [Fact]
        public void Cast_Generic()
        {
            Person person = new Person();

            Exception ex = Assert.Throws<NotSupportedException>(() =>
                SqlExp.Cast<string>(person.Salary, sql.Type("VARCHAR")));
            Assert.Equal("Only for expressions.", ex.Message);
        }

        [Fact]
        public void Ceiling()
        {
            Person person = new Person();

            Exception ex = Assert.Throws<NotSupportedException>(() => SqlExp.Ceiling(person.Salary));
            Assert.Equal("Only for expressions.", ex.Message);
        }

        [Fact]
        public void Coalesce()
        {
            Person person = new Person();

            Exception ex = Assert.Throws<NotSupportedException>(() => SqlExp.Coalesce(person.Name, person.Surname));
            Assert.Equal("Only for expressions.", ex.Message);
        }

        [Fact]
        public void Coalesce_Object_Overload()
        {
            Person person = new Person();

            Exception ex = Assert.Throws<NotSupportedException>(() => SqlExp.Coalesce(person.Name, person.Salary));
            Assert.Equal("Only for expressions.", ex.Message);
        }

        [Fact]
        public void Concat()
        {
            Person person = new Person();

            Exception ex = Assert.Throws<NotSupportedException>(() => SqlExp.Concat(person.Name, person.Surname));
            Assert.Equal("Only for expressions.", ex.Message);
        }

        [Fact]
        public void Count()
        {
            Exception ex = Assert.Throws<NotSupportedException>(() => SqlExp.Count());
            Assert.Equal("Only for expressions.", ex.Message);
        }

        [Fact]
        public void Count_Column()
        {
            Person person = new Person();

            Exception ex = Assert.Throws<NotSupportedException>(() => SqlExp.Count(person.Name));
            Assert.Equal("Only for expressions.", ex.Message);
        }

        [Fact]
        public void Count_Distinct()
        {
            Person person = new Person();

            Exception ex = Assert.Throws<NotSupportedException>(() => SqlExp.CountDistinct(person.Name));
            Assert.Equal("Only for expressions.", ex.Message);
        }

        [Fact]
        public void Floor()
        {
            Person person = new Person();

            Exception ex = Assert.Throws<NotSupportedException>(() => SqlExp.Floor(person.Salary));
            Assert.Equal("Only for expressions.", ex.Message);
        }

        [Fact]
        public void LastInsertId()
        {
            Exception ex = Assert.Throws<NotSupportedException>(() => SqlExp.LastInsertId());
            Assert.Equal("Only for expressions.", ex.Message);
        }

        [Fact]
        public void Length()
        {
            Person person = new Person();

            Exception ex = Assert.Throws<NotSupportedException>(() => SqlExp.Length(person.Name));
            Assert.Equal("Only for expressions.", ex.Message);
        }

        [Fact]
        public void Lower()
        {
            Person person = new Person();

            Exception ex = Assert.Throws<NotSupportedException>(() => SqlExp.Lower(person.Name));
            Assert.Equal("Only for expressions.", ex.Message);
        }

        [Fact]
        public void LTrim()
        {
            Person person = new Person();

            Exception ex = Assert.Throws<NotSupportedException>(() => SqlExp.LTrim(person.Name));
            Assert.Equal("Only for expressions.", ex.Message);
        }

        [Fact]
        public void LTrim_Character()
        {
            Person person = new Person();

            Exception ex = Assert.Throws<NotSupportedException>(() => SqlExp.LTrim(person.Name, ","));
            Assert.Equal("Only for expressions.", ex.Message);
        }

        [Fact]
        public void Max()
        {
            Person person = new Person();

            Exception ex = Assert.Throws<NotSupportedException>(() => SqlExp.Max(person.Salary));
            Assert.Equal("Only for expressions.", ex.Message);
        }

        [Fact]
        public void Min()
        {
            Person person = new Person();

            Exception ex = Assert.Throws<NotSupportedException>(() => SqlExp.Min(person.Salary));
            Assert.Equal("Only for expressions.", ex.Message);
        }

        [Fact]
        public void Now()
        {
            Exception ex = Assert.Throws<NotSupportedException>(() => SqlExp.Now());
            Assert.Equal("Only for expressions.", ex.Message);
        }

        [Fact]
        public void NullIf()
        {
            Person person = new Person();

            Exception ex = Assert.Throws<NotSupportedException>(() => SqlExp.NullIf(person.Name, "empty"));
            Assert.Equal("Only for expressions.", ex.Message);
        }

        [Fact]
        public void Replace()
        {
            Person person = new Person();

            Exception ex = Assert.Throws<NotSupportedException>(() => SqlExp.Replace(person.Name, "a", "b"));
            Assert.Equal("Only for expressions.", ex.Message);
        }

        [Fact]
        public void Round()
        {
            Person person = new Person();

            Exception ex = Assert.Throws<NotSupportedException>(() => SqlExp.Round(person.Salary));
            Assert.Equal("Only for expressions.", ex.Message);
        }

        [Fact]
        public void Round_Places()
        {
            Person person = new Person();

            Exception ex = Assert.Throws<NotSupportedException>(() => SqlExp.Round(person.Salary, 2));
            Assert.Equal("Only for expressions.", ex.Message);
        }

        [Fact]
        public void RTrim()
        {
            Person person = new Person();

            Exception ex = Assert.Throws<NotSupportedException>(() => SqlExp.RTrim(person.Name));
            Assert.Equal("Only for expressions.", ex.Message);
        }

        [Fact]
        public void RTrim_Character()
        {
            Person person = new Person();

            Exception ex = Assert.Throws<NotSupportedException>(() => SqlExp.RTrim(person.Name, ","));
            Assert.Equal("Only for expressions.", ex.Message);
        }

        [Fact]
        public void Substring()
        {
            Person person = new Person();

            Exception ex = Assert.Throws<NotSupportedException>(() => SqlExp.Substring(person.Name, 2, 4));
            Assert.Equal("Only for expressions.", ex.Message);
        }

        [Fact]
        public void Sum()
        {
            Person person = new Person();

            Exception ex = Assert.Throws<NotSupportedException>(() => SqlExp.Sum(person.Salary));
            Assert.Equal("Only for expressions.", ex.Message);
        }

        [Fact]
        public void SumDistinct()
        {
            Person person = new Person();

            Exception ex = Assert.Throws<NotSupportedException>(() => SqlExp.SumDistinct(person.Salary));
            Assert.Equal("Only for expressions.", ex.Message);
        }

        [Fact]
        public void Trim()
        {
            Person person = new Person();

            Exception ex = Assert.Throws<NotSupportedException>(() => SqlExp.Trim(person.Name));
            Assert.Equal("Only for expressions.", ex.Message);
        }

        [Fact]
        public void Trim_Character()
        {
            Person person = new Person();

            Exception ex = Assert.Throws<NotSupportedException>(() => SqlExp.Trim(person.Name, ","));
            Assert.Equal("Only for expressions.", ex.Message);
        }

        [Fact]
        public void Upper()
        {
            Person person = new Person();

            Exception ex = Assert.Throws<NotSupportedException>(() => SqlExp.Upper(person.Name));
            Assert.Equal("Only for expressions.", ex.Message);
        }
    }
}