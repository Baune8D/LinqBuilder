using System;
using System.Linq.Expressions;

namespace LinqBuilder.Specifications
{
    public interface IFilterSpecification<TEntity> : ISpecification<TEntity>
    {
        IFilterSpecification<TEntity> And(IFilterSpecification<TEntity> other);
        IFilterSpecification<TEntity> Or(IFilterSpecification<TEntity> other);
        IFilterSpecification<TEntity> Not();
        bool IsSatisfiedBy(TEntity entity);
        Expression<Func<TEntity, bool>> AsExpression();
    }
}
