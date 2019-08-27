namespace Suilder.Test.Reflection.Builder.TablePerType.Tables
{
    public class Person : BaseConfig
    {
        public virtual string SurName { get; set; }

        public virtual string FullName => $"{Name} {SurName}".TrimEnd();

        public virtual Address Address { get; set; }
    }
}