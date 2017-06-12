using System.Linq;

namespace LinqBuilder.OrderSpecifications
{
    public interface IOrderSpecification<T>
    {
        IOrderedQueryable<T> Invoke(IQueryable<T> query);
    }
}
