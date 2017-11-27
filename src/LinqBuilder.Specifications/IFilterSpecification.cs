using System;
using System.Linq.Expressions;

namespace LinqBuilder.Specifications
{
    public interface IFilterSpecification<T> : ISpecification<T>
    {
        IFilterSpecification<T> And(IFilterSpecification<T> other);
        IFilterSpecification<T> Or(IFilterSpecification<T> other);
        IFilterSpecification<T> Not();
        bool IsSatisfiedBy(T entity);
        Expression<Func<T, bool>> AsExpression();
    }
}
