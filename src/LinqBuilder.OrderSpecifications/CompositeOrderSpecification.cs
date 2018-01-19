using System.Collections.Generic;
using System.Linq;
using LinqBuilder.Specifications;

namespace LinqBuilder.OrderSpecifications
{
    public class CompositeOrderSpecification<TEntity> : ICompositeOrderSpecification<TEntity>
        where TEntity : class
    {
        private readonly ISpecification<TEntity> _specification;
        private readonly List<IOrderSpecification<TEntity>> _orderList;

        private int? _skip;
        private int? _take;

        public CompositeOrderSpecification(List<IOrderSpecification<TEntity>> orderList, IOrderSpecification<TEntity> right, int? skip = null, int? take = null)
        {
            _orderList = orderList;
            _orderList.Add(right);

            if (skip.HasValue) _skip = skip;
            if (take.HasValue) _take = take;
        }

        public CompositeOrderSpecification(ISpecification<TEntity> specificaiton, List<IOrderSpecification<TEntity>> orderList, IOrderSpecification<TEntity> right, int? skip = null, int? take = null)
            : this(orderList, right, skip, take)
        {
            _specification = specificaiton;
        }

        public ICompositeOrderSpecification<TEntity> ThenBy(IOrderSpecification<TEntity> other)
        {
            return new CompositeOrderSpecification<TEntity>(_specification, _orderList, other, _skip, _take);
        }

        public ICompositeOrderSpecification<TEntity> Skip(int count)
        {
            _skip = count;
            return this;
        }

        public ICompositeOrderSpecification<TEntity> Take(int count)
        {
            _take = count;
            return this;
        }

        public IQueryable<TEntity> Invoke(IQueryable<TEntity> query)
        {
            return Invoke(query.AsEnumerable()).AsQueryable();
        }

        public IEnumerable<TEntity> Invoke(IEnumerable<TEntity> collection)
        {
            if (_specification != null) collection = _specification.Invoke(collection);

            var orderedCollection = _orderList[0].InvokeOrdered(collection);

            for (var i = 1; i < _orderList.Count; i++)
            {
                orderedCollection = _orderList[i].InvokeOrdered(orderedCollection);
            }

            return SkipTake(orderedCollection.AsEnumerable(), _skip, _take);
        }

        private static IEnumerable<TEntity> SkipTake(IEnumerable<TEntity> collection, int? skip, int? take)
        {
            if (skip.HasValue) collection = collection.Skip(skip.Value);
            if (take.HasValue) collection = collection.Take(take.Value);
            return collection;
        }
    }
}
