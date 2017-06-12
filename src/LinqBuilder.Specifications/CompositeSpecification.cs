using System;
using System.Linq;
using System.Linq.Expressions;

namespace LinqBuilder.Specifications
{
    public abstract class CompositeSpecification<T> : ICompositeSpecification<T> 
        where T : class
    {
        private int? _skip;
        private int? _take;

        public ICompositeSpecification<T> And(ICompositeSpecification<T> other)
        {
            return new AndSpecification<T>(this, other);
        }

        public ICompositeSpecification<T> Or(ICompositeSpecification<T> other)
        {
            return new OrSpecification<T>(this, other);
        }

        public ICompositeSpecification<T> Not()
        {
            return new NotSpecification<T>(this);
        }

        public bool IsSatisfiedBy(T entity)
        {
            var predicate = AsExpression().Compile();
            return predicate(entity);
        }

        public ICompositeSpecification<T> Skip(int count)
        {
            _skip = count;
            return this;
        }

        public ICompositeSpecification<T> Take(int count)
        {
            _take = count;
            return this;
        }

        public abstract Expression<Func<T, bool>> AsExpression();

        public IQueryable<T> Invoke(IQueryable<T> query)
        {
            var result = query.Where(AsExpression());

            if (_skip.HasValue) result = result.Skip(_skip.Value);
            if (_take.HasValue) result = result.Take(_take.Value);

            return result;
        }
    }
}
