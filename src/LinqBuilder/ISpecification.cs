using System;
using System.Linq.Expressions;

namespace LinqBuilder
{
    public interface ISpecification<TEntity> : ILinqQuery<TEntity>
    {
        ISpecification<TEntity> And(ISpecification<TEntity> specification);
        ISpecification<TEntity> Or(ISpecification<TEntity> specification);
        ISpecification<TEntity> Not();
        bool IsSatisfiedBy(TEntity entity);
        Expression<Func<TEntity, bool>> AsExpression();
        Func<TEntity, bool> AsFunc();
    }
}
