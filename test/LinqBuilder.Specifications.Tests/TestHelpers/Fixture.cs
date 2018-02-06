using System.Collections.Generic;
using System.Linq;

namespace LinqBuilder.Specifications.Tests.TestHelpers
{
    public class Fixture
    {
        public readonly int Value = 3;

        public Value1Specification Specification => new Value1Specification(Value);

        public IEnumerable<Entity> Collection => new List<Entity>
        {
            new Entity { Value1 = Value },
            new Entity { Value1 = Value },
            new Entity { Value1 = 2 }
        };

        public IQueryable<Entity> Query => Collection.AsQueryable();
    }
}
