namespace LinqBuilder.Core
{
    public interface ISpecification<TEntity>
        where TEntity : class
    {
        LinqBuilder<TEntity> GetLinqBuilder();
    }
}
