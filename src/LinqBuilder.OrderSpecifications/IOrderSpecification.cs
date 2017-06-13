using System.Collections.Generic;
using System.Linq;

namespace LinqBuilder.OrderSpecifications
{
    public interface IOrderSpecification<T>
    {
        IOrderedQueryable<T> Invoke(IQueryable<T> query);
        IOrderedEnumerable<T> Invoke(IEnumerable<T> collection);
    }
}
