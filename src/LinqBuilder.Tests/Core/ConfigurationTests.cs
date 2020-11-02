using System.Collections.Generic;
using System.Linq;
using LinqBuilder.Core;
using LinqBuilder.OrderBy;
using LinqBuilder.Tests.Core.Internal;
using Shouldly;
using Xunit;

namespace LinqBuilder.Tests.Core
{
    public class ConfigurationTests
    {
        [Fact]
        public void Configuration_NewOrderSpecification_ShouldBeCorrectlyConfigured()
        {
            var configuration = new InternalConfiguration<Entity>(new OrderSpecification<Entity, int>(x => x.Value1));
            configuration.QuerySpecification.Internal.ShouldNotBeNull();
            configuration.QuerySpecification.ShouldNotBeNull();
            configuration.QuerySpecification.AsExpression().ShouldBeNull();
            configuration.QuerySpecification.AsFunc().ShouldBeNull();
            configuration.OrderSpecifications.Count.ShouldBe(1);
        }

        [Fact]
        public void Configuration_NewSpecification_ShouldBeCorrectlyConfigured()
        {
            var configuration = new InternalConfiguration<Entity>(new Specification<Entity>());
            configuration.QuerySpecification.Internal.ShouldNotBeNull();
            configuration.QuerySpecification.ShouldNotBeNull();
            configuration.QuerySpecification.AsExpression().ShouldBeNull();
            configuration.QuerySpecification.AsFunc().ShouldBeNull();
            configuration.OrderSpecifications.Any().ShouldBeFalse();
        }

        [Fact]
        public void Configuration_NewOrderSpecificationAndNewSpecification_ShouldBeCorrectlyConfigured()
        {
            var configuration = new InternalConfiguration<Entity>(new Specification<Entity>(), new List<IOrderSpecification<Entity>>
            {
                new OrderSpecification<Entity, int>(x => x.Value1),
            });
            configuration.QuerySpecification.Internal.ShouldNotBeNull();
            configuration.QuerySpecification.ShouldNotBeNull();
            configuration.QuerySpecification.AsExpression().ShouldBeNull();
            configuration.QuerySpecification.AsFunc().ShouldBeNull();
            configuration.OrderSpecifications.Count.ShouldBe(1);
        }
    }
}
