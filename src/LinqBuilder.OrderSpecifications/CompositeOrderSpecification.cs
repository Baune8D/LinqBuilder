using System.Collections.Generic;
using System.Linq;
using LinqBuilder.Specifications;

namespace LinqBuilder.OrderSpecifications
{
    public class CompositeOrderSpecification<T> : ICompositeSpecification<T>
    {
        private readonly ISpecification<T> _specification;
        private readonly List<OrderSpecification<T>> _orderList;

        private int? _skip;
        private int? _take;

        public CompositeOrderSpecification(List<OrderSpecification<T>> orderList, OrderSpecification<T> right, int? skip = null, int? take = null)
        {
            _orderList = orderList;
            _orderList.Add(right);

            if (skip.HasValue) _skip = skip;
            if (take.HasValue) _take = take;
        }

        public CompositeOrderSpecification(ISpecification<T> specificaiton, List<OrderSpecification<T>> orderList, OrderSpecification<T> right, int? skip = null, int? take = null)
            : this(orderList, right, skip, take)
        {
            _specification = specificaiton;
        }

        public ICompositeSpecification<T> ThenBy(OrderSpecification<T> other)
        {
            return new CompositeOrderSpecification<T>(_specification, _orderList, other, _skip, _take);
        }

        public ICompositeSpecification<T> Skip(int count)
        {
            _skip = count;
            return this;
        }

        public ICompositeSpecification<T> Take(int count)
        {
            _take = count;
            return this;
        }

        public IQueryable<T> Invoke(IQueryable<T> query)
        { 
            if (_specification != null) query = _specification.Invoke(query);

            var orderedQuery = _orderList[0].Invoke(query);

            for (var i = 1; i < _orderList.Count; i++)
            {
                orderedQuery = _orderList[i].Invoke(orderedQuery);
            }

            return OrderHelper.SkipTake(orderedQuery.AsQueryable(), _skip, _take);
        }

        public IEnumerable<T> Invoke(IEnumerable<T> collection)
        {
            if (_specification != null) collection = _specification.Invoke(collection);

            var orderedCollection = _orderList[0].Invoke(collection);

            for (var i = 1; i < _orderList.Count; i++)
            {
                orderedCollection = _orderList[i].Invoke(orderedCollection);
            }

            return OrderHelper.SkipTake(orderedCollection.AsEnumerable(), _skip, _take);
        }
    }
}
