using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LinqBuilder.Specifications
{
    public interface ISpecification<T>
    {
        ISpecification<T> And(ISpecification<T> other);
        ISpecification<T> Or(ISpecification<T> other);
        ISpecification<T> Not();
        ISpecification<T> Skip(int count);
        ISpecification<T> Take(int count);
        IQueryable<T> Invoke(IQueryable<T> query);
        IEnumerable<T> Invoke(IEnumerable<T> collection);
        bool IsSatisfiedBy(T entity);
        Expression<Func<T, bool>> AsExpression();
    }
}
