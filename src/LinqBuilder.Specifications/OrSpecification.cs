using System;
using System.Linq.Expressions;
using LinqKit;

namespace LinqBuilder.Specifications
{
    public class OrSpecification<TEntity> : Specification<TEntity> 
        where TEntity : class
    {
        private readonly IFilterSpecification<TEntity> _left;
        private readonly IFilterSpecification<TEntity> _right;

        public OrSpecification(IFilterSpecification<TEntity> left, IFilterSpecification<TEntity> right)
        {
            _left = left;
            _right = right;
        }

        public override Expression<Func<TEntity, bool>> AsExpression()
        {
            var leftExpression = _left.AsExpression();
            var rightExpression = _right.AsExpression();

            var predicate = PredicateBuilder.New(leftExpression);
            predicate = predicate.Or(rightExpression);

            return predicate;
        }
    }
}
