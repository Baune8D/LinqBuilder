using LinqBuilder.Internal;

namespace LinqBuilder
{
    public interface IDynamicSpecification<TEntity, out TValue1, out TValue2>
        : IQuerySpecification<TEntity>
        where TEntity : class
    {
        TValue1 Value1 { get; }

        TValue2 Value2 { get; }
    }
}
