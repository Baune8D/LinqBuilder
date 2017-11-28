using System.Collections.Generic;
using System.Linq;

namespace LinqBuilder.Specifications
{
    public interface ISpecification<T>
    {
        IQueryable<T> Invoke(IQueryable<T> query);
        IEnumerable<T> Invoke(IEnumerable<T> collection);
    }
}
