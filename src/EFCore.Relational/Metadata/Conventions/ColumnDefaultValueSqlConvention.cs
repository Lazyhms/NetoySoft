namespace Netoysoft.EntityFrameworkCore.Metadata.Conventions;

public sealed class ColumnDefaultValueSqlConvention : PropertyAttributeConventionBase<DefaultValueSqlAttribute>
{
    public ColumnDefaultValueSqlConvention(ProviderConventionSetBuilderDependencies dependencies) : base(dependencies)
    {
    }

    protected override void ProcessPropertyAdded(
        IConventionPropertyBuilder propertyBuilder,
        DefaultValueSqlAttribute attribute,
        MemberInfo clrMember,
        IConventionContext context)
    {
        if (!string.IsNullOrWhiteSpace(attribute.Value))
        {
            propertyBuilder.HasDefaultValueSql(attribute.Value, true);
        }
    }
}