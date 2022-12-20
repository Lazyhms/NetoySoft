namespace Netoysoft.EntityFrameworkCore.Metadata.Conventions;

public sealed class ColumnAddIgnoreConvention : PropertyAttributeConventionBase<AddIgnoreAttribute>
{
    public ColumnAddIgnoreConvention(ProviderConventionSetBuilderDependencies dependencies) : base(dependencies)
    {
    }

    protected override void ProcessPropertyAdded(
        IConventionPropertyBuilder propertyBuilder,
        AddIgnoreAttribute attribute,
        MemberInfo clrMember,
        IConventionContext context)
    {
        if (propertyBuilder.CanSetBeforeSave(PropertySaveBehavior.Ignore))
        {
            propertyBuilder.BeforeSave(PropertySaveBehavior.Ignore);
        };
    }
}