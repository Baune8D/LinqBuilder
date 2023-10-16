using System;
using System.Linq;
using FluentAssertions;
using LinqBuilder.OrderBy;
using LinqBuilder.Tests.Data;
using Xunit;

namespace LinqBuilder.Tests.OrderBy
{
    public class SpecificationExtensionsTests
    {
        private readonly ISpecification<Entity> _emptySpecification = Spec<Entity>.New();
        private readonly ISpecification<Entity> _value1ShouldBe1 = Spec<Entity>.New(entity => entity.Value1 == 1);
        private readonly IOrderSpecification<Entity> _orderValue1Asc = OrderSpec<Entity, int>.New(entity => entity.Value1);
        private readonly IOrderSpecification<Entity> _orderValue2Asc = OrderSpec<Entity, int>.New(entity => entity.Value2);

        private readonly Fixture _fixture;

        public SpecificationExtensionsTests()
        {
            _fixture = new Fixture();
            _fixture.AddToCollection(3, 1, 1);
            _fixture.AddToCollection(1, 1, 1);
            _fixture.AddToCollection(2, 2, 1);
            _fixture.AddToCollection(2, 1, 1);
        }

        [Fact]
        public void OrderBy_IQueryable_ShouldReturnCorrectResult()
        {
            var specification = _emptySpecification.OrderBy(_orderValue1Asc);
            var result = _fixture.Query.ExeSpec(specification).ToList();
            result.Count.Should().Be(4);
            result[0].Value1.Should().Be(1);
            result[1].Value1.Should().Be(2);
            result[2].Value1.Should().Be(2);
            result[3].Value1.Should().Be(3);
        }

        [Fact]
        public void OrderBy_IEnumerable_ShouldReturnCorrectResult()
        {
            var specification = _emptySpecification.OrderBy(_orderValue1Asc);
            var result = _fixture.Collection.ExeSpec(specification).ToList();
            result.Count.Should().Be(4);
            result[0].Value1.Should().Be(1);
            result[1].Value1.Should().Be(2);
            result[2].Value1.Should().Be(2);
            result[3].Value1.Should().Be(3);
        }

        [Fact]
        public void ThenBy_IQueryable_ShouldReturnCorrectResult()
        {
            var specification = _emptySpecification.OrderBy(_orderValue1Asc).ThenBy(_orderValue2Asc);
            var result = _fixture.Query.ExeSpec(specification).ToList();
            result.Count.Should().Be(4);
            result[0].Value1.Should().Be(1);
            result[1].Value1.Should().Be(2);
            result[1].Value2.Should().Be(1);
            result[2].Value1.Should().Be(2);
            result[2].Value2.Should().Be(2);
            result[3].Value1.Should().Be(3);
        }

        [Fact]
        public void ThenBy_IEnumerable_ShouldReturnCorrectResult()
        {
            var specification = _emptySpecification.OrderBy(_orderValue1Asc).ThenBy(_orderValue2Asc);
            var result = _fixture.Collection.ExeSpec(specification).ToList();
            result.Count.Should().Be(4);
            result[0].Value1.Should().Be(1);
            result[1].Value2.Should().Be(1);
            result[2].Value1.Should().Be(2);
            result[2].Value2.Should().Be(2);
            result[3].Value1.Should().Be(3);
        }

        [Fact]
        public void ThenByOrdered_IQueryable_ShouldReturnCorrectResult()
        {
            var specification = _orderValue1Asc.ThenBy(_orderValue2Asc);
            var result = _fixture.Query.ExeSpec(specification).ToList();
            result.Count.Should().Be(4);
            result[0].Value1.Should().Be(1);
            result[1].Value1.Should().Be(2);
            result[1].Value2.Should().Be(1);
            result[2].Value1.Should().Be(2);
            result[2].Value2.Should().Be(2);
            result[3].Value1.Should().Be(3);
        }

        [Fact]
        public void ThenByOrdered_IEnumerable_ShouldReturnCorrectResult()
        {
            var specification = _orderValue1Asc.ThenBy(_orderValue2Asc);
            var result = _fixture.Collection.ExeSpec(specification).ToList();
            result.Count.Should().Be(4);
            result[0].Value1.Should().Be(1);
            result[1].Value1.Should().Be(2);
            result[1].Value2.Should().Be(1);
            result[2].Value1.Should().Be(2);
            result[2].Value2.Should().Be(2);
            result[3].Value1.Should().Be(3);
        }

        [Fact]
        public void UseOrdering_IQueryable_ShouldReturnCorrectResult()
        {
            var ordering = _orderValue1Asc.Skip(1).Take(1);
            var specification = _emptySpecification.UseOrdering(ordering);
            var result = _fixture.Query.ExeSpec(specification).ToList();
            result.Count.Should().Be(1);
            result[0].Value1.Should().Be(2);
        }

        [Fact]
        public void UseOrdering_IEnumerable_ShouldReturnCorrectResult()
        {
            var ordering = _orderValue1Asc.Skip(1).Take(1);
            var specification = _emptySpecification.UseOrdering(ordering);
            var result = _fixture.Collection.ExeSpec(specification).ToList();
            result.Count.Should().Be(1);
            result[0].Value1.Should().Be(2);
        }

        [Fact]
        public void Skip_IQueryable_ShouldReturnCorrectResult()
        {
            var result = _fixture.Query.ExeSpec(_orderValue1Asc.Skip(1)).ToList();
            result.Count.Should().Be(3);
            result[0].Value1.Should().Be(2);
            result[1].Value1.Should().Be(2);
            result[2].Value1.Should().Be(3);
        }

        [Fact]
        public void Skip_IEnumerable_ShouldReturnCorrectResult()
        {
            var result = _fixture.Collection.ExeSpec(_orderValue1Asc.Skip(1)).ToList();
            result.Count.Should().Be(3);
            result[0].Value1.Should().Be(2);
            result[1].Value1.Should().Be(2);
            result[2].Value1.Should().Be(3);
        }

        [Fact]
        public void Take_IQueryable_ShouldReturnCorrectResult()
        {
            var result = _fixture.Query.ExeSpec(_orderValue1Asc.Take(2)).ToList();
            result.Count.Should().Be(2);
            result[0].Value1.Should().Be(1);
            result[1].Value1.Should().Be(2);
        }

        [Fact]
        public void Take_IEnumerable_ShouldReturnCorrectResult()
        {
            var result = _fixture.Collection.ExeSpec(_orderValue1Asc.Take(2)).ToList();
            result.Count.Should().Be(2);
            result[0].Value1.Should().Be(1);
            result[1].Value1.Should().Be(2);
        }

        [Fact]
        public void Paginate_PageNoAndSize_ShouldHaveCorrectValues()
        {
            var configuration = _orderValue1Asc.Paginate(2, 5).Internal;
            configuration.OrderSpecifications.Count.Should().Be(1);
            configuration.Skip.Should().Be(5);
            configuration.Take.Should().Be(5);
        }

        [Fact]
        public void Paginate_InvalidPageNo_ShouldThrowArgumentException()
        {
            Action act = () => _orderValue1Asc.Paginate(0, 5);

            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Paginate_InvalidPageSize_ShouldThrowArgumentException()
        {
            Action act = () => _orderValue1Asc.Paginate(1, 0);

            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void IsOrdered_OrderedSpecification_ShouldBeTrue()
        {
            ISpecification<Entity> specification = _value1ShouldBe1.OrderBy(_orderValue1Asc);
            specification
                .IsOrdered()
                .Should().BeTrue();
        }

        [Fact]
        public void IsOrdered_Specification_ShouldBeTrue()
        {
            _orderValue1Asc
                .IsOrdered()
                .Should().BeTrue();
        }

        [Fact]
        public void IsOrdered_Specification_ShouldBeFalse()
        {
            _value1ShouldBe1
                .IsOrdered()
                .Should().BeFalse();
        }

        [Fact]
        public void HasSkip_Specification_ShouldBeTrue()
        {
            _value1ShouldBe1
                .Skip(10)
                .HasSkip()
                .Should().BeTrue();
        }

        [Fact]
        public void HasSkip_Specification_ShouldBeFalse()
        {
            _value1ShouldBe1
                .HasSkip()
                .Should().BeFalse();
        }

        [Fact]
        public void HasTake_Specification_ShouldBeTrue()
        {
            _value1ShouldBe1
                .Take(10)
                .HasTake()
                .Should().BeTrue();
        }

        [Fact]
        public void HasTake_Specification_ShouldBeFalse()
        {
            _value1ShouldBe1
                .HasTake()
                .Should().BeFalse();
        }

        [Fact]
        public void Clone_Specifications_ShouldNotBeEqual()
        {
            var spec1 = _orderValue1Asc.Paginate(2, 10);
            var spec2 = spec1.Clone();
            spec1.Should().NotBe(spec2);
            spec1.Internal.QuerySpecification.Should().Be(spec2.Internal.QuerySpecification);
            spec1.Internal.OrderSpecifications.Should().Equal(spec2.Internal.OrderSpecifications);
            spec1.Internal.Skip.Should().Be(spec2.Internal.Skip);
            spec1.Internal.Take.Should().Be(spec2.Internal.Take);
        }
    }
}
