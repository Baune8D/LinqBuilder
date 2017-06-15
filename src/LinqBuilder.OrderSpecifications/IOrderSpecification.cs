using System.Collections.Generic;
using System.Linq;

namespace LinqBuilder.OrderSpecifications
{
    public interface IOrderSpecification<T>
    {
        ThenBySpecification<T> ThenBy(IOrderSpecification<T> other);
        IOrderedQueryable<T> Invoke(IQueryable<T> query);
        IOrderedQueryable<T> Invoke(IOrderedQueryable<T> query);
        IOrderedEnumerable<T> Invoke(IEnumerable<T> collection);
        IOrderedEnumerable<T> Invoke(IOrderedEnumerable<T> collection);
    }
}
