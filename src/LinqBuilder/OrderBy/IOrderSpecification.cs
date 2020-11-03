using System.Collections.Generic;
using System.Linq;

namespace LinqBuilder.OrderBy
{
    public interface IOrderSpecification<TEntity> : ISpecification<TEntity>
        where TEntity : class
    {
        IOrderedQueryable<TEntity> InvokeSort(IQueryable<TEntity> query);

        IOrderedEnumerable<TEntity> InvokeSort(IEnumerable<TEntity> collection);

        IOrderedQueryable<TEntity> InvokeSort(IOrderedQueryable<TEntity> query);

        IOrderedEnumerable<TEntity> InvokeSort(IOrderedEnumerable<TEntity> collection);
    }
}
