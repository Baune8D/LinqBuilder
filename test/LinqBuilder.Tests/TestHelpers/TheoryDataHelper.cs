using Xunit;

namespace LinqBuilder.Tests.TestHelpers
{
    public abstract class TheoryDataHelper : TheoryData<Entity, bool>
    {
        public void AddEntity(int value1, int value2, bool expected)
        {
            Add(new Entity
            {
                Value1 = value1, 
                Value2 = value2
            }, 
            expected);
        }
    }
}
