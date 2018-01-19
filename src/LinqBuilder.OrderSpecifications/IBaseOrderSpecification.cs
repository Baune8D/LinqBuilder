using LinqBuilder.Specifications;

namespace LinqBuilder.OrderSpecifications
{
    public interface IBaseOrderSpecification<TEntity> : ISpecification<TEntity>
    {
        ICompositeOrderSpecification<TEntity> ThenBy(IOrderSpecification<TEntity> other);
        ICompositeOrderSpecification<TEntity> Skip(int count);
        ICompositeOrderSpecification<TEntity> Take(int count);
    }
}
