namespace Suilder.Reflection
{
    /// <summary>
    /// Marks a type as nested.
    /// <para>This attribute is always processed even if the attributes are disabled.</para>
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Struct)]
    public class NestedAttribute : System.Attribute
    {
    }
}