using System.Linq;

namespace LinqBuilder.Ordering
{
    public interface IQueryOrderSpecification<T>
    {
        IOrderedQueryable<T> Invoke(IQueryable<T> query);
    }
}
