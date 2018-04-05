using System.Collections.Generic;
using System.Linq;

namespace LinqBuilder.Tests.TestHelpers
{
    public class Fixture
    {
        private readonly List<Entity> _collection;

        public IEnumerable<Entity> Collection => _collection.AsEnumerable();
        public IQueryable<Entity> Query => _collection.AsQueryable();

        public readonly int Value = 3;
        public Value1Specification Specification => new Value1Specification(Value);

        public Fixture()
        {
            _collection = new List<Entity>();
        }

        public void AddToCollection(int value1, int value2)
        {
            _collection.Add(new Entity
            {
                Value1 = value1,
                Value2 = value2
            });
        }
    }
}
