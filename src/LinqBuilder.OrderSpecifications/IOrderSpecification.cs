namespace LinqBuilder.OrderSpecifications
{
    public interface IOrderSpecification<T>
    {
        ICompositeOrderSpecification<T> ThenBy(OrderSpecification<T> other);
        ICompositeOrderSpecification<T> Skip(int count);
        ICompositeOrderSpecification<T> Take(int count);
    }
}
