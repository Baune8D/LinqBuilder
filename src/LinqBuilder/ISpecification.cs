using System;
using System.Linq.Expressions;

namespace LinqBuilder
{
    public interface ISpecification<TEntity> : ILinqBuilderQuery<TEntity>
    {
        ISpecification<TEntity> And(ISpecification<TEntity> specification);
        ISpecification<TEntity> Or(ISpecification<TEntity> specification);
        ISpecification<TEntity> Not();
        Expression<Func<TEntity, bool>> AsExpression();
        Func<TEntity, bool> AsFunc();
        bool IsSatisfiedBy(TEntity entity);
    }
}
