using LinqBuilder.Specifications;

namespace LinqBuilder.OrderSpecifications
{
    public interface ICompositeOrderSpecification<T>
        : IOrderSpecification<T>, ISpecification<T> { }
}
