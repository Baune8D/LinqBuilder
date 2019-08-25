namespace LinqBuilder
{
    public interface IDynamicSpecification<TEntity, out TValue1, out TValue2, out TValue3> : IDynamicSpecification<TEntity, TValue1, TValue2>
        where TEntity : class
    {
        TValue3 Value3 { get; }
    }
}
