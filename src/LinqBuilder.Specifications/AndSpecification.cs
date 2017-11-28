using System;
using System.Linq;
using System.Linq.Expressions;

namespace LinqBuilder.Specifications
{
    public class AndSpecification<T> : Specification<T> 
        where T : class
    {
        private readonly IFilterSpecification<T> _left;
        private readonly IFilterSpecification<T> _right;

        public AndSpecification(IFilterSpecification<T> left, IFilterSpecification<T> right)
        {
            _left = left;
            _right = right;
        }

        public override Expression<Func<T, bool>> AsExpression()
        {
            var leftExpression = _left.AsExpression();
            var rightExpression = _right.AsExpression();

            var paramExpr = Expression.Parameter(typeof(T));

            var andExpression = Expression.AndAlso(leftExpression.Body, rightExpression.Body);
            andExpression = (BinaryExpression) new ParameterReplacer(paramExpr).Visit(andExpression);

            return Expression.Lambda<Func<T, bool>>(andExpression, paramExpr);
        }
    }
}
