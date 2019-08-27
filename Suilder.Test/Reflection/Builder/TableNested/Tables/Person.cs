namespace Suilder.Test.Reflection.Builder.TableNested.Tables
{
    public class Person : BaseConfig
    {
        public string SurName { get; set; }

        public string FullName => $"{Name} {SurName}".TrimEnd();

        public Employee Employee { get; set; }
    }
}