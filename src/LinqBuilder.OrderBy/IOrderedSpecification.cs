namespace LinqBuilder.OrderBy
{
    public interface IOrderedSpecification<TEntity> : IBaseOrderSpecification<TEntity>
    {
        Ordering<TEntity> GetOrdering();
    }
}
