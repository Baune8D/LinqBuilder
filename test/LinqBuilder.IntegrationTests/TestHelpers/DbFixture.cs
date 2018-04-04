using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LinqBuilder.IntegrationTests.TestHelpers
{
    public class DbFixture
    {
        private readonly DbContextOptions<TestDbContext> _options;

        public DbFixture()
        {
            _options = new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;
        }

        public TestDbContext Create()
        {
            return new TestDbContext(_options);
        }

        public void Delete()
        {
            using (var context = Create())
            {
                context.Database.EnsureDeleted();
            }
        }

        public void Seed()
        {
            using (var context = Create())
            {
                context.Add(new Entity
                {
                    Id = 1,
                    Value1 = 1,
                    Value2 = 2,
                    ChildEntities = new List<ChildEntity>
                    {
                        new ChildEntity
                        {
                            Value1 = 3
                        }
                    }
                });

                context.Add(new Entity
                {
                    Id = 2,
                    Value1 = 1,
                    Value2 = 1,
                    ChildEntities = new List<ChildEntity>
                    {
                        new ChildEntity
                        {
                            Value1 = 5
                        }
                    }
                });

                context.Add(new Entity
                {
                    Id = 3,
                    Value1 = 2,
                    Value2 = 1
                });

                context.Add(new Entity
                {
                    Id = 4,
                    Value1 = 3,
                    Value2 = 1,
                    ChildEntities = new List<ChildEntity>
                    {
                        new ChildEntity
                        {
                            Value1 = 5
                        }
                    }
                });

                context.Add(new Entity
                {
                    Id = 5,
                    Value1 = 3,
                    Value2 = 2
                });

                context.SaveChanges();
            }
        }
    }
}
