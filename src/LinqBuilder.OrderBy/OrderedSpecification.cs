using System.Collections.Generic;
using System.Linq;

namespace LinqBuilder.OrderBy
{
    public class OrderedSpecification<TEntity> : IOrderedSpecification<TEntity>
        where TEntity : class
    {
        private readonly ILinqBuilderQuery<TEntity> _specification;
        private readonly List<IOrderSpecification<TEntity>> _orderList;

        private int? _skip;
        private int? _take;

        public OrderedSpecification(List<IOrderSpecification<TEntity>> orderList, IOrderSpecification<TEntity> right, int? skip = null, int? take = null)
        {
            _orderList = orderList;
            _orderList.Add(right);
            _skip = skip;
            _take = take;
        }

        public OrderedSpecification(ILinqQuery<TEntity> specificaiton, List<IOrderSpecification<TEntity>> orderList, IOrderSpecification<TEntity> right, int? skip = null, int? take = null)
            : this(orderList, right, skip, take)
        {
            _specification = specificaiton;
        }

        public IOrderedSpecification<TEntity> ThenBy(IOrderSpecification<TEntity> orderSpecification)
        {
            return new OrderedSpecification<TEntity>(_specification, _orderList, orderSpecification, _skip, _take);
        }

        public IOrderedSpecification<TEntity> Skip(int count)
        {
            _skip = count;
            return this;
        }

        public IOrderedSpecification<TEntity> Take(int count)
        {
            _take = count;
            return this;
        }

        public IQueryable<TEntity> Invoke(IQueryable<TEntity> query)
        {
            if (_specification != null) query = _specification.Invoke(query);
            var orderedQuery = _orderList[0].InvokeSort(query);

            for (var i = 1; i < _orderList.Count; i++)
            {
                orderedQuery = _orderList[i].InvokeSort(orderedQuery);
            }

            return SkipTake(orderedQuery, _skip, _take);
        }

        public IEnumerable<TEntity> Invoke(IEnumerable<TEntity> collection)
        {
            if (_specification != null) collection = _specification.Invoke(collection);
            var orderedCollection = _orderList[0].InvokeSort(collection);

            for (var i = 1; i < _orderList.Count; i++)
            {
                orderedCollection = _orderList[i].InvokeSort(orderedCollection);
            }

            return SkipTake(orderedCollection, _skip, _take);
        }

        private static IEnumerable<TEntity> SkipTake(IEnumerable<TEntity> collection, int? skip, int? take)
        {
            if (skip.HasValue) collection = collection.Skip(skip.Value);
            if (take.HasValue) collection = collection.Take(take.Value);
            return collection;
        }

        private static IQueryable<TEntity> SkipTake(IQueryable<TEntity> query, int? skip, int? take)
        {
            if (skip.HasValue) query = query.Skip(skip.Value);
            if (take.HasValue) query = query.Take(take.Value);
            return query;
        }
    }
}
