using System;
using System.Linq.Expressions;

namespace LinqBuilder
{
    public class NotSpecification<TEntity> : Specification<TEntity> 
        where TEntity : class
    {
        private readonly ISpecification<TEntity> _specification;

        public NotSpecification(ISpecification<TEntity> specification)
        {
            _specification = specification;
        }

        public override Expression<Func<TEntity, bool>> AsExpression()
        {
            var expression = _specification.AsExpression();
            var notExpression = Expression.Not(expression.Body);
            return Expression.Lambda<Func<TEntity, bool>>(notExpression, expression.Parameters);
        }
    }
}
