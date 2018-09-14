using System;
using System.Linq.Expressions;

namespace LinqBuilder
{
    public abstract class MultiSpecification<TEntity1, TEntity2>
        where TEntity1 : class
        where TEntity2 : class
    {
        public abstract Expression<Func<TEntity1, bool>> AsExpressionForEntity1();

        public abstract Expression<Func<TEntity2, bool>> AsExpressionForEntity2();

        protected virtual Specification<TEntity> Transform<TEntity>()
            where TEntity : class
        {
            object specification = null;
            if (typeof(TEntity) == typeof(TEntity1))
            {
                specification = new Specification<TEntity1>(AsExpressionForEntity1());
            }
            else if (typeof(TEntity) == typeof(TEntity2))
            {
                specification = new Specification<TEntity2>(AsExpressionForEntity2());
            }
            return (Specification<TEntity>) specification;
        }

        public Specification<TEntity> For<TEntity>()
            where TEntity : class
        {
            var result = Transform<TEntity>();
            if (result == null) throw new Exception("Type is not defined in specification!");
            return result;
        }
    }

    public abstract class MultiSpecification<TEntity1, TEntity2, TEntity3> : MultiSpecification<TEntity1, TEntity2>
        where TEntity1 : class
        where TEntity2 : class
        where TEntity3 : class
    {
        public abstract Expression<Func<TEntity3, bool>> AsExpressionForEntity3();

        protected override Specification<TEntity> Transform<TEntity>()
        {
            object specification = base.Transform<TEntity>();
            if (specification == null && typeof(TEntity) == typeof(TEntity3))
            {
                specification = new Specification<TEntity3>(AsExpressionForEntity3());
            }
            return (Specification<TEntity>) specification;
        }
    }

    public abstract class MultiSpecification<TEntity1, TEntity2, TEntity3, TEntity4> : MultiSpecification<TEntity1, TEntity2, TEntity3>
        where TEntity1 : class
        where TEntity2 : class
        where TEntity3 : class
        where TEntity4 : class
    {
        public abstract Expression<Func<TEntity4, bool>> AsExpressionForEntity4();

        protected override Specification<TEntity> Transform<TEntity>()
        {
            object specification = base.Transform<TEntity>();
            if (specification == null && typeof(TEntity) == typeof(TEntity4))
            {
                specification = new Specification<TEntity4>(AsExpressionForEntity4());
            }
            return (Specification<TEntity>) specification;
        }
    }
}
