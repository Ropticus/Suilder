namespace Suilder.Test.Reflection.TableNested.Tables
{
    public class Person : BaseConfig
    {
        public virtual string Surname { get; set; }

        public virtual string FullName => $"{Name} {Surname}".TrimEnd();

        public virtual Employee Employee { get; set; }
    }
}