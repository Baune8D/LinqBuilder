using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace LinqBuilder.EF6.Tests.TestHelpers
{
    public class DbFixture : IDisposable
    {
        public DbFixture()
        {
            Context = CreateContext();
        }

        public TestDbContext Context { get; private set; }

        // Tear down in-memory database.
        public void Dispose()
        {
            Context?.Dispose();
            Context = null;
        }

        public void AddEntity(int value1, int value2, int? childValue = null)
        {
            var entity = new Entity
            {
                Value1 = value1,
                Value2 = value2
            };

            if (childValue.HasValue)
            {
                entity.ChildEntities = new List<ChildEntity>
                {
                    new ChildEntity
                    {
                        Value = childValue.Value
                    }
                };
            }

            Context.Entities.Add(entity);
        }

        private static TestDbContext CreateContext()
        {
            var connection = CreateSqliteConnection();
            connection.Open();
            return new TestDbContext(connection);
        }

        private static SQLiteConnection CreateSqliteConnection()
        {
            var connectionStringBuilder = new SQLiteConnectionStringBuilder
            {
                DataSource = ":memory:",
                ForeignKeys = true
            };
            var connectionString = connectionStringBuilder.ConnectionString;
            return new SQLiteConnection(connectionString);
        }
    }
}
