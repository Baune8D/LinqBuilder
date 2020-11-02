using System;
using System.Linq.Expressions;

namespace LinqBuilder
{
    public abstract class MultiSpecification<TEntity1, TEntity2, TEntity3> : MultiSpecification<TEntity1, TEntity2>
        where TEntity1 : class
        where TEntity2 : class
        where TEntity3 : class
    {
        public abstract Expression<Func<TEntity3, bool>> AsExpressionForEntity3();

        protected override Specification<TEntity>? Transform<TEntity>()
        {
            object? specification = base.Transform<TEntity>();

            if (specification == null && typeof(TEntity) == typeof(TEntity3))
            {
                specification = new Specification<TEntity3>(AsExpressionForEntity3());
            }

            return (Specification<TEntity>?)specification;
        }
    }
}
