namespace Suilder.Test.Reflection.Builder.TableNested.Tables
{
    public class Person : BaseConfig
    {
        public virtual string SurName { get; set; }

        public virtual string FullName => $"{Name} {SurName}".TrimEnd();

        public virtual Employee Employee { get; set; }
    }
}