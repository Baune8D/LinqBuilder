namespace LinqBuilder.OrderBy
{
    public static class SpecificationQueryExtensions
    {
        public static bool IsOrdered<TEntity>(this ISpecificationQuery<TEntity> specification)
            where TEntity : class
        {
            return specification is IOrderedSpecification<TEntity>;
        }

        public static IOrderedSpecification<TEntity> AsOrdered<TEntity>(this ISpecificationQuery<TEntity> specification)
            where TEntity : class
        {
            return specification as IOrderedSpecification<TEntity>;
        }
    }
}
