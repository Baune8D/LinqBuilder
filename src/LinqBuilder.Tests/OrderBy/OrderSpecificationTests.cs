using System.Linq;
using LinqBuilder.OrderBy;
using LinqBuilder.Testing;
using Shouldly;
using Xunit;

namespace LinqBuilder.Tests.OrderBy
{
    public class OrderSpecificationTests
    {
        private readonly IOrderSpecification<Entity> _orderValue1Asc = OrderSpec<Entity, int>.New(entity => entity.Value1);
        private readonly IOrderSpecification<Entity> _orderValue1Desc = OrderSpec<Entity, int>.New(entity => entity.Value1, Sort.Descending);

        private readonly Fixture _fixture;

        public OrderSpecificationTests()
        {
            _fixture = new Fixture();
            _fixture.AddToCollection(3, 1, 1);
            _fixture.AddToCollection(1, 1, 1);
            _fixture.AddToCollection(2, 1, 1);
        }

        [Fact]
        public void Constructor_InlineExpression__ShouldReturnOrderedList()
        {
            var result = _fixture.Query.OrderBy(_orderValue1Asc).ToList();
            result[0].Value1.ShouldBe(1);
            result[1].Value1.ShouldBe(2);
            result[2].Value1.ShouldBe(3);
        }

        [Fact]
        public void Constructor_InlineExpression_ShouldReturnOrderedList()
        {
            var result = _fixture.Query.OrderBy(_orderValue1Desc).ToList();
            result[0].Value1.ShouldBe(3);
            result[1].Value1.ShouldBe(2);
            result[2].Value1.ShouldBe(1);
        }

        [Fact]
        public void New_InlineExpression__ShouldReturnOrderedList()
        {
            var result = _fixture.Query.OrderBy(_orderValue1Asc).ToList();
            result[0].Value1.ShouldBe(1);
            result[1].Value1.ShouldBe(2);
            result[2].Value1.ShouldBe(3);
        }

        [Fact]
        public void New_InlineExpression_ShouldReturnOrderedList()
        {
            var result = _fixture.Query.OrderBy(_orderValue1Desc).ToList();
            result[0].Value1.ShouldBe(3);
            result[1].Value1.ShouldBe(2);
            result[2].Value1.ShouldBe(1);
        }

        [Fact]
        public void InvokeSort_IQueryableAscending_ShouldReturnOrderedList()
        {
            var result = _orderValue1Asc.InvokeSort(_fixture.Query).ToList();
            result[0].Value1.ShouldBe(1);
            result[1].Value1.ShouldBe(2);
            result[2].Value1.ShouldBe(3);
        }

        [Fact]
        public void InvokeSort_IEnumerableAscending_ShouldReturnOrderedList()
        {
            var result = _orderValue1Asc.InvokeSort(_fixture.Collection).ToList();
            result[0].Value1.ShouldBe(1);
            result[1].Value1.ShouldBe(2);
            result[2].Value1.ShouldBe(3);
        }

        [Fact]
        public void InvokeSort_IQueryableDescending_ShouldReturnOrderedList()
        {
            var result = _orderValue1Desc.InvokeSort(_fixture.Query).ToList();
            result[0].Value1.ShouldBe(3);
            result[1].Value1.ShouldBe(2);
            result[2].Value1.ShouldBe(1);
        }

        [Fact]
        public void InvokeSort_IEnumerableDescending_ShouldReturnOrderedList()
        {
            var result = _orderValue1Desc.InvokeSort(_fixture.Collection).ToList();
            result[0].Value1.ShouldBe(3);
            result[1].Value1.ShouldBe(2);
            result[2].Value1.ShouldBe(1);
        }
    }
}
