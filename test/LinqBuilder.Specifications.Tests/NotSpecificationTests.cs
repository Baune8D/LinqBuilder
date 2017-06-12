using LinqBuilder.Filtering.Tests.TestHelpers;
using Shouldly;
using Xunit;

namespace LinqBuilder.Filtering.Tests
{
    public class NotSpecificationTests
    {
        [Fact]
        public void IsSatisfiedBy_WrongValue_ShouldReturnFalse()
        {
            var specification = new Value1Specification(5).Not();

            var entity = new TestEntity { Value1 = 5 };
            specification.IsSatisfiedBy(entity).ShouldBeFalse();
        }

        [Fact]
        public void IsSatisfiedBy_CorrectValue_ShouldReturnTrue()
        {
            var specification = new Value1Specification(5).Not();

            var entity = new TestEntity { Value1 = 4 };
            specification.IsSatisfiedBy(entity).ShouldBeTrue();
        }
    }
}
