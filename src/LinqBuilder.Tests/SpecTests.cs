using FluentAssertions;
using LinqBuilder.Tests.Data;
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
                .Should().BeTrue();
        }

        [Fact]
        public void Constructor_InlineExpression_ShouldBeTrue()
        {
            new Spec<Entity>(entity => entity.Value1 == 1)
                .IsSatisfiedBy(new Entity { Value1 = 1 })
                .Should().BeTrue();
        }

        [Fact]
        public void Constructor_InlineExpression_ShouldBeFalse()
        {
            new Spec<Entity>(entity => entity.Value1 == 2)
                .IsSatisfiedBy(new Entity { Value1 = 1 })
                .Should().BeFalse();
        }
    }
}
