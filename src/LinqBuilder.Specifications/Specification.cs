using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LinqBuilder.Specifications
{
    public class Specification<TEntity> : IFilterSpecification<TEntity> 
        where TEntity : class
    {
        public IFilterSpecification<TEntity> And(IFilterSpecification<TEntity> other)
        {
            return new AndSpecification<TEntity>(this, other);
        }

        public IFilterSpecification<TEntity> Or(IFilterSpecification<TEntity> other)
        {
            return new OrSpecification<TEntity>(this, other);
        }

        public IFilterSpecification<TEntity> Not()
        {
            return new NotSpecification<TEntity>(this);
        }

        public IQueryable<TEntity> Invoke(IQueryable<TEntity> query)
        {
            return query.Where(AsExpression());
        }

        public IEnumerable<TEntity> Invoke(IEnumerable<TEntity> collection)
        {
            return collection.Where(AsExpression().Compile());
        }

        public bool IsSatisfiedBy(TEntity entity)
        {
            var predicate = AsExpression().Compile();
            return predicate(entity);
        }

        public virtual Expression<Func<TEntity, bool>> AsExpression()
        {
            return t => true;
        }
    }
}
