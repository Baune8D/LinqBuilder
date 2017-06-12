using System;
using System.Linq;
using System.Linq.Expressions;

namespace LinqBuilder.Filtering
{
    public interface ICompositeSpecification<T>
    {
        ICompositeSpecification<T> And(ICompositeSpecification<T> other);
        ICompositeSpecification<T> Or(ICompositeSpecification<T> other);
        ICompositeSpecification<T> Not();
        bool IsSatisfiedBy(T entity);
        ICompositeSpecification<T> Skip(int count);
        ICompositeSpecification<T> Take(int count);
        Expression<Func<T, bool>> AsExpression();
        IQueryable<T> Invoke(IQueryable<T> query);
    }
}
