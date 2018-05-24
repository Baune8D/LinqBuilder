using System;
using System.Linq.Expressions;

namespace LinqBuilder
{
    public class Specification<TEntity> : BaseSpecification<TEntity>
        where TEntity : class
    {
        public Specification() { }

        public Specification(Expression<Func<TEntity, bool>> expression) : base(expression) { }

        public static ISpecification<TEntity> New()
        {
            return new Specification<TEntity>();
        }

        public static ISpecification<TEntity> New(Expression<Func<TEntity, bool>> expression)
        {
            return new Specification<TEntity>(expression);
        }
    }
}
