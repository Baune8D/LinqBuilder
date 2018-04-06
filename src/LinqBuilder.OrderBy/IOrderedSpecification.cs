namespace LinqBuilder.OrderBy
{
    public interface IOrderedSpecification<TEntity> : ISpecificationQuery<TEntity>
    {
        IOrderedSpecification<TEntity> ThenBy(IOrderSpecification<TEntity> orderSpecification);
        IOrderedSpecification<TEntity> Skip(int count);
        IOrderedSpecification<TEntity> Take(int count);
    }
}
