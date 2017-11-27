namespace LinqBuilder.OrderSpecifications
{
    public interface IOrderSpecification<T>
    {
        ICompositeSpecification<T> ThenBy(OrderSpecification<T> other);
    }
}
