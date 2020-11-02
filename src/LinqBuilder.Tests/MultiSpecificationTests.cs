using System;
using LinqBuilder.Tests.Internal;
using Shouldly;
using Xunit;

namespace LinqBuilder.Tests
{
    public class MultiSpecificationTests
    {
        [Fact]
        public void IsSatisfiedBy_MultiEntitySpecification2_Default()
        {
            new MultiEntitySpecification2()
                .IsSatisfiedBy(new Entity { Value1 = 1 });
        }

        [Fact]
        public void IsSatisfiedBy_MultiEntitySpecification2_Entity()
        {
            new MultiEntitySpecification2()
                .For<Entity>()
                .IsSatisfiedBy(new Entity { Value1 = 1 });
        }

        [Fact]
        public void IsSatisfiedBy_MultiEntitySpecification2_Entity2()
        {
            new MultiEntitySpecification2()
                .For<Entity2>()
                .IsSatisfiedBy(new Entity2 { Value2 = 2 });
        }

        [Fact]
        public void IsSatisfiedBy_MultiEntitySpecification2_ShouldThrowException()
        {
            Should.Throw<Exception>(() => new MultiEntitySpecification2()
                .For<Entity3>()
                .IsSatisfiedBy(new Entity3()));
        }

        [Fact]
        public void IsSatisfiedBy_MultiEntitySpecification3_Default()
        {
            new MultiEntitySpecification3()
                .IsSatisfiedBy(new Entity { Value1 = 1 });
        }

        [Fact]
        public void IsSatisfiedBy_MultiEntitySpecification3_Entity()
        {
            new MultiEntitySpecification3()
                .For<Entity>()
                .IsSatisfiedBy(new Entity { Value1 = 1 });
        }

        [Fact]
        public void IsSatisfiedBy_MultiEntitySpecification3_Entity2()
        {
            new MultiEntitySpecification3()
                .For<Entity2>()
                .IsSatisfiedBy(new Entity2 { Value2 = 2 });
        }

        [Fact]
        public void IsSatisfiedBy_MultiEntitySpecification3_Entity3()
        {
            new MultiEntitySpecification3()
                .For<Entity3>()
                .IsSatisfiedBy(new Entity3 { Value3 = 3 });
        }

        [Fact]
        public void IsSatisfiedBy_MultiEntitySpecification3_ShouldThrowException()
        {
            Should.Throw<Exception>(() => new MultiEntitySpecification3()
                .For<Entity4>()
                .IsSatisfiedBy(new Entity4()));
        }

        [Fact]
        public void IsSatisfiedBy_MultiEntitySpecification4_Default()
        {
            new MultiEntitySpecification4()
                .IsSatisfiedBy(new Entity { Value1 = 1 });
        }

        [Fact]
        public void IsSatisfiedBy_MultiEntitySpecification4_Entity()
        {
            new MultiEntitySpecification4()
                .For<Entity>()
                .IsSatisfiedBy(new Entity { Value1 = 1 });
        }

        [Fact]
        public void IsSatisfiedBy_MultiEntitySpecification4_Entity2()
        {
            new MultiEntitySpecification4()
                .For<Entity2>()
                .IsSatisfiedBy(new Entity2 { Value2 = 2 });
        }

        [Fact]
        public void IsSatisfiedBy_MultiEntitySpecification4_Entity3()
        {
            new MultiEntitySpecification4()
                .For<Entity3>()
                .IsSatisfiedBy(new Entity3 { Value3 = 3 });
        }

        [Fact]
        public void IsSatisfiedBy_MultiEntitySpecification4_Entity4()
        {
            new MultiEntitySpecification4()
                .For<Entity4>()
                .IsSatisfiedBy(new Entity4 { Value4 = 4 });
        }

        [Fact]
        public void IsSatisfiedBy_MultiEntitySpecification4_ShouldThrowException()
        {
            Should.Throw<Exception>(() => new MultiEntitySpecification4()
                .For<Entity5>()
                .IsSatisfiedBy(new Entity5()));
        }
    }
}
