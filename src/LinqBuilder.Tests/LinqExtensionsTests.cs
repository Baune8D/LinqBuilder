using System;
using System.Linq;
using FluentAssertions;
using LinqBuilder.Testing;
using Xunit;

namespace LinqBuilder.Tests
{
    public class LinqExtensionsTests
    {
        private readonly ISpecification<Entity> _emptySpecification = Spec<Entity>.New();
        private readonly ISpecification<Entity> _value1ShouldBe1 = Spec<Entity>.New(entity => entity.Value1 == 1);
        private readonly ISpecification<Entity> _value1ShouldBe3 = Spec<Entity>.New(entity => entity.Value1 == 3);
        private readonly ISpecification<Entity> _value1ShouldBe4 = Spec<Entity>.New(entity => entity.Value1 == 4);
        private readonly ISpecification<Entity> _value2ShouldBe1 = Spec<Entity>.New(entity => entity.Value2 == 1);

        private readonly Fixture _fixture;

        public LinqExtensionsTests()
        {
            _fixture = new Fixture();
            _fixture.AddToCollection(3, 1);
            _fixture.AddToCollection(3, 1);
            _fixture.AddToCollection(1, 1);
        }

        [Fact]
        public void Where_IQueryable_ShouldReturnCorrectResult()
        {
            var result = _fixture.Query.Where(_value1ShouldBe3);
            result.Should().BeAssignableTo<IQueryable<Entity>>();
            result.Should().AllSatisfy(e => e.Value1.Should().Be(3));
        }

        [Fact]
        public void Where_IEnumerable_ShouldReturnCorrectResult()
        {
            var result = _fixture.Collection.Where(_value1ShouldBe3).ToList();
            result.Should().NotBeAssignableTo<IQueryable<Entity>>();
            result.Should().AllSatisfy(e => e.Value1.Should().Be(3));
        }

        [Fact]
        public void WhereEmpty_IQueryable_ShouldReturnEqualQuery()
        {
            var result = _fixture.Query.Where(_emptySpecification);
            result.Should().BeAssignableTo<IQueryable<Entity>>();
            result.Should().Equal(_fixture.Query);
        }

        [Fact]
        public void WhereEmpty_IEnumerable_ShouldReturnEqualCollection()
        {
            var result = _fixture.Collection.Where(_emptySpecification).ToList();
            result.Should().NotBeAssignableTo<IQueryable<Entity>>();
            result.Should().Equal(_fixture.Collection);
        }

        [Fact]
        public void Any_IQueryable_ShouldBeTrue()
        {
            _fixture.Query
                .Any(_value1ShouldBe3)
                .Should().BeTrue();
        }

        [Fact]
        public void Any_IQueryable_ShouldBeFalse()
        {
            _fixture.Query
                .Any(_value1ShouldBe4)
                .Should().BeFalse();
        }

        [Fact]
        public void Any_IEnumerable_ShouldBeTrue()
        {
            _fixture.Collection
                .Any(_value1ShouldBe3)
                .Should().BeTrue();
        }

        [Fact]
        public void Any_IEnumerable_ShouldBeFalse()
        {
            _fixture.Collection
                .Any(_value1ShouldBe4)
                .Should().BeFalse();
        }

        [Fact]
        public void AnyEmpty_IQueryable_ShouldBeTrue()
        {
            _fixture.Query
                .Any(_emptySpecification)
                .Should().BeTrue();
        }

        [Fact]
        public void AnyEmpty_IEnumerable_ShouldBeTrue()
        {
            _fixture.Collection
                .Any(_emptySpecification)
                .Should().BeTrue();
        }

        [Fact]
        public void All_IQueryable_ShouldBeTrue()
        {
            _fixture.Query
                .All(_value2ShouldBe1)
                .Should().BeTrue();
        }

        [Fact]
        public void All_IQueryable_ShouldBeFalse()
        {
            _fixture.Query
                .All(_value1ShouldBe3)
                .Should().BeFalse();
        }

        [Fact]
        public void All_IEnumerable_ShouldBeTrue()
        {
            _fixture.Collection
                .All(_value2ShouldBe1)
                .Should().BeTrue();
        }

        [Fact]
        public void All_IEnumerable_ShouldBeFalse()
        {
            _fixture.Collection
                .All(_value1ShouldBe3)
                .Should().BeFalse();
        }

        [Fact]
        public void AllEmpty_IQueryable_ShouldBeTrue()
        {
            _fixture.Query
                .All(_emptySpecification)
                .Should().BeTrue();
        }

        [Fact]
        public void AllEmpty_IEnumerable_ShouldBeTrue()
        {
            _fixture.Collection
                .All(_emptySpecification)
                .Should().BeTrue();
        }

        [Fact]
        public void Count_IQueryable_ShouldReturnCorrectResult()
        {
            _fixture.Query
                .Count(_value1ShouldBe3)
                .Should().Be(2);
        }

        [Fact]
        public void CountEmpty_IQueryable_ShouldReturnCorrectResult()
        {
            _fixture.Query
                .Count(_emptySpecification)
                .Should().Be(_fixture.Collection.Count());
        }

        [Fact]
        public void Count_IEnumerable_ShouldReturnCorrectResult()
        {
            _fixture.Collection
                .Count(_value1ShouldBe3)
                .Should().Be(2);
        }

        [Fact]
        public void CountEmpty_IEnumerable_ShouldReturnEqualCount()
        {
            _fixture.Collection
                .Count(_emptySpecification)
                .Should().Be(_fixture.Collection.Count());
        }

        [Fact]
        public void First_IQueryable_ShouldReturnCorrectResult()
        {
            _fixture.Query
                .First(_value1ShouldBe3)
                .Should().Be(_fixture.Store[0]);
        }

        [Fact]
        public void FirstEmpty_IQueryable_ShouldReturnCorrectResult()
        {
            _fixture.Query
                .First(_emptySpecification)
                .Should().Be(_fixture.Store[0]);
        }

        [Fact]
        public void First_IEnumerable_ShouldReturnCorrectResult()
        {
            _fixture.Collection
                .First(_value1ShouldBe3)
                .Should().Be(_fixture.Store[0]);
        }

        [Fact]
        public void FirstEmpty_IEnumerable_ShouldReturnCorrectResult()
        {
            _fixture.Collection
                .First(_emptySpecification)
                .Should().Be(_fixture.Store[0]);
        }

        [Fact]
        public void FirstOrDefault_IQueryable_ShouldReturnCorrectResult()
        {
            _fixture.Query
                .FirstOrDefault(_value1ShouldBe3)
                .Should().Be(_fixture.Store[0]);
        }

        [Fact]
        public void FirstOrDefaultEmpty_IQueryable_ShouldReturnCorrectResult()
        {
            _fixture.Query
                .FirstOrDefault(_emptySpecification)
                .Should().Be(_fixture.Store[0]);
        }

        [Fact]
        public void FirstOrDefault_IEnumerable_ShouldReturnCorrectResult()
        {
            _fixture.Collection
                .FirstOrDefault(_value1ShouldBe3)
                .Should().Be(_fixture.Store[0]);
        }

        [Fact]
        public void FirstOrDefaultEmpty_IEnumerable_ShouldReturnCorrectResult()
        {
            _fixture.Collection
                .FirstOrDefault(_emptySpecification)
                .Should().Be(_fixture.Store[0]);
        }

        [Fact]
        public void Single_IQueryable_ShouldReturnCorrectResult()
        {
            _fixture.Query
                .Single(_value1ShouldBe1)
                .Should().Be(_fixture.Store[2]);
        }

        [Fact]
        public void SingleEmpty_IQueryable_ShouldReturnCorrectResult()
        {
            Action act = () => _fixture.Query.Single(_emptySpecification);

            act.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Single_IEnumerable_ShouldReturnCorrectResult()
        {
            _fixture.Collection
                .Single(_value1ShouldBe1)
                .Should().Be(_fixture.Store[2]);
        }

        [Fact]
        public void SingleEmpty_IEnumerable_ShouldReturnCorrectResult()
        {
            Action act = () => _fixture.Collection.Single(_emptySpecification);

            act.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void SingleOrDefault_IQueryable_ShouldReturnCorrectResult()
        {
            _fixture.Query
                .SingleOrDefault(_value1ShouldBe1)
                .Should().Be(_fixture.Store[2]);
        }

        [Fact]
        public void SingleOrDefaultEmpty_IQueryable_ShouldReturnCorrectResult()
        {
            Action act = () => _fixture.Query.SingleOrDefault(_emptySpecification);

            act.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void SingleOrDefault_IEnumerable_ShouldReturnCorrectResult()
        {
            _fixture.Collection
                .SingleOrDefault(_value1ShouldBe1)
                .Should().Be(_fixture.Store[2]);
        }

        [Fact]
        public void SingleOrDefaultEmpty_IEnumerable_ShouldReturnCorrectResult()
        {
            Action act = () => _fixture.Collection.SingleOrDefault(_emptySpecification);

            act.Should().Throw<InvalidOperationException>();
        }
    }
}
