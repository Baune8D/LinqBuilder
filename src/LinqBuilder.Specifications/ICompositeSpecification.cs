using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LinqBuilder.Specifications
{
    public interface ICompositeSpecification<T>
    {
        ICompositeSpecification<T> And(ICompositeSpecification<T> other);
        ICompositeSpecification<T> Or(ICompositeSpecification<T> other);
        ICompositeSpecification<T> Not();
        ICompositeSpecification<T> Skip(int count);
        ICompositeSpecification<T> Take(int count);
        IQueryable<T> Invoke(IQueryable<T> query);
        IEnumerable<T> Invoke(IEnumerable<T> collection);
        bool IsSatisfiedBy(T entity);
        Expression<Func<T, bool>> AsExpression();
    }
}
