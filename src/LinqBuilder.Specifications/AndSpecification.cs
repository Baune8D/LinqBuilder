using System;
using System.Linq.Expressions;

namespace LinqBuilder.Specifications
{
    public class AndSpecification<T> : Specification<T> 
        where T : class
    {
        private readonly ISpecification<T> _left;
        private readonly ISpecification<T> _right;

        public AndSpecification(ISpecification<T> left, ISpecification<T> right, int? skip, int? take)
        {
            _left = left;
            _right = right;
            if (skip.HasValue) Skip(skip.Value);
            if (take.HasValue) Take(take.Value);
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
