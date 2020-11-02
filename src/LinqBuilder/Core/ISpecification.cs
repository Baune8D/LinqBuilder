namespace LinqBuilder.Core
{
    public interface ISpecification<TEntity>
        where TEntity : class
    {
        InternalConfiguration<TEntity> Internal { get; }
    }
}
