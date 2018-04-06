using System.Collections.Generic;
using System.Linq;

namespace LinqBuilder
{
    public static class LinqExtensions
    {
        public static IQueryable<TEntity> Where<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
        {
            if (specification == null) throw Exceptions.SpecificationCannotBeNull(nameof(specification));
            return query.Where(specification.AsExpression());
        }

        public static IEnumerable<TEntity> Where<TEntity>(this IEnumerable<TEntity> collection, ISpecification<TEntity> specification)
        {
            if (specification == null) throw Exceptions.SpecificationCannotBeNull(nameof(specification));
            return collection.Where(specification.AsFunc());
        }

        public static bool Any<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
        {
            if (specification == null) throw Exceptions.SpecificationCannotBeNull(nameof(specification));
            return query.Any(specification.AsExpression());
        }

        public static bool Any<TEntity>(this IEnumerable<TEntity> collection, ISpecification<TEntity> specification)
        {
            if (specification == null) throw Exceptions.SpecificationCannotBeNull(nameof(specification));
            return collection.Any(specification.AsFunc());
        }

        public static bool All<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
        {
            if (specification == null) throw Exceptions.SpecificationCannotBeNull(nameof(specification));
            return query.All(specification.AsExpression());
        }

        public static bool All<TEntity>(this IEnumerable<TEntity> collection, ISpecification<TEntity> specification)
        {
            if (specification == null) throw Exceptions.SpecificationCannotBeNull(nameof(specification));
            return collection.All(specification.AsFunc());
        }

        public static int Count<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
        {
            if (specification == null) throw Exceptions.SpecificationCannotBeNull(nameof(specification));
            return query.Count(specification.AsExpression());
        }

        public static int Count<TEntity>(this IEnumerable<TEntity> collection, ISpecification<TEntity> specification)
        {
            if (specification == null) throw Exceptions.SpecificationCannotBeNull(nameof(specification));
            return collection.Count(specification.AsFunc());
        }

        public static long LongCount<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
        {
            if (specification == null) throw Exceptions.SpecificationCannotBeNull(nameof(specification));
            return query.LongCount(specification.AsExpression());
        }

        public static long LongCount<TEntity>(this IEnumerable<TEntity> collection, ISpecification<TEntity> specification)
        {
            if (specification == null) throw Exceptions.SpecificationCannotBeNull(nameof(specification));
            return collection.LongCount(specification.AsFunc());
        }

        public static TEntity First<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
        {
            if (specification == null) throw Exceptions.SpecificationCannotBeNull(nameof(specification));
            return query.First(specification.AsExpression());
        }

        public static TEntity First<TEntity>(this IEnumerable<TEntity> collection, ISpecification<TEntity> specification)
        {
            if (specification == null) throw Exceptions.SpecificationCannotBeNull(nameof(specification));
            return collection.First(specification.AsFunc());
        }

        public static TEntity FirstOrDefault<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
        {
            if (specification == null) throw Exceptions.SpecificationCannotBeNull(nameof(specification));
            return query.FirstOrDefault(specification.AsExpression());
        }

        public static TEntity FirstOrDefault<TEntity>(this IEnumerable<TEntity> collection, ISpecification<TEntity> specification)
        {
            if (specification == null) throw Exceptions.SpecificationCannotBeNull(nameof(specification));
            return collection.FirstOrDefault(specification.AsFunc());
        }

        public static TEntity Single<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
        {
            if (specification == null) throw Exceptions.SpecificationCannotBeNull(nameof(specification));
            return query.Single(specification.AsExpression());
        }

        public static TEntity Single<TEntity>(this IEnumerable<TEntity> collection, ISpecification<TEntity> specification)
        {
            if (specification == null) throw Exceptions.SpecificationCannotBeNull(nameof(specification));
            return collection.Single(specification.AsFunc());
        }

        public static TEntity SingleOrDefault<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification)
        {
            if (specification == null) throw Exceptions.SpecificationCannotBeNull(nameof(specification));
            return query.SingleOrDefault(specification.AsExpression());
        }

        public static TEntity SingleOrDefault<TEntity>(this IEnumerable<TEntity> collection, ISpecification<TEntity> specification)
        {
            if (specification == null) throw Exceptions.SpecificationCannotBeNull(nameof(specification));
            return collection.SingleOrDefault(specification.AsFunc());
        }
    }
}
