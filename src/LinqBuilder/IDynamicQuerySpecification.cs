using LinqBuilder.Core;

namespace LinqBuilder
{
    public interface IDynamicQuerySpecification<TEntity, out TValue> : IQuerySpecification<TEntity>
        where TEntity : class
    {
        TValue Value { get; }
    }

    public interface IDynamicQuerySpecification<TEntity, out TValue1, out TValue2> : IQuerySpecification<TEntity>
        where TEntity : class
    {
        TValue1 Value1 { get; }
        TValue2 Value2 { get; }
    }

    public interface IDynamicQuerySpecification<TEntity, out TValue1, out TValue2, out TValue3> : IDynamicQuerySpecification<TEntity, TValue1, TValue2>
        where TEntity : class
    {
        TValue3 Value3 { get; }
    }

    public interface IDynamicQuerySpecification<TEntity, out TValue1, out TValue2, out TValue3, out TValue4> : IDynamicQuerySpecification<TEntity, TValue1, TValue2, TValue3>
        where TEntity : class
    {
        TValue4 Value4 { get; }
    }
}
