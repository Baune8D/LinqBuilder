using Microsoft.EntityFrameworkCore;

namespace LinqBuilder.EFCore.Tests.Shared
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public virtual DbSet<Entity> Entities { get; set; }

        public virtual DbSet<ChildEntity> ChildEntities { get; set; }
    }
}
