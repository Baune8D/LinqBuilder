using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LinqBuilder
{
    public class Specification<TEntity> : LinqBuilderQuery<TEntity, bool>, ISpecification<TEntity> 
        where TEntity : class
    {
        public Specification() : this(entity => true) { }

        public Specification(Expression<Func<TEntity, bool>> expression) : base(expression) { }

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

        public override IQueryable<TEntity> Invoke(IQueryable<TEntity> query)
        {
            return query.Where(AsExpression());
        }

        public override IEnumerable<TEntity> Invoke(IEnumerable<TEntity> collection)
        {
            return collection.Where(AsFunc());
        }

        public bool IsSatisfiedBy(TEntity entity)
        {
            var predicate = AsFunc();
            return predicate(entity);
        }
    }
}
