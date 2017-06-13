using System;
using System.Linq.Expressions;

namespace LinqBuilder.OrderSpecifications
{
    public interface IOrderBySpecification<T>
    {
        Order Order { get; set; }
        ThenBySpecification<T> ThenBy(IOrderBySpecification<T> other);
        Expression<Func<T, IComparable>> AsExpression();
    }
}
