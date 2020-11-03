namespace LinqBuilder
{
    public interface IDynamicSpecification<TEntity, out TValue>
        : ISpecification<TEntity>
        where TEntity : class
    {
        TValue Value { get; }
    }
}
