using System.ComponentModel;

namespace Netoysoft.EntityFrameworkCore.Infrastructure;

public abstract class DbContextOptionsBuilder<TBuilder, TExtension> : IRelationalDbContextOptionsBuilderInfrastructure
    where TBuilder : DbContextOptionsBuilder<TBuilder, TExtension>
    where TExtension : DbContextOptionsExtension, new()
{
    public DbContextOptionsBuilder OptionsBuilder { get; }

    public DbContextOptionsBuilder(DbContextOptionsBuilder optionsBuilder) => OptionsBuilder = optionsBuilder;

    DbContextOptionsBuilder IRelationalDbContextOptionsBuilderInfrastructure.OptionsBuilder => OptionsBuilder;

    protected virtual TBuilder WithOption(Func<TExtension, TExtension> setAction)
    {
        ((IDbContextOptionsBuilderInfrastructure)OptionsBuilder).AddOrUpdateExtension(
            setAction(OptionsBuilder.Options.FindExtension<TExtension>() ?? new TExtension()));
        return (TBuilder)this;
    }

    #region Hidden System.Object members

    [EditorBrowsable(EditorBrowsableState.Never)]
    public override bool Equals(object? obj)
=> base.Equals(obj);

    [EditorBrowsable(EditorBrowsableState.Never)]
    public override int GetHashCode()
        => base.GetHashCode();

    [EditorBrowsable(EditorBrowsableState.Never)]
    public override string? ToString()
        => base.ToString();

    #endregion
}
