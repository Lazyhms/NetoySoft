namespace Netoysoft.EntityFrameworkCore.Metadata.Conventions;

public sealed class ColumnUpdateIgnoreConvention : PropertyAttributeConventionBase<UpdateIgnoreAttribute>
{
    public ColumnUpdateIgnoreConvention(ProviderConventionSetBuilderDependencies dependencies) : base(dependencies)
    {
    }

    protected override void ProcessPropertyAdded(
        IConventionPropertyBuilder propertyBuilder,
        UpdateIgnoreAttribute attribute,
        MemberInfo clrMember,
        IConventionContext context)
    {
        if (propertyBuilder.CanSetAfterSave(PropertySaveBehavior.Ignore))
        {
            propertyBuilder.AfterSave(PropertySaveBehavior.Ignore);
        }
    }
}