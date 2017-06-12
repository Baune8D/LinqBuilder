using System;
using System.Linq.Expressions;

namespace LinqBuilder.Filtering
{
    public class OrSpecification<T> : CompositeSpecification<T> 
        where T : class
    {
        private readonly ICompositeSpecification<T> _left;
        private readonly ICompositeSpecification<T> _right;

        public OrSpecification(ICompositeSpecification<T> left, ICompositeSpecification<T> right)
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
