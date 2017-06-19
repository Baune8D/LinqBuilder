using System;
using System.Linq.Expressions;

namespace LinqBuilder.Specifications
{
    public class NotSpecification<T> : Specification<T> 
        where T : class
    {
        private readonly ISpecification<T> _other;

        public NotSpecification(ISpecification<T> other, int? skip, int? take)
        {
            _other = other;
            if (skip.HasValue) Skip(skip.Value);
            if (take.HasValue) Take(take.Value);
        }

        public override Expression<Func<T, bool>> AsExpression()
        {
            var expression = _other.AsExpression();

            var notExpression = Expression.Not(expression.Body);

            return Expression.Lambda<Func<T, bool>>(notExpression, expression.Parameters);
        }
    }
}
