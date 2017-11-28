using LinqBuilder.Specifications;

namespace LinqBuilder.OrderSpecifications
{
    public interface IBaseOrderSpecification<T> : ISpecification<T>
    {
        ICompositeOrderSpecification<T> ThenBy(IOrderSpecification<T> other);
        ICompositeOrderSpecification<T> Skip(int count);
        ICompositeOrderSpecification<T> Take(int count);
    }
}
