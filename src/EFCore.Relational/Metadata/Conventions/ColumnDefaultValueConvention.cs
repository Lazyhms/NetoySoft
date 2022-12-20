using System.ComponentModel;

namespace Netoysoft.EntityFrameworkCore.Metadata.Conventions;

public sealed class ColumnDefaultValueConvention : PropertyAttributeConventionBase<DefaultValueAttribute>
{
    public ColumnDefaultValueConvention(ProviderConventionSetBuilderDependencies dependencies) : base(dependencies)
    {
    }

    protected override void ProcessPropertyAdded(
       IConventionPropertyBuilder propertyBuilder,
       DefaultValueAttribute attribute,
       MemberInfo clrMember,
       IConventionContext context)
    {
        if (attribute.Value is not null)
        {
            propertyBuilder.HasDefaultValue(attribute.Value, true);
        }
    }
}