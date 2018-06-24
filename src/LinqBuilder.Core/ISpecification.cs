namespace LinqBuilder.Core
{
    public interface ISpecification<TEntity>
        where TEntity : class
    {
        Configuration<TEntity> Internal { get; }
    }
}
