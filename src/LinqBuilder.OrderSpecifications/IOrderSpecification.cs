using System.Collections.Generic;
using System.Linq;

namespace LinqBuilder.OrderSpecifications
{
    public interface IOrderSpecification<TEntity> : IBaseOrderSpecification<TEntity>
    {
        IOrderedQueryable<TEntity> InvokeOrdered(IOrderedQueryable<TEntity> query);
        IOrderedQueryable<TEntity> InvokeOrdered(IQueryable<TEntity> query);
        IOrderedEnumerable<TEntity> InvokeOrdered(IOrderedEnumerable<TEntity> collection);
        IOrderedEnumerable<TEntity> InvokeOrdered(IEnumerable<TEntity> collection);
    }
}
