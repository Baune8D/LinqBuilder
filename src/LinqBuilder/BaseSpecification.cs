using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LinqBuilder
{
    public abstract class BaseSpecification<TEntity> : SpecificationQuery<TEntity, bool>, ISpecification<TEntity>
        where TEntity : class
    {
        protected BaseSpecification() : this(entity => true) { }

        protected BaseSpecification(Expression<Func<TEntity, bool>> expression) : base(expression) { }

        public ISpecification<TEntity> AsInterface()
        {
            return this;
        }

        public ISpecification<TEntity> And(ISpecification<TEntity> specification)
        {
            return new AndSpecification<TEntity>(this, specification);
        }

        public ISpecification<TEntity> Or(ISpecification<TEntity> specification)
        {
            return new OrSpecification<TEntity>(this, specification);
        }

        public ISpecification<TEntity> Not()
        {
            return new NotSpecification<TEntity>(this);
        }

        public bool IsSatisfiedBy(TEntity entity)
        {
            var predicate = AsFunc();
            return predicate(entity);
        }

        public override IQueryable<TEntity> Invoke(IQueryable<TEntity> query)
        {
            return query.Where(AsExpression());
        }

        public override IEnumerable<TEntity> Invoke(IEnumerable<TEntity> collection)
        {
            return collection.Where(AsFunc());
        }
    }
}
