using System;
using System.Linq;
using LinqBuilder.Tests.TestHelpers;
using Shouldly;
using Xunit;

namespace LinqBuilder.Tests
{
    public class LinqExtensionsTests
    {
        private readonly Fixture _fixture;

        public LinqExtensionsTests()
        {
            _fixture = new Fixture();
            _fixture.AddToCollection(3, 1);
            _fixture.AddToCollection(3, 1);
            _fixture.AddToCollection(1, 1);
        }

        [Fact]
        public void Where_IQueryable_ShouldReturnFilteredQueryable()
        {
            const int value = 3;

            var result = _fixture.Query.Where(new Value1Specification(value));

            result.ShouldBeAssignableTo<IQueryable<Entity>>();
            result.ShouldAllBe(e => e.Value1 == value);
        }

        [Fact]
        public void Where_IQueryable_ShouldThrowArgumentNullException()
        {
            Should.Throw<ArgumentNullException>(() => _fixture.Query.Where(null));
        }

        [Fact]
        public void Where_IEnumerable_ShouldReturnFilteredEnumerable()
        {
            const int value = 3;

            var result = _fixture.Collection.Where(new Value1Specification(value));

            result.ShouldNotBeAssignableTo<IQueryable<Entity>>();
            result.ShouldAllBe(e => e.Value1 == value);
        }

        [Fact]
        public void Where_IEnumerable_ShouldThrowArgumentNullException()
        {
            Should.Throw<ArgumentNullException>(() => _fixture.Collection.Where(null));
        }

        [Fact]
        public void Any_IQueryable_ShouldBeTrue()
        {
            _fixture.Query.Any(new Value1Specification(3)).ShouldBeTrue();
        }

        [Fact]
        public void Any_IQueryable_ShouldBeFalse()
        {
            _fixture.Query.Any(new Value1Specification(4)).ShouldBeFalse();
        }

        [Fact]
        public void Any_IQueryable_ShouldThrowArgumentNullException()
        {
            Should.Throw<ArgumentNullException>(() => _fixture.Query.Any(null));
        }

        [Fact]
        public void Any_IEnumerable_ShouldBeTrue()
        {
            _fixture.Collection.Any(new Value1Specification(3)).ShouldBeTrue();
        }

        [Fact]
        public void Any_IEnumerable_ShouldBeFalse()
        {
            _fixture.Collection.Any(new Value1Specification(4)).ShouldBeFalse();
        }

        [Fact]
        public void Any_IEnumerable_ShouldThrowArgumentNullException()
        {
            Should.Throw<ArgumentNullException>(() => _fixture.Collection.Any(null));
        }

        [Fact]
        public void All_IQueryable_ShouldBeTrue()
        {
            _fixture.Query.All(new Value2Specification(1)).ShouldBeTrue();
        }

        [Fact]
        public void All_IQueryable_ShouldBeFalse()
        {
            _fixture.Query.All(new Value1Specification(3)).ShouldBeFalse();
        }

        [Fact]
        public void All_IQueryable_ShouldThrowArgumentNullException()
        {
            Should.Throw<ArgumentNullException>(() => _fixture.Query.All(null));
        }

        [Fact]
        public void All_IEnumerable_ShouldBeTrue()
        {
            _fixture.Collection.All(new Value2Specification(1)).ShouldBeTrue();
        }

        [Fact]
        public void All_IEnumerable_ShouldBeFalse()
        {
            _fixture.Collection.All(new Value1Specification(3)).ShouldBeFalse();
        }

        [Fact]
        public void All_IEnumerable_ShouldThrowArgumentNullException()
        {
            Should.Throw<ArgumentNullException>(() => _fixture.Collection.All(null));
        }

        [Fact]
        public void Count_IQueryable_ShouldBeFilteredCount()
        {
            _fixture.Query.Count(new Value1Specification(3)).ShouldBe(2);
        }

        [Fact]
        public void Count_IQueryable_ShouldThrowArgumentNullException()
        {
            Should.Throw<ArgumentNullException>(() => _fixture.Query.Count(null));
        }

        [Fact]
        public void Count_IEnumerable_ShouldBeFilteredCount()
        {
            _fixture.Collection.Count(new Value1Specification(3)).ShouldBe(2);
        }

        [Fact]
        public void Count_IEnumerable_ShouldThrowArgumentNullException()
        {
            Should.Throw<ArgumentNullException>(() => _fixture.Collection.Count(null));
        }

        [Fact]
        public void LongCount_IQueryable_ShouldBeFilteredCount()
        {
            _fixture.Query.LongCount(new Value1Specification(3)).ShouldBe(2);
        }

        [Fact]
        public void LongCount_IQueryable_ShouldThrowArgumentNullException()
        {
            Should.Throw<ArgumentNullException>(() => _fixture.Query.LongCount(null));
        }

        [Fact]
        public void LongCount_IEnumerable_ShouldBeFilteredCount()
        {
            _fixture.Collection.LongCount(new Value1Specification(3)).ShouldBe(2);
        }

        [Fact]
        public void LongCount_IEnumerable_ShouldThrowArgumentNullException()
        {
            Should.Throw<ArgumentNullException>(() => _fixture.Collection.LongCount(null));
        }

        [Fact]
        public void First_IQueryable_ShouldBeStoreEntity()
        {
            _fixture.Query.First(new Value1Specification(3)).ShouldBe(_fixture.Store[0]);
        }

        [Fact]
        public void First_IQueryable_ShouldThrowArgumentNullException()
        {
            Should.Throw<ArgumentNullException>(() => _fixture.Query.First(null));
        }

        [Fact]
        public void First_IEnumerable_ShouldBeStoreEntity()
        {
            _fixture.Collection.First(new Value1Specification(3)).ShouldBe(_fixture.Store[0]);
        }

        [Fact]
        public void First_IEnumerable_ShouldThrowArgumentNullException()
        {
            Should.Throw<ArgumentNullException>(() => _fixture.Collection.First(null));
        }

        [Fact]
        public void FirstOrDefault_IQueryable_ShouldBeStoreEntity()
        {
            _fixture.Query.FirstOrDefault(new Value1Specification(3)).ShouldBe(_fixture.Store[0]);
        }

        [Fact]
        public void FirstOrDefault_IQueryable_ShouldThrowArgumentNullException()
        {
            Should.Throw<ArgumentNullException>(() => _fixture.Query.FirstOrDefault(null));
        }

        [Fact]
        public void FirstOrDefault_IEnumerable_ShouldBeStoreEntity()
        {
            _fixture.Collection.FirstOrDefault(new Value1Specification(3)).ShouldBe(_fixture.Store[0]);
        }

        [Fact]
        public void FirstOrDefault_IEnumerable_ShouldThrowArgumentNullException()
        {
            Should.Throw<ArgumentNullException>(() => _fixture.Collection.FirstOrDefault(null));
        }

        [Fact]
        public void Single_IQueryable_ShouldBeStoreEntity()
        {
            _fixture.Query.Single(new Value1Specification(1)).ShouldBe(_fixture.Store[2]);
        }

        [Fact]
        public void Single_IQueryable_ShouldThrowArgumentNullException()
        {
            Should.Throw<ArgumentNullException>(() => _fixture.Query.Single(null));
        }

        [Fact]
        public void Single_IEnumerable_ShouldBeStoreEntity()
        {
            _fixture.Collection.Single(new Value1Specification(1)).ShouldBe(_fixture.Store[2]);
        }

        [Fact]
        public void Single_IEnumerable_ShouldThrowArgumentNullException()
        {
            Should.Throw<ArgumentNullException>(() => _fixture.Collection.Single(null));
        }

        [Fact]
        public void SingleOrDefault_IQueryable_ShouldBeStoreEntity()
        {
            _fixture.Query.SingleOrDefault(new Value1Specification(1)).ShouldBe(_fixture.Store[2]);
        }

        [Fact]
        public void SingleOrDefault_IQueryable_ShouldThrowArgumentNullException()
        {
            Should.Throw<ArgumentNullException>(() => _fixture.Query.SingleOrDefault(null));
        }

        [Fact]
        public void SingleOrDefault_IEnumerable_ShouldBeStoreEntity()
        {
            _fixture.Collection.SingleOrDefault(new Value1Specification(1)).ShouldBe(_fixture.Store[2]);
        }

        [Fact]
        public void SingleOrDefault_IEnumerable_ShouldThrowArgumentNullException()
        {
            Should.Throw<ArgumentNullException>(() => _fixture.Collection.SingleOrDefault(null));
        }
    }
}
