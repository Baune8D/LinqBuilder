using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LinqBuilder
{
    public class Specification<TEntity> : ISpecification<TEntity> 
        where TEntity : class
    {
        private readonly Expression<Func<TEntity, bool>> _expression;
        private Func<TEntity, bool> _func;

        public Specification() { }

        public Specification(Expression<Func<TEntity, bool>> expression)
        {
            _expression = expression;
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

        public IQueryable<TEntity> Invoke(IQueryable<TEntity> query)
        {
            return query.Where(AsExpression());
        }

        public IEnumerable<TEntity> Invoke(IEnumerable<TEntity> collection)
        {
            return collection.Where(AsFunc());
        }

        public bool IsSatisfiedBy(TEntity entity)
        {
            var predicate = AsFunc();
            return predicate(entity);
        }

        public virtual Expression<Func<TEntity, bool>> AsExpression()
        {
            if (_expression != null) return _expression;
            return entity => true;
        }

        public Func<TEntity, bool> AsFunc()
        {
            return _func ?? (_func = AsExpression().Compile());
        }
    }
}
