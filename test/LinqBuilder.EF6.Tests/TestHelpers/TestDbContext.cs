using System.Data.Common;
using System.Data.Entity;
using SQLite.CodeFirst;

namespace LinqBuilder.EF6.Tests.TestHelpers
{
    public class TestDbContext : DbContext
    {
        private static DbModelBuilder _modelBuilder; 

        public TestDbContext(DbConnection connection) : base(connection, true)
        {
            if (_modelBuilder == null) return;
            var model = _modelBuilder.Build(Database.Connection);
            var sqliteDatabaseCreator = new SqliteDatabaseCreator();
            sqliteDatabaseCreator.Create(Database, model);
        }

        public virtual DbSet<Entity> Entities { get; set; }
        public virtual DbSet<ChildEntity> ChildEntities { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var sqliteConnectionInitializer = new SqliteDropCreateDatabaseAlways<TestDbContext>(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);
            _modelBuilder = modelBuilder;
            base.OnModelCreating(modelBuilder);
        }
    }
}
