using System.Linq;
using LinqBuilder.Tests.TestHelpers;
using Shouldly;
using Xunit;

namespace LinqBuilder.Tests
{
    public class SpecificationTests
    {
        private readonly Fixture _fixture;

        public SpecificationTests()
        {
            _fixture = new Fixture();
            _fixture.AddToCollection(3, 1);
            _fixture.AddToCollection(3, 1);
            _fixture.AddToCollection(1, 1);
        }

        [Theory]
        [ClassData(typeof(TestData))]
        public void IsSatisfiedBy_Theory(Entity entity, bool expected)
        {
            _fixture.Specification
                .IsSatisfiedBy(entity)
                .ShouldBe(expected);
        }

        [Fact]
        public void Invoke_IQueryable_ShouldReturnFilteredQueryable()
        {
            var result = _fixture.Specification.Invoke(_fixture.Query);

            result.ShouldBeAssignableTo<IQueryable<Entity>>();
            result.ShouldAllBe(e => e.Value1 == _fixture.Value);
        }

        [Fact]
        public void Invoke_IEnumerable_ShouldReturnFilteredEnumerable()
        {
            var result = _fixture.Specification.Invoke(_fixture.Collection);

            result.ShouldNotBeAssignableTo<IQueryable<Entity>>();
            result.ShouldAllBe(e => e.Value1 == _fixture.Value);
        }

        private class TestData : TheoryData<Entity, bool>
        {
            public TestData()
            {
                Add(new Entity { Value1 = 3 }, true);
                Add(new Entity { Value1 = 4 }, false);
            }
        }
    }
}
