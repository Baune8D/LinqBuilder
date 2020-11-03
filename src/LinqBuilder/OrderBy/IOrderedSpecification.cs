namespace LinqBuilder.OrderBy
{
    public interface IOrderedSpecification<TEntity> : ISpecification<TEntity>
        where TEntity : class
    {
    }
}
