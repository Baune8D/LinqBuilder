using System.Collections.Generic;
using System.Linq;

namespace LinqBuilder.Specifications
{
    public interface ISpecification<TEntity>
    {
        IQueryable<TEntity> Invoke(IQueryable<TEntity> query);
        IEnumerable<TEntity> Invoke(IEnumerable<TEntity> collection);
    }
}
