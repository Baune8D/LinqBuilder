using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LinqBuilder
{
    public abstract class LinqBuilderQuery<TEntity, TKey> : ILinqBuilderQuery<TEntity>
    {
        private readonly Expression<Func<TEntity, TKey>> _expression;
        private Func<TEntity, TKey> _func;

        protected LinqBuilderQuery(Expression<Func<TEntity, TKey>> expression)
        {
            _expression = expression;
        }

        public virtual Expression<Func<TEntity, TKey>> AsExpression() => _expression;

        public virtual Func<TEntity, TKey> AsFunc()
        {
            return _func ?? (_func = AsExpression().Compile());
        }

        public abstract IQueryable<TEntity> Invoke(IQueryable<TEntity> query);

        public abstract IEnumerable<TEntity> Invoke(IEnumerable<TEntity> collection);
    }
}
