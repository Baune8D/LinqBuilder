using System;
using System.Linq.Expressions;

namespace LinqBuilder.Specifications
{
    public class NotSpecification<T> : CompositeSpecification<T> 
        where T : class
    {
        private readonly ICompositeSpecification<T> _other;

        public NotSpecification(ICompositeSpecification<T> other)
        {
            _other = other;
        }

        public override Expression<Func<T, bool>> AsExpression()
        {
            var expression = _other.AsExpression();

            var notExpression = Expression.Not(expression.Body);

            return Expression.Lambda<Func<T, bool>>(notExpression, expression.Parameters);
        }
    }
}
