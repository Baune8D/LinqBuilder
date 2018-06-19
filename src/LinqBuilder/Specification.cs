using System;
using System.Linq;
using System.Linq.Expressions;
using LinqBuilder.Core;

namespace LinqBuilder
{
    public class Specification<TEntity> : QuerySpecificationBase<TEntity>
        where TEntity : class
    {
        public Specification() : this(entity => true) { }

        public Specification(Expression<Func<TEntity, bool>> expression) : base(expression) { }

        public static IQuerySpecification<TEntity> New()
        {
            return new Specification<TEntity>();
        }

        public static IQuerySpecification<TEntity> New(Expression<Func<TEntity, bool>> expression)
        {
            return new Specification<TEntity>(expression);
        }

        public static ISpecification<TEntity> All(params IQuerySpecification<TEntity>[] specifications)
        {
            return specifications.Aggregate((ISpecification<TEntity>)New(), (current, specification) => current.And(specification));
        }

        public static ISpecification<TEntity> None(params IQuerySpecification<TEntity>[] specifications)
        {
            return specifications.Aggregate((ISpecification<TEntity>)New(), (current, specification) => current.And(specification)).Not();
        }

        public static ISpecification<TEntity> Any(params IQuerySpecification<TEntity>[] specifications)
        {
            return specifications.Aggregate((ISpecification<TEntity>)New(), (current, specification) => current.Or(specification));
        }
    }
}
