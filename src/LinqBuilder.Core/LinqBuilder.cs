using System.Collections.Generic;

namespace LinqBuilder.Core
{
    public sealed class LinqBuilder<TEntity> : IOrderedSpecification<TEntity>
        where TEntity : class
    {
        public IQuerySpecification<TEntity> QuerySpecification { get; set; }
        public List<IOrderSpecification<TEntity>> OrderSpecifications { get; }

        public int? Skip { get; set; }
        public int? Take { get; set; }

        public LinqBuilder(IQuerySpecification<TEntity> querySpecification)
        {
            OrderSpecifications = new List<IOrderSpecification<TEntity>>();
            QuerySpecification = querySpecification;
        }

        public LinqBuilder(IQuerySpecification<TEntity> querySpecification, IOrderSpecification<TEntity> orderSpecification)
        {
            OrderSpecifications = new List<IOrderSpecification<TEntity>> {orderSpecification};
            QuerySpecification = querySpecification;
        }

        public LinqBuilder<TEntity> GetLinqBuilder()
        {
            return this;
        }
    }
}
