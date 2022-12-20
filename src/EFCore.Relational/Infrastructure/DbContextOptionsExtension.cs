using Microsoft.Extensions.DependencyInjection;
using Netoysoft.EntityFrameworkCore.Extensions;

namespace Netoysoft.EntityFrameworkCore.Infrastructure;

public class DbContextOptionsExtension : IDbContextOptionsExtension
{
    private DbContextOptionsExtensionInfo? _info;

    public DbContextOptionsExtensionInfo Info
        => _info ??= new ExtensionInfo(this);

    public void ApplyServices(IServiceCollection services)
    {
        services.AddEntityFrameworkCoreServices();

        ApplyCustomServices(services);
    }

    public virtual void ApplyCustomServices(IServiceCollection services)
    {
    }

    public virtual void Validate(IDbContextOptions options)
    {
    }

    protected class ExtensionInfo : DbContextOptionsExtensionInfo
    {
        public ExtensionInfo(IDbContextOptionsExtension extension) :
            base(extension)
        {
        }

        private new DbContextOptionsExtension Extension
            => (DbContextOptionsExtension)base.Extension;

        public override bool IsDatabaseProvider
            => false;

        public override int GetServiceProviderHashCode()
            => 0;

        public override bool ShouldUseSameServiceProvider(DbContextOptionsExtensionInfo other)
            => other is ExtensionInfo;

        public override void PopulateDebugInfo(IDictionary<string, string> debugInfo)
            => debugInfo["Netoysoft:EFCore"] = "1";

        public override string LogFragment
            => "using Netoysoft.EFCore ";
    }
}
