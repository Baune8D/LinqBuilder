using System.Collections.Generic;

namespace LinqBuilder.Core
{
    public sealed class InternalConfiguration<TEntity> : IOrderedSpecification<TEntity>
        where TEntity : class
    {
        public InternalConfiguration(IQuerySpecification<TEntity> querySpecification)
        {
            OrderSpecifications = new List<IOrderSpecification<TEntity>>();
            QuerySpecification = querySpecification;
        }

        public InternalConfiguration(IOrderSpecification<TEntity> orderSpecification)
        {
            OrderSpecifications = new List<IOrderSpecification<TEntity>> { orderSpecification };
            QuerySpecification = new DummySpecification<TEntity>();
        }

        public InternalConfiguration(IQuerySpecification<TEntity> querySpecification, List<IOrderSpecification<TEntity>> orderSpecifications)
        {
            OrderSpecifications = orderSpecifications;
            QuerySpecification = querySpecification;
        }

        public IQuerySpecification<TEntity> QuerySpecification { get; set; }

        public List<IOrderSpecification<TEntity>> OrderSpecifications { get; }

        public int? Skip { get; set; }

        public int? Take { get; set; }

        public InternalConfiguration<TEntity> Internal => this;

        public ISpecification<TEntity> Clone()
        {
            return new InternalConfiguration<TEntity>(QuerySpecification, OrderSpecifications)
            {
                Skip = Skip,
                Take = Take,
            };
        }
    }
}
