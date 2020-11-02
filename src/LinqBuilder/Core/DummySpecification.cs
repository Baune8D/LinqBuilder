using System;
using System.Linq.Expressions;

namespace LinqBuilder.Core
{
    internal class DummySpecification<TEntity> : IQuerySpecification<TEntity>
        where TEntity : class
    {
        public InternalConfiguration<TEntity> Internal => new InternalConfiguration<TEntity>(this);

        public Expression<Func<TEntity, bool>>? AsExpression() => null;

        public Func<TEntity, bool>? AsFunc() => null;
    }
}
