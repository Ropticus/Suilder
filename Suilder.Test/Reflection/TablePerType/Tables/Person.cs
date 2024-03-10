namespace Suilder.Test.Reflection.TablePerType.Tables
{
    public class Person : BaseConfig
    {
        public virtual string Surname { get; set; }

        public virtual string FullName => $"{Name} {Surname}".TrimEnd();

        public virtual Address Address { get; set; }
    }
}