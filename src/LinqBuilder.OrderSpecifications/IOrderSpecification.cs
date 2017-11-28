namespace LinqBuilder.OrderSpecifications
{
    public interface IOrderSpecification<T>
    {
        ICompositeSpecification<T> ThenBy(OrderSpecification<T> other);
        ICompositeSpecification<T> Skip(int count);
        ICompositeSpecification<T> Take(int count);
    }
}
