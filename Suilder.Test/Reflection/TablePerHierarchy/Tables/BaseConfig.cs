namespace Suilder.Test.Reflection.TablePerHierarchy.Tables
{
    public abstract class BaseConfig
    {
        public virtual int Id { get; set; }

        public virtual string Guid { get; set; }

        public virtual string Name { get; set; }
    }
}