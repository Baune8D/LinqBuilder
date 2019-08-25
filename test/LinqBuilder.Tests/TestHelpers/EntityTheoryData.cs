using Xunit;

namespace LinqBuilder.Tests.TestHelpers
{
    public abstract class EntityTheoryData : TheoryData<Entity, bool>
    {
        public void AddEntity(int value1, int value2, bool expected)
        {
            var entity = new Entity
            {
                Value1 = value1,
                Value2 = value2
            };
            Add(entity, expected);
        }

        public void AddEntity(int value1, int value2, int value3, int value4, bool expected)
        {
            var entity = new Entity
            {
                Value1 = value1,
                Value2 = value2,
                Value3 = value3,
                Value4 = value4
            };
            Add(entity, expected);
        }
    }
}
