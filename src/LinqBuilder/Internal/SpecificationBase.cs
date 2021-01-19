using System.Collections.Generic;
using System.Linq;
using LinqBuilder.OrderBy;

namespace LinqBuilder.Internal
{
    public sealed class SpecificationBase<TEntity> : IOrderedSpecification<TEntity>
        where TEntity : class
    {
        private readonly List<IOrderSpecification<TEntity>> _orderSpecifications;

        public SpecificationBase(IQuerySpecification<TEntity> querySpecification)
        {
            _orderSpecifications = new List<IOrderSpecification<TEntity>>();
            QuerySpecification = querySpecification;
        }

        public SpecificationBase(IOrderSpecification<TEntity> orderSpecification)
        {
            _orderSpecifications = new List<IOrderSpecification<TEntity>> { orderSpecification };
            QuerySpecification = new DummySpecification<TEntity>();
        }

        public SpecificationBase(IQuerySpecification<TEntity> querySpecification, IEnumerable<IOrderSpecification<TEntity>> orderSpecifications)
        {
            _orderSpecifications = orderSpecifications.ToList();
            QuerySpecification = querySpecification;
        }

        public IQuerySpecification<TEntity> QuerySpecification { get; set; }

        public int? Skip { get; set; }

        public int? Take { get; set; }

        public IReadOnlyList<IOrderSpecification<TEntity>> OrderSpecifications => _orderSpecifications;

        public SpecificationBase<TEntity> Internal => this;

        public void AddOrderSpecifications(params IOrderSpecification<TEntity>[] specifications)
        {
            _orderSpecifications.AddRange(specifications);
        }

        public ISpecification<TEntity> Clone()
        {
            return new SpecificationBase<TEntity>(QuerySpecification, _orderSpecifications)
            {
                Skip = Skip,
                Take = Take,
            };
        }
    }
}
