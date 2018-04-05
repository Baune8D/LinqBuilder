using LinqBuilder.Tests.TestHelpers;
using Shouldly;
using Xunit;

namespace LinqBuilder.Tests
{
    public class SpecTests
    {
        private readonly Fixture _fixture;

        public SpecTests()
        {
            _fixture = new Fixture();
            _fixture.AddToCollection(3, 1);
            _fixture.AddToCollection(3, 1);
            _fixture.AddToCollection(1, 1);
        }

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
            new Spec<Entity>(entity => entity.Value1 == _fixture.Value)
                .IsSatisfiedBy(new Entity { Value1 = _fixture.Value })
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
