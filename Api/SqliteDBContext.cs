using Microsoft.EntityFrameworkCore;

namespace Api
{
    public class SqliteDbContext : DbContext
    {
        private static bool _created = false;
        public SqliteDbContext()
        {
            if (!_created)
            {
                _created = true;
                Database.EnsureDeleted();
                Database.EnsureCreated();
            }
        }

        public SqliteDbContext(DbContextOptions<SqliteDbContext> options)
           : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().ToTable("tb_Category");
        }
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Guid { get; set; }
        public string Time { get; set; }
    }
}
