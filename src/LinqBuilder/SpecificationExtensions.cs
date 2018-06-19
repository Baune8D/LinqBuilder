using System;
using System.Linq.Expressions;
using LinqBuilder.Core;
using LinqKit;

namespace LinqBuilder
{
    public static class SpecificationExtensions
    {
        public static ISpecification<TEntity> And<TEntity>(this ISpecification<TEntity> left, ISpecification<TEntity> right)
            where TEntity : class
        {
            var compositeSpecification = And(left.GetLinqBuilder().QuerySpecification.AsExpression(), right.GetLinqBuilder().QuerySpecification.AsExpression());
            return SetQuerySpecification(left, compositeSpecification);
        }

        public static ISpecification<TEntity> Or<TEntity>(this ISpecification<TEntity> left, ISpecification<TEntity> right)
            where TEntity : class
        {
            var compositeSpecification = Or(left.GetLinqBuilder().QuerySpecification.AsExpression(), right.GetLinqBuilder().QuerySpecification.AsExpression());
            return SetQuerySpecification(left, compositeSpecification);
        }

        public static ISpecification<TEntity> Not<TEntity>(this ISpecification<TEntity> specification)
            where TEntity : class
        {
            var compositeSpecification = Not(specification.GetLinqBuilder().QuerySpecification.AsExpression());
            return SetQuerySpecification(specification, compositeSpecification);
        }

        public static bool IsSatisfiedBy<TEntity>(this ISpecification<TEntity> specification, TEntity entity)
            where TEntity : class
        {
            var predicate = specification.GetLinqBuilder().QuerySpecification.AsFunc();
            return predicate(entity);
        }

        private static ISpecification<TEntity> SetQuerySpecification<TEntity>(ISpecification<TEntity> specification, IQuerySpecification<TEntity> querySpecification)
            where TEntity : class
        {
            var linqBuilder = specification.GetLinqBuilder();
            linqBuilder.QuerySpecification = querySpecification;
            return linqBuilder;
        }

        private static Specification<TEntity> And<TEntity>(Expression<Func<TEntity, bool>> leftExpression, Expression<Func<TEntity, bool>> rightExpression)
            where TEntity : class
        {
            var predicate = PredicateBuilder.New(leftExpression);
            return new Specification<TEntity>(predicate.And(rightExpression));
        }

        private static Specification<TEntity> Or<TEntity>(Expression<Func<TEntity, bool>> leftExpression, Expression<Func<TEntity, bool>> rightExpression)
            where TEntity : class
        {
            var predicate = PredicateBuilder.New(leftExpression);
            return new Specification<TEntity>(predicate.Or(rightExpression));
        }

        private static Specification<TEntity> Not<TEntity>(Expression<Func<TEntity, bool>> specificationExpression)
            where TEntity : class
        {
            var notExpression = Expression.Not(specificationExpression.Body);
            var expression = Expression.Lambda<Func<TEntity, bool>>(notExpression, specificationExpression.Parameters);
            return new Specification<TEntity>(expression);
        }
    }
}

