using System;
using System.Linq;
using LinqBuilder.OrderBy.Tests.TestHelpers;
using Shouldly;
using Xunit;

namespace LinqBuilder.OrderBy.Tests
{
    public class SpecificationExtensionsTests
    {
        private readonly Fixture _fixture;

        public SpecificationExtensionsTests()
        {
            _fixture = new Fixture();
            _fixture.AddToCollection(3, 1, 1);
            _fixture.AddToCollection(1, 1, 1);
            _fixture.AddToCollection(2, 1, 1);
        }

        [Fact]
        public void Create_EmptyConstructor_ShouldBeInterface()
        {
            OrderSpecification<Entity, int>.New().ShouldBeAssignableTo<IOrderSpecification<Entity>>();
        }

        [Fact]
        public void Create_ExpressionSortConstructor_ShouldBeInterface()
        {
            OrderSpecification<Entity, int>.New(entity => 1).ShouldBeAssignableTo<IOrderSpecification<Entity>>();
        }

        [Fact]
        public void OrderBy_IQueryable_ShouldReturnFilteredAndOrderedList()
        {
            var specification = new Specification<Entity>()
                .OrderBy(new Value1OrderSpecification());

            var result = specification.Invoke(_fixture.Query).ToList();

            result.Count.ShouldBe(3);
            result[0].Value1.ShouldBe(1);
            result[1].Value1.ShouldBe(2);
            result[2].Value1.ShouldBe(3);
        }

        [Fact]
        public void OrderBy_IEnumerable_ShouldReturnFilteredAndOrderedList()
        {
            var specification = new Specification<Entity>()
                .OrderBy(new Value1OrderSpecification());

            var result = specification.Invoke(_fixture.Collection).ToList();

            result.Count.ShouldBe(3);
            result[0].Value1.ShouldBe(1);
            result[1].Value1.ShouldBe(2);
            result[2].Value1.ShouldBe(3);
        }

        [Fact]
        public void OrderBy_Null_ShouldThrowArgumentNullException()
        {
            Should.Throw<ArgumentNullException>(() => new Specification<Entity>().OrderBy(null));
        }

        [Fact]
        public void UseOrdering_IQueryable_ShouldReturnFilteredAndOrderedList()
        {
            var ordering = new Value1OrderSpecification().Skip(1).Take(1);

            var specification = new Specification<Entity>()
                .UseOrdering(ordering);

            var result = specification.Invoke(_fixture.Query).ToList();

            result.Count.ShouldBe(1);
            result[0].Value1.ShouldBe(2);
        }

        [Fact]
        public void UseOrdering_IEnumerable_ShouldReturnFilteredAndOrderedList()
        {
            var ordering = new Value1OrderSpecification().Skip(1).Take(1);

            var specification = new Specification<Entity>()
                .UseOrdering(ordering);

            var result = specification.Invoke(_fixture.Collection).ToList();

            result.Count.ShouldBe(1);
            result[0].Value1.ShouldBe(2);
        }

        [Fact]
        public void UseOrdering_Null_ShouldThrowArgumentNullException()
        {
            Should.Throw<ArgumentNullException>(() => new Specification<Entity>().UseOrdering(null));
        }
    }
}
