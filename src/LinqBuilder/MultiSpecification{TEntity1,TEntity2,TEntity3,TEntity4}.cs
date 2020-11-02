using System;
using System.Linq.Expressions;

namespace LinqBuilder
{
    public abstract class MultiSpecification<TEntity1, TEntity2, TEntity3, TEntity4> : MultiSpecification<TEntity1, TEntity2, TEntity3>
        where TEntity1 : class
        where TEntity2 : class
        where TEntity3 : class
        where TEntity4 : class
    {
        public abstract Expression<Func<TEntity4, bool>> AsExpressionForEntity4();

        protected override Specification<TEntity>? Transform<TEntity>()
        {
            object? specification = base.Transform<TEntity>();

            if (specification == null && typeof(TEntity) == typeof(TEntity4))
            {
                specification = new Specification<TEntity4>(AsExpressionForEntity4());
            }

            return (Specification<TEntity>?)specification;
        }
    }
}
