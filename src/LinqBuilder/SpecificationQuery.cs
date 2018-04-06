using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LinqBuilder
{
    public abstract class SpecificationQuery<TEntity, TResult> : ISpecificationQuery<TEntity>
    {
        private readonly Expression<Func<TEntity, TResult>> _expression;
        private Func<TEntity, TResult> _func;

        protected SpecificationQuery(Expression<Func<TEntity, TResult>> expression)
        {
            _expression = expression;
        }

        public virtual Expression<Func<TEntity, TResult>> AsExpression() => _expression;

        public virtual Func<TEntity, TResult> AsFunc()
        {
            return _func ?? (_func = AsExpression().Compile());
        }

        public abstract IQueryable<TEntity> Invoke(IQueryable<TEntity> query);

        public abstract IEnumerable<TEntity> Invoke(IEnumerable<TEntity> collection);
    }
}
