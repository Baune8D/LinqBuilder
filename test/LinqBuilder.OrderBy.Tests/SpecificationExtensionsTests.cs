using System;
using System.Linq;
using LinqBuilder.Core;
using LinqBuilder.OrderBy.Tests.TestHelpers;
using Shouldly;
using Xunit;

namespace LinqBuilder.OrderBy.Tests
{
    public class SpecificationExtensionsTests
    {
        private readonly ISpecification<Entity> _value1ShouldBe1 = Spec<Entity>.New(entity => entity.Value1 == 1);
        private readonly IOrderSpecification<Entity> _emptyOrderSpecification = OrderSpec<Entity, int>.New();
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
            var specification = _emptyOrderSpecification.OrderBy(_orderValue1Asc);
            var result = _fixture.Query.ExeSpec(specification).ToList();
            result.Count.ShouldBe(4);
            result[0].Value1.ShouldBe(1);
            result[1].Value1.ShouldBe(2);
            result[2].Value1.ShouldBe(2);
            result[3].Value1.ShouldBe(3);
        }

        [Fact]
        public void OrderBy_IEnumerable_ShouldReturnCorrectResult()
        {
            var specification = _emptyOrderSpecification.OrderBy(_orderValue1Asc);
            var result = _fixture.Collection.ExeSpec(specification).ToList();
            result.Count.ShouldBe(4);
            result[0].Value1.ShouldBe(1);
            result[1].Value1.ShouldBe(2);
            result[2].Value1.ShouldBe(2);
            result[3].Value1.ShouldBe(3);
        }

        [Fact]
        public void ThenBy_IQueryable_ShouldReturnCorrectResult()
        {
            var specification = _emptyOrderSpecification.OrderBy(_orderValue1Asc).ThenBy(_orderValue2Asc);
            var result = _fixture.Query.ExeSpec(specification).ToList();
            result.Count.ShouldBe(4);
            result[0].Value1.ShouldBe(1);
            result[1].Value1.ShouldBe(2);
            result[1].Value2.ShouldBe(1);
            result[2].Value1.ShouldBe(2);
            result[2].Value2.ShouldBe(2);
            result[3].Value1.ShouldBe(3);
        }

        [Fact]
        public void ThenBy_IEnumerable_ShouldReturnCorrectResult()
        {
            var specification = _emptyOrderSpecification.OrderBy(_orderValue1Asc).ThenBy(_orderValue2Asc);
            var result = _fixture.Collection.ExeSpec(specification).ToList();
            result.Count.ShouldBe(4);
            result[0].Value1.ShouldBe(1);
            result[1].Value1.ShouldBe(2);
            result[1].Value2.ShouldBe(1);
            result[2].Value1.ShouldBe(2);
            result[2].Value2.ShouldBe(2);
            result[3].Value1.ShouldBe(3);
        }

        [Fact]
        public void ThenByOrdered_IQueryable_ShouldReturnCorrectResult()
        {
            var specification = _orderValue1Asc.ThenBy(_orderValue2Asc);
            var result = _fixture.Query.ExeSpec(specification).ToList();
            result.Count.ShouldBe(4);
            result[0].Value1.ShouldBe(1);
            result[1].Value1.ShouldBe(2);
            result[1].Value2.ShouldBe(1);
            result[2].Value1.ShouldBe(2);
            result[2].Value2.ShouldBe(2);
            result[3].Value1.ShouldBe(3);
        }

        [Fact]
        public void ThenByOrdered_IEnumerable_ShouldReturnCorrectResult()
        {
            var specification = _orderValue1Asc.ThenBy(_orderValue2Asc);
            var result = _fixture.Collection.ExeSpec(specification).ToList();
            result.Count.ShouldBe(4);
            result[0].Value1.ShouldBe(1);
            result[1].Value1.ShouldBe(2);
            result[1].Value2.ShouldBe(1);
            result[2].Value1.ShouldBe(2);
            result[2].Value2.ShouldBe(2);
            result[3].Value1.ShouldBe(3);
        }

        [Fact]
        public void UseOrdering_IQueryable_ShouldReturnCorrectResult()
        {
            var ordering = _orderValue1Asc.Skip(1).Take(1);
            var specification = _emptyOrderSpecification.UseOrdering(ordering);
            var result = _fixture.Query.ExeSpec(specification).ToList();
            result.Count.ShouldBe(1);
            result[0].Value1.ShouldBe(2);
        }

        [Fact]
        public void UseOrdering_IEnumerable_ShouldReturnCorrectResult()
        {
            var ordering = _orderValue1Asc.Skip(1).Take(1);
            var specification = _emptyOrderSpecification.UseOrdering(ordering);
            var result = _fixture.Collection.ExeSpec(specification).ToList();
            result.Count.ShouldBe(1);
            result[0].Value1.ShouldBe(2);
        }

        [Fact]
        public void IsOrdered_OrderedSpecification_ShouldBeTrue()
        {
            ISpecification<Entity> specification = _value1ShouldBe1.OrderBy(_orderValue1Asc);
            specification
                .IsOrdered()
                .ShouldBeTrue();
        }

        [Fact]
        public void IsOrdered_Specification_ShouldBeTrue()
        {
            _orderValue1Asc
                .IsOrdered()
                .ShouldBeTrue();
        }

        [Fact]
        public void IsOrdered_Specification_ShouldBeFalse()
        {
            _value1ShouldBe1
                .IsOrdered()
                .ShouldBeFalse();
        }

        [Fact]
        public void HasSkip_Specification_ShouldBeTrue()
        {
            _value1ShouldBe1
                .Skip(10)
                .HasSkip()
                .ShouldBeTrue();
        }

        [Fact]
        public void HasSkip_Specification_ShouldBeFalse()
        {
            _value1ShouldBe1
                .HasSkip()
                .ShouldBeFalse();
        }

        [Fact]
        public void HasTake_Specification_ShouldBeTrue()
        {
            _value1ShouldBe1
                .Take(10)
                .HasTake()
                .ShouldBeTrue();
        }

        [Fact]
        public void HasTake_Specification_ShouldBeFalse()
        {
            _value1ShouldBe1
                .HasTake()
                .ShouldBeFalse();
        }

        [Fact]
        public void Skip_IQueryable_ShouldReturnCorrectResult()
        {
            var result = _fixture.Query.ExeSpec(_orderValue1Asc.Skip(1)).ToList();
            result.Count.ShouldBe(3);
            result[0].Value1.ShouldBe(2);
            result[1].Value1.ShouldBe(2);
            result[2].Value1.ShouldBe(3);
        }

        [Fact]
        public void Skip_IEnumerable_ShouldReturnCorrectResult()
        {
            var result = _fixture.Collection.ExeSpec(_orderValue1Asc.Skip(1)).ToList();
            result.Count.ShouldBe(3);
            result[0].Value1.ShouldBe(2);
            result[1].Value1.ShouldBe(2);
            result[2].Value1.ShouldBe(3);
        }

        [Fact]
        public void Take_IQueryable_ShouldReturnCorrectResult()
        {
            var result = _fixture.Query.ExeSpec(_orderValue1Asc.Take(2)).ToList();
            result.Count.ShouldBe(2);
            result[0].Value1.ShouldBe(1);
            result[1].Value1.ShouldBe(2);
        }

        [Fact]
        public void Take_IEnumerable_ShouldReturnCorrectResult()
        {
            var result = _fixture.Collection.ExeSpec(_orderValue1Asc.Take(2)).ToList();
            result.Count.ShouldBe(2);
            result[0].Value1.ShouldBe(1);
            result[1].Value1.ShouldBe(2);
        }

        [Fact]
        public void Paginate_PageNoAndSize_ShouldHaveCorrectValues()
        {
            var configuration = _orderValue1Asc.Paginate(2, 5).Internal;
            configuration.OrderSpecifications.Count.ShouldBe(1);
            configuration.Skip.ShouldBe(5);
            configuration.Take.ShouldBe(5);
        }

        [Fact]
        public void Paginate_InvalidPageNo_ShouldThrowArgumentException()
        {
            Should.Throw<ArgumentException>(() => _orderValue1Asc.Paginate(0, 5));
        }

        [Fact]
        public void Paginate_InvalidPageSize_ShouldThrowArgumentException()
        {
            Should.Throw<ArgumentException>(() => _orderValue1Asc.Paginate(1, 0));
        }
    }
}
