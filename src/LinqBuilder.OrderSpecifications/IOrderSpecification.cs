using System.Collections.Generic;
using System.Linq;

namespace LinqBuilder.OrderSpecifications
{
    public interface IOrderSpecification<T> : IBaseOrderSpecification<T>
    {
        IOrderedQueryable<T> InvokeOrdered(IOrderedQueryable<T> query);
        IOrderedQueryable<T> InvokeOrdered(IQueryable<T> query);
        IOrderedEnumerable<T> InvokeOrdered(IOrderedEnumerable<T> collection);
        IOrderedEnumerable<T> InvokeOrdered(IEnumerable<T> collection);
    }
}
