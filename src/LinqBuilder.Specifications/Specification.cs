using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LinqBuilder.Specifications
{
    public class Specification<T> : IFilterSpecification<T> 
        where T : class
    {
        public IFilterSpecification<T> And(IFilterSpecification<T> other)
        {
            return new AndSpecification<T>(this, other);
        }

        public IFilterSpecification<T> Or(IFilterSpecification<T> other)
        {
            return new OrSpecification<T>(this, other);
        }

        public IFilterSpecification<T> Not()
        {
            return new NotSpecification<T>(this);
        }

        public IQueryable<T> Invoke(IQueryable<T> query)
        {
            return query.Where(AsExpression());
        }

        public IEnumerable<T> Invoke(IEnumerable<T> collection)
        {
            return collection.Where(AsExpression().Compile());
        }

        public bool IsSatisfiedBy(T entity)
        {
            var predicate = AsExpression().Compile();
            return predicate(entity);
        }

        public virtual Expression<Func<T, bool>> AsExpression()
        {
            return t => true;
        }
    }
}
