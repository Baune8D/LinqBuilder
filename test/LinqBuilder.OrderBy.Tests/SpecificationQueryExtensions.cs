using LinqBuilder.OrderBy.Tests.TestHelpers;
using Shouldly;
using Xunit;

namespace LinqBuilder.OrderBy.Tests
{
    public class SpecificationQueryExtensions
    {
        [Fact]
        public void IsOrdered_OrderedSpecification_ShouldBeTrue()
        {
            var specification = new Value1Specification(1).OrderBy(new Value1OrderSpecification());

            specification.IsOrdered().ShouldBeTrue();
        }

        [Fact]
        public void IsOrdered_Specification_ShouldBeFalse()
        {
            var specification = new Value1Specification(1);

            specification.IsOrdered().ShouldBeFalse();
        }

        [Fact]
        public void AsOrdered_OrderedSpecification_ShouldNotBeNull()
        {
            var specification = new Value1Specification(1).OrderBy(new Value1OrderSpecification());

            specification.AsOrdered().ShouldNotBeNull();
        }

        [Fact]
        public void AsOrdered_Specification_ShouldBeNull()
        {
            var specification = new Value1Specification(1);

            specification.AsOrdered().ShouldBeNull();
        }
    }
}
