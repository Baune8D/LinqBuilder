using System;
using System.Linq;
using System.Linq.Expressions;

namespace LinqBuilder.Specifications
{
    public class NotSpecification<TEntity> : Specification<TEntity> 
        where TEntity : class
    {
        private readonly IFilterSpecification<TEntity> _other;

        public NotSpecification(IFilterSpecification<TEntity> other)
        {
            _other = other;
        }

        public override Expression<Func<TEntity, bool>> AsExpression()
        {
            var expression = _other.AsExpression();

            var notExpression = Expression.Not(expression.Body);

            return Expression.Lambda<Func<TEntity, bool>>(notExpression, expression.Parameters.Single());
        }
    }
}
