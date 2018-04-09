using System.Collections.Generic;

namespace LinqBuilder.OrderBy
{
    public class Ordering<TEntity>
    {
        public List<IOrderSpecification<TEntity>> OrderList { get; set; }

        public int? Skip { get; set; }

        public int? Take { get; set; }
    }
}
