using System;
using System.Linq.Expressions;

namespace LinqBuilder.Internal
{
    internal class DummySpecification<TEntity> : IQuerySpecification<TEntity>
        where TEntity : class
    {
        public SpecificationBase<TEntity> Internal => new(this);

        public Expression<Func<TEntity, bool>>? AsExpression() => null;

        public Func<TEntity, bool>? AsFunc() => null;
    }
}
