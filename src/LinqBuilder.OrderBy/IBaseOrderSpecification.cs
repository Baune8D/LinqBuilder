namespace LinqBuilder.OrderBy
{
    public interface IBaseOrderSpecification<TEntity> : ISpecificationQuery<TEntity>
    {
        IOrderedSpecification<TEntity> ThenBy(IOrderSpecification<TEntity> orderSpecification);
        IOrderedSpecification<TEntity> Skip(int count);
        IOrderedSpecification<TEntity> Take(int count);
    }
}
