namespace Microsoft.EntityFrameworkCore;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public sealed class DefaultValueSqlAttribute : Attribute
{
    public DefaultValueSqlAttribute(string? value = null)
    {
        Value = value;
    }

    public string? Value { get; }

}