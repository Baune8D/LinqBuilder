using System;
using System.Linq.Expressions;

namespace LinqBuilder.OrderSpecifications
{
    public interface IOrderBySpecification<T>
    {
        Order Order { get; set; }
        Expression<Func<T, IComparable>> AsExpression();
        ThenBySpecification<T> ThenBy(IOrderBySpecification<T> other);
    }
}
