using Microsoft.EntityFrameworkCore;

namespace LinqBuilder.EFCore.Testing
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public virtual DbSet<SomeEntity> Entities { get; set; } = null!;

        public virtual DbSet<SomeChildEntity> ChildEntities { get; set; } = null!;
    }
}
