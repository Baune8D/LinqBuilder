using System;
using System.Linq.Expressions;

namespace LinqBuilder.Specifications
{
    public class AndSpecification<TEntity> : Specification<TEntity> 
        where TEntity : class
    {
        private readonly IFilterSpecification<TEntity> _left;
        private readonly IFilterSpecification<TEntity> _right;

        public AndSpecification(IFilterSpecification<TEntity> left, IFilterSpecification<TEntity> right)
        {
            _left = left;
            _right = right;
        }

        public override Expression<Func<TEntity, bool>> AsExpression()
        {
            var leftExpression = _left.AsExpression();
            var rightExpression = _right.AsExpression();

            var paramExpr = Expression.Parameter(typeof(TEntity));

            var andExpression = Expression.AndAlso(leftExpression.Body, rightExpression.Body);
            andExpression = (BinaryExpression) new ParameterReplacer(paramExpr).Visit(andExpression);
            if (andExpression == null) throw new InvalidOperationException(nameof(andExpression));

            return Expression.Lambda<Func<TEntity, bool>>(andExpression, paramExpr);
        }
    }
}
