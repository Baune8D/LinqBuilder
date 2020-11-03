namespace LinqBuilder
{
    public interface IDynamicSpecification<TEntity, out TValue1, out TValue2, out TValue3, out TValue4>
        : IDynamicSpecification<TEntity, TValue1, TValue2, TValue3>
        where TEntity : class
    {
        TValue4 Value4 { get; }
    }
}
