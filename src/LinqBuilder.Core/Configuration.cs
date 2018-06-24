using System.Collections.Generic;

namespace LinqBuilder.Core
{
    public sealed class Configuration<TEntity> : IOrderedSpecification<TEntity>
        where TEntity : class
    {
        public IQuerySpecification<TEntity> QuerySpecification { get; set; }
        public List<IOrderSpecification<TEntity>> OrderSpecifications { get; }

        public int? Skip { get; set; }
        public int? Take { get; set; }

        public Configuration(IQuerySpecification<TEntity> querySpecification)
        {
            OrderSpecifications = new List<IOrderSpecification<TEntity>>();
            QuerySpecification = querySpecification;
        }

        public Configuration(IQuerySpecification<TEntity> querySpecification, IOrderSpecification<TEntity> orderSpecification)
        {
            OrderSpecifications = new List<IOrderSpecification<TEntity>> {orderSpecification};
            QuerySpecification = querySpecification;
        }

        public Configuration<TEntity> Internal => this;
    }
}
