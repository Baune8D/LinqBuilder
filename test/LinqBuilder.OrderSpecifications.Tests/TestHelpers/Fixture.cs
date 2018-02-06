using System.Collections.Generic;
using System.Linq;

namespace LinqBuilder.OrderSpecifications.Tests.TestHelpers
{
    public class Fixture
    {
        public IEnumerable<Entity> Collection => new List<Entity>
        {
            new Entity { Value1 = 3 },
            new Entity { Value1 = 1 },
            new Entity { Value1 = 2 }
        };

        public IQueryable<Entity> Query => Collection.AsQueryable();
    }
}
