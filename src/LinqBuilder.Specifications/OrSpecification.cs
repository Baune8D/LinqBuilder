using System;
using System.Linq.Expressions;

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

            var paramExpr = Expression.Parameter(typeof(TEntity));

            var orExpression = Expression.Or(leftExpression.Body, rightExpression.Body);
            orExpression = (BinaryExpression) new ParameterReplacer(paramExpr).Visit(orExpression);
            if (orExpression == null) throw new InvalidOperationException();

            return Expression.Lambda<Func<TEntity, bool>>(orExpression, paramExpr);
        }
    }
}
