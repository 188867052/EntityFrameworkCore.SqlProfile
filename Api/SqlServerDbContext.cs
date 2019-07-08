using Microsoft.EntityFrameworkCore;
using System;

namespace Api
{
    public class SqlServerDbContext : DbContext
    {
        public SqlServerDbContext()
        {
        }

        public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Log>(entity =>
            {
                entity.ToTable("log");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.CreateTime).HasColumnName("create_time").HasColumnType("datetime").HasDefaultValueSql("(getdate())");
                entity.Property(e => e.LogLevel).HasColumnName("log_level");
                entity.Property(e => e.Message).HasColumnName("message").HasColumnType("text");
                entity.Property(e => e.SqlOperateType).HasColumnName("sql_operate_type");
            });
        }
    }

    public class Log
    {
        public int Id { get; set; }

        public DateTime CreateTime { get; set; }

        public string Message { get; set; }

        public int LogLevel { get; set; }

        public int SqlOperateType { get; set; }
    }
}
