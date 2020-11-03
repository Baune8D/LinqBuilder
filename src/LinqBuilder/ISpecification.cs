using LinqBuilder.Internal;

namespace LinqBuilder
{
    public interface ISpecification<TEntity>
        where TEntity : class
    {
        SpecificationBase<TEntity> Internal { get; }
    }
}
