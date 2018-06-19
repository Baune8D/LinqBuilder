using LinqBuilder.Tests.TestHelpers;
using Shouldly;
using Xunit;

namespace LinqBuilder.Tests
{
    public class SpecTests
    {
        [Fact]
        public void Constructor_DefaultExpression_ShouldBeTrue()
        {
            new Spec<Entity>()
                .IsSatisfiedBy(new Entity())
                .ShouldBeTrue();
        }

        [Fact]
        public void Constructor_InlineExpression_ShouldBeTrue()
        {
            new Spec<Entity>(entity => entity.Value1 == 1)
                .IsSatisfiedBy(new Entity { Value1 = 1 })
                .ShouldBeTrue();
        }

        [Fact]
        public void Constructor_InlineExpression_ShouldBeFalse()
        {
            new Spec<Entity>(entity => entity.Value1 == 2)
                .IsSatisfiedBy(new Entity { Value1 = 1 })
                .ShouldBeFalse();
        }
    }
}
