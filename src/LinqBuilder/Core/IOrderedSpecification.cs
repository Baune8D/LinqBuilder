namespace LinqBuilder.Core
{
    public interface IOrderedSpecification<TEntity> : ISpecification<TEntity>
        where TEntity : class
    {
    }
}
