using System;
using System.Linq.Expressions;

namespace LinqBuilder.Specifications
{
    public class AndSpecification<T> : CompositeSpecification<T> 
        where T : class
    {
        private readonly ICompositeSpecification<T> _left;
        private readonly ICompositeSpecification<T> _right;

        public AndSpecification(ICompositeSpecification<T> left, ICompositeSpecification<T> right)
        {
            _left = left;
            _right = right;
        }

        public override Expression<Func<T, bool>> AsExpression()
        {
            var leftExpression = _left.AsExpression();
            var rightExpression = _right.AsExpression();

            var andExpression = Expression.AndAlso(leftExpression.Body, Expression.Invoke(rightExpression, leftExpression.Parameters));

            return Expression.Lambda<Func<T, bool>>(andExpression, leftExpression.Parameters);
        }
    }
}
