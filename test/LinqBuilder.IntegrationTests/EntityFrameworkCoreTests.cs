using System;
using System.Collections.Generic;
using System.Linq;
using LinqBuilder.IntegrationTests.TestHelpers;
using LinqBuilder.OrderSpecifications;
using LinqBuilder.Specifications;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace LinqBuilder.IntegrationTests
{
    public class EntityFrameworkCoreTests : IClassFixture<DbFixture>, IDisposable
    {
        private readonly DbFixture _dbFixture;

        public EntityFrameworkCoreTests(DbFixture dbFixture)
        {
            _dbFixture = dbFixture;
            _dbFixture.Seed();
        }

        [Fact]
        public void ExeSpec_ComplexSpecification_ShouldReturnCorrectList()
        {
            var specifiction = new Specification<Entity>()
                .And(new Value1Specification(1)
                    .Or(new Value1Specification(3)))
                .OrderBy(new Value1OrderSpecification())
                .ThenBy(new Value2OrderSpecification(Order.Descending))
                .Skip(1)
                .Take(2);

            List<Entity> result;
            using (var context = _dbFixture.Create())
            {
                result = context.TestData.ExeSpec(specifiction).ToList();
            }

            result.Count.ShouldBe(2);
            result[0].Id.ShouldBe(2);
            result[0].Value1.ShouldBe(1);
            result[0].Value2.ShouldBe(1);
            result[1].Id.ShouldBe(5);
            result[1].Value1.ShouldBe(3);
            result[1].Value2.ShouldBe(2);
        }

        [Fact]
        public void ExeSpec_ComplexSpecification_ShouldReturnCorrectList2()
        {
            var specifiction = new Specification<Entity>()
                .And(new ChildValue1Specification(5));

            List<Entity> result;
            using (var context = _dbFixture.Create())
            {
                result = context.TestData.Include(x => x.ChildEntities).ExeSpec(specifiction).ToList();
            }

            result.Count.ShouldBe(1);
            result[0].Id.ShouldBe(2);
        }

        public void Dispose()
        {
            _dbFixture.Delete();
        }
    }
}
