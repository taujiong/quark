using System.Linq.Expressions;

namespace System.Linq;

public static class QueryableExtensions
{
  public static IQueryable<T> PageBy<T>(this IQueryable<T> query, int skipCount, int maxResultCount)
  {
    if (query == null) throw new ArgumentNullException(nameof(query));

    return query.Skip(skipCount).Take(maxResultCount);
  }

  public static IQueryable<T> WhereIf<T>(
      this IQueryable<T> query,
      bool condition,
      Expression<Func<T, bool>> predicate
  )
  {
    if (query == null) throw new ArgumentNullException(nameof(query));

    return condition
        ? query.Where(predicate)
        : query;
  }
}
