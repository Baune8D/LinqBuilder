using System.Collections.Generic;
using LinqBuilder.OrderBy;

namespace LinqBuilder.Internal
{
    public sealed class SpecificationBase<TEntity> : IOrderedSpecification<TEntity>
        where TEntity : class
    {
        public SpecificationBase(IQuerySpecification<TEntity> querySpecification)
        {
            OrderSpecifications = new List<IOrderSpecification<TEntity>>();
            QuerySpecification = querySpecification;
        }

        public SpecificationBase(IOrderSpecification<TEntity> orderSpecification)
        {
            OrderSpecifications = new List<IOrderSpecification<TEntity>> { orderSpecification };
            QuerySpecification = new DummySpecification<TEntity>();
        }

        public SpecificationBase(IQuerySpecification<TEntity> querySpecification, List<IOrderSpecification<TEntity>> orderSpecifications)
        {
            OrderSpecifications = orderSpecifications;
            QuerySpecification = querySpecification;
        }

        public IQuerySpecification<TEntity> QuerySpecification { get; set; }

        public List<IOrderSpecification<TEntity>> OrderSpecifications { get; }

        public int? Skip { get; set; }

        public int? Take { get; set; }

        public SpecificationBase<TEntity> Internal => this;

        public ISpecification<TEntity> Clone()
        {
            return new SpecificationBase<TEntity>(QuerySpecification, OrderSpecifications)
            {
                Skip = Skip,
                Take = Take,
            };
        }
    }
}
