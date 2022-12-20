using System.Linq.Expressions;

namespace Microsoft.EntityFrameworkCore;

public static class EntityFrameworkCoreQueryableExtensions
{
    public static IQueryable<TSource> Where<TSource>(
        this IQueryable<TSource> source,
        bool condition,
        Expression<Func<TSource, bool>> predicate)
    {
        return condition ? source.Where(predicate) : source;
    }

    public static IQueryable<TSource> Where<TSource>(
        this IQueryable<TSource> source,
        bool condition,
        Expression<Func<TSource, int, bool>> predicate)
    {
        return condition ? source.Where(predicate) : source;
    }

    public static IQueryable<TResult> LeftJoin<TOuter, TInner, TKey, TResult>(
        this IQueryable<TOuter> outer,
        IQueryable<TInner> inner,
        Expression<Func<TOuter, TKey>> outerKeySelector,
        Expression<Func<TInner, TKey>> innerKeySelector,
        Func<TOuter, TInner?, TResult> resultSelector)
    {
        return outer.GroupJoin(inner, outerKeySelector, innerKeySelector, (outer, inners) => new
        {
            outer,
            inners
        }).SelectMany(collectionSelector => collectionSelector.inners.DefaultIfEmpty(), (r, s) => resultSelector(r.outer, s));
    }
}
