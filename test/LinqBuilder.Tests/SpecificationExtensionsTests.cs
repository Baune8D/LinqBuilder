using LinqBuilder.Core;
using LinqBuilder.Tests.TestHelpers;
using Shouldly;
using Xunit;

namespace LinqBuilder.Tests
{
    public class SpecificationExtensionsTests
    {
        private readonly ISpecification<Entity> _value1ShouldBe3 = Spec<Entity>.New(entity => entity.Value1 == 3);
        private readonly ISpecification<Entity> _value1ShouldBe5 = Spec<Entity>.New(entity => entity.Value1 == 5);
        private readonly ISpecification<Entity> _value2ShouldBe5 = Spec<Entity>.New(entity => entity.Value2 == 5);

        [Theory]
        [ClassData(typeof(TestData))]
        public void IsSatisfiedBy_Theory(Entity entity, bool expected)
        {
            _value1ShouldBe3
                .IsSatisfiedBy(entity)
                .ShouldBe(expected);
        }

        [Theory]
        [ClassData(typeof(AndTestData))]
        public void And_IsSatisfiedBy_Theory(Entity entity, bool expected)
        {
            _value1ShouldBe3
                .And(_value2ShouldBe5)
                .IsSatisfiedBy(entity)
                .ShouldBe(expected);
        }

        [Theory]
        [ClassData(typeof(OrTestData))]
        public void Or_IsSatisfiedBy_Theory(Entity entity, bool expected)
        {
            _value1ShouldBe3
                .Or(_value1ShouldBe5)
                .IsSatisfiedBy(entity)
                .ShouldBe(expected);
        }

        [Theory]
        [ClassData(typeof(NotTestData))]
        public void Not_IsSatisfiedBy_Theory(Entity entity, bool expected)
        {
            _value1ShouldBe5
                .Not()
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

        private class AndTestData : EntityTheoryData
        {
            public AndTestData()
            {
                AddEntity(3, 5, true);
                AddEntity(3, 4, false);
            }
        }

        private class OrTestData : EntityTheoryData
        {
            public OrTestData()
            {
                AddEntity(3, 1, true);
                AddEntity(5, 1, true);
                AddEntity(4, 1, false);
            }
        }

        private class NotTestData : EntityTheoryData
        {
            public NotTestData()
            {
                AddEntity(3, 1, true);
                AddEntity(5, 1, false);
            }
        }
    }
}
