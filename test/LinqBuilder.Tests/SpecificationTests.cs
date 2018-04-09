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

        [Fact]
        public void Use_Specification_ShouldBeSameClass()
        {
            var specification = new Value1Specification(1);
            specification.AsInterface().ShouldBe(specification);
        }

        [Fact]
        public void Create_EmptyConstructor_ShouldBeInterface()
        {
            Specification<Entity>.New().ShouldBeAssignableTo<ISpecification<Entity>>();
        }

        [Fact]
        public void Create_ExpressionConstructor_ShouldBeInterface()
        {
            Specification<Entity>.New(entity => true).ShouldBeAssignableTo<ISpecification<Entity>>();
        }

        [Fact]
        public void Invoke_IQueryable_ShouldReturnFilteredQueryable()
        {
            const int value = 3;

            var result = new Value1Specification(value).Invoke(_fixture.Query);

            result.ShouldBeAssignableTo<IQueryable<Entity>>();
            result.ShouldAllBe(e => e.Value1 == value);
        }

        [Fact]
        public void Invoke_IEnumerable_ShouldReturnFilteredEnumerable()
        {
            const int value = 3;

            var result = new Value1Specification(value).Invoke(_fixture.Collection);

            result.ShouldNotBeAssignableTo<IQueryable<Entity>>();
            result.ShouldAllBe(e => e.Value1 == value);
        }

        [Theory]
        [ClassData(typeof(TestData))]
        public void IsSatisfiedBy_Theory(Entity entity, bool expected)
        {
            new Value1Specification(3)
                .IsSatisfiedBy(entity)
                .ShouldBe(expected);
        }

        private class TestData : EntityTheoryData
        {
            public TestData()
            {
                AddEntity(3, 1, true);
                AddEntity(4, 1, false);
            }
        }
    }
}
