using System.Linq;
using LinqBuilder.OrderBy;
using LinqBuilder.Testing;
using Shouldly;
using Xunit;

namespace LinqBuilder.Tests
{
    public class LinqExtensionsExeSpecTests
    {
        private readonly ISpecification<Entity> _value1ShouldBe2 = Spec<Entity>.New(entity => entity.Value1 == 2);
        private readonly IOrderSpecification<Entity> _orderValue1Asc = OrderSpec<Entity, int>.New(entity => entity.Value1);
        private readonly IOrderSpecification<Entity> _orderValue2Asc = OrderSpec<Entity, int>.New(entity => entity.Value2);

        private readonly Fixture _fixture;

        public LinqExtensionsExeSpecTests()
        {
            _fixture = new Fixture();
            _fixture.AddToCollection(3, 1);
            _fixture.AddToCollection(1, 1);
            _fixture.AddToCollection(2, 2);
            _fixture.AddToCollection(2, 1);
        }

        [Fact]
        public void ExeSpecNoSort_IQueryable_ShouldReturnCorrectResult()
        {
            var result = _fixture.Query
                .ExeSpec(_value1ShouldBe2)
                .ToList();

            result.Count.ShouldBe(2);
            result[0].Value1.ShouldBe(2);
            result[0].Value2.ShouldBe(2);
            result[1].Value1.ShouldBe(2);
            result[1].Value2.ShouldBe(1);
        }

        [Fact]
        public void ExeSpecNoSort_IEnumerable_ShouldReturnCorrectResult()
        {
            var result = _fixture.Collection
                .ExeSpec(_value1ShouldBe2)
                .ToList();

            result.Count.ShouldBe(2);
            result[0].Value1.ShouldBe(2);
            result[0].Value2.ShouldBe(2);
            result[1].Value1.ShouldBe(2);
            result[1].Value2.ShouldBe(1);
        }

        [Fact]
        public void ExeSpecSort_IQueryable_ShouldReturnCorrectResult()
        {
            var result = _fixture.Query
                .ExeSpec(_value1ShouldBe2.OrderBy(_orderValue2Asc))
                .ToList();

            result.Count.ShouldBe(2);
            result[0].Value1.ShouldBe(2);
            result[0].Value2.ShouldBe(1);
            result[1].Value1.ShouldBe(2);
            result[1].Value2.ShouldBe(2);
        }

        [Fact]
        public void ExeSpecSort_IEnumerable_ShouldReturnCorrectResult()
        {
            var result = _fixture.Collection
                .ExeSpec(_value1ShouldBe2.OrderBy(_orderValue2Asc))
                .ToList();

            result.Count.ShouldBe(2);
            result[0].Value1.ShouldBe(2);
            result[0].Value2.ShouldBe(1);
            result[1].Value1.ShouldBe(2);
            result[1].Value2.ShouldBe(2);
        }

        [Fact]
        public void ExeSpecSkipSort_IQueryable_ShouldReturnCorrectResult()
        {
            var result = _fixture.Query
                .ExeSpec(_value1ShouldBe2.OrderBy(_orderValue2Asc), true)
                .ToList();

            result.Count.ShouldBe(2);
            result[0].Value1.ShouldBe(2);
            result[0].Value2.ShouldBe(2);
            result[1].Value1.ShouldBe(2);
            result[1].Value2.ShouldBe(1);
        }

        [Fact]
        public void ExeSpecSkipSort_IEnumerable_ShouldReturnCorrectResult()
        {
            var result = _fixture.Collection
                .ExeSpec(_value1ShouldBe2.OrderBy(_orderValue2Asc), true)
                .ToList();

            result.Count.ShouldBe(2);
            result[0].Value1.ShouldBe(2);
            result[0].Value2.ShouldBe(2);
            result[1].Value1.ShouldBe(2);
            result[1].Value2.ShouldBe(1);
        }

        [Fact]
        public void ExeSpecOnlySort_IQueryable_ShouldReturnCorrectResult()
        {
            var result = _fixture.Query
                .ExeSpec(_orderValue1Asc)
                .ToList();

            result.Count.ShouldBe(4);
            result[0].Value1.ShouldBe(1);
            result[1].Value1.ShouldBe(2);
            result[1].Value2.ShouldBe(2);
            result[2].Value1.ShouldBe(2);
            result[2].Value2.ShouldBe(1);
            result[3].Value1.ShouldBe(3);
        }

        [Fact]
        public void ExeSpecOnlySort_IEnumerable_ShouldReturnCorrectResult()
        {
            var result = _fixture.Collection
                .ExeSpec(_orderValue1Asc)
                .ToList();

            result.Count.ShouldBe(4);
            result[0].Value1.ShouldBe(1);
            result[1].Value1.ShouldBe(2);
            result[1].Value2.ShouldBe(2);
            result[2].Value1.ShouldBe(2);
            result[2].Value2.ShouldBe(1);
            result[3].Value1.ShouldBe(3);
        }

        [Fact]
        public void ExeSpecMultipleSort_IQueryable_ShouldReturnCorrectResult()
        {
            var result = _fixture.Query
                .ExeSpec(_orderValue1Asc.ThenBy(_orderValue2Asc))
                .ToList();

            result.Count.ShouldBe(4);
            result[0].Value1.ShouldBe(1);
            result[1].Value1.ShouldBe(2);
            result[1].Value2.ShouldBe(1);
            result[2].Value1.ShouldBe(2);
            result[2].Value2.ShouldBe(2);
            result[3].Value1.ShouldBe(3);
        }

        [Fact]
        public void ExeSpecMultipleSort_IEnumerable_ShouldReturnCorrectResult()
        {
            var result = _fixture.Collection
                .ExeSpec(_orderValue1Asc.ThenBy(_orderValue2Asc))
                .ToList();

            result.Count.ShouldBe(4);
            result[0].Value1.ShouldBe(1);
            result[1].Value1.ShouldBe(2);
            result[1].Value2.ShouldBe(1);
            result[2].Value1.ShouldBe(2);
            result[2].Value2.ShouldBe(2);
            result[3].Value1.ShouldBe(3);
        }

        [Fact]
        public void ExeSpecSkipTake_IQueryable_ShouldReturnCorrectResult()
        {
            var result = _fixture.Query
                .ExeSpec(_orderValue1Asc.Skip(1).Take(2))
                .ToList();

            result.Count.ShouldBe(2);
            result[0].Value1.ShouldBe(2);
            result[0].Value2.ShouldBe(2);
            result[1].Value1.ShouldBe(2);
            result[1].Value2.ShouldBe(1);
        }

        [Fact]
        public void ExeSpecSkipTake_IEnumerable_ShouldReturnCorrectResult()
        {
            var result = _fixture.Collection
                .ExeSpec(_orderValue1Asc.Skip(1).Take(2))
                .ToList();

            result.Count.ShouldBe(2);
            result[0].Value1.ShouldBe(2);
            result[0].Value2.ShouldBe(2);
            result[1].Value1.ShouldBe(2);
            result[1].Value2.ShouldBe(1);
        }
    }
}
