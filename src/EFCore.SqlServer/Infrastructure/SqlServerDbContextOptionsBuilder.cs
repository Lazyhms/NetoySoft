using Netoysoft.EntityFrameworkCore.Infrastructure;

namespace Netoysoft.EntityFrameworkCore.SqlServer.Infrastructure;

public class SqlServerDbContextOptionsBuilder : DbContextOptionsBuilder<SqlServerDbContextOptionsBuilder, SqlServerDbContextOptionsExtension>
{
    public SqlServerDbContextOptionsBuilder(DbContextOptionsBuilder optionsBuilder)
        : base(optionsBuilder)
    {
    }

}
