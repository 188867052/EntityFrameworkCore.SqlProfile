using EntityFrameworkCore.SqlProfile;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Api
{
    public class SqliteDBContext : DbContext
    {
        private static bool _created = false;
        public SqliteDBContext()
        {
            if (!_created)
            {
                _created = true;
                Database.EnsureDeleted();
                Database.EnsureCreated();
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().ToTable("tb_Category");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionbuilder)
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new EntityFrameworkLoggerProvider());
            optionbuilder.UseSqlite(@"Data Source=.\sqliteDB.db").UseLoggerFactory(loggerFactory).EnableSensitiveDataLogging();
        }
    }
}
