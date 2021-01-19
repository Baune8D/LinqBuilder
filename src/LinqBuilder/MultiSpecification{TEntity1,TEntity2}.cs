using System;
using System.Linq.Expressions;
using LinqBuilder.Internal;

namespace LinqBuilder
{
    public abstract class MultiSpecification<TEntity1, TEntity2>
        : ISpecification<TEntity1>
        where TEntity1 : class
        where TEntity2 : class
    {
        public SpecificationBase<TEntity1> Internal => new(For<TEntity1>());

        // ReSharper disable once MemberCanBeProtected.Global
        public abstract Expression<Func<TEntity1, bool>> AsExpressionForEntity1();

        // ReSharper disable once MemberCanBeProtected.Global
        public abstract Expression<Func<TEntity2, bool>> AsExpressionForEntity2();

        public Specification<TEntity> For<TEntity>()
            where TEntity : class
        {
            var result = Transform<TEntity>();
            if (result == null)
            {
                throw new InvalidOperationException("Type is not defined in specification!");
            }

            return result;
        }

        protected virtual Specification<TEntity>? Transform<TEntity>()
            where TEntity : class
        {
            object? specification = null;

            if (typeof(TEntity) == typeof(TEntity1))
            {
                specification = new Specification<TEntity1>(AsExpressionForEntity1());
            }
            else if (typeof(TEntity) == typeof(TEntity2))
            {
                specification = new Specification<TEntity2>(AsExpressionForEntity2());
            }

            return (Specification<TEntity>?)specification;
        }
    }
}
