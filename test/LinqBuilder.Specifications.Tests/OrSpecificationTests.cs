using LinqBuilder.Specifications.Tests.TestHelpers;
using Shouldly;
using Xunit;

namespace LinqBuilder.Specifications.Tests
{
    public class OrSpecificationTests
    {
        [Fact]
        public void IsSatisfiedBy_NoValueMatching_ShouldReturnFalse()
        {
            var specification = new Value1Specification(3)
                .Or(new Value1Specification(5));

            var entity = new TestEntity { Value1 = 4 };
            specification.IsSatisfiedBy(entity).ShouldBeFalse();
        }

        [Fact]
        public void IsSatisfiedBy_BothValuesMathcing_ShouldReturnTrue()
        {
            var specification = new Value1Specification(3)
                .Or(new Value1Specification(5));

            var entity = new TestEntity { Value1 = 3 };
            specification.IsSatisfiedBy(entity).ShouldBeTrue();

            entity.Value1 = 5;
            specification.IsSatisfiedBy(entity).ShouldBeTrue();
        }
    }
}
