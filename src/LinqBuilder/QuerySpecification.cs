using System;
using System.Linq.Expressions;
using LinqBuilder.Core;

namespace LinqBuilder
{
    public abstract class QuerySpecification<TEntity> : IQuerySpecification<TEntity>
        where TEntity : class
    {
        private readonly Expression<Func<TEntity, bool>> _expression;
        private Func<TEntity, bool> _func;

        protected QuerySpecification() { }

        protected QuerySpecification(Expression<Func<TEntity, bool>> expression)
        {
            _expression = expression;
        }

        public Configuration<TEntity> Internal => new Configuration<TEntity>(this);

        public virtual Expression<Func<TEntity, bool>> AsExpression()
        {
            return _expression;
        }

        public Func<TEntity, bool> AsFunc()
        {
            return _func ?? (_func = AsExpression().Compile());
        }
    }
}
