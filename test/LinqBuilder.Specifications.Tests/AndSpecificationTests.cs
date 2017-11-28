using LinqBuilder.Specifications.Tests.TestHelpers;
using Shouldly;
using Xunit;

namespace LinqBuilder.Specifications.Tests
{
    public class AndSpecificationTests
    {
        [Fact]
        public void IsSatisfiedBy_OneWrongValue_ShouldReturnFalse()
        {
            var specification = new Value1Specification(3)
                .And(new Value2Specification(5));

            var entity = new TestEntity
            {
                Value1 = 3,
                Value2 = 4
            };

            specification.IsSatisfiedBy(entity).ShouldBeFalse();
        }

        [Fact]
        public void IsSatisfiedBy_TwoCorrectValues_ShouldReturnTrue()
        {
            var specification = new Value1Specification(3)
                .And(new Value2Specification(5));

            var entity = new TestEntity
            {
                Value1 = 3,
                Value2 = 5
            };

            specification.IsSatisfiedBy(entity).ShouldBeTrue();
        }
    }
}
