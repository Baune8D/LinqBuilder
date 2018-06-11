using LinqBuilder.Tests.TestHelpers;
using Shouldly;
using Xunit;

namespace LinqBuilder.Tests
{
    public class DynamicSpecificationTests
    {
        [Theory]
        [ClassData(typeof(TestData))]
        public void IsSatisfiedBy_Theory(Entity entity, bool expected)
        {
            new MultipleValueSpecification4().Set(3, 5, 4, 2)
                .IsSatisfiedBy(entity)
                .ShouldBe(expected);
        }

        [Fact]
        public void Constructor_MultipleValueSpecification1_ShouldHaveCorrectValue()
        {
            var specification = new MultipleValueSpecification1(1);
            specification.Value.ShouldBe(1);
        }

        [Fact]
        public void Constructor_MultipleValueSpecification2_ShouldHaveCorrectValue()
        {
            var specification = new MultipleValueSpecification2(1, 2);
            specification.Value1.ShouldBe(1);
            specification.Value2.ShouldBe(2);
        }

        [Fact]
        public void Constructor_MultipleValueSpecification3_ShouldHaveCorrectValue()
        {
            var specification = new MultipleValueSpecification3(1, 2, 3);
            specification.Value1.ShouldBe(1);
            specification.Value2.ShouldBe(2);
            specification.Value3.ShouldBe(3);
        }

        [Fact]
        public void Constructor_MultipleValueSpecification4_ShouldHaveCorrectValue()
        {
            var specification = new MultipleValueSpecification4(1, 2, 3, 4);
            specification.Value1.ShouldBe(1);
            specification.Value2.ShouldBe(2);
            specification.Value3.ShouldBe(3);
            specification.Value4.ShouldBe(4);
        }

        private class TestData : EntityTheoryData
        {
            public TestData()
            {
                AddEntity(3, 5, 4, 2, true);
                AddEntity(3, 4, 4, 2, false);
            }
        }
    }
}
