using LinqBuilder.Specifications;

namespace LinqBuilder.OrderSpecifications
{
    public interface ICompositeSpecification<T>
        : IOrderSpecification<T>, ISpecification<T> { }
}
