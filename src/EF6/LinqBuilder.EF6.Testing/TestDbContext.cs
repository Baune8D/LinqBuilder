using System.Data.Common;
using System.Data.Entity;
using SQLite.CodeFirst;

namespace LinqBuilder.EF6.Testing
{
    public class TestDbContext : DbContext
    {
        private static DbModelBuilder? _modelBuilder;

        public TestDbContext(DbConnection connection)
            : base(connection, true)
        {
            if (_modelBuilder == null)
            {
                return;
            }

            var model = _modelBuilder.Build(Database.Connection);
            var sqliteDatabaseCreator = new SqliteDatabaseCreator();
            sqliteDatabaseCreator.Create(Database, model);
        }

        public virtual DbSet<SomeEntity> Entities { get; set; } = null!;

        public virtual DbSet<SomeChildEntity> ChildEntities { get; set; } = null!;

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var sqliteConnectionInitializer = new SqliteDropCreateDatabaseAlways<TestDbContext>(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);
            _modelBuilder = modelBuilder;
            base.OnModelCreating(modelBuilder);
        }
    }
}
