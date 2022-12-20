using Microsoft.Extensions.DependencyInjection;
using Netoysoft.EntityFrameworkCore.Infrastructure;

namespace Netoysoft.EntityFrameworkCore.SqlServer.Infrastructure;

public class SqlServerDbContextOptionsExtension : DbContextOptionsExtension
{
    public override void ApplyCustomServices(IServiceCollection services)
    {
    }

    private sealed class SqlServerExtensionInfo : ExtensionInfo
    {
        public SqlServerExtensionInfo(IDbContextOptionsExtension extension)
            : base(extension)
        {
        }

        public override void PopulateDebugInfo(IDictionary<string, string> debugInfo)
            => debugInfo["Netoysoft:EFCore:SqlServer"] = "1";
    }
}
