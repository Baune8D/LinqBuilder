using System;
using System.Linq.Expressions;

namespace LinqBuilder.Internal
{
    internal sealed class DummySpecification<TEntity> : IQuerySpecification<TEntity>
        where TEntity : class
    {
        public SpecificationBase<TEntity> Internal => new SpecificationBase<TEntity>(this);

        public Expression<Func<TEntity, bool>>? AsExpression() => null;

        public Func<TEntity, bool>? AsFunc() => null;
    }
}
