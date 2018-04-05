using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using LinqBuilder.EntityFramework.Tests.TestHelpers;
using LinqBuilder.OrderBy;
using Shouldly;
using Xunit;

namespace LinqBuilder.EntityFramework.Tests
{
    public class IntegrationTests : IDisposable
    {
        private readonly DbFixture _dbFixture;

        public IntegrationTests()
        {
            _dbFixture = new DbFixture();
            Seed();
        }

        [Fact]
        public async Task ExeQueryAsync_ChildSpecification_ShouldReturnCorrectList()
        {
            var specifiction = new ChildValueSpecification(1)
                .Or(new ChildValueSpecification(2));
            
            var result = await _dbFixture.Context.Entities
                .ExeQuery(specifiction)
                .ToListAsync();

            result.Count.ShouldBe(2);
            result[0].Id.ShouldBe(1);
            result[1].Id.ShouldBe(3);
        }

        private void Seed()
        {
            AddEntity(2, 1, 2); // 1
            AddEntity(1, 2, 3); // 2
            AddEntity(3, 1, 1); // 3
            _dbFixture.Context.SaveChanges();
        }

        private void AddEntity(int value1, int value2, int childValue)
        {
            _dbFixture.Context.Entities.Add(new Entity
            {
                Value1 = value1,
                Value2 = value2,
                ChildEntities = new List<ChildEntity>
                {
                    new ChildEntity
                    {
                        Value = childValue
                    }
                }
            });
        }

        public void Dispose()
        {
            _dbFixture.Dispose();
        }
    }
}
