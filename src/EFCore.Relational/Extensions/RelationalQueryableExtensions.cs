using System.Linq.Expressions;

namespace Microsoft.EntityFrameworkCore;

public static class EtherealRelationalQueryableExtensions
{
    public static IReadOnlyCollection<StoreObjectType> StoreObjectTypes => Enum.GetValues<StoreObjectType>();

    public static string? GetStroeColumn<TEntity, TProperty>(this DbSet<TEntity> source, string name) where TEntity : class
    {
        var storeObject = StoreObjectTypes.Select(storeObjectType => StoreObjectIdentifier.Create(source.EntityType, storeObjectType)).FirstOrDefault(storeObjectIdentifier => storeObjectIdentifier is not null).GetValueOrDefault();
        return source.EntityType.FindProperty(name)?.GetColumnName(storeObject);
    }

    public static string? GetStroeColumn<TEntity, TProperty>(this DbSet<TEntity> source, Expression<Func<TEntity, TProperty>> propertyExpression) where TEntity : class
    {
        var storeObject = StoreObjectTypes.Select(storeObjectType => StoreObjectIdentifier.Create(source.EntityType, storeObjectType)).FirstOrDefault(storeObjectIdentifier => storeObjectIdentifier is not null).GetValueOrDefault();
        return source.EntityType.FindProperty(propertyExpression.GetMemberAccess())?.GetColumnName(storeObject);
    }
}