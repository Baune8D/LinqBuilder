using System;
using System.Linq.Expressions;
using LinqKit;

namespace LinqBuilder
{
    public class OrSpecification<TEntity> : Specification<TEntity> 
        where TEntity : class
    {
        private readonly ISpecification<TEntity> _left;
        private readonly ISpecification<TEntity> _right;

        public OrSpecification(ISpecification<TEntity> left, ISpecification<TEntity> right)
        {
            _left = left;
            _right = right;
        }

        public override Expression<Func<TEntity, bool>> AsExpression()
        {
            var predicate = PredicateBuilder.New(_left.AsExpression());
            return predicate.Or(_right.AsExpression());
        }
    }
}
