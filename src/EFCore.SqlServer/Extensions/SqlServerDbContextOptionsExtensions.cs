using Netoysoft.EntityFrameworkCore.SqlServer.Infrastructure;

namespace Microsoft.EntityFrameworkCore;

public static class SqlServerDbContextOptionsExtensions
{
    public static DbContextOptionsBuilder UseNetoysSqlServer(
        this DbContextOptionsBuilder optionsBuilder,
        Action<SqlServerDbContextOptionsBuilder>? etherealOptionsAction = default)
    {
        optionsBuilder.AddOrUpdateExtension<SqlServerDbContextOptionsExtension>();

        etherealOptionsAction?.Invoke(new SqlServerDbContextOptionsBuilder(optionsBuilder));

        return optionsBuilder;
    }
}
