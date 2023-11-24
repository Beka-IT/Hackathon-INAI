using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using WebApi.Entities;

namespace WebApi
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Queue> Queues { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<OperationType> Operations { get; set; }
        public DbSet<DepartmentOperations> DepartmentOperations { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Branch>()
                .HasMany(e => e.Departments)
                .WithOne(e => e.Branch)
                .HasForeignKey(e => e.BranchId)
                .IsRequired();
        }
    }
}
