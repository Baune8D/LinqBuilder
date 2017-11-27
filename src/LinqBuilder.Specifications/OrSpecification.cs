using System;
using System.Linq.Expressions;

namespace LinqBuilder.Specifications
{
    public class OrSpecification<T> : Specification<T> 
        where T : class
    {
        private readonly IFilterSpecification<T> _left;
        private readonly IFilterSpecification<T> _right;

        public OrSpecification(IFilterSpecification<T> left, IFilterSpecification<T> right)
        {
            _left = left;
            _right = right;
        }

        public override Expression<Func<T, bool>> AsExpression()
        {
            var leftExpression = _left.AsExpression();
            var rightExpression = _right.AsExpression();

            var orExpression = Expression.OrElse(leftExpression.Body, Expression.Invoke(rightExpression, leftExpression.Parameters));

            return Expression.Lambda<Func<T, bool>>(orExpression, leftExpression.Parameters);
        }
    }
}
