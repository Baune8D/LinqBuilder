using LinqBuilder.Tests.TestHelpers;
using Shouldly;
using Xunit;

namespace LinqBuilder.Tests
{
    public class SpecTests
    {
        [Fact]
        public void IsSatisfiedBy_DefaltValue()
        {
            new Spec<Entity>()
                .IsSatisfiedBy(new Entity())
                .ShouldBeTrue();
        }

        [Fact]
        public void IsSatisfiedBy_Expression_ShouldBeTrue()
        {
            const int value = 3;

            new Spec<Entity>(entity => entity.Value1 == value)
                .IsSatisfiedBy(new Entity { Value1 = value })
                .ShouldBeTrue();
        }

        [Fact]
        public void IsSatisfiedBy_Expression_ShouldBeFalse()
        {
            new Spec<Entity>(entity => entity.Value1 == 2)
                .IsSatisfiedBy(new Entity { Value1 = 1 })
                .ShouldBeFalse();
        }
    }
}
