using LinqBuilder.Specifications;

namespace LinqBuilder.OrderSpecifications
{
    public interface ICompositeSpecification<T>
        : IOrderSpecification<T>, ISpecification<T>
    {
        ICompositeSpecification<T> Skip(int count);
        ICompositeSpecification<T> Take(int count);
    }
}
