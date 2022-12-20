namespace Microsoft.EntityFrameworkCore;

[AttributeUsage(AttributeTargets.Class)]
public sealed class SoftDeleteAttribute : Attribute
{
    public SoftDeleteAttribute(string? columnName = null, string? comment = null)
    {
        ColumnName = string.IsNullOrWhiteSpace(columnName) ? "is_deleted" : columnName;
        Comment = string.IsNullOrWhiteSpace(comment) ? "soft deleted" : comment;
    }

    public string ColumnName { get; }

    public string Comment { get; }
}