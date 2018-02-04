using System.Linq;
using LinqBuilder.Specifications.Tests.TestHelpers;
using Shouldly;
using Xunit;

namespace LinqBuilder.Specifications.Tests
{
    public class SpecificationTests : IClassFixture<TestFixture>
    {
        private readonly TestFixture _fixture;

        public SpecificationTests(TestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void IsSatisfiedBy_DefaltValue()
        {
            new Specification<TestEntity>()
                .IsSatisfiedBy(new TestEntity())
                .ShouldBeTrue();
        }

        [Theory]
        [ClassData(typeof(TestData))]
        public void IsSatisfiedBy_Theory(TestEntity entity, bool expected)
        {
            _fixture.Specification
                .IsSatisfiedBy(entity)
                .ShouldBe(expected);
        }

        [Fact]
        public void Invoke_IQueryable_ShouldReturnFilteredQueryable()
        {
            var result = _fixture.Specification.Invoke(_fixture.TestQuery);

            result.ShouldBeAssignableTo<IQueryable<TestEntity>>();
            result.ShouldAllBe(e => e.Value1 == _fixture.Value);
        }

        [Fact]
        public void Invoke_IEnumerable_ShouldReturnFilteredEnumerable()
        {
            var result = _fixture.Specification.Invoke(_fixture.TestCollection);

            result.ShouldNotBeAssignableTo<IQueryable<TestEntity>>();
            result.ShouldAllBe(e => e.Value1 == _fixture.Value);
        }

        private class TestData : TheoryData<TestEntity, bool>
        {
            public TestData()
            {
                Add(new TestEntity { Value1 = 3 }, true);
                Add(new TestEntity { Value1 = 4 }, false);
            }
        }
    }
}
