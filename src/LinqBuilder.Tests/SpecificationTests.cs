using FluentAssertions;
using LinqBuilder.Tests.Data;
using Xunit;

namespace LinqBuilder.Tests
{
    public class SpecificationTests
    {
        private readonly ISpecification<Entity> _emptySpecification = Spec<Entity>.New();
        private readonly ISpecification<Entity> _value1ShouldBe1 = Spec<Entity>.New(entity => entity.Value1 == 1);
        private readonly ISpecification<Entity> _value2ShouldBe2 = Spec<Entity>.New(entity => entity.Value2 == 2);

        [Fact]
        public void Constructor_DefaultExpression_ShouldBeTrue()
        {
            _emptySpecification
                .IsSatisfiedBy(new Entity())
                .Should().BeTrue();
        }

        [Fact]
        public void Constructor_InlineExpression_ShouldBeTrue()
        {
            _value1ShouldBe1
                .IsSatisfiedBy(new Entity { Value1 = 1 })
                .Should().BeTrue();
        }

        [Fact]
        public void Constructor_InlineExpression_ShouldBeFalse()
        {
            _value1ShouldBe1
                .IsSatisfiedBy(new Entity { Value1 = 2 })
                .Should().BeFalse();
        }

        [Fact]
        public void New_DefaultExpression_ShouldBeTrue()
        {
            _emptySpecification
                .IsSatisfiedBy(new Entity())
                .Should().BeTrue();
        }

        [Fact]
        public void New_InlineExpression_ShouldBeTrue()
        {
            _value1ShouldBe1
                .IsSatisfiedBy(new Entity { Value1 = 1 })
                .Should().BeTrue();
        }

        [Fact]
        public void New_InlineExpression_ShouldBeFalse()
        {
            _value1ShouldBe1
                .IsSatisfiedBy(new Entity { Value1 = 2 })
                .Should().BeFalse();
        }

        [Fact]
        public void All_Specifications_ShouldBeTrue()
        {
            Specification<Entity>.All(_value1ShouldBe1, _value2ShouldBe2)
                .IsSatisfiedBy(new Entity { Value1 = 1, Value2 = 2 });
        }

        [Fact]
        public void All_Specifications_ShouldBeFalse()
        {
            Specification<Entity>.All(_value1ShouldBe1, _value2ShouldBe2)
                .IsSatisfiedBy(new Entity { Value1 = 1, Value2 = 1 });
        }

        [Fact]
        public void None_Specifications_ShouldBeTrue()
        {
            Specification<Entity>.None(_value1ShouldBe1, _value2ShouldBe2)
                .IsSatisfiedBy(new Entity { Value1 = 2, Value2 = 1 });
        }

        [Fact]
        public void None_Specifications_ShouldBeFalse()
        {
            Specification<Entity>.None(_value1ShouldBe1, _value2ShouldBe2)
                .IsSatisfiedBy(new Entity { Value1 = 1, Value2 = 2 });
        }

        [Fact]
        public void Any_Specifications_ShouldBeTrue()
        {
            Specification<Entity>.Any(_value1ShouldBe1, _value2ShouldBe2)
                .IsSatisfiedBy(new Entity { Value1 = 1, Value2 = 1 });
        }

        [Fact]
        public void Any_Specifications_ShouldBeFalse()
        {
            Specification<Entity>.Any(_value1ShouldBe1, _value2ShouldBe2)
                .IsSatisfiedBy(new Entity { Value1 = 2, Value2 = 1 });
        }
    }
}
