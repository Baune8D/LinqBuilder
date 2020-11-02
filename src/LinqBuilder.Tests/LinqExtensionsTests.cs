using System;
using System.Linq;
using LinqBuilder.Core;
using LinqBuilder.Tests.Internal;
using Shouldly;
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
            result.ShouldBeAssignableTo<IQueryable<Entity>>();
            result.ShouldAllBe(e => e.Value1 == 3);
        }

        [Fact]
        public void Where_IEnumerable_ShouldReturnCorrectResult()
        {
            var result = _fixture.Collection.Where(_value1ShouldBe3);
            result.ShouldNotBeAssignableTo<IQueryable<Entity>>();
            result.ShouldAllBe(e => e.Value1 == 3);
        }

        [Fact]
        public void WhereEmpty_IQueryable_ShouldReturnEqualQuery()
        {
            var result = _fixture.Query.Where(_emptySpecification);
            result.ShouldBeAssignableTo<IQueryable<Entity>>();
            result.ShouldBe(_fixture.Query);
        }

        [Fact]
        public void WhereEmpty_IEnumerable_ShouldReturnEqualCollection()
        {
            var result = _fixture.Collection.Where(_emptySpecification);
            result.ShouldNotBeAssignableTo<IQueryable<Entity>>();
            result.ShouldBe(_fixture.Collection);
        }

        [Fact]
        public void Any_IQueryable_ShouldBeTrue()
        {
            _fixture.Query
                .Any(_value1ShouldBe3)
                .ShouldBeTrue();
        }

        [Fact]
        public void Any_IQueryable_ShouldBeFalse()
        {
            _fixture.Query
                .Any(_value1ShouldBe4)
                .ShouldBeFalse();
        }

        [Fact]
        public void Any_IEnumerable_ShouldBeTrue()
        {
            _fixture.Collection
                .Any(_value1ShouldBe3)
                .ShouldBeTrue();
        }

        [Fact]
        public void Any_IEnumerable_ShouldBeFalse()
        {
            _fixture.Collection
                .Any(_value1ShouldBe4)
                .ShouldBeFalse();
        }

        [Fact]
        public void AnyEmpty_IQueryable_ShouldBeTrue()
        {
            _fixture.Query
                .Any(_emptySpecification)
                .ShouldBeTrue();
        }

        [Fact]
        public void AnyEmpty_IEnumerable_ShouldBeTrue()
        {
            _fixture.Collection
                .Any(_emptySpecification)
                .ShouldBeTrue();
        }

        [Fact]
        public void All_IQueryable_ShouldBeTrue()
        {
            _fixture.Query
                .All(_value2ShouldBe1)
                .ShouldBeTrue();
        }

        [Fact]
        public void All_IQueryable_ShouldBeFalse()
        {
            _fixture.Query
                .All(_value1ShouldBe3)
                .ShouldBeFalse();
        }

        [Fact]
        public void All_IEnumerable_ShouldBeTrue()
        {
            _fixture.Collection
                .All(_value2ShouldBe1)
                .ShouldBeTrue();
        }

        [Fact]
        public void All_IEnumerable_ShouldBeFalse()
        {
            _fixture.Collection
                .All(_value1ShouldBe3)
                .ShouldBeFalse();
        }

        [Fact]
        public void AllEmpty_IQueryable_ShouldBeTrue()
        {
            _fixture.Query
                .All(_emptySpecification)
                .ShouldBeTrue();
        }

        [Fact]
        public void AllEmpty_IEnumerable_ShouldBeTrue()
        {
            _fixture.Collection
                .All(_emptySpecification)
                .ShouldBeTrue();
        }

        [Fact]
        public void Count_IQueryable_ShouldReturnCorrectResult()
        {
            _fixture.Query
                .Count(_value1ShouldBe3)
                .ShouldBe(2);
        }

        [Fact]
        public void CountEmpty_IQueryable_ShouldReturnCorrectResult()
        {
            _fixture.Query
                .Count(_emptySpecification)
                .ShouldBe(_fixture.Collection.Count());
        }

        [Fact]
        public void Count_IEnumerable_ShouldReturnCorrectResult()
        {
            _fixture.Collection
                .Count(_value1ShouldBe3)
                .ShouldBe(2);
        }

        [Fact]
        public void CountEmpty_IEnumerable_ShouldReturnEqualCount()
        {
            _fixture.Collection
                .Count(_emptySpecification)
                .ShouldBe(_fixture.Collection.Count());
        }

        [Fact]
        public void First_IQueryable_ShouldReturnCorrectResult()
        {
            _fixture.Query
                .First(_value1ShouldBe3)
                .ShouldBe(_fixture.Store[0]);
        }

        [Fact]
        public void FirstEmpty_IQueryable_ShouldReturnCorrectResult()
        {
            _fixture.Query
                .First(_emptySpecification)
                .ShouldBe(_fixture.Store[0]);
        }

        [Fact]
        public void First_IEnumerable_ShouldReturnCorrectResult()
        {
            _fixture.Collection
                .First(_value1ShouldBe3)
                .ShouldBe(_fixture.Store[0]);
        }

        [Fact]
        public void FirstEmpty_IEnumerable_ShouldReturnCorrectResult()
        {
            _fixture.Collection
                .First(_emptySpecification)
                .ShouldBe(_fixture.Store[0]);
        }

        [Fact]
        public void FirstOrDefault_IQueryable_ShouldReturnCorrectResult()
        {
            _fixture.Query
                .FirstOrDefault(_value1ShouldBe3)
                .ShouldBe(_fixture.Store[0]);
        }

        [Fact]
        public void FirstOrDefaultEmpty_IQueryable_ShouldReturnCorrectResult()
        {
            _fixture.Query
                .FirstOrDefault(_emptySpecification)
                .ShouldBe(_fixture.Store[0]);
        }

        [Fact]
        public void FirstOrDefault_IEnumerable_ShouldReturnCorrectResult()
        {
            _fixture.Collection
                .FirstOrDefault(_value1ShouldBe3)
                .ShouldBe(_fixture.Store[0]);
        }

        [Fact]
        public void FirstOrDefaultEmpty_IEnumerable_ShouldReturnCorrectResult()
        {
            _fixture.Collection
                .FirstOrDefault(_emptySpecification)
                .ShouldBe(_fixture.Store[0]);
        }

        [Fact]
        public void Single_IQueryable_ShouldReturnCorrectResult()
        {
            _fixture.Query
                .Single(_value1ShouldBe1)
                .ShouldBe(_fixture.Store[2]);
        }

        [Fact]
        public void SingleEmpty_IQueryable_ShouldReturnCorrectResult()
        {
            Should.Throw<InvalidOperationException>(() => _fixture.Query.Single(_emptySpecification));
        }

        [Fact]
        public void Single_IEnumerable_ShouldReturnCorrectResult()
        {
            _fixture.Collection
                .Single(_value1ShouldBe1)
                .ShouldBe(_fixture.Store[2]);
        }

        [Fact]
        public void SingleEmpty_IEnumerable_ShouldReturnCorrectResult()
        {
            Should.Throw<InvalidOperationException>(() => _fixture.Collection.Single(_emptySpecification));
        }

        [Fact]
        public void SingleOrDefault_IQueryable_ShouldReturnCorrectResult()
        {
            _fixture.Query
                .SingleOrDefault(_value1ShouldBe1)
                .ShouldBe(_fixture.Store[2]);
        }

        [Fact]
        public void SingleOrDefaultEmpty_IQueryable_ShouldReturnCorrectResult()
        {
            Should.Throw<InvalidOperationException>(() => _fixture.Query.SingleOrDefault(_emptySpecification));
        }

        [Fact]
        public void SingleOrDefault_IEnumerable_ShouldReturnCorrectResult()
        {
            _fixture.Collection
                .SingleOrDefault(_value1ShouldBe1)
                .ShouldBe(_fixture.Store[2]);
        }

        [Fact]
        public void SingleOrDefaultEmpty_IEnumerable_ShouldReturnCorrectResult()
        {
            Should.Throw<InvalidOperationException>(() => _fixture.Collection.SingleOrDefault(_emptySpecification));
        }
    }
}
